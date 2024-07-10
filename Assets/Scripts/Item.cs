using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private BaseItemData itemData;

    private InventoryController _inventoryController;
    // Start is called before the first frame update
    private void Awake()
    {
        if (PlayerGameObjects.findObject(gameObject.name))
            Destroy(gameObject);
    }

    void Start()
    {
        _inventoryController = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _inventoryController.AddItem(itemData, 1);
            PlayerGameObjects.addToList(gameObject.name);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _inventoryController.AddItem(itemData, 1);
            Destroy(gameObject);
        }
    }

}
