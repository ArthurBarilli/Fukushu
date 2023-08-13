using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class Shooting : ActionNode
{
    protected override void OnStart() {
        context.enemyAi.Shooting = true;
        context.enemyAi.ArrowInd.SetActive(true);
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        while (context.enemyAi.Shooting == true)
        {
            context.transform.LookAt(new Vector3(blackboard.player.transform.position.x,context.transform.position.y, blackboard.player.transform.position.z));
            return State.Running;
        }
        context.enemyAi.ArrowInd.SetActive(false);
        return State.Success;
        
    }
}
