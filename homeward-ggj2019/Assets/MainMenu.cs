using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        
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
        //transform.Find("MUSIC OFF").GetComponent<TextMeshPro>()
    }
    public void ToggleFX()
    {

    }
}
