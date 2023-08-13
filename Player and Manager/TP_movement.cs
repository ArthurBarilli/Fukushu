using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class TP_movement : MonoBehaviour
{
    [Header("Components")]
    public CharacterController controller;
    public Transform cam;
    public Quaternion Aim;
    public RaycastHit hit;
    public GameManager m;
    public PlayerLife pLife;
    public EnemiesInRange parriableEnemies;
    public List<EnemyAI> enemies;

    public Vector3 DodgeDir;
    //bools
    [Header("Bools")]
    public bool isDodging;
    public bool canDodge=true;
    public bool Attack;
    public bool isAttacking;
    public bool isParrying;

    //directions to loock

    public Vector3 LookDir;

    [Header("atributes")]
    public float speed = 10f;
    public float gravity = 1.5f;
    public float turnSmoothTime = 0.1f;
    public float MouseDir;

    public int specialDodgeSpeed;
    public int normalDodgeSpeed;

    public float dodgeCD;

    public float rotationSpeed;

    [Header("Skill Tree")]
    public bool dashUpgrade;
    public bool superDash;

    public float gravity1 = -9.81f;
    Vector3 velocity;
    AudioSource run;
    public AudioClip runClip;

    [Header("AnimationRigging")]
    public GameObject target;
    public Rig rigging;

    [Header("VFX")]
    public ParticleSystem[] dash;
    public ParticleSystem[] superDashFx;
    public GameObject windImpact;
    public Transform windImpactPlace;
    public TrailRenderer dashTrail;
    public GameObject parryBlink;
    public Transform parryBlinkPosition;

    Animator animator;

    private void Start()
    {        
        enemies = parriableEnemies.enemies;
        pLife = GetComponent<PlayerLife>();
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        run = GetComponent<AudioSource>();
        m = GameManager.Instance;
        if (m.isDashUpgrade)
        {
            dashUpgrade = true;
        }
        if(m.isSuperDash)
        {
            superDash = true;
        }
    }

    void Update()
    {
        AnimatorStateInfo baseStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo troncoStateInfo = animator.GetCurrentAnimatorStateInfo(1);
        if (troncoStateInfo.IsName("AttackNew") || troncoStateInfo.IsName("AtaqueCharge"))
        {
            isAttacking = true;
        }
        else 
        {
            isAttacking = false;
        }
        Vector3 dir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Camera.main.transform.position)).normalized * 100 ;
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        velocity.y += gravity1 * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


        //olhar para a direção dos passos
        if (direction.magnitude >= 0.1f && pLife.dead == false)
        {
            Vector3 moveDir = direction;
            controller.Move(moveDir * speed * Time.deltaTime);
            Quaternion toRotation = Quaternion.LookRotation(moveDir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            if (isAttacking == false || isParrying == false)
            {
                rigging.weight = 0;
            }
            
            
            
            
        //Animacao do Personagem
            animator.SetFloat("isRunning", direction.magnitude);
        }else{
            animator.SetFloat("isRunning", 0);
        }

        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        //Dodge Direction
        if (direction != new Vector3(0,0,0))
        {
            DodgeDir = direction;
        }
        // olhar para diração do ataque/parry
        speed = 10;
        if (groundPlane.Raycast(cameraRay, out rayLength) && (isAttacking == true) || (isParrying == true))
        {
            transform.LookAt(new Vector3(LookDir.x, transform.position.y, LookDir.z));
        }

        if (Input.GetKeyDown(KeyCode.Space) && canDodge == true && superDash == false)
        {
            StartCoroutine(DodgeState());
            animator.SetTrigger("Roll");          
        }
        else if (Input.GetKeyDown(KeyCode.Space) && canDodge == true)
        {
            StartCoroutine(DodgeState());
            animator.SetTrigger("Dash");          
        }
        if (isDodging == true && dashUpgrade == false)
        {
            controller.Move(DodgeDir * normalDodgeSpeed * Time.deltaTime);
        }
        else if (isDodging == true && dashUpgrade == true)
        {
            controller.Move(DodgeDir * specialDodgeSpeed * Time.deltaTime);
        }
        
        if(isDodging && superDash)
        {
            foreach(EnemyAI enemy in enemies)
            {
                if(enemy.GetComponent<EnemyLife>().enemyType == "Shield" || enemy.GetComponent<EnemyLife>().enemyType == "Sword")
                {
                    enemy.isParried = true;
                    Instantiate(parryBlink, enemy.transform.position, parryBlinkPosition.rotation);
                }
            }
        }
    }


    public IEnumerator DodgeState()
    {
        if (superDash)
        {
        canDodge = false;
        isDodging = true;  
        pLife.canTake = false;
        foreach (ParticleSystem fx in superDashFx)
        {
            fx.Play();
        }
        dashTrail.emitting = true;
        Instantiate(windImpact, windImpactPlace.position, windImpactPlace.rotation);
        yield return new WaitForSeconds(0.15f);
        isDodging = false;
        dashTrail.emitting = false;
        yield return new WaitForSeconds(0.1f);
        pLife.canTake = true;
        yield return new WaitForSeconds(dodgeCD);
        canDodge = true;
        } 
        else
        {
        canDodge = false;
        isDodging = true;  
        pLife.canTake = false;
        foreach (ParticleSystem fx in dash)
        {
            fx.Play();
        }

        Instantiate(windImpact, windImpactPlace.position, windImpactPlace.rotation);
        yield return new WaitForSeconds(0.15f);
        isDodging = false;
        yield return new WaitForSeconds(0.1f);
        pLife.canTake = true;
        yield return new WaitForSeconds(dodgeCD);
        canDodge = true;
        }
        
    }
}
