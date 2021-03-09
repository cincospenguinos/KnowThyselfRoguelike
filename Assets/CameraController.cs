using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  public GameObject Player;

  void Start() {
    var component = GetComponent<StartGame>();
    Debug.Log("Do we have the player object? " + component.PlayerObject != null);
    Player = component.PlayerObject;
  }

  void LateUpdate() {
    transform.position = Player.transform.position;
  }
}
