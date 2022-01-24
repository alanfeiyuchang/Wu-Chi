using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBox : MonoBehaviour
{
    public Transform leftDoor;
    public Transform rightDoor;
    private bool isOpening = false;
    private float targetDegrees;
    private float anglePerSecond;
    private float startingAngle;
    private float finishedFrame = 50f;
    // Start is called before the first frame update

    public void OpeningBox(float degree)
    {
        targetDegrees = degree;
        isOpening = true;
        startingAngle = this.transform.localEulerAngles.z;
    }
    // Update is called once per frame
    void Update()
    {
        if (isOpening)
        {
            anglePerSecond++;
            float z_angle = Mathf.Lerp(startingAngle, startingAngle + targetDegrees, anglePerSecond / finishedFrame);
            float negative_z_angle = Mathf.Lerp(startingAngle, startingAngle - targetDegrees, anglePerSecond / finishedFrame);
            leftDoor.transform.localEulerAngles = new Vector3(leftDoor.transform.localEulerAngles.x, leftDoor.transform.localEulerAngles.y,  z_angle);
            rightDoor.transform.localEulerAngles = new Vector3(rightDoor.transform.localEulerAngles.x, rightDoor.transform.localEulerAngles.y, negative_z_angle);
            if (z_angle >= targetDegrees)
                isOpening = false;
        }
    }
}
