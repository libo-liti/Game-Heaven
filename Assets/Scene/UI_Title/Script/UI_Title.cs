using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Title : MonoBehaviour // 타이틀 화면에 대한 전반적인 출력를 관리하기 위한 스크립트
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SceneManager.LoadScene("Integration_Scene");
        }
    }

}