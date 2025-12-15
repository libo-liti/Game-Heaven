using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Hand_UI : MonoBehaviour
{
    public bool isGameover = false; // 
    public bool isDataInput = false; //
    public bool isInitializing = false; //
    public bool isPause = false; // 
    public bool hasExecuted = false;
    public bool selectContinue = true;
    public bool selectRestart = false;
    public bool selectEndGame = false;
    public bool selectInitial1 = true;
    public bool selectInitial2 = false;
    public bool selectInitial3 = false;

    public bool pauseBGM = false;
    public bool pauseSFX = false;
    public bool pauseContinue = true;
    public bool pauseRestart = false;
    public bool pauseQuit = false;
    public bool pauseTempoContinue = true;
    public bool pauseTempoRestart = false;
    public bool pauseTempoQuit = false;
    
    public bool gameoverDataInput = true; // 통
    public bool gameoverRestart = false; // 통
    public bool gameoverQuit = false; // 통
    public bool zKeyProcessed = false;
    public bool hasFunctionRun =false;
    public bool isAfterInitializing = false;

    public bool dataInputSave = true; //
    public bool dataInputRestart = false; //
    public bool dataInputQuit = false; // 

    public Material newMaterials;

    public Text scoreText; // 점수를 출력할 UI 텍스트
    public Text scoreText2;
    public Text scoreText3;

    public GameObject gameoverUI; // 
    public GameObject dataInputUI; //
    public GameObject pauseUI;
    public GameObject inGameUI;
    public GameObject BGMGauge;
    public GameObject SFXGauge;
    public GameObject puaseArrow;
    public GameObject initialArrow1;//
    public GameObject initialArrow2;//
    
    public Text initial1;
    public Text initial2;
    public Text initial3;
    int[] initialNum = new int[3]; 

    GameObject obj;

    private void Start()
    {
        // 초기화
        initialNum[0] = 65;
        initialNum[1] = 65;
        initialNum[2] = 65;
        inGameUI.SetActive(true);
        obj = GameObject.Find("Hand_GameManager");
        obj.GetComponent<Hand_Audio>().BGMvolumesetting();
        obj = GameObject.Find("Hand_Player");
        obj.GetComponent<Hand_Audio>().SFXvolumesetting();
    }

    void Update()
    {
        ScoreUI();
        PauseUI();
        GameoverUI();
        DataInputUI();
    }

    // 점수를 표기하는 UI - 통과
    public void ScoreUI()
    {
        int score = Hand_GameManager.instance.score;
        scoreText.text = "" + score + ""; 
        scoreText2.text = "" + score + ""; 
        scoreText3.text = "" + score + "";
    }

    ///////////////////////////////////////// 일시정지 UI /////////////////////////////////////////

    // 일시정지 UI 동작 처리
    public void PauseUI(){
        // 일시정지 진입
        if (Hand_GameManager.instance.isPause == true)
        {
            pauseUI.SetActive(true);
            Time.timeScale = 0;
            PauseSelectMark();
            PauseSelectMove();
            PauseSelectResult();
            PauseBGMBar();
            PauseSFXBar();
        }
        // 일시정지 탈출
        else if (Hand_GameManager.instance.isPause == false && Hand_GameManager.instance.isCountDown == false && Hand_GameManager.instance.hasExecuted == false)
        {
            pauseUI.SetActive(false);
        }
    }
        
    // 일시정지 UI - 선택지 이동 
    public void PauseSelectMove()
    {
        if (pauseBGM == true && Input.GetKeyDown(KeyCode.DownArrow))
            {
                pauseSFX = true;
                pauseBGM = false;
            }
        else if (pauseSFX == true && Input.GetKeyDown(KeyCode.UpArrow)){
                pauseBGM = true;
                pauseSFX = false;
            }
        else if (pauseSFX == true && pauseTempoContinue == true && Input.GetKeyDown(KeyCode.DownArrow)){
                pauseContinue = true;
                pauseSFX = false;
            }
        else if (pauseSFX == true && pauseTempoRestart == true && Input.GetKeyDown(KeyCode.DownArrow)){
                pauseRestart = true;
                pauseSFX = false;
            }
        else if (pauseSFX == true && pauseTempoQuit == true && Input.GetKeyDown(KeyCode.DownArrow)){
                pauseQuit = true;
                pauseSFX = false;
            }
        else if (pauseContinue == true && Input.GetKeyDown(KeyCode.UpArrow)){
                pauseSFX = true;
                pauseContinue = false;
                pauseTempoContinue = true;
                pauseTempoRestart = false;
                pauseTempoQuit = false;
            }
        else if (pauseContinue == true && Input.GetKeyDown(KeyCode.RightArrow)){
                pauseRestart = true;
                pauseContinue = false;
            }
        else if (pauseRestart == true && Input.GetKeyDown(KeyCode.UpArrow)){
                pauseSFX = true;
                pauseRestart = false;
                pauseTempoContinue = false;
                pauseTempoRestart = true;
                pauseTempoQuit = false;
            }
        else if (pauseRestart == true && Input.GetKeyDown(KeyCode.LeftArrow)){
                pauseContinue = true;
                pauseRestart = false;
            }
        else if (pauseRestart == true && Input.GetKeyDown(KeyCode.RightArrow)){
                pauseQuit = true;
                pauseRestart = false;
            }
        else if (pauseQuit == true && Input.GetKeyDown(KeyCode.UpArrow)){
                pauseSFX = true;
                pauseQuit = false;
                pauseTempoContinue = false;
                pauseTempoRestart = false;
                pauseTempoQuit = true;
            }
        else if (pauseQuit == true && Input.GetKeyDown(KeyCode.LeftArrow)){
                pauseRestart = true;
                pauseQuit = false;
        }
    }

    // 일시정지 UI - 선택지 표기
    public void PauseSelectMark()
{
    Transform uiPauseTransform = GameObject.Find("Canvas/UI_Pause").transform;
    RectTransform rt3 = puaseArrow.GetComponent<RectTransform>();

    if (pauseBGM == true){
        // BGM 관련 코드
        rt3.anchoredPosition = new Vector2(rt3.anchoredPosition.x, 69);
    }
    if (pauseSFX == true){
        // SFX 관련 코드
        rt3.anchoredPosition = new Vector2(rt3.anchoredPosition.x, 20);
    }
    if(pauseBGM == false && pauseSFX == false){
        rt3.anchoredPosition = new Vector2(rt3.anchoredPosition.x, -1000);
    }
    if (pauseContinue == true){
        obj = uiPauseTransform.Find("Button_Continue").gameObject;
        ChangeMaterial(obj, newMaterials);
    }
    if (pauseContinue == false){
        obj = uiPauseTransform.Find("Button_Continue").gameObject;
        ResetMaterial(obj);
    }
    if (pauseRestart == true){
        obj = uiPauseTransform.Find("Button_ReStart").gameObject;
        ChangeMaterial(obj, newMaterials);
    }
    if (pauseRestart == false){
        obj = uiPauseTransform.Find("Button_ReStart").gameObject;
        ResetMaterial(obj);
    }
    if (pauseQuit == true){
        obj = uiPauseTransform.Find("Button_Exit").gameObject;
        ChangeMaterial(obj, newMaterials);
    }
    if (pauseQuit == false){
        obj = uiPauseTransform.Find("Button_Exit").gameObject;
        ResetMaterial(obj);
    }
}

public void ChangeMaterial(GameObject obj, Material newMaterial)
{
    Image img = obj.GetComponent<Image>();
    if (img != null)
    {
        img.material = newMaterials;
    }
}

public void ResetMaterial(GameObject obj)
{
    Image img = obj.GetComponent<Image>();
    if (img != null)
    {
        img.material = null; // 기본 Material로 설정
    }
}

    // 일시정지 UI - 선택지 실행
    public void PauseSelectResult()
    {
        RectTransform SFXgauge = SFXGauge.GetComponent<RectTransform>();

        if(pauseBGM == true && Input.GetKeyDown(KeyCode.LeftArrow)){
            obj = GameObject.Find("Hand_GameManager");
            obj.GetComponent<Hand_Audio>().type = 1;
            obj.GetComponent<Hand_Audio>().VolDown();
        }
        else if(pauseBGM == true && Input.GetKeyDown(KeyCode.RightArrow)){
            obj = GameObject.Find("Hand_GameManager");
            obj.GetComponent<Hand_Audio>().type = 1;
            obj.GetComponent<Hand_Audio>().VolUp();
        }
        else if(pauseSFX == true && Input.GetKeyDown(KeyCode.LeftArrow)){
            if(SFXgauge.offsetMin.x < -414){
                return;
            }
            else{
            SFXgauge.offsetMin = new Vector2(SFXgauge.offsetMin.x - 102, SFXgauge.offsetMin.y);
            SFXgauge.offsetMax = new Vector2(SFXgauge.offsetMax.x - 102, SFXgauge.offsetMax.y);
            }
            obj = GameObject.Find("Hand_Player");
            obj.GetComponent<Hand_Audio>().type = 2;
            obj.GetComponent<Hand_Audio>().VolDown();
        }
        else if(pauseSFX == true && Input.GetKeyDown(KeyCode.RightArrow)){
            if(SFXgauge.offsetMin.x > 403){
                return;
            }
            else{
            SFXgauge.offsetMin = new Vector2(SFXgauge.offsetMin.x + 102, SFXgauge.offsetMin.y);
            SFXgauge.offsetMax = new Vector2(SFXgauge.offsetMax.x + 102, SFXgauge.offsetMax.y);
            }
            obj = GameObject.Find("Hand_Player");
            obj.GetComponent<Hand_Audio>().type = 2;
            obj.GetComponent<Hand_Audio>().VolUp();
        }
        else if (pauseContinue == true && Input.GetKeyDown(KeyCode.Z))
        {
            Hand_GameManager.instance.isPause = false;
            obj = GameObject.Find("Hand_UI");
            obj.GetComponent<CountDownTimer>().PauseCountDown();
        }
        else if (pauseRestart == true && Input.GetKeyDown(KeyCode.Z))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (pauseQuit == true && Input.GetKeyDown(KeyCode.Z))
        {
            SceneManager.LoadScene("Integration_Scene");
            // #if UNITY_EDITOR
            // // 에디터에서는 에디터를 종료
            // UnityEditor.EditorApplication.isPlaying = false;
            // #else
            // // 빌드된 게임에서는 어플리케이션을 종료
            // Application.Quit();
            // #endif
        }
    }
    public void PauseBGMBar(){
    RectTransform BGMgauge = BGMGauge.GetComponent<RectTransform>();

    obj = GameObject.Find("Hand_GameManager");
    float volume = obj.GetComponent<AudioSource>().volume;

    // 볼륨을 0.2의 배수로 반올림
    float roundedVolume = Mathf.Round(volume * 5f) / 5f;

    switch(roundedVolume){
        case 1.0f:
            BGMgauge.offsetMin = new Vector2(-1, BGMgauge.offsetMin.y);
            BGMgauge.offsetMax = new Vector2(-1, BGMgauge.offsetMax.y);
            break;
        case 0.8f:
            BGMgauge.offsetMin = new Vector2(-103, BGMgauge.offsetMin.y);
            BGMgauge.offsetMax = new Vector2(-103, BGMgauge.offsetMax.y);
            break;
        case 0.6f:
            BGMgauge.offsetMin = new Vector2(-207, BGMgauge.offsetMin.y);
            BGMgauge.offsetMax = new Vector2(-207, BGMgauge.offsetMax.y);
            break;
        case 0.4f:
            BGMgauge.offsetMin = new Vector2(-309, BGMgauge.offsetMin.y);
            BGMgauge.offsetMax = new Vector2(-309, BGMgauge.offsetMax.y);
            break;
        case 0.2f:
            BGMgauge.offsetMin = new Vector2(-413, BGMgauge.offsetMin.y);
            BGMgauge.offsetMax = new Vector2(-413, BGMgauge.offsetMax.y);
            break;
        case 0.0f:
            BGMgauge.offsetMin = new Vector2(-513, BGMgauge.offsetMin.y);
            BGMgauge.offsetMax = new Vector2(-513, BGMgauge.offsetMax.y);
            break;
    }
}

public void PauseSFXBar(){
    RectTransform SFXgauge = SFXGauge.GetComponent<RectTransform>();

    obj = GameObject.Find("Hand_Player");
    float volume = obj.GetComponent<AudioSource>().volume;

    // 볼륨을 0.2의 배수로 반올림
    float roundedVolume = Mathf.Round(volume * 5f) / 5f;

    switch(roundedVolume){
        case 1.0f:
            SFXgauge.offsetMin = new Vector2(-1, SFXgauge.offsetMin.y);
            SFXgauge.offsetMax = new Vector2(-1, SFXgauge.offsetMax.y);
            break;
        case 0.8f:
            SFXgauge.offsetMin = new Vector2(-103, SFXgauge.offsetMin.y);
            SFXgauge.offsetMax = new Vector2(-103, SFXgauge.offsetMax.y);
            break;
        case 0.6f:
            SFXgauge.offsetMin = new Vector2(-209, SFXgauge.offsetMin.y);
            SFXgauge.offsetMax = new Vector2(-209, SFXgauge.offsetMax.y);
            break;
        case 0.4f:
            SFXgauge.offsetMin = new Vector2(-311, SFXgauge.offsetMin.y);
            SFXgauge.offsetMax = new Vector2(-311, SFXgauge.offsetMax.y);
            break;
        case 0.2f:
            SFXgauge.offsetMin = new Vector2(-413, SFXgauge.offsetMin.y);
            SFXgauge.offsetMax = new Vector2(-413, SFXgauge.offsetMax.y);
            break;
        case 0.0f:
            SFXgauge.offsetMin = new Vector2(-515, SFXgauge.offsetMin.y);
            SFXgauge.offsetMax = new Vector2(-515, SFXgauge.offsetMax.y);
            break;
    }
}



    ///////////////////////////////////////// 게임오버 UI /////////////////////////////////////////
    
    // 게임오버 UI 동작 처리
    public void GameoverUI()
    {
        if(isGameover == true){
            gameoverUI.SetActive(true);
            GameoverSelectMark();
            GameoverSelectMove();
            GameoverSelectResult();
        }
        
    }

    // 게임오버 UI - 선택지 이동 
    public void GameoverSelectMove(){
        if(isGameover == true){
            if(isDataInput == false){
                if (gameoverDataInput == true && Input.GetKeyDown(KeyCode.RightArrow))
                {
                    gameoverRestart = true;
                    gameoverDataInput = false;
                }
                else if (gameoverRestart == true && Input.GetKeyDown(KeyCode.LeftArrow)){
                        gameoverDataInput = true;
                        gameoverRestart = false;
                }
                else if (gameoverRestart == true && Input.GetKeyDown(KeyCode.RightArrow)){
                        gameoverQuit = true;
                        gameoverRestart = false;
                }
                else if (gameoverQuit == true && Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    gameoverRestart = true;
                    gameoverQuit = false;
                }
            }
        }
    }

    // 게임오버 UI - 선택지 표기
    public void GameoverSelectMark(){
        Transform uiGameOverTransform = GameObject.Find("Canvas/UI_GameOver").transform;
        if (isGameover == true){
            if (gameoverDataInput == true){
                obj = uiGameOverTransform.Find("Button_UI_Open_DataInput").gameObject;
                ChangeMaterial(obj, newMaterials);
            }
            if (gameoverDataInput == false){
                obj = uiGameOverTransform.Find("Button_UI_Open_DataInput").gameObject;
                ResetMaterial(obj);
            }
            if (gameoverRestart == true){
                obj = uiGameOverTransform.Find("Button_ReStart").gameObject;
                ChangeMaterial(obj, newMaterials);
            }
            if (gameoverRestart == false){
                obj = uiGameOverTransform.Find("Button_ReStart").gameObject;
                ResetMaterial(obj);
            }
            if (gameoverQuit == true){
                obj = uiGameOverTransform.Find("Button_Exit").gameObject;
                ChangeMaterial(obj, newMaterials);
            }
            if (gameoverQuit == false){
                obj = uiGameOverTransform.Find("Button_Exit").gameObject;
                ResetMaterial(obj);
            }
        }
    }

    IEnumerator DelayBeforeInitializing() {
    yield return new WaitForSeconds(0.5f);
    zKeyProcessed = false;
    }


    // 게임오버 UI - 선택지 실행
    public void GameoverSelectResult(){
        if(hasFunctionRun == true){
            return;
        }
         if (isGameover == true && !zKeyProcessed && !isAfterInitializing){
            if (gameoverDataInput == true && Input.GetKeyDown(KeyCode.Z)){
                zKeyProcessed = true;
                isDataInput = true;
                isInitializing = true;
                dataInputUI.SetActive(true);
                gameoverUI.SetActive(false);
                hasFunctionRun = true;
                StartCoroutine(DelayBeforeInitializing());
            }
            else if (gameoverRestart == true && Input.GetKeyDown(KeyCode.Z)){
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else if (gameoverQuit == true && Input.GetKeyDown(KeyCode.Z)){
                SceneManager.LoadScene("Integration_Scene");
                // #if UNITY_EDITOR
                // // 에디터에서는 에디터를 종료
                // UnityEditor.EditorApplication.isPlaying = false;
                // #else
                // // 빌드된 게임에서는 어플리케이션을 종료
                // Application.Quit();
                // #endif
            }
         }
    }

    ///////////////////////////////////////// 데이터 입력 UI /////////////////////////////////////////
    
    // 데이터 입력 UI 동작 처리
    public void DataInputUI(){
        if(isGameover == true && !zKeyProcessed){
            if (isDataInput && isInitializing) {
            InitialSelect();
            InitialSelectMark();
            InitialSelectMove();

            // 이 상태에서 Z키를 누르면 isinitializing을 false로 설정
                if (Input.GetKeyDown(KeyCode.Z)) {
                    isInitializing = false;
                    StartCoroutine(DelayBeforeDataInput());
                }
            }
        }

        // isgameover, isdatainput이 true이고 isinitializing이 false일 때 함수들 실행
        if (isDataInput && !isInitializing && isAfterInitializing) {
            DataInputSelectMark();
            DataInputSelectMove();
            DataInputSelectResult();

            // 이 상태에서 X키를 누르면 isinitializing을 true로 설정
            if (Input.GetKeyDown(KeyCode.X)) {
                isInitializing = true;
                isAfterInitializing = false;
            }
        }
    }
    IEnumerator DelayBeforeDataInput() {
    Debug.Log("start");
    yield return new WaitForSeconds(0.5f);
    Debug.Log("finish");
    isAfterInitializing = true;
    }
    
    ///////////// 이니셜 /////////////

    // 데이터 입력 UI - 이니셜 - 이니셜 위치 표기
    public void InitialSelectMark(){
    RectTransform rt1 = initialArrow1.GetComponent<RectTransform>();
    RectTransform rt2 = initialArrow2.GetComponent<RectTransform>();

    if (selectInitial1 == true){
        rt1.anchoredPosition = new Vector2(-10, rt1.anchoredPosition.y);
        rt2.anchoredPosition = new Vector2(-10, rt2.anchoredPosition.y);
    }
    if (selectInitial2 == true){
        rt1.anchoredPosition = new Vector2(20, rt1.anchoredPosition.y);
        rt2.anchoredPosition = new Vector2(20, rt2.anchoredPosition.y);
    }
    if (selectInitial3 == true){
        rt1.anchoredPosition = new Vector2(50, rt1.anchoredPosition.y);
        rt2.anchoredPosition = new Vector2(50, rt2.anchoredPosition.y);
    }
}

    // 데이터 입력 UI - 이니셜 - 이니셜 위치 변경
    public void InitialSelectMove(){
        if (selectInitial1 == true && Input.GetKeyDown(KeyCode.RightArrow)){
            selectInitial1 = false;
            selectInitial2 = true;
        }
        else if (selectInitial2 == true && Input.GetKeyDown(KeyCode.LeftArrow)){
                selectInitial1 = true;
                selectInitial2 = false;
            }
        else if (selectInitial2 == true && Input.GetKeyDown(KeyCode.RightArrow)){
                selectInitial3 = true;
                selectInitial2 = false;
            }
        else if (selectInitial3 == true && Input.GetKeyDown(KeyCode.LeftArrow)){
            selectInitial2 = true;
            selectInitial3 = false;
        }
    }

    // 데이터 입력 UI - 이니셜 - 이니셜 변경
    public void InitialSelect(){
        if(selectInitial1 == true){
            InitialSelectPrefab(1);
        }
        else if(selectInitial2 == true){
            InitialSelectPrefab(2);
        }
        else if(selectInitial3 == true){
            InitialSelectPrefab(3);
        }
        
    }

    // 이니셜 변경 프리팹
    public void InitialSelectPrefab(int selection){
    
        int index = selection - 1;

        if (Input.GetKeyDown(KeyCode.UpArrow)){
            if (initialNum[index] == 90) {
                initialNum[index] = 97;
            } else if (initialNum[index] == 122) {
                initialNum[index] = 65;
            } else {
                initialNum[index]++;
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow)){
            if (initialNum[index] == 65) {
                initialNum[index] = 122;
            } else if (initialNum[index] == 97) {
                initialNum[index] = 90;
            } else {
                initialNum[index]--;
            }
        }
        if(index == 0){
            initial1.text = "" + (char)initialNum[index] + "";
        }
        else if(index == 1){
            initial2.text = "" + (char)initialNum[index] + "";
        }
        else if(index == 2){
            initial3.text = "" + (char)initialNum[index] + "";
        }

    }

    ///////////// 데이터 /////////////

    // 데이터 입력 UI - 선택지 이동 
    public void DataInputSelectMove(){
        if(isInitializing == false){
            if (dataInputSave == true && Input.GetKeyDown(KeyCode.RightArrow)){
                    dataInputRestart = true;
                    dataInputSave = false;
            }
            else if (dataInputRestart == true && Input.GetKeyDown(KeyCode.LeftArrow)){
                    dataInputSave = true;
                    dataInputRestart = false;
            }
            else if (dataInputRestart == true && Input.GetKeyDown(KeyCode.RightArrow)){
                    dataInputQuit = true;
                    dataInputRestart = false;
            }
            else if (dataInputQuit == true && Input.GetKeyDown(KeyCode.LeftArrow)){
                    dataInputRestart = true;
                    dataInputQuit = false;
            }
        }
    }

    // 데이터 입력 UI - 선택지 표기
    public void DataInputSelectMark(){
        Transform uiDataInputTransform = GameObject.Find("Canvas/UI_DataInput").transform;
        if (isInitializing == false){
            if (dataInputSave == true){
                obj = uiDataInputTransform.Find("Button_DataInsert").gameObject;
                ChangeMaterial(obj, newMaterials);
            }
            if (dataInputSave == false){
                obj = uiDataInputTransform.Find("Button_DataInsert").gameObject;
                ResetMaterial(obj);
            }
            if (dataInputRestart == true){
                obj = uiDataInputTransform.Find("Button_ReStart").gameObject;
                ChangeMaterial(obj, newMaterials);
            }
            if (dataInputRestart == false){
                obj = uiDataInputTransform.Find("Button_ReStart").gameObject;
                ResetMaterial(obj);
            }
            if (dataInputQuit == true){
                obj = uiDataInputTransform.Find("Button_Exit").gameObject;
                ChangeMaterial(obj, newMaterials);
            }
            if (dataInputQuit == false){
                obj = uiDataInputTransform.Find("Button_Exit").gameObject;
                ResetMaterial(obj);
            }
        }
    }

    // 데이터 입력 UI - 선택지 실행
    public void DataInputSelectResult(){
        if (isInitializing == false){
            if (dataInputSave == true && Input.GetKeyDown(KeyCode.Z)){
                obj = GameObject.Find("Hand_GameManager");
                obj.GetComponent<Hand_GameManager>().DB_Aquest_Before(initialNum[0], initialNum[1], initialNum[2]);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else if (dataInputRestart == true && Input.GetKeyDown(KeyCode.Z)){
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else if (dataInputQuit == true && Input.GetKeyDown(KeyCode.Z)){
                SceneManager.LoadScene("Integration_Scene");
                // #if UNITY_EDITOR
                // // 에디터에서는 에디터를 종료
                // UnityEditor.EditorApplication.isPlaying = false;
                // #else
                // // 빌드된 게임에서는 어플리케이션을 종료
                // Application.Quit();
                // #endif
            }
        }
    } 
}