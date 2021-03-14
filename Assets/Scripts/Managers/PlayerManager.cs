using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
  public Player Player;
  public Animator animator;
  public GameObject damageNumber;
  public static bool inputEnabled = true;

  void Start() {
    Player = Grid.instance.Player;
    Player.OnHit += HandlePlayerHit;
    Player.OnHeal += HandleHeal;
    transform.position = new Vector3(Player.Coordinates.x, Player.Coordinates.y, 0);
  }

  void Update() {
    if (!Player.Dead) {
      if (inputEnabled && actionLoop == null) {
        MaybeTakePlayerTurn();
      }

      transform.position = Vector3.Lerp(transform.position, new Vector3(Player.Coordinates.x, Player.Coordinates.y, 0), 0.1f);
    }
  }

  private void HandleHeal(int amount) {
    AnimUtils.AddDamageOrHealNumber(amount, transform.position, false);
  }

  void HandlePlayerHit(int damage) {
    AnimUtils.AddDamageOrHealNumber(damage, transform.position, true);
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
      RunActionLoop(false);
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
        RunActionLoop(true);
      } else {
        if (Player.move(nextCoordinates)) {
          animatorUpdatePlayerMoved(direction.Value);
          RunActionLoop(false);
        }
      }
    }
  }

  private Grid grid => Grid.instance;

  Coroutine actionLoop = null;
  void RunActionLoop(bool hasDelay) {
    if (actionLoop == null) {
      Debug.Log("actionLoop started");
      actionLoop = StartCoroutine(Grid.instance.actionTaken(hasDelay, () => {
        actionLoop = null;
        Debug.Log("actionLoop finished");
      }));
    }
  }

  private Direction? getUserInputDirection() {
    if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) {
      return Direction.NORTH;
    }

    if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) {
      return Direction.SOUTH;
    }

    if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
      return Direction.WEST;
    }

    if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
      return Direction.EAST;
    }
    return null;
  }
}
