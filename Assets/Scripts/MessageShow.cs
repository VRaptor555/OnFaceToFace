using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageShow : MonoBehaviour
{
    private Canvas canvas;
    private Transform _transform_icon;
    private Image _image;
    private TextMeshProUGUI _text;
    void Start()
    {
        canvas = GetComponent<Canvas>(); //Получение компонента Canvas
        FindItemInChild();
        _text = gameObject.transform.GetComponentInChildren<TextMeshProUGUI>();
        canvas.enabled = false; //Отключение при старте
    }

    public void showMessage(Sprite icon, string text)
    {
        if (_image)
            _image.sprite = icon;
        _text.text = text;
        StartCoroutine(ShowMessageAsync());
    }

    private IEnumerator ShowMessageAsync()
    {
        canvas.enabled = true;
        yield return new WaitForSeconds(4.0f);
        canvas.enabled = false;
    }

    void FindItemInChild()
    {
        foreach (Image child in gameObject.transform.GetComponentsInChildren<Image>())
        {
            if (child.CompareTag("InventoryIcon"))
            {
                _image = child;
                break;
            }
        }
    }
}
