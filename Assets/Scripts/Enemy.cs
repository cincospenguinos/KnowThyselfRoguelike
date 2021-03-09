using System;
using UnityEngine;

public class Enemy {
  public Vector2Int Coordinates;
  public int HitPoints;
  public bool Dead => HitPoints <= 0;

  public event Action OnDeath;

  public Enemy(Vector2Int position) {
    Coordinates = position;
    HitPoints = 3;
  }

  /// Taking damage from getting hit by the player
  public void GetShrekt(Player player) {
    HitPoints -= 1;
  }

  public void GoDie() {
    OnDeath();
  }
}