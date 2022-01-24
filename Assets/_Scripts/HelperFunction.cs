using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperFunction : MonoBehaviour
{
    public static IEnumerator RotationAnim(GameObject go, float angle, float timeDuration)
    {
        Debug.Log("In here");
        float timeElaped = 0f;
        Vector3 q = go.transform.rotation.eulerAngles;
        Vector3 toQ = new Vector3(go.transform.rotation.eulerAngles.x,
            go.transform.rotation.eulerAngles.y, go.transform.rotation.eulerAngles.z + angle);
        while (timeElaped <= timeDuration)
        {
            go.transform.rotation =Quaternion.Euler(Vector3.Lerp(q, toQ, timeElaped / timeDuration));
            timeElaped += Time.deltaTime;
            yield return null;
        }
    }
}
