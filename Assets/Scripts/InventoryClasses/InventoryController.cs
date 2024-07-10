using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Класс отвечающий за логику работы инвентаря
public class InventoryController : MonoBehaviour {
    // Размер инвентаря по ширине
    public const int Width = 4;
    // Размер инвентаря по высоте
    public const int Height = 3;
    // Список состояний ячеек в инвентаре
    public ItemState[] items;
    
    private Canvas canvas; 
    // Метод для добавления определенного кол-ва предметов в инвентарь
    public void AddItem(BaseItemData item, int count) {
        // Вызовем метод предмета, чтобы он мог использовать
        //     свою логику добавления предмета в инвентарь.
        // Здесь мы передаем в метод ссылку на инвентарь, 
        //    для того, чтобы предмет мог анализировать состояние инвентаря.
        // Также передаем информацию о том, сколько предметов было подобрано
        // И функцию для реального размещения предмета в инвентаре
        // Подробная реализация будет описана ниже
        item.PutToInventory(this, count, (countToPut) => PutNewItem(item, countToPut));
    }

    public void DelItem(BaseItemData item, int count)
    {
        item.DelFromInventory(this, count, (countToDel) => DelFromItem(item, countToDel));
    }

    private ItemState DelFromItem(BaseItemData item, int count)
    {
        int count_ost = count;
        for (int i = items.Length-1; i >= 0; i--)
        {
            if (items[i].Data == item)
            {
                if (items[i].Count > count_ost)
                {
                    items[i].Count -= count_ost;
                    return items[i];
                }
                count_ost -= items[i].Count;
                items[i].Data = null;
                items[i].Count = 0;
            }
        }
        return null;
    }
    
    // Внутренний метод для заполнения свободной ячейки инвентаря
    private ItemState PutNewItem(BaseItemData item, int count) {
        var state = FindEmptyState();
        if (state == null) {
            // Если не смогли получить свободную ячейку, значит инвентарь заполнен
            // Вы можете обрабатывать ошибки любым другим способом
            // Но для упрощения мы просто выведем информацию в лог
            Debug.Log("Inventory is full");
            return null;
        }
        
        state.Data = item;
        state.Count = count;
        
        return state;
    }

    private ItemState FindEmptyState()
    {
        foreach (var item in items)
        {
            if (item.Data is null)
            {
                return item;
            }
        }
        return null;
    }

    private void Start()
    {
        //items = new List<ItemState>();
    }
}
