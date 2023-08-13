using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class PlayAuraBurst : ActionNode
{
    protected override void OnStart() {
        context.enemyAi.auraBurst.SetActive(true);
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        return State.Success;
    }
}
