using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    private InventoryUsedCallback callback;
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private Text label;
    [SerializeField] private Text count;
    [SerializeField] private Image gemImg;
    private CrystallType crystallType;
    private float quantity;
    public CrystallType CrystallType { get => crystallType; set => crystallType = value; }
    public float Quantity { get => quantity; set => quantity = value; }
    public InventoryUsedCallback Callback { get => callback; set => callback = value; }

    private void Start()
    {
        SpriteToObject();
        gameObject.GetComponent<Button>().onClick.AddListener( () => callback(this) );
    }
    void SpriteToObject()
    {
       string spriteNameToSearch = crystallType.ToString().ToLower();
        gemImg.sprite = sprites.Find ( x => x.name.Contains(spriteNameToSearch) );
        label.text = spriteNameToSearch;
        count.text = Quantity.ToString();
    }
}
