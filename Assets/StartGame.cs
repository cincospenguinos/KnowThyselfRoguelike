using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
  public Grid gameGrid;

  public GameObject WallPrefab;
  public GameObject FloorPrefab;
  public GameObject PlayerPrefab;


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
  }

  void Update()
  {
      
  }
}
