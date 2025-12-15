using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class War_Boss : MonoBehaviour
{
    War_SpawnBoss Boss;
    War_Player Player;
    int damage;
    float bossSpeed;
    public War_Boss()
    {
        bossSpeed = 1f;
    }
    public IEnumerator Move()                      // 보스 첫등장시 이동
    {
        while (true)
        {
            if (transform.position.x >= 2.5f)
                transform.position += Vector3.left * Time.deltaTime * bossSpeed;
            yield return new WaitForSeconds(0.01f);
        }
    }
    public void BossCollision(Collider2D collision, Vector3 pos, ref float hp, float fullHp, float score)     // 보스 피격
    {
        Boss = GameObject.Find("Boss").GetComponent<War_SpawnBoss>();
        Player = GameObject.Find("Player").GetComponent<War_Player>();
        if (collision.tag == "PlayerLaser")
        {
            damage = Player.laserLevel;
            hp -= damage;
            Boss.BossHPUI[Boss.bossIndex].GetComponent<Slider>().value = hp / fullHp;
            War_GameManager.instance.score += GameObject.Find("Player").GetComponent<War_Player>().laserLevel;
            if (hp <= 0)
            {
                War_GameManager.instance.score += score;
                GameObject.Find("Boss").GetComponent<War_SpawnBoss>().SpawnBoss();
                GameObject.Find("Boss").GetComponent<War_SpawnBoss>().SpawnCoin(pos);
                Destroy(gameObject);
            }
        }
    }
}