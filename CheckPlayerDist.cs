using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class CheckPlayerDist : ActionNode
{
    public int moveDist;
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if ( Vector3.Distance(blackboard.player.transform.position, context.transform.position) < moveDist)
        {
            return State.Success;
        }
        return State.Failure;
    }
}
