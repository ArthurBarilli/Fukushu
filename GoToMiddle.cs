using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class GoToMiddle : ActionNode
{
    public bool running;
    public int runSpeed;
    public int phaseSpeed;
    protected override void OnStart() {
        if (running)
        {
            context.agent.speed = runSpeed;
            context.animator.SetTrigger("Run");
        }
        context.agent.SetDestination(context.enemyAi.middlePlace.position);
    }

    protected override void OnStop() {
        context.agent.speed = phaseSpeed;
    }

    protected override State OnUpdate() {
        if (Vector3.Distance(context.transform.position, context.enemyAi.middlePlace.position) < 1)
        {
            return State.Success;
        }
        else 
        {
            return State.Running;
        }
    }
}
