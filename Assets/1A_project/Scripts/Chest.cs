using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CrystallType { Random, Red, Green, Blue }
public class Chest : MonoBehaviour
{
    [SerializeField] private CrystallType content;
    private int amount;//колличесвто кристалов
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Knight knight = collider.gameObject.GetComponent<Knight>();
        if (knight != null)
        {
            if (content == CrystallType.Random)
            {
                content = (CrystallType)Random.Range(1, 4);
            }
            if (amount == 0)
            {
                amount = Random.Range(1, 6);
            } 
            GameController.Instance.AddNewInventoryItem(content, amount);          
            Destroy(gameObject);
        }       
    }
}
