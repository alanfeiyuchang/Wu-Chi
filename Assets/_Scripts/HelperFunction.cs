using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperFunction : MonoBehaviour
{
    public static IEnumerator RotationAnim(GameObject go, float angle, float timeDuration)
    {
        float timeElapsed = 0f;
        Vector3 q = go.transform.rotation.eulerAngles;
        Vector3 toQ = new Vector3(go.transform.rotation.eulerAngles.x,
            go.transform.rotation.eulerAngles.y, go.transform.rotation.eulerAngles.z + angle);
        while (timeElapsed <= timeDuration)
        {
            go.transform.rotation =Quaternion.Euler(Vector3.Lerp(q, toQ, timeElapsed / timeDuration));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    public static IEnumerator TranslateAnim(GameObject go, Vector3 moveDis, float timeDuration)
    {
        float timeElapsed = 0f;
        Vector3 initPos = go.transform.position;
        Vector3 toPos = new Vector3(go.transform.position.x + moveDis.x,
            go.transform.position.y + moveDis.y, go.transform.position.z + moveDis.z);
        while (timeElapsed <= timeDuration)
        {
            go.transform.position = Vector3.Lerp(initPos, toPos, timeElapsed / timeDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }
}
