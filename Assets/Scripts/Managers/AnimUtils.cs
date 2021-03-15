using System;
using System.Collections;
using UnityEngine;

public static class AnimUtils {
  private static GameObject damageNumber;

  public static GameObject ShowFloatingText(string message, Vector3 position) {
    if (damageNumber == null) {
      damageNumber = Resources.Load<GameObject>("Damage Number");
    }
    var obj = UnityEngine.Object.Instantiate(damageNumber, position, Quaternion.identity);
    var text = obj.GetComponentInChildren<TMPro.TMP_Text>();
    text.color = new Color(1, 1, 1, 1);
    text.text = message;
    obj.GetComponentInChildren<Animator>().speed = 0.5f;
    return obj;
  }

  public static GameObject AddDamageOrHealNumber(int amount, Vector3 position, bool isDamage) {
    if (damageNumber == null) {
      damageNumber = Resources.Load<GameObject>("Damage Number");
    }
    var obj = UnityEngine.Object.Instantiate(damageNumber, position, Quaternion.identity);
    var text = obj.GetComponentInChildren<TMPro.TMP_Text>();
    if (isDamage) {
      text.text = "-" + amount.ToString();
    } else {
      text.text = "+" + amount.ToString();
      text.color = new Color(0.2f, 1, 0.2f, 1);
    }
    return obj;
  }

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