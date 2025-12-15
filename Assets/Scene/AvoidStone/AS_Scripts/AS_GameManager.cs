using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
//using System.Diagnostics;
public class AS_GameManager : MonoBehaviour
{
    public int nameNumpio = 0;// 마지막 이름 위치
    private int Goi = 0;//게임 오버 ui에서 선택시 어느 위치에 있는지 표현
    private int Di = 0;// 데이터 인풋 ui에서 선택시 어느 위치에 있는지 표현
    private int Pi = 0;// 퍼즈 ui에서 아래쪽 3개선택시 어느 위치에 있는지 표현
    private int Pui = 1;// 퍼즈 ui에서 위쪽 2개선택시 어느 위치에 있는지 표현
    private int Sandcheck = 0;// 데이터를 몇번 보냈는지 안보냈는지 체크
    public static AS_GameManager instance = null;// 게임 메니저에서 다른 함수로의 접근 용이를 위해
    [SerializeField]
    Material material;//메테리얼 효과 넣기
    [SerializeField]
    Image[] image;// 메테리얼 효과를 받는 이미지

    [SerializeField]
    public Slider BGMSlider;// 브금 볼륨 슬라이더
    public Slider SFXSlider;// 효과음 볼륨 슬라이더

    [SerializeField]
    private Text scoretext; // 스코어 텍스트 게임하는중 보이는 스코어
    [SerializeField]
    private Text GameOver_score;// 게임 오버ui에서 뜨는 스코어
    [SerializeField] private Text Datainput_score;// 데이터 인풋ui에서 뜨는 스코어
    public GameObject PipointObject;//퍼즈에서 화살표
    public GameObject NamePoint;// 이름 선택시 위치 표시 화살표

    public GameObject uiDatainputObject;// 데이터를 넣는 ui
    public GameObject uiPauseGameObject; // 게임 일시 정지 UI
    public GameObject uiOverGameObject; //게임 오버 ui
    public Text firstName; // 첫 번째 Text UI
    public Text middleName; // 두 번째 Text UI
    public Text lastName; // 세 번째 Text UI
    public int nameNum = 0;// 이름 입력시 어디 위치에 있는지
    public int index = 97;// 이름 첫자리 아스키 코드97로 시작
    public int index1 = 97;// 이름 두번째 자리 아스키 코드97로 시작
    public int index2 = 97;// 이름 세번째 자리 아스키 코드97로 시작
    public int score = 0;// 점수 0점으로 초기 설정
    public bool isGameOver = false;// 게임이 끝났는지 판단
    private bool DatainputObject;// 데이터 인풋 을 할건지 말건지 선택시 사용
    public bool isGamePaused = true; // 게임이 일시 정지 상태인지 여부

    private bool pudown;// 퍼즈 아래 선택 인지 아닌지
    public bool cantCount = true;//카운트 중인지 아닌지
                        // private bool Diup;// 데이터 인풋 ui에서 이름 선택인지 아닌지
    public int outLineIndex;// 아웃라인
    private AS_Sound soundManager;// 사운드 메니저 정의

    void Start()
    {
        BGMSlider.value = SoundData.control.BGM_Data;
        SFXSlider.value = SoundData.control.SFX_Data;
        uiOverGameObject.SetActive(false);// 시작시 아래 ui는 끄고 시작
        uiDatainputObject.SetActive(false);
        uiPauseGameObject.SetActive(false);
        DatainputObject = false;
        //Diup=true;
        isGamePaused = false;
        pudown = false;
        soundManager = GetComponent<AS_Sound>();
        image = new Image[9];// 메테리얼 받을 버튼 이미지 정의
        image[0] = GameObject.Find("Canvas").transform.GetChild(4).GetChild(1).GetComponent<Image>();       // continue
        image[1] = GameObject.Find("Canvas").transform.GetChild(4).GetChild(2).GetComponent<Image>();     // restart
        image[2] = GameObject.Find("Canvas").transform.GetChild(4).GetChild(3).GetComponent<Image>();// exit

        image[3] = GameObject.Find("Canvas").transform.GetChild(2).GetChild(2).GetComponent<Image>();       // open datainput ui
        image[4] = GameObject.Find("Canvas").transform.GetChild(2).GetChild(3).GetComponent<Image>();     // Gameover의 restart
        image[5] = GameObject.Find("Canvas").transform.GetChild(2).GetChild(4).GetComponent<Image>();       //  Gameover의exit

        image[6] = GameObject.Find("Canvas").transform.GetChild(3).GetChild(3).GetComponent<Image>();       // 데이터 인풋의continue
        image[7] = GameObject.Find("Canvas").transform.GetChild(3).GetChild(4).GetComponent<Image>();     // restart
        image[8] = GameObject.Find("Canvas").transform.GetChild(3).GetChild(5).GetComponent<Image>();       // exit

        if (soundManager == null)
        {
            soundManager = gameObject.AddComponent<AS_Sound>();
        }
        outLineIndex = 0;

        StartGame();// 게임 시작하는 함수
        //아래는 이름을 넣을때 각 자리에 대한 정의
        if (transform.childCount >= 1)
        {
            firstName = transform.GetChild(0).Find("FirstName").GetComponent<Text>();
        }
        if (transform.childCount >= 2)
        {
            middleName = transform.GetChild(1).Find("MiddleName").GetComponent<Text>();
        }
        if (transform.childCount >= 3)
        {
            lastName = transform.GetChild(2).Find("LastName").GetComponent<Text>();
        }
    }


