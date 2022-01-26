using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontCover : MonoBehaviour
{
    private SpriteRenderer sp;
    private bool validCover;
    private Color color;
    private IEnumerator changingAlpha;
    void Start()
    {
        if ((sp = this.GetComponent<SpriteRenderer>()) != null)
            validCover = true;
    }

    public void Disappear()
    {
        if (changingAlpha != null)
            StopCoroutine(changingAlpha);
        if (validCover)
        {
            changingAlpha = ChangingAlpha(color.a, 0, 1f);
            StartCoroutine(changingAlpha);
        }

    }

    public void Appear()
    {
        if (changingAlpha != null)
            StopCoroutine(changingAlpha);
        if (validCover)
        {
            changingAlpha = ChangingAlpha(color.a, 1, 1f);
            StartCoroutine(changingAlpha);
        }
    }

    IEnumerator ChangingAlpha(float start, float end, float duration)
    {
        float time = 0; 
        while (time < duration)
        {
            color = sp.color;
            color.a = Mathf.Lerp(start, end, time / duration);
            sp.color = color;

            time += Time.deltaTime;
            yield return null;
        }

        color.a = end;
        sp.color = color;
    }
}
