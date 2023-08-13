using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class Thrust : ActionNode
{
    bool prepare;
    public float counter;
    public float thrustTime;
    
    
    protected override void OnStart() {
        prepare = true;
    }

    protected override void OnStop() {
        counter = 0;
    }

    protected override State OnUpdate() {
        if (prepare == true)
        {
            if (counter < thrustTime)
            {
                counter += Time.deltaTime;
                context.transform.LookAt(new Vector3(blackboard.player.transform.position.x,context.transform.position.y, blackboard.player.transform.position.z));
            }
            else
            {
                prepare = false;
            }
        }
        else
        {
            context.animator.SetTrigger("Thrust");
            return State.Success;
        }
        return State.Running;
    }
}
