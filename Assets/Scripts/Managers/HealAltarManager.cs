using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAltarManager : MonoBehaviour {
  public HealAltar altar;
  public Sprite usedAltar;

  void Start() {
    altar.OnUsed += HandleUsed;
    altar.OnDeath += () => {
      Destroy(this.gameObject);
    };
    transform.position = new Vector3(altar.Coordinates.x, altar.Coordinates.y, 0);
  }

  private void HandleUsed() {
    GetComponent<SpriteRenderer>().sprite = usedAltar;
    GetComponentInChildren<ParticleSystem>().Stop();
  }
}
