using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "newItem", menuName = "Data/Items/Collectable", order = 51)]
public class CollectableItemData : BaseItemData {
    // Коллекционируемые предметы, это те, 
    //   которые складываются в одну ячейку инвентаря 
    //   и постепенно увеличивается счетчик предметов в ячейке
    
    // Максимальное кол-во предметов в одной пачке
    public int MaxCollectionCount;
    
    // Обратите внимание на override - это специальный модификатор метода, 
//    который говорит о том, что мы хотим переопределить логику 
//    выполнения метода в базовом классе.
// Тем самым, когда мы будем добавлять предметы, 
//     созданные на основе CollectableItemData, 
//     они будут добавляться по логике представленной ниже, 
//     а все остальные по логике описанной в классе BaseItemData, 
//     если это было не определено иначе.
public override void PutToInventory(InventoryController inventory, int count, Func<int, ItemState> putNewItem)
{
    // Сперва попробуем найти все незаполненные коллекции нужного типа
    // Вас может удивить почему это мы сравниваем типы объектов? 
    // Как было замечено в предыдущей статье,
    //    ссылки на один и тот же ScriptableObject могут быть разные 
    //    в зависимости от того, как они были загружены в сцену
    //  Поэтому надежнее будет сравнивать конкретные типы и идентификаторы предметов
    var notFullCollections = inventory.items.Where(
    	state => state.Data is not null
        && state.Data.GetType() == GetType() 
        && state.Data.Title == Title
    	&& state.Count < MaxCollectionCount
    ).ToList();
    var remainingCount = count;
    
    foreach (var state in notFullCollections) {
        // Сколько максимальное кол-во предметов мы можем добавить 
        //     в незаполненную ячейку инвентаря:
        //  Это может быть либо фактически оставшееся кол-во предметов для добавления
        //     или кол-во свободных слотов в коллекции предметов
        var countToPut = Math.Min(remainingCount, MaxCollectionCount - state.Count);
        
        // Добавляем предметы в коллекцию
        state.Count += countToPut;
        // Уменьшаем кол-во предметов, которые осталось распределить по ячейкам
        remainingCount -= countToPut;
        
        if (remainingCount <= 0) {
            // Завершаем логику, если все предметы были распределены
            return;
        }
    }
    
    // Если есть еще что распределять то, добавляем предметы, как новые в инвентарь
    while (remainingCount > 0) {
        var countToPut = Math.Min(remainingCount, MaxCollectionCount);
        var state = putNewItem(countToPut);
        
        // Если не смогли добавить предмет в инвентарь, завершаем логику добавления
        if (state == null) {
            return;
        }
        
        // Уменьшаем кол-во предметов, которые осталось распределить по ячейкам
        remainingCount -= countToPut;
    }
}
}