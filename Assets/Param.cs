using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Param : MonoBehaviour
{
    // Start is called before the first frame update
    public static readonly float orgSpeed = 12f;
    public float speed;
    public static readonly float orgSpeedUpRange = 100;
    public static float speedUpRange = orgSpeedUpRange;
    public static int upCount = 0;
    private void Start()
    {
        speed = orgSpeed;
        upCount = 0;
    }
    public float getSpeed() {
        return speed;
    }

    public void  speedUp() {
        speed = speed + 4f;
        speedUpRangeNext();
    }

    public float getSpeedUpRange()
    {
        return speedUpRange;
    }

    void speedUpRangeNext()
    {
        upCount += 1;
        speedUpRange = speedUpRange + orgSpeedUpRange * Mathf.Pow(2, upCount);
        print("speed up range == "+ speedUpRange);
        
    }


    public void reset()
    {
        speed = orgSpeed;
        speedUpRange = orgSpeedUpRange;
        upCount = 0;
    }
}
