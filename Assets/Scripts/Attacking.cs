using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : MonoBehaviour
{
    private PlayerController _playerController;
    [SerializeField] private float damage = 10;

    private void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }

    public void DamageToPlayer(float damage)
    {
        if (_playerController.GetHealth() <= damage)
        {
            Destroy(_playerController.gameObject);
        }
        else
        {
            _playerController.AdjustHealth(-1 * damage);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DamageToPlayer(damage);
        }
    }
}
