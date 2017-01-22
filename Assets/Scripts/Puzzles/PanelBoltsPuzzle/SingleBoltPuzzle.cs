using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleBoltPuzzle : MonoBehaviour
{

    public PanelBoltsPuzzle ParentPanel;

    private bool canUnScrewe = true;
    public int MinScrewes = 1;
    public int MaxScrewes = 4;
    public int RequiredScrewes;
    public int CurrentScrewes;

    private void Start()
    {
        RequiredScrewes = Random.Range(MinScrewes, MaxScrewes);
    }

    private void OnMouseUpAsButton()
    {
        if (ParentPanel && ParentPanel.CanUnScreweBolt() && canUnScrewe)
        {
            canUnScrewe = false;
            CurrentScrewes++;
            LeanTween.rotateZ(gameObject, gameObject.transform.rotation.z * Mathf.Rad2Deg + 90, 0.5f)
            .setOnComplete(EnableScrewesAgain);
        }
    }

    public void EnableScrewesAgain()
    {
        if (CurrentScrewes >= RequiredScrewes)
        {
            ParentPanel.BoltUnscrewed();
            gameObject.SetActive(false);
        }
        canUnScrewe = true;
    }
}
