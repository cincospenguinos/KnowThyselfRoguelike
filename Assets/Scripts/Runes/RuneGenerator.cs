using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class RuneGenerator {
  // TODO: Refactor to this, maybe:
  // typeof(HalfHitPointsTrigger).GetConstructor(new Type[]{})
  private static Dictionary<string, RuneTrigger> _all_triggers;
  private static Dictionary<string, RuneAction> _all_actions;

  public static Rune generate(string triggerName, string actionName, Entity entity) {
    RuneTrigger trigger;
    RuneAction action;

    if (!RuneGenerator.AllTriggers().TryGetValue(triggerName, out trigger)) {
      Debug.LogError("COULD NOT GET TRIGGER GIVEN NAME \"" + triggerName + "\"");
      return new Rune(entity);
    }

    if (!RuneGenerator.AllActions().TryGetValue(actionName, out action)) {
      Debug.LogError("COULD NOT GET TRIGGER GIVEN NAME \"" + actionName + "\"");
      return new Rune(entity);
    }

    trigger = trigger.Clone();
    trigger.OwningEntity = entity;

    action = action.Clone(entity);

    return new Rune(trigger, action);
  }

  public static Rune generateRandom(Entity entity) {
    string triggerName = new List<string>(RuneGenerator.AllTriggers().Keys).GetRandom();
    string actionName = new List<string>(RuneGenerator.AllActions().Keys).GetRandom();

    return RuneGenerator.generate(triggerName, actionName, entity);
  }

  public static RuneTrigger randomTrigger(List<RuneShard> blocklist) {
    var list = AllTriggers().Values.ToList();
    list.RemoveAll(t => blocklist.Contains(t));

    return list.GetRandom();
  }

  public static RuneAction randomAction(List<RuneShard> blocklist) {
    var list = AllActions().Values.ToList();
    list.RemoveAll(t => blocklist.Contains(t));

    return list.GetRandom();
  }

  public static Dictionary<string, RuneTrigger> AllTriggers() {
    if (RuneGenerator._all_triggers == null) {
      RuneGenerator._all_triggers = new Dictionary<string, RuneTrigger>();
      RuneGenerator._all_triggers.Add("EntityDiesTrigger", new EntityDiesTrigger());
      RuneGenerator._all_triggers.Add("HalfHitPointsTrigger", new HalfHitPointsTrigger(null));
      RuneGenerator._all_triggers.Add("MovementTrigger", new MovementTrigger(null));
      RuneGenerator._all_triggers.Add("DealDamageTrigger", new DealDamageTrigger(null));
      RuneGenerator._all_triggers.Add("ReceiveDamageTrigger", new ReceiveDamageTrigger(null));
      RuneGenerator._all_triggers.Add("Every50TurnsTrigger", new Every50TurnsTrigger(null));
      RuneGenerator._all_triggers.Add("Every100TurnsTrigger", new Every100TurnsTrigger(null));
      RuneGenerator._all_triggers.Add("Every200TurnsTrigger", new Every200TurnsTrigger(null));
      RuneGenerator._all_triggers.Add("TenTurnsNoDamage", new TenTurnsNoDamage(null));
    }

    return RuneGenerator._all_triggers;
  }

  public static Dictionary<string, RuneAction> AllActions() {
    if (RuneGenerator._all_actions == null) {
      RuneGenerator._all_actions = new Dictionary<string, RuneAction>();
      RuneGenerator._all_actions.Add("IncreaseDamageAction", new IncreaseDamageAction(null));
      RuneGenerator._all_actions.Add("IncreaseDamageSmallAction", new IncreaseDamageSmallAction(null));
      RuneGenerator._all_actions.Add("HealTinyAction", new HealTinyAction(null));
      RuneGenerator._all_actions.Add("HealMediumAction", new HealMediumAction(null));
      RuneGenerator._all_actions.Add("HealLargeAction", new HealLargeAction(null));
      RuneGenerator._all_actions.Add("TeleportToRandomSpotAction", new TeleportToRandomSpotAction(null));
      RuneGenerator._all_actions.Add("IncreaseViewRadiusAction", new IncreaseViewRadiusAction(null));
      RuneGenerator._all_actions.Add("NextAttackAddDamageAction", new NextAttackAddDamageAction(null));
      RuneGenerator._all_actions.Add("GainBlockAction", new GainBlockAction(null));
      RuneGenerator._all_actions.Add("PermanentBlockAction", new PermanentBlockAction(null));
      RuneGenerator._all_actions.Add("LessHPMoreBlockAction", new LessHPMoreBlockAction(null));
      RuneGenerator._all_actions.Add("IncreaseMaxHitPoints", new IncreaseMaxHitPoints(null));
      RuneGenerator._all_actions.Add("Damage1EntitiesInRange3Action", new Damage5EntitiesInRange3Action(null));
      RuneGenerator._all_actions.Add("Damage2EntitiesInRange2Action", new Damage8EntitiesInRange2Action(null));
      RuneGenerator._all_actions.Add("Damage3EntitiesInRange1Action", new Damage12EntitiesInRange1Action(null));
    }

    return RuneGenerator._all_actions;
  }
}