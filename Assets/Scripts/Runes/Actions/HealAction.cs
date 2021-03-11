public class HealAction : RuneAction {
  private const int CHARGE_THRESHOLD = 66;

  public HealAction(Entity e) : base(e) {}

  public override void ReceiveCharge(int amount) {
    CurrentCharge += amount;

    while (CurrentCharge > CHARGE_THRESHOLD) {
      OwningEntity.Heal(2);
      CurrentCharge -= CHARGE_THRESHOLD;
    }
  }

  public override string Text() => "Heal 2 HP.";

  public override RuneAction Clone(Entity otherEntity) {
    return new HealAction(otherEntity);
  }
}