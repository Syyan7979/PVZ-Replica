using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedlingCursor : MonoBehaviour
{
    // Global Variables
    // Private Variables
    Camera mainCamera;

        // Public Variables

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main; // for efficiency
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5));
    }
}
