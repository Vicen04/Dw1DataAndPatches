using Godot;
using System;

public partial class MapDigimon : Control
{
	[Export] Panel IconP;
	[Export] Panel IconP2;
	[Export] TextureRect Icon;
	[Export] Label Digimon;
	[Export] TextureRect Icon2;
	[Export] Label Digimon2;
	[Export] Label Map;
	[Export] Label HP;
	[Export] Label MP;
	[Export] Label cHP;
	[Export] Label cMP;
	[Export] Label Off;
	[Export] Label Def;
	[Export] Label Spd;
	[Export] Label Brn;
	[Export] Label Money;
	[Export] Label Attack1;
	[Export] Label Attack2;
	[Export] Label Attack3;
	[Export] Label Attack4;
	[Export] Label Attack1Chance;
	[Export] Label Attack2Chance;
	[Export] Label Attack3Chance;
	[Export] Label Attack4Chance;

	// Called when the node enters the scene tree for the first time.
	public void SetupDigimon(Texture2D icon, string name, int hp, int mp, int chp, int cmp, int off, int def, int spd, int brn, int money,
							 string att1, string att2, string att3, string att4, int attC1, int attC2, int attC3, int attC4, string map = null)
	{
		Icon.Texture = icon;
		Digimon.Text = name;
		if (map != null)
		{
			IconP.Visible = false;
			Digimon.Visible = false;
			IconP2.Visible = true;
			Digimon2.Visible = true;
			Icon2.Texture = icon;
			Digimon2.Text = name;
			Map.Text = map;
			Map.Visible = true;
		}
		HP.Text = hp.ToString();
		MP.Text = mp.ToString();
		cHP.Text = chp.ToString();
		cMP.Text = cmp.ToString();
		Off.Text = off.ToString();
		Def.Text = def.ToString();
		Spd.Text = spd.ToString();
		Brn.Text = brn.ToString();
		Money.Text = money.ToString();
		Attack1.Text = att1;
		Attack2.Text = att2;
		Attack3.Text = att3;
		Attack4.Text = att4;
		Attack1Chance.Text = attC1.ToString();
		Attack2Chance.Text = attC2.ToString();
		Attack3Chance.Text = attC3.ToString();
		Attack4Chance.Text = attC4.ToString();
	}
}
