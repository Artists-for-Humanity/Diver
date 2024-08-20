using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownItemScript : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;
    public float force;
    private int damage = 3;
    public PlayerHp Hp;
    private float timer;    
    private PlayerMove movement;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer>3){
            timer = 0;
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision){
        
        if(collision.gameObject.tag == "Player"){
            movement = collision.GetComponent<PlayerMove>();
            if(movement.getDashing()){
                return;
            }
            Hp = collision.gameObject.GetComponent<PlayerHp>();
            Hp.TakeDamage(damage);
            Debug.Log("Damage Taken");
            Destroy(gameObject);
        }
        if((collision.gameObject.tag == "Stage")){
            Destroy(gameObject);
            Debug.Log("Break");
        }
        if(collision.gameObject.tag == "playerAtk"){
            Debug.Log("Damage Taken");
            Destroy(gameObject);
        }
    }
}
