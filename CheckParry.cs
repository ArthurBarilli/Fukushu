using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class CheckParry : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (context.enemyAi.isParried == true)
        {
            context.agent.ResetPath();
            return State.Success;
        }
        else 
        {
            return State.Failure;
        }
    }
}
