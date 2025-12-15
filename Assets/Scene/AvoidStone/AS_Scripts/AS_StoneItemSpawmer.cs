using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AS_StoneItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject stonePrefab;         // 돌 프리팹
    [SerializeField] private GameObject heartItemPrefab;     // 하트 아이템 프리팹
    [SerializeField] private GameObject starItemPrefab;      // 별 아이템 프리팹
    [SerializeField] private int itemSpawnProbability = 60; // 아이템 스폰 확률 (기본값: 60%)
    private float[] arrPosy = { -1.3f, 0f, 1.3f };          // y 좌표 배열
    [SerializeField] private float spawnInterval = 1.5f;    // 스폰 간격
    [SerializeField] private float minSpawnInterval = 1.0f; // 최소 스폰 간격
    [SerializeField] private float SpawnIntervalchange = 0.1f; // 최소 스폰 간격
    
    private int spawnCount = 0;                             // 스폰 횟수
    [SerializeField] private float moveSpeedControl = 1;
   
    private readonly object lockObject = new object(); // 동기화 객체

    void Start()
    {
        StartStoneRoutine();
    }

    void StartStoneRoutine()
    {
        StartCoroutine("StoneRoutine");
    }

    IEnumerator StoneRoutine()
    {
        yield return new WaitForSeconds(2f);
        float moveSpeed = 5f;

        while (true)
        {
            // 아이템 스폰 위치를 무작위로 섞기 위한 리스트
            List<float> spawnPositions = new List<float>(arrPosy);
            for (int i = 0; i < spawnPositions.Count; i++)
            {
                float temp = spawnPositions[i];
                int randomIndex = Random.Range(i, spawnPositions.Count);
                spawnPositions[i] = spawnPositions[randomIndex];
                spawnPositions[randomIndex] = temp;
            }

            int maxSpawns = 2;
            if (spawnCount < 10)
            {
                maxSpawns = Random.Range(1, 3);
            }
            else if (spawnCount >= 11 && spawnCount <= 15)
            {
                maxSpawns = Random.Range(2, 3);
            }

            for (int i = 0; i < Mathf.Min(spawnPositions.Count, maxSpawns); i++)
            {
                spawnStone(spawnPositions[i], moveSpeed);

                // 아이템 스폰 (0부터 9까지의 범위에서 랜덤값을 선택)
                if (Random.Range(0, 100) < itemSpawnProbability)
                {
                    SpawnRandomItem(spawnPositions[i], moveSpeed); // 돌의 속도를 전달
                }
            }

            spawnCount += 1;
            if (spawnCount >= 10)
            {
                spawnInterval -= SpawnIntervalchange; // 스폰 간격을 줄임
                
                if (spawnInterval < minSpawnInterval) // 최소 스폰 간격 미만으로 떨어지지 않도록 제한
                {
                    spawnInterval = minSpawnInterval;
                }
                maxSpawns = 2;
            }

            moveSpeed += moveSpeedControl;

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void spawnStone(float posY, float moveSpeed)
    {
        Vector3 spawnPos = new Vector3(transform.position.x, posY, transform.position.z);
        GameObject stoneObject = Instantiate(stonePrefab, spawnPos, Quaternion.identity);
        AS_Stone stone = stoneObject.GetComponent<AS_Stone>();
        stone.SetMoveSpeed(moveSpeed);
    }

     void SpawnRandomItem(float posY, float moveSpeed)
    {
        int randomItem = Random.Range(0, 2); // 0 또는 1 중에서 랜덤으로 선택

        // 사용 가능한 위치 중에서 랜덤으로 선택하여 아이템을 스폰
        List<float> availablePositions = new List<float>(arrPosy);
        foreach (float position in arrPosy)
        {
            if (IsStoneSpawnedAtPosition(new Vector3(transform.position.x, position, transform.position.z)))
            {
                availablePositions.Remove(position);
            }
        }

        if (availablePositions.Count == 1)
        {
            // 사용 가능한 위치 중에서 랜덤으로 선택하여 아이템을 스폰
            float randomPosY = availablePositions[Random.Range(0, availablePositions.Count)];
            Vector3 spawnPos = new Vector3(transform.position.x, randomPosY, transform.position.z);

            if (randomItem == 0)
            {
                // 하트 아이템 스폰
                GameObject heartItem = Instantiate(heartItemPrefab, spawnPos, Quaternion.identity);
                AS_Item heart = heartItem.GetComponent<AS_Item>();
                heart.SetMoveSpeed(moveSpeed); // 아이템의 속도를 설정
            }
            else if (randomItem == 1)
            {
                // 별 아이템 스폰
                GameObject starItem = Instantiate(starItemPrefab, spawnPos, Quaternion.identity);
                AS_Item star = starItem.GetComponent<AS_Item>();
                star.SetMoveSpeed(moveSpeed); // 아이템의 속도를 설정
            }
        }else if (availablePositions.Count == 2)
        {
            // 사용 가능한 위치 중에서 랜덤으로 선택하여 아이템을 스폰
            float randomPosY = availablePositions[Random.Range(0, availablePositions.Count)];
            Vector3 spawnPos = new Vector3(transform.position.x, randomPosY, transform.position.z);

            if (randomItem == 0)
            {
                // 하트 아이템 스폰
                GameObject heartItem = Instantiate(heartItemPrefab, spawnPos, Quaternion.identity);
                AS_Item heart = heartItem.GetComponent<AS_Item>();
                heart.SetMoveSpeed(moveSpeed); // 아이템의 속도를 설정
            }
            else if (randomItem == 1)
            {
                // 별 아이템 스폰
                GameObject starItem = Instantiate(starItemPrefab, spawnPos, Quaternion.identity);
                AS_Item star = starItem.GetComponent<AS_Item>();
                star.SetMoveSpeed(moveSpeed); // 아이템의 속도를 설정
            }
        }
    }

    bool IsStoneSpawnedAtPosition(Vector3 position)
    {
        // 현재 위치 주변에 돌이 있는지 확인
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 1.0f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Stone"))
            {
                return true;
            }
        }
        return false;
    }
}