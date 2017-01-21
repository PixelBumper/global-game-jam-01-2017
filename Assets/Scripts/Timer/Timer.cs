using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float StartingTimeInSeconds;

    public GameObject m1GameObject;
    public GameObject m2GameObject;
    public GameObject s1GameObject;
    public GameObject s2GameObject;

    public List<Sprite> sprites;

    private float CurrentTimeInSeconds;

    void Start()
    {
        CurrentTimeInSeconds = StartingTimeInSeconds;
    }

    void Update()
    {
        CurrentTimeInSeconds -= Time.deltaTime;

        UpdateUi();

        if (CurrentTimeInSeconds <= 10)
        {
            // TODO: play sound
        }

        if (CurrentTimeInSeconds <= 0)
        {
            GameState.GetGlobalGameState().UnlockGameProgress(GameProgress.HamsterExplode);
        }
    }

    private void UpdateUi()
    {
        int minutes = (int) ((CurrentTimeInSeconds % 3600) / 60);
        int seconds = (int) (CurrentTimeInSeconds % 60);

        m1GameObject.GetComponent<SpriteRenderer>().sprite = sprites[minutes / 10];
        m2GameObject.GetComponent<SpriteRenderer>().sprite = sprites[minutes % 10];

        s1GameObject.GetComponent<SpriteRenderer>().sprite = sprites[seconds / 10];
        s2GameObject.GetComponent<SpriteRenderer>().sprite = sprites[seconds % 10];
    }
}
