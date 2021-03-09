using System;
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

  public Entity(Grid world, Vector2Int coords, int HitPoints) {
    _world = world;
    _hitPoints = HitPoints;
    Coordinates = coords;
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
      entity.TakeDamage();
      return true;
    }

    return false;
  }

  public void GoDie() {
    OnDeath();
  }

  /// Taking damage from getting hit by the player
  public void TakeDamage() {
    _hitPoints -= 1;
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
  public Player(Grid world) : base(world, new Vector2Int(3, 3), 20) {
  }
}
