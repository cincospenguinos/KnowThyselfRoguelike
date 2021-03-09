using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class FloorUtils {

  // public static List<Tile> TilesSortedByCorners(Floor floor, Room room) {
  //   var tiles = floor.EnumerateRoomTiles(room).ToList();
  //   tiles.Sort((x, y) => (int)Mathf.Sign(Vector2.Distance(y.pos, room.centerFloat) - Vector2.Distance(x.pos, room.centerFloat)));
  //   return tiles;
  // }

  // internal static List<Tile> EmptyTilesInRoom(Floor floor, Room room) {
  //   return floor.EnumerateRoomTiles(room).Where(t => t.CanBeOccupied() && !(t is Downstairs || t is Upstairs)).ToList();
  // }

  // internal static List<Tile> TilesFromCenter(Floor floor, Room room) {
  //   return FloorUtils
  //     .EmptyTilesInRoom(floor, room)
  //     .OrderBy((t) => Vector2.Distance(t.pos, room.centerFloat))
  //     .ToList();
  // }

  // public static List<Tile> TilesAwayFromCenter(Floor floor, Room room) {
  //   var tiles = TilesFromCenter(floor, room);
  //   tiles.Reverse();
  //   return tiles;
  // }

  // public static IEnumerable<Vector2Int> Line3x3(Floor floor, Vector2Int start, Vector2Int end) {
  //     return floor.EnumerateLine(start, end).SelectMany((pos) => floor.GetAdjacentTiles(pos).Select(t => t.pos));
  // }

  // // surround floor perimeter with walls
  // public static void SurroundWithWalls(Floor floor) {
  //   foreach (var p in floor.EnumeratePerimeter()) {
  //     floor.Put(new Wall(p));
  //   }
  // }

  // public static void PutGround(Floor floor, IEnumerable<Vector2Int> points = null) {
  //   if (points == null) {
  //     points = floor.EnumerateFloor();
  //   }
  //   foreach (var pos in points) {
  //     floor.Put(new Ground(pos));
  //   }
  // }
}