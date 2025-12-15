using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class War_UI_Pause : MonoBehaviour
{
    [SerializeField]
    Material material;
    Image[] image;
    RectTransform rectTransform;
    GameObject cursor;
    Slider bgmSlider;
    Slider sfxSlider;
    float startTime;
    int cursorIndex;
    int outLineIndex;
    // Start is called before the first frame update
    void Start()
    {
        image = new Image[3];
        image[0] = transform.GetChild(3).GetComponent<Image>();     // continue
        image[1] = transform.GetChild(4).GetComponent<Image>();     // restart
        image[2] = transform.GetChild(5).GetComponent<Image>();     // exit
        rectTransform = transform.GetChild(6).GetComponent<RectTransform>();
        cursor = transform.GetChild(6).gameObject;
        bgmSlider = transform.GetChild(1).GetChild(0).GetComponent<Slider>();
        sfxSlider = transform.GetChild(2).GetChild(0).GetComponent<Slider>();
        cursorIndex = 2;
        outLineIndex = 0;
        bgmSlider.value = SoundData.control.BGM_Data;
        sfxSlider.value = SoundData.control.SFX_Data;
    }

    // Update is called once per frame
    void Update()
    {
        Select();
    }
    void Select()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && cursorIndex > 0)
            cursorIndex--;
        else if (Input.GetKeyDown(KeyCode.UpArrow) && cursorIndex < 2)
            cursorIndex++;

        switch (cursorIndex)
        {
            case 2:
                rectTransform.anchoredPosition = new Vector3(-110, 70, -400);
                if (Input.GetKeyDown(KeyCode.LeftArrow) && bgmSlider.value > 0) bgmSlider.value -= 0.2f;
                else if (Input.GetKeyDown(KeyCode.RightArrow) && bgmSlider.value < 1) bgmSlider.value += 0.2f;
                SoundData.control.ChangeSound(bgmSlider.value, sfxSlider.value);
                break;
            case 1:
                cursor.SetActive(true);
                RemoveOutLine();
                if (Input.GetKeyDown(KeyCode.LeftArrow) && sfxSlider.value > 0) sfxSlider.value -= 0.2f;
                else if (Input.GetKeyDown(KeyCode.RightArrow) && sfxSlider.value < 1) sfxSlider.value += 0.2f;
                SoundData.control.ChangeSound(bgmSlider.value, sfxSlider.value);
                rectTransform.anchoredPosition = new Vector3(-110, 20, -400);
                break;
            case 0:
                if (cursor) cursor.SetActive(false);
                break;
        }

        if (!cursor.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) && outLineIndex < 2) outLineIndex++;
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && outLineIndex > 0) outLineIndex--;

            RemoveOutLine();
            switch (outLineIndex)
            {
                case 0:
                    image[0].material = material;
                    if (Input.GetKeyDown(KeyCode.Z)) War_GameManager.instance.Status(0);
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
    }
    void RemoveOutLine()
    {
        for (int i = 0; i < 3; i++)
            image[i].material = null;
    }
}
