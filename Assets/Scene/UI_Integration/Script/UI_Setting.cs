using UnityEngine;
using UnityEngine.UI;

public class UI_Setting : MonoBehaviour // 세팅 화면 포인터 및 아웃라인에 대한 전반적인 출력를 관리하기 위한 스크립트
{
    public GameObject BGM_Audio_Icon;
    public GameObject SFX_Audio_Icon;
    public GameObject Audio_Pointer;

    public Image Setting_Exit;

    public Material Outline;

    internal void SettingPointer(int i = 0)   //Audio_Icon의 위치를 통해 Audio_Pointer의 위치를 설정하는 함수
    {
        if (i == 0)
        {
            Setting_Exit.material = null;
            Audio_Pointer.gameObject.transform.position = new Vector3(Audio_Pointer.gameObject.transform.position.x, BGM_Audio_Icon.gameObject.transform.position.y, 1f);
            Audio_Pointer.SetActive(true);
        }
        else if (i == 1)
        {
            Setting_Exit.material = null;
            Audio_Pointer.gameObject.transform.position = new Vector3(Audio_Pointer.gameObject.transform.position.x, SFX_Audio_Icon.gameObject.transform.position.y, 1f);
            Audio_Pointer.SetActive(true);
        }
        else
        {
            Setting_Exit.material = Outline;
            Audio_Pointer.SetActive(false);
        }
    }
}
