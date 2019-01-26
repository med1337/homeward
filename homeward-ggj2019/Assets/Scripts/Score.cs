using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int score = 0;
    public float timeBetweenIncrementing = 0.5f;
    public Text ScoreText;


    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        coroutine = IncrementScore(timeBetweenIncrementing);
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //when the ducks die, call:
        //StopCoroutine(coroutine);
    }

    private IEnumerator IncrementScore(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            score += 1;
            ScoreText.text = score.ToString() + "m";
        }
    }
}
