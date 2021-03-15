public class AddDamageTakeDamageAction : RuneAction {
  public AddDamageTakeDamageAction(Entity entityToModify) : base(entityToModify) {
  }

  public override int ThresholdBase => 54;

  public override RuneAction Clone(Entity otherEntity) {
    return new NextAttackAddDamageAction(otherEntity);
  }

  bool isActive = false;

  public override void Perform() {
    OwningEntity.TakeDamage(3);
    OwningEntity.AddedDamage += 12;
    isActive = true;
  }

  public override void OnAddedOrRemovedFromRune() {
    if (isActive) {
      OwningEntity.AddedDamage -= 12;
    }
    isActive = false;
  }

  internal override void OnEvent(GameEvent gameEvent) {
    if (isActive && FromOwnEntity(gameEvent) && gameEvent.GameEventType == GameEvent.EventType.DAMAGE_DEALT) {
      OwningEntity.AddedDamage -= 12;
      isActive = false;
    }
  }

  public override string Text() => "take 3 damage; your next attack deals +12 damage.";
}
