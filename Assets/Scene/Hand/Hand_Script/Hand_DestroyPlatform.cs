using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_DestroyPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other){
    if (other.gameObject.tag.Equals("Axe")){
        Destroy(other.gameObject);
     }
    if (other.gameObject.tag.Equals("Pickaxe")){
        Destroy(other.gameObject);
     }
    }
         private void OnCollisionEnter2D(Collision2D collision) {
       if(collision.gameObject.name == "pickaxe(Clone)"||collision.gameObject.name == "axe(Clone)"){
        Destroy(collision.gameObject);
       }
     }
}
