using UnityEngine;
using UnityEngine.UI;

public class UI_Outline : MonoBehaviour // UI 버튼을 선택시 Outline을 사용하기 위한 스크립트
{
    public Image[] Games = { };

    public Image Select_Option;
    public Image Select_Exit;

    
    public Image Info_Start;
    public Image Info_Ranking;
    public Image Info_Exit;

    public Image Setting_Button;


    public Material Outline;

    internal void GameInfo_Outline(int Num)    // 함수를 호출받으면 입력값에 따라 Outline을 사용 및 해제한다.
    {
        if (Num == 0)
        {
            Info_Start.material = Outline;
            Info_Ranking.material = null;
            Info_Exit.material = null;
        }
        else if (Num == 1)
        {
            Info_Ranking.material = Outline;
            Info_Start.material = null;
            Info_Exit.material = null;
        }
        else if (Num == 2)
        {
            Info_Exit.material = Outline;
            Info_Start.material = null;
            Info_Ranking.material = null;
        }
        else
        {
            Info_Start.material = null;
            Info_Ranking.material = null;
            Info_Exit.material = null;
        }
    }
    internal void GameSelect_Outline(int Gamenum = 0, int low = -1)
    {
        Games[0].material = null;
        Games[1].material = null;
        Games[2].material = null;
        Games[3].material = null;
        Select_Option.material = null;
        Select_Exit.material = null;
        if (low == -1)
        {
            Games[Gamenum].material = Outline;
        }
        else
        {
            if (low == 0)
            {
                Select_Option.material = Outline;
            }
            else if (low == 1)
            {
                Select_Exit.material = Outline;
            }
        }
    }
}
