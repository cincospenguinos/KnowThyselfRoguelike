using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneManager : MonoBehaviour {
  public Rune rune;
  public TMPro.TMP_Text actionText;
  public TMPro.TMP_Text triggerText;
  void Start() {
    Update();
  }

  void Update() {
    actionText.text = rune.action.Text();
    triggerText.text = rune.trigger.Text();
  }
}
