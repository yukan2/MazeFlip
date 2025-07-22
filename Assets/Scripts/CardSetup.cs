using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSetup : MonoBehaviour
{
    public Sprite[] cardIcons; // Assigned in Inspector (sliced icons)
    public GameObject cardGrid; // Assigned in Inspector
    private List<Card> allCards = new List<Card>();

    void Start()
    {
        allCards.AddRange(cardGrid.GetComponentsInChildren<Card>());

        List<int> ids = new List<int>();
        int pairCount = allCards.Count / 2;

        for (int i = 0; i < pairCount; i++)
        {
            ids.Add(i);
            ids.Add(i);
        }

        Shuffle(ids);

        for (int i = 0; i < allCards.Count; i++)
        {
            allCards[i].cardId = ids[i];
            allCards[i].front.GetComponent<Image>().sprite = cardIcons[ids[i]];
        }
    }

    void Shuffle(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rnd = Random.Range(i, list.Count);
            int temp = list[i];
            list[i] = list[rnd];
            list[rnd] = temp;
        }
    }
}
