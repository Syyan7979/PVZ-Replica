using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationPlants : MonoBehaviour
{
    // Config Params
    [SerializeField] GameObject plant;

    // Dynamic Variables
    bool[,] positions = new bool[9, 5];
    SpriteRenderer seedlingCursor;
    Seedpacket seedPacket;
    SeedBank seedBank;
    int index;

    // Start is called before the first frame update
    void Start()
    {
        seedlingCursor = FindObjectOfType<SeedlingCursor>().GetComponent<SpriteRenderer>();
        seedBank = FindObjectOfType<SeedBank>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Private Methods
    private void OnMouseDown()
    {
        SpawnPlant(StationPlantPos());
    }

    Vector2 StationPlantPos()
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        worldPos.x = Mathf.RoundToInt(worldPos.x);
        worldPos.y = Mathf.RoundToInt(worldPos.y);

        return worldPos;
    }

    void SpawnPlant(Vector2 plantPos)
    {
        int xPos = (int)plantPos.x - 1;
        int yPos = (int)plantPos.y - 1;

        if (!positions[xPos, yPos] && plant)
        {
            GameObject newPlant = Instantiate(plant, plantPos, Quaternion.identity);
            positions[xPos, yPos] = true;
            seedlingCursor.sprite = null;
            seedPacket.SetSelectedWhenPlacingPlant();
            plant = null;
            seedPacket.SetStationedInactive();
            seedBank.SunCountChanges(seedPacket.GetSeedVal());
        }
    }

    // Public Methods
    public void SetConfigurations(Seedpacket seedling, GameObject plantInstance)
    {
        seedPacket = seedling;
        plant = plantInstance;
    }

    public void SetIndex(int selected)
    {
        index = selected;
    }

    public bool CompareIndices(int selected)
    {
        seedPacket.SetSelectedWhenPlacingPlant();
        return (index == selected);
    }

    public bool CheckInstantiation()
    {
        return (seedPacket != null);
    }

    public void WhenStationedPlantDestroyed(Vector2 pos)
    {
        int xPos = (int)pos.x - 1;
        int yPos = (int)pos.y - 1;
        positions[xPos, yPos] = false;
    }

    // Coroutines
}
