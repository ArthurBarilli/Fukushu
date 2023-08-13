using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using UnityEngine.SceneManagement;

[System.Serializable]
public class EndGame : ActionNode
{
    protected override void OnStart() {
        if(context.enemyAi.FinalPhase)
        {
            SceneManager.LoadScene("FinalCutscene1");
        }
        else{
            SceneManager.LoadScene("FinalCutscene2");
        }
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        return State.Success;
    }
}
