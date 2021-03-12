using System;
using UnityEngine;

public class HealAltar : Entity {
  public event Action OnUsed;
  public bool used = false;
  public HealAltar(Vector2Int coords) : base(coords, 100) {
  }

  public override void onWalkInto(Player player) {
    if (!used) {
      used = true;
      Use(player);
    }
  }

  void Use(Player player) {
    OnUsed?.Invoke();
    player.Heal(player.MaxHitPoints - player.HitPoints);
  }
}