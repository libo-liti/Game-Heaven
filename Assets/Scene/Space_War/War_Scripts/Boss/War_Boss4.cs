using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class War_Boss4 : War_BossPattern
{
    [SerializeField]
    GameObject laserBim;
    [SerializeField]
    GameObject Laser;
    SpriteRenderer spriteRenderer;
    War_Player player;
    War_SpawnBoss SpawnBossBoss;
    float laserSpeed;
    //float speed;
    int status;
    float angle;
    int laserDirection;
    int plusAngle;
    float endTime;
    float time;
    float radian;
    float vaneRotateSpeed;
    float previousTime;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<War_Player>();
        SpawnBossBoss = GameObject.Find("Boss").GetComponent<War_SpawnBoss>();
        vaneRotateSpeed = 0.3f;
        plusAngle = 10;
        endTime = 60;
        time = endTime; 
        laserSpeed = 60f;
        laserDirection = 4;
        angle = 360 / laserDirection;
        radian = Mathf.PI / 180;
        //speed = 1f;
        status = 0;
        previousTime = 0f;
        StartCoroutine(Move());
        StartCoroutine(Shoot());
    }

    // Update is called once per frame
    void Update()
    {
        GameEnd();
    }
    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(3f);
        while (true)
        {
            switch (status)
            {
                case 0:
                    yield return StartCoroutine(LaserBim(gameObject, laserBim));
                    status = 1;
                    break;
                case 1:
                    yield return StartCoroutine(MoveToMid());
                    yield return StartCoroutine(Vane());
                    break;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerLaser")
        {
            War_GameManager.instance.score += player.laserLevel;
        }
    }
    IEnumerator Vane()
    {
        for (int i = 0; i < 170; i++)
        {
            for (int j = 0; j < laserDirection; j++)
            {
                GameObject laser0;
                laser0 = Instantiate(Laser, transform.position, Quaternion.identity);
                laser0.GetComponent<Rigidbody2D>().AddForce(new Vector3(laserSpeed * Mathf.Cos(radian * angle * j + radian * i * plusAngle), laserSpeed * Mathf.Sin(radian * angle * j + radian * i * plusAngle)), 0);
            }   
            if (i == 20) vaneRotateSpeed = 0.2f;        
            else if (i <= 80 && i % 40 == 0) plusAngle *= -1;
            else if (i > 100 && i % 15 == 0) plusAngle *= -1;

            yield return new WaitForSeconds(vaneRotateSpeed);
        }
    }
    void GameEnd()
    {
        if (Time.realtimeSinceStartup - previousTime > 1)
        {
            previousTime = Time.realtimeSinceStartup;
            if(War_GameManager.instance.status == 0)
                {
                    endTime--;
                    SpawnBossBoss.BossHPUI[SpawnBossBoss.bossIndex].GetComponent<Slider>().value = endTime / time;
                    War_GameManager.instance.score += 3;
                }
            if(endTime <= 0)
                War_GameManager.instance.Status(2);
        }
    }
}