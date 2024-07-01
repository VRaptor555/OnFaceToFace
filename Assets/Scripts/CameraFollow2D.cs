using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [SerializeField] private float damping = 1.5f;
//    private Vector2 offset = new Vector2(2f, 1f);
    private bool faceLeft;
    private Transform player;
    private int lastX;
    private int lastY;

    void Start ()
    {
        FindPlayer();
    }

    public void FindPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastX = Mathf.RoundToInt(player.position.x);
        lastY = Mathf.RoundToInt(player.position.y);
        transform.position = new Vector3(lastX, lastY, transform.position.z);
    }

    void Update () 
    {
        if(player)
        {
            Vector3 target;
            target = new Vector3(player.position.x, player.position.y, transform.position.z);
            Vector3 currentPosition = Vector3.Lerp(transform.position, target, damping * Time.deltaTime);
            transform.position = currentPosition;
        }
    }}
