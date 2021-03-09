using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  void Start() {}

  void LateUpdate() {
    var pos = transform.position;
    pos.x = Grid.instance.Player.Coordinates.x;
    pos.y = Grid.instance.Player.Coordinates.y;
    transform.position = pos;
  }
}
