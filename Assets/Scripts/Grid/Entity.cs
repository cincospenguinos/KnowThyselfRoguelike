using System;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {
  NORTH = 0, EAST = 1, SOUTH = 2, WEST = 3
};

public abstract class Entity {
  protected Grid _grid;
  public bool isVisible => _grid.Tiles[Coordinates.x, Coordinates.y].isVisible;

  public int CurrentHitPoints;

  private Vector2Int _Coordinates;
  /// set through SetCoordinates()
  public Vector2Int Coordinates => _Coordinates;
  public event Action OnDeath;
  public event System.Action<Entity> OnAttack;
  public event Action<int> OnHit;
  public event Action<int> OnHeal;
  public Rune[] RuneList = new Rune[3];

  public event Action<string> OnRuneTriggered;
  internal void RuneActionTriggered(string v) {
    OnRuneTriggered?.Invoke(v);
  }

  public int AddedDamage = 0;
  public abstract int BaseDamage { get; }
  public int TotalDamage => BaseDamage + AddedDamage;
  public int Block = 0;
  public int HitPoints => CurrentHitPoints;
  public int MaxHitPoints;
  public bool Dead => CurrentHitPoints <= 0;

  public int SightRange = 4;

  public int FreeAttacks = 0;

  public Entity(Vector2Int coords, int HitPoints) {
    CurrentHitPoints = HitPoints;
    MaxHitPoints = HitPoints;
    _Coordinates = coords;

    RuneList[0] = RuneGenerator.generateRandom(this);
  }

  public void SetGrid(Grid grid) {
    this._grid = grid;
  }

  /// this doesn't care about grid walkability; it just sets the field
  public virtual void SetCoordinates(Vector2Int c) {
    _Coordinates = c;
  }

  public virtual bool move(Vector2Int newCoordinates) {
    if (_grid.canOccupy(newCoordinates)) {
      SetCoordinates(newCoordinates);
      _grid.EnqueueEvent(new GameEvent(GameEvent.EventType.MOVEMENT, this));
      return true;
    }

    return false;
  }

  public bool attack(Entity entity) {
    if (entity != null) {
      OnAttack?.Invoke(entity);
      entity.TakeDamage(TotalDamage);
      _grid.EnqueueEvent(new GameEvent(GameEvent.EventType.DAMAGE_DEALT, this));

      if (FreeAttacks > 0) {
        FreeAttacks -= 1;
        return false;
      }

      return true;
    }
    return false;
  }

  public void EmitEvent(GameEvent gameEvent) {
    if (!Dead) {
      foreach(var rune in RuneList) {
        rune?.EventOccurred(gameEvent);
      }
    }
  }

  public void GoDie() {
    _grid.Player.score++;
    OnDeath();
  }

  /// Taking damage from getting hit by the player
  public virtual void TakeDamage(int damage) {
    damage = Math.Max(damage - Block, 0);
    OnHit?.Invoke(damage);
    bool wasAboveHalf = CurrentHitPoints >= MaxHitPoints / 2;
    CurrentHitPoints -= damage;
    Grid.instance.EnqueueEvent(new GameEvent(GameEvent.EventType.DAMAGE_RECEIVED, this));

    if (wasAboveHalf && CurrentHitPoints < MaxHitPoints / 2) {
      Grid.instance.EnqueueEvent(new GameEvent(GameEvent.EventType.REACH_HALF_HIT_POINTS, this));
    }
  }

  public void Heal(int amount) {
    if (amount == -1) {
      amount = MaxHitPoints - CurrentHitPoints;
    }


    if (CurrentHitPoints < MaxHitPoints) {
      amount = Math.Min(amount, MaxHitPoints - CurrentHitPoints);
      OnHeal?.Invoke(amount);
      CurrentHitPoints += amount;
      Grid.instance.EnqueueEvent(new GameEvent(GameEvent.EventType.HEAL, this));
    }
  }

  public Vector2Int adjacentIn(Direction direction) {
    Vector2Int newCoordinates = new Vector2Int(Coordinates.x, Coordinates.y);

    switch(direction) {
      case Direction.NORTH:
        newCoordinates.y += 1;
        break;
      case Direction.SOUTH:
        newCoordinates.y -= 1;
        break;
      case Direction.EAST:
        newCoordinates.x += 1;
        break;
      case Direction.WEST:
        newCoordinates.x -= 1;
        break;
    }

    return newCoordinates;
  }

  public bool isNextTo(Entity e) {
    var offset = Coordinates - e.Coordinates;
    return (Math.Abs(offset.x) + Math.Abs(offset.y) <= 1);
  }

  internal void wait() {
    // no-op for now
  }

  public abstract void onWalkInto(Player player);

  public float DistanceTo(Entity other) {
    return Vector2.Distance(Coordinates, other.Coordinates);
  }
}
