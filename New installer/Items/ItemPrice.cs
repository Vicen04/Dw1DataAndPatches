using Godot;
using System;

public partial class ItemPrice : Control
{

	[Export] TextureRect ItemIcon;
	[Export] Label ItemName;
	[Export] Label itemPrice;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetupData(Texture2D icon, string itemName, string chance = null)
	{
		ItemIcon.Texture = icon;		
		ItemName.Text = itemName;
		itemPrice.Text = chance;

	}
}
