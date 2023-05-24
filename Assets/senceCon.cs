using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class senceCon : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject lastObstacle, lastCloud;
    private Sprite[] treeArray;
    private float obstacleRange, cloudRange;
    public GameObject treePos;
    public Sprite tree1, tree2, tree3, tree4, tree5, tree6, cloud;
    public GameObject bird;
    public GameObject ground;
    float weight;
    public GameObject groundPrefab;
    List<GameObject> obstacles;

    private void Start()
    {
        obstacles = new List<GameObject>();
        weight = groundPrefab.GetComponent<BoxCollider2D>().size.x;
        treeArray = new Sprite[] { tree1, tree2, tree3, tree4, tree5, tree6 };
    }

    public void init()
    {
        obstacles = new List<GameObject>();
        weight = groundPrefab.GetComponent<BoxCollider2D>().size.x;
        treeArray = new Sprite[] { tree1, tree2, tree3, tree4, tree5, tree6 };

    }
    public void createObstacle()
    {
        if (lastObstacle == null)
        {
            lastObstacle = createTree(treeArray[Random.Range(0, 5)]);
            obstacles.Add(lastObstacle);

            obstacleRange = Random.Range(10f, 25f);
        }
        else
        {
            float range = treePos.transform.position.x - lastObstacle.transform.position.x;
            if (range >= obstacleRange)
            {
                if (Random.Range(0, 2) == 1)
                    lastObstacle = createTree(treeArray[Random.Range(0, 6)]);
                else
                    lastObstacle = createBird();
                obstacleRange = Random.Range(8f, 20f);
                obstacles.Add(lastObstacle);
            }
        }
    }


    GameObject newCloud()
    {
        GameObject t = new GameObject("cloud");
        t.AddComponent<SpriteRenderer>().sprite = cloud;
        t.GetComponent<SpriteRenderer>().sortingLayerName = "object";
        t.transform.localScale = new Vector3(2, 2, 2);
        float height = Random.Range(1.6f, 4.3f);
        t.transform.position = new Vector3(treePos.transform.position.x, height, 0);
        t.AddComponent<groundCon>();
        return t;
    }


    GameObject createBird()
    {
        float height = Random.Range(-2.0f, 1.6f);
        Vector3 pos = new Vector3(treePos.transform.position.x, height, 0);
        GameObject re = Instantiate(bird, pos, bird.transform.rotation);
        re.tag = "Obstacle";
        return re;
    }

    public void createCloud()
    {
        if (lastCloud == null)
        {
            lastCloud = newCloud();
            cloudRange = Random.Range(8f, 16f);
        }
        else
        {
            float range = treePos.transform.position.x - lastCloud.transform.position.x;
            if (range >= cloudRange)
            {
                lastCloud = newCloud();
                cloudRange = Random.Range(8f, 16f);
            }

        }


    }

    public void createGround()
    {
        if (ground.transform.position.x * -1 >= weight / 2)
        {
            Vector3 pos = new Vector3(weight*2 + (ground.transform.position.x), ground.transform.position.y, 0);
            ground = Instantiate(groundPrefab, pos, ground.transform.rotation);
            //Debug.Break();
        }

       

    }

    GameObject createTree(Sprite tree)
    {
        GameObject t = new GameObject("tree");
        t.AddComponent<SpriteRenderer>().sprite = tree;
        t.AddComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        t.GetComponent<Rigidbody2D>().freezeRotation = true;

         //co = RigidbodyConstraints2D.FreezePositionY;
         //co = RigidbodyConstraints2D.FreezeRotation;
         //t.AddComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;

        t.GetComponent<SpriteRenderer>().sortingLayerName = "object";
        t.transform.localScale = new Vector3(2, 2, 2);
        //t.AddComponent<CapsuleCollider2D>();
        //t.AddComponent<BoxCollider2D>();

        t.AddComponent<PolygonCollider2D>();
        t.AddComponent<treeCollision>();
        //t.AddComponent<CapsuleCollider2D>().isTrigger = true;
        t.tag = "Obstacle";

        t.transform.position = new Vector3(treePos.transform.position.x, treePos.transform.position.y, 0);
        t.AddComponent<groundCon>();
        return t;
    }

    public void restart()
    {
        destoryAll();
    }

    void destoryAll() {
        foreach (GameObject a in obstacles) {
            Destroy(a);
        }
        
    
    }

}
