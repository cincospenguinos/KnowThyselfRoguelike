using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
  public Player Player;
  public Animator animator;
  public static bool inputEnabled = true;

  void Start() {
    Player = Grid.instance.Player;
    Player.OnHit += animatorUpdatePlayerHit;
    transform.position = new Vector3(Player.Coordinates.x, Player.Coordinates.y, 0);
  }

  void Update() {
    if (!Player.Dead) {
      if (inputEnabled) {
        MaybeTakePlayerTurn();
      }

      transform.position = Vector3.Lerp(transform.position, new Vector3(Player.Coordinates.x, Player.Coordinates.y, 0), 0.1f);
    }
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
      animatorUpdatePlayerDirection(direction.Value);
      var nextCoordinates = Player.adjacentIn(direction.Value);
      var entity = Grid.instance.EntityAt(nextCoordinates);
      if (entity != null) {
        entity.onWalkInto(Player);
        animatorUpdatePlayerAttack();
        Grid.instance.actionTaken();
      } else {
        if (Player.move(nextCoordinates)) {
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
