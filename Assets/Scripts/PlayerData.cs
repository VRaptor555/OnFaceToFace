using UnityEngine;
using System;

public class PlayerData
{
    public static Action<float> OnHealthChanged = default;

    private static float _healthAmount = 100f;
    

    public static float HealthAmount {
        get => _healthAmount;
        set {
            _healthAmount = Mathf.Clamp(value, 0f, 100f);
            OnHealthChanged?.Invoke(_healthAmount);
        }
    }
}
