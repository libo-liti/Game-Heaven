using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab1, platformPrefab2, platformPrefab3, platformPrefab4, platformPrefab5, platformPrefab6; // 생성할 발판의 원본 프리팹
    public Transform PlatformSpawnPoint;

    private float[] minWaitTimes = { 0.36f, 0.54f, 0.72f, 0.90f, 1.08f, 1.26f }; // 각 발판의 최소 대기 시간
    private float timeBetSpawnMax = 1.62f; // 다음 배치까지의 시간 간격 최댓값
    private float timeBetSpawn; // 다음 배치까지의 시간 간격
    private float lastSpawnTime; // 마지막 배치 시점
    private GameObject nowPlatform; // 현재 생성할 발판
    private GameObject nextPlatform; // 다음에 생성할 발판

    void Start()
    {
        // 변수들을 초기화하고 사용할 발판들을 미리 생성
        lastSpawnTime = 0f;
        timeBetSpawn = 0f;

        // 초기에 현재와 다음에 생성할 발판을 랜덤하게 선택
        nowPlatform = GetRandomPlatform1();
        nextPlatform = GetRandomPlatform1();
    }

    void Update()
    {
        // 순서를 돌아가며 주기적으로 발판을 배치
        if (Hand_GameManager.instance.isGameover)
        {
            return;
        }

        if (Hand_GameManager.instance.isPause){
            return;
        }

        if (Time.time >= lastSpawnTime + timeBetSpawn)
        {
            lastSpawnTime = Time.time;

            // 현재 발판을 생성
            Instantiate(nowPlatform, PlatformSpawnPoint.position, Quaternion.identity);

            // 다음에 생성할 발판을 랜덤하게 선택
            nowPlatform = nextPlatform;
            int score = Hand_GameManager.instance.score;
            if(score <20){
                nextPlatform = GetRandomPlatform1();
            }
            else if(score > 20 && score <40){
                nextPlatform = GetRandomPlatform2();
            }
            else{
                nextPlatform = GetRandomPlatform3();
            }
            // 이번에 생성할 발판과 다음에 생성할 발판의 최소 대기 시간을 이용하여 timeBetSpawn 계산
            int nowIndex = GetPlatformIndex(nowPlatform);
            int nextIndex = GetPlatformIndex(nextPlatform);

            float currentMinWaitTime = minWaitTimes[nowIndex];
            float nextMinWaitTime = minWaitTimes[nextIndex];

            timeBetSpawn = Random.Range(currentMinWaitTime + nextMinWaitTime / 2 , timeBetSpawnMax);
        }
    }

    GameObject GetRandomPlatform1()
    {
        int randomIndex = Random.Range(4, 6);

        if (randomIndex == 0)
        {
            return platformPrefab1;
        }
        else if (randomIndex == 1)
        {
            return platformPrefab2;
        }
        else if (randomIndex == 2)
        {
            return platformPrefab3;
        }
        else if (randomIndex == 3)
        {
            return platformPrefab4;
        }
        else if (randomIndex == 4)
        {
            return platformPrefab5;
        }
        else
        {
            return platformPrefab6;
        }
    }

        GameObject GetRandomPlatform2()
    {
        int randomIndex = Random.Range(2, 6);

        if (randomIndex == 0)
        {
            return platformPrefab1;
        }
        else if (randomIndex == 1)
        {
            return platformPrefab2;
        }
        else if (randomIndex == 2)
        {
            return platformPrefab3;
        }
        else if (randomIndex == 3)
        {
            return platformPrefab4;
        }
        else if (randomIndex == 4)
        {
            return platformPrefab5;
        }
        else
        {
            return platformPrefab6;
        }
    }

    GameObject GetRandomPlatform3()
    {
        int randomIndex = Random.Range(0, 6);

        if (randomIndex == 0)
        {
            return platformPrefab1;
        }
        else if (randomIndex == 1)
        {
            return platformPrefab2;
        }
        else if (randomIndex == 2)
        {
            return platformPrefab3;
        }
        else if (randomIndex == 3)
        {
            return platformPrefab4;
        }
        else if (randomIndex == 4)
        {
            return platformPrefab5;
        }
        else
        {
            return platformPrefab6;
        }
    }

    int GetPlatformIndex(GameObject platform)
    {
        if (platform == platformPrefab1)
        {
            return 0;
        }
        else if (platform == platformPrefab2)
        {
            return 1;
        }
        else if (platform == platformPrefab3)
        {
            return 2;
        }
        else if (platform == platformPrefab4)
        {
            return 3;
        }
        else if (platform == platformPrefab5)
        {
            return 4;
        }
        else
        {
            return 5;
        }
    }
}
