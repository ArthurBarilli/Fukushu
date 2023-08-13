using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class IsMedDist : ActionNode
{
    public float closeDist;
    public float farDist;
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if ( Vector3.Distance(blackboard.player.transform.position, context.transform.position) > closeDist && Vector3.Distance(blackboard.player.transform.position, context.transform.position) < farDist)
        {
            return State.Success;
        }
        return State.Failure;
    }
}
