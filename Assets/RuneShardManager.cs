using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuneShardManager : MonoBehaviour {
  public RuneShard shard;
  public TMPro.TMP_Text text;
  public Image chargePercentage;

  void Start() {
    text = GetComponentInChildren<TMPro.TMP_Text>();
    Update();
  }

  void Update() {
    if (shard != null) {
      text.text = shard.TextFull();
      if (shard is RuneAction a && chargePercentage != null) {
        chargePercentage.fillAmount = a.ChargePercentage;
      }
    }
  }

  public void OnShardInventoryClick() {
    if (Grid.instance.Player.EditingRunes) {
      if (shard != null) {
        Grid.instance.Player.SwapShard(shard);
      }
    }
  }
}
