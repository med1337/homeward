using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ButtonClicks : MonoBehaviour
{
    public Button PlayButton;
    public Button QuitButton;
    public Button SettingsButton;

    public AudioSource TempSettingsAudio;

    // Start is called before the first frame update
    void Start()
    {
        PlayButton.onClick.AddListener(PlayClicked);
        SettingsButton.onClick.AddListener(SettingsClicked);
        QuitButton.onClick.AddListener(QuitClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void PlayClicked()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    void SettingsClicked()
    {
        TempSettingsAudio.PlayOneShot(TempSettingsAudio.clip);
    }

    void QuitClicked()
    {
        Application.Quit();
    }
}
