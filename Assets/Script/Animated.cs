using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animated : MonoBehaviour
{
    public CharacterControl characterController;
    public Animator animator;
    void Update()
    {
        RunningAnim();
        JumpAnim();
    }

    private void RunningAnim()
    {
        animator.SetFloat("isRunning", characterController.absInput);
    }
    private void JumpAnim(){
        if(characterController.isJumping){
            animator.SetBool("isJumping", true);
        }
        if(!characterController.isJumping){
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", true);
        }
        if(characterController.isGrounded || characterController.enemyPhysic){
            animator.SetBool("isFalling", false);
        }
    }
}
