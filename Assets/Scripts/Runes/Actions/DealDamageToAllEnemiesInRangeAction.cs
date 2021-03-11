using System.Collections.Generic;
using UnityEngine;

public class DealDamageToAllEntitiesInRangeAction : RuneAction {
  private const int CHARGE_THRESHOLD = 66;

  public DealDamageToAllEntitiesInRangeAction(Entity e) : base(e) {}

  public override void ReceiveCharge(int amount) {
    CurrentCharge += amount;

    while (CurrentCharge > CHARGE_THRESHOLD) {
      List<Entity> entitiesToDamage = new List<Entity>();

      foreach (var point in AllAdjacentTo(OwningEntity.Coordinates, 3)) {
        Entity e = Grid.instance.EntityAt(point);

        if (e != null && e != OwningEntity && !e.Dead) {
          e.TakeDamage(1);
          Grid.instance.EnqueueEvent(new GameEvent(GameEvent.EventType.DAMAGE_DEALT, OwningEntity));
        }
      }

      CurrentCharge -= CHARGE_THRESHOLD;
    }
  }

  public override RuneAction Clone(Entity otherEntity) {
      return new DealDamageToAllEntitiesInRangeAction(otherEntity);
  }

  public override string Text() => " apply 1 damage to all entities within 3 squares";

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