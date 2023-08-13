using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class CheckExhaust : ActionNode
{
    public int exhaustLimit;
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (context.enemyAi.exhaustCount >= exhaustLimit)
        {
            context.enemyAi.exhausted = true;
            return State.Success;
        }
        else
        {
            context.enemyAi.exhausted = false;
            return State.Failure;
        }
    }
}
