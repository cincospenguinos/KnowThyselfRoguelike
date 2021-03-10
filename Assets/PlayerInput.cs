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
    MaybeTakePlayerTurn();

    transform.position = Vector3.Lerp(transform.position, new Vector3(Player.Coordinates.x, Player.Coordinates.y, 0), 0.1f);
  }

  void MaybeTakePlayerTurn() {
    if (Input.GetKeyDown(KeyCode.Space)) {
      Player.wait();
      Grid.instance.actionTaken();
      return;
    }

    Direction? direction = getUserInputDirection();
    if (direction != null) {
      var nextCoordinates = Player.adjacentIn(direction.Value);
      var enemy = Grid.instance.EntityAt(nextCoordinates);
      if (enemy != null) {
        Player.attack(enemy);
        Grid.instance.actionTaken();
      } else {
        if (Player.move(nextCoordinates)) {
          Grid.instance.actionTaken();
        }
      }
    }
  }

  private Direction? getUserInputDirection() {
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
