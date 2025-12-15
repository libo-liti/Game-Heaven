using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class War_UI_DataInput : MonoBehaviour
{
    [SerializeField]
    Material material;
    Image[] image;
    RectTransform namePoint1;
    RectTransform namePoint2;
    Text text;
    Text firstName;
    Text middleName;
    Text lastName;
    bool isNameSelect;
    int nameNum;
    int firstNameNum;
    int middleNameNum;
    int lastNameNum;
    int cursor;
    private string HOST = "113.198.229.227";
    private string table = "War_Score";
    private int PORT = 4005;
    // Start is called before the first frame update
    void Start()
    {
        image = new Image[3];
        image[0] = transform.GetChild(3).GetComponent<Image>();     // Save
        image[1] = transform.GetChild(4).GetComponent<Image>();     // restart
        image[2] = transform.GetChild(5).GetComponent<Image>();     // exit
        namePoint1 = transform.GetChild(6).GetChild(0).GetComponent<RectTransform>();
        namePoint2 = transform.GetChild(6).GetChild(1).GetComponent<RectTransform>();
        firstName = transform.GetChild(2).GetChild(2).GetComponent<Text>();
        middleName = transform.GetChild(2).GetChild(3).GetComponent<Text>();
        lastName = transform.GetChild(2).GetChild(4).GetComponent<Text>();
        nameNum = 0;
        text = transform.GetChild(1).GetChild(2).GetComponent<Text>();
        text.text = War_GameManager.instance.score.ToString();
        isNameSelect = true;
        firstName.text = "A";
        middleName.text = "A";
        lastName.text = "A";
        firstNameNum = 65;
        middleNameNum = 65;
        lastNameNum = 65;
        cursor = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isNameSelect)
            Name();
        else
            Cursor();
    }
    void Name()
    {
        switch (nameNum)         // Canvas�� �̸� ������ text�� ����
        {                       // text ���ڰ� string�� �޾Ƽ� string���� ��ȯ
            case 0:
                firstName.text = IntToString(firstNameNum);
                firstNameNum = SelectName(firstNameNum);
                namePoint1.anchoredPosition = new Vector3(-11, -22, -400);
                namePoint2.anchoredPosition = new Vector3(-11, 12, -400);
                break;
            case 1:
                middleName.text = IntToString(middleNameNum);
                middleNameNum = SelectName(middleNameNum);
                namePoint1.anchoredPosition = new Vector3(20, -22, -400);
                namePoint2.anchoredPosition = new Vector3(20, 12, -400);
                break;
            case 2:
                lastName.text = IntToString(lastNameNum);
                lastNameNum = SelectName(lastNameNum);
                namePoint1.anchoredPosition = new Vector3(50, -22, -400);
                namePoint2.anchoredPosition = new Vector3(50, 12, -400);
                break;
        }
        if(Input.GetKeyDown(KeyCode.Z))
        {
            isNameSelect = false;
            namePoint1.gameObject.SetActive(false);
            namePoint2.gameObject.SetActive(false);
        }
    }
    int SelectName(int num)
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) num++;
        else if (Input.GetKeyDown(KeyCode.DownArrow)) num--;
        else if (Input.GetKeyDown(KeyCode.RightArrow) && nameNum < 2) nameNum++;
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && nameNum > 0) nameNum--;

        if (num == 123)
        {
            num = 65;
        }
        else if (num == 91)
        {
            num = 97;
        }
        else if (num == 96)
        {
            num = 90;
        }
        else if (num == 64)
        {
            num = 122;
        }
        return num;
    }
    string IntToString(int index)
    {
        return ((char)index).ToString();
    }
    void Cursor()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && cursor < 2) cursor++;
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && cursor > 0) cursor--;
        else if (Input.GetKeyDown(KeyCode.X))
        {
            isNameSelect = true;
            namePoint1.gameObject.SetActive(true);
            namePoint2.gameObject.SetActive(true);
            RemoveOutLine();
            return;
        }

        RemoveOutLine();
        switch (cursor)
        {
            case 0:
                image[0].material = material;
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    StartCoroutine(UnityWebRequestGETSend());
                    Time.timeScale = 1f;
                    SceneManager.LoadScene("War_Scene");
                }
                break;
            case 1:
                image[1].material = material;
                if (Input.GetKeyDown(KeyCode.Z)) { SceneManager.LoadScene("War_Scene"); Time.timeScale = 1.0f; }
                break;
            case 2:
                image[2].material = material;
                if (Input.GetKeyDown(KeyCode.Z)) SceneManager.LoadScene("Integration_Scene");
                break;
        }
    }
    void RemoveOutLine()
    {
        for (int i = 0; i < 3; i++)
            image[i].material = null;
    }
    IEnumerator UnityWebRequestGETSend()        // DB�� ���� �ֱ�
    {
        string name = firstName.text + middleName.text + lastName.text;
        float score = War_GameManager.instance.score;
        // GET ���
        string url = $"http://{HOST}:{PORT}/insert?table_name={table}&name={name}&score={score}";

        // UnityWebRequest�� ������ִ� GET �޼ҵ带 ����Ѵ�.
        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();  // ������ �ö����� ����Ѵ�.

        if (www.error == null)  // ������ ���� ������ ����.
        {
            Debug.Log(www.downloadHandler.text);
        }
        else
        {
            Debug.Log("error");
        }
    }
}
