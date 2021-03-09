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

  public void move(Direction direction) {
    switch(direction) {
      case Direction.NORTH:
        Coordinates.y += 1;
        break;
      case Direction.SOUTH:
        Coordinates.y -= 1;
        break;
      case Direction.EAST:
        Coordinates.x += 1;
        break;
      case Direction.WEST:
        Coordinates.x -= 1;
        break;
    }

    Debug.Log("player is now at " + Coordinates.x + ", " + Coordinates.y);
  }
}
