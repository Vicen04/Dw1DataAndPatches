using Godot;
using System;

public partial class EvoItems : Control
{
	[Export] VBoxContainer Items;
	[Export] Label ItemTitle;
	[Export] Label Evolution;

	System.Collections.Generic.List<EvoItem> allItems; 
	uint machi = 0x14CF5A74;
	uint[] extra = { 0x14CF5A50, 0x14CF5A44, 0x14CF5A5C };
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ItemTitle.Text = Tr("ItemCheckLabel");
		Evolution.Text = Tr("EvolutionF_T");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public void SetupData(System.IO.Stream bin, DataCheck parent, ItemsStuff items, bool vice)
	{
		bin.Position = 0x14D68BA4;
		int Digimon;

		if (allItems != null)
		{
			foreach (EvoItem item in allItems)
				item.QueueFree();
			allItems.Clear();
		}
		allItems = new System.Collections.Generic.List<EvoItem>();
		for (int i = 0; i < 44; i++)
		{
			Digimon = bin.ReadByte();
			if (Digimon > 65)
				Digimon = 0;
			var scene = GD.Load<PackedScene>("res://Items/EvoItem.tscn");
			allItems.Add(scene.Instantiate() as EvoItem);
			allItems[i].SetupEvoItem(items.GetItemData(i + 71).name, parent.GetDigimonData(Digimon).name, parent.GetItemTex(i + 71), parent.GetDigimonData(Digimon).digimonSprite);
			Items.AddChild(allItems[i]);

		}
		int currentCount = 44;
		if (vice)
		{
			extra[0] = 0x14CF5A50;
			extra[1] = 0x14CF5A44;
			extra[2] = 0x14CF5A5C;
			bin.Position = machi;
			Digimon = bin.ReadByte();
			var scene = GD.Load<PackedScene>("res://Items/EvoItem.tscn");
			allItems.Add(scene.Instantiate() as EvoItem);
			allItems[44].SetupEvoItem(items.GetItemData(124).name, parent.GetDigimonData(Digimon).name, parent.GetItemTex(124), parent.GetDigimonData(Digimon).digimonSprite);
			Items.AddChild(allItems[44]);
			currentCount++;
		}
		else
		{
			bin.Position = 0x14CF5A30;
			if (bin.ReadByte() == 17)
			{
				extra[0] = 0x14CF5A44;
				extra[1] = 0x14CF5A58;
				extra[2] = 0x14CF5A6C;
			}
		}
		for (int i = 0; i < 3; i++)
		{
			bin.Position = extra[i];
			Digimon = bin.ReadByte();
			var scene = GD.Load<PackedScene>("res://Items/EvoItem.tscn");
			allItems.Add(scene.Instantiate() as EvoItem);
			allItems[currentCount].SetupEvoItem(items.GetItemData(i + 125).name, parent.GetDigimonData(Digimon).name, parent.GetItemTex(i + 125), parent.GetDigimonData(Digimon).digimonSprite);
			Items.AddChild(allItems[currentCount]);
			currentCount++;
		}
		

	}
}
