using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
  public Player Player;

  void Start() {
    Player = Grid.instance.Player;
  }

  void Update() {
    Direction? direction = getDirection();
    if (direction != null) {
      if (Player.move(direction.Value) || Player.attack(direction.Value)) {
        Grid.instance.actionTaken();
      }
    }

    var playerPos = transform.position;
    playerPos.x = Player.Coordinates.x;
    playerPos.y = Player.Coordinates.y;
    transform.position = playerPos;
  }

  private Direction? getDirection() {
    if (Input.GetKeyDown(KeyCode.UpArrow)) {
      return Direction.NORTH;
    }

    if (Input.GetKeyDown(KeyCode.DownArrow)) {
      return Direction.SOUTH;
    }

    if (Input.GetKeyDown(KeyCode.LeftArrow)) {
      return Direction.WEST;
    }

    if (Input.GetKeyDown(KeyCode.RightArrow)) {
      return Direction.EAST;
    }
    return null;
  }
}
