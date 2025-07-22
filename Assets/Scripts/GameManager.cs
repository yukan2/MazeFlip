using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TMP_Text turnsText;
    public TMP_Text matchesText;

    private int turnCount = 0;
    private int matchCount = 0;


    private List<Card> flippedCards = new List<Card>();

    private void Awake()
    {
        Instance = this;
    }

    public void RegisterFlip(Card card)
    {
        if (flippedCards.Contains(card) || flippedCards.Count >= 2)
            return;

        flippedCards.Add(card);

        if (flippedCards.Count == 2)
        {
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(0.5f);

        turnCount++;
        turnsText.text = "Turns: " + turnCount;


        if (flippedCards[0].cardId == flippedCards[1].cardId)
        {
            flippedCards[0].SetMatched();
            flippedCards[1].SetMatched();

            matchCount++;
            matchesText.text = "Matches: " + matchCount;

        }
        else
        {
            flippedCards[0].ForceFlip(false);
            flippedCards[1].ForceFlip(false);
        }

        flippedCards.Clear();
    }

}
