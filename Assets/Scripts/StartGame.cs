using System.Collections;
using UnityEngine;

public class StartGame : MonoBehaviour {
  public GameObject GridPrefab;
  public GameObject depthPanel;
  public TMPro.TMP_Text TurnText;
  public TMPro.TMP_Text GameOverText;
  GameObject currentGrid;
  Player player;

  void Awake() {
    player = new Player();
    NewGrid(1);
  }

  void NewGrid(int depth) {
    Grid.instance = GridGenerator.generateMultiRoomGrid(player, depth, 6);
    Grid.instance.OnCleared += HandleGridCleared;
    currentGrid = Instantiate(GridPrefab);
    currentGrid.GetComponent<GridManager>().grid = Grid.instance;
  }

  void HandleGridCleared() {
    StartCoroutine(ChangeFloors(Grid.instance.depth + 1));
    // recreate a new grid
    // Destroy(currentGrid);
    // NewGrid();
  }

  IEnumerator ChangeFloors(int newDepth) {
    PlayerManager.inputEnabled = false;
    // let player movement animation finish
    // yield return new WaitForSeconds(0.25f);
    // disable player input
    var pManager = GameObject.Find("Player").GetComponent<PlayerManager>();
    // pManager.enabled = false;
    // shrink player
    var initialScale = pManager.transform.localScale;
    yield return AnimUtils.Animate(0.5f, (t) => {
      var s = 1 - t;
      var newScale = new Vector3(s, s, 1);
      newScale.Scale(initialScale);
      pManager.transform.localScale = newScale;
    });
    yield return new WaitForSeconds(0.5f);
    depthPanel.GetComponentInChildren<TMPro.TMP_Text>().text = "Depth " + newDepth;
    depthPanel.SetActive(true);
    yield return null;
    Destroy(currentGrid);
    NewGrid(newDepth);
    yield return new WaitForSeconds(2);
    depthPanel.SetActive(false);

    yield return AnimUtils.Animate(0.5f, (t) => {
      var newScale = new Vector3(t, t, 1);
      newScale.Scale(initialScale);
      pManager.transform.localScale = newScale;
    });
    PlayerManager.inputEnabled = true;
    // pManager.enabled = true;
  }

  void Update() {
    if (player.Dead) {
      GameOverText.text = $"<b><color=red>Game Over</color></b>";
    } else {
      TurnText.text = $"Turn {Grid.instance.CurrentTurn}";
    }
  }
}
