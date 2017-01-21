using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SpriteRenderer))]
public class Key : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler,
    IPointerUpHandler
{
    private SpriteRenderer _renderer;

    private Color _normalColor;

    public bool active;

    public Sprite normal;

    public Sprite pressed;

    public Sprite glowing;

    // Use this for initialization
    void Start()
    {
        active = true;
        _renderer = GetComponent<SpriteRenderer>();
        _normalColor = _renderer.color;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
//        _renderer.color = new Color(0, 100, 0);
//        Debug.LogError("Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
//        _renderer.color = _normalColor;
//        Debug.LogError("Exit");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (active)
        {
            Debug.LogError("sending number");
            transform.parent.gameObject.SendMessage("PressedKey", name);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (active)
        {
            _renderer.sprite = pressed;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (active)
        {
            SetNormalState();
        }
    }

    public void DisallowUsage()
    {
        active = false;
    }

    public void AllowUsage()
    {
        active = true;
    }

    public void StartGlowing()
    {
        _renderer.sprite = glowing;
    }

    public void SetNormalState()
    {
        _renderer.sprite = normal;
    }

    public void SetPressedState()
    {
        _renderer.sprite = pressed;
    }
}