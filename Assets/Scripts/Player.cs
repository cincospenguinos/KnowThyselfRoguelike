using UnityEngine;

public class Player {
  private Grid _world;
  private int _hitPoints;
  public Vector2Int Coordinates;

  public Player(Grid world) {
    _world = world;
    _hitPoints = 20;
    Coordinates = new Vector2Int(3, 3);
  }
}
