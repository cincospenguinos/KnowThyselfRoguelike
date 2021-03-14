using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneManager : MonoBehaviour {
  public Rune rune;
  public RuneShardManager action;
  public RuneShardManager trigger;
  public GameObject chargePrefab;

  void Start() {
    action.shard = rune.action;
    trigger.shard = rune.trigger;
    rune.OnChargeAdded += HandleChargeAdded;
    rune.action.OnTriggered += HandleActionTriggered;
  }

  void OnDestroy() {
    rune.OnChargeAdded -= HandleChargeAdded;
    rune.action.OnTriggered -= HandleActionTriggered;
  }

  private void HandleChargeAdded(int charge) {
    StartCoroutine(CreateChargeParticles(charge));
  }

  IEnumerator CreateChargeParticles(int num) {
    while (num > 0) {
      var charge = Instantiate(chargePrefab, transform);
      charge.SetActive(true);
      num--;
      yield return new WaitForEndOfFrame();
    }
  }

  Coroutine pulse;
  private void HandleActionTriggered() {
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
