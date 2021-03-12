using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
  void Start() {
    var t = Grid.instance.Player.Coordinates;
    transform.position = new Vector3(t.x, t.y, transform.position.z);
  }

  void LateUpdate() {
    var t = Grid.instance.Player.Coordinates;
    transform.position = Vector3.Lerp(transform.position, new Vector3(t.x, t.y, transform.position.z), 0.1f);
  }
}
