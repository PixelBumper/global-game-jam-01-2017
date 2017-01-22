using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostItUISprite : MonoBehaviour
{
    private bool _isFullscreen = false;

	void Start () {
        gameObject.SetActive(false);
	}

    public void ConfigureForSprite(Sprite sprite)
    {
        var uiImage = gameObject.GetComponent<Image>();
        uiImage.sprite = sprite;
        var c = uiImage.color;
        c.a = 0.0f;
        uiImage.color = c;
    }

    public void MakeAvailableInInventory()
    {
        gameObject.SetActive(true);
        LeanTween.value(0.0f, 1.0f, 0.3f)
            .setOnUpdate((value) =>
            {
                var uiImage = gameObject.GetComponent<Image>();
                var c = uiImage.color;
                c.a = value;
                uiImage.color = c;
            });

        LeanTween.scale(gameObject, new Vector3(1.1f, 1.1f, 1.0f), 0.2f)
            .setOnComplete(() =>
            {
                LeanTween.scale(gameObject, Vector3.one, 0.2f);
            });
    }

    private Vector2 _previousAnchor;
    private Vector2 _previousParentSizeDelta;

    public void OnClicked()
    {
        if (_isFullscreen)
        {
            InitiateTweenTo(_previousParentSizeDelta.x, _previousAnchor.y, 1.0f, 0.3f);
        }
        else
        {
            var grandParentTransform = gameObject.transform.parent.parent.gameObject.GetComponent<RectTransform>();
            var parentTransform = gameObject.transform.parent.gameObject.GetComponent<RectTransform>();
            var myTransform = gameObject.GetComponent<RectTransform>();

            _previousAnchor = myTransform.anchoredPosition;
            _previousParentSizeDelta = parentTransform.sizeDelta;

            var xScale = grandParentTransform.rect.width / myTransform.rect.width;
            var yScale = grandParentTransform.rect.height / myTransform.rect.height;
            var scaleFactor = Math.Min(xScale, yScale) * 0.6f; // scale to 60% of fullscreen

            InitiateTweenTo(grandParentTransform.sizeDelta.x, 0.0f, scaleFactor, 0.3f);
        }

        _isFullscreen = !_isFullscreen;
    }

    private void InitiateTweenTo(float parentRectTransformDeltaXTarget, float anchoredYTarget, float newScale, float tweenTime)
    {
        // Warning: this rather hackishly modifies the anchor reference frame of the inventory to
        // temporarily fill the grandparents anchor reference frame. This may not be the desired
        // way to do it but for now if delivers the required result: centering the sprite
        var parentTransform = gameObject.transform.parent.gameObject.GetComponent<RectTransform>();
        var rectTransform = gameObject.GetComponent<RectTransform>();
        LeanTween.value(parentTransform.sizeDelta.x, parentRectTransformDeltaXTarget, tweenTime)
            .setEaseInOutCubic()
            .setOnUpdate((value) =>
            {
                var delta = parentTransform.sizeDelta;
                parentTransform.sizeDelta = new Vector2(value, delta.y);
            });
        LeanTween.value(rectTransform.anchoredPosition.y, anchoredYTarget, tweenTime)
            .setEaseInOutCubic()
            .setOnUpdate((value) =>
            {
                var oldAnchor = rectTransform.anchoredPosition;
                rectTransform.anchoredPosition = new Vector2(oldAnchor.x, value);
            });
        LeanTween.scale(gameObject, new Vector3(newScale, newScale, 1.0f), tweenTime)
            .setEaseInOutCubic();
    }
}
