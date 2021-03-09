public class Rune {
    private Player _player;

    public Rune(Player player) {
      _player = player;    
    }

    public void EventOccurred(String eventName) {
      if (eventName == "EnemyDead") {
        // Here's the trigger
        
      }
    }
}