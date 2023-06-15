using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private int timeForComplete = 60;
    [SerializeField] private Image timerImage;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI winScoreText;
    [SerializeField] private TextMeshProUGUI loseScoreText;
    [SerializeField] private TextMeshProUGUI turnText;

    Coroutine timer;
    private int score;
    private int turn = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        timer = StartCoroutine(Timer());
        AddScore(0);
    }

    IEnumerator Timer()
    {
        int tempTimer = timeForComplete;
        timeText.text = timeForComplete.ToString();

        while (tempTimer > 0)
        {
            tempTimer--;
            yield return new WaitForSeconds(1);
            timerImage.fillAmount = tempTimer / (float)timeForComplete;
            timeText.text = tempTimer.ToString();
        }

        GameManager.Instance.CheckGameOver();
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = score.ToString("D8");
        winScoreText.text = score.ToString();
        loseScoreText.text = score.ToString();
    }

    public void AddTurn()
    {
        turn++;
        turnText.text = turn.ToString("D2");
    }

    public void StopTimer()
    {
        StopCoroutine(timer);
    }
}
