using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public GameObject m1GameObject;
    public GameObject m2GameObject;
    public GameObject s1GameObject;
    public GameObject s2GameObject;

    public List<Sprite> sprites;

    public void UpdateTime(int time)
    {
        var minutes = (time % 3600) / 60;
        var seconds = time % 60;

        m1GameObject.GetComponent<SpriteRenderer>().sprite = GetSprite(minutes / 10);
        m2GameObject.GetComponent<SpriteRenderer>().sprite = GetSprite(minutes % 10);

        s1GameObject.GetComponent<SpriteRenderer>().sprite = GetSprite(seconds / 10);
        s2GameObject.GetComponent<SpriteRenderer>().sprite = GetSprite(seconds % 10);
    }

    private Sprite GetSprite(int index)
    {
        return index < 0 ? sprites[0] : sprites[index];
    }
}
