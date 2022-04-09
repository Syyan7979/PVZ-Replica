using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingPlants : MonoBehaviour
{
    // Config Params
    [SerializeField] bool normalExplosion;

    // Global Variable
    Animator animator;
    Plants plants;
    StationPlants stationPlants;
    float elapsedTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        plants = GetComponent<Plants>();
        stationPlants = FindObjectOfType<StationPlants>();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (normalExplosion)
        {
            if (elapsedTime > 15)
            {
                animator.SetBool("ToExplode", true);
                GetComponent<Collider2D>().offset = new Vector2(0.03f, 0.09f);
                plants.enabled = false;
            }
        }
    }

    // Private Methods

    // Public Methods
    public void DestroyExplosivePlant()
    {
        stationPlants.WhenStationedPlantDestroyed(transform.position);
        Destroy(gameObject);
    }
}
