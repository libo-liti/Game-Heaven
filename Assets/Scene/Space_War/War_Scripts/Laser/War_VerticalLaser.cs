using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class War_VerticalLaser : MonoBehaviour
{   
    public bool right;
    void Awake()
    {
        right = true;
    }
    void Start()
    {
        if(right) StartCoroutine(MoveAndDie());
        else StartCoroutine(LeftMove());

        StartCoroutine(BossDie());
    }
    IEnumerator MoveAndDie()
    {
        while(transform.position.y >= 0)
        {
            transform.position += 6f * Vector3.down * Time.deltaTime;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(2f);
        while(true)
        {
            transform.position += 0.4f * Vector3.right * Time.deltaTime;
            if(transform.position.x >= 0) Destroy(gameObject);
            yield return new WaitForSeconds(0.01f);
        }
    }
    IEnumerator LeftMove()
    {
        transform.position = new Vector3(4f, 0, 0);
        while(true)
        {
            transform.position += 3f * Vector3.left * Time.deltaTime;
            yield return new WaitForSeconds(0.01f);
            if(transform.position.x <= -3) {Destroy(gameObject);}
        }
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
