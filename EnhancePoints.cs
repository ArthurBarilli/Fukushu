using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class EnhancePoints : ActionNode
{
    protected override void OnStart() {
        GameManager.Instance.AddPoints(context.enemyAi.points);
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        return State.Success;
    }
}
