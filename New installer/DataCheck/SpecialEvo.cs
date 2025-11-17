using Godot;
using System;
using System.Linq;

public partial class SpecialEvo : Control
{
	[Export] Label RequiredTitle;
	[Export] Label DescriptionTitle;
	[Export] Label EvolutionTitle;
	[Export] Label ChanceTitle;
	[Export] Label[] RequiredDigi;
	[Export] Label[] RequiredG;
	[Export] TextureRect[] IconRequired;
	[Export] Label[] Descriptions;
	[Export] Label[] Evolutions;
	[Export] TextureRect[] IconEvolutions;
	[Export] Label[] Chances;
	[Export] Control ExtraEvo;
	// Called when the node enters the scene tree for the first time.

	uint[] EvoOffsets = { 0x14D1A0AC, 0x14CD7584, 0x14D19AC8, 0x14D1A114, 0x14D1A148, 0x140A2E11, 0x140A2EFB, 0x140A2EA9, 0x140A2E5D, 0x140A2E5D, 0x14D19FEC, 0x14D19FEC,
						  0x14D1A0F8, 0x14D1A0F8, 0x14D1A01C, 0x14D1A054, 0x14046841, 0x14D1A098 },
	chanceOffsets = { 0x14D1A060, 0x14D1A13C, 0x140A2E07, 0x140A2EEB, 0x140A2E9F, 0x140A2E53, 0x140A2E53, 0x14D19FA8, 0x14D19FA8, 0x14D1A0EC, 0x14D1A0EC, 0x14D19FA8, 0x14D19FA8,
					  0x14D1A060 },
	ReqOffsets = { 0x140A2C65, 0x140A2C69, 0x140A2C6D, 0x140A2C71, 0x140A2DB1, 0x140A2DB5, 0x140A2DB9, 0x140A2DBD, 0x140A2DF1, 0x140A2DE1, 0x140A2DCD, 0x140A2DD1, 0x14D19FC0,
				   0x14D19FB4, 0x14D1A0BC, 0x14D1A0C8, 0x14D19FF8, 0x14D1A028, 0x140467EF };

	public override void _Ready()
	{
		RequiredTitle.Text = Tr("DigimonCheck");
		DescriptionTitle.Text = Tr("DescriptionCheck");
		EvolutionTitle.Text = Tr("EvolutionF_T");
		ChanceTitle.Text = Tr("StatusChaTechCheck");
		for (int i = 0; i < Descriptions.Length; i++)
			Descriptions[i].Text = Tr("SpecialDescription" + i);
		for (int i = 0; i < RequiredG.Length; i++)
			RequiredG[i].Text = Tr("SpecialRequiredGen" + i);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetupData(System.IO.Stream bin, DataCheck parent, bool vice)
	{
		int devi = 0;
		if (vice)
		{
			ExtraEvo.Visible = true;
			EvoOffsets[2] = 0x14D19AC8;
		}
		else
		{
			ExtraEvo.Visible = false;
			devi = 1;
			ReqOffsets[0] = 0x0;
			EvoOffsets[2] = 0x14D19A3C;
			bin.Position = 0x14D19AC8;
			if (bin.ReadByte() == 62)
				EvoOffsets[2] = 0x14D19B38;
		}

		for (int i = 0; i < chanceOffsets.Length; i++)
		{
			bin.Position = chanceOffsets[i];
			int chance = bin.ReadByte();
			if (i > 1 && i < 7)
				chance = chance + 1;
			Chances[i].Text = chance + "%";
		}

		for (int i = 0; i < EvoOffsets.Length - devi; i++)
		{
			bin.Position = EvoOffsets[i];
			int digimon = bin.ReadByte();
			Evolutions[i].Text = parent.GetDigimonData(digimon).name;
			IconEvolutions[i].Texture = parent.GetDigimonData(digimon).digimonSprite;
		}

		for (int i = 0; i < 8; i++)
		{
			bin.Position = ReqOffsets[i];
			int digi = bin.ReadByte();
			IconRequired[i].Texture = parent.GetDigimonData(digi).digimonSprite;
			IconRequired[i].TooltipText = parent.GetDigimonData(digi).name;
		}

		for (int i = 0; i < RequiredDigi.Length; i++)
		{
			bin.Position = ReqOffsets[i + 8];
			int digimon = bin.ReadByte();
			if (digimon > 65)
				digimon = 0;
			RequiredDigi[i].Text = parent.GetDigimonData(digimon).name;
			IconRequired[i + 8].Texture = parent.GetDigimonData(digimon).digimonSprite;
		}

	}
}
