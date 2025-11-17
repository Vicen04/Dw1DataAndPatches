using Godot;
using System;
using System.Collections.Generic;

public partial class Recruits : Control
{
	[Export] VBoxContainer list;
	[Export] Control first;
	[Export] Label recruitTitle;
	[Export] Label prosTitle;
	[Export] Label joinsTitle;

	[Export] Label MtPros;
	[Export] Label[] MtDigi;
	[Export] Label[] MtRecruit;
	[Export] TextureRect[] MtDigiIcon;
	[Export] TextureRect[] MtRecruitIcon;

	uint[] offsets = { 0x13FDE354, 0x13FDEB8C, 0x13FDF54E, 0x13FE08F2, 0x13FE1D48, 0x13FE4B16, 0x13FE6B52, 0x13FE88E0, 0x13FE909A, 0x13FEB63C, 0x13FED052, 0x13FF1972,
					   0x13FF572E, 0x13FF6456, 0x13FF8018, 0x13FFFADC, 0x14000C52, 0x1400BA8C, 0x1400DD0A, 0x1400E6D6, 0x1400F914, 0x14012928, 0x140172B6, 0x1401F636,
					   0x14020E84, 0x14029508, 0x1402A742, 0x1402BAA2, 0x140304F2, 0x140360A2, 0x140396B0, 0x1403AAF6, 0x1403CE84, 0x14040D64, 0x140450AE, 0x14047768,
					   0x1404CF3E, 0x1404FAD8, 0x14052A7A, 0x1405779C, 0x1405B8C8, 0x14074630, 0x1407750A, 0x1407B9AE, 0x1407C320, 0x14081CC4, 0x14096844},
	prosperity = {0x13FDE359, 0x13FDEB91, 0x13FDF553, 0x13FE08F7, 0x13FE1D4D, 0x13FE4B1B, 0x13FE6B57, 0x13FE88E5, 0x13FE909F, 0x13FEB641, 0x13FED04F, 0x13FF1977,
				  0x13FF5733, 0x13FF645B, 0x13FF801D, 0x13FFFAE1, 0x14000C4F, 0x1400BA91, 0x1400DD0F, 0x1400E6DB, 0x1400F919, 0x1401292D, 0x140172BB, 0x1401F63B,
				  0x14020E89, 0x1402950D, 0x1402A747, 0x1402BAA7, 0x140304F7, 0x140360A7, 0x140396B5, 0x1403AAFB, 0x1403CE8D, 0x14040D69, 0x140450B3, 0x1404776D,
				  0x1404CF43, 0x1404FADD, 0x14052A7F, 0x140577A1, 0x1405B8CD, 0x14074635, 0x1407750F, 0x1407B9B3, 0x1407C325, 0x14081CC9, 0x14096849 };
	int[] recruits = {32, 46, 42, 49, 10, 58, 55, 25, 4, 36, 13, 34, 9, 38, 37, 53, 47, 26, 18, 31, 45, 48, 8, 22, 20, 19, 51, 3, 53, 17, 35, 21, 52, 23, 24, 14,
					  27, 40, 41, 39, 5, 11, 56, 33, 28, 54, 57},
	infinity = {0x14078704, 0x14078708, 0x1407870C}, MtOG = { 12, 7, 6};

	uint infinityPros = 0x14078711, Phoenix = 0x14010A84, HKabu = 0x14010D70, MegaSeadra = 0x14045932;
	

