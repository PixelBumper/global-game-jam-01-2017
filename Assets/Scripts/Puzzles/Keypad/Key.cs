using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SpriteRenderer))]
public class Key : MonoBehaviour
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

    private void OnMouseUpAsButton()
    {
        if (active)
        {
            transform.parent.gameObject.SendMessage("PressedKey", name);
        }
    }

    private void OnMouseDown()
    {
        if (active)
        {
            _renderer.sprite = pressed;
        }
    }

    private void OnMouseUp()
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