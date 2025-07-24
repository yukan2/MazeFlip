using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TMP_Text turnsText;
    public TMP_Text matchesText;

    private int turnCount = 0;
    private int matchCount = 0;

    public AudioSource flipSound;
    public AudioSource matchSound;
    public AudioSource mismatchSound;
    public AudioSource gameOverSound;

    private int totalPairs;

    public GameObject restartButton;

    private bool isCheckingMatch = false;

    public bool allowFlips = true;





    private List<Card> flippedCards = new List<Card>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        totalPairs = 10;
    }


    public void RegisterFlip(Card card)
    {
        if (!allowFlips || isCheckingMatch || flippedCards.Contains(card) || flippedCards.Count >= 2)
            return;

        flippedCards.Add(card);

        if (flippedCards.Count == 2)
        {
            isCheckingMatch = true;

            StartCoroutine(CheckMatch());
        }

        flipSound.Play();

    }

    private IEnumerator CheckMatch()
    {
        allowFlips = false;

        yield return new WaitForSeconds(0.5f);

        turnCount++;
        turnsText.text = "Turns: " + turnCount;


        if (flippedCards[0].cardId == flippedCards[1].cardId)
        {
            flippedCards[0].SetMatched();
            flippedCards[1].SetMatched();

            matchCount++;
            matchesText.text = "Matches: " + matchCount;

            matchSound.Play();

            if (matchCount == totalPairs)
            {
                gameOverSound.Play();
                restartButton.SetActive(true);

            }


        }
        else
        {
            mismatchSound.Play();

            flippedCards[0].ForceFlip(false);
            flippedCards[1].ForceFlip(false);
        }

        flippedCards.Clear();
        isCheckingMatch = false;
        allowFlips = true;


    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
