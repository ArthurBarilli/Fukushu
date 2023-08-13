using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class SummonCuts : ActionNode
{
    public bool par;
    protected override void OnStart() {
        if (par)
        {
            foreach(Transform slashes in context.enemyAi.pares.positions)
            {
                GameObject.Instantiate(context.enemyAi.finalBossScenarioCuts, slashes.position, Quaternion.identity);
            }
        }
        else
        {
            foreach(Transform slashes in context.enemyAi.impares.positions)
            {
                GameObject.Instantiate(context.enemyAi.finalBossScenarioCuts, slashes.position, Quaternion.identity);
            }
        }
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        return State.Success;
    }
}
