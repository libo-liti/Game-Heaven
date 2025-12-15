using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountDownTimer : MonoBehaviour
{
    // 3 2 1 을 표현할 텍스트
    public TMP_Text countdownText;

    void Start()
    {
        Time.timeScale = 0;
        StartCoroutine(CountFirst());
    }

    public void CountDown(){
        StartCoroutine(Count());
    }

    public void PauseCountDown(){
        StartCoroutine(PauseCount());
    }

    // 3 2 1 start를 순서대로 출력하는 코드 
    IEnumerator CountFirst()
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
        Hand_GameManager.instance.isCountDown = false;
        Hand_GameManager.instance.hasExecuted = false;
        Hand_GameManager.instance.cantCount = false;
        Time.timeScale = 1;
    }

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
        Hand_GameManager.instance.isCountDown = false;
        Time.timeScale = 1;
    }

    IEnumerator PauseCount()
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
        Hand_GameManager.instance.isCountDown = false;
        Hand_GameManager.instance.isPauseCountDown = false;
        Hand_GameManager.instance.hasExecuted = true;
        Hand_GameManager.instance.cantCount = false;
        Time.timeScale = 1;
    }

    // 이후에 게임이 진행되도록 코드 작성
    // ex) 카운트 전에는 일시정지와 같은 상태였다가, 카운트 종료시 일시정지가 해제되도록.
    //     이렇게 하면 Scene Reload를 통해 게임을 재시작해도 카운트부터 시작해서 유용.
}
