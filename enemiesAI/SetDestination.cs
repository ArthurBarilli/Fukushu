using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class SetDestination : ActionNode
{
    public Transform placeToGo;
    protected override void OnStart() {
        context.agent.SetDestination(placeToGo.position);
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        return State.Success;
    }
}
