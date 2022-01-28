using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    [SerializeField] private GameObject TopPlayer;
    [SerializeField] private GameObject BottomPlayer;
    [SerializeField] private Transform backgroundTrans;
    [SerializeField] private GameObject UpPart;
    [SerializeField] private GameObject virtualCam;
    [SerializeField] private Transform mushroom;
    [SerializeField] private Transform upMushroom;
    [SerializeField] private SpriteRenderer EndingBackground;
    //[SerializeField] private GameObject TopGround;
    [SerializeField] private List<SpriteRenderer> uselessSprites;

    [SerializeField] private List<SpriteRenderer> mushroomSprites;

    [SerializeField] private GameObject EndingPanel;
    private int pressCount = 0;
    float timeElapsed = 0f;

    public void EndingButtonPressed()
    {
        pressCount++;
        if (pressCount >= 2)
        {
            StartCoroutine(EndingAnim());
        }
    }


    IEnumerator EndingAnim()
    {
        GameManager.instance.canInput = false;
        EndingBackground.transform.position = new Vector3(Camera.main.transform.position.x, 0f, 0f);
        SpriteRenderer topPlayerSR = TopPlayer.GetComponent<SpriteRenderer>();
        SpriteRenderer bottomPlayerSR = BottomPlayer.GetComponent<SpriteRenderer>();
        Color initTopColor = topPlayerSR.color;
        Color goTopColor = new Color(topPlayerSR.color.r, topPlayerSR.color.g, 
            topPlayerSR.color.b, 0f);

        Color initBottomColor = bottomPlayerSR.color;
        Color goBottomColor = new Color(bottomPlayerSR.color.r, bottomPlayerSR.color.g,
            bottomPlayerSR.color.b, 0f);

        float duration = 2f;
        while (timeElapsed <= duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, timeElapsed / duration);
            foreach (SpriteRenderer sprite in uselessSprites)
            {
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
            }
            timeElapsed += Time.deltaTime;
            topPlayerSR.color = Color.Lerp(initTopColor, goTopColor, timeElapsed / duration);
            bottomPlayerSR.color = Color.Lerp(initBottomColor, goBottomColor, timeElapsed / duration);
            yield return null;
        }

        foreach (SpriteRenderer sprite in uselessSprites)
        {
            sprite.enabled = false;
        }

        timeElapsed = 0f;

        TopPlayer.SetActive(false);
        BottomPlayer.SetActive(false);

        Vector3 initPos = TopPlayer.transform.position;
        Vector3 toPos = new Vector3(TopPlayer.transform.position.x,
            TopPlayer.transform.position.y - 5f, TopPlayer.transform.position.z);

        Vector3 initBackPos = backgroundTrans.position;
        Vector3 toBackPos = new Vector3(backgroundTrans.position.x,
            backgroundTrans.position.y - 5f, backgroundTrans.position.z);

        Vector3 initUpPos = UpPart.transform.position;
        Vector3 toUpPos = new Vector3(UpPart.transform.position.x,
            UpPart.transform.position.y - 5f, UpPart.transform.position.z);
        virtualCam.SetActive(false);
        GameManager.instance.DisableCharacterController();

        duration = 3.5f;
        while (timeElapsed <= duration)
        {
            timeElapsed += Time.deltaTime;
            backgroundTrans.position = Vector3.Lerp(initBackPos, toBackPos, timeElapsed / duration);
            UpPart.transform.position = Vector3.Lerp(initUpPos, toUpPos, timeElapsed / duration);
            yield return null;
        }
        timeElapsed = 0f;

        duration = 0.5f;
        float mushroomGrowHeight = 0.2f;
        Vector3 initMushPos = mushroom.position;
        Vector3 toMushPos = new Vector3(mushroom.position.x, 
            mushroom.position.y + mushroomGrowHeight, mushroom.position.z);
        while (timeElapsed <= duration)
        {
            timeElapsed += Time.deltaTime;
            mushroom.position = Vector3.Lerp(initMushPos, toMushPos, timeElapsed / duration);
            upMushroom.position = Vector3.Lerp(initMushPos, toMushPos, timeElapsed / duration);
            yield return null;
        }
        timeElapsed = 0f;

        duration = 1f;
        while (timeElapsed <= duration)
        {
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        timeElapsed = 0f;

        duration = 1f;
        while (timeElapsed <= duration)
        {
            timeElapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timeElapsed / duration);
            foreach (SpriteRenderer sprite in mushroomSprites)
            {
                sprite.color = new Color(sprite.color.r, sprite.color.b, sprite.color.g, alpha);
            }
            yield return null;
        }
        timeElapsed = 0f;

        duration = 0.5f;
        while (timeElapsed <= duration)
        {
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        timeElapsed = 0f;

        duration = 2.5f;
        EndingPanel.SetActive(true);
        CanvasGroup EndingCan = EndingPanel.GetComponent<CanvasGroup>();
        EndingCan.alpha = 0f;
        while (timeElapsed <= duration)
        {
            timeElapsed += Time.deltaTime;
            EndingCan.alpha = Mathf.Lerp(0f, 1f, timeElapsed / duration);
            yield return null;
        }

    }
}
