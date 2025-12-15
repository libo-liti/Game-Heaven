using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet_DataController : MonoBehaviour  // 서버에 데이터를 저장하기 위한 스크립트
{
    internal void DataInput()   //Bullet_Game에 대이터를 인자로 함수 UnityWebRequestGETTest를 호출하여 서버에 데이터를 저장한다.
    {
        int User_Total_Score = Bullet_GameController.total_score;
        int User_Time_Score = Bullet_GameController.time_score / 100;
        string user_ID = GameObject.Find("UI_DataInput").GetComponent<Bullet_ObjectPosition>().Get_ID();
        StartCoroutine(UnityWebRequestGETTest(user_ID, User_Total_Score));
    }

    IEnumerator UnityWebRequestGETTest(string user_ID, int User_Total_Score = -1)
    {

        // GET 방식
        string url = "http://113.198.229.227:4005/insert?table_name=Bullet_Score&name=" + user_ID + "&score=" + User_Total_Score;
        Debug.Log(url);

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
