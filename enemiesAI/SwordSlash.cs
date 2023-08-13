using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class SwordSlash : ActionNode
{
    protected override void OnStart() {
        context.transform.LookAt(blackboard.player.transform.position);
        context.enemyAi.isParriable = true;
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        return State.Success;
    }
}
