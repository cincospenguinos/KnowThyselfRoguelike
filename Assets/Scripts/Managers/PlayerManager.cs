using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
  public Player Player;
  public Animator animator;
  public GameObject damageNumber;
  public static bool inputEnabled = true;
  public AudioSource SoundEffectSource;
  public AudioClip heal;
  public List<AudioClip> attackEffects;

  void Start() {
    Player = Grid.instance.Player;
    Player.OnHit += HandlePlayerHit;
    Player.OnHeal += HandleHeal;
    Player.OnRuneTriggered += (message) => AnimUtils.ShowFloatingText(message, transform.position);
    transform.position = new Vector3(Player.Coordinates.x, Player.Coordinates.y, 0);
    SoundEffectSource = GameObject.Find("Player").GetComponent<AudioSource>();

    attackEffects = new List<AudioClip>(new AudioClip[] {
      (AudioClip) Resources.Load("Sounds/thwack-01"),
      (AudioClip) Resources.Load("Sounds/thwack-02"),
      (AudioClip) Resources.Load("Sounds/thwack-04"),
      (AudioClip) Resources.Load("Sounds/thwack-06"),
      (AudioClip) Resources.Load("Sounds/thwack-07"),
      (AudioClip) Resources.Load("Sounds/thwack-08"),
      (AudioClip) Resources.Load("Sounds/thwack-09"),
    });
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
    AudioSource.PlayClipAtPoint(heal, transform.position, 1.4f);
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
        SoundEffectSource.PlayOneShot(attackEffects.GetRandom());
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
      actionLoop = StartCoroutine(Grid.instance.actionTaken(hasDelay, () => {
        actionLoop = null;
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
