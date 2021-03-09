using System;
using UnityEngine;

public class Enemy : Entity {
  bool hasSeenPlayer = false;

  public Enemy(Grid world, Vector2Int position) : base(world, position, 3) {
  }

  public void TakeTurn() {
    if (hasSeenPlayer) {
      /// run relentlessly at player

    } else {

      /// wait, or move randomly
    }
  }
}