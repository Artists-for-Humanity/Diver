using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeanieDamage : MonoBehaviour{
    public int damage;
    public PlayerHp Hp;
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Player"){
            Hp.TakeDamage(damage);
            Debug.Log("Damage Taken");
        }
    }
}
