using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GridGenerator {
  public static Grid generateMultiRoomGrid(Player player, int depth, int numSplits = 10) {
    int numEnemies = 2 + ((depth - 1) / 2);

    var width = 18 + 3 * depth;
    var height = Mathf.RoundToInt(12 + 40f / 28f * depth);

    Grid grid = new Grid(player, depth, width, height);
    foreach (var point in grid.EnumerateFloor()) {
      grid.Tiles[point.x, point.y] = new Wall(grid, point);
    }

    // randomly partition space into rooms
    Room root = new Room(grid);
    for (int i = 0; i < numSplits; i++) {
      bool success = root.randomlySplit();
      if (!success) {
        Debug.LogWarning("couldn't split at iteration " + i);
        break;
      }
    }

    // collect all rooms
    List<Room> rooms = new List<Room>();
    root.Traverse(node => {
      if (node.isTerminal) {
        rooms.Add(node);
      }
    });

    // shrink it into a subset of the space available; adds more 'emptiness' to allow
    // for less rectangular shapes
    rooms.ForEach(room => room.randomlyShrink());

    foreach (var (r1, r2) in ComputeRoomConnections(rooms).Concat(BSPSiblingRoomConnections(rooms, root))) {
      /// draw lines 
      foreach (var point in grid.EnumerateManhattanLine(r1.center, r2.center).Where(p => grid.InBounds(p)).Take(1000)) {
        grid.Tiles[point.x, point.y] = new Floor(grid, point);
      }
    }

    rooms.ForEach(room => {
      // fill each room with floor
      foreach (var point in grid.EnumerateRoom(room)) {
        grid.Tiles[point.x, point.y] = new Floor(grid, point);
      }
    });

    var floors = grid.EnumerateFloor().Where(grid.canOccupy).ToList();
    var enemyPositions = floors.Shuffle().Take(numEnemies).ToList();
    for (int i = 0; i < numEnemies; i++) {
      var enemyType = new List<Type>() { typeof(Enemy0), typeof(Enemy1), typeof(Enemy2) }.GetRandom();

      if (enemyType == typeof(Enemy2)) {
        var pos = enemyPositions[0];
        SpawnEnemy(enemyType, pos);
        enemyPositions.RemoveAt(0);
        var adjacentPos = grid.EnumerateCircle(pos, 2).Where(grid.canOccupy).FirstOrDefault();
        if (adjacentPos != null) {
          SpawnEnemy(enemyType, adjacentPos);
          enemyPositions.Remove(adjacentPos);
        }

      } else {
        SpawnEnemy(enemyType, enemyPositions[0]);
        enemyPositions.RemoveAt(0);
      }

      void SpawnEnemy(Type type, Vector2Int pos) {
        var constructor = enemyType.GetConstructor(new Type[] { typeof(Vector2Int) });
        var enemy = (Enemy) constructor.Invoke(new object[1] { pos });
        enemy.AddedDamage += (depth - 1);
        enemy.MaxHitPoints += (2 * depth / 3);
        enemy.CurrentHitPoints += (2 * depth / 3);

        grid.AddEntity(enemy);
        floors.Remove(pos);
      }
    }

    List<Room> blocklist = new List<Room>();

    var healAltarPos = randomPosInRoom(grid, rooms, blocklist);
    grid.AddEntity(new HealAltar(healAltarPos));
    floors.Remove(healAltarPos);

    var runeEditAltarPos = randomPosInRoom(grid, rooms, blocklist);
    grid.AddEntity(new RuneEditAltar(runeEditAltarPos));
    floors.Remove(runeEditAltarPos);

    if (depth % 2 == 0) {
      var upgradeAltarPos = randomPosInRoom(grid, rooms, blocklist);
      grid.AddEntity(new UpgradeAltar(upgradeAltarPos));
      floors.Remove(upgradeAltarPos);
    }

    var downstairsPos = randomPosInRoom(grid, rooms, blocklist);
    grid.Tiles[downstairsPos.x, downstairsPos.y] = new Downstairs(grid, downstairsPos);

    // put player in bottom-left corner
    var playerStartPos = randomPosInRoom(grid, rooms, blocklist);
    player.SetCoordinates(playerStartPos);

    return grid;
  }

  private static Vector2Int randomPosInRoom(Grid grid, List<Room> rooms, List<Room> blocklist) {
    Room room = rooms.FindAll(r => !blocklist.Contains(r))
            .ToList()
            .GetRandom();
    var pos = grid.EnumerateRoom(room, -1).ToList().GetRandom();
    blocklist.Add(room);
    return pos;
  }

  /// Connect all the rooms together with at least one through-path
  // private static List<(Vector2Int, Vector2Int)> ComputeRoomConnections(List<Room> rooms, Room root) {
  //   return BSPSiblingRoomConnections(rooms, root);
  // }

  private static List<(Room, Room)> ComputeRoomConnections(List<Room> rooms) {
    var neighborMap = new Dictionary<Room, List<Room>>();
    // initialize empty lists
    foreach (var room in rooms) {
      neighborMap.Add(room, new List<Room>());
    }

    // connect each room to its 2 closest neighbor rooms
    foreach (var r1 in rooms) {
      var r1List = neighborMap[r1];

      while (r1List.Count < 2) {
        var roomsByDistance = rooms
          /// don't connect to yourself
          .Where(r => r != r1)
          /// don't connect to already existing neighbors
          .Except(r1List)
          /// sort by distance
          .OrderBy(r2 => Vector2.Distance(r1.center, r2.center));
        var newNeighbors = roomsByDistance.Take(2 - r1List.Count);
        foreach (var r2 in newNeighbors) {
          // double-connect
          r1List.Add(r2);
          neighborMap[r2].Add(r1);
        }
      }
    }

    var list = new List<(Room, Room)>();
    foreach (var tuple in neighborMap) {
      foreach (var room2 in tuple.Value) {
        list.Add( (tuple.Key, room2) );
      }
    }
    return list;
  }

  /// draw a path connecting siblings together, including intermediary nodes (guarantees connectedness)
  /// this tends to draw long lines that cut right through single thickness walls
  private static List<(Room, Room)> BSPSiblingRoomConnections(List<Room> rooms, Room root) {
    List<(Room, Room)> paths = new List<(Room, Room)>();
    root.Traverse(node => {
      if (!node.isTerminal) {
        Vector2Int nodeCenter = node.getCenter();
        RoomSplit split = node.split.Value;
        split.a.connections.Add(split.b);
        split.b.connections.Add(split.a);
        Vector2Int aCenter = split.a.getCenter();
        Vector2Int bCenter = split.b.getCenter();
        paths.Add((node, split.a));
        paths.Add((node, split.b));
        // paths.Add((nodeCenter, aCenter));
        // paths.Add((nodeCenter, bCenter));
      }
    });
    return paths;
  }

  // public static bool AreStairsConnected(Grid Grid) {
  //   var path = Grid.FindPath(Grid.downstairs.pos, Grid.upstairs.pos);
  //   return path.Any();
  // }
}

public static class ListExtensions {
  public static IList<T> Shuffle<T>(this IList<T> list) {
    int n = list.Count;  
    while (n > 1) {  
      n--;  
      int k = UnityEngine.Random.Range(0, n + 1);  
      T value = list[k];  
      list[k] = list[n];  
      list[n] = value;  
    }  
    return list;
  }

  public static T GetRandom<T>(this IList<T> list) {
    return list[UnityEngine.Random.Range(0, list.Count)];
  }
}