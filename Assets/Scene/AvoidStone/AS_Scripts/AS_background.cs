using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AS_background : MonoBehaviour
{
   private float Movespeed =3f;
    // Update is called once per frame
    void Update()
    {
        transform.position +=Vector3.left* Movespeed * Time.deltaTime;
        if(transform.position.x< -10.09){
            transform.position += new Vector3(20.18f,0,0);
        }
    }
}
