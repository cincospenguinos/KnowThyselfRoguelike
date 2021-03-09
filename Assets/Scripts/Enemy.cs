using UnityEngine;

public class Enemy {
  public Vector2Int Coordinates;
  public int HitPoints;

  public Enemy(Vector2Int position) {
    Coordinates = position;
    HitPoints = 3;
  }
}