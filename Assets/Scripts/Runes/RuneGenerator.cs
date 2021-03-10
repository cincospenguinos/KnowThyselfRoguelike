using UnityEngine;
using System.Collections.Generic;

public class RuneGenerator {
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

    public static Dictionary<string, RuneTrigger> AllTriggers() {
        if (RuneGenerator._all_triggers == null) {
            RuneGenerator._all_triggers = new Dictionary<string, RuneTrigger>();
            RuneGenerator._all_triggers.Add("ThreeDeadEnemiesTrigger", new ThreeDeadEnemiesTrigger());
            RuneGenerator._all_triggers.Add("HalfHitPointsTrigger", new HalfHitPointsTrigger(null));
        }

        return RuneGenerator._all_triggers;
    }

    public static Dictionary<string, RuneAction> AllActions() {
        if (RuneGenerator._all_actions == null) {
            RuneGenerator._all_actions = new Dictionary<string, RuneAction>();
            RuneGenerator._all_actions.Add("IncreaseDamageAction", new IncreaseDamageAction(null));
        }

        return RuneGenerator._all_actions;
    }
}