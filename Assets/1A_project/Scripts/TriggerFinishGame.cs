using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerFinishGame : MonoBehaviour
{ private void OnTriggerEnter2D(Collider2D collider) 
    {
        Knight knight = collider.gameObject.GetComponent<Knight>();
        if (knight != null)
        {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }     
    }
}
