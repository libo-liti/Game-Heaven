using UnityEngine;

public class Bullet_ProjectileMaker : MonoBehaviour // 투사체 생성에 대한 전반적인 데이터를 관리하기 위한 스크립트
{
    // 투사체 프리펩
    public GameObject bullet_Fire;

    public GameObject Coin_Gold;
    public GameObject Item_Heal;

    //총알 생성 위치
    float Game_Field_x = 4.1f;
    float Game_Field_y = 2.5f;

    public int ItemCycle = 0;

    private void Start()
    {
        InvokeRepeating("MakeItem", 0f, 1f);
    }
    internal void ProjectileBullet_Start()    // 투사체 발사 주기를 제어하는 함수
    {
        InvokeRepeating("MakeProjectile", 0f, 1f);
    }

    void MakeProjectile()   // 특정이름의 투사체를 만드는 함수
    {
        ThrowProjectile(bullet_Fire);
    }
    void MakeItem()
    {
        if (ItemCycle % 15 == 0 && ItemCycle != 0)
        {
            ThrowProjectile(Item_Heal);
        }
        else if (ItemCycle % 5 == 0 && ItemCycle != 0)
        {
            ThrowProjectile(Coin_Gold);
        }
        ItemCycle += 1;
    }
    internal void ProjectileMaker_End()
    {
        CancelInvoke("MakeProjectile");
    }

    void ThrowProjectile(GameObject Projectile_Name)    // Projectile_Name 이름의 투사체를 발사 위치를 정하는 함수
    {
        GameObject Projectile;

        float switchValue = Random.value;
        float xValue = Random.Range(-Game_Field_x, Game_Field_x);
        float yValue = Random.Range(-Game_Field_y, Game_Field_y);

        if (switchValue > 0.5f)
        {
            if (Random.value > 0.5f)
            {
                Projectile = Instantiate(Projectile_Name, new Vector2(-Game_Field_x, yValue), Quaternion.identity);
            }
            else
            {
                Projectile = Instantiate(Projectile_Name, new Vector2(Game_Field_x, yValue), Quaternion.identity);
            }
        }
        else
        {
            if (Random.value > 0.5f)
            {
                Projectile = Instantiate(Projectile_Name, new Vector2(xValue, -Game_Field_y), Quaternion.identity);
            }
            else
            {
                Projectile = Instantiate(Projectile_Name, new Vector2(xValue, Game_Field_y), Quaternion.identity);
            }
        }
    }

    void DistoryProjectile(string Projectile_Name)  // Projectile_Name 이름의 투사체를 삭제하는 함수
    {
        GameObject[] Projectile = GameObject.FindGameObjectsWithTag($"{Projectile_Name}");
        foreach (GameObject items in Projectile)
        {
            Destroy(items);
        }
    }

    internal void DistoryAllProjectile()    // 모든 투사체를 삭제하는 함수
    {
        string name;

        name = "Bullet";
        DistoryProjectile(name);
        name = "Coin_Gold";
        DistoryProjectile(name);
        name = "Coin_Dia";
        DistoryProjectile(name);
        name = "Item_Heal";
        DistoryProjectile(name);
    }

}
