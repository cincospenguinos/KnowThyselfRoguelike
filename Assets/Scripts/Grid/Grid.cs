using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Grid {
  public enum TileType {
      FLOOR = 0,
      WALL = 1,
  };

  public static Grid instance;

  // [x][y]
  private TileType[,] _grid;
  public Player Player;
  public List<Enemy> Enemies;
  private int _elapsedTurns;
  public event Action OnCleared;

  private Queue<GameEvent> _eventQueue;

  public TileType[,] Tiles => _grid;

  public const int WIDTH = 40;
  public const int HEIGHT = 28;

  /// min inclusive, max exclusive in terms of map WIDTH/HEIGHT
  public Vector2Int boundsMin => new Vector2Int(0, 0);
  public Vector2Int boundsMax => new Vector2Int(WIDTH, HEIGHT);
  public Vector2 center => new Vector2(WIDTH / 2.0f, HEIGHT / 2.0f);
  public int CurrentTurn => _elapsedTurns + 1;

  public Grid(Player player) {
    Enemies = new List<Enemy>();
    _elapsedTurns = 0;
    _grid = new TileType[40,28];
    player.SetGrid(this);
    Player = player;
    _eventQueue = new Queue<GameEvent>();
  }

  public bool InBounds(Vector2Int pos) {
    return pos.x >= 0 && pos.y >= 0 && pos.x < WIDTH && pos.y < HEIGHT;
  }

  public IEnumerable<Vector2Int> EnumerateCircle(Vector2Int center, float radius) {
    Vector2Int extent = new Vector2Int(Mathf.CeilToInt(radius), Mathf.CeilToInt(radius));
    foreach (var pos in EnumerateRectangle(center - extent, center + extent)) {
      if (Vector2Int.Distance(pos, center) <= radius) {
        yield return pos;
      }
    }
  }

  /// max is exclusive
  public IEnumerable<Vector2Int> EnumerateRectangle(Vector2Int min, Vector2Int max) {
    min = Vector2Int.Max(min, boundsMin);
    max = Vector2Int.Min(max, boundsMax);
    for (int x = min.x; x < max.x; x++) {
      for (int y = min.y; y < max.y; y++) {
        yield return new Vector2Int(x, y);
      }
    }
  }

  public IEnumerable<Vector2Int> EnumeratePerimeter(int inset = 0) {
    // top edge, including top-left, excluding top-right
    for (int x = inset; x < WIDTH - inset - 1; x++) {
      yield return new Vector2Int(x, inset);
    }
    // right edge
    for (int y = inset; y < HEIGHT - inset - 1; y++) {
      yield return new Vector2Int(WIDTH - 1 - inset, y);
    }
    // bottom edge, now going right-to-left, now excluding bottom-left
    for (int x = WIDTH - inset - 1; x > inset; x--) {
      yield return new Vector2Int(x, HEIGHT - 1 - inset);
    }
    // left edge
    for (int y = HEIGHT - inset - 1; y > inset; y--) {
      yield return new Vector2Int(inset, y);
    }
  }

  public IEnumerable<Vector2Int> EnumerateRoom(Room room, int extrude = 0) {
    Vector2Int extrudeVector = new Vector2Int(extrude, extrude);
    return EnumerateRectangle(room.min - extrudeVector, room.max + new Vector2Int(1, 1) + extrudeVector);
  }


  public IEnumerable<Vector2Int> EnumerateFloor() {
    return this.EnumerateRectangle(boundsMin, boundsMax);
  }

  /// always starts right on the startPoint, and always ends right on the endPoint
  public IEnumerable<Vector2Int> EnumerateManhattanLine(Vector2Int start, Vector2Int end) {
    // Vector2 offset = endPoint - startPoint;
    // for (float t = 0; t <= offset.magnitude; t += 0.5f) {
    //   Vector2 point = startPoint + offset.normalized * t;
    //   Vector2Int p = new Vector2Int(Mathf.RoundToInt(point.x), Mathf.RoundToInt(point.y));
    //   if (InBounds(p)) {
    //     yield return p;
    //   }
    // }
    // if (InBounds(endPoint)) {
    //   yield return endPoint;
    // }

    /// for now, go horizontal first, then vertical, then horizontal

    var midX = (end.x + start.x) / 2;
    var dX = end.x > start.x ? 1 : -1;
    /// first horizontal half
    for (int x = start.x; x != midX; x += dX) {
      yield return new Vector2Int(x, start.y);
    }

    /// entire vertical
    var dY = end.y > start.y ? 1 : -1;
    for (int y = start.y; y != end.y; y += dY) {
      yield return new Vector2Int(midX, y);
    }

    /// second horizontal half
    for (int x = midX; x != end.x; x += dX) {
      yield return new Vector2Int(x, end.y);
    }
  }

  public void actionTaken() {
    _elapsedTurns += 1;
    EnqueueEvent(new GameEvent(GameEvent.EventType.TURN_ELAPSED));

    Enemies.FindAll(e => e.Dead).ForEach(e => {
      e.GoDie();
      this.EnqueueEvent(new GameEvent(GameEvent.EventType.ENEMY_DEAD));
    });

    Enemies.RemoveAll(e => e.Dead);

    ClearEventQueue();

    foreach (var e in Enemies) {
      e.TakeTurn();
    }

    ClearEventQueue();

    /// all enemies are dead, move onto the next floor!
    if (!Enemies.Any()) {
      OnCleared?.Invoke();
    }
  }

  /// Emit a game event first to the player and then to ever enemy on the
  /// board. Events are handled by runes in that order.
  public void EnqueueEvent(GameEvent gameEvent) {
    _eventQueue.Enqueue(gameEvent);
  }

  private void ClearEventQueue() {
    int count = 0;

    while (_eventQueue.Count > 0) {
      var gameEvent = _eventQueue.Dequeue();
      Player.EmitEvent(gameEvent);
      Enemies.ForEach(e => e.EmitEvent(gameEvent));
      count += 1;

      if (count >= 1000) {
        throw new OverflowException("Infinite loop occurred! Need to fix!");
      }
    }
  }

  public void AddEnemy(Enemy e) {
    Enemies.Add(e);
    e.SetGrid(this);
  }

  public Entity EntityAt(Vector2Int pos) {
    if (Player.Coordinates == pos) {
      return Player;
    }
    return Enemies.Find(e => e.Coordinates.x == pos.x && e.Coordinates.y == pos.y);
  }

  public bool canOccupy(Vector2Int pos) {
    if (!InBounds(pos)) {
      return false;
    }

    if (EntityAt(pos) != null) {
      return false;
    }

    return Tiles[pos.x, pos.y] == TileType.FLOOR;
  }
}