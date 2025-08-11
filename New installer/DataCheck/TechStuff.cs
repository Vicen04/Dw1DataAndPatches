using Godot;
using System;
using System.IO;
using System.Linq;

public partial class TechStuff : Control
{
	public class Techdata
	{
		public string name { get; set; }
		public int type { get; set; }
		public int power { get; set; }
		public int MP { get; set; }
		public int accuracy { get; set; }
		public string effect { get; set; }
		public int effectChance { get; set; }
	}

	uint namesOffset = 0x14D65494, ptrOffset = 0x14D66C0C;


	[Export] Button Data;
	[Export] Button Learn;
	[Export] Button Types;
	[Export] Button Calculator;
	[Export] Button Boost;
	[Export] TechData TechDataCheck;
	[Export] LearnBattle LearnCheck;
	[Export] TechDamage TypeCheck;
	[Export] Control CalculatorCheck;
	[Export] TechBoost BoostCheck;

	Techdata[] techniques = new Techdata[122];
	Techdata emptyTech;

	uint techDataOffset = 0x14D66DF8;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Data.Text = Tr("TechDataCheck");
		Learn.Text = Tr("TechLearnCheck");
		Types.Text = Tr("TechTypeCheck");
		Calculator.Text = Tr("CalculatorCheck");
		Boost.Text = Tr("TechBoostCheck");
		emptyTech = new Techdata();
		emptyTech.name = Tr("NoTechCheck");
		emptyTech.type = 7;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetupData(System.IO.Stream bin, BinaryReader reader, DataCheck parent, bool vice)
	{
		uint currentPos = techDataOffset;
		for (int i = 0; i < techniques.Length; i++)
		{
			bin.Position = i * 16 + currentPos;
			if (bin.Position == 0x14D673B8)
			{
				currentPos = currentPos + 0x130;
				bin.Position = i * 16 + currentPos;
			}

			techniques[i] = new Techdata();
			techniques[i].power = reader.ReadInt16();
			techniques[i].MP = bin.ReadByte() * 3;
			reader.ReadInt16();
			techniques[i].type = bin.ReadByte();
			techniques[i].effect = GetEffectName(bin.ReadByte());
			techniques[i].accuracy = bin.ReadByte();
			techniques[i].effectChance = bin.ReadByte();
		}

		uint jump = 0x0;

		for (int i = 0; i < 121; i++)
		{
			bin.Position = i * 4 + ptrOffset;
			uint currentOffset;


			if (i == 33 || i == 40 || i == 42 || i == 45 || i > 112)
				currentOffset = reader.ReadUInt32() - 0x80134338 + 0x14D77030;
			else if (i == 107 || i == 108)
				currentOffset = reader.ReadUInt32() - 0x80124c6c + namesOffset;
			else
				currentOffset = reader.ReadUInt32() - 0x80124c6c + namesOffset + jump;
			if (i == 57 && vice)
				currentOffset = currentOffset + 0x130;

			bin.Position = currentOffset;
			if (i != 70)
			{
				int count = 0;
				while (bin.ReadByte() != 0)
					count++;
				bin.Position = currentOffset;
				techniques[i].name = System.Text.Encoding.Default.GetString(reader.ReadBytes(count));
			}
			else
			{
				techniques[i].name = System.Text.Encoding.Default.GetString(reader.ReadBytes(4));
				bin.Position = bin.Position + 0x130;

				int count = 0;
				uint tempOffset = (uint)bin.Position;
				while (bin.ReadByte() != 0)
					count++;
				bin.Position = tempOffset;
				techniques[i].name = techniques[i].name + System.Text.Encoding.Default.GetString(reader.ReadBytes(count));
				jump = 0x130;
			}

		}
		techniques[121].name = Tr("flatAttack");

		LearnCheck.SetupData(bin, parent, this, vice);
		TechDataCheck.SetupData(parent, this, vice);
		TypeCheck.SetupData(bin, parent, this);

		bin.Position = 0x14C66650;
		if (bin.ReadByte() != 33)
		{
			BoostCheck.SetupData(bin, reader, parent, this);
			Boost.Visible = true;
		}
		else
			Boost.Visible = false;

		this.Visible = false;
	}

	string GetEffectName(int effect)
	{
		switch (effect)
		{
			case 0:
				return Tr("NoEffect");
			case 1:
				return Tr("PoisonEffect");
			case 2:
				return Tr("ConfuseEffect");
			case 3:
				return Tr("StunEffect");
			case 4:
				return Tr("FlatEffect");
			default:
				return Tr("NoEffect");

		}
	}

	public Techdata GetTechData(int value) { if (value > 121) return emptyTech; return techniques[value]; }

	public void RestartData()
	{
		LearnCheck.RestartData();
		TechDataCheck.RestartData();
		TechDataCheck.Visible = false;
		LearnCheck.Visible = false;
		TypeCheck.Visible = false;
		CalculatorCheck.Visible = false;
		BoostCheck.Visible = false;
	}

	void TechDataPressed()
	{
		TechDataCheck.Visible = true;
		LearnCheck.Visible = false;
		TypeCheck.Visible = false;
		CalculatorCheck.Visible = false;
		BoostCheck.Visible = false;
	}

	void LearnPressed()
	{
		TechDataCheck.Visible = false;
		LearnCheck.Visible = true;
		TypeCheck.Visible = false;
		CalculatorCheck.Visible = false;
		BoostCheck.Visible = false;
	}

	void TypePressed()
	{
		TechDataCheck.Visible = false;
		LearnCheck.Visible = false;
		TypeCheck.Visible = true;
		CalculatorCheck.Visible = false;
		BoostCheck.Visible = false;
	}
	void CalculatorPressed()
	{
		TechDataCheck.Visible = false;
		LearnCheck.Visible = false;
		TypeCheck.Visible = false;
		CalculatorCheck.Visible = true;
		BoostCheck.Visible = false;
	}
	void BoostPressed()
	{
		TechDataCheck.Visible = false;
		LearnCheck.Visible = false;
		TypeCheck.Visible = false;
		CalculatorCheck.Visible = false;
		BoostCheck.Visible = true;
	}
}
