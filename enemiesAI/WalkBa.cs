using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class WalkBa : ActionNode
{
    public float walkTime;
    public float counter;
    public float speed;
    protected override void OnStart() {
        context.animator.SetTrigger("WalkBack");
        counter = 0;
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        counter += Time.fixedDeltaTime;
        if (counter <= walkTime)
        {
            context.agent.Move(((blackboard.player.transform.position) - (context.transform.position)).normalized * Time.deltaTime * speed * context.animator.GetFloat("Time"));
        }
        else if(counter > walkTime)
        {
            return State.Success;
        }
        return State.Running;
        
    }
}
