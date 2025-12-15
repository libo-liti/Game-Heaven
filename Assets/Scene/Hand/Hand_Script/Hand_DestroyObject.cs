using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_DestroyObject : MonoBehaviour
{
    public float deleteTime = 10.0f;
    
    void Update()
    {
        if(!Hand_GameManager.instance.isGameover && !Hand_GameManager.instance.isPause){
            // 현재 오브젝트의 위치
            Vector3 position = transform.position;

            // x값이 -20보다 작아지면 파괴
            if (position.x < -20.0f) {
                Destroy(gameObject);
            }
        }
    }
}
