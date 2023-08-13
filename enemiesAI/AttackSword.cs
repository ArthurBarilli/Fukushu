using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class AttackSword : ActionNode
{
    private float counter;
    public float dashTime;
    public int speed;
    public Vector3 playerLastPosition;
    protected override void OnStart() {
    counter = 0;
    context.transform.LookAt(blackboard.player.transform.position); 
    playerLastPosition = blackboard.player.transform.position;
    context.enemyAi.isParriable = true;
    context.enemyAi.shieldInd.SetActive(false);
    }

    protected override void OnStop() {
        context.enemyAi.isParriable = false;
    }

    protected override State OnUpdate() {
         if (counter < dashTime)
        {
            
            context.agent.Move(((playerLastPosition) - (context.transform.position)).normalized * Time.deltaTime * speed * context.animator.GetFloat("Time"));
            counter += Time.deltaTime * context.animator.GetFloat("Time");
        }
        else 
        {
            return State.Success;
        }
        return State.Running;
    }
}
