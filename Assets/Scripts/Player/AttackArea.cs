using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackArea : MonoBehaviour
{
    public int damage = 1;
    public MeanieHp Hp;
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Meanie"){
            Hp.TakeDamage(damage);
            Debug.Log("Enemy Hit");
        }
    }
}
