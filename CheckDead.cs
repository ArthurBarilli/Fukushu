using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class CheckDead : ActionNode
{
    public bool shield;
    public bool archer;
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if(context.enemylife.dead == true)
        {
            context.agent.ResetPath();
            context.enemyAi.auraBurst.SetActive(false);
            if (shield)
            {
                context.enemyAi.shieldInd.SetActive(false);
            }
            else if(archer)
            {
                context.enemyAi.ArrowInd.SetActive(false);
            }
            return State.Success;
        }
        else 
        {
            return State.Failure;
        }
        
    }
}
