using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class IsFar : ActionNode
{
    public int medDist;
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() 
    {
        if ( Vector3.Distance(blackboard.player.transform.position, context.transform.position) >= medDist)
        {
            return State.Success;
        }
        return State.Failure;
    }
}
