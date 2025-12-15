using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class War_SmallVerticalLaser2 : War_BossLaser
{
    public GameObject Laser;
    void Start()
    {
        StartCoroutine(Shoot());
    }
    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(1.5f);
        for(int i = 0; i < 5; i++)
        {
            GameObject laser1 = Instantiate(Laser, transform.position, Quaternion.identity);
            laser1.GetComponent<Rigidbody2D>().AddForce(Vector3.down * 100f);
            GameObject laser2 = Instantiate(Laser, transform.position, Quaternion.identity);
            laser2.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 100f);
            yield return new WaitForSeconds(0.2f);
        }
        Destroy(gameObject);
    }
}
