using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Networking;


public class UI_GameText : MonoBehaviour // 텍스트에 대한 전반적인 출력를 관리하기 위한 스크립트
{
    private string[] GameName = { "Dodge_Bullet", "What's in your hand", "Avoid Stone", "Space War" };
    private string[] GameInfo = {
        "\nATTENTION! While exploring, you will encounter extraterrestrial beings. Evade their bullets! REMEMBER, as time passes, the enemy's offensive intensifies. It's crucial to stay alert till the end!",
        "\nJump to avoid cliffs and press z and x to destroy obstacles!",
        "\n\"Aviod Stone\" is a horizontal scrolling game where players dodge stones, collect items to boost HP and score. Starting with 3 HP, the game speed and difficulty increase over time.",
        "\nThis is a game where an astronaut adventures through space and encounters a monster. Avoid monster attacks and defeat them with lasers."
    };
    public TMP_Text GameName_Text;
    public TMP_Text Info_Text;
    public TMP_Text[] Top3_Name;
    private string[] result;
    private string HOST = "113.198.229.227";
    private string[] table = { "Bullet_Score", "Hand_Score", "Stone_Score", "War_Score" };
    private int PORT = 4005;
    internal void Game_Text(int Gamenum)
    {
        GameName_Text.text = GameName[Gamenum];
        Info_Text.text = GameInfo[Gamenum];
        // StartCoroutine(GetText(Gamenum));
    }
    IEnumerator GetText(int i = 0)
    {
        string url = $"http://{HOST}:{PORT}/select?table_name={table[i]}";
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Split(www.downloadHandler.text);
            byte[] results = www.downloadHandler.data;
        }
        
        for (int j = 0; j < 3; j++)
            Top3_Name[j].text = "";
        for (int j = 0; j < 3 && j < result.Length / 2; j++)
            Top3_Name[j].text = result[j * 2];
    }
    void Split(string str2)
    {
        string replaceStr = str2.Replace("[", "");
        replaceStr = replaceStr.Replace("]", "");
        replaceStr = replaceStr.Replace("\"", "");
        result = replaceStr.Split(',');
    }
}
