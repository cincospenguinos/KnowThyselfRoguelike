public class ReceiveDamageTrigger : RuneTrigger {
  public override int Charge => 17;

  public ReceiveDamageTrigger(Entity e) : base(e) { }

  public override RuneTrigger Clone() {
    return new ReceiveDamageTrigger(OwningEntity);
  }

  public override int OnEvent(GameEvent gameEvent) {
    bool entityReceivedDamage = FromOwnEntity(gameEvent) && gameEvent.GameEventType == GameEvent.EventType.DAMAGE_RECEIVED;

    return entityReceivedDamage ? Charge : 0;
  }

  public override void Reset() { }

  public override string Text() => "when you take damage.";
}