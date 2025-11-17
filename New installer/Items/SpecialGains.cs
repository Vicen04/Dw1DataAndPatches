using Godot;
using System;

public partial class SpecialGains : Control
{
	[Export] TextureRect icon;
	[Export] Label DigimonName;
	[Export] Label Description;
	[Export] Label Gains;

	// Called when the node enters the scene tree for the first time.
	public void Setup(Texture2D tex, string dig, int mult, int gains)
	{
		icon.Texture = tex;
		DigimonName.Text = dig;
		Description.Text = Tr("MultStatGainsCheck") + (mult / 10) + "." + (mult % 10); 
		Gains.Text = gains.ToString() + "%";
	}
}
