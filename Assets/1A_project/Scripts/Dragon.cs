using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Creature,IDestructable
{
   
    [SerializeField] private CircleCollider2D hitCollider;
    private void Awake() 
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>(); 
    }
    private void Update() 
    {
        MoveDragon();
    }
    void MoveDragon()
    {
        Vector2 velocity = rigidBody.velocity;
  	    velocity.x = speed * transform.localScale.x * -1;
       	rigidBody.velocity = velocity;
    }

    private void OnTriggerStay2D(Collider2D collider) 
    {  
        Knight knight =  collider.gameObject.GetComponent<Knight>();
        if (knight != null) animator.SetTrigger("Atack");
        else ChangeDirection();
    }
    void ChangeDirection()
    {
        if (transform.localScale.x < 0)transform.localScale = new Vector3(1.3f,1.3f,1);//даркон смотрит в право с учетом моштаба спрайта
    	else transform.localScale = new Vector3(-1.3f, 1.3f, 1);//дракон смотрит в лево с учетом маштаба
    }
    public void Attack()	
    {
        Vector3 hitPosition = transform.TransformPoint(hitCollider.offset);
        Collider2D[] hits = Physics2D.OverlapCircleAll(hitPosition, hitCollider.radius);
 
        for (int i = 0; i < hits.Length; i++)  
        {
            if (!GameObject.Equals(hits[i].gameObject, gameObject))
            {
                IDestructable destructable = hits[i].gameObject.GetComponent<IDestructable>();
             	if (destructable != null) destructable.Hit(damage);	
        	}
        }
    }
}
