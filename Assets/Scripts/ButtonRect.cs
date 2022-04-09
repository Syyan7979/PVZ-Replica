using UnityEngine;
using UnityEngine.UI;

public class ButtonRect : MonoBehaviour
{
    public float AlphaThreshold = 0.1f;

    private void Start()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = AlphaThreshold;
    }
}
