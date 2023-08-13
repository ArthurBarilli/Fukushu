using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class IsClose : ActionNode
{
    public int closeDist;
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if ( Vector3.Distance(blackboard.player.transform.position, context.transform.position) < closeDist)
        {
            return State.Success;
        }
        return State.Failure;
    }
}
