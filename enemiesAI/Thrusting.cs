using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class Thrusting : ActionNode
{
    public float thrustSpeed;
    protected override void OnStart() {
        context.enemyAi.dashFX.SetActive(true);
        context.enemyAi.dmgBox.SetActive(true);
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {

        if (context.enemyAi.wallHit == true)
        {
            context.enemyAi.dashFX.SetActive(false);
            context.enemyAi.dmgBox.SetActive(false);
            return State.Success;
        }
        else
        {
            context.agent.Move(context.transform.forward * thrustSpeed * Time.deltaTime);
            return State.Running;
        }
    }
}
