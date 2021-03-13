using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {
  private List<RuneShard> _runePieceInventory;
  public int TurnsSinceHitByEnemy = 0;

  public Player() : base(new Vector2Int(3, 3), 20) {
    _runePieceInventory = new List<RuneShard>();
  }

  public override void onWalkInto(Player player) {
    // no op
  }

  public void AddRunePiece(RuneShard piece) {
    _runePieceInventory.Add(piece);
  }

  public override void TakeDamage(int damage) {
    base.TakeDamage(damage);
    TurnsSinceHitByEnemy = 0;
  }
}