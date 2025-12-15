using UnityEngine;
using UnityEngine.UI;

public class War_GameManager : MonoBehaviour
{
    public static War_GameManager instance;

    const int PLAYING = 0;      
    const int MENU = 1;         
    const int GAMEOVER = 2;     
        
    [HideInInspector]
    public int status;        
    [HideInInspector]
    public float score;     

    Text text;
    Transform canvas;
    Slider bgmSlider;
    Slider sfxSlider;
    War_SpawnBoss war_SpawnBoss;
    AudioSource audioSource;
    AudioSource laserSound;

    internal bool cantCount = true;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        bgmSlider = GameObject.Find("Canvas").transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Slider>();
        sfxSlider = GameObject.Find("Canvas").transform.GetChild(1).GetChild(2).GetChild(0).GetComponent<Slider>();
        war_SpawnBoss = GameObject.Find("Boss").GetComponent<War_SpawnBoss>();
        laserSound = GameObject.Find("Player").GetComponent<AudioSource>();
        canvas = GameObject.Find("Canvas").transform;
        text = GameObject.Find("Score_Num").GetComponent<Text>();

        status = PLAYING;
        score = 0;
    }

    void Update()
    {
        Audio();
        KeyInput();
        Score();
    }
    void Audio()        // 보스 나올때마다 소리 재생
    {
        switch (war_SpawnBoss.bossIndex)
        {
            case 0:
                if(GameObject.Find("Boss1"))
                    audioSource = GameObject.Find("Boss1").GetComponent<AudioSource>();
                break;
            case 1:
                if (GameObject.Find("Boss2"))
                    audioSource = GameObject.Find("Boss2").GetComponent<AudioSource>();
                break;
            case 2:
                if (GameObject.Find("Boss3"))
                    audioSource = GameObject.Find("Boss3").GetComponent<AudioSource>();
                break;
            case 3:
                if (GameObject.Find("Boss4"))
                    audioSource = GameObject.Find("Boss4").GetComponent<AudioSource>();
                break;
        }
        
        if(audioSource != null)
        {
            laserSound.volume = sfxSlider.value;
            audioSource.volume = bgmSlider.value;
        }
    }
    void Score()        // 점수
    {
        text.text = score.ToString();
    }
    void KeyInput()
    {
        if(Input.GetKeyDown(KeyCode.Q) && status == PLAYING && !cantCount)
        {
            Status(MENU);
            cantCount = true;
        }
        else if(Input.GetKeyDown(KeyCode.Q) && status == MENU)
        {
            Status(PLAYING);
        }
    }
    public void Status(int index)       // 현재 상태
    {
        switch(index)
        {
            case PLAYING:
                status = 0;
                canvas.GetChild(1).transform.gameObject.SetActive(false);
                GameObject.Find("GameManager").GetComponent<War_CountDownTimer>().CountDown();
                break;
            case MENU:
                status = 1;
                Time.timeScale = 0f;
                canvas.GetChild(1).transform.gameObject.SetActive(true);
                break;
            case GAMEOVER:
                status = 2;
                Time.timeScale = 0f;
                canvas.GetChild(2).transform.gameObject.SetActive(true);
                break;
        }
    }
}
