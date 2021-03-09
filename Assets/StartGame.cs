using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
  public Grid gameGrid;

  public GameObject WallPrefab;
  public GameObject FloorPrefab;
  public GameObject PlayerPrefab;
  public GameObject Enemy0Prefab;
  public TMPro.TMP_Text TurnText;

  void Awake() {
    gameGrid = Grid.instance = GridGenerator.generateMultiRoomGrid(10);

    for (int x = 0; x < Grid.WIDTH; x++) {
      for (int y = 0; y < Grid.HEIGHT; y++) {
        var tile = gameGrid.Tiles[x,y];

        switch(tile) {
          case Grid.TileType.FLOOR:
              Object.Instantiate(FloorPrefab, new Vector3(x, y, 0), Quaternion.identity);
              break;
          case Grid.TileType.WALL:
              Object.Instantiate(WallPrefab, new Vector3(x, y, 0), Quaternion.identity);
              break;
        }
      }
    }

    Object.Instantiate(PlayerPrefab, new Vector3(gameGrid.Player.Coordinates.x,
      gameGrid.Player.Coordinates.y, 0), Quaternion.identity);
    
    gameGrid.Enemies.ForEach((enemy) => {
      var enemyGameObj = Object.Instantiate(Enemy0Prefab,
      new Vector3(enemy.Coordinates.x, enemy.Coordinates.y, 0), Quaternion.identity);
      enemyGameObj.GetComponent<EnemyManager>().Enemy = enemy;
    });
  }

  void Update() {
    TurnText.text = "Turn " + gameGrid.CurrentTurn;
  }
}