	List<Recruit> allRecruits;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		recruitTitle.Text = Tr("RecruitmentCheck");
		prosTitle.Text = Tr("ProsperityCheck");
		prosTitle.TooltipText = Tr("ProsperityCheckInfo");
		joinsTitle.Text = Tr("JoinedCityCheck");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public void SetupData(System.IO.Stream bin, System.IO.BinaryReader reader, DataCheck parent, bool vice)
	{
		if (allRecruits != null)
		{
			foreach (Recruit recruit in allRecruits)
				recruit.QueueFree();
			allRecruits.Clear();
		}

		allRecruits = new List<Recruit>();

		bin.Position = 0x6580D42;
		if (bin.ReadByte() != 0x80)
		{
			prosperity[3] = 0x13FE0891; //Coelamon
			prosperity[9] = 0x13FEB63F; //Centarumon
			prosperity[14] = 0x13FF8017; //Bakemon

			offsets[14] = 0x13FF8012; //Bakemon
			offsets[3] = 0x13FE088C; //Coelamon
			offsets[9] = 0x13FEB63A; //Centarumon
		}
		else
		{
			prosperity[3] = 0x13FE08F7; //Coelamon
			prosperity[9] = 0x13FEB641; //Centarumon
			prosperity[14] = 0x13FF801D; //Bakemon

			offsets[14] = 0x13FF8018; //Bakemon
			offsets[3] = 0x13FE08F2; //Coelamon
			offsets[9] = 0x13FEB63C; //Centarumon
		}

		for (int i = 0; i < offsets.Length; i++)
		{
			bin.Position = offsets[i];
			int recruit = reader.ReadInt16();
			recruit = recruit - 200;

			bin.Position = prosperity[i];
			int pros = bin.ReadByte();

			var scene = GD.Load<PackedScene>("res://Items/Recruit.tscn");
			allRecruits.Add(scene.Instantiate() as Recruit);
			allRecruits[i].Setup(parent.GetDigimonData(recruits[i]).digimonSprite, parent.GetDigimonData(recruit).digimonSprite, parent.GetDigimonData(recruits[i]).name,
			parent.GetDigimonData(recruit).name, pros);
			first.AddSibling(allRecruits[i]);
		}

		if (vice)
		{
			bin.Position = Phoenix;
			int recruit = reader.ReadInt16() - 200;

			var scene = GD.Load<PackedScene>("res://Items/Recruit.tscn");
			allRecruits.Add(scene.Instantiate() as Recruit);
			allRecruits[offsets.Length].Setup(parent.GetDigimonData(59).digimonSprite, parent.GetDigimonData(recruit).digimonSprite, parent.GetDigimonData(59).name,
			parent.GetDigimonData(recruit).name, 0);
			list.AddChild(allRecruits[offsets.Length]);

			bin.Position = HKabu;
			recruit = reader.ReadInt16() - 200;

			scene = GD.Load<PackedScene>("res://Items/Recruit.tscn");
			allRecruits.Add(scene.Instantiate() as Recruit);
			allRecruits[offsets.Length + 1].Setup(parent.GetDigimonData(60).digimonSprite, parent.GetDigimonData(recruit).digimonSprite, parent.GetDigimonData(60).name,
			parent.GetDigimonData(recruit).name, 0);
			list.AddChild(allRecruits[offsets.Length + 1]);

			bin.Position = MegaSeadra;
			recruit = reader.ReadInt16() - 200;

			scene = GD.Load<PackedScene>("res://Items/Recruit.tscn");
			allRecruits.Add(scene.Instantiate() as Recruit);
			allRecruits[offsets.Length + 2].Setup(parent.GetDigimonData(61).digimonSprite, parent.GetDigimonData(recruit).digimonSprite, parent.GetDigimonData(61).name,
			parent.GetDigimonData(recruit).name, 0);
			list.AddChild(allRecruits[offsets.Length + 2]);
		}

		bin.Position = infinityPros;
		MtPros.Text = bin.ReadByte().ToString();

		for (int i = 0; i < MtDigi.Length; i++)
		{
			bin.Position = infinity[i];
			int recruit = reader.ReadInt16() - 200;

			MtRecruit[i].Text = parent.GetDigimonData(recruit).name;
			MtRecruitIcon[i].Texture = parent.GetDigimonData(recruit).digimonSprite;
			MtDigi[i].Text = parent.GetDigimonData(MtOG[i]).name;
			MtDigiIcon[i].Texture = parent.GetDigimonData(MtOG[i]).digimonSprite;
		}
	}
}
