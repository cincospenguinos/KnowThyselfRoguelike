using System.Linq;
using UnityEngine;

public class TeleportToRandomSpotAction : RuneAction {
  public override int ThresholdBase => 66;

  public TeleportToRandomSpotAction(Entity e) : base(e) { }

  public override void Perform() {
    Vector2Int newPosition = Grid.instance
      .EnumerateFloor()
      .Where(Grid.instance.canOccupy)
      .ToList()
      .GetRandom();
    OwningEntity.SetCoordinates(newPosition);
  }

  public override string Text() => "Teleport to a random location.";

  public override RuneAction Clone(Entity otherEntity) {
    return new TeleportToRandomSpotAction(otherEntity);
  }
}