using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D: MonoBehaviour
{
    private Transform target;
    public float lerpSpeed = 1.0f;

    private Vector3 offset;

    private Vector3 targetPos;

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