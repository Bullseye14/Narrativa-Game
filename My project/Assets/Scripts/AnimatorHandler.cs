using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorHandler : MonoBehaviour
{
    Animator animator;

    int movement;
    int vertical;

    public int verticalP;
    public int horizontalP;

    private void Awake()
    {
        animator    = GetComponent<Animator>();

        movement  = Animator.StringToHash("horizontal");
       
    }
    public void updateAnimatorValues(float horizontalMovement,float verticalMovement) 
    {
        float totalMovement = new Vector2(horizontalMovement, verticalMovement).magnitude;



        if (totalMovement > 0 && verticalMovement > 0.1 && horizontalMovement == 0)
        {
            animator.SetFloat("movementAmmount", verticalMovement);
        }
        else if (totalMovement > 0 && verticalMovement < 0.1 && horizontalMovement == 0)
        {
            animator.SetFloat("movementAmmount", verticalMovement);
        }else if(totalMovement == 0)
        {
            animator.SetFloat("movementAmmount", totalMovement);
        }

        
        
    }
}
