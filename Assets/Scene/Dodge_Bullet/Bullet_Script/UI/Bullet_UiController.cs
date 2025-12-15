using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet_UiController : MonoBehaviour    // UI에 대한 전반적인 데이터를 관리하기 위한 스크립트
{
    public GameObject Ui_GameReady;
    public GameObject Ui_InGameData;
    public GameObject Ui_Pause;
    public GameObject Ui_GameOver;
    public GameObject Ui_DataInput;

    int Pause_Select = 0;
    int GameOver_Select = 0;
    int Name_Select = 0;
    int DataInput_Select = 0;
    int gab = 1;
    bool IsName = true;

    internal bool UIKeyOn = true;

    void Update()   // 키입력을 받아 UI에서 어떤 선택을 할지 정하는 함수
    {
        if (Input.GetKeyDown(KeyCode.Q) && Ui_GameOver.activeSelf == false && Ui_DataInput.activeSelf == false && UIKeyOn == true)
        {
            Pause();
        }

        if (Ui_Pause.activeSelf == true)
        {
            GameObject.Find("UI_Pause").GetComponent<Bullet_ObjectPosition>().AudioPointer(Pause_Select);
            GameObject.Find("UI_Pause").GetComponent<Bullet_ButtonOutline>().ButtonOutline(Pause_Select - 2);

            if (Input.GetKeyDown(KeyCode.UpArrow) && Pause_Select > 0)
            {
                if (Pause_Select > 1)
                {
                    gab = Pause_Select - 1;
                    Pause_Select = 1;
                }
                else
                {
                    Pause_Select += -1;
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && Pause_Select < 2)
            {
                if (Pause_Select == 1)
                {
                    Pause_Select += gab;
                }
                else
                {
                    Pause_Select += 1;
                }

            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && Pause_Select > 2)
            {
                Pause_Select += -1;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && Pause_Select < 4 && Pause_Select > 1)
            {
                Pause_Select += 1;
            }


            if (Input.GetKeyDown(KeyCode.Z) && Pause_Select == 2)
            {
                Continue();
            }
            else if (Input.GetKeyDown(KeyCode.Z) && Pause_Select == 3)
            {
                ReStart();
            }
            else if (Input.GetKeyDown(KeyCode.Z) && Pause_Select == 4)
            {
                SceneManager.LoadScene("Integration_Scene");
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow) && Pause_Select == 0)
            {
                GameObject.Find("SoundManager").GetComponent<Bullet_SoundController>().ChangeBgmSound(0);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && Pause_Select == 0)
            {
                GameObject.Find("SoundManager").GetComponent<Bullet_SoundController>().ChangeBgmSound(1);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow) && Pause_Select == 1)
            {
                GameObject.Find("SoundManager").GetComponent<Bullet_SoundController>().ChangeSfxSound(0);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && Pause_Select == 1)
            {
                GameObject.Find("SoundManager").GetComponent<Bullet_SoundController>().ChangeSfxSound(1);
            }
        }

        else if (Ui_GameOver.activeSelf == true)
        {
            GameObject.Find("UI_GameOver").GetComponent<Bullet_ButtonOutline>().ButtonOutline(GameOver_Select);

            if (Input.GetKeyDown(KeyCode.LeftArrow) && GameOver_Select > 0)
            {
                GameOver_Select += -1;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && GameOver_Select < 2)
            {
                GameOver_Select += 1;
            }

            if (Input.GetKeyDown(KeyCode.Z) && GameOver_Select == 0)
            {
                DataInput();
            }
            else if (Input.GetKeyDown(KeyCode.Z) && GameOver_Select == 1)
            {
                ReStart();
            }
            else if (Input.GetKeyDown(KeyCode.Z) && GameOver_Select == 2)
            {
                SceneManager.LoadScene("Integration_Scene");
            }
        }
        else if (Ui_DataInput.activeSelf == true)
        {
            GameObject.Find("UI_DataInput").GetComponent<Bullet_ObjectPosition>().NamePointer(Name_Select, IsName);
            GameObject.Find("UI_DataInput").GetComponent<Bullet_ButtonOutline>().ButtonOutline(DataInput_Select, IsName);
            //Debug.Log(DataInput_Select);
            if (IsName == true)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow) && Name_Select > 0)
                {
                    Name_Select -= 1;
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow) && Name_Select < 2)
                {
                    Name_Select += 1;
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    GameObject.Find("UI_DataInput").GetComponent<Bullet_ObjectPosition>().NameChange(Name_Select, 1);
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    GameObject.Find("UI_DataInput").GetComponent<Bullet_ObjectPosition>().NameChange(Name_Select, -1);
                }
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    IsName = false;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow) && DataInput_Select > 0)
                {
                    DataInput_Select -= 1;
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow) && DataInput_Select < 2)
                {
                    DataInput_Select += 1;
                }

                if (Input.GetKeyDown(KeyCode.Z) && DataInput_Select == 0)
                {
                    GameObject.Find("Bullet_Game").GetComponent<Bullet_DataController>().DataInput();
                    ReStart();
                }
                else if (Input.GetKeyDown(KeyCode.Z) && DataInput_Select == 1)
                {
                    ReStart();
                }
                else if (Input.GetKeyDown(KeyCode.Z) && DataInput_Select == 2)
                {
                    SceneManager.LoadScene("Integration_Scene");
                }

                if (Input.GetKeyDown(KeyCode.X))
                {
                    IsName = true;
                }
            }

        }
    }
    void Pause()    // Pause 창 활성화 및 비활성화를 제어하는 함수
    {
        if (Ui_Pause.activeSelf == false)
        {
            Ui_Pause.SetActive(true);
            GameObject.Find("Player").GetComponent<Bullet_PlayerController>().Controll_Player(false);
            Pause_Select = 0;
            Time.timeScale = 0;
        }
        else
        {
            Ui_Pause.SetActive(false);
            GameObject.Find("Ui_Count").GetComponent<Bullet_CountDownTimer>().CountDown();
            //GameObject.Find("Player").GetComponent<Bullet_PlayerController>().Controll_Player(true);
            //Time.timeScale = 1;
        }
    }
    internal void GameOver()    // 게임 오버시 Ui_GameOver 창을 활성화하기 위한 함수
    {
        UIKeyOn = false;
        GameObject.Find("Player").GetComponent<Bullet_PlayerAnimation>().Player_Die_Animation();
        StartCoroutine(GameOverUIOpen());
    }
    IEnumerator GameOverUIOpen()
    {
        yield return new WaitForSeconds(2.4f);
        GameObject.Find("Bullet_Game").GetComponent<Bullet_GameController>().Score_Result();
        Ui_GameOver.SetActive(true);
        GameOver_Select = 0;
        Time.timeScale = 0;
        UIKeyOn = true;
    }
    void DataInput()    // Ui_DataInput 창을 활성화 하기위한 함수
    {
        Ui_GameOver.SetActive(false);
        Ui_DataInput.SetActive(true);
    }
    void Continue() // 계속하기 입력시 Ui_Pause 창을 비활성화 하는 함수
    {
        Ui_Pause.SetActive(false);
        GameObject.Find("Ui_Count").GetComponent<Bullet_CountDownTimer>().CountDown();
        //Time.timeScale = 1;
    }
    void ReStart()  // 다시하기 입력시 게임을 초기화하는 함수
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // Pause_Select = 0;
        // GameOver_Select = 2;
        // DataInput_Select = 0;

        // if (Ui_DataInput.activeSelf == true)
        // {
        //     GameObject.Find("UI_DataInput").GetComponent<Bullet_ObjectPosition>().NameChange(-1);
        // }

        // GameObject.Find("Bullet_Game").GetComponent<Bullet_GameController>().Life_Change(-1);
        // GameObject.Find("Bullet_Game").GetComponent<Bullet_GameController>().GameController_End();
        // GameObject.Find("Bullet_Game").GetComponent<Bullet_ProjectileMaker>().ProjectileMaker_End();
        // GameObject.Find("Bullet_Game").GetComponent<Bullet_GameController>().Start();
        // GameObject.Find("Player").GetComponent<Bullet_PlayerController>().Player_Pos_Reset();
        // GameObject.Find("Bullet_Game").GetComponent<Bullet_ProjectileMaker>().DistoryAllProjectile();
        // GameObject.Find("Player").GetComponent<Bullet_PlayerController>().Controll_Player(true);
        // GameObject.Find("Player").GetComponent<Bullet_PlayerAnimation>().Player_Idle_Animation();
        // GameObject.Find("Bullet_Game").GetComponent<Bullet_ProjectileMaker>().ItemCycle = 0;

        // Ui_Pause.SetActive(false);
        // Ui_GameOver.SetActive(false);
        // Ui_DataInput.SetActive(false);
        // Time.timeScale = 1;
        // GameObject.Find("Ui_Count").GetComponent<Bullet_CountDownTimer>().CountDown();
    }
}
