using System;
using UnityEngine;
using System.Collections.Generic;

public class RuneGenerator {
    private static Dictionary<string, Type> _all_triggers;
    private static Dictionary<string, Type> _all_actions;

    public static Rune generate(string triggerName, string actionName, Entity entity) {
        Type triggerType;

        if (!RuneGenerator.AllTriggers().TryGetValue(triggerName, out triggerType)) {
            Debug.LogError("COULD NOT GET TRIGGER GIVEN NAME \"" + triggerName + "\"");
            return new Rune(entity);
        }

        RuneTrigger trigger = (RuneTrigger) Activator.CreateInstance(triggerType, false);

        return new Rune(trigger, new IncreaseDamageAction(entity));
    }

    public static Dictionary<string, Type> AllTriggers() {
        if (RuneGenerator._all_triggers == null) {
            RuneGenerator._all_triggers = new Dictionary<string, Type>();
            RuneGenerator._all_triggers.Add("ThreeDeadEnemiesTrigger", typeof(ThreeDeadEnemiesTrigger));
        }

        return RuneGenerator._all_triggers;
    }

    public static Dictionary<string, Type> AllActions() {
        if (RuneGenerator._all_triggers == null) {
            RuneGenerator._all_actions = new Dictionary<string, Type>();
            RuneGenerator._all_actions.Add("IncreaseDamageAction", typeof(IncreaseDamageAction));
        }

        return RuneGenerator._all_actions;
    }
}