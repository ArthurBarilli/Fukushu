using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class GetPorrete : ActionNode
{
    public string RunningTrigger;
    public float closeDist;
    GameObject porrete;

    protected override void OnStart() {
        context.animator.SetTrigger(RunningTrigger);
        porrete = GameObject.FindGameObjectWithTag("Porrete");
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {

        context.agent.SetDestination(porrete.transform.position);
        context.agent.speed = 10;
        if ((Vector3.Distance(context.transform.position, porrete.transform.position) < closeDist))
        {
            context.agent.ResetPath();
            context.agent.speed = 6;
            context.animator.SetTrigger("Idle");
            porrete.GetComponent<PorreteProjectile>().Get();
            context.enemyAi.porrete.SetActive(true);
            return State.Success;
        }
        return State.Running;
    }
}
