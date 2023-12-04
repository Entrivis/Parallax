using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CharacterCondition : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    private Shake playerShake;
    GameObject[] enemies;
    [SerializeField] Transform healthBar;
    [HideInInspector] public bool isHurt;
    HealthSystem healthSystem;
    private void Start()
    {
        healthSystem = new HealthSystem(100);
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (playerShake == null)
        {
            playerShake = GetComponent<Shake>();
        }
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
                if (enemy.GetComponent<Enemy>().knockBackingPlayer == true)
                {
                    KnockBackPlayer();
                    enemy.GetComponent<Enemy>().knockBackingPlayer = false;
                }

        }
    }

    private void KnockBackPlayer()
    {
        if (gameObject.name == "Char")
        {
            StartCoroutine(KnockingBackPlayer(rb));
        }
    }

    private IEnumerator KnockingBackPlayer(Rigidbody2D Player)
    {
        InputSystem.inputSystem.enabled = false; //Any input will not avaible to use 
        isHurt = true;//Make sure cannot input for certain time when attacked by enemy
        gameObject.layer = LayerMask.NameToLayer("Invisible");
        animator.SetBool("Hurt", true);
        playerShake.ShakeMe();
        healthSystem.Damage(25);
        Vector3 healthBar = new Vector3(healthSystem.GetHealthBar(), 1, 1);
        this.healthBar.localScale = healthBar;
        if (healthSystem.GetHealthBar() > 0)
        {
            yield return new WaitForSeconds(1f);

            animator.SetBool("Hurt", false);
            InputSystem.inputSystem.enabled = true; //Any input will be avaiable to use
            for (int i = 0; i < 4; i++)
            {
                if (i % 2 == 0)
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(123 / 255, 123 / 255, 123 / 255);
                else
                    gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                yield return new WaitForSeconds(0.2f);
            }
            gameObject.layer = LayerMask.NameToLayer("Player");
            isHurt = false;
        }
        else
        {
            GameManager.instance.CallDie();
            GameObject.Find("HealthSystem").SetActive(false);
        }
    }

}
