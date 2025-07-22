using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Card : MonoBehaviour
{
    public GameObject front;
    public GameObject back;

    public int cardId; 
    private bool isFlipped = false;
    private bool isMatched = false;

    public void Flip()
    {
        isFlipped = !isFlipped;
        front.SetActive(isFlipped);
        back.SetActive(!isFlipped);

        if (isFlipped)
        {
            GameManager.Instance.RegisterFlip(this);
        }
    }

    public void SetMatched()
    {
        isMatched = true;
    }


}



