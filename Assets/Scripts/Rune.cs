public class Rune {
  private Entity _owningEntity;

  public Rune(Entity entity) {
    _owningEntity = entity;    
  }

  public void EventOccurred(string eventName) {
    // Every time an enemy dies, double the damage modifier
    if (eventName == "EnemyDead") {
      _owningEntity.DamageModifier += 2;
    }
  }
}