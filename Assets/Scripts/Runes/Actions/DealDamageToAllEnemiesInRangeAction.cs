using System.Collections.Generic;
using UnityEngine;

public class DealDamageToAllEntitiesInRangeAction : RuneAction {
  public override int Threshold => 66;

  public DealDamageToAllEntitiesInRangeAction(Entity e) : base(e) {}

  public override void Perform() {
    foreach (var point in AllAdjacentTo(OwningEntity.Coordinates, 3)) {
      Entity e = Grid.instance.EntityAt(point);

      if (e != null && e != OwningEntity && !e.Dead) {
        e.TakeDamage(1);
        Grid.instance.EnqueueEvent(new GameEvent(GameEvent.EventType.DAMAGE_DEALT, OwningEntity));
      }
    }
  }

  public override RuneAction Clone(Entity otherEntity) {
      return new DealDamageToAllEntitiesInRangeAction(otherEntity);
  }

  public override string Text() => "deal 1 damage to enemies in range 3.";

  private HashSet<Vector2Int> AllAdjacentTo(Vector2Int point, int surrounding) {
    if (surrounding == 1) {
      return new HashSet<Vector2Int>(pointsSurrounding(point));
    }

    HashSet<Vector2Int> aggregate = new HashSet<Vector2Int>();
    foreach(var pointToAggregate in pointsSurrounding(point)) {
      foreach(var p in AllAdjacentTo(pointToAggregate, surrounding - 1)) {
        aggregate.Add(p);
      }
    }

    return aggregate;
  }

  private Vector2Int[] pointsSurrounding(Vector2Int point) {
    return new Vector2Int[] {
      new Vector2Int(point.x + 1, point.y),
      new Vector2Int(point.x - 1, point.y),
      new Vector2Int(point.x, point.y + 1),
      new Vector2Int(point.x, point.y - 1),
    };
  }
}