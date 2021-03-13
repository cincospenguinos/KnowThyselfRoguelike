using UnityEngine;

public class StartGame : MonoBehaviour {
  public GameObject GridPrefab;
  public TMPro.TMP_Text TurnText;
  public TMPro.TMP_Text GameOverText;
  GameObject currentGrid;
  Player player;

  void Awake() {
    player = new Player();
    NewGrid();
  }

  void NewGrid() {
    Grid.instance = GridGenerator.generateMultiRoomGrid(player, 6);
    Grid.instance.OnCleared += HandleGridCleared;
    currentGrid = Instantiate(GridPrefab);
    currentGrid.GetComponent<GridManager>().grid = Grid.instance;
  }

  void HandleGridCleared() {
    // StartCoroutine(ChangeFloors());
    // recreate a new grid
    Destroy(currentGrid);
    NewGrid();
  }

  // IEnumerator ChangeFloors() {
  //   // disable player input
  //   var PlayerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
  //   PlayerManager.enabled = false;
  //   // shrink player
  //   yield return RuneManager.

  //   // pop level out

  //   // create new level

  //   // maybe fade to black for a second?
  // }

  void Update() {
    if (player.Dead) {
      GameOverText.text = $"<b><color=red>Game Over</color></b>";
    } else {
      TurnText.text = $"Turn {Grid.instance.CurrentTurn}\nHP: {Grid.instance.Player.HitPoints}/20";
    }
  }
}
