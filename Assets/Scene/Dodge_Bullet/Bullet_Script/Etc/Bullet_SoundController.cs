using UnityEngine;
using UnityEngine.UI;

public class Bullet_SoundController : MonoBehaviour // 사운드에 대한 전반적인 데이터를 관리하기 위한 스크립트
{
    public static AudioSource BGM_Player;
    public static AudioSource SFX_Player;
    public static Bullet_SoundController instance;

    public AudioClip[] BGM_AudioClips;
    public AudioClip[] SFX_AudioClips;

    public Slider BGM_Audio_Img;
    public Slider SFX_Audio_Img;

    void Awake()    // 시작 사운드를 40% 으로 낮추기 위한 함수
    {
        instance = this;
        BGM_Player = GameObject.Find("BGM_Player").GetComponent<AudioSource>();
        SFX_Player = GameObject.Find("SFX_Player").GetComponent<AudioSource>();
        ChangeBgmSound(-1);
        ChangeSfxSound(-1);
    }

    public void BgmSound(string type)   // 배경음을 다양하게 보관하기 위한 함수
    {
        int index = 0;
        switch (type)
        {
            case "Main_BGM":
                index = 0;
                break;
        }
        BGM_Player.clip = BGM_AudioClips[index];
        BGM_Player.Play();
    }

    public void SfxSound(string type)   // 효과음을 다양하게 보관하기 위한 함수
    {
        int index = 0;
        switch (type)
        {
            case "Select":
                index = 0;
                break;
            case "GetItem":
                index = 1;
                break;
            case "Hit":
                index = 2;
                break;
        }
        SFX_Player.clip = SFX_AudioClips[index];
        SFX_Player.Play();
    }


    internal void ChangeBgmSound(int i = 0) // 배경음의 증가 및 감소를 제어하는 함수
    {
        if (i == 0 && BGM_Player.volume != 0f)
        {
            BGM_Player.volume -= 0.2f;
            BGM_Audio_Img.value = BGM_Player.volume;
        }
        else if (i == 1 && BGM_Player.volume != 1f)
        {
            BGM_Player.volume += 0.2f;
            BGM_Audio_Img.value = BGM_Player.volume;
        }
        else if (i == -1)
        {
            BGM_Player.volume = SoundData.control.BGM_Data;
            BGM_Audio_Img.value = BGM_Player.volume;
        }
        SoundData.control.ChangeSound(BGM_Player.volume, SFX_Player.volume);

    }

    internal void ChangeSfxSound(int i = 0) // 효과음의 증가 및 감소를 제어하는 함수
    {
        if (i == 0 && SFX_Player.volume != 0f)
        {
            SFX_Player.volume -= 0.2f;
            SFX_Audio_Img.value =  SFX_Player.volume;
        }
        else if (i == 1 && SFX_Player.volume != 1f)
        {
            SFX_Player.volume += 0.2f;
            SFX_Audio_Img.value =  SFX_Player.volume;
        }
        else if (i == -1)
        {
            SFX_Player.volume = SoundData.control.SFX_Data;
            SFX_Audio_Img.value = SFX_Player.volume;
        }

        SoundData.control.ChangeSound(BGM_Player.volume, SFX_Player.volume);
    }
}
