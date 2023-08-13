using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class CheckDizzy : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (context.enemyAi.dizzy == true)
        {
            context.enemylife.canTake = true;
            context.animator.SetTrigger("Dizzy");
            return State.Success;
        }
        else 
        {
            return State.Failure;
        }
        
    }
}
