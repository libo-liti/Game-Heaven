using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_DestroyStone : MonoBehaviour
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
    if (other.gameObject.tag.Equals("Pickaxe")){
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
    if (other.gameObject.tag.Equals("Axe")){
        Destroy(other.gameObject);
    }
   }
}
