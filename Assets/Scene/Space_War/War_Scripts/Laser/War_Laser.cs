using UnityEngine;

public class War_Laser : MonoBehaviour
{
    float verticalEnd;
    float horizontalEnd;

    public War_Laser() { verticalEnd = 3; horizontalEnd = 5; }
    public void justLeftMove(float speed)
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
        RangeOutOfField();
    }
    public void RangeOutOfField()                   // 범위 이탈시 파괴
    {
        if (transform.position.x > horizontalEnd || transform.position.x < -horizontalEnd
            || transform.position.y > verticalEnd || transform.position.y < -verticalEnd)
            Destroy(gameObject);
    }
}