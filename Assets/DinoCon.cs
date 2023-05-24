using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoCon : MonoBehaviour
{

    Animator aniDio;
    string isRunStr, isDeadStr, isDownStr;
    bool isOnSky,isDown;
    Rigidbody2D rig;
    Vector2 downPos,downSize;
    BoxCollider2D downColider;
    PolygonCollider2D runCollider;

    //float jumpHei; 
    //down speed
    public bool isDead;
    private Vector3 orgPos;
    public AudioClip jumpClip;
    public AudioClip deadClip;

    public bool isTest;
    public Transform collisionPoint;
    // Start is called before the first frame update

    List<GameObject> destroyInRestart;
    void Start()
    {
        orgPos = transform.position;
        aniDio = GetComponentInChildren<Animator>();
        isRunStr = "isRun";
        isDeadStr = "isDead";
        isDownStr = "isDown";
        rig = GetComponent<Rigidbody2D>();
        rig.isKinematic = false;
        isOnSky = true;
        isDead = false;
        
        runCollider = GetComponents<PolygonCollider2D>()[0];
        downColider = GetComponents<BoxCollider2D>()[0];
        downColider.enabled = false;
        destroyInRestart = new List<GameObject>();
        //aniDio.enabled = false;
        //Debug.Break();
        //print(downSize);

        if (Application.platform == RuntimePlatform.WindowsPlayer) {
            Debug.Log("Do something special here");
        }

        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            isTest = true;
        }
        else {
            isTest = false;
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawLine(new Vector3(0, 0, 0), new Vector3(10, 10, 0), Color.red);
        if (isDead) {
            return;
        }
        
        bool isDownHold = Input.GetKey(KeyCode.DownArrow);
        if (isDownHold && !isOnSky && !isDown)
        {
            downStart();
            return;
        } else if (!isDownHold && isDown) {
            downEnd();
            return;
        }


        if (isDownHold && isOnSky) {
            fallFast();
            return;
        }

        if (isDown || isOnSky) {
            return;
        }

        bool space = Input.GetKey(KeyCode.Space);
        if (!space) {
            if (Input.touchCount != 0)
            {
                space = true;
            }
           
        }


    


        if (space) {
            jump();
        }
        bool k = Input.GetKeyUp(KeyCode.K);

        if (k)
        {
            aniDio.SetBool(isDeadStr, true);

        }


    }

    void jump() {
        if (isOnSky) {
            return;
        }
        AudioSource.PlayClipAtPoint(jumpClip,new Vector3(0,0,-10),1);
        aniDio.SetBool(isRunStr, false);
        //print("jump");
        //Debug.Break();
        bool a = aniDio.GetCurrentAnimatorStateInfo(0).IsName("AniDinoStand");
        bool b = aniDio.GetCurrentAnimatorStateInfo(0).IsName("Down");
        if (a) {
            //print("AniDinoStand");
        }
        if (b) {
            //print("Down");
        }
         //aniDio.enabled = false;

        GetComponent<Rigidbody2D>().AddForce(Vector2.up*500*3);
        isOnSky = true;
        
    }
    void gameOver()
    {
        aniDio.SetBool(isDeadStr, true);
        AudioSource.PlayClipAtPoint(deadClip, new Vector3(0, 0, -10), 1);
        isDead = true;
        //GetComponent<Rigidbody2D>().isKinematic = true;
        //Destroy(GetComponent<Rigidbody2D>());
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        //Debug.Break();
    }

    void downStart()
    {

        //print("downStart");
        //Debug.Break();
        runCollider.enabled = false;
        downColider.enabled = true;
        aniDio.SetBool(isDownStr, true);
        isDown = true;

    }

    void downEnd()
    {
        //print("downEnd");
        //Debug.Break();
        runCollider.enabled = true;
        downColider.enabled = false;


        aniDio.SetBool(isDownStr, false);
        isDown = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameOver();

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //print(collision.relativeVelocity);
        if (collision.gameObject.CompareTag("Obstacle")) {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

            showAllPoint(collision.contacts);

           



            //Debug.Break();
            gameOver();
            
            return;
        }

        
        if (collision.relativeVelocity.magnitude > 1 && collision.relativeVelocity.magnitude < 100) {
            //print("OnCollisionEnter2D");
            //print(collision.relativeVelocity);
            isOnSky = false;
            aniDio.SetBool(isRunStr, true);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
          
        }
      
    }

    void showAllPoint(ContactPoint2D[] cs) {
        collisionPoint.gameObject.SetActive(true);
        foreach (ContactPoint2D c in cs) {
            GameObject point = Instantiate(collisionPoint.gameObject);
            point.transform.position = c.point;
            destroyInRestart.Add(point);
        }
        collisionPoint.gameObject.SetActive(false);
    }

    void fallFast()
    {
        //print("fallFast");
        GetComponent<Rigidbody2D>().AddForce(Vector2.down*30);
    }

  
    public void restart() {
        collisionPoint.position = new Vector2(0,0);
        collisionPoint.gameObject.SetActive(false);
        isDead = false;
        isOnSky = false;

        aniDio.SetBool(isDeadStr, false);
        aniDio.SetBool(isDownStr, false);
        aniDio.SetBool(isRunStr, true);
        GetComponent<Rigidbody2D>().isKinematic = false;
        transform.position = orgPos;
        while (destroyInRestart.Count !=0) {
            Destroy(destroyInRestart[0]);
            destroyInRestart.RemoveAt(0);
        }
        

    }
}
