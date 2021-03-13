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

  public void SwapShard(RuneShard shard) {
    if (shard == RuneList[0].trigger || shard == RuneList[0].action) {
      return;
    }

    var rune = RuneList[0];
    shards.Remove(shard);

    if (shard is RuneAction action) {
      Rune newRune = new Rune(rune.trigger, action);
      shards.Add(RuneList[0].action);
      RuneList[0] = newRune;
    } else {
      Rune newRune = new Rune((RuneTrigger) shard, rune.action);
      shards.Add(RuneList[0].trigger);
      RuneList[0] = newRune;
    }
  }
}