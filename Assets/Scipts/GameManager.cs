using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }

    [SerializeField] private MusicManagerSO musicSFXSO;
    [SerializeField] private bool hideMatches;
    [SerializeField] private int scorePerMatch = 100;
    [SerializeField] private GameObject winConfettiVFX;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private TextMeshProUGUI winScoreText;
    [SerializeField] private TextMeshProUGUI LoseScoreText;

    private List<Card> pickedCards = new List<Card>();
    private bool picked;
    private bool gameOver;
    private int pairs;
    private int pairsCounter;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        winConfettiVFX.SetActive(false);
    }

    public void AddCardFromList(Card card)
    {
        if(pickedCards.Contains(card)) return;
        
        pickedCards.Add(card);

        if(pickedCards.Count == 2)
        {
            picked = true;
            StartCoroutine(CheckMatch());
        }
    }

    IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(1.5f);

        if (pickedCards[0].GetCardId() == pickedCards[1].GetCardId())
        {
            if(hideMatches)
            {
                pickedCards[0].gameObject.SetActive(false);
                pickedCards[1].gameObject.SetActive(false);
                AudioSource.PlayClipAtPoint(musicSFXSO.matchSFX, transform.position);
            }

            else
            {
                pickedCards[0].GetComponent<BoxCollider>().enabled = false;
                pickedCards[1].GetComponent<BoxCollider>().enabled = false;
                AudioSource.PlayClipAtPoint(musicSFXSO.matchSFX, transform.position);
            }

            pairsCounter++;
            CheckForWin();
            pickedCards[0].PlayConfetti();
            pickedCards[1].PlayConfetti();
            ScoreManager.Instance.AddScore(scorePerMatch);
        }

        else
        {
            pickedCards[0].FlippedOpen(false);
            pickedCards[1].FlippedOpen(false);
            yield return new WaitForSeconds(1.5f);
        }
            
        picked = false;
        pickedCards.Clear();
        ScoreManager.Instance.AddTurn();
    }

    private void CheckForWin()
    {
        if(pairs == pairsCounter)
        {
            winPanel.SetActive(true);
            AudioSource.PlayClipAtPoint(musicSFXSO.winSFX, transform.position);
            winConfettiVFX.gameObject.SetActive(true);
            ScoreManager.Instance.StopTimer();
            //Debug.Log("Win");
        }
    }

    public void CheckGameOver()
    {
        losePanel.SetActive(true);
        AudioSource.PlayClipAtPoint(musicSFXSO.loseSFX, transform.position);
        gameOver = true;
        //Debug.Log("Times Up");
    }

    public bool HasPickedCard()
    {
        return picked;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void SetPairs(int paitAmount)
    {
        pairs = paitAmount;
    }
}
