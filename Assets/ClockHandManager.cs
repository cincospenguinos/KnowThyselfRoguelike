using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockHandManager : MonoBehaviour {

  // Update is called once per frame
  void Update() {
    float rotation = ((Grid.instance.CurrentTurn - 1) % 10 / 10f) * -360;
    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, rotation), 0.2f);
  }
}
