using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackArea : MonoBehaviour
{
    private int damage = 1;
    private void onTriggerEnter2D(Collider2D collider){
        Debug.Log("Hit Something!");
    }
}
