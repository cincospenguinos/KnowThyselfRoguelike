using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
  public Enemy Enemy;
  private EnemyRunesManager enemyRunesManager;

  // Start is called before the first frame update
  void Start() {
    this.enemyRunesManager = GameObject.Find("Enemy Runes").GetComponent<EnemyRunesManager>();
    Enemy.OnDeath += () => {
      Destroy(this.gameObject);
    };
  }

  // Update is called once per frame
  void Update() {
    transform.position = Vector3.Lerp(transform.position, new Vector3(Enemy.Coordinates.x, Enemy.Coordinates.y, 0), 0.1f);
    UpdateRuneUI();
  }

  void OnDestroy() {
    enemyRunesManager.EnsureDeregistered(Enemy, this);
  }

  void UpdateRuneUI() {
    Vector3 screenPoint = Camera.main.WorldToViewportPoint(new Vector3(Enemy.Coordinates.x, Enemy.Coordinates.y, 0));
    bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    if (onScreen) {
      enemyRunesManager.EnsureRegistered(Enemy, this);
    } else {
      enemyRunesManager.EnsureDeregistered(Enemy, this);
    }
  }
}
