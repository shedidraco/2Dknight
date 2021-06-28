using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Play, Pause }
public delegate void InventoryUsedCallback(InventoryItem item); 
public class GameController : MonoBehaviour
{
    private Knight _knight;
    [SerializeField] private  float  maxHealth;
    private List<InventoryItem> inventory;
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

    public Knight Knight { get => _knight; set => _knight = value; }

    private void Awake() 
    {
        inventory = new List<InventoryItem>();
        _instance = this;
        state = GameState.Play;
    } 
    private void Start() 
    {
        HUD.Instance.HealthBar.maxValue = maxHealth;
        HUD.Instance.HealthBar.value = maxHealth;
        HUD.Instance.SetScore(Score.ToString());
        HUD.Instance.UpdateCharacterValues(MaxHealth, _knight.Speed, _knight.Damage);   
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
        }
        if(victim.GetType() == typeof(Knight))
    	{
            HUD.Instance.HealthBar.value = victim.Health;
    	}

	}
    public void AddNewInventoryItem(CrystallType type, int amount)//метод который будет получать в качестве параметра тип и количество кристаллов
    {
        InventoryItem newItem = HUD.Instance.AddNewInventoryItem(type, amount);
        InventoryUsedCallback callback = new InventoryUsedCallback(InventoryItemUsed);
        newItem.Callback = callback;
        inventory.Add(newItem);
    }
    public void InventoryItemUsed(InventoryItem item)
    {
        switch (item.CrystallType)
        {
        case CrystallType.Blue:
        _knight.Speed += item.Quantity / 10f;
        break;
        case CrystallType.Red:
        _knight.Damage += item.Quantity / 10f; 
        break;
        case CrystallType.Green:
        MaxHealth += item.Quantity / 10f;
        _knight.Health = MaxHealth;
        HUD.Instance.HealthBar.maxValue = MaxHealth;
        HUD.Instance.HealthBar.value = MaxHealth;
        break;
        default:
                        Debug.LogError("Wrong crystall type!");
        break;
        }
        inventory.Remove(item); // удаляем ссылку на предмет инвентаря из массива
        Destroy(item.gameObject);  //уничтожаем геймобджект предмета инвентаря
        HUD.Instance.UpdateCharacterValues(_knight.Health, _knight.Speed, _knight.Damage);
    }
}
