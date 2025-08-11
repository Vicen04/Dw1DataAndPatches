using Godot;
using System;

public partial class TechBoostInfo : Control
{
	[Export] TextureRect digimonIcon;
	[Export] TextureRect typeIcon;
	[Export] Label digimonName;
	[Export] Label techName;
	[Export] Label techBoost;
	[Export] Label techFinal;
	// Called when the node enters the scene tree for the first time.
	public void SetupBoost(Texture2D icon, Texture2D type, string digimon, string tech, int boost, int final)
	{
		digimonIcon.Texture = icon;
		typeIcon.Texture = type;
		digimonName.Text = digimon;
		techName.Text = tech;
		techBoost.Text = boost.ToString();
		techFinal.Text = final.ToString();
	}
}
