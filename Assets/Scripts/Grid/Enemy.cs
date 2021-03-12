using System.Linq;
using UnityEngine;

public class Enemy : Entity {
  bool hasDetectedPlayer = false;

  public Enemy(Vector2Int position) : base(position, 3) {
  }

  public void TakeTurn() {
    if (hasDetectedPlayer) {
      moveTowardsPlayerOrAttack();
    } else {
      hasDetectedPlayer = Vector2.Distance(_grid.Player.Coordinates, Coordinates) < 7;
      moveRandomly();
    }
  }

  public float DistanceTo(Entity other) {
    return Vector2.Distance(Coordinates, other.Coordinates);
  }

  public void moveTowardsPlayerOrAttack() {
    var player = _grid.Player;
    if (isNextTo(player)) {
      /// if we're already next to the player, attack them
      attack(player);
    } else {
      /// otherwise, move towards the empty adjacent tile that's closest to the player
      var directions = new Direction[] { Direction.NORTH, Direction.EAST, Direction.SOUTH, Direction.WEST };
      var newCoordinate = directions
        .Select(d => adjacentIn(d))
        .Where((c) => _grid.canOccupy(c))
        .OrderBy(c => Vector2.Distance(c, player.Coordinates))
        .FirstOrDefault();
      if (newCoordinate != null) {
        move(newCoordinate);
      }
    }
  }

  public void moveRandomly() {
    /// randomly move if possible
    var directions = new Direction[] { Direction.NORTH, Direction.EAST, Direction.SOUTH, Direction.WEST };
    var possibleNewCoordinates = directions.Select(d => adjacentIn(d)).Where((c) => _grid.canOccupy(c)).ToList();
    if (possibleNewCoordinates.Any()) {
      var randomIndex = UnityEngine.Random.Range(0, possibleNewCoordinates.Count);
      var newCoordinates = possibleNewCoordinates[randomIndex];
      move(newCoordinates);
    }
  }

  public override void onWalkInto(Player player) {
    player.attack(this);
  }
}
