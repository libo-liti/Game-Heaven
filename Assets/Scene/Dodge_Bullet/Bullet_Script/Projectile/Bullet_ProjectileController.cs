using UnityEngine;

public class Bullet_ProjectileController : MonoBehaviour    // 투사체 이동에 대한 전반적인 데이터를 관리하기 위한 스크립트
{
    // 목표 위치
    Vector2 targetPos;
    // 총알의 위치
    Vector2 myPos;
    // 이동 간격
    Vector3 newPos;
    //총알 속도 조정
    public float mySpeed;
    public float RotaSPeed;
    //총알 삭제 위치
    float Game_Field_x = 4.5f;
    float Game_Field_y = 3f;

    void Start()    // 투사체 생성시 Player를 향해 가도록 하는 함수
    {
        targetPos = GameObject.Find("Player").transform.position;
        myPos = transform.position;

    }

    void FixedUpdate()  //투사체의 이동을 같은 프레임에서 관리 하기위한 함수
    {
        newPos = (targetPos - myPos).normalized * mySpeed * Time.fixedDeltaTime;
        transform.Rotate(new Vector3(0, 0, RotaSPeed) * Time.fixedDeltaTime);
        transform.position = transform.position + newPos;

        if (transform.position.x <= -Game_Field_x || transform.position.x >= Game_Field_x || transform.position.y <= -Game_Field_y || transform.position.y >= Game_Field_y)
        {
            Destroy(gameObject);
        }
    }
}
