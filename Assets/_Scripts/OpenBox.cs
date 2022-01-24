using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBox : MonoBehaviour
{
    public Transform leftDoor;
    public Transform rightDoor;
    public Transform leftDoorDest;
    public Transform rightDoorDest;
    private bool isOpening = false;
    private bool openWithTranslate = false;
    private float targetDegrees;
    private float anglePerSecond;
    private float leftStartingAngle;
    private float rightStartingAngle;
    private Vector3 leftStartingPos;
    private Vector3 rightStartingPos;
    public float finishedFrame = 0.5f;
    private float translateProcess;
    private float currentPorcess;
    private Vector3 leftTranslateVector;
    private Vector3 rightTranslateVector;
    // Start is called before the first frame update


    private void Start()
    {
        leftTranslateVector = leftDoorDest.position - leftDoor.position;
        rightTranslateVector = rightDoorDest.position - rightDoor.position;
        leftStartingPos = leftDoor.position;
        rightStartingPos = rightDoor.position;
    }
    public void OpeningGateWithTranslate(float openpercent)
    {
        openWithTranslate = true;
        translateProcess += openpercent;
        currentPorcess = 0f;
    }
    public void OpeningBox(float degree)
    {
        targetDegrees = degree;
        isOpening = true;
        leftStartingAngle = leftDoor.transform.localEulerAngles.z;
        rightStartingAngle = rightDoor.transform.localEulerAngles.z;
        anglePerSecond = 0;
    }

    public void OpenHalf()
    {
        Vector3 disUp = new Vector3(0f, 1.6f, 0f);
        Vector3 disDown = new Vector3(0f, -1.6f, 0f);
        StartCoroutine(HelperFunction.TranslateAnim(leftDoor.gameObject, disUp, 1f));
        StartCoroutine(HelperFunction.TranslateAnim(rightDoor.gameObject, disDown, 1f));
    }
    // Update is called once per frame
    void Update()
    {
        /*if (isOpening)
        {
            anglePerSecond += Time.deltaTime;
            float z_angle = Mathf.Lerp(leftStartingAngle, leftStartingAngle + targetDegrees, anglePerSecond / finishedFrame);
            float negative_z_angle = Mathf.Lerp(rightStartingAngle, rightStartingAngle - targetDegrees, anglePerSecond / finishedFrame);
            leftDoor.transform.localEulerAngles = new Vector3(leftDoor.transform.localEulerAngles.x, leftDoor.transform.localEulerAngles.y,  z_angle);
            rightDoor.transform.localEulerAngles = new Vector3(rightDoor.transform.localEulerAngles.x, rightDoor.transform.localEulerAngles.y, negative_z_angle);
            if (z_angle >= leftStartingAngle + targetDegrees)
                isOpening = false;
        }

        if (openWithTranslate)
        {
            currentPorcess += Time.deltaTime;
            leftDoor.position = Vector3.Lerp(leftStartingPos, leftTranslateVector/translateProcess + leftStartingPos, currentPorcess / finishedFrame);
            Debug.Log(leftTranslateVector / translateProcess + leftStartingPos);
            rightDoor.position = Vector3.Lerp(rightStartingPos, rightTranslateVector/ translateProcess + rightStartingPos, currentPorcess / finishedFrame);
            if (currentPorcess / finishedFrame >= 0.999f)
                openWithTranslate = false;
        }*/
    }
}
