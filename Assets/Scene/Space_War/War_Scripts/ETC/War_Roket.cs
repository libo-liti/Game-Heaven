using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class War_Roket : MonoBehaviour
{
    [HideInInspector]
    public bool go;
    bool stop;
    float speed;
    void Awake() { go = true; stop = false; }
    void Start()
    {
        speed = 3f;
        StartCoroutine(Move());
    }
    IEnumerator Move()
    {
        while (true)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
            if(transform.position.y >= -1.7f && !stop)
            {
                yield return new WaitForSeconds(2f);
                stop = !stop;
                if(!go) speed *= -1;
            }
            else if(transform.position.y > 4 || transform.position.y < -4)
                Destroy(gameObject);
             yield return new WaitForSeconds(0.01f);
        }
    }
}
