using Godot;
using System;

public partial class DigimonDropsSetup : Control
{
	[Export] Label name;
	[Export] TextureRect digimonIcon;
	[Export] TextureRect itemIcon;
	[Export] Label itemName;
	[Export] Label chance;

	public void Setup(string nameDigi, Texture2D digimonI, Texture2D itemI, string itemN, int chanceI)
	{
		name.Text = nameDigi;
		digimonIcon.Texture = digimonI;
		itemIcon.Texture = itemI;
		itemName.Text = itemN;
		chance.Text = chanceI.ToString() + "%";
	}
}
