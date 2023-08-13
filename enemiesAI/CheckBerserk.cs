using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class CheckBerserk : ActionNode
{
    public GameObject porrete;
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (context.enemylife.life <= 2)
        {
            context.agent.ResetPath();
            context.agent.speed = 13;
            context.animator.SetTrigger("Rage");
            context.enemyAi.secondPhase = true;
            if ((porrete = GameObject.FindGameObjectWithTag("Porrete")) != null)
            {
                porrete.GetComponent<PorreteProjectile>().Get();
                context.enemyAi.porrete.SetActive(true);
            }
            return State.Success;
        }
        else
        {
            return State.Failure;
        }
        
    }
}
