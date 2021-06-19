using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HUD : MonoBehaviour
{
	[SerializeField]private Transform inventoryContainer;
	[SerializeField]private InventoryItem inventoryItemPrefab;
	[SerializeField] private GameObject inventoryWindow;
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
	public void ShowWindow(GameObject window)
	{
		window.GetComponent<Animator>().SetBool("Open", true);
		GameController.Instance.State = GameState.Pause;
	}

	public void HideWindow(GameObject window)
	{
		window.GetComponent<Animator>().SetBool("Open", false);
		GameController.Instance.State = GameState.Play;
	}
	public InventoryItem AddNewInventoryItem(CrystallType crystallType, int amount)
	{
		InventoryItem newItem = Instantiate(inventoryItemPrefab) as InventoryItem;
		newItem.transform.SetParent(inventoryContainer);
		newItem.Quantity = amount;
		newItem.CrystallType = crystallType;
		return newItem;
	}
}
