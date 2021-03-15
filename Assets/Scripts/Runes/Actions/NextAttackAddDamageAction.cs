public class NextAttackAddDamageAction : RuneAction {
  public NextAttackAddDamageAction(Entity entityToModify) : base(entityToModify) {
  }

  public override int Threshold => 60;

  public override RuneAction Clone(Entity otherEntity) {
    return new NextAttackAddDamageAction(otherEntity);
  }

  bool isActive = false;

  public override void Perform() {
    OwningEntity.AddedDamage += 5;
    isActive = true;
  }

  public override void OnAddedOrRemovedFromRune() {
    if (isActive) {
      OwningEntity.AddedDamage -= 5;
    }
    isActive = false;
  }

  internal override void OnEvent(GameEvent gameEvent) {
    if (isActive && FromOwnEntity(gameEvent) && gameEvent.GameEventType == GameEvent.EventType.DAMAGE_DEALT) {
      OwningEntity.AddedDamage -= 5;
      isActive = false;
    }
  }

  public override string Text() => "Your next attack deals +5 damage.";
}
