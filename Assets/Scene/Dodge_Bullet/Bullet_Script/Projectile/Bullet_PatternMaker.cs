using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_PatternMaker : MonoBehaviour // 패턴에 대한 전반적인 출력를 관리하기 위한 스크립트
{
    //총알을 생성후 Target에게 날아갈 변수
    public Transform Target;

    //발사될 일반 총알 오브젝트
    public GameObject Bullet;

    //발사될 유도 총알 오브젝트
    public GameObject Bullet_Guided;

    //총알 생성 위치
    float Game_Field_x = 4.1f;
    float Game_Field_y = 2.5f;

    internal void CircleShot()
    {
        //360번 반복
        for (int i = 0; i < 360; i += 13)
        {
            //총알 생성
            GameObject temp = Instantiate(Bullet);

            //총알 생성 위치를 (0,0) 좌표로 한다.
            temp.transform.position = Vector2.zero;

            //Z에 값이 변해야 회전이 이루어지므로, Z에 i를 대입한다.
            temp.transform.rotation = Quaternion.Euler(0, 0, i);
        }
    }
    internal void Guided_Shot()
    {
        //Target방향으로 발사될 오브젝트 수록
        List<Transform> bullets = new List<Transform>();

        for (int i = 0; i < 360; i += 13)
        {
            //총알 생성
            GameObject temp = Instantiate(Bullet_Guided);

            //총알 생성 위치를 (0,0) 좌표로 한다.
            temp.transform.position = Vector2.zero;

            //?초후에 Target에게 날아갈 오브젝트 수록
            bullets.Add(temp.transform);

            //Z에 값이 변해야 회전이 이루어지므로, Z에 i를 대입한다.
            temp.transform.rotation = Quaternion.Euler(0, 0, i);
        }

        //총알을 Target 방향으로 이동시킨다.
        StartCoroutine(BulletToTarget(bullets));
    }
    private IEnumerator BulletToTarget(IList<Transform> objects, float time = 1f)
    {
        //0.5초 후에 시작
        //yield return new WaitForSeconds(0.5f);

        yield return new WaitForSeconds(time);

        for (int i = 0; i < objects.Count; i++)
        {
            if (objects[i] != null)
            {
                //현재 총알의 위치에서 플레이의 위치의 벡터값을 뻴셈하여 방향을 구함
                Vector3 targetDirection = Target.transform.position - objects[i].position;

                //x,y의 값을 조합하여 Z방향 값으로 변형함. -> ~도 단위로 변형
                float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

                //Target 방향으로 이동
                objects[i].rotation = Quaternion.Euler(0, 0, angle);
            }
        }

        //데이터 해제
        objects.Clear();
    }
    internal void Rand_CircleShot()
    {
        float switchValue = Random.value;
        float xValue = Random.Range(-Game_Field_x, Game_Field_x);
        float yValue = Random.Range(-Game_Field_y, Game_Field_y);
        Vector2 randPos;

        if (switchValue > 0.5f)
        {
            if (Random.value > 0.5f)
            {
                randPos = new Vector2(-Game_Field_x, yValue);
            }
            else
            {
                randPos = new Vector2(Game_Field_x, yValue);
            }
        }
        else
        {
            if (Random.value > 0.5f)
            {
                randPos = new Vector2(xValue, -Game_Field_y);
            }
            else
            {
                randPos = new Vector2(xValue, Game_Field_y);
            }
        }
        //360번 반복
        for (int i = 0; i < 360; i += 13)
        {
            //총알 생성
            GameObject temp = Instantiate(Bullet);

            //총알 생성 위치를 랜덤 좌표로 한다.
            temp.transform.position = randPos;

            //Z에 값이 변해야 회전이 이루어지므로, Z에 i를 대입한다.
            temp.transform.rotation = Quaternion.Euler(0, 0, i);
        }
    }
    internal void Rand_Guided_Shot()
    {
        float switchValue = Random.value;
        float xValue = Random.Range(-Game_Field_x, Game_Field_x);
        float yValue = Random.Range(-Game_Field_y, Game_Field_y);
        Vector2 randPos;

        if (switchValue > 0.5f)
        {
            if (Random.value > 0.5f)
            {
                randPos = new Vector2(-Game_Field_x, yValue);
            }
            else
            {
                randPos = new Vector2(Game_Field_x, yValue);
            }
        }
        else
        {
            if (Random.value > 0.5f)
            {
                randPos = new Vector2(xValue, -Game_Field_y);
            }
            else
            {
                randPos = new Vector2(xValue, Game_Field_y);
            }
        }

        //Target방향으로 발사될 오브젝트 수록
        List<Transform> bullets = new List<Transform>();

        for (int i = 0; i < 360; i += 13)
        {
            //총알 생성
            GameObject temp = Instantiate(Bullet_Guided);

            //총알 생성 위치를 (0,0) 좌표로 한다.
            temp.transform.position = randPos;

            //?초후에 Target에게 날아갈 오브젝트 수록
            bullets.Add(temp.transform);

            //Z에 값이 변해야 회전이 이루어지므로, Z에 i를 대입한다.
            temp.transform.rotation = Quaternion.Euler(0, 0, i);
        }

        //총알을 Target 방향으로 이동시킨다.
        StartCoroutine(BulletToTarget(bullets));
    }
    internal void Horizontal_Pattern_To_Right()
    {
        int N = 6;  //
        float Horizontal_Pattern_y = 2.3f;

        for (float i = 0; i < Horizontal_Pattern_y * 2; i += Horizontal_Pattern_y * 2 / N)
        {
            //총알 생성
            GameObject temp = Instantiate(Bullet);
            //총알 생성 위치를 (0,0) 좌표로 한다.
            temp.transform.position = new Vector2(-4.05f, -Horizontal_Pattern_y + i);
        }
    }
    internal void Vertical_Pattern_To_Up()
    {
        int N = 12;  //
        float Vertical_Pattern_x = 3.9f;

        for (float i = 0; i < Vertical_Pattern_x * 2; i += Vertical_Pattern_x * 2 / N)
        {
            //총알 생성
            GameObject temp = Instantiate(Bullet);
            //총알 생성 위치를 (0,0) 좌표로 한다.
            temp.transform.position = new Vector2(-Vertical_Pattern_x + i, -2.46f);
            temp.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
    }
    internal void Horizontal_Pattern_To_Left()
    {
        int N = 6;  //
        float Horizontal_Pattern_y = 2.3f;

        for (float i = 0; i < Horizontal_Pattern_y * 2; i += Horizontal_Pattern_y * 2 / N)
        {
            //총알 생성
            GameObject temp = Instantiate(Bullet);
            //총알 생성 위치를 (0,0) 좌표로 한다.
            temp.transform.position = new Vector2(+4.05f, -Horizontal_Pattern_y + i);
            temp.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
    }
    internal void Vertical_Pattern_To_Down()
    {
        int N = 12;  //
        float Vertical_Pattern_x = 3.9f;

        for (float i = 0; i < Vertical_Pattern_x * 2; i += Vertical_Pattern_x * 2 / N)
        {
            //총알 생성
            GameObject temp = Instantiate(Bullet);
            //총알 생성 위치를 (0,0) 좌표로 한다.
            temp.transform.position = new Vector2(-Vertical_Pattern_x + i, +2.46f);
            temp.transform.rotation = Quaternion.Euler(0, 0, 270);
        }
    }

    internal void Gaide_Vertical_Pattern_To_Down()
    {
        float switchValue = Random.value;
        float xValue = Random.Range(-Game_Field_x, Game_Field_x);
        float yValue = Random.Range(-Game_Field_y, Game_Field_y);
        Vector2 randPos;

        if (switchValue > 0.5f)
        {
            if (Random.value > 0.5f)
            {
                randPos = new Vector2(-Game_Field_x, yValue);
            }
            else
            {
                randPos = new Vector2(Game_Field_x, yValue);
            }
        }
        else
        {
            if (Random.value > 0.5f)
            {
                randPos = new Vector2(xValue, -Game_Field_y);
            }
            else
            {
                randPos = new Vector2(xValue, Game_Field_y);
            }
        }

        int N = 12;  //
        float Vertical_Pattern_x = 3.9f;

        //Target방향으로 발사될 오브젝트 수록
        List<Transform> bullets = new List<Transform>();

        for (float i = 0; i < Vertical_Pattern_x * 2; i += Vertical_Pattern_x * 2 / N)
        {
            //총알 생성
            GameObject temp = Instantiate(Bullet);
            //총알 생성 위치를 (0,0) 좌표로 한다.
            temp.transform.position = new Vector2(-Vertical_Pattern_x + i, +2.46f);
            temp.transform.rotation = Quaternion.Euler(0, 0, 270);
            //?초후에 Target에게 날아갈 오브젝트 수록
            bullets.Add(temp.transform);
        }
        //총알을 Target 방향으로 이동시킨다.
        StartCoroutine(BulletToTarget(bullets, 0f));
    }
    internal void Gaide_Vertical_Pattern_To_Up()
    {
        float switchValue = Random.value;
        float xValue = Random.Range(-Game_Field_x, Game_Field_x);
        float yValue = Random.Range(-Game_Field_y, Game_Field_y);
        Vector2 randPos;

        if (switchValue > 0.5f)
        {
            if (Random.value > 0.5f)
            {
                randPos = new Vector2(-Game_Field_x, yValue);
            }
            else
            {
                randPos = new Vector2(Game_Field_x, yValue);
            }
        }
        else
        {
            if (Random.value > 0.5f)
            {
                randPos = new Vector2(xValue, -Game_Field_y);
            }
            else
            {
                randPos = new Vector2(xValue, Game_Field_y);
            }
        }

        int N = 12;  //
        float Vertical_Pattern_x = 3.9f;

        //Target방향으로 발사될 오브젝트 수록
        List<Transform> bullets = new List<Transform>();

        for (float i = 0; i < Vertical_Pattern_x * 2; i += Vertical_Pattern_x * 2 / N)
        {
            //총알 생성
            GameObject temp = Instantiate(Bullet);
            //총알 생성 위치를 (0,0) 좌표로 한다.
            temp.transform.position = new Vector2(-Vertical_Pattern_x + i, -2.46f);
            temp.transform.rotation = Quaternion.Euler(0, 0, 90);
            //?초후에 Target에게 날아갈 오브젝트 수록
            bullets.Add(temp.transform);
        }
        //총알을 Target 방향으로 이동시킨다.
        StartCoroutine(BulletToTarget(bullets, 0f));
    }
    internal void Gaide_Vertical_Pattern_To_Left()
    {
        float switchValue = Random.value;
        float xValue = Random.Range(-Game_Field_x, Game_Field_x);
        float yValue = Random.Range(-Game_Field_y, Game_Field_y);
        Vector2 randPos;

        if (switchValue > 0.5f)
        {
            if (Random.value > 0.5f)
            {
                randPos = new Vector2(-Game_Field_x, yValue);
            }
            else
            {
                randPos = new Vector2(Game_Field_x, yValue);
            }
        }
        else
        {
            if (Random.value > 0.5f)
            {
                randPos = new Vector2(xValue, -Game_Field_y);
            }
            else
            {
                randPos = new Vector2(xValue, Game_Field_y);
            }
        }

        int N = 6;  //
        float Horizontal_Pattern_y = 2.3f;

        //Target방향으로 발사될 오브젝트 수록
        List<Transform> bullets = new List<Transform>();

        for (float i = 0; i < Horizontal_Pattern_y * 2; i += Horizontal_Pattern_y * 2 / N)
        {
            //총알 생성
            GameObject temp = Instantiate(Bullet);
            //총알 생성 위치를 (0,0) 좌표로 한다.
            temp.transform.position = new Vector2(+4.05f, -Horizontal_Pattern_y + i);
            temp.transform.rotation = Quaternion.Euler(0, 0, 180);
            //?초후에 Target에게 날아갈 오브젝트 수록
            bullets.Add(temp.transform);
        }
        //총알을 Target 방향으로 이동시킨다.
        StartCoroutine(BulletToTarget(bullets, 0f));
    }
    internal void Gaide_Vertical_Pattern_To_Right()
    {
        float switchValue = Random.value;
        float xValue = Random.Range(-Game_Field_x, Game_Field_x);
        float yValue = Random.Range(-Game_Field_y, Game_Field_y);
        Vector2 randPos;

        if (switchValue > 0.5f)
        {
            if (Random.value > 0.5f)
            {
                randPos = new Vector2(-Game_Field_x, yValue);
            }
            else
            {
                randPos = new Vector2(Game_Field_x, yValue);
            }
        }
        else
        {
            if (Random.value > 0.5f)
            {
                randPos = new Vector2(xValue, -Game_Field_y);
            }
            else
            {
                randPos = new Vector2(xValue, Game_Field_y);
            }
        }

        int N = 6;  //
        float Horizontal_Pattern_y = 2.3f;

        //Target방향으로 발사될 오브젝트 수록
        List<Transform> bullets = new List<Transform>();

        for (float i = 0; i < Horizontal_Pattern_y * 2; i += Horizontal_Pattern_y * 2 / N)
        {
            //총알 생성
            GameObject temp = Instantiate(Bullet);
            //총알 생성 위치를 (0,0) 좌표로 한다.
            temp.transform.position = new Vector2(-4.05f, -Horizontal_Pattern_y + i);
            //?초후에 Target에게 날아갈 오브젝트 수록
            bullets.Add(temp.transform);
        }
        //총알을 Target 방향으로 이동시킨다.
        StartCoroutine(BulletToTarget(bullets, 0f));
    }
}