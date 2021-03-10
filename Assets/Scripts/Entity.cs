using System;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {
  NORTH, SOUTH, EAST, WEST
};

public class Entity {
  protected Grid _grid;

  protected int _currentHitPoints;
  protected int _maxHitPoints;

  public Vector2Int Coordinates;
  public event Action OnDeath;
  public List<Rune> RuneList;
  
  public int DamageModifier = 0;
  public int DamageOut => 1 + DamageModifier;
  public int HitPoints => _currentHitPoints;
  public int MaxHitPoints => _maxHitPoints;
  public bool Dead => _currentHitPoints <= 0;

  public Entity(Vector2Int coords, int HitPoints) {
    _currentHitPoints = HitPoints;
    _maxHitPoints = HitPoints;
    Coordinates = coords;

    RuneList = new List<Rune>();
    RuneList.Add(RuneGenerator.generateRandom(this));
  }

  public void SetGrid(Grid grid) {
    this._grid = grid;
  }

  public bool move(Vector2Int newCoordinates) {
    if (_grid.canOccupy(newCoordinates)) {
      Coordinates = newCoordinates;
      return true;
    }

    return false;
  }

  public bool attack(Entity entity) {
    if (entity != null) {
      entity.TakeDamage(DamageOut);
      return true;
    }
    return false;
  }

  public void EmitEvent(GameEvent gameEvent) {
    if (!Dead) {
      foreach(var rune in RuneList) {
        rune.EventOccurred(gameEvent);
      }
    }
  }

  public void GoDie() {
    OnDeath();
  }

  /// Taking damage from getting hit by the player
  public void TakeDamage(int damage) {
    _currentHitPoints -= damage;
  }

  public void Heal(int amount) {
    _currentHitPoints += amount;
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
}

public class Player : Entity {
  public Player() : base(new Vector2Int(3, 3), 20) {}
}
