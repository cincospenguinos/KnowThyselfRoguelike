using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour {
  public GameObject GridPrefab;
  public GameObject depthPanel;
  public TMPro.TMP_Text TurnText;
  public TMPro.TMP_Text DamageText;
  public TMPro.TMP_Text BlockText;
  public TMPro.TMP_Text ScoreText;
  public TMPro.TMP_Text GameOverText;
  public GameObject GameOverContainer;
  GameObject currentGrid;
  Player player;

  void Awake() {
    GameOverContainer.SetActive(false);
    player = new Player();
    NewGrid(1);
  }

  void NewGrid(int depth) {
    var retry = true;
    for (int i = 0; i < 100 && retry; i++) {
      try {
        Grid.instance = GridGenerator.generateMultiRoomGrid(player, depth);
        retry = false;
      } catch (System.Exception) {
        /// try again
      }
    }
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

  private bool gameOverTriggered = false;
  void Update() {
    if (player.Dead) {
      if (!gameOverTriggered) {
        gameOverTriggered = true;
        StartCoroutine(GameOverScreen());
      }
    } else {
      TurnText.text = $"{Grid.instance.CurrentTurn}";
      DamageText.text = $"{player.minBaseDamage + player.AddedDamage}-{player.maxBaseDamage + player.AddedDamage}";
      BlockText.text = $"{player.Block}";
      ScoreText.text = $"Depth {Grid.instance.depth} - Score {player.score}";
    }
  }

  IEnumerator GameOverScreen() {
    GameOverContainer.SetActive(true);
    GameOverText.gameObject.SetActive(false);
    var replay = GameOverContainer.transform.Find("Replay").gameObject;
    var backToMain = GameOverContainer.transform.Find("Back to Main").gameObject;
    replay.SetActive(false);
    backToMain.SetActive(false);

    yield return AnimUtils.Animate(1f, (t) => {
      GameOverContainer.GetComponent<Image>().color = new Color(0, 0, 0, t * 0.9f);
    });

    yield return new WaitForSeconds(0.25f);

    if (player.newHighscoreReached == null) {
      // hasn't been computed yet; compute it for the first time, and store it 
      player.newHighscoreReached = player.score > Player.HIGHSCORE;
      Player.HIGHSCORE = player.score;
    }
    GameOverText.text = $"<b><color=red>You succumb to insanity...</color></b>\nScore: {player.score}\nHigh Score: {Player.HIGHSCORE}";
    if (player.newHighscoreReached == true) {
      GameOverText.text += $"\nNew high score!";
    }

    GameOverText.gameObject.SetActive(true);
    yield return AnimUtils.Animate(1, (t) => {
      GameOverText.color = new Color(1, 1, 1, t);
    });

    yield return new WaitForSeconds(0.25f);
    replay.SetActive(true);

    yield return new WaitForSeconds(0.25f);
    backToMain.SetActive(true);
  }

  public void Replay() {
    SceneManager.LoadSceneAsync("Scenes/SampleScene", LoadSceneMode.Single);
  }

  public void BackToMain() {
    SceneManager.LoadSceneAsync("Scenes/StartGameScene", LoadSceneMode.Single);
  }
}
