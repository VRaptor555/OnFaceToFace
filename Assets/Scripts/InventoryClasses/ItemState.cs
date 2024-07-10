using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Этот класс отвечает за хранение состояния ячейки инвентаря с предметом
[Serializable]
public class ItemState {
    // Информация о хранимом предмете
    public BaseItemData Data;
    
    // Кол-во предметов определенного типа в ячейке
    public int Count;
}