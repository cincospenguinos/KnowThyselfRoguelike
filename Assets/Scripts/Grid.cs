using UnityEngine;

public class Grid {
  public enum TileType {
      FLOOR = 0,
      WALL = 1,
  };

  private static Grid _instance;

  // [x][y]
  private TileType[,] _grid;
  private Player _player;

  public TileType[,] Tiles => _grid;
  public Player Player => _player;

  public const int WIDTH = 40;
  public const int HEIGHT = 28;

  public Grid() {
    _grid = new TileType[40,28];

    for (int x = 0; x < WIDTH; x++) {
      for (int y = 0; y < HEIGHT; y++) {
        if (x == 0 || y == 0 || x == WIDTH - 1 || y == HEIGHT - 1) {
          _grid[x, y] = TileType.WALL;
        }
      }
    }

    _player = new Player(this);
  }

  public bool validPlayerMovement(Vector2Int movement) {
    if (movement.x < 0 || movement.y < 0 || movement.x >= WIDTH || movement.y >= HEIGHT) {
      return false;
    }

    return Tiles[movement.x, movement.y] == TileType.FLOOR;
  }

  public static Grid getInstance() {
    if (_instance == null) {
      _instance = new Grid();
    }

    return _instance;
  }
}