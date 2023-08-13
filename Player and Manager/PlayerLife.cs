using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class PlayerLife : MonoBehaviour
{
    public int life;
    public bool move = true;
    public bool Dying = false;
    public bool dead;
    public Animator anim;
    public DialogueManager dialogueManager;

    public Attack attack;
    bool doOnce = true;
    public bool canTake = true;
    public VisualEffect[] bloodFx;


    // Start is called before the first frame update
    public void Start()
    {
        canTake = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (life <= 0)
        {
            Die();
            foreach(VisualEffect blood in bloodFx)
            {
                blood.SendEvent("Squirt");
            }
        }
    }

    public void AnimationDeath(){
        
        if(doOnce){
            anim.SetTrigger("Death");
            GetComponent<TP_movement>().enabled = false;
            attack.enabled = false;
            doOnce = false;
        }
    }

    public void Die()
    {
        dialogueManager.dialogueImages.Clear();
        dialogueManager.sentences.Clear();
        dialogueManager.image = null;
        dialogueManager.sentence = null;
        dead = true;

        GameManager.Instance.Die(); 
    }
    public void TakeDamage(int dmg)
    {
        life -= dmg;
    }



}
