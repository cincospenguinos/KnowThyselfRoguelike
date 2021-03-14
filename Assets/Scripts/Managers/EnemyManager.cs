using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
  public Enemy Enemy;
  private EnemyRunesManager enemyRunesManager;
  public GameObject damageNumber;
  public TMPro.TMP_Text hpText;

  // Start is called before the first frame update
  void Start() {
    this.enemyRunesManager = GameObject.Find("Enemy Runes").GetComponent<EnemyRunesManager>();
    transform.localScale = new Vector3(0, 0, 1);
    Enemy.OnDeath += () => {
      Destroy(this.gameObject);
    };
    Enemy.OnHit += (int damage) => {
      var obj = Instantiate(damageNumber, transform.position, Quaternion.identity);
      obj.GetComponentInChildren<TMPro.TMP_Text>().text = "-" + damage.ToString();
    };
  }

  // Update is called once per frame
  void Update() {
    transform.position = Vector3.Lerp(transform.position, new Vector3(Enemy.Coordinates.x, Enemy.Coordinates.y, 0), 0.1f);
    UpdateRuneUI();
    var targetScale = Enemy.isVisible ? 1 : 0;
    transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(targetScale, targetScale, 1), 0.2f);
    hpText.text = $"{Enemy.HitPoints}/{Enemy.MaxHitPoints}";
  }

  void OnDestroy() {
    if (enemyRunesManager != null) {
      enemyRunesManager.EnsureDeregistered(Enemy, this);
    }
  }

  void UpdateRuneUI() {
    if (Enemy.isVisible) {
      enemyRunesManager.EnsureRegistered(Enemy, this);
    } else {
      enemyRunesManager.EnsureDeregistered(Enemy, this);
    }
  }
}
