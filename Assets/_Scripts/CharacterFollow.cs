using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFollow : MonoBehaviour
{
    public GameObject followTarget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float yValue;
        if(followTarget.transform.position.y > 0)
        {
            yValue = followTarget.transform.position.y - 5;
        }
        else
        {
            yValue = followTarget.transform.position.y + 5;
        }
        transform.position = new Vector3(followTarget.transform.position.x, yValue,transform.position.z);
    }
}
