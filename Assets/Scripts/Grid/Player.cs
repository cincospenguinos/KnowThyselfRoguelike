using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {
  public List<RuneShard> shards;
  public int TurnsSinceHitByEnemy = 0;

  public Player() : base(new Vector2Int(3, 3), 20) {
    shards = new List<RuneShard>();
  }

  public override void onWalkInto(Player player) {
    // no op
  }

  public void AddRuneShard(RuneShard shard) {
    shards.Add(shard);
  }

  public override void TakeDamage(int damage) {
    base.TakeDamage(damage);
    TurnsSinceHitByEnemy = 0;
  }
}