using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {
  public Grid grid;
  public GameObject WallPrefab;
  public GameObject FloorPrefab;
  public GameObject PlayerPrefab;
  public GameObject Enemy0Prefab;

  void Start() {
    for (int x = 0; x < Grid.WIDTH; x++) {
      for (int y = 0; y < Grid.HEIGHT; y++) {
        var tile = grid.Tiles[x,y];

        switch(tile) {
          case Grid.TileType.FLOOR:
              Object.Instantiate(FloorPrefab, new Vector3(x, y, 0), Quaternion.identity, transform);
              break;
          case Grid.TileType.WALL:
              Object.Instantiate(WallPrefab, new Vector3(x, y, 0), Quaternion.identity, transform);
              break;
        }
      }
    }

    Object.Instantiate(PlayerPrefab, new Vector3(grid.Player.Coordinates.x,
      grid.Player.Coordinates.y, 0), Quaternion.identity, transform);
    
    grid.Enemies.ForEach((enemy) => {
      var enemyGameObj = Object.Instantiate(Enemy0Prefab,
      new Vector3(enemy.Coordinates.x, enemy.Coordinates.y, 0), Quaternion.identity, transform);
      enemyGameObj.GetComponent<EnemyManager>().Enemy = enemy;
    });
  }
}
