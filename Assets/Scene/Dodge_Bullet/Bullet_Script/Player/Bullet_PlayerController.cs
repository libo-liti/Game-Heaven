using System.Collections;
using UnityEngine;

public class Bullet_PlayerController : MonoBehaviour    // 플레이어에 대한 전반적인 데이터를 관리하기 위한 스크립트
{

    // 키보드 입력으로 이동하는 플레이어 벡터
    public Vector2 inputVec;
    // 플레이어 속도
    public float speed = 2;
    // 플레이어 이동 공간 제어 (상,하,좌,우)
    bool Wall_T = false;
    bool Wall_B = false;
    bool Wall_L = false;
    bool Wall_R = false;

    bool HitOn = true;
    bool PlayerKeyOn = true;
    bool VecOn = true;


    // player 제어
    public static Rigidbody2D rigid;
    static SpriteRenderer spriteRenderer;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()   // 함수 실행시 플레이어의 이동을 입력받기 위한 함수
    {
        if (PlayerKeyOn && VecOn)
        {
            inputVec.x = Input.GetAxisRaw("Horizontal");
            inputVec.y = Input.GetAxisRaw("Vertical");
        }
        else if (!VecOn)
        {
            inputVec = Vector2.zero;
        }
        GameObject.Find("Player").GetComponent<Bullet_PlayerAnimation>().Player_Move_Animation(inputVec.x, inputVec.y);
    }

    internal void Player_Pos_Reset()    // 게임 초기화시 플레이어의 시작 위치를 지정하기 위한 함수
    {
        rigid.position = new Vector2(0, 0f);
    }

    internal void Controll_Player(bool state, bool Vecstate = true)
    {
        VecOn = Vecstate;
        PlayerKeyOn = state;
    }

    void OnTriggerStay2D(Collider2D collider)   // 벽 충돌시 플레이어의 이동을 제한 하기 위한 함수
    {
        if (collider.tag == "Wall_Top")
        {
            Wall_T = true;
        }
        else if (collider.tag == "Wall_Bottom")
        {
            Wall_B = true;
        }
        else if (collider.tag == "Wall_Left")
        {
            Wall_L = true;
        }
        else if (collider.tag == "Wall_Right")
        {
            Wall_R = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)  // 투사체 충돌시 collider 태그를 통해 이후 처리를 하기 위한 함수
    {
        if (HitOn)
        {
            if (collider.tag == "Bullet")
            {
                GameObject.Find("Bullet_Game").GetComponent<Bullet_GameController>().Life_Change(0);
                Destroy(collider.gameObject);
                Bullet_SoundController.instance.SfxSound("Hit");
            }
            if (collider.tag == "Item_Heal")
            {
                GameObject.Find("Bullet_Game").GetComponent<Bullet_GameController>().Life_Change(1);
                Destroy(collider.gameObject);
                Bullet_SoundController.instance.SfxSound("GetItem");
            }
            if (collider.tag == "Coin_Gold")
            {
                Bullet_GameController.item_score += 100;
                Destroy(collider.gameObject);
                Bullet_SoundController.instance.SfxSound("GetItem");
                GameObject.Find("Bullet_Game").GetComponent<Bullet_TextBox>().Text_About_Item(100);
            }
        }
    }
    internal void Want_Blink(bool Die = false)
    {
        StartCoroutine(Blink(spriteRenderer, Die));
    }

    IEnumerator Blink(SpriteRenderer spriteRenderer, bool Die = false)     // 깜박임
    {
        HitOn = false;
        VecOn = false;


        if (!Die)
        {
            GameObject.Find("Player").GetComponent<Bullet_PlayerAnimation>().Player_Hit_True_Animation();
            for (int i = 0; i < 10; i++)
            {
                spriteRenderer.color = new Color(255, 255, 255, 0.3f);

                yield return new WaitForSeconds(0.1f);
            }
            GameObject.Find("Player").GetComponent<Bullet_PlayerAnimation>().Player_Hit_False_Animation();
            VecOn = true;
            for (int i = 0; i < 4; i++)
            {
                if (i % 2 == 0)
                {
                    spriteRenderer.color = new Color(255, 255, 255, 0.6f);
                }
                else
                {
                    spriteRenderer.color = new Color(255, 255, 255, 1f);
                }
                yield return new WaitForSeconds(0.1f);
            }
            for (int i = 0; i < 12; i++)
            {
                if (i % 2 == 0)
                {
                    spriteRenderer.color = new Color(255, 255, 255, 0.6f);
                }
                else
                {
                    spriteRenderer.color = new Color(255, 255, 255, 1f);
                }
                yield return new WaitForSeconds(0.05f);
            }
        }
        else if (Die)
        {

            yield return new WaitForSeconds(2.06f);
            VecOn = true;
        }
        else
        {
            Debug.Log("Bullet_PlayerController 스크립트 에러");
        }
        HitOn = true;
    }

    void FixedUpdate()  //플레이어의 이동을 같은 프레임에서 관리 하기위한 함수
    {
        // 대각 이동 또한 같은 속도로 가기 위함
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;


        if (Wall_T && nextVec.y > 0)       //벽 충돌시 이동 제한
        {
            if (Wall_L && nextVec.x < 0)
            {
                nextVec.y = 0;
                nextVec.x = 0;
                rigid.MovePosition(rigid.position + nextVec);
            }
            else if (Wall_R && nextVec.x > 0)
            {
                nextVec.y = 0;
                nextVec.x = 0;
                rigid.MovePosition(rigid.position + nextVec);
            }
            else
            {
                nextVec.y = 0;
                rigid.MovePosition(rigid.position + nextVec);
                Wall_R = false;
                Wall_L = false;
            }
        }
        else if (Wall_B && nextVec.y < 0)
        {
            if (Wall_L && nextVec.x < 0)
            {
                nextVec.y = 0;
                nextVec.x = 0;
                rigid.MovePosition(rigid.position + nextVec);
            }
            else if (Wall_R && nextVec.x > 0)
            {
                nextVec.y = 0;
                nextVec.x = 0;
                rigid.MovePosition(rigid.position + nextVec);
            }
            else
            {
                nextVec.y = 0;
                rigid.MovePosition(rigid.position + nextVec);
                Wall_R = false;
                Wall_L = false;
            }
        }
        else if (Wall_L && nextVec.x < 0)
        {
            nextVec.x = 0;
            rigid.MovePosition(rigid.position + nextVec);
            Wall_T = false;
            Wall_B = false;
        }
        else if (Wall_R && nextVec.x > 0)
        {
            nextVec.x = 0;
            rigid.MovePosition(rigid.position + nextVec);
            Wall_T = false;
            Wall_B = false;
        }
        else
        {
            rigid.MovePosition(rigid.position + nextVec);
            Wall_T = false;
            Wall_R = false;
            Wall_B = false;
            Wall_L = false;
        }
    }

}
