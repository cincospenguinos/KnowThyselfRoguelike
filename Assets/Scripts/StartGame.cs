using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {
  public GameObject GridPrefab;
  public GameObject depthPanel;
  public TMPro.TMP_Text TurnText;
  public TMPro.TMP_Text ScoreText;
  public TMPro.TMP_Text GameOverText;
  GameObject currentGrid;
  Player player;

  void Awake() {
    GameOverText.gameObject.SetActive(false);
    player = new Player();
    NewGrid(1);
  }

  void NewGrid(int depth) {
    Grid.instance = GridGenerator.generateMultiRoomGrid(player, depth);
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

    if (newDepth % 2 == 0) {
      var trigger = RuneGenerator.randomTrigger(player.shards);
      player.AddRuneShard(trigger);
    } else {
      var action = RuneGenerator.randomAction(player.shards);
      player.AddRuneShard(action);
    }

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
      GameOverText.gameObject.SetActive(true);
      if (player.newHighscoreReached == null) {
        // hasn't been computed yet; compute it for the first time, and store it 
        player.newHighscoreReached = player.score > Player.HIGHSCORE;
        Player.HIGHSCORE = player.score;
      }
      var highscore = Player.HIGHSCORE;
      GameOverText.text = $"<b><color=red>You succumb to insanity...</color></b>\nScore: {player.score}\nHigh Score: {Player.HIGHSCORE}";
      if (player.newHighscoreReached == true) {
        GameOverText.text += $"\nNew high score!";
      }
      GameOverText.text += "\n\n<u>Replay<u>";
    } else {
      TurnText.text = $"Turn {Grid.instance.CurrentTurn}\n" +
      $"Damage: {player.minBaseDamage + player.AddedDamage}-{player.maxBaseDamage + player.AddedDamage}\n" +
      $"Block: {player.Block}\n";
      ScoreText.text = $"Score: {player.score}";
    }
  }

  public void Replay() {
    SceneManager.LoadSceneAsync("Scenes/SampleScene", LoadSceneMode.Single);
  }
}
