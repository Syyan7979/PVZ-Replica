using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultingZombie : MonoBehaviour
{

    // Global Variables
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Private Methods

    // Public Methods
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Plants"))
        {
            animator.SetTrigger("Vault");
        }
    }
}
