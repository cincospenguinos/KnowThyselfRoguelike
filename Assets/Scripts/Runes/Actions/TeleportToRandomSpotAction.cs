using System.Linq;
using UnityEngine;

public class TeleportToRandomSpotAction : RuneAction {
  private const int CHARGE_THRESHOLD = 66;

  public TeleportToRandomSpotAction(Entity e) : base(e) { }

  public override void ReceiveCharge(int amount) {
    CurrentCharge += amount;

    while (CurrentCharge > CHARGE_THRESHOLD) {
      Perform();
      CurrentCharge -= CHARGE_THRESHOLD;
    }

  }

  void Perform() {
    Vector2Int newPosition = Grid.instance
      .EnumerateFloor()
      .Where(p => Grid.instance.Tiles[p.x, p.y] == Grid.TileType.FLOOR)
      .ToList()
      .GetRandom();
    OwningEntity.Coordinates = newPosition;
  }

  public override string Text() => "teleport to a random location.";

  public override RuneAction Clone(Entity otherEntity) {
    return new TeleportToRandomSpotAction(otherEntity);
  }
}