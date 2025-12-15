using System.Collections;
using UnityEngine;

public class Bullet_Pattern : MonoBehaviour // 시간에 따른 패턴 생성에 대한 전반적인 출력를 관리하기 위한 스크립트
{
    public GameObject[] Region = { };
    int i = 0;
    int Side_num = 1;

    internal void Bullet_Pattern_Ingame(int time = 0) // 시간에 따른 패턴들의 출력
    {
        System.Random rand = new System.Random();
        int Num = rand.Next(1, 50);
        if (time == 250)
        {
            Bullet_Red(i);
        }
        if (time > 100)
        {
            if (time % 4 == 0)
            {
                Pattern_Set(2);
            }
            if (time % 5 == 0)
            {
                Pattern_Set(Num % 4 + 4);
            }
        }
        else if (time > 20)
        {
            if (time % 4 == 0)
            {
                Pattern_Set(1);
            }
            if (time % 5 == 0)
            {
                if (time >= 170) //170
                {
                    Side_num = 2;
                    Pattern_Set(Num % 4 + 8);
                }
                else if (time >= 120) //120
                {
                    Side_num = 1;
                    Pattern_Set(Num % 4 + 8);
                }
                else if (time >= 70) //70
                {
                    Side_num = 2;
                    Pattern_Set(Num % 4 + 4);
                }
                else
                {
                    Pattern_Set(Num % 4 + 4);
                }

            }
        }
        else if (time > 2)
        {
            if (time % 4 == 0)
            {
                Pattern_Set(1);
            }
        }
    }
    void Pattern_Set(int Pattern_Num) // 패턴들의 모음
    {
        switch (Pattern_Num)
        {
            case 1:
                GameObject.Find("Bullet_Game").GetComponent<Bullet_PatternMaker>().Rand_CircleShot();
                break;
            case 2:
                GameObject.Find("Bullet_Game").GetComponent<Bullet_PatternMaker>().Rand_Guided_Shot();
                break;
            case 3:
                GameObject.Find("Bullet_Game").GetComponent<Bullet_ProjectileMaker>().ProjectileBullet_Start();
                break;
            case 4:
                StartCoroutine(Blink(0, Side_num));
                //GameObject.Find("Bullet_Game").GetComponent<Bullet_PatternMaker>().Vertical_Pattern_To_Down();
                break;
            case 5:
                StartCoroutine(Blink(1, Side_num));
                //GameObject.Find("Bullet_Game").GetComponent<Bullet_PatternMaker>().Vertical_Pattern_To_Up();
                break;
            case 6:
                StartCoroutine(Blink(2, Side_num));
                //GameObject.Find("Bullet_Game").GetComponent<Bullet_PatternMaker>().Horizontal_Pattern_To_Right();
                break;
            case 7:
                StartCoroutine(Blink(3, Side_num));
                //GameObject.Find("Bullet_Game").GetComponent<Bullet_PatternMaker>().Horizontal_Pattern_To_Left();
                break;
            case 8:
                StartCoroutine(Blink(4, Side_num));
                //GameObject.Find("Bullet_Game").GetComponent<Bullet_PatternMaker>().Gaide_Vertical_Pattern_To_Down();
                break;
            case 9:
                StartCoroutine(Blink(5, Side_num));
                //GameObject.Find("Bullet_Game").GetComponent<Bullet_PatternMaker>().Gaide_Vertical_Pattern_To_Up();
                break;
            case 10:
                StartCoroutine(Blink(6, Side_num));
                //GameObject.Find("Bullet_Game").GetComponent<Bullet_PatternMaker>().Gaide_Vertical_Pattern_To_Right();
                break;
            case 11:
                StartCoroutine(Blink(7, Side_num));
                //GameObject.Find("Bullet_Game").GetComponent<Bullet_PatternMaker>().Gaide_Vertical_Pattern_To_Left();
                break;
            default:
                break;
        }
    }
    void Bullet_Red(int iyy = 0)
    {
        if (iyy == 0)
        {
            Pattern_Set(3);
        }
        i += 1;
    }


