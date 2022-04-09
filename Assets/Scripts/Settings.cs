using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    // Config Params
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    // Global Parameters
    Button[] buttons;

    // Start is called before the first frame update
    void Start()
    {
        buttons = FindObjectsOfType<Button>();
    }

    public void MainMenuSelected()
    {
        foreach(Button button in buttons)
        {
            button.enabled = true;
        }
        gameObject.SetActive(false);
    }
}
