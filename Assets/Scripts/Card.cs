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
    private bool isAnimating = false;

    public void Flip()
    {
        if (isMatched || isFlipped || isAnimating) return;

        StartCoroutine(FlipAnimation());
    }

    private IEnumerator FlipAnimation()
    {
        isAnimating = true;

        float duration = 0.05f;
        float time = 0f;

        // Shrink
        while (time < duration)
        {
            float scale = Mathf.Lerp(1f, 0f, time / duration);
            transform.localScale = new Vector3(scale, 1f, 1f);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localScale = new Vector3(0f, 1f, 1f);

        isFlipped = true;
        front.SetActive(true);
        back.SetActive(false);

        GameManager.Instance.RegisterFlip(this);

        time = 0f;
        while (time < duration)
        {
            float scale = Mathf.Lerp(0f, 1f, time / duration);
            transform.localScale = new Vector3(scale, 1f, 1f);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localScale = Vector3.one;
        isAnimating = false;
    }

    public void ForceFlip(bool showFront)
    {
        if (showFront)
        {
            front.SetActive(true);
            back.SetActive(false);
            isFlipped = true;
        }
        else
        {
            if (isMatched) return;
            StartCoroutine(FlipBackAnimation());
        }

        transform.localScale = Vector3.one;
    }

    private IEnumerator FlipBackAnimation()
    {
        isAnimating = true;

        float duration = 0.05f;
        float time = 0f;

        // Shrink
        while (time < duration)
        {
            float scale = Mathf.Lerp(1f, 0f, time / duration);
            transform.localScale = new Vector3(scale, 1f, 1f);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localScale = new Vector3(0f, 1f, 1f);

        // Hide front, show back
        isFlipped = false;
        front.SetActive(false);
        back.SetActive(true);

        // Expand
        time = 0f;
        while (time < duration)
        {
            float scale = Mathf.Lerp(0f, 1f, time / duration);
            transform.localScale = new Vector3(scale, 1f, 1f);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localScale = Vector3.one;
        isAnimating = false;
    }



    public void SetMatched()
    {
        isMatched = true;
    }
}
