using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagementScript : MonoBehaviour
{
    public void LoadStartScreen()
    {
        SceneManager.LoadScene("Start Scene");
    }
}
