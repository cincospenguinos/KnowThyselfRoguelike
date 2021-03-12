using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
  public Enemy Enemy;
  private EnemyRunesManager enemyRunesManager;

  // Start is called before the first frame update
  void Start() {
    this.enemyRunesManager = GameObject.Find("Enemy Runes").GetComponent<EnemyRunesManager>();
    transform.localScale = new Vector3(0, 0, 1);
    Enemy.OnDeath += () => {
      Destroy(this.gameObject);
    };
  }

  // Update is called once per frame
  void Update() {
    transform.position = Vector3.Lerp(transform.position, new Vector3(Enemy.Coordinates.x, Enemy.Coordinates.y, 0), 0.1f);
    UpdateRuneUI();
    var targetScale = Enemy.isVisible ? 1 : 0;
    transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(targetScale, targetScale, 1), 0.2f);
  }

  void OnDestroy() {
    enemyRunesManager.EnsureDeregistered(Enemy, this);
  }

  void UpdateRuneUI() {
    if (Enemy.isVisible) {
      enemyRunesManager.EnsureRegistered(Enemy, this);
    } else {
      enemyRunesManager.EnsureDeregistered(Enemy, this);
    }
  }
}
