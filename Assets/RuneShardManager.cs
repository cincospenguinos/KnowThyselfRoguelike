using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneShardManager : MonoBehaviour {
  public RuneShard shard;
  public TMPro.TMP_Text text;

  void Start() {
    text = GetComponentInChildren<TMPro.TMP_Text>();
    Update();
  }

  void Update() {
    text.text = shard.TextFull();
  }
}
