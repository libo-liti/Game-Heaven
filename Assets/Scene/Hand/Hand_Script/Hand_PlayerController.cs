using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Hand_PlayerController : MonoBehaviour
{
    public AudioClip deathClip;
    public AudioClip slideClip;
    public AudioClip throwClip;
    public AudioClip jumpClip;

    public float jumpForce = 500f; // 점프 힘
    public float throwingForce = 500f; // 던지는 힘

    private int jumpCount = 0; // 누적 점프 횟수
    private bool isGrounded = false; // 바닥에 닿았는지 나타냄
    private bool isDead = false; // 사망 상태
    private bool pauseState = false; // 정지 상태
    bool isThrowing = false;

    public GameObject axeBall; // 도끼
    public GameObject pickaxeBall; // 곡괭이
    public GameObject obj;

    //public Transform axeSpawn;

    private Rigidbody2D playerRigidbody; // 사용할 리지드바디 컴포넌트
    private Animator animator; // 사용할 애니메이터 컴포넌트
    private AudioSource playerAudio;

    // Start is called before the first frame update
    private void Start()
    {
        // 초기화
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
    }

    private void Update()
    {
        if (Hand_GameManager.instance.isPause == true)
        {
            pauseState = true;
        }
        else if (Hand_GameManager.instance.isPause == false)
        {
            pauseState = false;
        }

        // 사용자 입력을 감지하고 점프하는 처리
        if (isDead)
        {
            return;
        }
        if (pauseState)
        {
            return;
        }
        if (Hand_GameManager.instance.isCountDown == true)
        {
            return;
        }
        if (Hand_GameManager.instance.isPauseCountDown == true)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && jumpCount < 2)
        {
            jumpCount++;
            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
            playerAudio.clip = jumpClip;
            playerAudio.Play();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && playerRigidbody.velocity.y > 0)
        {
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
        }

        animator.SetBool("Grounded", isGrounded);

        // 사용자의 입력을 감지하고 슬라이딩하는 처리
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Slide(true);
            playerAudio.clip = slideClip;
            playerAudio.Play();
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            Slide(false);
        }

        // 사용자의 입력을 감지하고 도구를 던지는 처리
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Shoot_axe();
            playerAudio.clip = throwClip;
            playerAudio.Play();
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            Shoot_pickaxe();
            playerAudio.clip = throwClip;
            playerAudio.Play();
        }
    }

    private void Die()
    {
        // 사망 처리
        animator.SetTrigger("Die");
        playerAudio.clip = deathClip;
        playerAudio.Play();
        playerRigidbody.velocity = Vector2.zero;
        isDead = true;
        Hand_GameManager.instance.OnPlayerDead();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 트리거 콜라이더를 가진 장애물과의 충돌을 감지
        if (other.tag == "Dead" && !isDead)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 바닥에 닿았음을 감지하는 처리
        if (collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 바닥에서 벗어났음을 감지하는 처리
        isGrounded = false;
    }

IEnumerator ThrowToolAndWait(GameObject tool, Vector3 spawnPosition, float throwingForce)
{
    isThrowing = true;
    // 도구를 생성하고 물리적 힘을 가함
    GameObject newToolBullet = Instantiate(tool, spawnPosition, Quaternion.identity);
    Rigidbody2D toolRigidbody = newToolBullet.GetComponent<Rigidbody2D>();
    toolRigidbody.AddForce(Vector3.right * throwingForce);
    toolRigidbody.angularVelocity = 500f;

    // 0.5초 동안 대기
    yield return new WaitForSeconds(0.2f);

    isThrowing = false;
}

void Shoot_axe()
{
    if (!isThrowing){
    // 도끼를 던지는 처리
    Vector3 spawnPosition = transform.position + new Vector3(1.0f, 0.0f, 0.0f);
    StartCoroutine(ThrowToolAndWait(axeBall, spawnPosition, throwingForce));
    }
}

void Shoot_pickaxe()
{
    if (!isThrowing){
    // 곡괭이를 던지는 처리
    Vector3 spawnPosition = transform.position + new Vector3(1.0f, 0.0f, 0.0f);
    StartCoroutine(ThrowToolAndWait(pickaxeBall, spawnPosition, throwingForce));
    }
}


    void Slide(bool startSlide)
    {
        // 슬라이딩을 하는 처리
        if (startSlide)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = true;
            animator.SetBool("Slided", true);
        }
        else
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            animator.SetBool("Slided", false);
        }
    }
}
