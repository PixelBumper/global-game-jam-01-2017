using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{

    public List<GameObject> LightSwitches;


    private void OnMouseDown()
    {
        GameState.GetGlobalGameState().UnlockGameProgress(GameProgress.HamsterExplode);
        foreach (var lightSwitch in LightSwitches)
        {
            lightSwitch.SetActive(!lightSwitch.activeSelf);
        }
    }
}
