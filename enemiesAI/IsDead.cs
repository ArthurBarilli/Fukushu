using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class IsDead : ActionNode
{
    protected override void OnStart() {
        context.enemyAi.isBody = true;
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
            return State.Success;      
    }
}
