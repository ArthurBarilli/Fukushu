using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class Die : ActionNode
{
    protected override void OnStart() {
        context.enemyAi.isBody = true;
        GameManager.Instance.RemoveEnemyFromList(context.gameObject);
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        return State.Success;
    }
}
