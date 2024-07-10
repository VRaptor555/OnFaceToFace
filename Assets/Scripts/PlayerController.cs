using System;
using UnityEngine;

public sealed class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    [SerializeField] private float speedPlayer;
    [SerializeField] private float distanceDoor = 1.5f;
    
    private Animator anim;
    private SpriteRenderer spriteR;
    private AudioSource stepAudio;
    private string curentAnimTrigger;
    private Rigidbody2D rb;
    private RaycastHit2D hit;
    private string nameNewScene;
    private int heath;
    private HealthBar _healthBar;
    
    private const String SI = "sideIdle";
    private const String SW = "sideWalk";
    private const String UI = "upIdle";
    private const String UW = "upWalk";
    private const String DI = "downIdle";
    private const String DW = "downWalk";

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
        
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        spriteR = GetComponentInParent<SpriteRenderer>();
        anim = GetComponentInParent<Animator>();
        stepAudio = GetComponentInParent<AudioSource>();
        _healthBar = GetComponentInChildren<HealthBar>();
        curentAnimTrigger = "sideIdle";
        anim.SetTrigger(curentAnimTrigger);
        PlayerGameObjects.addMoney(100.0f); // Добавляем денюжку при старте
    }

    // Update is called once per frame
    void Update()
    {   
        // Испускаем 2 луча, вверх и вниз. Если чего нашли в Layer "Door",
        // значит есть дверь/телепорт в который можно войти
        hit = Physics2D.Raycast(rb.position, Vector2.up, distanceDoor, LayerMask.GetMask("Door"));
        if (!hit)
            hit = Physics2D.Raycast(rb.position, Vector2.down, distanceDoor, LayerMask.GetMask("Door"));
        
        // Движение по вертикали и горизонтали
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
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (hit)
            {
                TeleportToScene teleportScript = hit.transform.GetComponent<TeleportToScene>();
                if (teleportScript)
                    teleportScript.ActivateTeleport();
            }
        }
    }

    public float GetHealth()
    {
        return HealthBar.currentValue;
    }

    public void AdjustHealth(float adjust)
    {
        HealthBar.AdjustCurrentValue(adjust);
    }
}