    void Update()
    {
        if (isGameOver && DatainputObject == false)
        {
            uiOverGameObject.SetActive(true);
            GOSelection();
        }
        else if (isGameOver && DatainputObject == true && nameNum < 3)
        {
            uiDatainputObject.SetActive(true);
            Name();

        }
        else if (isGameOver && DatainputObject == true && nameNum >= 3)
        {
            NamePoint.SetActive(false);
            DISelection();

        }

        else if (isGameOver == false)
        {
            if (Input.GetKeyDown(KeyCode.Q) && isGamePaused == false && !cantCount)
            {
                PauseGame();
            }
            else if (Input.GetKeyDown(KeyCode.Q) && isGamePaused == true)
            {
                ResumeGame();
            }
            if (isGamePaused == true && pudown == false)
            {
                PUSelectionup();
            }
            else if (isGamePaused == true && pudown)
            {
                PUSelection();
            }
        }

    }
    public void Name()
    {
        switch (nameNum)         // Canvas에 미리 만들어둔 text를 수정
        {                       // text 상자가 string만 받아서 string으로 변환
            case 0:
                firstName.text = ((char)index).ToString();
                NamePoint.transform.position = new Vector3(-0.32f, -0.1f, 0f);
                break;
            case 1:
                middleName.text = ((char)index1).ToString();
                NamePoint.transform.position = new Vector3(-0.025f, -0.1f, 0f);
                break;
            case 2:
                lastName.text = ((char)index2).ToString();
                NamePoint.transform.position = new Vector3(0.28f, -0.1f, 0f);
                break;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))   // 방향키 위로하면 숫자 커짐 즉 알파벳 순서 뒤로
        {
            if (nameNum == 0)
            {
                index++;
                if (index == 91)
                {
                    index = 97;
                }
                else if (index == 123)
                {
                    index = 65;
                }
            }
            if (nameNum == 1)
            {
                index1++;
                if (index1 == 91)
                {
                    index1 = 97;
                }
                else if (index1 == 123)
                {
                    index1 = 65;
                }
            }
            if (nameNum == 2)
            {
                index2++;
                if (index2 == 91)
                {
                    index2 = 97;
                }
                else if (index2 == 123)
                {
                    index2 = 65;
                }
            }

        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (nameNum == 0)
            {
                index--;
                if (index == 64)
                {
                    index = 122;
                }
                else if (index == 96)
                {
                    index = 90;
                }

            }
            if (nameNum == 1)
            {
                index1--;
                if (index1 == 64)
                {
                    index1 = 122;
                }
                else if (index1 == 96)
                {
                    index1 = 90;
                }
            }
            if (nameNum == 2)
            {
                index2--;
                if (index2 == 64)
                {
                    index2 = 122;
                }
                else if (index2 == 96)
                {
                    index2 = 90;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            nameNum++;

            if (nameNum == 3)
            {
                nameNum = 2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            nameNum--;
            if (nameNum == -1)
            {
                nameNum = 0;
            }

        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            nameNumpio = nameNum;
            nameNum = 4;
        }

    }

    public string GetPlayerName()//이름을 다 모으는 함수
    {
        string name = firstName.text + middleName.text + lastName.text;
        return name;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        // 게임 시작 시 일시 정지 상태로 설정
    }

    public void StartGame()//게임 시작 함수
    {
        Debug.Log("게임 시작!");

        isGamePaused = false;// 게임 퍼즈가 아니라는걸 설정
        Time.timeScale = 1f; // 게임 시간 스케일을 원래 속도(1x)로 설정
    }

    public void PauseGame()// 게임 퍼즈 함수
    {
        Debug.Log("게임 일시 정지");

        isGamePaused = true;
        cantCount = true;
        Time.timeScale = 0f; // 게임 시간 스케일을 멈춤(0x)
        uiPauseGameObject.SetActive(true); // 게임 일시 정지 UI를 활성화


    }
    public void ResumeGame()// 게임 재개 함수 퍼즈에서 재개
    {
        Debug.Log("게임 재개");
        isGamePaused = false;
        Time.timeScale = 0;
        uiPauseGameObject.SetActive(false); // 게임 일시 정지 UI를 비활성화
        GameObject.Find("GameManager").GetComponent<AS_CountDownTimer>().CountDown();
    }

    public void ExitGame()// 게임 종료 함수
    {
        Debug.Log("게임 종료");
        SceneManager.LoadScene("Integration_Scene");
    }
    public void RestartGame()//게임 재시작 함수
    {
        // 게임 재시작을 위해 씬을 다시 로드합니다.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f; // 게임 시간 스케일을 원래 속도(1x)로 설정
    }

    public void SandData()// 데이터 보내는 함수
    {
        SendScoreToDatabase(AS_GameManager.instance.score);
        Sandcheck++;
    }
    private void SendScoreToDatabase(int score)// 데이터를 DB로 보내는 함수
    {
        // 여기에 점수를 데이터베이스로 보내는 코드

        string playerName = GetPlayerName(); // 플레이어 이름을 설정
        string url = "http://113.198.229.227:4005/insert?table_name=Stone_Score&name=" + playerName + "&score=" + score;

        UnityWebRequest www = UnityWebRequest.Get(url);

        StartCoroutine(SendScoreCoroutine(www));
    }

    private IEnumerator SendScoreCoroutine(UnityWebRequest www)// 데이터가 들어갔는지 확인
    {
        yield return www.SendWebRequest();

        if (www.error == null)
        {
            Debug.Log("Score sent successfully: " + www.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Error sending score: " + www.error);
        }
    }
    public void IncreaseScore()// 스코어 증가 함수
    {
        score += 100; // 100씩 증가하도록 수정
        UpdateScoreText();
        UpdatGameoverScoreText();
        Update_datainput_ScoreText();
    }

    public void DecreaseScore()// 스코어 감소 함수
    {
        score -= 100; // 300씩 감소하도록 수정
        UpdateScoreText();
        UpdatGameoverScoreText();
        Update_datainput_ScoreText();
    }

    private void UpdateScoreText()// 게임중 스코어를 나타내주는 함수
    {
        if (scoretext != null)
        {
            scoretext.text = score.ToString();
        }
        else
        {
            Debug.LogError("Text field is not assigned in AS_GameManager.");
        }
    }
    private void UpdatGameoverScoreText()// 게임이 끝났을때 게임오버ui에서 스코어를 나타내주는 함수
    {
        if (GameOver_score != null)
        {
            GameOver_score.text = score.ToString();
        }
        else
        {
            Debug.LogError("GameOver_score Text field is not assigned in AS_GameManager.");
        }
    }
    private void Update_datainput_ScoreText()// 데이터 인풋ui에서 스코어를 나타내주는 함수
    {
        if (Datainput_score != null)
        {
            Datainput_score.text = score.ToString();
        }
        else
        {
            Debug.LogError("GameOver_score Text field is not assigned in AS_GameManager.");
        }
    }

    void GOSelection()
    {// 게임오버시 게임오버 ui에서 선택하는 부분
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Goi--;
            if (Goi < 0)
            {
                Goi = 2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Goi++;
            if (Goi > 2)
            {
                Goi = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            switch (Goi)
            {
                case 2:
                    ExitGame();
                    break;
                case 1:
                    RestartGame();
                    break;
                case 0:
                    uiOverGameObject.SetActive(false);
                    DatainputObject = true;
                    break;
            }
        }
        switch (Goi)
        {
            case 2:
                image[3].material = null;
                image[4].material = null;
                image[5].material = material;
                break;
            case 1:
                image[3].material = null;
                image[4].material = material;
                image[5].material = null;
                break;
            case 0:
                image[3].material = material;
                image[4].material = null;
                image[5].material = null;
                break;
        }
    }
    void RemoveOutLine()// 퍼즈의ui에서 아웃라인 한번에 지우는것
    {
        for (int i = 0; i < 3; i++)
            image[i].material = null;
    }

    void DISelection()
    {// 데이터 인풋ui에서 선택하는 함수
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Di--;
            if (Di < 0)
            {
                Di = 2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Di++;
            if (Di > 2)
            {
                Di = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))//Z를 눌러 선택
        {
            switch (Di)
            {
                case 2:
                    ExitGame();
                    break;
                case 1:
                    RestartGame();
                    break;
                case 0:
                    SandData();
                    RestartGame();
                    break;
            }
        }


        switch (Di)
        {
            case 2:
                image[6].material = null;
                image[7].material = null;
                image[8].material = material;
                break;
            case 1:
                image[6].material = null;
                image[7].material = material;
                image[8].material = null;
                break;
            case 0:
                image[6].material = material;
                image[7].material = null;
                image[8].material = null;
                break;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {

            uiDatainputObject.SetActive(true);
            NamePoint.SetActive(true);
            DatainputObject = true;
            image[6].material = null;
            image[7].material = null;
            image[8].material = null;
            nameNum = nameNumpio;
        }

    }
    void PUSelection()
    {// 퍼즈 상태에서 선택하는 함수
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Pi--;
            if (Pi < 0)
            {
                Pi = 2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Pi++;
            if (Pi > 2)
            {
                Pi = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            pudown = false;
            PipointObject.SetActive(true);

            PUSelectionup();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            switch (Pi)
            {
                case 2:
                    ExitGame();
                    break;
                case 1:
                    RestartGame();
                    break;
                case 0:
                    ResumeGame();
                    break;
            }
        }
        switch (Pi)//y값은 다 -102
        {
            case 2://55
                image[2].material = material;
                image[1].material = null;
                image[0].material = null;
                if (pudown == false) RemoveOutLine();
                break;
            case 1://-48
                image[2].material = null;
                image[1].material = material;
                image[0].material = null;
                if (pudown == false) RemoveOutLine();
                break;
            case 0://-143
                image[2].material = null;
                image[1].material = null;
                image[0].material = material;
                if (pudown == false) RemoveOutLine();
                break;
        }
    }
    void PUSelectionup()
    {// 퍼즈 상태에서 위에 음량조절 선택함수
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Pui--;
            if (Pui < 0)
            {
                pudown = true;
                PipointObject.SetActive(false);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Pui++;
            if (Pui > 1)
            {
                Pui = 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))//사운드 업
        {
            switch (Pui)
            {
                case 1:
                    IncreaseBackgroundMusicLevel();
                    break;
                case 0:
                    IncreaseEffectSoundLevel();
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))//사운드 다운
        {
            switch (Pui)
            {
                case 1:
                    DecreaseBackgroundMusicLevel();
                    break;
                case 0:
                    DecreaseEffectSoundLevel();
                    break;
            }
        }
        switch (Pui)//y값은 다 -102
        {
            case 1://-48
                PipointObject.transform.position = new Vector3(-1.10f, 0.70f, 0.0f);
                break;
            case 0://-143
                PipointObject.transform.position = new Vector3(-1.10f, 0.20f, 0.0f);
                break;
        }

    }
    void IncreaseBackgroundMusicLevel()// 배경음악을 증가하는 함수
    {
        BGMSlider.value += 0.2f;
        soundManager.IncreaseBackgroundMusicLevel();
    }

    void DecreaseBackgroundMusicLevel()// 배경음악 감소 함수
    {
        BGMSlider.value -= 0.2f;
        soundManager.DecreaseBackgroundMusicLevel();
    }

    void IncreaseEffectSoundLevel()// 효과음 증가 함수
    {
        SFXSlider.value += 0.2f;
        soundManager.IncreaseEffectSoundLevel();
    }

    void DecreaseEffectSoundLevel() // 효과음 감소 함수
    {
        SFXSlider.value -= 0.2f;
        soundManager.DecreaseEffectSoundLevel();
    }
}