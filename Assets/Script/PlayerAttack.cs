using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //Animation
    private Animator animator;
    [HideInInspector] public bool isAttacking;
    private float attackDuration = 0.5f;

    //DamageEnemy
    public bool triggerAttack;

    //Layer
    public LayerMask enemyLayer;
    public LayerMask groundLayer;

    //Check Enemy
    public Transform attackPoint;
    private float circleRadius = 0.47f;
    private List<GameObject> enemyList = new List<GameObject>();
    private GameObject[] enemies;
    [HideInInspector]public GameObject lastHitEnemy;
    private void Awake() {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies){
            enemyList.Add(enemy);
        }
    }
    private void Update()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        Attack();
    }

    private void Attack()
    {
        //Player can attack if on ground
        Collider2D isGrounded = Physics2D.OverlapCapsule(transform.position, GetComponent<CapsuleCollider2D>().size, 0, 0, groundLayer);
        if (InputSystem.inputSystem.Attack() && !isAttacking && isGrounded)
        {
            animator.SetTrigger("Attack");
            isAttacking = true;
            InputSystem.inputSystem.enabled = false;
            Invoke(nameof(AttackFinish), attackDuration);
        }
    }
    public void DamageEnemy()
    {
        Collider2D hitCollider = Physics2D.OverlapCircle(attackPoint.position, circleRadius, enemyLayer);
        if(hitCollider != null){
            lastHitEnemy = hitCollider.gameObject;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(attackPoint.position, circleRadius);
    }
    private void AttackFinish()
    {
        isAttacking = false;
        if (!GetComponent<CharacterCondition>().isHurt)
            InputSystem.inputSystem.enabled = true;
    }
}