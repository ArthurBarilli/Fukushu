using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class GetVulnerable : ActionNode
{
    public bool can;
    protected override void OnStart() {
        if (can == true)
        {
            context.enemylife.canTake = true;
        }
        else 
        {
            context.enemylife.canTake = false;
        }
        
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        return State.Success;
    }
}
