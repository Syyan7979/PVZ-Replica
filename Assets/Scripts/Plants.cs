using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plants : MonoBehaviour
{
    // Config Params
    [SerializeField] int plantHealth = 6;

    // Global Variables
    Animator animator;
    StationPlants stationPlants;
    bool destroyed = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        stationPlants = FindObjectOfType<StationPlants>();
    }

    // Update is called once per frame
    void Update()
    {
        PlantDestroyCheck();
        SetAnimationValues();
    }

    // Private Methods
    private bool HasParameter(string param)
    {
        foreach(AnimatorControllerParameter parameter in animator.parameters)
        {
            if (param == parameter.name)
            {
                return true;
            }
        }
        return false;
    }

    private void PlantDestroyCheck()
    {
        if (plantHealth <= 0)
        {
            destroyed = true;
            stationPlants.WhenStationedPlantDestroyed(transform.position);
            Destroy(gameObject, 0.1f);
        }
    }

    private void SetAnimationValues()
    {
        if (HasParameter("Hit Points"))
        {
            animator.SetInteger("Hit Points", plantHealth);
        }
    }

    // Public Methods
    public void PlantBitten()
    {
        plantHealth--;
    }

    public bool Destroyed()
    {
        return destroyed;
    }
    // Coroutines if any
}
