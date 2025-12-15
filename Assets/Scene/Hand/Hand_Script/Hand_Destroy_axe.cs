using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Destroy_axe : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     private void OnCollisionEnter2D(Collision2D collision) {
       if(collision.gameObject.name == "pickaxe(Clone)"||collision.gameObject.name == "axe(Clone)"){
        Destroy(collision.gameObject);
       }
     }
}
