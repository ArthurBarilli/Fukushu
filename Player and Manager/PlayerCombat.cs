using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{


    //components
    public Animator anim;
    public TP_movement movement;
    public PlayerLife life;
    public EnemiesInRange enemiesObj;
    public List<EnemyAI> enemies;
    public List<Arrow> arrows;

    //attackparticles
    public ParticleSystem bAttackEffect1;
    public ParticleSystem bAttackEffect2;

    //Bools
    public bool canAttack;
    public bool parrying;
    public bool canParry = true;
    public bool slowed;

    //skilltreebools

    public bool chargeAttack;
    public bool parrySlowTime;
    public bool parryProjectileBack;


    //espada
    public GameObject espada;
    public GameObject SlashAA;
    public BoxCollider hitCollider;

    //charges
    public float chargeProgress;
    public float chargeIntensity;
    public bool fullCharge;
    public GameObject chargeSlash;
    public Transform chargeSlashPlace;

    [Header("VFX")]
    public GameObject Charging;
    public ParticleSystem finishCharging; 
    public VisualEffect slashFx;
    public Transform parryBlinkPosition;
    public VisualEffect parryBlink;
    public GameObject slashFxObj;
    public Transform slashPlace;
    public ParticleSystem[] parryEffect;

    [Header("aim")]
    public GameObject aim;
    public Image aimSprite;
    public RaycastHit aimHit;
    public Ray aimRay;
    public Transform aimTranform;
    public float indicatorPosition;
    public float aimCounter;

    // Start is called before the first frame update
    void Start()
    {
        canParry = true;
        enemies = enemiesObj.enemies;
        arrows = enemiesObj.arrows;
        canAttack = true;
        anim = GetComponent<Animator>();
        movement = GetComponent<TP_movement>();
        life = GetComponent<PlayerLife>();
        if (GameManager.Instance.isParryProjectile == true)
        {
            parryProjectileBack = true;
        }
        if (GameManager.Instance.isParrySlow == true)
        {
            parrySlowTime = true;
        }
        if (GameManager.Instance.isChargeAttack == true)
        {
            chargeAttack = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        movement.isParrying = parrying;

        //normal Attack

        if (Input.GetMouseButtonUp(0) && canAttack)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                movement.LookDir = hit.point;
                //Debug.Log(hit.collider.gameObject);
                movement.rigging.weight = 1f;
                //movement.target.transform.position = hit.point;
            }
            anim.SetTrigger("Attack");
            if (fullCharge)
            {
                fullCharge = false;
                chargeProgress = 0;
                movement.Attack = true;
                //anim.SetTrigger("ChargeAttack");
                // Ataque carregado, inserir som aqui nessa linha
                GameObject projectile =  Instantiate(chargeSlash, chargeSlashPlace.position, chargeSlashPlace.rotation);
                projectile.GetComponent<ProjectileSpeed>().projectileDir = new Vector3(movement.LookDir.x, projectile.transform.position.y, movement.LookDir.z);
            }
            //bAttackEffect1.Play();
            movement.Attack = true;
            StartCoroutine(BAttackCd());
        }
        if(Input.GetMouseButtonUp(0) && !fullCharge)
        {
            chargeProgress = 0;
            Charging.SetActive(false);
        }

        //parry
        if (Input.GetMouseButtonDown(1) && canParry)
        {
            canParry = false;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 200))
            {
                movement.LookDir = hit.point;
            }
            anim.SetBool("Parry", true);
            life.canTake = false;
            parrying = true;
            StartCoroutine(Parry());
        }


        //charge Attack
        if (Input.GetMouseButton(0) && chargeAttack && fullCharge == false)
        {
            bool doOnce = false;
            chargeProgress= chargeProgress + Time.deltaTime * chargeIntensity;
            Charging.gameObject.SetActive(true);
            if (chargeProgress >= 100)
            {
                
                fullCharge = true;
                Charging.SetActive(false);
                chargeProgress = 100;
                if (doOnce == false)
                {
                    doOnce = true;
                    finishCharging.Play();
                }
            }
        }
        
       


        //parry

        if (parrying)
        {
        foreach (var enemy in enemies)
        {
            
            if(enemy.isParriable == true)
            {
                Debug.Log(enemy.name);
                enemy.isParriable = false;
                enemy.isParried = true;
                Instantiate(parryBlink, parryBlinkPosition.position, parryBlinkPosition.rotation);
                if (parrySlowTime == true)
                {
                    GameManager.Instance.SlowDownTime();
                }
            }
            
        }
        foreach (var arrow in arrows)
        {
            if (parryProjectileBack == true)
            {
                if (arrow.fliped == false)
                {
                    arrow.fliped = true;
                    Instantiate(parryBlink, parryBlinkPosition.position, parryBlinkPosition.rotation);
                    arrow.transform.rotation = gameObject.transform.rotation;
                    arrow.direction = gameObject.transform.forward;
                    arrow.hitDir = transform;
                    
                }  
            }
            else if (parryProjectileBack == false)
            {
                Instantiate(parryBlink, parryBlinkPosition.position, parryBlinkPosition.rotation);
                arrow.GetComponent<Arrow>().DeflectArrow(gameObject.transform);
                arrows.Remove(arrow);
            }
                   
        }
        }
        //aim update
        aimRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(aimRay, out aimHit, 200))
        {
            aim.transform.LookAt(new Vector3(aimHit.point.x,aim.transform.position.y,aimHit.point.z ));
        }

        aimTranform.localPosition = new Vector3(0,indicatorPosition,0);
        
    }
    private IEnumerator ParryCD()
    {
        float aimCounter = 0f;
        while (aimCounter < 1.5)
        {
            indicatorPosition = Mathf.Lerp(-6.27f, 0, aimCounter / 1.5f);
            aimCounter += Time.deltaTime;
            yield return null;
        }

        // Ensure the value reaches the target exactly
        indicatorPosition = 0;
    }

    public void SendSlash()
    {
        Instantiate(slashFxObj, slashPlace.position, slashPlace.rotation);
        hitCollider.enabled=true;
        Debug.Log("bateu");
    }
    public void SlashDisable(float colliderTime)
    {
        StartCoroutine(SlashCounter(colliderTime));
    }
    IEnumerator SlashCounter(float time)
    {
        yield return new WaitForSeconds(time);
        hitCollider.enabled = false;
    }

    IEnumerator BAttackCd()
    {
        canAttack = false;
        yield return new WaitForSeconds(0.5f);
        canAttack = true;
    }
    IEnumerator Parry()
    {
        indicatorPosition = 0;
        foreach(ParticleSystem parryfx in parryEffect)
        {
            parryfx.Play();
        }
        StartCoroutine(ParryCD());
        yield return new WaitForSeconds(0.5f);
        life.canTake = true;
        anim.SetBool("Parry", false);
        parrying = false;
        yield return new WaitForSeconds(1f);
        canParry = true;
    }
}
