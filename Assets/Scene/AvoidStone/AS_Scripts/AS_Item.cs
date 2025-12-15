using UnityEngine;

namespace AS_Common
{
    public enum ItemType
    {
        Heart,
        Star
    }
}
public class AS_Item : MonoBehaviour
{
    public ItemType itemType;
     [SerializeField]
    private float moveSpeed = 10f;
    private float minX = -5;

    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }

    
    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;// 아이템의 속도

        if (transform.position.x < minX)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)// 아이템 충돌 처리
    {
        if (other.CompareTag("Player"))
        {
            AS_PlayerController playerController = other.GetComponent<AS_PlayerController>();
            if (playerController != null)
            {
                // 아이템을 소비하고 플레이어 컨트롤러에 알림
                Destroy(gameObject); // 아이템 오브젝트를 파괴
                
            }
        }
        else if (other.CompareTag("Stone"))
        {
            // 돌과 충돌할 때 아이템 오브젝트를 파괴
            Destroy(gameObject);
        }
    }


    
}
