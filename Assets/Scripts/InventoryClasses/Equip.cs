using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;

public class Equip : MonoBehaviour, IPointerEnterHandler , IPointerExitHandler
{
    private EquipController _equipController;

    [SerializeField] private int equipSlot;

    private Image _image;
    private CanvasRenderer _button;
    private TextMeshProUGUI _text;
    private Component[] _components;
    private TextMeshProUGUI _count;              

    private string _description;
    // Start is called before the first frame update
    void Start()
    {
        _equipController = FindObjectOfType<EquipController>();
        _components = transform.GetComponentsInChildren<Component>(true);
        foreach (var component in _components)
        {
            if (component.CompareTag("InventoryIcon") && component is Image)
                _image = (Image)component;
            if (component.CompareTag("InvBtn") && component is CanvasRenderer)
                _button = (CanvasRenderer)component;
            if (component.CompareTag("InvName") && component is TextMeshProUGUI)
                _text = (TextMeshProUGUI)component;
            if (component.CompareTag("InvCount") && component is TextMeshProUGUI)
                _count = (TextMeshProUGUI)component;
        }
        /*
        _image = GetComponentInChildren<Image>();
        _text = transform.parent.GetComponentInChildren<TMP_Text>();

        _button = transform.parent.GetComponentsInChildren<Component>();
        _count = transform.parent.GetComponentInChildren<TextMeshProUGUI>();
        */
    }

    // Update is called once per frame
    void Update()
    {
        if (equipSlot < _equipController.items.Length)
        {
            var data = _equipController.items[equipSlot].Data;
            var count = _equipController.items[equipSlot].Count;
            if (data is not null)
            {
                _image.sprite = data.Icon;
                _text.text = data.Title;
                _description = data.Description;
                if (count > 1)
                {
                    _button.gameObject.SetActive(true);
                    _count.text = count.ToString();
                }
                else
                {
                    _button.gameObject.SetActive(false);
                    _count.text = "";
                }
            }
            else
            {
                _image.sprite = null;
                _text.text = "";
                _description = "";
                _button.gameObject.SetActive(false);
                _count.text = "";
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse Under Icon #"+equipSlot);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse Out Icon #"+equipSlot);
    }
}
