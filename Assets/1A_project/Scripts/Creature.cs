using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour, IDestructable
{
protected Animator animator; 
	protected Rigidbody2D rigidBody;
	[SerializeField] protected float speed;
    [SerializeField] protected float damage;
	[SerializeField] protected float health = 100;
	public float Health
	{
     	get
    	{
            return health;
        }
    set
        {
            health = value;
        }
	}
	void Awake()
	{
    	animator = gameObject.GetComponentInChildren<Animator>();
     	rigidBody = gameObject.GetComponent<Rigidbody2D>();
	}
 
	public void Die()
	{
    	Destroy(gameObject);
	}
 
	public void Hit(float damage)
	{
 	
    	Health -= damage;
		GameController.Instance.Hit(this);
    	if (Health <= 0)
    	{
        	Die();
    	}
	}
}

