using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private List<Collider2D> colList = new List<Collider2D>();
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
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "spike")
        {
            GameManager.instance.SwitchGameState(GameManager.GameState.Dead);
            Debug.Log("spike!!" + collision.gameObject.name+ "hit "+this.gameObject.name+"at " + gameObject.transform.position);
            
        }

        if (collision.CompareTag("bomb"))
        {
            GameManager.instance.SwitchGameState(GameManager.GameState.Dead);
            Debug.Log("bomb");
        }

        if (collision.CompareTag("obstacle"))
        {
            if (!colList.Contains(collision))
                colList.Add(collision);
        }

        if (collision.CompareTag("checkPoint"))
        {
            GameManager.instance.UpdateCheckPoint(collision.transform.parent.position);
        }

        if (collision.CompareTag("BombButton"))
        {
            Debug.Log("trigger buttton");
            collision.GetComponent<Button>().TriggerButton();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("obstacle"))
        {
            Debug.Log("Remove");
            if (colList.Contains(collision))
                colList.Remove(collision);
        }
    }

    public bool CanSwitch()
    {
        return colList.Count == 0;
    }
}
