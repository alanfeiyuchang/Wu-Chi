using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    [SerializeField] private GameObject TopPlayer;
    [SerializeField] private Transform backgroundTrans;
    [SerializeField] private GameObject UpPart;
    [SerializeField] private GameObject virtualCam;
    [SerializeField] private GameObject TopGround;
    private int pressCount = 0;
    float timeElapsed = 0f;

    public void EndingButtonPressed()
    {
        pressCount++;
        if (pressCount >= 2)
        {
            StartCoroutine(EndingAnim(2f));
        }
    }


    IEnumerator EndingAnim(float duration)
    {
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
        TopGround.SetActive(false);
        while (timeElapsed <= duration)
        {
            TopPlayer.transform.position = Vector3.Lerp(initPos, toPos, timeElapsed / duration);
            backgroundTrans.position = Vector3.Lerp(initBackPos, toBackPos, timeElapsed / duration);
            UpPart.transform.position = Vector3.Lerp(initUpPos, toUpPos, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        //GameManager.instance.InitialControl();
    }
}
