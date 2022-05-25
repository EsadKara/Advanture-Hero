using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class musicControl : MonoBehaviour
{
    AudioSource musicSource;
    Slider musicSlider;
    [SerializeField] TextMeshProUGUI musicTxt;
    
    
    void Start()
    {
        musicSource = GameObject.Find("GameManager").GetComponent<AudioSource>();
        musicSlider = GetComponent<Slider>();
        LoadAudio();
    }
    private void Update()
    {
        musicTxt.text = ((int)(musicSource.volume * 100)).ToString();
    }

    public void MusicVolume(float value)
    {
        musicSource.volume = value;
        musicSlider.value = value;
        SaveAudio();
    }

    void SaveAudio()
    {
        PlayerPrefs.SetFloat("Music", musicSource.volume);
    }
    void LoadAudio()
    {
        if (PlayerPrefs.HasKey("Music"))
        {
            musicSource.volume = PlayerPrefs.GetFloat("Music");
            musicSlider.value = PlayerPrefs.GetFloat("Music");
            musicTxt.text = ((int)(musicSource.volume * 100)).ToString();

        }
        else
        {
            PlayerPrefs.SetFloat("Music", 0.5f);
            musicSource.volume = PlayerPrefs.GetFloat("Music");
            musicSlider.value = PlayerPrefs.GetFloat("Music");
            musicTxt.text = ((int)(musicSource.volume * 100)).ToString();
        }
    }
}
