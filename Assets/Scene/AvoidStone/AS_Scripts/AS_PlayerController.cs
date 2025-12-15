using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using Unity.VisualScripting;

public enum ItemType
{
    Heart,
    Star
}

public class AS_PlayerController : MonoBehaviour
{
    
    private float playerY = 0f; // Player Y 위치
    public int hp = 3; // 플레이어의 목숨 수
    private bool isInvincible = false; // 무적 상태 여부
    private float invincibleDuration = 1f; // 무적 지속 시간
    private float invincibleTimer = 0f; // 무적 타이머
    private AS_GameManager GM;
    // 하트 이미지 배열
    public SpriteRenderer[] heartImages;

    // 가득 찬 하트와 빈 하트의 스프라이트
    public Sprite fullHeartSprite;
    public Sprite emptyHeartSprite;
    private AS_Sound sfx_player;
    
    void Start()
    {
        GM = AS_GameManager.instance; // AS_GameManager에 대한 참조 설정
        
        if (GM == null)
        {
            Debug.LogError("AS_GameManager가 참조되지 않았습니다. GameManager 오브젝트를 생성하거나 확인하세요.");
        }
        else
        {
            GM.uiOverGameObject.SetActive(false); // 게임 오버 UI 비활성화
        }
        
    }
    void Awake()
    {
        sfx_player = FindObjectOfType<AS_Sound>();
        if (sfx_player == null)
        {
            Debug.LogError("AS_Sound 컴포넌트를 찾을 수 없습니다!");
        }
    }
    
    void Update()
    {
        if (hp > 0 && Time.timeScale > 0f) // 목숨이 0 이상일 때만 움직임 제어, 게임이 진행중일때만 움직임 제어
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (playerY == 0f) // 현재 위치가 0일 때
                {
                    playerY = 1.3f; // W 키를 누를 때 y값을 3으로 설정
                }
                else if (playerY == -1.3f) // 현재 위치가 -3일 때
                {
                    playerY = 0f; // W 키를 누를 때 y값을 0으로 설정
                }
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (playerY == 0f) // 위치가 0일 때
                {
                    playerY = -1.3f; // S 키를 누를 때 y값을 -3으로 설정
                }
                else if (playerY == 1.3f) // 현재 위치가 3일 때
                {
                    playerY = 0f; // S 키를 누를 때 y값을 0으로 설정
                }
            }
            
        }

        // 플레이어의 위치를 x와 z 값은 그대로 유지하고 y 값만 변경
        Vector3 newPosition = new Vector3(transform.position.x, playerY, transform.position.z);
        transform.position = newPosition;
        if (isInvincible)
        {
            invincibleTimer += Time.deltaTime;
            if (invincibleTimer >= invincibleDuration)
            {
                isInvincible = false;
                SetAlpha(1f); // 무적 시간이 끝나면 캐릭터의 알파(불투명도)를 1로 설정하여 다시 보이게
            }
            else
            {
                // 일정한 간격으로 알파 값을 변경하여 불투명하게
                float alpha = Mathf.PingPong(Time.time * 5f, 0.5f) + 0.5f;
                SetAlpha(alpha);
            }
        }
        
    }
    private void SetAlpha(float alpha)// 불투명에 쓰이는 함수
    {
        Color color = GetComponent<SpriteRenderer>().color;
        color.a = alpha;
        GetComponent<SpriteRenderer>().color = color;
    }

    private void OnTriggerEnter2D(Collider2D other)// 플레이어와 충돌처리 함수
    {
        if (!isInvincible) // 무적 상태가 아닌 경우에만 처리
        {
            if (other.gameObject.tag == "Stone")
            {
                hp--; // 목숨 감소
                
                UpdateHeartImages();
                GM.DecreaseScore();
                Destroy(other.gameObject);
                if(hp==0){
                    GM.isGamePaused = true;
                    GM.isGameOver = true;
                    Time.timeScale = 0f;
                    PlayDeadSound();
                    
                }
                else{
                    // 돌에 맞았을 때 무적 상태로 전환
                    PlayHitSound();
                    isInvincible = true;
                    invincibleTimer = 0f;
                    GetComponent<SpriteRenderer>().enabled = true; // 깜빡임을 시작하기 전에 보이도록 설정
                }
            }
            else if (other.gameObject.CompareTag("Item"))
            {
                // 아이템 획득
                ItemType itemType = other.gameObject.GetComponent<AS_Item>().itemType;
                ConsumeItem(itemType);
                Destroy(other.gameObject);
            }
        }
    }

    private void UpdateHeartImages()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < hp)
            {
                // HP가 현재 하트 개수보다 작을 때는 가득 찬 하트 스프라이트로 설정
                heartImages[i].sprite = fullHeartSprite;
            }
            else
            {
                // HP가 현재 하트 개수보다 클 때는 빈 하트 스프라이트로 설정
                heartImages[i].sprite = emptyHeartSprite;
            }
        }
    }
    
   
    
   
    public void ConsumeItem(ItemType itemType)
    {
        if (itemType == ItemType.Heart)
        {
            PlayHeartSound();
            if (hp < 3) // 현재 hp가 1 또는 2일 경우에만 1 증가
            {
                IncreaseHP();
            }
            else if (hp == 3) // 현재 hp가 3 일 경우 점수 증가
            {
                AS_GameManager.instance.IncreaseScore();
            }
        }
        else if (itemType == ItemType.Star)
        {
            PlayStarSound();
            AS_GameManager.instance.IncreaseScore(); // 별 아이템을 먹을 때 점수 증가
        }
    
    }
    public void  IncreaseHP()//체력 증가 함수
    {
        hp ++;
        UpdateHeartImages();
    }
    public void PlayHeartSound()// 하트 아이템 먹을때 소리 출력
    {
        if (sfx_player != null)
        {
            sfx_player.PlayHeartItemSound();
        }
        else
        {
            Debug.LogError("AS_Sound heart컴포넌트를 찾을 수 없습니다!");
        }
    }
    public void PlayStarSound()// 별 아이템 먹을때 소리 출력
    {
        
        if (sfx_player != null)
        {
            sfx_player.PlayStarItemSound();
        }
        else
        {
            Debug.LogError("AS_Sound star컴포넌트를 찾을 수 없습니다!");
        }
    }
    public void PlayHitSound()//돌에 맞을때 소리 출력
    {
        
        if (sfx_player != null)
        {
            sfx_player.PlayHitSound();
        }
          else
        {
            Debug.LogError("AS_Sound Hit컴포넌트를 찾을 수 없습니다!");
        }
    }
   public void PlayDeadSound()//죽을때 소리 출력
    {
      
        if (sfx_player != null)
        {
            sfx_player.PlayDeadSound();
        }  else
        {
            Debug.LogError("AS_Sound dead컴포넌트를 찾을 수 없습니다!");
        }
    }
   
}
