
using TMPro;
using UnityEngine;

public class Bullet_TextBox : MonoBehaviour // 텍스트 애니메이션에 대한 전반적인 출력를 관리하기 위한 스크립트
{
    public TextMeshProUGUI Text_Box;
    public Transform Player;
    public int sortingOrder;

    internal void Text_About_HP(bool Up) // 채력 변동시 텍스트 애니메이션을 출력하는 함수
    {
        TextMeshProUGUI box = Instantiate(Text_Box, Player.transform.position, Quaternion.identity, GameObject.Find("Canvas").transform);
        box.fontSize = 12;
        if (Up)
        {
            box.text = "<shake>HP + 1</shake>";
        }
        else if (!Up)
        {
            box.text = "<shake>HP - 1</shake>";
        }

    }
    internal void Text_About_Item(int Score = 0) // 스코어 아이템 획득시 텍스트 에니메이션을 출력하는 함수
    {
        TextMeshProUGUI box = Instantiate(Text_Box, Player.transform.position, Quaternion.identity, GameObject.Find("Canvas").transform);
        box.fontSize = 12;
        box.text = $"<shake>Score + {Score}</shake>";
    }
}
