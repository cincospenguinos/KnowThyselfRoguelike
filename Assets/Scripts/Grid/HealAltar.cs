using System;
using UnityEngine;

public abstract class Altar : Entity {
  public event Action OnUsed;
  public bool Used = false;

  public Altar(Vector2Int coords) : base(coords, 100) {}

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
    Debug.Log("Hey show the screen homie");
    // TODO: Show the screen
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
    player.Heal(player.MaxHitPoints - player.HitPoints);
  }
}