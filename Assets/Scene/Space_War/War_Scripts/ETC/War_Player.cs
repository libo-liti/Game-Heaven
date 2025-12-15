using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class War_Player : MonoBehaviour
{
    public GameObject[] hpImg;                                          // 목숨 이미지                
    AudioSource audioSource;                                // 레이저 소리    
    Vector3 move;
    SpriteRenderer SpriteRenderer;
    [HideInInspector]
    public int laserLevel;



    bool hitOn;                                             // 충돌 가능 여부
    int hp;
    int hpIndex;
    float speed;
    float verticalEnd;                                      // 가로 이동범위
    float horizontalEnd;                                    // 세로 이동범위
    float lastShootTime;                                    // 마지막으로 레이저 쏜 시간
    float shootInterval;                                    // 레이저 발사 간격

    void Start()
    {
        //hpImg = new Image[3];
        // hpImg[2] = GameObject.Find("Icon_Heart (2)").GetComponent<Image>();
        // hpImg[1] = GameObject.Find("Icon_Heart (1)").GetComponent<Image>();
        // hpImg[0] = GameObject.Find("Icon_Heart").GetComponent<Image>();

        audioSource = GetComponent<AudioSource>();
        move = Vector3.zero;
        SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        hp = 3;
        hitOn = true;
        hpIndex = 2;
        laserLevel = 1;
        speed = 2f;
        verticalEnd = 2.1f;
        horizontalEnd = 3.8f;
        lastShootTime = 0f;
        shootInterval = 0.4f;
    }
    void Update()
    {
        Move();
        Shoot();
        SlowMotion();
    }
    void Move()         // 플레이어 이동
    {
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
        /* GetAxis는 -1f ~ 1f 범위의 값을 반환해서 부드럽지만 느리다.
        GetAxisRaw는 -1f, 0f, 1f를 반환하기 때문에 끊기지만 빠르다. */
        move.Normalize();
        move = transform.position + move * speed * Time.deltaTime;

        move.x = Mathf.Clamp(move.x, -horizontalEnd, horizontalEnd);      // 이동 범위
        move.y = Mathf.Clamp(move.y, -verticalEnd, 1.8f);                // UI 밑으로만? 아니면 UI까지 이동...?
        transform.position = move;
    }
    void Shoot()        // 레이저 발사
    {
        if (Input.GetKey(KeyCode.Z) && War_GameManager.instance.status == 0)
        {
            if (Time.time - lastShootTime > shootInterval)
            {
                audioSource.Play();
                switch (laserLevel)
                {
                    case 1:        // 오브젝트풀 에서 빌려오기
                        var green = War_ObjectPoolManager.instance.GetGo("green");
                        green.transform.position = transform.position;
                        break;
                    case 2:
                        var puple = War_ObjectPoolManager.instance.GetGo("puple");
                        puple.transform.position = transform.position;
                        break;
                    case 3:
                        var thunder = War_ObjectPoolManager.instance.GetGo("thunder");
                        thunder.transform.position = transform.position;
                        break;
                    case 4:
                        var fire = War_ObjectPoolManager.instance.GetGo("fire");
                        fire.transform.position = transform.position;
                        break;
                }
                lastShootTime = Time.time;
            }
        }
    }
    void SlowMotion()   // x 누르면 느려짐
    {
        if (Input.GetKeyDown(KeyCode.X) && War_GameManager.instance.status == 0) Time.timeScale = 0.5f;
        else if (Input.GetKeyUp(KeyCode.X) && War_GameManager.instance.status == 0) Time.timeScale = 1f;
    }
    private void OnTriggerEnter2D(Collider2D collision)     // 충돌시
    {
        if (hitOn)
        {
            if (collision.tag == "BossLaser" || collision.tag == "Roket" || collision.tag == "Boss")
            {
                hp -= 1;
                //hpImg[hpIndex--].color = new Color(0, 0, 0, 0);
                hpImg[hpIndex--].GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
                if (hp <= 0) War_GameManager.instance.Status(2);    // GameOver
                StartCoroutine(Blink(SpriteRenderer));
            }
            else if (collision.tag == "BossCoin")
            {
                laserLevel++;
                Destroy(collision.gameObject);
            }
        }
    }
    public IEnumerator Blink(SpriteRenderer spriteRenderer)     // 깜박임
    {
        if (gameObject.name == "Player") hitOn = false;
        for (int i = 0; i < 10; i++)
        {
            if (i % 2 == 0)
            {
                spriteRenderer.color = new Color(255, 255, 255, 0);
            }
            else
            {
                spriteRenderer.color = new Color(255, 255, 255, 255);
            }
            yield return new WaitForSeconds(0.1f);
        }
        if (gameObject.name == "Player") hitOn = true;
    }
}