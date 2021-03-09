using System;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {
  NORTH, SOUTH, EAST, WEST
};

public class Entity {
  protected Grid _world;
  protected int _hitPoints;
  public Vector2Int Coordinates;
  public bool Dead => _hitPoints <= 0;
  public event Action OnDeath;
  public List<Rune> RuneList;
  
  public int DamageModifier = 0;
  public int DamageOut => 1 + DamageModifier;

  public Entity(Grid world, Vector2Int coords, int HitPoints) {
    _world = world;
    _hitPoints = HitPoints;
    Coordinates = coords;

    RuneList = new List<Rune>();
    RuneList.Add(new Rune(this));
  }

  public bool move(Direction direction) {
    Vector2Int newCoordinates = adjacentIn(direction);

    if (_world.canOccupy(newCoordinates)) {
      Coordinates = newCoordinates;
      return true;
    }

    return false;
  }

  public bool attack(Direction direction) {
    Vector2Int newCoordinates = adjacentIn(direction);
    Entity entity = _world.EntityAt(newCoordinates);

    if (entity != null) {
      entity.TakeDamage(DamageOut);
      return true;
    }

    return false;
  }

  public void EmitEvent(String eventName) {
    Debug.Log("Does this occur?" + eventName);
    foreach(var rune in RuneList) {
      rune.EventOccurred(eventName);
    }
  }

  public void GoDie() {
    OnDeath();
  }

  /// Taking damage from getting hit by the player
  public void TakeDamage(int damage) {
    _hitPoints -= damage;
  }

  private Vector2Int adjacentIn(Direction direction) {
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
}

public class Player : Entity {
  public Player(Grid world) : base(world, new Vector2Int(3, 3), 20) {}
}
