using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class War_Boss2 : War_BossPattern
{
    [SerializeField]
    GameObject []Laser;
    [SerializeField]
    GameObject Roket;
    Vector3 []roketPos;
    private IEnumerator stir;
    private IEnumerator sin;
    
    Vector3 upDownLeftMove;
    Vector3 upDownMove;

    int status;
    float hp;
    float score;

    float laserSpeed;
    void Start()
    {
        roketPos = new Vector3[3];
        roketPos[0] = new Vector3(-3.2f, -4f, 0f);
        roketPos[1] = new Vector3(-1.1f, -4f, 0f);
        roketPos[2] = new Vector3(1f, -4f, 0f);
        upDownLeftMove = new Vector3(-0.3f, 0.7f, 0);
        upDownMove = new Vector3(0, 1.8f, 0);
        hp = 200;
        score = 200;
        status = 0;
        laserSpeed = 70f;

        StartCoroutine(Move());
        StartCoroutine(Shoot());
    }
    IEnumerator Shoot()
    {
        stir = GoAndStir(Laser[0], laserSpeed);
        sin = SinLaser(gameObject, Laser[1]);
        yield return new WaitForSeconds(3f);
        while(true)
        {
            switch(status)
            {
                case 0:
                    StartCoroutine(stir);
                    yield return StartCoroutine(UpDownLeftMove(upDownLeftMove));
                    StopCoroutine(stir);
                    yield return new WaitForSeconds(6f);
                    status = 1;
                    break;
                case 1:
                    StartCoroutine(sin);
                    yield return StartCoroutine(UpDownMove(upDownMove));
                    Instantiate(Roket, roketPos[0], quaternion.identity);
                    Instantiate(Roket, roketPos[1], quaternion.identity);
                    GameObject roket = Instantiate(Roket, roketPos[2], quaternion.identity);
                    roket.GetComponent<War_Roket>().go = false;
                    yield return new WaitUntil(()=>roket == null);
                    StopCoroutine(sin);
                    yield return new WaitForSeconds(1f);
                    status = 0;
                    break;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) { BossCollision(collision, transform.position, ref hp, score, score); }
}