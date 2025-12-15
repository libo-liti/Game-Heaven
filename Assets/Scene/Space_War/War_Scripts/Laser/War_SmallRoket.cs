using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class War_SmallRoket : MonoBehaviour
{
    public GameObject Laser;
    void Start()
    {
        StartCoroutine(Status());
        StartCoroutine(BossDie());
    }
    IEnumerator Status()
    {
        yield return StartCoroutine(FirstMove());
        StartCoroutine(Shoot());
    }
    IEnumerator FirstMove()
    {
        if(transform.eulerAngles == Vector3.zero)
        {
            while(transform.position.y <= -2)
            {
                transform.position += 1f * Vector3.up * Time.deltaTime;
                yield return new WaitForSeconds(0.01f);
            }
        }
        else
        {
            while(transform.position.y >= 1.7)
            {
                transform.position += 1f * Vector3.down * Time.deltaTime;
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
    IEnumerator Shoot()
    {
        while(true)
        {
            GameObject laser = Instantiate(Laser, transform.position, quaternion.identity);
            if(transform.eulerAngles == Vector3.zero)
                laser.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 20f);
            else
                laser.GetComponent<Rigidbody2D>().AddForce(Vector3.down * 20f);
            yield return new WaitForSeconds(4.5f);
        }
    }
    public IEnumerator LastMove()
    {
        if(transform.eulerAngles == Vector3.zero)
        {
            while(transform.position.y >= -3)
            {
                transform.position += 1f * Vector3.down * Time.deltaTime;
                yield return new WaitForSeconds(0.01f);
            }
        }
        else
        {
            while(transform.position.y <= 2.7)
            {
                transform.position += 1f * Vector3.up * Time.deltaTime;
                yield return new WaitForSeconds(0.01f);
            }
        }
        Destroy(gameObject);
    }
    IEnumerator BossDie()
    {
        while(true)
        {
            if(GameObject.Find("Boss3") == null)
            {
                Destroy(gameObject);
                yield return null;
            }
            yield return new WaitForSeconds(1f);
        }
    }
}   