using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagerDealer : MonoBehaviour
{
    [SerializeField] int damager = 10;

    public int GetDamage()
    {
        return damager;
    }
    public void Hit()
    {
        Destroy(gameObject);
    }
}
