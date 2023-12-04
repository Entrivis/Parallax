using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Attack player variable
    private float enemySpeed = 2f;
    public LayerMask playerLayer;
     GameObject player;
    [SerializeField] float distanceToTrigger; //Adjust distance for player to trigger enemy
    [SerializeField] float distanceToAttackPlayer; //Adjust distance for attack player

    //Animator
    [HideInInspector] public Animator animator;

    //SpriteRenderer
    private SpriteRenderer spriteRenderer;

    //Physics
    private Rigidbody2D rb;

    //Direction
    private int directionX = 1;

    //KnockBack
    public bool knockBackingPlayer;

    //Damage
    PlayerAttack playerAttack;
    HealthSystem healthSystem;
    bool playerNearby;
    private bool isHurt;

    private void Start()
    {
        healthSystem = new HealthSystem(100);
        player = GameObject.FindWithTag("Player");
        playerAttack = player.GetComponent<PlayerAttack>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        //If detect player Running toward player and attack
        PlayerNearby();
        //Enemy Recieve Damage from player
        RecieveDamage();
    }
    private void PlayerNearby()
    {
        float distanceFromPlayer = player.transform.position.x - transform.position.x;
        float setEnemyMoveTowardPlayer = 0;
        //Attack
        if (Mathf.Abs(distanceFromPlayer) <= distanceToAttackPlayer && !isHurt)
        {
            Flip(distanceFromPlayer, distanceToAttackPlayer);
            if (player.layer != LayerMask.NameToLayer("Invisible") && player.GetComponent<CharacterControl>().isGrounded)
            {
                animator.SetBool("Attack", true);
            }
            else
            {
                animator.SetBool("Attack", false);
            }
        }
        //Run toward Player
        else if (Mathf.Abs(distanceFromPlayer) <= distanceToTrigger && !isHurt)
        {
            Flip(distanceFromPlayer, distanceToAttackPlayer);
            animator.SetBool("Running", true);
            setEnemyMoveTowardPlayer = Mathf.MoveTowards(transform.position.x, player.transform.position.x, enemySpeed * Time.deltaTime);
            Vector2 move = new Vector2(setEnemyMoveTowardPlayer, transform.position.y);
            transform.position = move;
            if (Mathf.Abs(Mathf.Round(distanceFromPlayer)) <= 2.1)
            {
                animator.SetBool("Running", false);
            }
        }
        else
        {
            animator.SetBool("Running", false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, distanceToTrigger);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanceToAttackPlayer);
    }
    private void AttackPlayer()
    {
        knockBackingPlayer = true;
    }
    private IEnumerator EnemyHurt()
    {
        isHurt = true;
        animator.SetBool("Hurt", true);
        yield return new WaitForSeconds(1f);
        isHurt = false;
        animator.SetBool("Hurt", false);
    }
    private void RecieveDamage()
    {
        if (playerAttack.lastHitEnemy == gameObject) // Check if this enemy is the one hit by the player
        {
            healthSystem.Damage(20);
            playerAttack.lastHitEnemy = null; // Reset the reference
            StartCoroutine(nameof(EnemyHurt));
        }
        if (healthSystem.currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Flip(float distanceFromPlayer,float distanceToAttackPlayer){
        if(distanceFromPlayer < distanceToAttackPlayer)
            print("1");
        else{
            print("2");
        }
    }
}