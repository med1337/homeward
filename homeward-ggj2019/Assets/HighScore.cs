using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class HighScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = "HIGHSCORE:\n" + PlayerPrefs.GetInt("Score",0) + "M"; 
    }
}
