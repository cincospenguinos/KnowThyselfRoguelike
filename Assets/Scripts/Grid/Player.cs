using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {
  public List<RuneShard> shards;
  public int TurnsSinceHitByEnemy = 0;
  public override int BaseDamage => Random.Range(minBaseDamage, maxBaseDamage + 1);
  public static int HIGHSCORE {
    get => PlayerPrefs.GetInt("highscore");
    set => PlayerPrefs.SetInt("highscore", value);
  }

  public bool EditingRunes = false;

  public bool? newHighscoreReached = null;

  public int minBaseDamage = 4;
  public int maxBaseDamage = 6;
  public int score;

  public Player() : base(new Vector2Int(3, 3), 100) {
    shards = new List<RuneShard>();
    RuneList[0] = RuneGenerator.generateRandom(this);
    RuneList[1] = new Rune(null, null);
    RuneList[2] = new Rune(null, null);
  }

  public override void onWalkInto(Player player) {
    // no op
  }

  public void AddRuneShard(RuneShard shard) {
    shard.OwningEntity = this;
    shards.Add(shard);
  }

  public override void TakeDamage(int damage) {
    base.TakeDamage(damage);
    TurnsSinceHitByEnemy = 0;
  }

  /// assumes shard is somewhere in RuneList
  ///
  /// 1. find rune with shard
  /// 2. assign new rune with null in the spot
  /// 3. add shard to inventory
  public void MoveShardFromRuneListToInventory(RuneShard shard) {
    var index = System.Array.FindIndex(RuneList, rune => shard is RuneAction ? rune.action == shard : rune.trigger == shard);
    if (index == -1) {
      // couldn't find it
      Debug.LogError("can't find shard in runelist");
    }
    var oldRune = RuneList[index];
    Rune newRune;
    if (shard is RuneAction) {
      newRune = new Rune(oldRune.trigger, null);
    } else {
      newRune = new Rune(null, oldRune.action);
    }
    shard.OnAddedOrRemovedFromRune();
    RuneList[index] = newRune;
    shards.Add(shard);
  }

  /// put a shard from your inventory into a rune in your RuneList
  /// we need to:
  ///
  /// find rune in RuneList with empty spot for the shard to fit
  /// remove shard from inventory
  /// set new rune with new shard
  public void MoveShardFromInventoryIntoFirstEmptyRuneList(RuneShard shard) {
    var index = System.Array.FindIndex(RuneList, rune => shard is RuneAction ? rune.action == null : rune.trigger == null);
    if (index == -1) {
      Debug.Log("no empty slots");
      // no empty slots
      return;
    }
    var oldRune = RuneList[index];
    shards.Remove(shard);
    Rune newRune;

    if (shard is RuneAction action) {
      newRune = new Rune(oldRune.trigger, action);
    } else {
      newRune = new Rune((RuneTrigger) shard, oldRune.action);
    }
    shard.OnAddedOrRemovedFromRune();
    RuneList[index] = newRune;
  }
}