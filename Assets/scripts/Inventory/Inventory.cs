using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Item[] hotbar = new Item[9], inventory;
    private Item temp_slot = null;
    void Start()
    {
        expand_inventory(27);
    }
    public void expand_inventory(int slots)
    {
        Item[] temp = inventory;
        inventory = new Item[slots];

        for (int i = 0; i < temp.Length; i++) 
        { 
            inventory[i] = temp[i];
        }
    }
    public void pick_up_item(Item new_item)
    {
        if (filled_slots(hotbar) < hotbar.Length)
        {
            fill_slot(new_item, filled_slots(hotbar), 0);
        }
        else if (filled_slots(inventory) < inventory.Length)
        {
            fill_slot(new_item, filled_slots(inventory), 1);
        }
        else
            return;
    }
    //bar is a code for whether or not the item goes to the hotbar or the inventory 0 being hotbar, 1 being inventory, and 2 being temp slot
    private void fill_slot(Item new_item, int slot, int bar)
    {
        if (bar == 0)
            hotbar[slot] = new_item;
        else
            inventory[slot] = new_item;
    }
    public void replace_item(int slot, int bar)
    {
        Item temp = (bar == 0) ? hotbar[slot] : inventory[slot];
    }
    private int filled_slots(Item[] items)
    {
        int filled_slot_count = 0;
        foreach (Item item in items)
        {
            filled_slot_count += (item == null) ? 0 : 1;
        }
        return filled_slot_count;
    }

    public bool inventory_full(int bar)
    {
        return filled_slots((bar == 0) ? hotbar : inventory) == ((bar == 0) ? hotbar.Length : inventory.Length);
    }
}
