using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCon : MonoBehaviour
{

   
  
    public DinoCon dino;
   
    // Start is called before the first frame update
    private Param param;
    senceCon sence;
    UiCon ui;
    private List<int> speedUpList;
    public AudioClip speedUpClip;
    void Start()
    {
        
        ui = GetComponent<UiCon>();
        sence = GetComponent<senceCon>();
        param = GameObject.FindGameObjectsWithTag("script")[0].GetComponent<Param>();
        sence.init();

        ui.init();
        ui.createScore();
        ui.createHiScore();
        speedUpList = new List<int>(new int[] { 100, 300,600,900 });

    }

    // Update is called once per frame
    void Update()
    {

        if (dino.isDead) {
            ui.deadUi();
            return;
        }

        if (checkSpeedUp()) {
          
            param.speedUp();
        }
        sence.createGround();
        sence.createObstacle();
        sence.createCloud();

        ui.updateScore(param.getSpeed());

    }

    bool checkSpeedUp()
    {
        if (ui.getScore() >= param.getSpeedUpRange()) {
            AudioSource.PlayClipAtPoint(speedUpClip, transform.position, 1);
            return true;
            
        }
      
        return false;
    }

    public void restartGame() {


        dino.restart();
        sence.restart();
        ui.restartSet();
        param.reset();
    }

   
   
   
 



}
