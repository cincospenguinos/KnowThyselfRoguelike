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
      foreach (var point in grid.EnumerateRoom(room)) {
        grid.Tiles[point.x, point.y] = Grid.TileType.FLOOR;
      }
      // GridUtils.PutGround(grid, grid.EnumerateRoom(room));
    });

    // // sort rooms by distance to top-left, where the upstairs will be.
    // Vector2Int topLeft = new Vector2Int(0, grid.height);
    // rooms.OrderBy(room => Util.manhattanDistance(room.getTopLeft() - topLeft));

    // Room upstairsRoom = rooms.First();
    // // 1-px padding from the top-left of the room
    // Vector2Int upstairsPos = new Vector2Int(upstairsRoom.min.x + 1, upstairsRoom.max.y - 1);
    // grid.PlaceUpstairs(upstairsPos);

    // Room downstairsRoom = rooms.Last();
    // // 1-px padding from the bottom-right of the room
    // Vector2Int downstairsPos = new Vector2Int(downstairsRoom.max.x - 1, downstairsRoom.min.y + 1);
    // grid.PlaceDownstairs(downstairsPos);
    // grid.root = root;
    // grid.rooms = rooms;
    // grid.upstairsRoom = upstairsRoom;
    // grid.downstairsRoom = downstairsRoom;
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