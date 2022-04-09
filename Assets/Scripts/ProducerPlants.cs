using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProducerPlants : MonoBehaviour
{
    // Config Params
    [Header("Sunflower")]
    [SerializeField] GameObject sun;
    [SerializeField] float waitTime = 1.667f;

    // Global Variable
    bool firstSun = true;
    float elapsedTime = 0;
    Animator animator;
    Animator spawnedSunAnimator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        SunflowerProduceSun();
        if (spawnedSunAnimator)
        {
            StartCoroutine(StartDestroyTimer());
        }
    }
    // Private Methods
    private void SunflowerProduceSun()
    {
        if (firstSun)
        {
            if (elapsedTime >= Random.Range(2.333f, 6.333f))
            {
                StartCoroutine(NegateAfterWait());
                firstSun = false;
            }
            elapsedTime += Time.deltaTime;
        }
        else
        {
            if (elapsedTime >= 22.333f)
            {
                StartCoroutine(NegateAfterWait());
            }
            elapsedTime += Time.deltaTime;
        }
    }

    // Public Methods
    public void SunFlowerSpawnSun()
    {
        GameObject spawnedSun = Instantiate(sun, transform.position + new Vector3(0.699f, -0.177f, 0), Quaternion.identity);
        spawnedSun.GetComponent<Sun>().StopFalling();
        spawnedSunAnimator = spawnedSun.GetComponent<Animator>();
    }

    // Coroutines
    IEnumerator NegateAfterWait()
    {
        animator.SetBool("Produce", true);
        elapsedTime = 0;
        yield return new WaitForSeconds(waitTime);
        animator.SetBool("Produce", false);
    }

    IEnumerator StartDestroyTimer()
    {
        yield return new WaitForSeconds(7.5f);
        if (spawnedSunAnimator)
        {
            spawnedSunAnimator.SetTrigger("Disappear");
        }
    }
}
