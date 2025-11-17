using Godot;
using System;

public partial class StatGainsDigimon : Control
{
	[Export] TextureRect icon;
	[Export] Label DigimonName;
	[Export] Label HP;
	[Export] Label MP;
	[Export] Label Off;
	[Export] Label Def;
	[Export] Label Spd;
	[Export] Label Brn;

	public void Setup(Texture2D sprite, string name, int hp, int mp, int off, int def, int spd, int brn)
	{
		icon.Texture = sprite;
		DigimonName.Text = name;
		HP.Text = hp.ToString();
		MP.Text = mp.ToString();
		Off.Text = off.ToString();
		Def.Text = def.ToString();
		Spd.Text = spd.ToString();
		Brn.Text = brn.ToString();
	}
}
