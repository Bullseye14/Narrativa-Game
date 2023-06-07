using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_v2 : MonoBehaviour
{
    // Start is called before the first frame update

    //INPUTS
    public float verticalInput;
    public float horizontalInput;

    public float movementSpeed;
    public float rotationSpeed;

    AH_v2 animatorHandler;

    CharacterController characterController;
    public Camera camera;
    Rigidbody rb;
    Vector3 movementForward;
    public bool attacking;
    BoxCollider boxCollider;
    SphereCollider sphereCollider;

    float DashCallTime;
    float speedMultiplyer;

    public float playerHealth;
    bool dead;
    public float attackDamage;

    public MissionsManager manager;

    void Start()
    {
        manager = GameObject.Find("Missions Handler").GetComponent<MissionsManager>();
        rb = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();
        animatorHandler = GetComponent<AH_v2>();
        boxCollider = GetComponent<BoxCollider>();
        sphereCollider = GetComponent<SphereCollider>();
        movementSpeed = 10f;
        rotationSpeed = 6f;
        attacking = false;
        attackDamage = 0;
        DashCallTime = 0;
        speedMultiplyer = 1f;

        playerHealth = 10000;
        dead = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (!manager.playerCanMove) verticalInput = 0;
    }


    private void FixedUpdate()
    {
        if (!manager.playerCanMove) verticalInput = 0;

        if (dead == false)
        {
            moveChC();

            if (playerHealth <= 0)
            {
                Die();
            }
        }else if (dead)
        {
           
        }        
    }


    public void setMovementValues(Vector2 moveVec)
    {
        if (manager.playerCanMove)
        {
            horizontalInput = moveVec.x;
            verticalInput = moveVec.y;
            animatorHandler.setAnimatorMovementValues(moveVec.magnitude);
        }

        else 
        { 
            horizontalInput = 0;
            verticalInput = 0;
            animatorHandler.setAnimatorMovementValues(0);
        }
       
        //if(verticalInput != 0)
        //{

        //}
        //else
        //{
        //    animatorHandler.setAnimatorMovementValues(0);
        //}
        
    }

    public void setAttackValues(string str,float damage)
    {
        animatorHandler.playOneTimeAnimation(str);
        attacking = true;
        attackDamage = damage;
        if(str == "attackorder")
        {
            attack0();
        }else if(str == "attackorder1")
        {
            attack1();
        }
        
    }

    public void DashNow()
    {
        if(Time.time - DashCallTime > 2)
        {
            DashCallTime = Time.time;
            speedMultiplyer = 6.0f;
        }    
    }

    private void moveChC()
    {
        if (manager.playerCanMove)
        {
            if (Time.time - DashCallTime > 0.2)//dash duration
                speedMultiplyer = 1.0f;//return to normal velocity
            float y = transform.position.y;
            movementForward = Vector3.Normalize(transform.position - camera.transform.position);


            if (verticalInput > 0.1)
            {

                Vector3 lookDir = camera.transform.forward;
                lookDir.y = transform.forward.y;

                transform.forward = lookDir;
                characterController.Move(transform.forward * movementSpeed * Time.deltaTime * speedMultiplyer);

            }
            else if (verticalInput < -0.1)
            {

                Vector3 lookDir = camera.transform.forward;
                lookDir.y = transform.forward.y;

                transform.forward = lookDir;

                characterController.Move(transform.forward * -movementSpeed * Time.deltaTime * speedMultiplyer);

            }
            if (horizontalInput > 0.1)
            {
                characterController.Move(camera.transform.right * movementSpeed * Time.deltaTime * speedMultiplyer);
            }
            else if (horizontalInput < -0.1)
            {
                characterController.Move(camera.transform.right * -movementSpeed * Time.deltaTime * speedMultiplyer);
            }
        }

        else
            characterController.Move(Vector3.zero);
        
    }
    void takeDamage(float damage)
    {
        playerHealth -= damage;
        Debug.Log("currPlayerHealth"+playerHealth.ToString());
    }

    void disableBoxCollider()
    {
        boxCollider.enabled = false;
        attackDamage = 0;
    }
    void disableSphereCollider()
    {
        sphereCollider.enabled = false;
        attackDamage = 0;
    }
    void attack0()
    {
        boxCollider.enabled = true;
        Invoke("disableBoxCollider", 0.3f);
    }

    void attack1()
    {
        sphereCollider.enabled = true;
        Invoke("disableSphereCollider", 0.3f);
    }

    void Die()
    {
        dead = true;
        animatorHandler.setAnimationBool("dead", true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "enemySlime")
        { 
            takeDamage(other.gameObject.GetComponent<EnemySlime>().attackDamage);
        }

        if (other.gameObject.tag == "enemyTurtle")
        {
            takeDamage(other.gameObject.GetComponent<EnemyTurtle>().attackDamage);
        }

    }

}
