using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SeedBank : MonoBehaviour
{
    // Config Params
    [SerializeField] TextMeshProUGUI textField;
    [SerializeField] GameObject seedChooser;
    [SerializeField] GameObject seedBankPos;
    [SerializeField] GameObject[] seeds;
    [SerializeField] int sunCount = 9999;

    // Global Variables
    // Private Variables

    List<Transform> seedPos = new List<Transform>();
    List<Transform> seedBankPackets = new List<Transform>();
    List<GameObject> seedPackets = new List<GameObject>();
    LockCameraSize mainCam;
    Animator animator;
        // Public Variables
    public bool gameStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = FindObjectOfType<LockCameraSize>();
        animator = GetComponent<Animator>();
        ListInitialization();
    }

    // Update is called once per frame
    void Update()
    {
        textField.text = $"{sunCount}";
        MoveSeedPackets();
        if (mainCam.gameStarted)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(3f, transform.position.y, transform.position.z), 0.25f);
        }
    }

    // Private Methods
    private void ListInitialization()
    {
        foreach (Transform child in seedChooser.transform)
        {
            seedPos.Add(child);
        }

        foreach (Transform child in seedBankPos.transform)
        {
            seedBankPackets.Add(child);
        }

        for (int i = 0; i < seeds.Length; i++)
        {
            GameObject seedPacket = Instantiate(seeds[i], seedPos[i].position, Quaternion.identity);
            seedPacket.transform.parent = seedChooser.transform;
        }
    }

    private void MoveSeedPackets()
    {
        for (int i = 0; i < seedPackets.Count; i++)
        {
            Vector3 currentPos = seedPackets[i].transform.position;
            seedPackets[i].transform.position = Vector3.MoveTowards(currentPos, seedBankPackets[i].position, 0.5f);
        }
    }

    // Public Methods
    public void SunCountChanges(int sunVal)
    {
        sunCount += sunVal;
    }

    public int GetSunCount()
    {
        return sunCount;
    }

    public void SeedPacketsAdd(GameObject seedpack)
    {
        seedPackets.Add(seedpack);
    }

    public void SeedPacketsRemove(GameObject seedpack)
    {
        seedPackets.Remove(seedpack);
    }

    public bool SeedAddAble()
    {
        return seedBankPackets.Count > seedPackets.Count;
    }

    public void GameStart()
    {
        mainCam.gameStarted = true;
        gameStarted = true;
    }

    public void LetsRock()
    {
        if (seedBankPackets.Count == seedPackets.Count)
        {
            for (int i = 0; i < seedPackets.Count; i++)
            {
                seedPackets[i].transform.parent = transform;
                seedPackets[i].GetComponent<Seedpacket>().index = i;
            }
            animator.SetTrigger("Game Start");
        }
    }
}
