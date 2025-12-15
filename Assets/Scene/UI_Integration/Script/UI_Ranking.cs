using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UI_Ranking : MonoBehaviour  // 랭킹에 대한 전반적인 출력를 관리하기 위한 스크립트
{
    List<string> SearchResults;
    public GameObject[] Text_Box;
    public GameObject Cursor;
    public Text[] Name;

    private string[] result;
    private string HOST = "113.198.229.227";
    private string[] table = { "Bullet_Score", "Hand_Score", "Stone_Score", "War_Score" };
    private int PORT = 4005;
    private bool top3RankImage;
    public int rankingNum;
    public int rankingIndex;
    int searchRankingNum;
    int firstNameNum;
    int secondNameNum;
    int thirdNameNum;
    int nameNum;
    public bool isSearch;
    bool isResult;

    float lastMoveTime = 0;
    float moveInterval = 0.1f;

    private void Awake()
    {
        SearchResults = new List<string>();
        rankingNum = 1;
        rankingIndex = 0;
        searchRankingNum = 0;
        top3RankImage = false;
        firstNameNum = 65;
        secondNameNum = 65;
        thirdNameNum = 65;
        nameNum = 1;
        isSearch = false;
        isResult = false;
    }
    private void Update()
    {
        if (isSearch) RankingSearch();
        else PageMove();
    }
    internal IEnumerator GetText(int i = 0) //DB를 통해 랭킹을 불러오는 함수
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
        Ranking();
    }
    void Split(string str2)
    {
        string replaceStr = str2.Replace("[", "");
        replaceStr = replaceStr.Replace("]", "");
        replaceStr = replaceStr.Replace("\"", "");
        result = replaceStr.Split(',');
    }
    void PageMove() // 랭킹 페이지를 이동하는 함수
    {
        if (!isSearch)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) && rankingNum + 10 <= result.Length / 2)
            {
                rankingNum += 10;
                rankingIndex += 20;
                DistoryAllProjectile();
                Ranking();
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && rankingNum - 10 > 0)
            {
                rankingNum -= 10;
                rankingIndex -= 20;
                DistoryAllProjectile();
                Ranking();
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                isSearch = true;
                transform.Find("SearchName").gameObject.SetActive(true);
            }
        }
    }
    void PageMove2()
    {
        if (!isSearch)
        {
            if (Input.GetKey(KeyCode.DownArrow) && rankingNum + 10 <= result.Length / 2 && Time.time - lastMoveTime > moveInterval)
            {
                rankingNum += 1;
                rankingIndex += 2;
                lastMoveTime = Time.time;
                DistoryAllProjectile();
                Ranking();
            }
            else if (Input.GetKey(KeyCode.UpArrow) && rankingNum - 1 > 0 && Time.time - lastMoveTime > moveInterval)
            {
                rankingNum -= 1;
                rankingIndex -= 2;
                lastMoveTime = Time.time;
                DistoryAllProjectile();
                Ranking();
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                isSearch = true;
                transform.Find("SearchName").gameObject.SetActive(true);
            }
        }
    } 
    internal void Ranking() // 결과를 화면에 출력하는 함수
    {
        // for (int i = rankingIndex, j = rankingNum; i < result.Length && result.Length != 1 && j < rankingNum + 10; i += 2, j++)
        // {
        //     GameObject Place;
        //     Place = Instantiate(Text_Box[0], transform.position, Quaternion.identity, GameObject.Find("Game_Ranking").transform);
        //     Place.GetComponent<RectTransform>().anchoredPosition = new Vector2(Text_Box[0].gameObject.transform.position.x * 100, Text_Box[0].gameObject.transform.position.y * 100 - 15 * (i - rankingIndex + 3));
        //     if (j < 4)
        //     {
        //         if (!top3RankImage) IsTop3(true);
        //         Place.GetComponent<Text>().text = " ";
        //     }
        //     else
        //     {
        //         if (top3RankImage && j > 10) IsTop3(false);
        //         Place.GetComponent<Text>().text = $"{j}";
        //     }
        //     Place.tag = "Rank_Place";
        //
        //     GameObject Name;
        //     Name = Instantiate(Text_Box[1], transform.position, Quaternion.identity, GameObject.Find("Game_Ranking").transform);
        //     Name.GetComponent<RectTransform>().anchoredPosition = new Vector2(Text_Box[1].gameObject.transform.position.x * 100, Text_Box[1].gameObject.transform.position.y * 100 - 15 * (i - rankingIndex + 3));
        //     Name.GetComponent<Text>().text = $"{result[i]}";
        //     Name.tag = "Rank_Name";
        //
        //     GameObject Score;
        //     Score = Instantiate(Text_Box[2], transform.position, Quaternion.identity, GameObject.Find("Game_Ranking").transform);
        //     Score.GetComponent<RectTransform>().anchoredPosition = new Vector2(Text_Box[2].gameObject.transform.position.x * 100, Text_Box[2].gameObject.transform.position.y * 100 - 15 * (i - rankingIndex + 3));
        //     Score.GetComponent<Text>().text = $"{result[i + 1]}";
        //     Score.tag = "Rank_Score";
        //
        //     if (j == 1)
        //     {
        //         Name.GetComponent<Text>().color = Color.red;
        //         Score.GetComponent<Text>().color = Color.red;
        //     }
        //     else if (j == 2)
        //     {
        //         Name.GetComponent<Text>().color = Color.blue;
        //         Score.GetComponent<Text>().color = Color.blue;
        //     }
        //     else if (j == 3)
        //     {
        //         Name.GetComponent<Text>().color = new Color32(18, 130, 0, 255);
        //         Score.GetComponent<Text>().color = new Color32(18, 130, 0, 255);
        //     }
        // }
    }
    int SelectName(int num)
    {
        if (!isResult)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                num++;
                if (num == 91) num = 97;
                else if (num == 123) num = 65;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                num--;
                if (num == 64) num = 122;
                else if (num == 96) num = 90;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && nameNum != 3)
            {
                nameNum++;
                Cursor.GetComponent<RectTransform>().anchoredPosition += new Vector2(60, 0);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && nameNum != 1)
            {
                nameNum--;
                Cursor.GetComponent<RectTransform>().anchoredPosition += new Vector2(-60, 0);
            }
        }
        return num;
    }
    string IntToString(int num)
    {
        return ((char)num).ToString();
    }
    void RankingSearch() // 랭킹을 검색하는 함수
    {
        Name[0].text = IntToString(firstNameNum);
        Name[1].text = IntToString(secondNameNum);
        Name[2].text = IntToString(thirdNameNum);
        string searchName = Name[0].text + Name[1].text + Name[2].text;

        switch (nameNum)
        {
            case 1:
                firstNameNum = SelectName(firstNameNum);
                break;
            case 2:
                secondNameNum = SelectName(secondNameNum);
                break;
            case 3:
                thirdNameNum = SelectName(thirdNameNum);
                break;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (GameObject.Find("First")) GameObject.Find("First").SetActive(false);
            if (GameObject.Find("Second")) GameObject.Find("Second").SetActive(false);
            if (GameObject.Find("Third")) GameObject.Find("Third").SetActive(false);
            top3RankImage = false;
            SearchResults.Clear();
            for (int i = 0; i < result.Length; i += 2)
            {
                if (result[i] == searchName)
                {
                    SearchResults.Add($"{i / 2 + 1}");      // Rank
                    SearchResults.Add(result[i + 1]);       // Score
                }
            }
            SearchResultScreen(searchName);
            return;
        }
        else if (Input.GetKeyDown(KeyCode.X) && !isResult)
        {
            isSearch = false;
            isResult = false;
            firstNameNum = 65;
            secondNameNum = 65;
            thirdNameNum = 65;
            DistoryAllProjectile();
            Ranking();
            GameObject.Find("SearchName").SetActive(false);
        }
        if (isResult)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                isResult = false;
                searchRankingNum = 0;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && SearchResults.Count > searchRankingNum + 20)
            {
                searchRankingNum += 20;
                SearchResultScreen(searchName);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && searchRankingNum - 20 >= 0)
            {
                searchRankingNum -= 20;
                SearchResultScreen(searchName);
            }
            // else if (Input.GetKey(KeyCode.DownArrow) && SearchResults.Count > searchRankingNum + 20 && Time.time - lastMoveTime > moveInterval)
            // {
            //     searchRankingNum += 2;
            //     lastMoveTime = Time.time;
            //     SearchResultScreen(searchName);
            // }
            // else if (Input.GetKey(KeyCode.UpArrow) && searchRankingNum - 2 >= 0 && Time.time - lastMoveTime > moveInterval)
            // {
            //     searchRankingNum -= 2;
            //     lastMoveTime = Time.time;
            //     SearchResultScreen(searchName);
            // }
        }
    }
    void SearchResultScreen(string searchName) // 결과를 화면에 출력하는 함수
    {
        DistoryAllProjectile();
        if (SearchResults.Count != 0)
        {
            if (SearchResults.Count > 10) isResult = true;
            for (int i = searchRankingNum; i < SearchResults.Count && i < searchRankingNum + 20; i += 2)
            {
                GameObject Place;
                Place = Instantiate(Text_Box[0], transform.position, Quaternion.identity, GameObject.Find("Game_Ranking").transform);
                Place.GetComponent<RectTransform>().anchoredPosition = new Vector2(Text_Box[0].gameObject.transform.position.x * 100, Text_Box[0].gameObject.transform.position.y * 100 - 15 * (i - searchRankingNum + 3));
                Place.GetComponent<Text>().text = $"{SearchResults[i]}";
                Place.tag = "Rank_Place";

                GameObject Name;
                Name = Instantiate(Text_Box[1], transform.position, Quaternion.identity, GameObject.Find("Game_Ranking").transform);
                Name.GetComponent<RectTransform>().anchoredPosition = new Vector2(Text_Box[1].gameObject.transform.position.x * 100, Text_Box[1].gameObject.transform.position.y * 100 - 15 * (i - searchRankingNum + 3));
                Name.GetComponent<Text>().text = $"{searchName}";
                Name.tag = "Rank_Name";

                GameObject Score;
                Score = Instantiate(Text_Box[2], transform.position, Quaternion.identity, GameObject.Find("Game_Ranking").transform);
                Score.GetComponent<RectTransform>().anchoredPosition = new Vector2(Text_Box[2].gameObject.transform.position.x * 100, Text_Box[2].gameObject.transform.position.y * 100 - 15 * (i - searchRankingNum + 3));
                Score.GetComponent<Text>().text = $"{SearchResults[i + 1]}";
                Score.tag = "Rank_Score";

                if (i == 0)
                {
                    Place.GetComponent<Text>().color = Color.red;
                    Name.GetComponent<Text>().color = Color.red;
                    Score.GetComponent<Text>().color = Color.red;
                }
                else if (i == 2)
                {
                    Place.GetComponent<Text>().color = Color.blue;
                    Name.GetComponent<Text>().color = Color.blue;
                    Score.GetComponent<Text>().color = Color.blue;
                }
                else if (i == 4)
                {
                    Place.GetComponent<Text>().color = new Color32(18, 130, 0, 255);
                    Name.GetComponent<Text>().color = new Color32(18, 130, 0, 255);
                    Score.GetComponent<Text>().color = new Color32(18, 130, 0, 255);
                }
            }
        }
        else
        {
            GameObject Name;
            Name = Instantiate(Text_Box[1], transform.position, Quaternion.identity, GameObject.Find("Game_Ranking").transform);
            Name.GetComponent<RectTransform>().anchoredPosition = new Vector2(Text_Box[1].gameObject.transform.position.x * 100, Text_Box[1].gameObject.transform.position.y * 100 - 15 * 3);
            Name.GetComponent<Text>().text = "Nothing";
            Name.tag = "Rank_Name";
        }
    }
    void IsTop3(bool isTop3)
    {
        if (isTop3)
        {
            transform.Find("First").gameObject.SetActive(true);
            transform.Find("Second").gameObject.SetActive(true);
            transform.Find("Third").gameObject.SetActive(true);
            top3RankImage = true;
        }
        else
        {
            if (GameObject.Find("First")) GameObject.Find("First").SetActive(false);
            if (GameObject.Find("Second")) GameObject.Find("Second").SetActive(false);
            if (GameObject.Find("Third")) GameObject.Find("Third").SetActive(false);
            top3RankImage = false;
        }
    }
    void DistoryProjectile(string Projectile_Name)  // Projectile_Name 이름의 오브젝트를 삭제하는 함수
    {
        GameObject[] Projectile = GameObject.FindGameObjectsWithTag($"{Projectile_Name}");
        foreach (GameObject items in Projectile)
        {
            Destroy(items);
        }
    }

    internal void DistoryAllProjectile()    // 화면의 랭킹을 삭제하는 함수
    {
        string name;

        name = "Rank_Place";
        DistoryProjectile(name);
        name = "Rank_Name";
        DistoryProjectile(name);
        name = "Rank_Score";
        DistoryProjectile(name);
    }
}

