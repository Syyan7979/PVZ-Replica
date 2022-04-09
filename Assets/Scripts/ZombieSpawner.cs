using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    // Config Params
    [Header("Spawn Zombie Config")]
    [SerializeField] List<GameObject> Enemies;
    //[SerializeField] int[] waveCount;
    [SerializeField] int spawnNumber;

    [Header("Spawn Sun Config")]
    [SerializeField] GameObject sun;

    // Dynamic Global Variables
    System.Random rnd;
    int enemiesListSize;
    int[] offsets = { -2, -1, 0, 1, 2 };
    Vector2 initialPos = new Vector2(10.25f, 3.4f);
    bool spawn = true;
    float elapsedTime = 0;
    GameObject[] transformPositions;
    SeedBank seedBank;

    // Start is called before the first frame update
    void Start()
    {
        rnd = new System.Random();
        enemiesListSize = Enemies.Count;
        transformPositions = GameObject.FindGameObjectsWithTag("Spawn Positions");
        seedBank = FindObjectOfType<SeedBank>();
    }

    // Update is called once per frame
    void Update()
    {
        if (seedBank.gameStarted)
        {
            if (Time.time > 5 && spawn)
            {
                spawn = false;
                StartCoroutine(SpawnZombiesCoroutine());
            }
            SpawnSuns();
        }
    }

    // Private Methods

    void SpawnSuns()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= 8f)
        {
            var spawnedSun = Instantiate(sun, new Vector2(Random.Range(1f, 9f), 5.5f), Quaternion.identity);
            elapsedTime = 0;
        }
    }

    // Public Methods

    // Coroutines
    IEnumerator SpawnZombiesCoroutine()
    {
        for (int i = 0; i < spawnNumber; i++)
        {
            yield return new WaitForSeconds((float)rnd.NextDouble() * 20);
            int offset = offsets[rnd.Next() % 5];
            Vector2 newPos = new Vector2(initialPos.x, initialPos.y + offset);
            var enemy = Instantiate(Enemies[rnd.Next() % enemiesListSize], newPos, Quaternion.identity);
            enemy.GetComponent<SpriteRenderer>().sortingOrder -= offset;
            enemy.transform.parent = transformPositions[offset + 2].transform;
        }
    }
}
