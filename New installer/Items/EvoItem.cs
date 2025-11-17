using Godot;
using System;

public partial class EvoItem : Control
{
	[Export] Label Digimon;
	[Export] Label Evo;
	[Export] TextureRect DigimonIcon;
	[Export] TextureRect EvoIcon;
	// Called when the node enters the scene tree for the first time.
	public void SetupEvoItem(string digi, string evo, Texture2D icon, Texture2D item)
	{
		Digimon.Text = digi;
		Evo.Text = evo;
		DigimonIcon.Texture = icon;
		EvoIcon.Texture = item;
	}
}
