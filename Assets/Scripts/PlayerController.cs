using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speedPlayer;
    
    private Animator anim;
    private SpriteRenderer spriteR;
    private AudioSource stepAudio;
    private string curentAnimTrigger;
    
    private const String SI = "sideIdle";
    private const String SW = "sideWalk";
    private const String UI = "upIdle";
    private const String UW = "upWalk";
    private const String DI = "downIdle";
    private const String DW = "downWalk";
    
    void Start()
    {
        spriteR = GetComponentInParent<SpriteRenderer>();
        anim = GetComponentInParent<Animator>();
        stepAudio = GetComponentInParent<AudioSource>();
        curentAnimTrigger = "sideIdle";
        anim.SetTrigger(curentAnimTrigger);
    }

    // Update is called once per frame
    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        if (Math.Round(horizontal, 3) == 0 && Math.Round(vertical, 3) == 0)
        {
            if (!curentAnimTrigger.Contains("Idle"))
            {
                if (curentAnimTrigger == SW)
                    curentAnimTrigger = SI;
                else if (curentAnimTrigger == UW)
                    curentAnimTrigger = UI;
                else
                    curentAnimTrigger = DI;
                anim.SetTrigger(curentAnimTrigger);
                if (stepAudio.isPlaying)
                    stepAudio.Stop();
            }
        }
        else
        {
            if (Math.Round(horizontal, 3) != 0)
            {
                spriteR.flipX = horizontal > 0;
                if (curentAnimTrigger != SW)
                {
                    curentAnimTrigger = SW;
                    anim.SetTrigger(curentAnimTrigger);
                    if (!stepAudio.isPlaying)
                        stepAudio.Play();
                }
            }
            else
            {
                if (Math.Round(vertical, 3) > 0)
                {
                    if (curentAnimTrigger != UW)
                    {
                        curentAnimTrigger = UW;
                        anim.SetTrigger(curentAnimTrigger);
                        if (!stepAudio.isPlaying)
                            stepAudio.Play();
                    }
                }
                else
                {
                    if (curentAnimTrigger != DW)
                    {
                        curentAnimTrigger = DW;
                        anim.SetTrigger(curentAnimTrigger);
                        if (!stepAudio.isPlaying)
                            stepAudio.Play();
                    }
                }
            }
            transform.Translate(Vector2.right * (horizontal * speedPlayer * Time.deltaTime));
            transform.Translate(Vector2.up * (vertical * speedPlayer * Time.deltaTime));
        }
        
    }
}
