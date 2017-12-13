using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent onReset;          //이렇게 한 다음에 에디터에서 드래그 앤 드롭시키면, onReset이 실행될 때마다 드롭한 메서드가 실행된다. onReset 실행방법은 onReset.Invoke() 하면 된다. 밑에 코루틴 함수 첫부분 참고. 

    public static GameManager instance;

    public GameObject readyPanel;
    public Text scoreText;
    public Text bestScoreText;
    public Text messageText;
    public bool isRoundActive = false;

    public ShooterRotator shooterRotator;
    public CamFollow cam;

    private int score = 0;

    void Awake()
    {
        instance = this;
        UpdateUI();
    }

    void Start()
    {
        StartCoroutine("RoundRoutine");
    }

    public void Addscore(int newScore)
    {
        score = score + newScore;
        UpdateBestScore();
        UpdateUI();
    }

    void UpdateBestScore()
    {
        if (GetBestScore() < score)
        {
            PlayerPrefs.SetInt("BestScore", score);
        }        
    }

    int GetBestScore()
    {
        int bestScore = PlayerPrefs.GetInt("BestScore");    //만약 값이 없으면 자동으로 0이 저장된다. 
        return bestScore;
    }

    void UpdateUI()
    {
        scoreText.text = "Score " + score;
        bestScoreText.text = "Best Score " + GetBestScore();
    }

    public void OnBallDestroy()
    {
        UpdateUI();
        isRoundActive = false;
    }

    public void Reset()
    {
        score = 0;
        UpdateUI();

        //라운드 다시 첨으부터 시작하는 코드
        StartCoroutine("RoundRoutine");
    }

    IEnumerator RoundRoutine()
    {
        //Ready
        onReset.Invoke();

        readyPanel.SetActive(true);
        cam.SetTarget(shooterRotator.transform, CamFollow.State.Idle);
        shooterRotator.enabled = false;

        isRoundActive = false;

        messageText.text = "Ready";

        yield return new WaitForSeconds(3f);

        //Player
        isRoundActive = true;
        readyPanel.SetActive(false);
        shooterRotator.enabled = true;

        cam.SetTarget(shooterRotator.transform, CamFollow.State.Ready);

        while (isRoundActive == true)
        {
            yield return null;
        }

        //END
        readyPanel.SetActive(true);
        shooterRotator.enabled = false;
        messageText.text = "Wait for Next Round";

        yield return new WaitForSeconds(3f);

        Reset();
    }
}
