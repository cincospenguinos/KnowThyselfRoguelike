using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuneShardManager : MonoBehaviour {
  public RuneShard shard;
  public TMPro.TMP_Text text;
  public Image chargePercentage;
  public Transform upgradeContainer;
  public GameObject pearlPrefab;

  void Start() {
    text = GetComponentInChildren<TMPro.TMP_Text>();
    Update();
  }

  void Update() {
    if (shard != null) {
      text.text = shard.TextFull();
      if (shard is RuneAction a && chargePercentage != null) {
        var targetFillAmount = a.ChargePercentage;
        chargePercentage.fillAmount = Mathf.Lerp(chargePercentage.fillAmount, targetFillAmount, 0.2f);
      }
      for(int i = upgradeContainer.childCount; i < shard.Upgrades; i++) {
        var newUpgradePearl = Instantiate(pearlPrefab, upgradeContainer);
        newUpgradePearl.SetActive(true);
      }
    }
  }

  public void HandleClicked() {
    if (!Grid.instance.Player.EditingRunes) {
      return;
    }

    var isInInventory = Grid.instance.Player.shards.Contains(shard);
    var isInRuneList = System.Array.FindIndex(Grid.instance.Player.RuneList, (rune) => rune.action == shard || rune.trigger == shard) != -1;
    if (isInInventory) {
      Grid.instance.Player.MoveShardFromInventoryIntoFirstEmptyRuneList(shard);
    } else if (isInRuneList) {
      Grid.instance.Player.MoveShardFromRuneListToInventory(shard);
    }
  }
}
