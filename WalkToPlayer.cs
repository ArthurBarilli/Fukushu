using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class WalkToPlayer : ActionNode
{
    public int closeDist;
    public string walkingTrigger;
    public string stopAnimation;
    public bool changeSpeed;
    public int newSpeed;
    public int oldSpeed;

    protected override void OnStart() {
        context.animator.SetTrigger(walkingTrigger);
        if (changeSpeed)
        {
            context.agent.speed = newSpeed;
        }
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        context.agent.SetDestination(blackboard.player.transform.position);
        context.agent.speed = context.agent.speed * context.animator.GetFloat("Time");
        if ((Vector3.Distance(context.transform.position, blackboard.player.transform.position) < closeDist))
        {
            context.agent.ResetPath();
            context.animator.SetTrigger(stopAnimation);
            if (changeSpeed)
            {
                context.agent.speed = oldSpeed;
            }
            return State.Success;
        }
        return State.Running;
    }
}
