using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SplashScreen : MonoBehaviour
{
    // Configuration Parameters
    [Range(0, 1)] [SerializeField] float musicVolume = 0.5f;
    [SerializeField] float waitTime = 12f;
    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] Button button;

    // Dynamic Variables
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayThemeMusic();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(LoadingFreeze());
    }

    // Private Methods
    private void PlayThemeMusic()
    {
        audioSource.Play();
        audioSource.volume = musicVolume;
    }

    void UpdateButtonText()
    {
        buttonText.text = "Click Here to Start";
    }

    // Public Mehthods

    // Coroutines
    IEnumerator LoadingFreeze()
    {
        yield return new WaitForSeconds(waitTime);
        UpdateButtonText();
        button.GetComponent<Button>().enabled = true;
    }
}
