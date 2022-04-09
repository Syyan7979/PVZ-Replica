using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterPlants : MonoBehaviour
{
    // Config Params
    [SerializeField] GameObject bullet;
    [SerializeField] Transform shootPos;

    // Global Variables
    GameObject[] transformPositions;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        transformPositions = GameObject.FindGameObjectsWithTag("Spawn Positions");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ShootHandler();
    }

    // Private Methods
    private void ShootHandler()
    {
        if (transformPositions[(int)transform.position.y-1].transform.childCount > 0)
        {
            animator.SetBool("Shoot", true);
        }
        else
        {
            animator.SetBool("Shoot", false);
        }
    }

    // Public Methods
    public void shoot(float type)
    {
        GameObject projectile = Instantiate(bullet, shootPos.position, transform.rotation);
        projectile.GetComponent<DamageDealer>().SetType(type);
    }

    // Coroutines
}
