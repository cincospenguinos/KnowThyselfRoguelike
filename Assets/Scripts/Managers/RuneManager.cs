using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneManager : MonoBehaviour {
  public Rune rune;
  public RuneShardManager action;
  public RuneShardManager trigger;

  void Start() {
    action.shard = rune.action;
    trigger.shard = rune.trigger;
    rune.OnTriggered += HandleRuneTriggered;
  }

  void OnDestroy() {
    rune.OnTriggered -= HandleRuneTriggered;
  }

  Coroutine pulse;
  private void HandleRuneTriggered(GameEvent obj) {
    if (pulse != null) {
      StopCoroutine(pulse);
    }
    pulse = StartCoroutine(Pulse());
  }

  IEnumerator Pulse() {
    yield return AnimUtils.Animate(0.2f, (t) => {
      var scale = 1 + Mathf.Sin(t * Mathf.PI) * 0.33f;
      transform.localScale = new Vector3(scale, scale, 1);
    });
    transform.localScale = new Vector3(1, 1, 1);
    pulse = null;
  }
}

public static class AnimUtils {
  public static IEnumerator Animate(float duration, Action<float> callback, bool smoothStep = true) {
    var start = Time.time;
    callback(0);
    while(Time.time - start < duration) {
      var t = (Time.time - start) / duration;
      if (smoothStep) {
        t = t * t * (3.0f - 2.0f * t);
      }
      callback(t);
      yield return new WaitForEndOfFrame();
    }
    callback(1);
  }
}