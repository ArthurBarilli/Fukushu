using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class CheckSecondLife : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (context.enemylife.secondLife == true)
        {
            return State.Success;
        }
        else 
        {
            return State.Failure;
        }
    }
}
