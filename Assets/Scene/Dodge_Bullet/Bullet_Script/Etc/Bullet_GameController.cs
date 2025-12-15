using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Bullet_GameController : MonoBehaviour  // 게임에 대한 전반적인 데이터를 관리하기 위한 스크립트
{

    public Text Time_Num;
    public Text Score_Num;
    public Text Score_Num_GameOver;
    public Text Score_Num_DataInput;
    public GameObject Life_Heart_1;
    public GameObject Life_Heart_2;
    public GameObject Life_Heart_3;

    public Material SkyBox_Material;

    // 시간 변수
    internal int sec;

    // 점수 변수
    public static int item_score;
    public static int time_score;
    public static int total_score;

    internal void Start()   // 게임 시작 및 재시작시 세팅하기 위한 함수
    {
        sec = 0;
        time_score = 0;
        item_score = 0;
        total_score = 0;
        Time_Num.text = "" + sec;
        Score_Num.text = "" + total_score;
        InvokeRepeating("SetTime", 1f, 1f);
        InvokeRepeating("SetScore", 1f, 0.1f);
        InvokeRepeating("Pattern", 1f, 1f);
    }

    void FixedUpdate()
    {
        SkyBox_Material.SetFloat("_Rotation", 30 + Time.time * 0.5f);
        SkyBox_Material.SetFloat("_Exposure", 1 + sec * 0.01f);
    }

    void Pattern()
    {
        GameObject.Find("Bullet_Game").GetComponent<Bullet_Pattern>().Bullet_Pattern_Ingame(sec + 3);
    }
    internal void GameController_End() // 재시작시 세팅하기 위한 함수
    {
        CancelInvoke("SetTime");
        CancelInvoke("SetScore");
        CancelInvoke("Pattern");
    }

    void SetTime()  // 플레이 시간을 계산하기 위한 함수
    {
        sec = sec + 1;
        Time_Num.text = "" + sec;
    }

    void SetScore() // 플레이 점수를 계산하기 위한 함수
    {
        time_score = sec * 100;
        total_score = time_score + item_score;
        Score_Num.text = "" + total_score;
    }

    internal void Life_Change(int i = 0) // 함수 호출시 입력값에 따라 플레이어의 라이프를 증가, 감소, 초기화 하는 함수
    {
        if (i == -1)
        {
            Life_Heart_1.SetActive(true);
            Life_Heart_2.SetActive(true);
            Life_Heart_3.SetActive(true);
        }
        else if (i == 0)
        {
            if (Life_Heart_3.activeSelf == true)
            {
                Life_Heart_3.SetActive(false);
                GameObject.Find("Player").GetComponent<Bullet_PlayerController>().Want_Blink();
                GameObject.Find("Bullet_Game").GetComponent<Bullet_TextBox>().Text_About_HP(false);
            }
            else if (Life_Heart_2.activeSelf == true)
            {
                Life_Heart_2.SetActive(false);
                GameObject.Find("Player").GetComponent<Bullet_PlayerController>().Want_Blink();
                GameObject.Find("Bullet_Game").GetComponent<Bullet_TextBox>().Text_About_HP(false);

            }
            else if (Life_Heart_1.activeSelf == true)
            {
                Life_Heart_1.SetActive(false);
                GameObject.Find("Player").GetComponent<Bullet_PlayerController>().Want_Blink(true);
                GameObject.Find("Bullet_Game").GetComponent<Bullet_TextBox>().Text_About_HP(false);
                GameObject.Find("Player").GetComponent<Bullet_PlayerController>().Controll_Player(false, false);
                GameObject.Find("Player").GetComponent<Bullet_PlayerAnimation>().Player_Move_Animation(0, 0);
                GameObject.Find("Canvas").GetComponent<Bullet_UiController>().GameOver();
            }
            else
            {
                Debug.Log("err");
                Score_Num_GameOver.text = "" + total_score;
                Score_Num_DataInput.text = "" + total_score;
            }
        }
        else
        {
            if (Life_Heart_1.activeSelf == false)
            {
                Life_Heart_1.SetActive(true);
                GameObject.Find("Bullet_Game").GetComponent<Bullet_TextBox>().Text_About_HP(true);

            }
            else if (Life_Heart_2.activeSelf == false)
            {
                Life_Heart_2.SetActive(true);
                GameObject.Find("Bullet_Game").GetComponent<Bullet_TextBox>().Text_About_HP(true);
            }
            else if (Life_Heart_3.activeSelf == false)
            {
                Life_Heart_3.SetActive(true);
                GameObject.Find("Bullet_Game").GetComponent<Bullet_TextBox>().Text_About_HP(true);
            }
            else
            {
                item_score += 500;
                GameObject.Find("Bullet_Game").GetComponent<Bullet_TextBox>().Text_About_Item(500);
            }
        }

    }
    internal void Score_Result() // 점수를 화면에 출력하는 함수
    {
        Score_Num_GameOver.text = "" + total_score;
        Score_Num_DataInput.text = "" + total_score;
    }

}
