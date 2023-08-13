using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class PlayAnimation : ActionNode
{
    public bool isTrigger;
    public string triggerAnim;
    public string boolAnim;
    public AnimatorControllerParameter[] parameters;

// Loop through the trigger parameters and reset them

    protected override void OnStart() {
        parameters = context.animator.parameters;
        foreach (AnimatorControllerParameter parameter in parameters)
        {
            if (parameter.type == AnimatorControllerParameterType.Trigger)
            {
            context.animator.ResetTrigger(parameter.name);
            }
        }
        if (isTrigger)
        {
            context.animator.SetTrigger(triggerAnim);
        }
        else 
        {
             context.animator.SetBool(boolAnim, true);
        }
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        return State.Success;
    }
}
