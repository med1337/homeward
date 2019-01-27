using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] Score score;

    [SerializeField] GameObject gameOverUI;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("MusicToggle") == 0)
            transform.GetComponent<AudioSource>().enabled = false;
        else
            transform.GetComponent<AudioSource>().enabled = true;

        AudioManager.instance.ambientSource1.Stop();
        AudioManager.instance.ambientSource2.Stop();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        score.StopAllCoroutines();

        gameOverUI.SetActive(true);

        if (PlayerPrefs.GetInt("Score", 0) < score.GetScore())
        {
            PlayerPrefs.SetInt("Score", score.GetScore());
        }
    }
}
