using Godot;
using System;

public partial class ItemSearch : Control
{
	[Export] TextureRect ItemIcon;
	[Export] Label MapName;
	[Export] Label ItemName;
	[Export] Label ItemChance;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetupData(Texture2D icon, string mapName, string itemName, string chance = null)
	{
		ItemIcon.Texture = icon;
		MapName.Text = mapName;
		ItemName.Text = itemName;
		if (chance != null)
			ItemChance.Text = chance + "%";
		else
			ItemChance.Visible = false;
	}
}
			
