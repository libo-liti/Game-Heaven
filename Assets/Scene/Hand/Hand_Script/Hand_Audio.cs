using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Hand_Audio : MonoBehaviour
{
    internal AudioSource audioSource; // 오디오 소스 참조
    internal float volume = 1.0f; // 기본 볼륨 값 (0.0에서 1.0 사이)

    internal int type = 0; // 1이 되면 BGM 변경, 2가 되면 SFX 변경

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // 오디오 소스 컴포넌트 가져오기
    }

    void Update()
    {

    }

    public void VolUp()
    {
        volume = Mathf.Min(volume + 0.2f, 1.0f); // 볼륨을 0.2 증가시키되, 1.0을 초과하지 않도록 함
        audioSource.volume = volume; // 오디오 소스의 볼륨 설정
        if (type == 1)
        {
            SoundData.control.ChangeSound(volume, -1);
        }
        else if (type == 2)
        {
            SoundData.control.ChangeSound(-1, volume);
        }
    }

    public void VolDown()
    {
        volume = Mathf.Max(volume - 0.2f, 0.0f); // 볼륨을 0.2 감소시키되, 0.0 미만으로 내려가지 않도록 함
        audioSource.volume = volume; // 오디오 소스의 볼륨 설정
        if (type == 1)
        {
            SoundData.control.ChangeSound(volume, -1);
        }
        else if (type == 2)
        {
            SoundData.control.ChangeSound(-1, volume);
        }
    }

    public void BGMvolumesetting()
    {
        volume = SoundData.control.BGM_Data;
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        volume = Mathf.Min(volume * 0 + volume, 1.0f);
        audioSource.volume = volume;
    }

    public void SFXvolumesetting()
    {
        volume = SoundData.control.SFX_Data;
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        volume = Mathf.Min(volume * 0 + volume, 1.0f);
        audioSource.volume = volume;
    }
}
