using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTime : MonoBehaviour {

    public GameObject SecondsOne;
    public GameObject SecondsTen;

    public List<Sprite> sprites;

    public void SetValueAndFloat(int seconds)
    {
        SecondsTen.GetComponent<SpriteRenderer>().sprite = GetSprite(seconds / 10);
        SecondsOne.GetComponent<SpriteRenderer>().sprite = GetSprite(seconds % 10);

        LeanTween.scale(gameObject, new Vector3(1f, 1f, 1f), 0.25f);
        LeanTween.moveY(gameObject, gameObject.transform.position.y + 3f, 2.5f).setEaseOutQuad();
        LeanTween.alpha(gameObject, 0f, 2.25f).setOnComplete(() => Destroy(gameObject));
    }

    private Sprite GetSprite(int index)
    {
        return index < 0 ? sprites[0] : sprites[index];
    }
}
