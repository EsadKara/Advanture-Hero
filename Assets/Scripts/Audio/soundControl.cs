using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class soundControl : MonoBehaviour
{
    AudioSource soundSource;
    Slider soundSlider;
    [SerializeField] TextMeshProUGUI soundTxt;

    void Start()
    {
        soundSource = GameObject.Find("Player").GetComponent<AudioSource>();
        soundSlider = GetComponent<Slider>();
        LoadAudio();
    }
    private void Update()
    {
        soundTxt.text = ((int)(soundSource.volume * 100)).ToString();
    }

    public void SoundVolume(float value)
    {
        soundSource.volume = value;
        soundSlider.value = value;
        SaveAudio();
    }

    void SaveAudio()
    {
        PlayerPrefs.SetFloat("Sound", soundSource.volume);
    }
    void LoadAudio()
    {
        if (PlayerPrefs.HasKey("Sound"))
        {
            soundSource.volume = PlayerPrefs.GetFloat("Sound");
            soundSlider.value = PlayerPrefs.GetFloat("Sound");
            soundTxt.text = ((int)(soundSource.volume * 100)).ToString();
        }
        else
        {
            PlayerPrefs.SetFloat("Music", 0.5f);
            soundSource.volume = PlayerPrefs.GetFloat("Sound");
            soundSlider.value = PlayerPrefs.GetFloat("Sound");
            soundTxt.text = ((int)(soundSource.volume * 100)).ToString();
        }
    }
}
