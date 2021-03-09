using UnityEngine;

public class Player {
  public enum Direction {
    NORTH, SOUTH, EAST, WEST, NONE
  };

  private Grid _world;
  private int _hitPoints;
  public Vector2Int Coordinates;

  public Player(Grid world) {
    _world = world;
    _hitPoints = 20;
    Coordinates = new Vector2Int(3, 3);
  }

  public bool move(Direction direction) {
    if (direction == Direction.NONE) {
      return false;
    }

    Vector2Int newCoordinates = adjacentIn(direction);

    if (_world.validPlayerMovement(newCoordinates)) {
      Coordinates = newCoordinates;
      return true;
    }

    return false;
  }

  public bool attack(Direction direction) {
    if (direction == Direction.NONE) {
      return false;
    }

    Vector2Int newCoordinates = adjacentIn(direction);
    Enemy enemy = _world.EnemyAt(newCoordinates);

    if (enemy != null) {
      enemy.GetShrekt(this);
      return true;
    }

    return false;
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
