using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockCameraSize : MonoBehaviour
{
    // Global Variable
        // Private Variable

        // Public Variable
        public bool gameStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = 10.03137f / 6.25f;

        if (screenRatio >= targetRatio)
        {
            Camera.main.orthographicSize = 6.25f / 2f;
        }
        else
        {
            float difference = targetRatio / screenRatio;
            Camera.main.orthographicSize = 6.25f / 2f * difference;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted)
        {
            StartCoroutine(PanCamera());
        }
    }

    // Private Method
    IEnumerator PanCamera()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(5, 3.28f, -10), 0.25f);
        yield return new WaitForSeconds(2);
        gameStarted = false;

    }
}
