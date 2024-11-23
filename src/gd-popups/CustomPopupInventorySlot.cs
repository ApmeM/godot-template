using System.Collections.Generic;
using Godot;
using GodotTemplate.Presentation.Utils;

[SceneReference("CustomPopupInventorySlot.tscn")]
[Tool]
public partial class CustomPopupInventorySlot
{
    [Export]
    public int ItemIndex;
    private int itemsCount = 0;

    [Export]
    public int ItemsCount
    {
        get => itemsCount; set
        {
            itemsCount = value;
            if (IsInsideTree())
            {
                this.countLabel.Text = value.ToString();
                this.countLabel.Visible = value > 1;
            }
        }
    }

    public override void _Ready()
    {
        base._Ready();
        this.FillMembers();
        this.RemoveItem();

        this.ItemsCount = itemsCount;
    }

    public bool HasItem()
    {
        return this.lootContainer.GetChildCount() > 0;
    }

    public void SetItem(Node loot, int item)
    {
        if (HasItem())
        {
            GD.PrintErr("Can not add loot item as slot is already occupied.");
            return;
        }

        this.lootContainer.AddChild(loot);
        this.ItemIndex = item;
    }

    public (int, int) GetItem()
    {
        if (this.HasItem())
        {
            return (this.ItemIndex, ItemsCount);
        }
        else
        {
            return (-1, 0);
        }
    }

    public void UpdateCount(int countDiff)
    {
        if (countDiff == 0)
        {
            return;
        }

        if (!HasItem())
        {
            GD.PrintErr($"Updating count for slot with no item set.");
        }

        var result = itemsCount + countDiff;
        if (result < 0)
        {
            GD.PrintErr($"Removing count {countDiff} from slot with only {itemsCount}.\nIt is succesfull, but final sum up calculations might be wrong.");
        }

        this.ItemsCount = result;

        if (result <= 0)
        {
            RemoveItem();
        }

    }

    public void RemoveItem()
    {
        this.lootContainer.ClearChildren();
        this.ItemsCount = 0;
        this.ItemIndex = -1;
    }
}
