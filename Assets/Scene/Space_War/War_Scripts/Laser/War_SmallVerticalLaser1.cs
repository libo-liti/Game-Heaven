using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class War_SmallVerticalLaser1 : War_BossLaser
{
    public GameObject[] Laser;
    [HideInInspector]
    public float spawnTime;
    void Awake()
    {
        spawnTime = 0.3f;
    }
    void Start()
    {
        StartCoroutine(Shoot());
    }
    IEnumerator Shoot()
    {
        int i = 0;
        while(true)
        {
            yield return new WaitForSeconds(spawnTime);
            RangeOutOfField();
            if(i % 3 != 0) Instantiate(Laser[0], transform.position, Quaternion.identity);
            else Instantiate(Laser[1], transform.position, Quaternion.identity);
            i++;
        }
    }
}