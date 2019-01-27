using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class MainMenu : MonoBehaviour
{
    public GameObject panelWithMusicAudioSource; 

    private void Start()
    {
        Application.targetFrameRate = 120;
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Application.Quit(); 
    }
    public void ToggleMusic()
    {
        if (PlayerPrefs.GetInt("MusicToggle") == 0)
        {
            GameObject.Find("MUSIC OFF").GetComponent<TextMeshProUGUI>().text = "MUSIC ON";
            PlayerPrefs.SetInt("MusicToggle", 1);
            panelWithMusicAudioSource.GetComponent<AudioSource>().enabled = true; 
        }
        else
        {
            GameObject.Find("MUSIC OFF").GetComponent<TextMeshProUGUI>().text = "MUSIC OFF";
            PlayerPrefs.SetInt("MusicToggle", 0);
            panelWithMusicAudioSource.GetComponent<AudioSource>().enabled = false;
        }
    }
    public void ToggleFX()
    {
        if (PlayerPrefs.GetInt("FXToggle") == 0)
        {
            GameObject.Find("SOUND FX OFF").GetComponent<TextMeshProUGUI>().text = "SOUND FX ON";
            PlayerPrefs.SetInt("FXToggle", 1);
        }
        else
        {
            GameObject.Find("SOUND FX OFF").GetComponent<TextMeshProUGUI>().text = "SOUND FX OFF";
            PlayerPrefs.SetInt("FXToggle", 0);
        }
    }
}
