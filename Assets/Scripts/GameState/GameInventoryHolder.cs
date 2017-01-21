using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInventoryHolder : MonoBehaviour
{
    public List<GameInventory> itemsThatWeCanHold;
    public List<GameObject> inventory;


    private void Start()
    {
        foreach (var objectItem in inventory)
        {
            objectItem.SetActive(false);
        }
    }

    public void PickedItem(GameInventory gameItem)
    {
        var itemIndex = itemsThatWeCanHold.IndexOf(gameItem);
        inventory[itemIndex].SetActive(true);
    }
}
