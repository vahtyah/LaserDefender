using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifeTime = 3f;
    [SerializeField] float firingRate = 2f;
    [SerializeField] bool useAI = false;

    public bool isFiring;
    Coroutine coroutine;
    private void Start()
    {

        if (useAI) isFiring = true;
    }
    private void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if (isFiring && coroutine == null)
            coroutine = StartCoroutine(FireContinously());
        else if (!isFiring && coroutine != null)
        {
            print("Tuan");
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    IEnumerator FireContinously()
    {
        while (true)
        {
            GameObject instance = Instantiate(projectilePrefab,transform.position,Quaternion.identity);
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if(rb) rb.velocity = transform.up * projectileSpeed;
            Destroy(instance, projectileLifeTime);
            yield return new WaitForSeconds(firingRate);
        }
    }
}
