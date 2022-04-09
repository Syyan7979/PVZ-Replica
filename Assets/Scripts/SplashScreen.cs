using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] GameObject loading;

    // Dynamic Variables
    AudioSource audioSource;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = loading.GetComponent<Animator>();
        PlayThemeMusic();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= waitTime)
        {
            UpdateButtonText();
            button.GetComponent<Button>().enabled = true;
            animator.speed = 0;
        }
    }

    private void PlayThemeMusic()
    {
        audioSource.Play();
        audioSource.volume = musicVolume;
    }

    void UpdateButtonText()
    {
        buttonText.text = "Click Here to Start";
    }
}
