using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {
  public Grid grid;
  public GameObject WallPrefab;
  public GameObject FloorPrefab;
  public GameObject HealAltarPrefab;
  public GameObject Enemy0Prefab;

  void Start() {
    for (int x = 0; x < Grid.WIDTH; x++) {
      for (int y = 0; y < Grid.HEIGHT; y++) {
        var tile = grid.Tiles[x,y];

        switch(tile) {
          case Floor floor:
              var f = Object.Instantiate(FloorPrefab, new Vector3(x, y, 0), Quaternion.identity, transform);
              f.GetComponent<FloorManager>().floor = floor;
              break;
          case Wall _:
              Object.Instantiate(WallPrefab, new Vector3(x, y, 0), Quaternion.identity, transform);
              break;
        }
      }
    }
    
    grid.Entities.ForEach((entity) => {
      var prefab = GetPrefabFor(entity);
      var gameObj = Object.Instantiate(
        prefab,
        new Vector3(entity.Coordinates.x, entity.Coordinates.y, 0),
        Quaternion.identity,
        transform
      );
      if (entity is Enemy e) {
        gameObj.GetComponent<EnemyManager>().Enemy = e;
      } else if (entity is HealAltar a) {
        gameObj.GetComponent<HealAltarManager>().altar = a;
      }
    });
  }

  private GameObject GetPrefabFor(Entity entity) {
    switch (entity) {
      case Enemy _:
        return Enemy0Prefab;
      case HealAltar _:
        return HealAltarPrefab;
      default:
        return null;
    }
  }
}
