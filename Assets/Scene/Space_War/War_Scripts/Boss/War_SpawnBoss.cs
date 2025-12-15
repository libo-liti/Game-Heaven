using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class War_SpawnBoss : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Boss;
    [SerializeField]
    private GameObject Coin;
    public GameObject []BossHPUI;
    [HideInInspector]
    public int bossIndex;
    float coinSpeed;
    void Start()
    {
        bossIndex = 0;
        coinSpeed = 3f;
        GameObject boss = Instantiate(Boss[bossIndex], new Vector3(5f, 0f, 0f), Quaternion.identity);
        boss.name = $"Boss{bossIndex + 1}";
        BossHPUI[bossIndex].gameObject.SetActive(true);
    }
    public void SpawnBoss() { StartCoroutine(spawnBoss()); }        // 각각의 보스 스크립트에서 코루틴 실행시 중간에 파괴되면서 중단되서 따로 만듬
    public void SpawnCoin(Vector3 pos) { StartCoroutine(spawnCoin(pos)); }
    IEnumerator spawnBoss()                     // 보스 생성
    {
        BossHPUI[bossIndex].gameObject.SetActive(false);
        yield return new WaitForSeconds(3f);      
        GameObject boss = Instantiate(Boss[++bossIndex], new Vector3(5f, 0f, 0f), Quaternion.identity);
        boss.name = $"Boss{bossIndex + 1}";
        BossHPUI[bossIndex].gameObject.SetActive(true);
    }
    IEnumerator spawnCoin(Vector3 spawnPos)     // 코인 생성 
    {
        GameObject coinObj;
        coinObj = Instantiate(Coin, spawnPos, Quaternion.identity);
        while(coinObj)
        {
            if(coinObj.transform.position.x <= -3.8f) break;
            coinObj.transform.position += Vector3.left * Time.deltaTime * coinSpeed;
            yield return new WaitForSeconds(0.01f);
        }
    }
}