using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HUD : MonoBehaviour
{
    [SerializeField] private Text scoreLabel;
    static private HUD _instance;
	[SerializeField] private Slider healthBar;
	public Slider HealthBar { get => healthBar; set => healthBar = value; }
 
	public static HUD Instance
	{
     	get
		{
			return _instance;
		}
	}

    private void Awake()
	{
       	_instance = this;
	}

	public void SetScore(string scoreValue)
	{
        scoreLabel.text = scoreValue;
	}

}
