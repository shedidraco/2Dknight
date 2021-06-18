using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Play, Pause }
public class GameController : MonoBehaviour
{

    [SerializeField] private  float  maxHealth;
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    private GameState state;
    private int score;
    private int Score
    {
     	get
        {
         	return score;
        }
     	set
        {
         	if (value != score)
        	{
            	score = value;
           	    HUD.Instance.SetScore(score.ToString());
        	}
        }
	} 
    public GameState State
    {
        get
        {
            return state;
        }
        set
        {
            if (value == GameState.Play)
            {
                Time.timeScale = 1.0f;
            }
            else
            {
                Time.timeScale = 0.0f;
            }
            state = value;
        }
    }


    [SerializeField] private int dragonHitScore; 
    [SerializeField] private int dragonKillScore;
    private static GameController _instance;
    public static GameController Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake() 
    {
        _instance = this;
        state = GameState.Play;
    } 
    private void Start() 
    {
        HUD.Instance.HealthBar.maxValue = maxHealth;
        HUD.Instance.HealthBar.value = maxHealth;
        HUD.Instance.SetScore(Score.ToString());
    }
    public void Hit(IDestructable victim)
	{
    	if (victim.GetType() == typeof(Dragon))
    	{
            if (victim.Health > 0)
        	{
            	//дракон получил урон
                Score += dragonHitScore;
            }
         	else
        	{
            	//дракон убит
                Score += dragonKillScore;
        	}
        	Debug.Log("Dragon hit.Current score " +score);
        }
        if(victim.GetType() == typeof(Knight))
    	{
            HUD.Instance.HealthBar.value = victim.Health;
    	}

	}
}
