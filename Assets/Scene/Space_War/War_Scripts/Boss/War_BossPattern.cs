using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class War_BossPattern : War_Boss
{
    public IEnumerator Vane(GameObject Laser, float laserSpeed = 200f, float oneDegree = Mathf.PI / 180, int laserDirection = 4) // 바람개비 보스패턴
    {
        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < laserDirection; j++)
            {
                GameObject laser0;
                laser0 = Instantiate(Laser, transform.position, Quaternion.identity);
                laser0.GetComponent<Rigidbody2D>().AddForce(new Vector3(laserSpeed * Mathf.Cos(oneDegree * 360 / laserDirection * j + oneDegree * i * 11), laserSpeed * Mathf.Sin(oneDegree * 360 / laserDirection * j + oneDegree * i * 11), 0));
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    public IEnumerator Sector(GameObject Laser, float angleInterval, float laserSpeed = 100f, float oneDegree = Mathf.PI / 180, float patternAngle = 90, int laserNum = 36) // 부채꼴 보스패턴
    {
        for (int j = 0; j < 8; j++)
        {
            for (int i = 0; i <= laserNum / 2; i++)
            {
                GameObject laser;
                laser = Instantiate(Laser, transform.position, Quaternion.identity);
                
                laser.GetComponent<Rigidbody2D>().AddForce(new Vector3(laserSpeed * Mathf.Cos(oneDegree * (angleInterval * i + patternAngle)), laserSpeed * Mathf.Sin(oneDegree * (angleInterval * i + patternAngle))), 0);
            }
            if (patternAngle == 90) patternAngle = 95;      // 부채꼴이라 원을 반 만들고 90도 회전
            else patternAngle = 90;
            yield return new WaitForSeconds(1f);
        }
    }
    public IEnumerator BigLaser(GameObject Laser, float laserSpeed = 200f)          // 큰 레이저 보스패턴
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject laser;
            laser = Instantiate(Laser, transform.position, Quaternion.identity);
            laser.GetComponent<Rigidbody2D>().AddForce(Vector3.left * laserSpeed * 2);
            yield return new WaitForSeconds(1f);
        }
    }
    public IEnumerator GoAndStir(GameObject Laser, float speed)                     // 왼쪽으로 가다가 원으로 퍼지는 패턴
    {
        while (true)
        {
            GameObject laser;
            laser = Instantiate(Laser, transform.position, Quaternion.identity);
            laser.GetComponent<Rigidbody2D>().AddForce(Vector3.left * speed);
            yield return new WaitForSeconds(1.5f);
        }
    }
    public IEnumerator UpDownLeftMove(Vector3 move)                                     // 보스가 위아래로 움직이면서 가운데로 이동
    {
        while (true)
        {
            transform.position += move * Time.deltaTime;
            if (transform.position.y >= 1.2 || transform.position.y <= -1.6) move.y *= -1;
            if (transform.position.x <= 1 && transform.position.y >= 0) break;
            yield return new WaitForSeconds(0.01f);
        }

        while (transform.position.x <= 2.5f)
        {
            transform.position += Vector3.right * 2f * Time.deltaTime;
            yield return new WaitForSeconds(0.01f);
        }
    }
    public IEnumerator UpDownMove(Vector3 move)
    {
        float startTime = Time.time;
        float finishTime = 10;
        while (true)
        {
            transform.position += move * Time.deltaTime;
            if (transform.position.y >= 1.2 || transform.position.y <= -1.6) move.y *= -1.01f;
            if (Time.time - startTime > finishTime) break;
            yield return new WaitForSeconds(0.01f);
        }
        if (transform.position.y > 0)
        {
            while (transform.position.y >= 0)
            {
                transform.position += Vector3.down * Time.deltaTime;
                yield return new WaitForSeconds(0.01f);
            }
        }
        else
        {
            while (transform.position.y <= 0)
            {
                transform.position += Vector3.up * Time.deltaTime;
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
    public IEnumerator SinLaser(GameObject obj, GameObject Laser)      // Sin 함수 패턴 양쪽으로 해서 DNA모양으로 나옴
    {
        while (true)
        {
            GameObject laser = Instantiate(Laser, obj.transform.position, quaternion.identity);
            laser.GetComponent<War_SinLaser>().amplitude *= -1;
            Instantiate(Laser, obj.transform.position, quaternion.identity);
            yield return new WaitForSeconds(0.2f);
        }
    }
    public IEnumerator SmallRoket(GameObject SmallRoket, GameObject Laser)
    {
        List<GameObject> Rokets = new List<GameObject>();
        GameObject laser0 = Instantiate(Laser);
        laser0.GetComponent<War_VerticalLaser>().right = false;
        yield return new WaitUntil(() => laser0 == null);
        for (int i = 0; i < 8; i++)
        {
            GameObject smallRoket = Instantiate(SmallRoket, new Vector3(-3.5f + 0.5f * i, -3, 0), Quaternion.identity);
            Rokets.Add(smallRoket);
            if (i % 2 == 1)
            {
                smallRoket.transform.Rotate(new Vector3(0, 0, 180));
                smallRoket.transform.position += new Vector3(0, 5.7f, 0);
            }
        }
        
        GameObject laser1 = Instantiate(Laser, new Vector3(-2.5f, 5, 0), Quaternion.identity);
        laser1.transform.Rotate(0, 0, 90);
        GameObject laser2 = Instantiate(Laser, new Vector3(-4f, 5, 0), Quaternion.identity);
        laser2.transform.Rotate(0, 0, 90);
        yield return new WaitUntil(() => laser2 == null);
        foreach (GameObject obj in Rokets) { StartCoroutine(obj.GetComponent<War_SmallRoket>().LastMove()); }
    }
    public IEnumerator SmallVerticalLaser(GameObject Laser, Vector3 pos)
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject laser = Instantiate(Laser, pos, Quaternion.identity);
            laser.GetComponent<Rigidbody2D>().AddForce(new Vector3(-200, 0, 0));
            laser.GetComponent<War_SmallVerticalLaser1>().spawnTime = (i % 3 + 2) * 0.1f;
            yield return new WaitForSeconds(4f);
        }
    }
    public IEnumerator MoveToMid()
    {
        while (transform.position.x >= 0)
        {
            transform.position += Vector3.left * Time.deltaTime  * 3;
            yield return new WaitForSeconds(0.01f);
        }
        yield return null;
    }
    public IEnumerator LaserBim(GameObject obj, GameObject Laser)
    {
        War_Player player = GameObject.Find("Player").GetComponent<War_Player>();
        StartCoroutine(player.Blink(obj.GetComponent<SpriteRenderer>()));        
        yield return new WaitForSeconds(2f);
        GameObject laser = Instantiate(Laser, Vector3.zero, Quaternion.identity); 
        yield return new WaitForSeconds(2f);
        Destroy(laser);
    }
}