using UnityEngine;

public class StartGame : MonoBehaviour {
  public GameObject GridPrefab;
  public TMPro.TMP_Text TurnText;
  GameObject currentGrid;
  Player player;

  void Awake() {
    player = new Player();
    NewGrid();
  }

  void NewGrid() {
    Grid.instance = GridGenerator.generateMultiRoomGrid(player, 3);
    Grid.instance.OnCleared += HandleGridCleared;
    currentGrid = Instantiate(GridPrefab);
    currentGrid.GetComponent<GridManager>().grid = Grid.instance;
  }

  void HandleGridCleared() {
    // recreate a new grid
    Destroy(currentGrid);
    NewGrid();
  }

  void Update() {
    TurnText.text = $"Turn {Grid.instance.CurrentTurn}\nHP: {Grid.instance.Player.HitPoints}/20";
  }
}
