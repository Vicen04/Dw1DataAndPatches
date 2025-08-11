using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

public partial class TechBoost : Control
{
	[Export] Label techTitle;
	[Export] Label BoostTitle;
	[Export] Label FinalTitle;
	[Export] VBoxContainer BoostContainer;

	uint startOffset = 0x14D627FF;
	List<TechBoostInfo> allTechs;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		techTitle.Text = Tr("Techniques_T");
		BoostTitle.Text = Tr("BoostCheckT");
		FinalTitle.Text = Tr("FinalTechCheckT");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetupData(System.IO.Stream bin, BinaryReader reader, DataCheck mainParent, TechStuff parent)
	{
		if (allTechs != null)
		{
			foreach (TechBoostInfo tech in allTechs)
				tech.QueueFree();
			allTechs.Clear();	
		}
		allTechs = new List<TechBoostInfo>();
		bool jumped = false;
		for (int i = 1; i < 66; i++)
		{
			if (!jumped)
				bin.Position = i * 0x1C + startOffset;
			else
				bin.Position = i * 0x1C + startOffset + 0x130;

			if (bin.Position > 0x14D62A38 && !jumped)
			{
				jumped = true;
				bin.Position = i * 0x1C + startOffset + 0x130;
			}

			int tech = bin.ReadByte();
			reader.ReadInt16();
			int boost = reader.ReadInt16();

			var scene = GD.Load<PackedScene>("res://Items/TechBoostInfo.tscn");
			allTechs.Add(scene.Instantiate() as TechBoostInfo);
			allTechs[i-1].SetupBoost(mainParent.GetDigimonData(i).digimonSprite, mainParent.GetTechsSprites(parent.GetTechData(tech).type),
			mainParent.GetDigimonData(i).name, parent.GetTechData(tech).name, boost, parent.GetTechData(tech).power + boost);
			BoostContainer.AddChild(allTechs[i-1]);
		}
	}
}
