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
        inventory[itemsThatWeCanHold.IndexOf(GameInventory.None)].SetActive(true);

        var itemIndex = itemsThatWeCanHold.IndexOf(gameItem);
        inventory[itemIndex].SetActive(true);
    }

    public void DropItem(GameInventory gameItem)
    {
        var itemIndex = itemsThatWeCanHold.IndexOf(gameItem);
        inventory[itemIndex].SetActive(false);

        if (!IsThereMoreThanOneItemActive())
        {
            inventory[itemsThatWeCanHold.IndexOf(GameInventory.None)].SetActive(false);
        }
    }

    private bool IsThereMoreThanOneItemActive()
    {
        int itemsActive = 0;
        foreach (var item in inventory)
        {
            itemsActive += item.activeSelf ? 1 : 0;
        }
        return itemsActive > 1;
    }

    public void HoldKeys()
    {
        GameState.GetGlobalGameState().HeldInventoryItem = GameInventory.InitialKey;
        UseAsCursor(GameState.GetGlobalGameState().KeyCursorTexture);
    }

    public void HoldHand()
    {
        GameState.GetGlobalGameState().HeldInventoryItem = GameInventory.None;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void HoldScissors()
    {
        GameState.GetGlobalGameState().HeldInventoryItem = GameInventory.Scissors;
        UseAsCursor(GameState.GetGlobalGameState().ScissorCursorTexture);
    }

    public void HoldScrewdriver()
    {
        GameState.GetGlobalGameState().HeldInventoryItem = GameInventory.Screwdriver;
        UseAsCursor(GameState.GetGlobalGameState().ScrewDriverCursorTexture);
    }

    private static void UseAsCursor(Texture2D keyCursorTexture)
    {
        Vector2 cursorHotspot = new Vector2(keyCursorTexture.width / 2, keyCursorTexture.height / 2);
        Cursor.SetCursor(keyCursorTexture, cursorHotspot, CursorMode.Auto);
    }
}