    IEnumerator Blink(int num, int SideNum)     // 깜박임 효과 ( 경고 ), 및 가장 자리에서의 패턴 관리
    {
        Region[num % 4].SetActive(true);
        if (SideNum == 2)
        {
            Region[(num + 1) % 4].SetActive(true);
        }

        for (int i = 0; i < 4; i++)
        {
            if (i % 2 == 0)
            {
                Region[num % 4].GetComponent<SpriteRenderer>().color = new Color(209, 0, 0, 0.4f);
            }
            else
            {
                Region[num % 4].GetComponent<SpriteRenderer>().color = new Color(209, 0, 0, 0.0f);
            }

            if (SideNum == 2)
            {
                if (i % 2 == 0)
                {
                    Region[(num + 1) % 4].GetComponent<SpriteRenderer>().color = new Color(209, 0, 0, 0.4f);
                }
                else
                {
                    Region[(num + 1) % 4].GetComponent<SpriteRenderer>().color = new Color(209, 0, 0, 0.0f);
                }
            }
            yield return new WaitForSeconds(0.2f);
        }
        Region[num % 4].SetActive(false);

        if (SideNum == 2)
        {
            Region[(num + 1) % 4].SetActive(false);
        }

        switch (num)
        {
            case 0:
                GameObject.Find("Bullet_Game").GetComponent<Bullet_PatternMaker>().Vertical_Pattern_To_Down();
                break;
            case 1:
                GameObject.Find("Bullet_Game").GetComponent<Bullet_PatternMaker>().Vertical_Pattern_To_Up();
                break;
            case 2:
                GameObject.Find("Bullet_Game").GetComponent<Bullet_PatternMaker>().Horizontal_Pattern_To_Right();
                break;
            case 3:
                GameObject.Find("Bullet_Game").GetComponent<Bullet_PatternMaker>().Horizontal_Pattern_To_Left();
                break;
            case 4:
                GameObject.Find("Bullet_Game").GetComponent<Bullet_PatternMaker>().Gaide_Vertical_Pattern_To_Down();
                break;
            case 5:
                GameObject.Find("Bullet_Game").GetComponent<Bullet_PatternMaker>().Gaide_Vertical_Pattern_To_Up();
                break;
            case 6:
                GameObject.Find("Bullet_Game").GetComponent<Bullet_PatternMaker>().Gaide_Vertical_Pattern_To_Right();
                break;
            case 7:
                GameObject.Find("Bullet_Game").GetComponent<Bullet_PatternMaker>().Gaide_Vertical_Pattern_To_Left();
                break;
            default:
                break;
        }

        if (SideNum == 2)
        {
            if (num >= 4)
            {
                num = (num + 1) % 4 + 4;
            }
            else
            {
                num = (num + 1) % 4;
            }
            switch (num)
            {
                case 0:
                    GameObject.Find("Bullet_Game").GetComponent<Bullet_PatternMaker>().Vertical_Pattern_To_Down();
                    break;
                case 1:
                    GameObject.Find("Bullet_Game").GetComponent<Bullet_PatternMaker>().Vertical_Pattern_To_Up();
                    break;
                case 2:
                    GameObject.Find("Bullet_Game").GetComponent<Bullet_PatternMaker>().Horizontal_Pattern_To_Right();
                    break;
                case 3:
                    GameObject.Find("Bullet_Game").GetComponent<Bullet_PatternMaker>().Horizontal_Pattern_To_Left();
                    break;
                case 4:
                    GameObject.Find("Bullet_Game").GetComponent<Bullet_PatternMaker>().Gaide_Vertical_Pattern_To_Down();
                    break;
                case 5:
                    GameObject.Find("Bullet_Game").GetComponent<Bullet_PatternMaker>().Gaide_Vertical_Pattern_To_Up();
                    break;
                case 6:
                    GameObject.Find("Bullet_Game").GetComponent<Bullet_PatternMaker>().Gaide_Vertical_Pattern_To_Right();
                    break;
                case 7:
                    GameObject.Find("Bullet_Game").GetComponent<Bullet_PatternMaker>().Gaide_Vertical_Pattern_To_Left();
                    break;
                default:
                    break;
            }
        }
    }
}
