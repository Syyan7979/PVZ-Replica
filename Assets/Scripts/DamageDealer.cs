using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    // Config Params
    [SerializeField] int damage = 1;
    [SerializeField] float type;
    [Range(3, 7)] [SerializeField] float projectileSpeed = 4.75f;

    // Global Variables
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * projectileSpeed);
        animator.SetFloat("Type", type);
    }

    // Private Methods

    // Public Methods
    public int GetDamage()
    {
        return damage;
    }

    public void DoubleDamage()
    {
        damage *= 2;
    }

    public void DestroyDamageDealer()
    {
        animator.SetBool("Hit", true);
        Destroy(gameObject, 0.125f);
    }

    public void StopProjectile()
    {
        projectileSpeed = 0;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Shredder")
        {
            Destroy(gameObject);
        }
    }

    public void SetType(float val)
    {
        type = val;
    }

    public float GetProjectileType()
    {
        return type;
    }

    // Coroutines
}
