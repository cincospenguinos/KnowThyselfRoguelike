public class Rune {
    private Player _player;

    public Rune(Player player) {
      _player = player;    
    }

    public void EventOccurred(string eventName) {
      if (eventName == "EnemyDead") {
        // Here's the trigger
        
      }
    }
}