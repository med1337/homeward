using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    private int score = 0;
    public float timeBetweenIncrementing = 0.5f;
    public Text ScoreText;
    public Button PauseButton;
    public Sprite[] buttonImages;
    private bool GamePaused = false;
    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        coroutine = IncrementScore(timeBetweenIncrementing);
        StartCoroutine(coroutine);
        PauseButton.onClick.AddListener(PauseClicked);
    }

    private IEnumerator IncrementScore(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);

            score++;
            ScoreText.text = score.ToString("0") + "m";
        }
    }

    public void PauseClicked()
    {
        if(!GamePaused)
        {
            Time.timeScale = 0;
            PauseButton.image.sprite = buttonImages[1];
            GamePaused = true;
        }
        else
        {
            Time.timeScale = 1;
            PauseButton.image.sprite = buttonImages[0];
            GamePaused = false;
        }
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
