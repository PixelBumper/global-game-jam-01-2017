using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SpriteRenderer))]
public class Key : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private SpriteRenderer _renderer;

    private Color _normalColor;

    // Use this for initialization
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _normalColor = _renderer.color;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _renderer.color = new Color(0, 100, 0);
//        Debug.LogError("Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _renderer.color = _normalColor;
//        Debug.LogError("Exit");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.LogError("sending number");
        transform.parent.gameObject.SendMessage("PressedKey", name);
    }
}