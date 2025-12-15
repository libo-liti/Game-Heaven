using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class War_CountDownTimer : MonoBehaviour
{
    // 3 2 1 을 표현할 텍스트
    public TMP_Text countdownText;

    // 게임 시작과 동시에 카운트다운하도록 함
    void Start()
    {
        Time.timeScale = 0;
        StartCoroutine(Count());
    }

    // Coroutine을 호출하기 위한 함수
    public void CountDown(){
        StartCoroutine(Count());
    }

    // 카운트 다운 함수
    IEnumerator Count()
    {
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
        GameObject.Find("GameManager").GetComponent<War_GameManager>().cantCount = false;
        Time.timeScale = 1;
    }
}
