using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Creature,IDestructable
{
  private bool onGround;
  private bool onStair;
  [SerializeField] private float jumpForce;//сила прыжка
  [SerializeField] private float stairSpeed;// скорость движения по лестнице
  [SerializeField] private Transform groundCheck; //трасформ проверки на земле
  [SerializeField] private Transform atackPoint;//трансформ точки атаки
  [SerializeField] private float atackRange;//радиус воклруг точки поражения
  private float hitDelay = 0.4f ;//время после наимации удара мечом нужно для того чтобы урон был в момент срабатывания опускания меча

  public bool OnStair 
  { 
    get { return onStair; }
    set { 
          if (value == true) rigidBody.gravityScale = 0;
          else rigidBody.gravityScale = 1;
          onStair = value;
        }
  }

  private void Awake() 
  {
  animator = GetComponentInChildren<Animator>();
  rigidBody = GetComponent<Rigidbody2D>();
  }
  private void Start()
  {
    health = GameController.Instance.MaxHealth;
  }
  private void Update() 
  {
  onGround = CheckGround();
  MoveKnight();
  AtackKnight();
  Jump();
  }

  void MoveKnight()
  {
    animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));
    Vector2 velocity = rigidBody.velocity;
    velocity.x = Input.GetAxis("Horizontal") * speed;
    rigidBody.velocity = velocity;

    if (OnStair) //движение по лестнице
    {
      velocity = rigidBody.velocity;
      velocity.y = Input.GetAxis("Vertical") * stairSpeed;
      rigidBody.velocity = velocity;
    } 

    if (transform.localScale.x < 0)
    {
      if (Input.GetAxis("Horizontal") > 0) transform.localScale = Vector3.one;
    }
    else
    {
      if (Input.GetAxis("Horizontal") < 0) transform.localScale = new Vector3(-1, 1, 1);
    }
  }

 void AtackKnight()
 {
  if (Input.GetButtonDown("Fire1")) 
  {
    animator.SetTrigger("Atack");
    Invoke("Attack", hitDelay);
  }
 }
 void Jump()
 {
  animator.SetBool("Jump", !onGround);
  if (Input.GetButtonDown("Jump") && onGround)rigidBody.AddForce(Vector2.up * jumpForce);
 }
 private bool CheckGround()
 {
  RaycastHit2D[] hits = Physics2D.LinecastAll(transform.position, groundCheck.position);
  for (int i = 0; i < hits.Length; i++)
  {
    if (!GameObject.Equals(hits[i].collider.gameObject, gameObject)) return true; 
  }
  return false;
  }
  public void Attack()
  {
    Collider2D[] hits = Physics2D.OverlapCircleAll(atackPoint.position, atackRange);
    for (int i = 0; i < hits.Length; i++)  
    {
      if (!GameObject.Equals(hits[i].gameObject, gameObject)) 
      {
        IDestructable destructable = hits[i].gameObject.GetComponent<IDestructable>();
        if (destructable != null) 
        {
          Debug.Log("Hit " +    destructable.ToString());
          destructable.Hit(damage);
          break;
        }
      }
    }   
  } 
}
