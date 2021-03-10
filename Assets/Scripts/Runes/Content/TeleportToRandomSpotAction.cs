using UnityEngine;

public class TeleportToRandomSpotAction : RuneAction
{
    public TeleportToRandomSpotAction(Entity e) : base(e) {}

    public override void Apply() {
        Vector2Int newPosition = new Vector2Int(-1, -1);

        while (newPosition.x == -1 && newPosition.y == -1) {
            int x = Random.Range(0, Grid.WIDTH);
            int y = Random.Range(0, Grid.HEIGHT);

            if (Grid.instance.Tiles[x,y] == Grid.TileType.FLOOR) {
                newPosition = new Vector2Int(x, y);
            }
        }

        OwningEntity.Coordinates = newPosition;
    }

    public override RuneAction Clone(Entity otherEntity) {
        return new TeleportToRandomSpotAction(otherEntity);
    }
}