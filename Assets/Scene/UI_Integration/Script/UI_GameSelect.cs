using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_GameSelect : MonoBehaviour // 게임 선택창에 대한 전반적인 출력 및 키입력을 관리하기 위한 스크립트
{
    public GameObject[] Games = { };
    private string[] GameScene = { "Bullet_Scene", "Hand_Scene", "AS_Scene", "War_Scene" };

    public GameObject Ui_Select;
    public GameObject UI_GameRanking;
    public GameObject Ui_GameInfo;
    public GameObject Ui_Setting;


    public int GameSelect;
    int OptionSelect;
    int ButtonSelect;
    int SettingSelect;
    bool Button;

    void Start()
    {
        GameSelect = 0;
        GameObject.Find("Canvas").GetComponent<UI_Outline>().GameSelect_Outline(0);
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.RightArrow) && !Ui_GameInfo.activeSelf && !UI_GameRanking.activeSelf && !Button && !Ui_Setting.activeSelf)
        {
            GameSelect++;
            if (GameSelect == 4)
            {
                GameSelect = 0;
            }
            SetIndex(GameSelect);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && !Ui_GameInfo.activeSelf && !UI_GameRanking.activeSelf && !Button && !Ui_Setting.activeSelf)
        {
            GameSelect--;
            if (GameSelect == -1)
            {
                GameSelect = 3;
            }
            SetIndex(GameSelect);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && !Ui_GameInfo.activeSelf && !UI_GameRanking.activeSelf && !Button && !Ui_Setting.activeSelf)
        {
            Button = true;
            GameObject.Find("Canvas").GetComponent<UI_Outline>().GameSelect_Outline(GameSelect, 0);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && !Ui_GameInfo.activeSelf && !UI_GameRanking.activeSelf && Button && !Ui_Setting.activeSelf)
        {
            Button = false;
            GameObject.Find("Canvas").GetComponent<UI_Outline>().GameSelect_Outline(GameSelect, -1);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && !Ui_GameInfo.activeSelf && !UI_GameRanking.activeSelf && Button && !Ui_Setting.activeSelf)
        {
            ButtonSelect++;
            if (ButtonSelect == 2)
            {
                ButtonSelect = 0;
            }
            GameObject.Find("Canvas").GetComponent<UI_Outline>().GameSelect_Outline(GameSelect, ButtonSelect);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && !Ui_GameInfo.activeSelf && !UI_GameRanking.activeSelf && Button && !Ui_Setting.activeSelf)
        {
            ButtonSelect--;
            if (ButtonSelect == -1)
            {
                ButtonSelect = 1;
            }
            GameObject.Find("Canvas").GetComponent<UI_Outline>().GameSelect_Outline(GameSelect, ButtonSelect);
        }
        else if (Input.GetKeyDown(KeyCode.Z) && Ui_Setting.activeSelf)
        {
            if (SettingSelect == 2)
            {
                Ui_Setting.SetActive(false);
                SettingSelect = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.X) && Ui_Setting.activeSelf)
        {
            Ui_Setting.SetActive(false);
            SettingSelect = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Z) && !Ui_GameInfo.activeSelf && !UI_GameRanking.activeSelf && Button && ButtonSelect == 0)
        {
            Ui_Setting.SetActive(true);
            GameObject.Find("GameObject").GetComponent<UI_Setting>().SettingPointer();
        }
        else if (Input.GetKeyDown(KeyCode.Z) && !Ui_GameInfo.activeSelf && !UI_GameRanking.activeSelf && Button && ButtonSelect == 1)
        {
            Application.Quit();
        }
        else if (Input.GetKeyDown(KeyCode.Z) && Ui_GameInfo.activeSelf && OptionSelect == 0)
        {
            SceneManager.LoadScene(GameScene[GameSelect]);
        }
        else if (Input.GetKeyDown(KeyCode.Z) && Ui_GameInfo.activeSelf && OptionSelect == 1)
        {
            UI_GameRanking.SetActive(true);
            Ui_GameInfo.SetActive(false);
            Ui_Select.SetActive(false);
            StartCoroutine(GameObject.Find("Game_Ranking").GetComponent<UI_Ranking>().GetText(GameSelect));
        }
        else if (Input.GetKeyDown(KeyCode.Z) && Ui_GameInfo.activeSelf && OptionSelect == 2)
        {
            Ui_GameInfo.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Z) && !Ui_GameInfo.activeSelf && !UI_GameRanking.activeSelf && !Button && !Ui_Setting.activeSelf)
        {
            OptionSelect = 0;
            Ui_GameInfo.SetActive(true);
            GameObject.Find("GameObject").GetComponent<UI_GameText>().Game_Text(GameSelect);
        }
        else if (Input.GetKeyDown(KeyCode.X) && UI_GameRanking.activeSelf && GameObject.Find("Game_Ranking").GetComponent<UI_Ranking>().isSearch == false)
        {
            GameObject.Find("Game_Ranking").GetComponent<UI_Ranking>().DistoryAllProjectile();
            GameObject.Find("Game_Ranking").GetComponent<UI_Ranking>().rankingNum = 1;
            GameObject.Find("Game_Ranking").GetComponent<UI_Ranking>().rankingIndex = 0;
            UI_GameRanking.SetActive(false);
            Ui_GameInfo.SetActive(true);
            Ui_Select.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.X) && Ui_GameInfo.activeSelf)
        {
            Ui_GameInfo.SetActive(false);
        }

        if (Ui_GameInfo.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                OptionSelect += 1;
                if (OptionSelect == 3)
                {
                    OptionSelect = 0;
                }
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                OptionSelect -= 1;
                if (OptionSelect == -1)
                {
                    OptionSelect = 2;
                }
            }
            GameObject.Find("Canvas").GetComponent<UI_Outline>().GameInfo_Outline(OptionSelect);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && Ui_Setting.activeSelf)
        {
            SettingSelect--;
            if (SettingSelect == -1)
            {
                SettingSelect = 2;
            }
            GameObject.Find("GameObject").GetComponent<UI_Setting>().SettingPointer(SettingSelect);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && Ui_Setting.activeSelf)
        {
            SettingSelect++;
            if (SettingSelect == 3)
            {
                SettingSelect = 0;
            }
            GameObject.Find("GameObject").GetComponent<UI_Setting>().SettingPointer(SettingSelect);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && Ui_Setting.activeSelf)
        {
            if (SettingSelect == 0)
            {
                Ui_SoundController.instance.ChangeBgmSound(0);
            }
            else if (SettingSelect == 1)
            {
                Ui_SoundController.instance.ChangeSfxSound(0);
            }

        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && Ui_Setting.activeSelf)
        {
            if (SettingSelect == 0)
            {
                Ui_SoundController.instance.ChangeBgmSound(1);
            }
            else if (SettingSelect == 1)
            {
                Ui_SoundController.instance.ChangeSfxSound(1);
            }
        }

    }

    void SetIndex(int n) // 게임이 선택됨에 따라 위치, 순서, 크기가 달라지는 함수
    {
        Games[0].transform.SetSiblingIndex((n + 3) % 4);
        Games[1].transform.SetSiblingIndex((n + 2) % 4);
        Games[2].transform.SetSiblingIndex((n + 1) % 4);
        Games[3].transform.SetSiblingIndex(n % 4);

        SetPos(Games[0], (n + 3) % 4);
        SetPos(Games[1], (n + 2) % 4);
        SetPos(Games[2], (n + 1) % 4);
        SetPos(Games[3], n % 4);

        SetScale(Games[0]);
        SetScale(Games[1]);
        SetScale(Games[2]);
        SetScale(Games[3]);

        GameObject.Find("Canvas").GetComponent<UI_Outline>().GameSelect_Outline(n);
    }
    void SetScale(GameObject GameName)
    {
        if (GameName.transform.GetSiblingIndex() == 3)
        {
            //GameName.transform.localScale = new Vector3(1f, 1f, 1f);
            GameName.transform.localScale = new Vector3(0.213f, 0.253f, 0.213f);
        }
        else
        {
            //GameName.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            GameName.transform.localScale = new Vector3(0.149f, 0.177f, 0.149f);
        }

    }
    void SetPos(GameObject GameName, int n = 0)
    {
        if (n == 3)
        {
            GameName.transform.position = new Vector3(0.0f, 0.0f, 100f);
        }
        else if (n == 2)
        {
            GameName.transform.position = new Vector3(1.6f, 0.0f, 101f);
        }
        else if (n == 1)
        {
            GameName.transform.position = new Vector3(0.0f, 0.0f, 100f);
        }
        else if (n == 0)
        {
            GameName.transform.position = new Vector3(-1.6f, 0.0f, 101f);
        }
    }
}
