using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class CheckPhase : ActionNode
{
    public int phaseLife;
    public int nextPhaseLife;
    protected override void OnStart() {  
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {

        if(context.enemylife.life <= phaseLife && context.enemylife.life > nextPhaseLife)
        {
            context.animator.SetTrigger("Idle");
            return State.Success;
        }
        else 
        {
            return State.Failure;
        }
    }
        
}
