using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class GoToFlyingPlace : ActionNode
{
    public int flySpeed;
    public int phaseSpeed;
    protected override void OnStart() {
        context.agent.SetDestination(context.enemyAi.flyingPlace.position);
        context.agent.speed = flySpeed;
        context.animator.SetTrigger("Fly");
    }

    protected override void OnStop() {
        context.agent.speed = phaseSpeed;
    }

    protected override State OnUpdate() {
        if (Vector3.Distance(context.transform.position, context.enemyAi.flyingPlace.position) > 2)
        {
            return State.Running;
        }
        else 
        {
            context.agent.ResetPath();
            return State.Success;
        }
    }
}
