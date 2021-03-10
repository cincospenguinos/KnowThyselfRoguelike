using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneManager : MonoBehaviour {
  public Rune rune;
  public TMPro.TMP_Text actionText;
  public TMPro.TMP_Text triggerText;

  void Start() {
    rune.OnTriggered += HandleRuneTriggered;
    Update();
  }

  Coroutine pulse;
  private void HandleRuneTriggered(GameEvent obj) {
    if (pulse != null) {
      StopCoroutine(pulse);
    }
    pulse = StartCoroutine(Pulse());
  }

  IEnumerator Pulse() {
    var start = Time.time;
    var originalScale = new Vector3(1, 1, 1);
    var duration = 0.2f;
    while(Time.time - start < duration) {
      var t = (Time.time - start) / duration;
      var scale = 1 + Mathf.Sin(t * Mathf.PI) * 0.33f;
      transform.localScale = new Vector3(scale, scale, 1);
      yield return new WaitForEndOfFrame();
    }
    transform.localScale = originalScale;
    pulse = null;
  }

  void Update() {
    actionText.text = rune.action.Text();
    triggerText.text = rune.trigger.Text();
  }
}
