using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;   // 전역 설정

    public bool isGameStart = false;
    public bool isGameOver = false;

    GameObject titleUI;
    GameObject gameOverUI;
    Text scoreUI;
    Text info;

    [SerializeField]
    Text gameTimeText;
    float gameTime = 0;

    float bestTime = 0;

    int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        gm = this;

        titleUI = GameObject.Find("TitleUI");
        gameTimeText = GameObject.Find("GameTimeText").GetComponent<Text>();
        gameOverUI = GameObject.Find("GameOverUI");
        info = GameObject.Find("InfoUI").GetComponent<Text>();
        scoreUI = GameObject.Find("ScoreUI").GetComponent<Text>();

        if (PlayerPrefs.HasKey("BestTime"))
        {
            bestTime = PlayerPrefs.GetFloat("BestTime");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameStart)   // (isGameStart == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                titleUI.SetActive(false);
                CameraController.cm.SIZE = true;
            }
        }
        else
        {
            SetGameTime();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }

    public void GameStart() // 게임 시작 시 호출될 함수
    {
        isGameStart = true;
        EarthComponent.ec.ActiveHpBar();
        gameTimeText.enabled = true;
        scoreUI.enabled = true;
    }
    public void GameOver()  // 게임 끝나면 호출될 함수
    {
        isGameOver = true;
        gameOverUI.transform.GetChild(0).gameObject.SetActive(true);
        RecordTime();
    }

    void SetGameTime()
    {
        if (isGameOver)
        {
            return;
        }
        gameTime += Time.deltaTime;
        gameTimeText.text = "GAME TIME\n<size=55>" + GetTimeString(gameTime) + "</size>";
    }

    string GetTimeString(float t)
    {
        string mins = Mathf.FloorToInt(t / 60).ToString();

        if(int.Parse(mins) < 10)
        {
            mins = "0" + mins;
        }

        string secs = Mathf.FloorToInt((int)(t % 60)).ToString();

        if(int.Parse(secs) < 10)
        {
            secs = "0" + secs;
        }

        return mins + ":" + secs;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Main");
        // SceneManager.LoadScene("SampleScene");
    }

    void RecordTime()
    {
        SetBestTime();

        gameOverUI.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "PLAY TIME\n<size=55>" + GetTimeString(gameTime) + "</size>";
        
        gameOverUI.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = "BEST TIME\n<size=55>" + GetTimeString(bestTime) + "</size>";
    }

    void SetBestTime()
    {
        if (PlayerPrefs.HasKey("BestTime"))
        {
            if(bestTime < gameTime)
            {
                bestTime = gameTime;
                PlayerPrefs.SetFloat("BestTime", bestTime);
            }

        }
        else
        {
            bestTime = gameTime;
            PlayerPrefs.SetFloat("BestTime", gameTime);
        }
    }

    public void InfoItemEffect(string str)
    {
        StopCoroutine("InfoText");
        StartCoroutine(InfoText(str));
    }

    IEnumerator InfoText(string str)
    {
        info.text = str;
        info.enabled = true;

        yield return new WaitForSeconds(1);
        info.enabled = false;
    }

    public void GetScore(int _score)
    {
        score += _score;
        scoreUI.text = "SCORE : " + score.ToString();
    }

}
