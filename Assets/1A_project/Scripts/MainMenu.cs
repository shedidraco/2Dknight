using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    private void Start()
	{
    	Time.timeScale = 1f;
	}
    public void Play()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
    public void Option()
    {
        
    }
    public void Exit()
    {
       Application.Quit(); 
    }
}
