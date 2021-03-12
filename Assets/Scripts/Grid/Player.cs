using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {
  private List<RunePiece> _runePieceInventory;

  public Player() : base(new Vector2Int(3, 3), 20) {
    _runePieceInventory = new List<RunePiece>();
  }

  public override void onWalkInto(Player player) {
    // no op
  }

  public void AddRunePiece(RunePiece piece) {
    _runePieceInventory.Add(piece);
  }
}