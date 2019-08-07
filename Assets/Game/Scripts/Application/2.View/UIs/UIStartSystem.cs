using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStartSystem : MonoBehaviour {

    private Toggle toggle_BgMusic;
    private Toggle toggle_AudioEffect;

    private bool isInitSet = false;

    private void Awake()
    {
        toggle_BgMusic = transform.Find("Bg/PanelGameSetting/BgMusic/Toggle").GetComponent<Toggle>();
        toggle_AudioEffect = transform.Find("Bg/PanelGameSetting/AudioEffect/Toggle").GetComponent<Toggle>();


        toggle_BgMusic.isOn = PlayerPrefs.GetInt("BgMusic") == 1 ? true : false;
        toggle_AudioEffect.isOn = PlayerPrefs.GetInt("AudioEffect") == 1 ? true : false;

        toggle_BgMusic.onValueChanged.AddListener(isOn => OnSelectBgMusic(isOn));
        toggle_AudioEffect.onValueChanged.AddListener(isOn => OnSelectAudioEffect(isOn));
    }

    public void OnSelectBgMusic(bool isOn)
    {
        if(isOn)
        {
            Game.Instance.a_Sound.PlayBgMusic(Consts.MusicName);
            PlayerPrefs.SetInt("BgMusic", 1);
        }
        else
        {
            Game.Instance.a_Sound.StopBgMusic();
            PlayerPrefs.SetInt("BgMusic", 0);
        }
    }

    public void OnSelectAudioEffect(bool isOn)
    {
        if(isOn)
        {
            Game.Instance.a_Sound.PlayEffect();
            PlayerPrefs.SetInt("AudioEffect", 1);
        }
        else
        {
            Game.Instance.a_Sound.StopEffect();
            PlayerPrefs.SetInt("AudioEffect", 0);
        }
    }

    public void ShowSelf()
    {
        gameObject.SetActive(true);
    }

    public void HideSelf()
    {
        gameObject.SetActive(false);
    }
}
