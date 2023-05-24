using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeCollision : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject debugPoint;
    void Start()
    {
        debugPoint = GameObject.FindGameObjectWithTag("debugPoint");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.CompareTag("Player")) {
            /*
            GameObject point = Instantiate(debugPoint);
            point.SetActive(true);
            point.transform.position = collision.GetContact(0).point;
            point.GetComponent<SpriteRenderer>().color = Color.blue;
            */
            GetComponent<Rigidbody2D>().isKinematic = true;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
          

    }
}
