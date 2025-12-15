using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_ScrollingObject : MonoBehaviour
{
    public float speed = 10f; // 이동 속도

    private void Update() {
        // 게임 오브젝트를 왼쪽으로 일정 속도로 평행 이동하는 처리
        if(!Hand_GameManager.instance.isGameover && !Hand_GameManager.instance.isPause){
            transform.Translate(Vector3.left*speed*Time.deltaTime);
        }
        
    }
}
