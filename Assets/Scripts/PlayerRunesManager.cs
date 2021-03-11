using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunesManager : MonoBehaviour {
  public GameObject runePrefab;
  void Start() {
    var runes = Grid.instance.Player.RuneList;
    foreach (var rune in runes) {
      var runeObject = Instantiate(runePrefab, transform);
      runeObject.GetComponent<RuneManager>().rune = rune;
    }
  }

  // void MatchRuneList() {
  //   // create new runes
  // }

  // Update is called once per frame
  void Update() {
    // MatchRuneList();
  }
}
