using System.Collections.Generic;
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

    foreach (var (a, b) in ComputeRoomConnections(rooms, root)) {
      /// draw lines 
      foreach (var point in grid.EnumerateLine(a, b)) {
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
  private static List<(Vector2Int, Vector2Int)> ComputeRoomConnections(List<Room> rooms, Room root) {
    return BSPSiblingRoomConnections(rooms, root);
  }

  /// draw a path connecting siblings together, including intermediary nodes (guarantees connectedness)
  /// this tends to draw long lines that cut right through single thickness walls
  private static List<(Vector2Int, Vector2Int)> BSPSiblingRoomConnections(List<Room> rooms, Room root) {
    List<(Vector2Int, Vector2Int)> paths = new List<(Vector2Int, Vector2Int)>();
    root.Traverse(node => {
      if (!node.isTerminal) {
        Vector2Int nodeCenter = node.getCenter();
        RoomSplit split = node.split.Value;
        split.a.connections.Add(split.b);
        split.b.connections.Add(split.a);
        Vector2Int aCenter = split.a.getCenter();
        Vector2Int bCenter = split.b.getCenter();
        paths.Add((nodeCenter, aCenter));
        paths.Add((nodeCenter, bCenter));
      }
    });
    return paths;
  }

  // public static bool AreStairsConnected(Grid Grid) {
  //   var path = Grid.FindPath(Grid.downstairs.pos, Grid.upstairs.pos);
  //   return path.Any();
  // }
}