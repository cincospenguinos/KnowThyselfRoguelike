using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Tile {
  public Vector2Int Coordinates;
  protected Grid _grid;
  // +4/-4; show 9 vertical tiles
  public bool isVisible => ManhattanDistanceToPlayer <= _grid.Player.SightRange;

  public int ManhattanDistanceToPlayer {
    get {
      /// hack - make an oval shape; players can see longer in X. Matches the usual 
      var xScale = 9f / 16f;
      var distanceX = Mathf.RoundToInt(Mathf.Abs(_grid.Player.Coordinates.x - Coordinates.x) * xScale);
      var distanceY = Mathf.Abs(_grid.Player.Coordinates.y - Coordinates.y);
      return distanceX + distanceY;
    }
  }

  protected Tile(Grid grid, Vector2Int Coordinates) {
    _grid = grid;
    this.Coordinates = Coordinates;
  }
}

public class Floor : Tile
{
  public Floor(Grid grid, Vector2Int Coordinates) : base(grid, Coordinates)
  {
  }
}

public class Wall : Tile
{
  public Wall(Grid grid, Vector2Int Coordinates) : base(grid, Coordinates)
  {
  }
}

public class Downstairs : Tile {
  public Downstairs(Grid grid, Vector2Int Coordinates) : base(grid, Coordinates) {}
}

public class Grid {
  public static Grid instance;

  // [x][y]
  private Tile[,] _grid;
  public Player Player;
  public List<Entity> Entities;
  public IEnumerable<Enemy> Enemies => Entities.Where((e) => e is Enemy).Cast<Enemy>();
  private int _elapsedTurns;
  public event Action OnCleared;
  public event Action<Entity> OnEntityAdded;

  private Queue<GameEvent> _eventQueue;
  internal int depth;

  public Tile[,] Tiles => _grid;

  public readonly int WIDTH = 40;
  public readonly int HEIGHT = 28;

  /// min inclusive, max exclusive in terms of map WIDTH/HEIGHT
  public Vector2Int boundsMin => new Vector2Int(0, 0);
  public Vector2Int boundsMax => new Vector2Int(WIDTH, HEIGHT);
  public Vector2 center => new Vector2(WIDTH / 2.0f, HEIGHT / 2.0f);
  public int CurrentTurn => _elapsedTurns + 1;

  public Grid(Player player, int depth, int width, int height) {
    WIDTH = width;
    HEIGHT = height;
    this.depth = depth;
    Entities = new List<Entity>();
    _elapsedTurns = 0;
    _grid = new Tile[WIDTH, HEIGHT];
    for (var x = 0; x < WIDTH; x++) {
      for (var y = 0; y < HEIGHT; y++) {
        _grid[x, y] = new Wall(this, new Vector2Int(x, y));
      }
    }
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

  public IEnumerable<Vector2Int> EnumerateDiamond(Vector2Int center, int radius) {
    Vector2Int extent = new Vector2Int(radius + 1, radius + 1);
    foreach (var pos in EnumerateRectangle(center - extent, center + extent)) {
      var diamondDistance = Math.Abs(center.x - pos.x) + Math.Abs(center.y - pos.y);
      if (diamondDistance <= radius) {
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

  public IEnumerator actionTaken(bool delay, Action then) {
    _elapsedTurns += 1;
    EnqueueEvent(new GameEvent(GameEvent.EventType.TURN_ELAPSED));

    var turnWaitDuration = delay ? 0.25f : 0;
    // wait 0.2f seconds
    yield return new WaitForSeconds(turnWaitDuration);

    ClearEventQueue();

    Entities.FindAll(e => e.Dead).ForEach(e => {
      e.GoDie();

      Rune rune = e.RuneList[0];
      if (rune != null) {
        AnimUtils.ShowFloatingText("Rune Shard Recovered!", new Vector3(e.Coordinates.x, e.Coordinates.y, 0));
        if (UnityEngine.Random.value > 0.5f) {
          Player.AddRuneShard(rune.action);
        } else {
          Player.AddRuneShard(rune.trigger);
        }
      }

      this.EnqueueEvent(new GameEvent(GameEvent.EventType.ENEMY_DEAD));
    });

    Entities.RemoveAll(e => e.Dead);

    bool enemiesMoved = false;
    foreach (var e in Enemies) {
      e.TakeTurn();
      // if (e.isVisible) {
        enemiesMoved = true;
      //   yield return new WaitForSeconds(turnWaitDuration);
      // }
    }

    ClearEventQueue();

    // We want to prevent movement scumming, so if the player hasn't been hit in 75 turns, spawn some enemies
    if (++Player.TurnsSinceHitByEnemy >= 200) {
      var positions = EnumerateCircle(Player.Coordinates, 3)
        .Where(pos => canOccupy(pos))
        .ToList()
        .Shuffle()
        .Take(2);

      foreach(var pos in positions) {
        Enemy enemy = new Enemy0(pos);
        AddEntity(enemy);
        OnEntityAdded(enemy);
      }

      Player.TurnsSinceHitByEnemy = 0;      
    }

    if (enemiesMoved) {
      yield return new WaitForSeconds(turnWaitDuration);
    }

    /// Move onto the next floor!
    if (Tiles[Player.Coordinates.x, Player.Coordinates.y] is Downstairs) {
      Player.score += 10;
      OnCleared?.Invoke();
    }

    then();
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
      Entities.ForEach(e => e.EmitEvent(gameEvent));
      count += 1;

      if (count >= 1000) {
        throw new OverflowException("Infinite loop occurred! Need to fix!");
      }
    }
  }

  public void AddEntity(Entity e) {
    Entities.Add(e);
    e.SetGrid(this);
  }

  public Entity EntityAt(Vector2Int pos) {
    if (Player.Coordinates == pos) {
      return Player;
    }
    return Entities.Find(e => e.Coordinates == pos);
  }

  public bool canOccupy(Vector2Int pos) {
    if (!InBounds(pos)) {
      return false;
    }

    if (EntityAt(pos) != null) {
      return false;
    }

    return Tiles[pos.x, pos.y] is Floor || Tiles[pos.x, pos.y] is Downstairs;
  }
}