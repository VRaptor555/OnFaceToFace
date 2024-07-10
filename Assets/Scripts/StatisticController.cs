using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatisticController : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinText;

    // Update is called once per frame
    void Update()
    {
        _coinText.text = PlayerGameObjects.getMoney().ToString();
    }
}
