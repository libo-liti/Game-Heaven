using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class War_Boss1 : War_BossPattern
{
    [SerializeField]
    private GameObject[] Laser;            
    SpriteRenderer spriteRenderer; 

    int laserNum;               // 360도를 몇개로 나눌지              
    int laserIndex;                    
    float hp;                                 
    float score;                             

    float angleInterval;        // 레이저 원으로 발사할때 얼마나 촘촘하게 할건지                        

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        laserNum = 36;
        laserIndex = 0;
        hp = 45;
        score = 45;
        angleInterval = 360 / laserNum;
        StartCoroutine(Move());
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(2f);        
        while (true)
        {
            switch (laserIndex)
            {
                case 0:
                    yield return StartCoroutine(Sector(Laser[0], angleInterval));   // 부채꼴 패턴
                    laserIndex = 1;
                    break;
                case 1:
                    yield return StartCoroutine(GameObject.Find("Player").GetComponent<War_Player>().Blink(spriteRenderer));
                    yield return new WaitForSeconds(1f);
                    yield return StartCoroutine(BigLaser(Laser[1]));                // 레이저 큰거
                    laserIndex = 2;
                    break;
                case 2:
                    yield return StartCoroutine(Vane(Laser[0]));                    // 바람개비 패턴
                    laserIndex = 0;
                    break;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) { BossCollision(collision, transform.position, ref hp, score, score); }
}