using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GridGenerator {
  public static Grid generateMultiRoomGrid(int numSplits) {
    Grid grid = new Grid();
    foreach (var point in grid.EnumerateFloor()) {
      grid.Tiles[point.x, point.y] = Grid.TileType.WALL;
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
        grid.Tiles[point.x, point.y] = Grid.TileType.FLOOR;
      }
    }

    rooms.ForEach(room => {
      // fill each room with floor
      bool addedEnemy = false;

      foreach (var point in grid.EnumerateRoom(room)) {
        grid.Tiles[point.x, point.y] = Grid.TileType.FLOOR;

        // Add an enemy in each room
        // TODO: Smarter enemy addition

        if (!addedEnemy) {
          grid.AddEnemy(new Enemy(point));
          addedEnemy = true;
        }
      }
    });

    return grid;
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