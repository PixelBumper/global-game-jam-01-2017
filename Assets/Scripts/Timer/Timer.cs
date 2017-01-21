using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float StartingTimeInSeconds;

    public GameObject m1GameObject;
    public GameObject m2GameObject;
    public GameObject s1GameObject;
    public GameObject s2GameObject;

    public Sprite sprite0;
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite4;
    public Sprite sprite5;
    public Sprite sprite6;
    public Sprite sprite7;
    public Sprite sprite8;
    public Sprite sprite9;

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

        m1GameObject.GetComponent<SpriteRenderer>().sprite = GetSpriteForNumber(minutes / 10);
        m2GameObject.GetComponent<SpriteRenderer>().sprite = GetSpriteForNumber(minutes % 10);

        s1GameObject.GetComponent<SpriteRenderer>().sprite = GetSpriteForNumber(seconds / 10);
        s2GameObject.GetComponent<SpriteRenderer>().sprite = GetSpriteForNumber(seconds % 10);
    }

    private Sprite GetSpriteForNumber(int number)
    {
        switch (number)
        {
            case 0:
                return sprite0;
            case 1:
                return sprite1;
            case 2:
                return sprite2;
            case 3:
                return sprite3;
            case 4:
                return sprite4;
            case 5:
                return sprite5;
            case 6:
                return sprite6;
            case 7:
                return sprite7;
            case 8:
                return sprite8;
            case 9:
                return sprite9;
            default:
                throw new Exception("Should never happen");
        }
    }
}
