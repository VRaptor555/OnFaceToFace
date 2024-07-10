using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public static class PlayerGameObjects
{
    private static ListDictionary listObjectToDestroy = new ListDictionary();
    private static float money = 0;

    public static void addToList(string obj)
    {
        listObjectToDestroy.Add(obj, 1);
    }

    public static bool findObject(string obj)
    {
        return listObjectToDestroy.Contains(obj);
    }

    public static float getMoney()
    {
        return money;
    }

    public static void addMoney(float _money)
    {
        money += _money;
    }

    public static bool giveMoney(float _money)
    {
        if (money < _money)
            return false;
        money -= _money;
        return true;
    }
}
