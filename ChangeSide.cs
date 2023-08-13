using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class ChangeSide : ActionNode
{
    public float chance;

    protected override void OnStart() {
        
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (Random.value > chance)
        {
            //n aconteceu
            return State.Success;
        }
        else 
        {
            //aconteceu
            if (context.enemyAi.currentSpinning.GetComponent<Spin>().left)
            {
                context.enemyAi.currentSpinning.GetComponent<Spin>().left = false;
            }
            else
            {
                context.enemyAi.currentSpinning.GetComponent<Spin>().left = true;
            }
        }
        return State.Success;
    }
}
