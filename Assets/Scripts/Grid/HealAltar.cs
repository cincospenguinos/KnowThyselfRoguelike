using System;
using UnityEngine;

public abstract class Altar : Entity {
  public event Action OnUsed;
  public bool Used = false;
  public override int BaseDamage => 0;

  public Altar(Vector2Int coords) : base(coords, 1000) {
    RuneList.Clear();
  }

  public override void onWalkInto(Player player) {
    if (!Used) {
      Used = true;
      Use(player);
    }
  }

  protected void TriggerOnUse() {
    OnUsed?.Invoke();
  }

  public abstract void Use(Player player);
}

public class RuneEditAltar : Altar {
  public RuneEditAltar(Vector2Int coords) : base(coords) {}

  public override void Use(Player player) {
    TriggerOnUse();
    player.EditingRunes = true;
    GameObject.Find("GameManager").GetComponent<RuneEditorAcceptButtonManager>().Button.SetActive(true);
  }
}

public class HealAltar : Altar {
  public HealAltar(Vector2Int coords) : base(coords) {}

  public override void onWalkInto(Player player) {
    if (!Used) {
      Used = true;
      Use(player);
    }
  }

  public override void Use(Player player) {
    TriggerOnUse();
    player.Heal(-1);
  }
}

public class UpgradeAltar : Altar {
  public UpgradeAltar(Vector2Int coords) : base(coords) {}

  public override void Use(Player player) {
    TriggerOnUse();
    Debug.Log("Hey show the upgrade dialog screen homie");
    // TODO: Show the screen
  }
}