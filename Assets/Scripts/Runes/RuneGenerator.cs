using System;
using UnityEngine;
using System.Collections.Generic;

public class RuneGenerator {
    private static Dictionary<string, RuneTrigger> _all_triggers;
    private static Dictionary<string, Type> _all_actions;

    public static Rune generate(string triggerName, string actionName, Entity entity) {
        RuneTrigger trigger;

        if (!RuneGenerator.AllTriggers().TryGetValue(triggerName, out trigger)) {
            Debug.LogError("COULD NOT GET TRIGGER GIVEN NAME \"" + triggerName + "\"");
            return new Rune(entity);
        }

        return new Rune(trigger.Clone(), new IncreaseDamageAction(entity));
    }

    public static Dictionary<string, RuneTrigger> AllTriggers() {
        if (RuneGenerator._all_triggers == null) {
            RuneGenerator._all_triggers = new Dictionary<string, RuneTrigger>();
            RuneGenerator._all_triggers.Add("ThreeDeadEnemiesTrigger", new ThreeDeadEnemiesTrigger());
        }

        return RuneGenerator._all_triggers;
    }

    public static Dictionary<string, Type> AllActions() {
        if (RuneGenerator._all_actions == null) {
            RuneGenerator._all_actions = new Dictionary<string, Type>();
            RuneGenerator._all_actions.Add("IncreaseDamageAction", Type.GetType("IncreaseDamageAction"));
        }

        return RuneGenerator._all_actions;
    }
}