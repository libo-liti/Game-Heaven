using System.Collections;
using UnityEngine;
using TMPro;

public class Bullet_CountDownTimer : MonoBehaviour // 카운트 다운 애니메이션에 대한 전반적인 출력를 관리하기 위한 스크립트
{
    // 3 2 1 을 표현할 텍스트
    public TMP_Text countdownText;

    // 게임 시작과 동시에 카운트다운하도록 함
    void Start()
    {
        StartCoroutine(Count());
    }

    // Coroutine을 호출하기 위한 함수
    internal void CountDown()
    {
        StartCoroutine(Count());
    }
    // 카운트 다운 함수
    IEnumerator Count()
    {
        Time.timeScale = 0;
        GameObject.Find("Canvas").GetComponent<Bullet_UiController>().UIKeyOn = false;
        GameObject.Find("Player").GetComponent<Bullet_PlayerController>().Controll_Player(false);
        int count = 3;

        while (count > 0)
        {
            countdownText.text = count.ToString();
            yield return new WaitForSecondsRealtime(1);
            count--;
        }
        countdownText.text = "Start!";
        yield return new WaitForSecondsRealtime(0.5f);
        countdownText.text = "";
        Time.timeScale = 1;
        GameObject.Find("Canvas").GetComponent<Bullet_UiController>().UIKeyOn = true;
        GameObject.Find("Player").GetComponent<Bullet_PlayerController>().Controll_Player(true);
    }
}
