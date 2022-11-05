using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamagerDealer damager = collision.GetComponent<DamagerDealer>();
        if (damager)
        {
            TakeDamage(damager.GetDamage());
            damager.Hit();
        }
    }

    private void TakeDamage(int damage)
    {
        health -= damage;
        if(health <=0 ) Destroy(gameObject);
    }
}
