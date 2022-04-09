using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    // Config Params
    [SerializeField] Canvas settings;

    // Global Variable
    Button[] buttons1;
    public void Start()
    {
        buttons1 = FindObjectsOfType<Button>();
    }
    public void Options()
    {
        settings.gameObject.SetActive(true);
        foreach(Button buttons in buttons1)
        {
            buttons.enabled = false;
        }
    }
}
