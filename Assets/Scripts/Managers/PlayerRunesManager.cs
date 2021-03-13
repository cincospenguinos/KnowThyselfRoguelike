using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunesManager : MonoBehaviour {
  public GameObject runePrefab;
  private GameObject runeObject;

  void Start() {
    var runes = Grid.instance.Player.RuneList;
    foreach (var rune in runes) {
      runeObject = Instantiate(runePrefab, transform);
      runeObject.GetComponent<RuneManager>().rune = rune;
    }
  }

  void Update() {
    MatchRuneList();
  }

  void MatchRuneList() {
    var rune = Grid.instance.Player.RuneList[0];

    if (runeObject.GetComponent<RuneManager>().rune != rune) {
      Destroy(runeObject);
      runeObject = Instantiate(runePrefab, transform);
      runeObject.GetComponent<RuneManager>().rune = rune;
    }
  }
}
