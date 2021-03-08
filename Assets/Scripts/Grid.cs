public class Grid {
  public enum TileType {
      FLOOR = 0,
      WALL = 1,
  };

  // [x][y]
  private TileType[,] _grid;
  public TileType[,] Tiles => _grid;

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
  }

  public static Grid generate() {
    return new Grid();
  }
}