using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public Grid gameGrid;
    public GameObject WallPrefab;
    public GameObject FloorPrefab;

    // Start is called before the first frame update
    void Start()
    {
        gameGrid = Grid.generate();
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
