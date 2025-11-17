using Godot;
using System;
using System.Linq.Expressions;

public partial class FactorialUp : Control
{
	[Export] Label[] Upgradable;
	[Export] Label[] Evolutions;
	[Export] Label[] Rares;
	[Export] TextureRect[] UpgradableIcons;
	[Export] TextureRect[] EvolutionIcons;
	[Export] TextureRect[] RareIcons;
	[Export] Label EvoTitle;
	[Export] Label RareTitle;
	[Export] Control[] Expanded;

	[Export] Label[] SuperUpgradable;
	[Export] TextureRect[] SuperUpgradableIcons;
	[Export] Label SuperEvo;
	[Export] TextureRect SuperEvoIcon;
	[Export] Label SuperEvoTitle;
	[Export] Label SuperEvoTitleC;
	[Export] Control[] SuperEvoStuff;


	uint[] OGOffsets = { 0x140543AD, 0x14054589, 0x14054503, //Mamemon, 50% evo, 10% evo
						 0x14D19DAC, 0x14D19E78, 0x14D19E1C, //Evo2, 50% evo, 10% evo
						 0x14D19DB4, 0x14D19E84, 0x14D19E28, //Evo3, 50% evo, 10% evo
						 0x14D19DBC, 0x14D19E90, 0x14D19E34, //Evo4, 50% evo, 10% evo
						 0x14D19DC4, 0x14D19E9C, 0x14D19E40, //Evo5, 50% evo, 10% evo
						 0x14D19DCC, 0x14D19EA8, 0x14D19E4C, //Evo6, 50% evo, 10% evo
						 0x14D19DD4, 0x14D19EB4, 0x14D19E58, //Evo7, 50% evo, 10% evo
						 0x14D19DDC, 0x14054589, 0x14054503 }, //Evo8, 50% evo, 10% evo
	 viceOffsets = { 0x14054453, 0x140546E5, 0x140545EB,
	 				 0x14054457, 0x140546F9, 0x140545FF,
					 0x1405445B, 0x1405470D, 0x14054613, 
					 0x1405445F, 0x14054721, 0x14054627,
					 0x1405446F, 0x14054735, 0x1405463B,
					 0x14054473, 0x14054749, 0x1405464F,
					 0x14054477, 0x1405475D, 0x14054663,
					 0x1405447B, 0x14054765, 0x1405466B },
	superOffsets = {0x14054815, 0x14054819, 0x1405481D, 0x14054821, 0x14054825 };

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		EvoTitle.Text = Tr("UpgradeFactCheck") + " (50%)";
		RareTitle.Text = Tr("RareFactCheck") + " (10%)";
		SuperEvoTitleC.Text = Tr("UpgradeFactCheck") + " (73%)";
		SuperEvoTitle.Text = Tr("SuperFactCheck");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetupData(System.IO.Stream bin, DataCheck parent)
	{
		uint[] offsets;
		int count = 8;

		bin.Position = 0x14054300;
		if (bin.ReadByte() == 234)
		{
			for (int i = 0; i < Expanded.Length; i++)
				Expanded[i].Visible = true;

			for (int i = 0; i < SuperEvoStuff.Length; i++)
				SuperEvoStuff[i].Visible = true;

			offsets = viceOffsets;
			for (int i = 0; i < superOffsets.Length; i++)
			{
				bin.Position = superOffsets[i];
				int digi = bin.ReadByte();
				SuperUpgradableIcons[i].Texture = parent.GetDigimonData(digi).digimonSprite;
				SuperUpgradable[i].Text = parent.GetDigimonData(digi).name;
			}

			bin.Position = 0x1405495D;
			int superEvo = bin.ReadByte();
			SuperEvoIcon.Texture = parent.GetDigimonData(superEvo).digimonSprite;
			SuperEvo.Text = "\n" + parent.GetDigimonData(superEvo).name;
		}
		else
		{
			offsets = OGOffsets;
			bin.Position = 0x14D19DEC;
			if (bin.ReadByte() == 0)
			{
				count = 1;
				for (int i = 0; i < Expanded.Length; i++)
					Expanded[i].Visible = false;

			}
			else
			{
				for (int i = 0; i < Expanded.Length; i++)
					Expanded[i].Visible = true;
			}

			for (int i = 0; i < SuperEvoStuff.Length; i++)
				SuperEvoStuff[i].Visible = false;
		}

		for (int i = 0; i < count; i++)
		{
			bin.Position = offsets[i * 3];
			int digi = bin.ReadByte();
			UpgradableIcons[i].Texture = parent.GetDigimonData(digi).digimonSprite;
			Upgradable[i].Text = parent.GetDigimonData(digi).name;

			bin.Position = offsets[i * 3 + 1];
			digi = bin.ReadByte();
			EvolutionIcons[i].Texture = parent.GetDigimonData(digi).digimonSprite;
			Evolutions[i].Text = parent.GetDigimonData(digi).name;

			bin.Position = offsets[i * 3 + 2];
			digi = bin.ReadByte();
			RareIcons[i].Texture = parent.GetDigimonData(digi).digimonSprite;
			Rares[i].Text = parent.GetDigimonData(digi).name;

		}

	}
}
