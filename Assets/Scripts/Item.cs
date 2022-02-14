using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        Ammo,
        Mask,
        Medkit
    }

    public ItemType itemType;
    public int amount;


    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Ammo: return ItemAssets.Instance.ammoSprite;
            case ItemType.Mask: return ItemAssets.Instance.maskSprite;
            case ItemType.Medkit: return ItemAssets.Instance.medkitSprite;
        }
    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Ammo:
            case ItemType.Mask:
            case ItemType.Medkit:
                return true;
        }
    }
}
