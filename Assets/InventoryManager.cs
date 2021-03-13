using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {
  public GameObject triggerPrefab, actionPrefab;
  public Dictionary<RuneShard, GameObject> mapping = new Dictionary<RuneShard, GameObject>();

  void Start() {
    // create new shards
    MatchShards();
    // for (int i = 0; i < shards.Count; i++) {
    //   var shard = shards[i];
    //   var shardObject = transform.GetChild(i);
    // }
  }

  GameObject CreateShard(RuneShard shard) {
    GameObject gameObject;
    if (shard is RuneTrigger) {
      gameObject = Instantiate(triggerPrefab, transform);
    } else {
      gameObject = Instantiate(actionPrefab, transform);
    }
    gameObject.GetComponent<RuneShardManager>().shard = shard;
    return gameObject;
  }

  void MatchShards() {
    var shards = Grid.instance.Player.shards;
    foreach (var newShard in shards) {
      if (!mapping.ContainsKey(newShard)) {
        mapping.Add(newShard, CreateShard(newShard));
      }
    }
    // delete shards
    List<RuneShard> toRemove = new List<RuneShard>();

    foreach (var oldShard in mapping.Keys) {
      if (!shards.Contains(oldShard)) {
        var gameObject = mapping[oldShard];
        Destroy(gameObject);
        toRemove.Add(oldShard);
      }
    }

    toRemove.ForEach(shard => mapping.Remove(shard));
  }

  void Update() {
    MatchShards();
  }
}
