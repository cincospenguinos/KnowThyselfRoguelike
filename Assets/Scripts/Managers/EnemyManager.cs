using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
  public Enemy Enemy;
  private EnemyRunesManager enemyRunesManager;
  public TMPro.TMP_Text hpText;
  public Coroutine attackAnimation;

  // Start is called before the first frame update
  void Start() {
    this.enemyRunesManager = GameObject.Find("Enemy Runes").GetComponent<EnemyRunesManager>();
    transform.localScale = new Vector3(0, 0, 1);
    Enemy.OnDeath += () => {
      Destroy(this.gameObject);
    };
    Enemy.OnAttack += (target) => {
      float intensity = 1;
      var startPosition = transform.position;
      var targetPosition = new Vector3(target.Coordinates.x, target.Coordinates.y, startPosition.z);
      /// attack animation
      attackAnimation = StartCoroutine(AnimUtils.Animate(0.5f, (t) => {
        float lerp = Mathf.Pow(Mathf.Cos(Mathf.PI / 2 + Mathf.PI * Mathf.Sqrt(t)), 4) * intensity;
        transform.position = Vector3.Lerp(startPosition, targetPosition, lerp);
        if (t == 1) {
          attackAnimation = null;
        }
      }));
    };
    Enemy.OnHeal += (int heal) => {
      AnimUtils.AddDamageOrHealNumber(heal, transform.position, false);
    };
    Enemy.OnHit += (int damage) => {
      AnimUtils.AddDamageOrHealNumber(damage, transform.position, true);
    };
    Enemy.OnRuneTriggered += (message) => AnimUtils.ShowFloatingText(message, transform.position);
  }

  // Update is called once per frame
  void Update() {
    if (attackAnimation == null) {
      transform.position = Vector3.Lerp(transform.position, new Vector3(Enemy.Coordinates.x, Enemy.Coordinates.y, 0), 0.1f);
    }
    if (Enemy.RuneList[0] != null) {
      UpdateRuneUI();
    }
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
