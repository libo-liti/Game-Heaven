using UnityEngine;
using UnityEngine.UI;

public class Bullet_ObjectPosition : MonoBehaviour  //UI에서의 아이콘들의 위치를 통한 화살표 위치 설정 및 플레이어의 이름을 관리하는 스크립트
{
    public GameObject BGM_Audio_Icon;
    public GameObject SFX_Audio_Icon;
    public GameObject Audio_Pointer;

    public Text First_Name;
    public Text Second_Name;
    public Text Thrid_Name;
    public GameObject Name_Pointer;

    int First_Name_num;
    int Second_Name_num;
    int Thrid_Name_num;

    public static string user_ID;

    void Start()
    {
        First_Name_num = 65;
        Second_Name_num = 65;
        Thrid_Name_num = 65;
    }

    internal void AudioPointer(int i = 0)   //Audio_Icon의 위치를 통해 Audio_Pointer의 위치를 설정하는 함수
    {
        if (i == 0)
        {
            Audio_Pointer.gameObject.transform.position = new Vector2(Audio_Pointer.gameObject.transform.position.x, BGM_Audio_Icon.gameObject.transform.position.y);
            Audio_Pointer.SetActive(true);
        }
        else if (i == 1)
        {
            Audio_Pointer.gameObject.transform.position = new Vector2(Audio_Pointer.gameObject.transform.position.x, SFX_Audio_Icon.gameObject.transform.position.y);
            Audio_Pointer.SetActive(true);
        }
        else if (i > 1)
        {
            Audio_Pointer.SetActive(false);
        }
    }

    internal void NamePointer(int i = 0, bool IsName = false)    // Text Name의 위치를 통해 Name_Pointer의 위치를 설정하는 함수
    {
        if (IsName == false)
        {
            Name_Pointer.SetActive(false);
        }
        else if (i == 0)
        {
            Name_Pointer.gameObject.transform.position = new Vector2(First_Name.gameObject.transform.position.x + 0.1f, Name_Pointer.gameObject.transform.position.y);
            Name_Pointer.SetActive(true);
        }
        else if (i == 1)
        {
            Name_Pointer.gameObject.transform.position = new Vector2(Second_Name.gameObject.transform.position.x + 0.1f, Name_Pointer.gameObject.transform.position.y);
            Name_Pointer.SetActive(true);
        }
        else if (i == 2)
        {
            Name_Pointer.gameObject.transform.position = new Vector2(Thrid_Name.gameObject.transform.position.x + 0.1f, Name_Pointer.gameObject.transform.position.y);
            Name_Pointer.SetActive(true);
        }
    }
    internal void NameChange(int name_Pos = 0, int name_ch = 0) // 함수 호출시 입력값에 따라 이름을 지정하는 아스키 코드를 증가, 감소하는 함수
    {
        if (name_Pos == -1)
        {
            Start();
        }
        else if (name_Pos == 0)
        {
            First_Name_num += name_ch;
            First_Name_num = NameChange_OnlyEng(First_Name_num);
        }
        else if (name_Pos == 1)
        {
            Second_Name_num += name_ch;
            Second_Name_num = NameChange_OnlyEng(Second_Name_num);
        }
        else if (name_Pos == 2)
        {
            Thrid_Name_num += name_ch;
            Thrid_Name_num = NameChange_OnlyEng(Thrid_Name_num);
        }

        First_Name.text = "" + (char)First_Name_num;
        Second_Name.text = "" + (char)Second_Name_num;
        Thrid_Name.text = "" + (char)Thrid_Name_num;
    }

    internal string Get_ID()    // 함수 호출시 지정된 이름을 하나로 통합하는 함수
    {
        char C_name;
        C_name = (char)First_Name_num;
        user_ID = C_name.ToString();
        C_name = (char)Second_Name_num;
        user_ID += C_name.ToString();
        C_name = (char)Thrid_Name_num;
        user_ID += C_name.ToString();
        return user_ID;
    }

    int NameChange_OnlyEng(int name_num)    // 함수 호출시 아스키코드로 변환되는 문자중 영어가 아닌 문자를 방지하기 위한 함수
    {
        if (name_num == 64)
        {
            name_num = 122;
        }
        else if (name_num == 91)
        {
            name_num = 96;
        }
        else if (name_num == 96)
        {
            name_num = 90;
        }
        else if (name_num == 123)
        {
            name_num = 65;
        }
        return name_num;
    }


}
