using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class ResetTriggers : ActionNode
{
    protected override void OnStart() 
    {
        AnimatorControllerParameter[] parameters = context.animator.parameters;
        foreach (AnimatorControllerParameter parameter in parameters)
        {
            if (parameter.type == AnimatorControllerParameterType.Trigger)
            {
                context.animator.ResetTrigger(parameter.name);
            }
        }
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        
        return State.Success;
    }
}
