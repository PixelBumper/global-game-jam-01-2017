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
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
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
        var globalGameState = GameState.GetGlobalGameState();
        globalGameState.HeldInventoryItem = GameInventory.InitialKey;
        UseAsCursor(globalGameState.KeyCursorTexture, globalGameState.KeyCursorHotspot);
    }

    public void HoldHand()
    {
        GameState.GetGlobalGameState().HeldInventoryItem = GameInventory.None;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void HoldScissors()
    {
        var globalGameState = GameState.GetGlobalGameState();
        globalGameState.HeldInventoryItem = GameInventory.Scissors;
        UseAsCursor(globalGameState.ScissorCursorTexture, globalGameState.ScissorCursorHotspot);
    }

    public void HoldScrewdriver()
    {
        var globalGameState = GameState.GetGlobalGameState();
        globalGameState.HeldInventoryItem = GameInventory.Screwdriver;
        UseAsCursor(globalGameState.ScrewDriverCursorTexture, globalGameState.ScrewDriverCursorHotspot);
    }

    private static void UseAsCursor(Texture2D keyCursorTexture, Vector2 hotspotRelative)
    {
        Vector2 cursorHotspot = new Vector2(keyCursorTexture.width * hotspotRelative.x, keyCursorTexture.height * hotspotRelative.y);
        Cursor.SetCursor(keyCursorTexture, cursorHotspot, CursorMode.Auto);
    }
}
