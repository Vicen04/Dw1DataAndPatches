using Godot;
using System;

public partial class TechDamage : Control
{
	[Export] Label Attacker;
	[Export] Label Defender;
	[Export] Label NoType;
	[Export] Label[] Damages;
	[Export] TextureRect[] AttackerTypes;
	[Export] TextureRect[] DefenderTypes;
	[Export] Label NoTypeDamage;
	[Export] Label CalcTitle;
	[Export] Label AttackLabel;
	[Export] Label Type1;
	[Export] Label Type2;
	[Export] Label Type3;
	[Export] OptionButton[] TypeOptions;
	[Export] OptionButton AttackType;
	[Export] Label CalcResult;
	[Export] Label CalcValue;

	uint typeDamageOffset = 0x14D669F8;

	int[] damageTypes = new int[49];
	int[] iconOrder = { 0, 2, 4, 5, 3, 1, 6 };
	int Attack = 0, type1 = 10, type2 = 10, type3 = 10, noType = 10;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Attacker.Text = Tr("AttackerTypes");
		Defender.Text = Tr("DefenderTypes");
		NoType.Text = Tr("NoTypeCheck");
		CalcTitle.Text = Tr("TypeCalcCheckT");
		Type1.Text = Tr("Type1CheckDamage");
		Type2.Text = Tr("Type2CheckDamage");
		Type3.Text = Tr("Type3CheckDamage");
		CalcResult.Text = Tr("TypeCalcCheckR");
		AttackLabel.Text = Tr("AttackTypeCheck");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public void SetupData(System.IO.Stream bin, DataCheck mainParent, TechStuff parent)
	{
		Attack = 0;
		type1 = type2 = type3 = 10;
		TypeOptions[0].Clear();
		TypeOptions[1].Clear();
		TypeOptions[2].Clear();
		AttackType.Clear();

		bin.Position = 0x14B586A0;

		if (bin.ReadByte() == 0xD0) //checks if the Attack function still exists there
			bin.Position = 0x14B58744;
		else
			bin.Position = 0x14CC0E68;		
		
		int noneDamage = bin.ReadByte();
		noType = noneDamage;
		NoTypeDamage.Text = noneDamage.ToString();
		StyleBoxFlat StyleboxUpdate = NoTypeDamage.GetThemeStylebox("normal").Duplicate() as StyleBoxFlat;
		StyleboxUpdate.BgColor = GetColor(noneDamage);
		NoTypeDamage.AddThemeStyleboxOverride("normal", StyleboxUpdate);
		if (noneDamage == 10 || noneDamage == 15 || noneDamage == 20)
			NoTypeDamage.AddThemeColorOverride("font_color", new Godot.Color(0, 0, 0));
		else
			NoTypeDamage.AddThemeColorOverride("font_color", new Godot.Color(1, 1, 1));

		bin.Position = typeDamageOffset;

		for (int i = 0; i < 49; i++)
		{
			int typeDamage = bin.ReadByte();
			damageTypes[i] = typeDamage;
			Damages[i].Text = typeDamage.ToString();
			StyleboxUpdate = Damages[i].GetThemeStylebox("normal").Duplicate() as StyleBoxFlat;
			StyleboxUpdate.BgColor = GetColor(typeDamage);
			Damages[i].AddThemeStyleboxOverride("normal", StyleboxUpdate);
			if (typeDamage == 10 || typeDamage == 20 || typeDamage == 15)
				Damages[i].AddThemeColorOverride("font_color", new Godot.Color(0, 0, 0));
			else
				Damages[i].AddThemeColorOverride("font_color", new Godot.Color(1, 1, 1));
		}

		for (int i = 0; i < AttackerTypes.Length; i++)
		{
			AttackerTypes[i].Texture = mainParent.GetTechsSprites(i);
			DefenderTypes[i].Texture = mainParent.GetTechsSprites(i);
		}

		for (int i = 0; i < 7; i++)
		{
			AttackType.AddIconItem(mainParent.GetTechsSprites(iconOrder[i]), Tr("TechOrderType" + i));
			TypeOptions[0].AddIconItem(mainParent.GetTechsSprites(iconOrder[i]), Tr("TechOrderType" + i));
			TypeOptions[1].AddIconItem(mainParent.GetTechsSprites(iconOrder[i]), Tr("TechOrderType" + i));
			TypeOptions[2].AddIconItem(mainParent.GetTechsSprites(iconOrder[i]), Tr("TechOrderType" + i));
		}
		TypeOptions[0].AddItem(Tr("TechOrderNone"));
		TypeOptions[1].AddItem(Tr("TechOrderNone"));
		TypeOptions[2].AddItem(Tr("TechOrderNone"));
		TypeOptions[0].Selected = 7;
		TypeOptions[1].Selected = 7;
		TypeOptions[2].Selected = 7;
		AttackType.Selected = 0;
		CalculateTypeMultiplier();
	}

	Godot.Color GetColor(int value)
	{
		switch (value)
		{
			case 1: return new Godot.Color(0, 0, 0, 0.5f);
			case 2: return new Godot.Color(1.0f, 0, 0, 0.5f);
			case 5: return new Godot.Color(1.0f, 0.5f, 0, 0.5f);
			case 15: return new Godot.Color(0.0f, 1.0f, 0.5f, 0.5f);
			case 20: return new Godot.Color(0, 1.0f, 0, 0.5f);
			case 30: return new Godot.Color(0.0f, 0.5f, 1.0f, 0.5f);
			case 40: return new Godot.Color(0.0f, 0.0f, 1.0f, 0.5f);
			default: return new Godot.Color(1.0f, 1.0f, 0, 0.5f);
		}
	}

	void AttackSelect(int opt)
	{
		Attack = opt;
		type1 = GetDamageMultiplier(TypeOptions[0].Selected);
		type2 = GetDamageMultiplier(TypeOptions[1].Selected);
		type3 = GetDamageMultiplier(TypeOptions[2].Selected);
		CalculateTypeMultiplier();
	}

	void Type1Select(int opt)
	{
		type1 = GetDamageMultiplier(opt);
		CalculateTypeMultiplier();
	}
	void Type2Select(int opt)
	{
		type2 = GetDamageMultiplier(opt);
		CalculateTypeMultiplier();
	}

	void Type3Select(int opt)
	{
		type3 = GetDamageMultiplier(opt);
		CalculateTypeMultiplier();
	}

	void CalculateTypeMultiplier()
	{
		CalcValue.Text = ((type1 + type2 + type3) * 100 / 30).ToString() + "%";
	}

	int GetDamageMultiplier(int option)
	{
		if (option < 7)
			return damageTypes[iconOrder[Attack] * 7 + iconOrder[option]];
		else
			return noType;
	}
}
