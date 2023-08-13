using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class Parriable : ActionNode
{
    public bool endParry;
    protected override void OnStart() {
        if (endParry == true)
        {
            context.enemyAi.isParriable = false;
        }
        else 
        {
            context.enemyAi.isParriable = true;
        }
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        return State.Success;
    }
}
