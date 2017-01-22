using UnityEngine;

public class LightSwitch : MonoBehaviour {

    private void OnMouseDown()
    {
        GameState.GetGlobalGameState().UnlockGameProgress(GameProgress.HamsterExplode);
    }
}
