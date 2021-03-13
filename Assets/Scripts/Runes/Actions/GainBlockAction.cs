public class GainBlockAction : RuneAction {
  public GainBlockAction(Entity entityToModify) : base(entityToModify) { }

  public override int Threshold => 22;

  public override RuneAction Clone(Entity otherEntity) {
    return new GainBlockAction(otherEntity);
  }

  bool isActive = false;

  public override void Perform() {
    OwningEntity.Block += 3;
    isActive = true;
  }

  internal override void OnEvent(GameEvent gameEvent) {
    if (isActive && FromOwnEntity(gameEvent) && gameEvent.GameEventType == GameEvent.EventType.DAMAGE_RECEIVED) {
      OwningEntity.Block -= 3;
      isActive = false;
    }
  }

  public override string Text() => "Block 3 damage from the next attack.";
}
