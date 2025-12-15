using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class War_Boss3 : War_BossPattern
{
    [SerializeField]
    GameObject[] Laser;
    [SerializeField]
    GameObject smallRoket;
    IEnumerator sRoket;
    IEnumerator sVerticalLaser;

    int status;
    float hp;
    float score;

    void Start()
    {
        hp = 320;
        score = 320;
        status = 0;
        StartCoroutine(Move());
        StartCoroutine(Shoot());
    }
    IEnumerator Shoot()
    {
    
        while (true)
        {
            yield return new WaitForSeconds(4f);
            switch (status)
            {
                case 0:
                    yield return StartCoroutine(SmallRoket(smallRoket, Laser[0]));
                    status = 1;
                    break;
                case 1:
                    yield return StartCoroutine(SmallVerticalLaser(Laser[1], transform.position));
                    status = 0;
                    break;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(hp <= 0)
        {
            StopCoroutine(sRoket);
            StopCoroutine(sVerticalLaser);
        }
        BossCollision(collision, transform.position, ref hp, score, score);
    }
}