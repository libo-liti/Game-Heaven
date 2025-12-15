using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class War_UI_GameOver : MonoBehaviour
{
    [SerializeField]
    Material material;
    Image[] image;
    Text text;
    int cursor;
    bool isSelect;
    // Start is called before the first frame update
    void Start()
    {
        image = new Image[3];
        image[0] = transform.GetChild(2).GetComponent<Image>();     // continue
        image[1] = transform.GetChild(3).GetComponent<Image>();     // restart
        image[2] = transform.GetChild(4).GetComponent<Image>();     // exit
        text = transform.GetChild(1).GetChild(2).GetComponent<Text>();
        text.text = War_GameManager.instance.score.ToString();
        cursor = 0;
        isSelect = false;
        StartCoroutine(Select());
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelect)
        {
            Cursor();
        }
    }
    void Cursor()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && cursor < 2) cursor++;
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && cursor > 0) cursor--;

        RemoveOutLine();
        switch (cursor)
        {
            case 0:
                image[0].material = material;
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    GameObject.Find("Canvas").transform.GetChild(3).gameObject.SetActive(true);
                    gameObject.SetActive(false);
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
    IEnumerator Select()
    {
        yield return new WaitForSecondsRealtime(1f);
        isSelect = true;
    }
}
