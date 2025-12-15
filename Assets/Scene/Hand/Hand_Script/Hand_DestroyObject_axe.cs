using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_DestroyObject_axe : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(!Hand_GameManager.instance.isGameover && !Hand_GameManager.instance.isPause){
            // 현재 오브젝트의 위치
            Vector3 position = transform.position;

            if (position.x > 4.0f) {
                Destroy(gameObject);
            }
        }
    }
}
