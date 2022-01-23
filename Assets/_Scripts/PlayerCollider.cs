using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "spike")
        {
            GameManager.instance.Die();
            Debug.Log("spike!!");
        }

        if (collision.CompareTag("bomb"))
        {
            GameManager.instance.Die();
            Debug.Log("bomb");
        }
    }
}
