using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AS_Sound : MonoBehaviour
{
    // [SerializeField] private AudioClip backgroundSound; // 배경사운드
    [SerializeField] private AudioClip HitSound;
    [SerializeField] private AudioClip DeadSound;
    [SerializeField] private AudioClip StarItemSound;
    [SerializeField] private AudioClip HeartItemSound;


    private AudioSource bgm_player;
    private AudioSource sfx_player;

    private const string EffectVolumeKey = "EffectVolume"; // PlayerPrefs에 사용할 키


    private void Start()
    {
        StartBackgroundMusic();

    }
    void Awake()
    {
        bgm_player = GameObject.Find("BGM Player").GetComponent<AudioSource>();
        sfx_player = GameObject.Find("SFX Player").GetComponent<AudioSource>();
        bgm_player.volume = SoundData.control.BGM_Data;
        sfx_player.volume = SoundData.control.SFX_Data;

    }

    private void StartBackgroundMusic()
    {
        bgm_player.Play();
    }

    private void PlayEffectSound(AudioClip sound)
    {
        sfx_player.clip = sound;

        sfx_player.Play();
    }


    public void PlayHitSound()
    {
        PlayEffectSound(HitSound);
        Debug.Log("Hit Sound");
    }

    public void PlayDeadSound()
    {
        PlayEffectSound(DeadSound);
        Debug.Log("Dead Sound");
    }

    public void PlayStarItemSound()
    {
        PlayEffectSound(StarItemSound);
        Debug.Log("Star Sound");
    }

    public void PlayHeartItemSound()
    {
        PlayEffectSound(HeartItemSound);
        Debug.Log("Heart Sound");
    }

    // 배경음 다운 조절 함수
    public void DecreaseBackgroundMusicLevel()
    {
        bgm_player.volume = Mathf.Max(0f, bgm_player.volume - 0.2f);
        SoundData.control.ChangeSound(bgm_player.volume,sfx_player.volume);
    }

    // 배경음 업 조절 함수
    public void IncreaseBackgroundMusicLevel()
    {

        bgm_player.volume = Mathf.Min(1.0f, bgm_player.volume + 0.2f);
        SoundData.control.ChangeSound(bgm_player.volume,sfx_player.volume);
    }


    public void IncreaseEffectSoundLevel()
    {
        sfx_player.volume = Mathf.Min(1.0f, sfx_player.volume + 0.2f);
        SoundData.control.ChangeSound(bgm_player.volume,sfx_player.volume);
    }

    // 효과음 다운 조절 함수
    public void DecreaseEffectSoundLevel()
    {
        sfx_player.volume = Mathf.Max(0f, sfx_player.volume - 0.2f);
        SoundData.control.ChangeSound(bgm_player.volume,sfx_player.volume);
    }

}