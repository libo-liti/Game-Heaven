using UnityEngine;

public class SoundData : MonoBehaviour // 사운드에 대한 전반적인 데이터를 관리하기 위한 스크립트
{
    public static SoundData control;

    public float BGM_Data;
    public float SFX_Data;
    void Awake() // 오브젝트가 파괴되지 않게 하여 사운드 데이터를 유지하게 끔하는 함수
    {
        DontDestroyOnLoad(gameObject);

        if (control == null)
        {
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }
    }
    public void ChangeSound(float Bgm, float Sfx)
    {
        if (Bgm == -1)
        {
            SFX_Data = Sfx;
        }
        else if (Sfx == -1)
        {
            BGM_Data = Bgm;
        }
        else
        {
            BGM_Data = Bgm;
            SFX_Data = Sfx;
        }

    }
}
