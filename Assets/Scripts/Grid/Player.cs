using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {
  public List<RuneShard> shards;

  public Player() : base(new Vector2Int(3, 3), 20) {
    shards = new List<RuneShard>();
  }

  public override void onWalkInto(Player player) {
    // no op
  }

  public void AddRuneShard(RuneShard shard) {
    shards.Add(shard);
  }
}