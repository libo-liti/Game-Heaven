using UnityEngine;

public class Bullet_PatternBullet : MonoBehaviour // 패턴에 대한 전반적인 삭제를 관리하기 위한 스크립트
{
    public float Speed;

    //총알 삭제 위치
    float Game_Field_x = 4.5f;
    float Game_Field_y = 3f;

    void FixedUpdate()
    {
        //두번째 파라미터에 Space.World를 해줌으로써 Rotation에 의한 방향 오류를 수정함
        transform.Translate(Vector2.right * (Speed * Time.deltaTime), Space.Self);
        if (transform.position.x <= -Game_Field_x || transform.position.x >= Game_Field_x || transform.position.y <= -Game_Field_y || transform.position.y >= Game_Field_y)
        {
            Destroy(gameObject);
        }
    }
}
