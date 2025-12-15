using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Hand_DB : MonoBehaviour
{
    string HOST = "113.198.229.227";
    int PORT = 4005;
    string table_name = "Hand_Score";

    //DB에 점수를 전송하는 함수
    public void DB_Aquest(int num1, int num2, int num3, int score){
        
        StartCoroutine(UnityWebRequestGETSend(num1, num2, num3, score));
    
    }

    IEnumerator UnityWebRequestGETSend(int first, int second, int third, int score)        // DB에 정보 넣기
    {
        char char1 = (char)first;
        char char2 = (char)second;
        char char3 = (char)third;
        
        string name = $"{char1}{char2}{char3}";
        
        // GET 방식
        string url = $"http://{HOST}:{PORT}/insert?table_name={table_name}&name={name}&score={score}";

        // UnityWebRequest에 내장되있는 GET 메소드를 사용한다.
        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();  // 응답이 올때까지 대기한다.

        if (www.error == null)  // 에러가 나지 않으면 동작.
        {
            Debug.Log(www.downloadHandler.text);
        }
        else
        {
            Debug.Log("error");
        }
    }
}
