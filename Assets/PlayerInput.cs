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
      Player.Direction direction = getDirection();

      if (Player.move(direction) || Player.attack(direction)) {
        Grid.instance.actionTaken();
      }

      var playerPos = transform.position;
      playerPos.x = Player.Coordinates.x;
      playerPos.y = Player.Coordinates.y;
      transform.position = playerPos;
    }

    private Player.Direction getDirection()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
          return Player.Direction.NORTH;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow)) {
          return Player.Direction.SOUTH;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
          return Player.Direction.WEST;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)) {
          return Player.Direction.EAST;
        }

        return Player.Direction.NONE;
    }
}
