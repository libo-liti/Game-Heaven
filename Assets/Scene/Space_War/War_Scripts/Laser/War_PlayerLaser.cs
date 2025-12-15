using UnityEngine;

public class War_PlayerLaser : War_PoolAble
{
    public float speed = 2f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
	}

    void FixedUpdate ()
    {
		if (speed != 0)     // 레이저 이동
        {
            rb.velocity = Vector3.right * speed * Time.deltaTime;
            //transform.position += transform.right * (speed * Time.deltaTime);
        }
        else                // 오브젝트 풀에서 나오면 0이라서 
        {
            speed = 200f;
        }
        if (transform.position.x > 5)
        {
            ReleaseObject();
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        speed = 0;

        //Spawn hit effect on collision
        if (collision.tag == "Boss")
        {
            ReleaseObject();
        }
    }
}
