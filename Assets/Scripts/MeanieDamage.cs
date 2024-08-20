using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeanieDamage : MonoBehaviour{
    public int damage;
    public PlayerHp Hp;
    private PlayerMove player;
    private void OnCollisionEnter2D(Collision2D collider){
        if(collider.gameObject.tag == "Player"){
            player = collider.gameObject.GetComponent<PlayerMove>();
            if(player.getDashing()){
                return;
            }
            Hp.TakeDamage(damage);
            Debug.Log("Damage Taken");
        }
    }
    private void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            player = collider.GetComponent<PlayerMove>();
            if(player.getDashing()){
                return;
            }
            Hp.TakeDamage(damage);
            Debug.Log("Damage Taken");
        }
    }
}
