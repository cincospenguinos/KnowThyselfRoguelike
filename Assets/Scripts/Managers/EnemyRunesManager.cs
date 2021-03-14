using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyRunesManager : MonoBehaviour {
  private static Vector3[] corners = new Vector3[4];
  public GameObject runePrefab;
  public GameObject runeConnectionLinePrefab;
  /// rune, line renderer
  public Dictionary<EnemyManager, EnemyRuneConnection> visibleRunes = new Dictionary<EnemyManager, EnemyRuneConnection>();

  void Start() { }

  // // Update is called once per frame
  void Update() {
    // var enemiesInSight = Grid.instance.Enemies.Where(e => e.DistanceTo(Grid.instance.Player))
    // draw lines
    foreach (var tuple in visibleRunes) {
      var enemyManager = tuple.Value.enemyManager;
      var line = tuple.Value.lineRenderer;
      var rune = tuple.Value.rune;
      var runePoint = rune.GetComponent<RectTransform>().TransformPoint(0, 0, 0);
      var camera = Camera.main;
      var worldPos = camera.ScreenToWorldPoint(runePoint);
      line.SetPositions(new Vector3[] { enemyManager.transform.position, new Vector3(worldPos.x, worldPos.y, 0) });
    }
  }

  internal void EnsureRegistered(Enemy enemy, EnemyManager enemyManager) {
    if (!visibleRunes.ContainsKey(enemyManager)) {
      /// create
      var rune = Instantiate(runePrefab, transform);
      rune.GetComponent<RuneManager>().rune = enemy.RuneList[0];
      var line = Instantiate(runeConnectionLinePrefab);
      visibleRunes.Add(enemyManager, new EnemyRuneConnection {
        rune = rune,
        lineRenderer = line.GetComponent<LineRenderer>(),
        enemyManager = enemyManager
      });
    }
  }

  internal void EnsureDeregistered(Enemy enemy, EnemyManager enemyManager) {
    if (visibleRunes.ContainsKey(enemyManager)) {
      /// destroy
      var connection = visibleRunes[enemyManager]; 
      if (connection.lineRenderer.gameObject != null) {
        Destroy(connection.lineRenderer.gameObject);
      }
      if (connection.rune != null) {
        Destroy(connection.rune);
      }
      visibleRunes.Remove(enemyManager);
    }
  }
}

public class EnemyRuneConnection {
  public EnemyManager enemyManager;
  public GameObject rune;
  public LineRenderer lineRenderer;
}
