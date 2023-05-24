using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiCon : MonoBehaviour
{

    private float score;
    private float hiScore;
    public GameObject scoreDis, hiScoreDis;
    public GameObject scorePos,gameOver,restart,hiScorePos,hiTextPos;
    private int scorerange;
    private List<GameObject> scores;
    private List<GameObject> hiScores;
    public AudioClip checkPoint;
   // Start is called before the first frame update

   public void init()
    {
        scores = new List<GameObject>();
        hiScores = new List<GameObject>();
        scorerange = 22;
        score = 0;
        hiScore = 0;

    }
    // Update is called once per frame
   

    public void updateScore(float speed)
    {
        score += speed* Time.deltaTime;
        writeAllNumber((int)score, scores);

    }

    public void createScore()
    {
       // scores.Add(scoreDis);
        //float x = scoreDis.GetComponent<RectTransform>().anchoredPosition.x;
        for (int i = 0; i < 5; i++)
        {
            GameObject a = Instantiate(scoreDis, scorePos.transform);
            scores.Add(a);
            //a.GetComponent<RectTransform>().anchoredPosition = new Vector3(scorerange * (i + 1) + x, 0, 0);

        }
    }

    

    public void createHiScore()
    {
        createHiText();
        //float x = hiScoreDis.GetComponent<RectTransform>().anchoredPosition.x;
        //hiScores.Add(hiScoreDis);
        for (int i = 0; i < 5; i++)
        {
            GameObject a = Instantiate(hiScoreDis, hiScorePos.transform);
            hiScores.Add(a);
            //a.GetComponent<RectTransform>().anchoredPosition = new Vector2(scorerange * (i + 1) + x, 0);
        }

        
        writeAllNumber(0, hiScores);
    }

    void createHiText()
    {
        GameObject textH = Instantiate(hiScoreDis, hiTextPos.transform);
        GameObject textI = Instantiate(hiScoreDis, hiTextPos.transform);
       
        writeScore(textH, 11);
        writeScore(textI, 10);
        /*
        hiScores[0].GetComponent<RectTransform>().anchoredPosition += new Vector2(-scorerange, 0);


        hiScores.RemoveAt(0);
        hiScores[0].GetComponent<RectTransform>().anchoredPosition += new Vector2(-scorerange, 0);
        hiScores.RemoveAt(0);
        */
    }
    void writeScore(GameObject score, int number)
    {
        Rect r = new Rect(score.GetComponent<RawImage>().uvRect);
        r.x = 0.083f * number;
        score.GetComponent<RawImage>().uvRect = r;
    }

    void writeAllNumber(int nu, List<GameObject> sc)
    {
        int div = 10000;
        int count = sc.Count-1;
        for (int i = count; i > -1; i--)
        {
            int n = nu / div;
            nu -= n * div;
            writeScore(sc[i], n);
            div = div / 10;
        }
    }

    public void deadUi()
    {
        gameOver.SetActive(true);
        restart.SetActive(true);

    }

    public void restartSet() {

        gameOver.SetActive(false);
        restart.SetActive(false);
        hiScore = Mathf.Max(hiScore, score);
        writeAllNumber((int)hiScore,hiScores);
        writeAllNumber(0, scores);


        score = 0;
    }

    public float getScore() {
        return score;
    }
}
