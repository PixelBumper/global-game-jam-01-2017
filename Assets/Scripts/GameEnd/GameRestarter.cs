using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRestarter : MonoBehaviour {
    private void OnMouseUpAsButton()
    {
        SceneManager.LoadScene("Game");
    }
}
