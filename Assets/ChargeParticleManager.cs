using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeParticleManager : MonoBehaviour {
  public Vector2 target;
  public Vector2 velocity;
  public RectTransform rectTransform;
  private float startTime;
  void Start() {
    startTime = Time.time;
    velocity = Random.insideUnitCircle * 200;
    rectTransform = GetComponent<RectTransform>();
  }

  // Update is called once per frame
  void Update() {
    var t = Time.time - startTime;
    rectTransform.anchoredPosition += velocity * Time.deltaTime;
    velocity *= 0.95f;
    rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, target, t * t * t);
  }
}
