using UnityEngine;

public class Bullet_TextBoxMove : MonoBehaviour // 텍스트 애니메이션 박스에 대한 전반적인 출력를 관리하기 위한 스크립트
{
    Vector3 dir;
    private void Start() 
    {
        dir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
        Destroy(gameObject, 0.75f);
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(dir * Time.deltaTime*0.5f);
    }
}