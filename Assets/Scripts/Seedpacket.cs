using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seedpacket : MonoBehaviour
{
    // Config Params if any
    [SerializeField] GameObject plant;
    [SerializeField] Sprite image;
    [SerializeField] int activationVal = 100;
    [SerializeField] float inactiveTime;
    [SerializeField] Transform overlay, activeOverlay;

    // Global Variables
        // Private Variables
    SpriteRenderer seedlingCursor;
    StationPlants stationPlants;
    SeedBank seedBank;
    Vector3 initialPos = new Vector3();
    bool selected = false;
    float elapsedTime = 0;
    bool active = true;
    bool planted = false;
    bool seedBanked = false;

        // Public Variables
    public int index;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        seedlingCursor = FindObjectOfType<SeedlingCursor>().GetComponent<SpriteRenderer>();
        stationPlants = FindObjectOfType<StationPlants>();
        seedBank = FindObjectOfType<SeedBank>();
    }

    // Update is called once per frame
    void Update()
    {
        if (seedBank.gameStarted)
        {
            ActivationLoad();
            Activation();
            ActiveOverlayDisplay();
        }
    }

    // Private Methods
    private void OnMouseDown()
    {
        if (seedBank.gameStarted)
        {
            if (active)
            {
                if (!selected)
                {
                    if (stationPlants.CheckInstantiation())
                    {
                        if (!stationPlants.CompareIndices(index))
                        {
                            stationPlants.SetIndex(index);
                        }
                    }
                    StationPlantsConfiguration(image, true);
                }
                else
                {
                    StationPlantsConfiguration(null, false);
                }
            }
        } else
        {
            if (!seedBanked && seedBank.SeedAddAble())
            {
                seedBank.SeedPacketsAdd(gameObject);
                seedBanked = true;
            }
            else
            {
                seedBank.SeedPacketsRemove(gameObject);
                seedBanked = false;
                float distance = Vector3.Distance(transform.position, initialPos);
                transform.position = Vector3.MoveTowards(transform.position, initialPos, distance/0.4f);
            }
        }
    }

    private void StationPlantsConfiguration(Sprite imageConfig, bool selectedConfig)
    {
        if (selectedConfig)
        {
            stationPlants.SetConfigurations(GetComponent<Seedpacket>(), plant);
        }
        else
        {
            stationPlants.SetConfigurations(GetComponent<Seedpacket>(), null);
        }
        seedlingCursor.sprite = imageConfig;
        selected = selectedConfig;
    }

    private void ActivationLoad()
    {
        if (planted)
        {
            active = false;
            elapsedTime += Time.deltaTime;
            overlay.localScale = new Vector3(1, Mathf.Clamp((inactiveTime - elapsedTime) / inactiveTime, 0, 1), 1);
            if (elapsedTime > inactiveTime)
            {
                active = true;
                planted = false;
            }
        }
        else
        {
            elapsedTime = 0;
        }
    }

    private void Activation()
    {
        if (seedBank.GetSunCount() >= activationVal && !planted)
        {
            active = true;
        }
        else
        {
            active = false;
        }
    }

    private void ActiveOverlayDisplay()
    {
        if (active)
        {
            activeOverlay.localScale = new Vector3(1, 0, 1);
        } else
        {
            activeOverlay.localScale = new Vector3(1, 1, 1);
        }
    }

    // Public Methods
    public void SetSelectedWhenPlacingPlant()
    {
        selected = false;
    }

    public void SetStationedInactive()
    {
        planted = true;
    }

    public int GetSeedVal()
    {
        return -activationVal;
    }

    // Coroutines
    IEnumerator SeedpackInactiveLoad()
    {
        yield return new WaitForSeconds(inactiveTime);
        //overlay.localScale = new Vector3(1, 0, 1);
        active = true;
    }
}
