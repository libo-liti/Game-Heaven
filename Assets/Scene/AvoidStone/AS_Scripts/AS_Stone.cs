using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AS_Stone : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;
    private float minX = -4.8f;

    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if (transform.position.x < minX)
        {
            AS_GameManager.instance.IncreaseScore();
            Destroy(gameObject);
        }
    }

}
