using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : MonoBehaviour
{
    private CharacterMovement movement;

    public bool isAttacking;
    public bool isAttackingEnemy;
    public bool inRangeOfEnemy;
    private bool damageOnce = true;
    private bool animOnce = true;
    private float attackTimer = 0.5f;
    private float currentAttackTime = 0;
    private void Start()
    {
        movement = GetComponentInParent<CharacterMovement>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Enemy")
        {
            if(isAttacking)
            {
                if(damageOnce)
                {
//                    other.GetComponentInChildren<AI_Slime_Split>().takeDamage(1);
                    damageOnce = false;
                }
            }
        }
    }

    private void Update()
    {
        if (movement.interactInput)
        {
            if(movement.MovingOnGround)
            {
                if(!isAttacking)
                {
                    isAttacking = true;
                    if(inRangeOfEnemy)
                    {
                        isAttackingEnemy = true;
                    }
                }
            }
        }
        if(isAttacking)
        {

            if(currentAttackTime >= attackTimer)
            {
                isAttacking = false;
                damageOnce = true;
                animOnce = true;
            }
            else
            {
                if(animOnce)
                {
                    Debug.Log("ATTACK");
                    transform.parent.GetComponentInChildren<Animator>().SetTrigger("Attacking");
                    animOnce = false;
                }


                currentAttackTime = currentAttackTime + Time.deltaTime;
            }
        }
        else
        {
            currentAttackTime = 0;
        }
    }
}
