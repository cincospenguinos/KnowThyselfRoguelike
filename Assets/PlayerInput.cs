using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
  public Player Player;
  public Animator animator;

  void Start() {
    Player = Grid.instance.Player;
    Player.OnHit += animatorUpdatePlayerHit;
  }

  void Update() {
    MaybeTakePlayerTurn();

    transform.position = Vector3.Lerp(transform.position, new Vector3(Player.Coordinates.x, Player.Coordinates.y, 0), 0.1f);
  }

  void animatorUpdatePlayerHit() {
    animator.SetTrigger("hit");
  }

  void animatorUpdatePlayerAttack() {
    animator.SetTrigger("attack");
  }

  void animatorUpdatePlayerDirection(Direction direction) {
    animator.SetInteger("direction", (int) direction);
  }

  void animatorUpdatePlayerMoved(Direction direction) {
    animator.SetTrigger("moved");
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
        animatorUpdatePlayerDirection(direction.Value);
        animatorUpdatePlayerAttack();
        Grid.instance.actionTaken();
      } else {
        if (Player.move(nextCoordinates)) {
          animatorUpdatePlayerDirection(direction.Value);
          animatorUpdatePlayerMoved(direction.Value);
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
