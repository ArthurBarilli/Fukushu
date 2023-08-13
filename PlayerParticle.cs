using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class PlayerParticle : ActionNode
{
    public string ParticleName;
    public bool shield;
    protected override void OnStart() {
        context.enemyAi.attackIndicator01.Play();
        context.enemyAi.attackIndicator02.Play();
        if(shield)
        {
            context.enemyAi.shieldInd.SetActive(true);
        }
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        return State.Success;
    }
}
