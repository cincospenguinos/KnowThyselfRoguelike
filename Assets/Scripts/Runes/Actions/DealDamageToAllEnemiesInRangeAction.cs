using System.Collections.Generic;
using UnityEngine;

public abstract class DamageEntitiesInRangeAction : RuneAction {
  public abstract int damage { get; }
  public abstract int range { get; }

  public DamageEntitiesInRangeAction(Entity e) : base(e) {}

  public override void Perform() {
    foreach (var point in AllAdjacentTo(OwningEntity.Coordinates, range)) {
      Entity e = Grid.instance.EntityAt(point);

      if (e != null && e != OwningEntity && !e.Dead) {
        e.TakeDamage(damage);
        Grid.instance.EnqueueEvent(new GameEvent(GameEvent.EventType.DAMAGE_DEALT, OwningEntity));
      }
    }
  }

  public override string Text() => $"deal {damage} damage to enemies in range {range}.";

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

public class Damage1EntitiesInRange3Action : DamageEntitiesInRangeAction {
  public Damage1EntitiesInRange3Action(Entity e) : base(e) { }

  public override int damage => 1;

  public override int range => 3;

  public override int Threshold => 66;

  public override RuneAction Clone(Entity otherEntity) {
    return new Damage1EntitiesInRange3Action(otherEntity);
  }
}

public class Damage2EntitiesInRange2Action : DamageEntitiesInRangeAction {
  public Damage2EntitiesInRange2Action(Entity e) : base(e) { }

  public override int damage => 2;

  public override int range => 2;

  public override int Threshold => 56;

  public override RuneAction Clone(Entity otherEntity) {
    return new Damage2EntitiesInRange2Action(otherEntity);
  }
}

public class Damage3EntitiesInRange1Action : DamageEntitiesInRangeAction {
  public Damage3EntitiesInRange1Action(Entity e) : base(e) { }

  public override int damage => 3;

  public override int range => 1;

  public override int Threshold => 36;

  public override RuneAction Clone(Entity otherEntity) {
    return new Damage2EntitiesInRange2Action(otherEntity);
  }
}