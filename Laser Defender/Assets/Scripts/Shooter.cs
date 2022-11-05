using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifeTime = 3f;
    [SerializeField] float baseFiringRate = 2f;

    [Header("AI")]
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minimumFiringRate = 0.1f;
    [SerializeField] bool useAI = false;

    [HideInInspector]public bool isFiring;
    Coroutine coroutine;
    AudioPlayer audioPlayer;
    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
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
            float timetoNextProjectile = Random.Range(baseFiringRate - firingRateVariance, baseFiringRate + firingRateVariance);
            timetoNextProjectile = Mathf.Clamp(timetoNextProjectile, minimumFiringRate, float.MaxValue);
            audioPlayer.PlayShootingClip();
            yield return new WaitForSeconds(timetoNextProjectile);
        }
    }
}
