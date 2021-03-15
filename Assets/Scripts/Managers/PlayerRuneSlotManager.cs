using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRuneSlotManager : MonoBehaviour {
  public GameObject runePrefab;
  private GameObject runeObject;
  Rune[] runes => Grid.instance.Player.RuneList;
  public int index;
  bool isEditing => Grid.instance.Player.EditingRunes;

  void Start() {
    MatchRune();
  }

  void AddRuneObject(Rune targetRune) {
    runeObject = Instantiate(runePrefab, transform);
    runeObject.GetComponent<RuneManager>().rune = targetRune;
  }

  void Update() {
    MatchRune();
  }

  Rune targetRune => index < runes.Length ? runes[index] : null;

  void MatchRune() {
    if (runeObject != null) {
      if (targetRune != runeObject.GetComponent<RuneManager>().rune) {
        // clear cache
        Destroy(runeObject);
        runeObject = null;
      }
    }

    if (runeObject == null && targetRune != null) {
      AddRuneObject(targetRune);
    }
  }
}
