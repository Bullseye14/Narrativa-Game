using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AH_v2 : MonoBehaviour
{

    Animator animator;
    AnimatorClipInfo[] currentClipInfo;
    public bool attacking;

    public string currentAnimation;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        
    }
    

    public void setAnimatorMovementValues(float movement)
    {
        animator.SetFloat("movementAmmount", movement);
    }

    public void playOneTimeAnimation(string str)
    {
        animator.SetTrigger(str);
        
    }
    
}
