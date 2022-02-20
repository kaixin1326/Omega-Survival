using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory
{
    private List<Item> itemList;

    public event EventHandler OnItemListChanged;
   
    public Inventory()
    {
        itemList = new List<Item>();

        //一開始先有60發子彈(暫定)
        AddItem(new Item { itemType = Item.ItemType.Ammo, amount = 60 });
    }

    public void AddItem(Item item)
    {
        if (item.IsStackable())
        {
            bool itemAlreadayInInventory = false;
            foreach(Item inventoryItem in itemList)
            {
                if(inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount += item.amount;
                    itemAlreadayInInventory = true;
                }
            }
            if (!itemAlreadayInInventory)
            {
                itemList.Add(item);
            }
        }
        else
        {
            itemList.Add(item);
        }

        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void UseItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.Medkit:
                if(item.amount > 0)
                {
                    Health _health = GameObject.Find("FPCharacterControlller_copy").GetComponent<Health>();
                    _health.AddHealth(25);
                    AddItem(new Item { itemType = Item.ItemType.Medkit, amount = -1 });
                }
                break;
        }
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}
