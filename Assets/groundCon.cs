using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundCon : MonoBehaviour
{
    // Start is called before the first frame update
   
   
    private DinoCon dino;
    private Param param;
    void Start()
    {
        param = GameObject.FindGameObjectsWithTag("script")[0].GetComponent<Param>();
        dino = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<DinoCon>();

       
     
    }

    // Update is called once per frame
    void Update()
    {
        if (dino.isDead) {
            Animator an = GetComponent<Animator>();
            if (an!= null) {
                an.enabled = false;
            }
            return;
        }
        Vector3 move = Vector2.left * param.getSpeed()*Time.deltaTime;
        transform.position = transform.position+ move;
        //跑出畫面外? 設定coliser 撞到destory
        if (transform.position.x * -1> 48)
        {
            Destroy(gameObject);
        }
    }

}
