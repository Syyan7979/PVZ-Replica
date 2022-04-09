using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    // Config Params
    [SerializeField] int sunVal = 25;
    [SerializeField] Transform sunPosition;
    [Range(0, 1)] [SerializeField] float moveSpeed;
    [SerializeField] float downMovementSpeed = 4.5f;

    // Global Variables
    bool move;
    bool add = true;
    float distance;
    bool fade = true;
    Animator animator;
    SeedBank seedBank;

    // Start
    private void Start()
    {
        animator = GetComponent<Animator>();
        seedBank = FindObjectOfType<SeedBank>();
    }

    // Update
    private void Update()
    {
        MoveSunToPos();
        IncreaseSunVal();
        MoveSunDown();
    }

    // Private Methods
    private void OnMouseDown()
    {
        move = true;
        distance = Vector2.Distance(sunPosition.position, transform.position);
        animator.SetTrigger("clicked");
        GetComponent<Collider2D>().enabled = false;
    }

    private void MoveSunToPos()
    {
        if (move)
        {
            transform.position = Vector3.MoveTowards(transform.position, sunPosition.position, (distance/moveSpeed) * Time.deltaTime);
        }
    }

    private void MoveSunDown()
    {
        if (!move)
        {
            transform.Translate(Vector2.down * downMovementSpeed * Time.deltaTime);
            if (transform.position.y <= 0.75f)
            {
                downMovementSpeed = 0;
                if (fade)
                {
                    animator.SetTrigger("Disappear");
                    fade = false;
                }    
            }
        }
    }

    private void IncreaseSunVal()
    {
        if (transform.position == sunPosition.position)
        {
            if (add)
            {
                seedBank.SunCountChanges(sunVal);
                add = false;
            }

            Destroy(gameObject, 0.05f);
        }
    }

    // Public Methods
    public void StopFalling()
    {
        downMovementSpeed = 0;
    }

    public void DestroySun()
    {
        Destroy(gameObject);
    }

    // Coroutines
}
