using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Platform : MonoBehaviour
{
    public GameObject[] obstacles; // 장애물 오브젝트들
    private bool stepped = false; // 플레이어 캐릭터가 밟았었는가

    // 컴포넌트가 활성화될때 마다 매번 실행되는 메서드
    private void OnEnable() {
        // 발판을 리셋하는 처리
        stepped =false;
        int score = Hand_GameManager.instance.score;
         if(score <20){
            for(int i = 0; i<obstacles.Length; i++){
            if(Random.Range(0,7) == 0){
                obstacles[i].SetActive(true);
            }
            else{
                obstacles[i].SetActive(false);
            }
        }
            }
            else if(score > 20 && score <40){
            for(int i = 0; i<obstacles.Length; i++){
            if(Random.Range(0,5) == 0 || Random.Range(0,5) == 1 ){
                obstacles[i].SetActive(true);
            }
            else{
                obstacles[i].SetActive(false);
            }
        }
            }
            else if(score > 40 && score <60){
            for(int i = 0; i<obstacles.Length; i++){
            if(Random.Range(0,3) == 0){
                obstacles[i].SetActive(true);
            }
            else{
                obstacles[i].SetActive(false);
            }
        }
            }
            else{
            for(int i = 0; i<obstacles.Length; i++){
            if(Random.Range(0,2) == 0){
                obstacles[i].SetActive(true);
            }
            else{
                obstacles[i].SetActive(false);
            }
        }
            }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        // 플레이어 캐릭터가 자신을 밟았을때 점수를 추가하는 처리
        if(collision.collider.tag == "Player" && !stepped){
            stepped = true;
            Hand_GameManager.instance.AddScore(100);
        }
    }
}
