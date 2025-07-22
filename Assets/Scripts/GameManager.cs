using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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
        yield return new WaitForSeconds(1f);

        if (flippedCards[0].cardId == flippedCards[1].cardId)
        {
            flippedCards[0].SetMatched();
            flippedCards[1].SetMatched();
        }
        else
        {
            flippedCards[0].Flip();
            flippedCards[1].Flip();
        }

        flippedCards.Clear();
    }

}
