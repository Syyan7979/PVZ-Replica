using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombies : MonoBehaviour
{
    [Range(0, 5)] [SerializeField] float walkSpeed = 0.2f;
    [SerializeField] int hitPoints = 10;
    [SerializeField] GameObject vault;
    // Global Variables
    // Private
    float walkSpeedCopy;
    Animator animator;
    Plants plants;
    SpriteRenderer spriteRenderer;
    Component[] spriteRenderers;
    bool deathyByExplosion = false;
    bool cherryBomb = false;
    bool plantSet = false;
    bool chilled = false;

        // Public

    // Start is called before the first frame update
    void Start()
    {
        walkSpeedCopy = walkSpeed;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderers = GetComponentsInChildren(typeof(SpriteRenderer), true);
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetInteger("Hit Points", hitPoints);
        ZombieMovement();
        DeathTriggerCheck();
        WalkAfterEat();
        ZombieSlowed();
    }

    // Private Methods
    void ZombieMovement()
    {
        transform.Translate(Vector2.left * Time.deltaTime * walkSpeed);
    }

    void DeathTriggerCheck()
    {
        if (hitPoints <= 0 && !deathyByExplosion)
        {
            animator.SetBool("Death", true);
        }
        else if (hitPoints <= 0 && deathyByExplosion)
        {
            if (cherryBomb)
            {
                foreach (SpriteRenderer spriteRendererCopnonent in spriteRenderers)
                {
                    spriteRendererCopnonent.color = Color.black;
                }
                animator.speed = 0;
                walkSpeed = 0;
                Destroy(gameObject, 1.5f);
            }
            else
            {
                Destroy(gameObject, 0.05f);
            }
            
        }
    }

    private void WalkAfterEat()
    {
        if (plantSet && plants.Destroyed())
        {
            animator.SetFloat("WorE", 0);
        }
    }

    private void ZombieSlowed()
    {
        if (chilled)
        {
            spriteRenderer.color = new Color32(55, 172, 221, 255);
            walkSpeed = walkSpeedCopy / 2;
        }
    }

    // Public Methods

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Projectile" && !deathyByExplosion)
        {
            DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
            damageDealer.StopProjectile();
            hitPoints -= damageDealer.GetDamage();
            if (damageDealer.GetProjectileType() == -1)
            {
                chilled = true;
            } else if (damageDealer.GetProjectileType() == 0)
            {
                StartCoroutine(HitColorChange());
            }
            damageDealer.DestroyDamageDealer();
        }
        
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Explosives")
        {
            cherryBomb = true;
            hitPoints -= 90;
            if (hitPoints <= 0)
            {
                deathyByExplosion = true;
            }
        }
        else if (collision.tag == "Plants")
        {
            plants = collision.GetComponent<Plants>();
            if (plants.enabled == false)
            {
                var bombAnimator = collision.GetComponent<Animator>();
                bombAnimator.SetBool("Explode", true);
                hitPoints -= 90;
                if (hitPoints <= 0)
                {
                    deathyByExplosion = true;
                }
            }
            if (!vault || !vault.activeSelf)
            {
                walkSpeed = 0;
                animator.SetFloat("WorE", 1);
            }
            plantSet = true;
            transform.position = new Vector3(collision.transform.position.x + 0.5f, transform.position.y, transform.position.z);
        }
    }

    public void PauseDeathAndDestroy()
    {
        animator.speed = 0;
        Destroy(gameObject, 0.5f);
    }

    public void DamagePlant()
    {
        plants.PlantBitten();
    }

    public void SetMovementSpeed(float moveSpeed)
    {
        walkSpeed = moveSpeed;
    }

    // Coroutines
    IEnumerator HitColorChange()
    {
        spriteRenderer.color = new Color(235f / 255f, 255f / 255f, 40f / 255f);
        yield return new WaitForSeconds(0.125f);
        spriteRenderer.color = Color.white;
    }
}
