using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class RushAttack : ActionNode
{
    Vector3 rushTarget;
    protected override void OnStart() {
        rushTarget = blackboard.player.transform.position;
        context.agent.speed = 15;
        context.animator.SetTrigger("Rush");
        context.agent.SetDestination(rushTarget);
        context.enemyAi.isRush = true;
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (Vector3.Distance(context.transform.position, rushTarget) < 1 )
        {
            context.animator.SetTrigger("Idle");
            context.agent.ResetPath();
            context.enemyAi.isRush = false;
            return State.Success;
        }
        else 
        {
            return State.Running;
        }
    }
}
