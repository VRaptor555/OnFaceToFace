using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class CameraFollow2D: MonoBehaviour
{
    public static CameraFollow2D Instance { get; private set; }
    private Transform target;
    public float lerpSpeed = 1.0f;

    private Vector3 offset;

    private Vector3 targetPos;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
    }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        if (target is null) return;

        offset = transform.position - target.position;
    }

    private void Update()
    {
        if (target is null) return;
        targetPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPos, lerpSpeed * Time.deltaTime);
    }

}