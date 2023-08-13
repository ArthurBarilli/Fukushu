using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class LookToPlayer : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        context.transform.LookAt(new Vector3(blackboard.player.transform.position.x,context.transform.position.y, blackboard.player.transform.position.z));
        return State.Success;
    }
}
