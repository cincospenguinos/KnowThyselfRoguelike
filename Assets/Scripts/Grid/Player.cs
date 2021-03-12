public class Player : Entity {
  public Player() : base(new Vector2Int(3, 3), 20) {}

  public override void onWalkInto(Player player) {
    // no op
  }
}