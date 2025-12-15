using UnityEngine;
using UnityEngine.UI;

public class Bullet_ButtonOutline : MonoBehaviour //UI 버튼을 선택시 Outline을 사용하기 위한 스크립트
{
    public Image image_1;
    public Image image_2;
    public Image image_3;

    public Material materials;

    internal void ButtonOutline(int Num , bool IsName = false)    // 함수를 호출받으면 입력값에 따라 Outline을 사용 및 해제한다.
    {
        if (IsName == true)
        {       
            image_1.material = null;
            image_2.material = null;
            image_3.material = null;
            return;
        }

        if (Num == 0)
        {
            image_1.material = materials;
            image_2.material = null;
            image_3.material = null;
        }
        else if (Num == 1)
        {
            image_2.material = materials;
            image_1.material = null;
            image_3.material = null;
        }
        else if (Num == 2)
        {
            image_3.material = materials;
            image_1.material = null;
            image_2.material = null;
        }
        else
        {
            image_1.material = null;
            image_2.material = null;
            image_3.material = null;
        }
    }
}
