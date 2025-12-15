using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Hand_GameManager : MonoBehaviour
{
    public static Hand_GameManager instance; // 싱글톤을 할당할 전역 변수

    public bool isGameover = false; // 게임 오버 상태
    public bool isPause = false; // 일시정지 상태
    public bool isCountDown = true;
    public bool isPauseCountDown = false;
    public bool hasExecuted = true;
    public bool cantCount = true;

    GameObject obj;

    public int score = 0; // 게임 점수

    // 게임 시작과 동시에 싱글톤을 구성
    void Awake()
    {
        // 싱글톤 변수 instance가 비어있는가?
        if (instance == null)
        {
            // instance가 비어있다면(null) 그곳에 자기 자신을 할당
            instance = this;
        }
        else
        {
            // instance에 이미 다른 GameManager 오브젝트가 할당되어 있는 경우

            // 씬에 두개 이상의 GameManager 오브젝트가 존재한다는 의미.
            // 싱글톤 오브젝트는 하나만 존재해야 하므로 자신의 게임 오브젝트를 파괴
            Debug.LogWarning("씬에 두개 이상의 게임 매니저가 존재합니다!");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // 초기화
    }

    void Update() { 
        Pause();
    }

    //플레이 도중 q를 누르면 게임이 일시정지 되는 처리
    public void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Q) && isPause == false && isGameover == false && cantCount == false)
        {
            isPause = true;
            hasExecuted = false;
            isPauseCountDown = true;
            cantCount = true;
        }
        else if (Input.GetKeyDown(KeyCode.Q) && isPause == true && isGameover == false)
        {
            isPause = false;
            cantCount = true;
            obj = GameObject.Find("Hand_UI");
            obj.GetComponent<CountDownTimer>().PauseCountDown();
        }
    }

    // 플레이어 이니셜과 점수를 DB로 전송하는 처리
    public void DB_Aquest_Before(int num1, int num2, int num3)
    {
        obj = GameObject.Find("Hand_DB");
        obj.GetComponent<Hand_DB>().DB_Aquest(num1, num2, num3, score);
    }

    // 점수를 증가시키는 처리
    public void AddScore(int newScore)
    {
        if (!isGameover)
        {
            score += newScore;
        }
    }

    // 플레이어가 사망시 게임 오버를 실행하는 처리
    public void OnPlayerDead()
    {
        isGameover = true;
        obj = GameObject.Find("Hand_UI");
        obj.GetComponent<Hand_UI>().isGameover = true;
    }
}
