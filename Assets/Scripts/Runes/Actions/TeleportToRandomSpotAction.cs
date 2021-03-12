using System.Linq;
using UnityEngine;

public class TeleportToRandomSpotAction : RuneAction {
  public override int Threshold => 66;

  public TeleportToRandomSpotAction(Entity e) : base(e) { }

  public override void Perform() {
    Vector2Int newPosition = Grid.instance
      .EnumerateFloor()
      .Where(p => Grid.instance.Tiles[p.x, p.y] == Grid.TileType.FLOOR)
      .ToList()
      .GetRandom();
    OwningEntity.Coordinates = newPosition;
  }

  public override string Text() => "Teleport to a random location.";

  public override RuneAction Clone(Entity otherEntity) {
    return new TeleportToRandomSpotAction(otherEntity);
  }
}