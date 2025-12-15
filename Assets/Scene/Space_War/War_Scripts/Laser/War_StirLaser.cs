using System.Collections;
using UnityEngine;

public class War_StirLaser : War_BossLaser
{
    [SerializeField]
    GameObject Laser;

    int laserNum;

    float laserSpeed;
    float angle;
    float radian;
    void Start()
    {
        laserSpeed = 40f;
        laserNum = 13;
        angle = 360 / laserNum;
        radian = Mathf.PI / 180;
        StartCoroutine(Shoot());
    }
    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < laserNum; i++)
        {
            GameObject laser;
            laser = Instantiate(Laser, transform.position, Quaternion.identity);
            laser.GetComponent<Rigidbody2D>().AddForce(new Vector3(laserSpeed * Mathf.Cos(radian * angle * i), laserSpeed * Mathf.Sin(radian * angle * i)), 0);
        }
        Destroy(gameObject);
    }
}
