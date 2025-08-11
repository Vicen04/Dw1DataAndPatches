using Godot;
using System;

public partial class TechInfo : Control
{
	[Export] TextureRect Icon;
	[Export] Label NameTech;
	[Export] Label Power;
	[Export] Label MP;
	[Export] Label Accuracy;
	[Export] Label Status;
	[Export] Label StatusChance;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetupTech(Texture2D icon, string name, int power, int mp, int acc, string status, int chance)
	{
		Icon.Texture = icon;
		NameTech.Text = name;
		MP.Text = mp.ToString();
		Power.Text = power.ToString();
		Accuracy.Text = acc.ToString();
		Status.Text = status;
		StatusChance.Text = chance.ToString();
	}
}
