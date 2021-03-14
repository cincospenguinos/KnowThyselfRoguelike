using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunesManager : MonoBehaviour {
  public GameObject runePrefab;
  List<Rune> runes => Grid.instance.Player.RuneList;

  void Start() {
    foreach (var rune in runes) {
      var runeObject = Instantiate(runePrefab, transform);
      runeObject.GetComponent<RuneManager>().rune = rune;
    }
  }

  void Update() {
    MatchRuneList();
  }

  void MatchRuneList() {
    for (int i = 0; i < runes.Count; i++) {
      var rune = runes[i];
      var runeObject = transform.GetChild(i).gameObject;

      if (runeObject.GetComponent<RuneManager>().rune != rune) {
        Destroy(runeObject);
        var newRuneObject = Instantiate(runePrefab, transform);
        newRuneObject.GetComponent<RuneManager>().rune = rune;
        newRuneObject.transform.SetSiblingIndex(i);
      }
    }
  }
}
