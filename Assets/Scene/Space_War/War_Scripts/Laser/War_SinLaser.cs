using System.Collections;
using UnityEngine;

public class War_SinLaser : War_BossLaser
{
    [HideInInspector]
    public float amplitude;
    float moveDistance;
    float bossPosY;
    void Awake()
    {
        amplitude = 1.2f;
        moveDistance = 0.03f;
        bossPosY = 0;
    }
    void Start()
    {
        StartCoroutine(SinMove());
    }
    IEnumerator SinMove()
    {
        while(true)
        {
            if(GameObject.Find("Boss2")) bossPosY = GameObject.Find("Boss2").transform.position.y;
            transform.position = new Vector3(transform.position.x - moveDistance, bossPosY + amplitude * Mathf.Sin(1.3f * transform.position.x), 0);
            RangeOutOfField();
            yield return new WaitForSeconds(0.01f);
        }
    }
}