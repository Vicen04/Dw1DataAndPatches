using Godot;
using System;

public partial class Recruit : Control
{
	[Export] TextureRect iconDigi;
	[Export] TextureRect iconJoin;
	[Export] Label recruit;
	[Export] Label joined;
	[Export] Label prosperity;
	// Called when the node enters the scene tree for the first time.
	public void Setup(Texture2D digi, Texture2D join, string recr, string joins, int pros)
	{
		iconDigi.Texture = digi;
		iconJoin.Texture = join;
		recruit.Text = recr;
		joined.Text = joins;
		prosperity.Text = pros.ToString();
	}
}
