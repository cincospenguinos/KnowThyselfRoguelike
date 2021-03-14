using System.Linq;
using UnityEngine;

public abstract class Enemy : Entity {
  public bool hasDetectedPlayer = false;

  public Enemy(Vector2Int coords, int HitPoints) : base(coords, HitPoints) { }

  public abstract void TakeTurn();

  public override void onWalkInto(Player player) {
    player.attack(this);
  }

  public void moveTowardsPlayerOrAttack(int numMoves = 1) {
    var player = _grid.Player;
    if (isNextTo(player)) {
      /// if we're already next to the player, attack them
      attack(player);
    } else {
      for ( ; numMoves > 0 ; numMoves--) {
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
}

public class Enemy0 : Enemy {
  public override int BaseDamage => Random.Range(4, 7);

  public Enemy0(Vector2Int position) : base(position, 15) { }

  public override void TakeTurn() {
    if (hasDetectedPlayer) {
      moveTowardsPlayerOrAttack();
    } else {
      hasDetectedPlayer = Vector2.Distance(_grid.Player.Coordinates, Coordinates) < 7;
      moveRandomly();
    }
  }
}

public class Enemy1 : Enemy {
  public override int BaseDamage => Random.Range(7, 11);
  public bool mustWait = true;

  public Enemy1(Vector2Int position) : base(position, 30) { }

  public override void TakeTurn() {
    if (mustWait) {
      mustWait = false;
      wait();
      return;
    }
    if (hasDetectedPlayer) {
      moveTowardsPlayerOrAttack();
    } else {
      hasDetectedPlayer = Vector2.Distance(_grid.Player.Coordinates, Coordinates) < 7;
      moveRandomly();
    }
    mustWait = true;
  }
}

public class Enemy2 : Enemy {
  public override int BaseDamage => Random.Range(2, 5);
  public Enemy2(Vector2Int position) : base(position, 7) { }

  public override void TakeTurn() {
    if (hasDetectedPlayer) {
      moveTowardsPlayerOrAttack(2);
    } else {
      hasDetectedPlayer = Vector2.Distance(_grid.Player.Coordinates, Coordinates) < 7;
      moveRandomly();
    }
  }
}