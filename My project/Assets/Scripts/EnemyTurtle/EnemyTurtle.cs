using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurtle : MonoBehaviour
{
    [SerializeField] private AudioClip attack2AudioClip;

    public MissionsManager missionManager;

    public GameObject player;
    public PC_v2 playerScript;
    public Transform playerPos;
    public Rigidbody rb;
    Collider attackCollider;
    public Animator animator;

    public float movementSpeed;
    public bool playerSeen;

    private float lastAttackTime;
    public float monsterHealth;
    public float attackDamage;
    public float attackDelay;
    // Start is called before the first frame update
    void Start()
    {
        missionManager = GameObject.Find("Missions Handler").GetComponent<MissionsManager>();
        rb = gameObject.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PC_v2>();
        attackCollider = GetComponent<BoxCollider>();
        animator = GetComponent<Animator>();
        playerPos = player.transform;
        movementSpeed = 8;
        lastAttackTime = 0;
        playerSeen = false;
        monsterHealth = 30;
        attackDamage = 20;
        attackDelay = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.playerHealth <= 0)
        {
            animator.Play("Victory");
        }
        else
        {
            if (monsterHealth > 0)
            {
                playerPos = player.transform;

                if (!playerSeen)
                {
                    float distance = Vector3.Distance(this.transform.position, playerPos.position);

                    if (distance < 20 && distance > 15)
                    {
                        animator.SetBool("suspect", true);
                        animator.SetBool("taunt", false);
                    }
                    if (distance < 15 || (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f && animator.GetCurrentAnimatorStateInfo(0).IsName("suspecting")))
                    {
                        playerSeen = true;
                        animator.SetBool("taunt", true);
                    }
                    else
                    {
                        animator.SetBool("taunt", false);
                    }
                    if (distance > 20)
                    {
                        animator.SetBool("suspect", false);
                    }
                }
                else if (playerSeen)
                {

                    float distance = Vector3.Distance(this.transform.position, playerPos.position);
                    this.transform.forward = Vector3.Normalize(playerPos.position - this.transform.position);
                    if (distance > 3)
                    {
                        animator.SetBool("inRange", false);
                        Vector3 newPos = Vector3.MoveTowards(transform.position, playerPos.position, movementSpeed * Time.deltaTime);
                        this.transform.position = newPos;
                    }
                    else if (distance < 3 && Time.time - lastAttackTime > attackDelay)
                    {
                        animator.SetBool("inRange", true);
                        lastAttackTime = Time.time;
                        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
                        {
                            attack();
                        }

                    }
                    
                }
            }
            else if (monsterHealth <= 0)
            {
                Die();
            }
        }

    }

    void disableAttackCollider()
    {
        attackCollider.enabled = false;
    }
    void attack()
    {
        float attack = Random.Range(0f, 10f);

        if (attack > 4)
        {
            animator.SetTrigger("attack2");
            audioManager.instance.PlaySound(attack2AudioClip);
            attackDamage = 15;
        }
        else
        {
            animator.SetTrigger("attack1");
            attackDamage = 10;
        }
        Debug.Log("slime collider activated");
        attackCollider.enabled = true;
        Invoke("disableAttackCollider", 0.3f);

    }

    void getHit(float damage)
    {
        monsterHealth -= damage;
        if (damage != 0)
        {
            animator.SetTrigger("getHit");
        }
    }

    void Die()
    {
        animator.SetBool("dead", true);

        missionManager.activeMission.GetComponent<MissionBehaviour>().KillEnemy(this.gameObject.name);

        rb.isKinematic = true;
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)//dead ended
        {
            //add dead particles
            Invoke("deleteEnemy", 2f);
        }
    }

    void deleteEnemy()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            getHit(other.gameObject.GetComponent<PC_v2>().attackDamage);

        }

    }
}
