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
    private float finishedFrame = 0.5f;
    private float translateProcess;
    private Vector3 leftTranslateVector;
    private Vector3 rightTranslateVector;
    // Start is called before the first frame update


    public void OpeningGateWithTranslate(float openpercent)
    {
        leftTranslateVector = leftDoorDest.position - leftDoor.position;
        rightTranslateVector = rightDoorDest.position - rightDoor.position;
        leftStartingPos = leftDoor.position;
        rightStartingPos = rightDoor.position;
        openWithTranslate = true;
    }
    public void OpeningBox(float degree)
    {
        targetDegrees = degree;
        isOpening = true;
        leftStartingAngle = leftDoor.transform.localEulerAngles.z;
        rightStartingAngle = rightDoor.transform.localEulerAngles.z;
        anglePerSecond = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (isOpening)
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
            translateProcess += Time.deltaTime;
            Vector3 newPos = Vector3.Lerp(leftStartingPos, leftTranslateVector/2 + leftStartingPos, translateProcess / finishedFrame);
            leftDoor.position = newPos;
            if (translateProcess / finishedFrame >= 0.999f)
                openWithTranslate = false;
        }
    }
}
