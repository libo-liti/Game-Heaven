using UnityEngine;

public class War_BossLaser : War_Laser
{
    private void Update() {
        RangeOutOfField();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}