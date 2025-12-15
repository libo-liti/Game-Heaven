using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Obstacles : MonoBehaviour
{
    public GameObject tree;
    public GameObject stone;
    public GameObject gas;

    public Transform treeSpawnPoint;
    public Transform stoneSpawnPoint;
    public Transform gasSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        if(Random.Range(0, 3) == 0){
            GameObject newTree = Instantiate(tree, treeSpawnPoint.position, Quaternion.identity) as GameObject;
        }
        else if(Random.Range(0, 3) == 1){
            GameObject newStone =  Instantiate(stone, stoneSpawnPoint.position, Quaternion.identity) as GameObject;
        }
        else if(Random.Range(0, 3) == 2){
            GameObject newGas =  Instantiate(gas, gasSpawnPoint.position, Quaternion.identity) as GameObject;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
