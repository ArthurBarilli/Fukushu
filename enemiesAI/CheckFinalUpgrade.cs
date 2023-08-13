using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class CheckFinalUpgrade : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (context.enemyAi.FinalPhase == true)
        {
            context.enemylife.dead = true;
            return State.Failure;
        }
        else
        {
            return State.Success;
        }
    }
}
