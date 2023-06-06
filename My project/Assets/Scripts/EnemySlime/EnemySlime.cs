using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlime : MonoBehaviour
{
    public GameObject player;
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
        rb = gameObject.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
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
                else if (Time.time - lastAttackTime > 0.3f && Time.time - lastAttackTime < 1.0f)
                {
                    attackCollider.enabled = true;
                }
                else if (Time.time - lastAttackTime > 1.0f)
                {
                    attackCollider.enabled = false;
                }
            }
        }
        else if (monsterHealth <= 0)
        {
            Die();
        }
    }


    void attack()
    {
        float attack = Random.Range(0f, 10f);

        if (attack > 4)
        {
            animator.SetTrigger("attack2");
        }
        else
        {
            animator.SetTrigger("attack1");
        }

        //attackCollider.enabled = true;

    }

    void getHit(float damage)
    {
        monsterHealth -= damage;
        Debug.Log("damage get: " + damage.ToString());
    }

    void Die()
    {
        animator.SetBool("dead", true);

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
