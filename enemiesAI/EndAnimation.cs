using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class EndAnimation : ActionNode
{
    public string Animation;
    protected override void OnStart() {
        context.animator.SetBool(Animation, false);
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        return State.Success;
    }
}
