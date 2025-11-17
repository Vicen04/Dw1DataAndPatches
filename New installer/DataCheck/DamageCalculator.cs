using Godot;
using System;

public partial class DamageCalculator : Control
{
	[Export] Label AttackerTitle;
	[Export] Label DefenderTitle;
	[Export] Label Attacker;
	[Export] Label Defender;
	[Export] Label Attack;
	[Export] Label Defend;
	[Export] Label SpeedAtk;
	[Export] Label SpeedDef;
	[Export] Label Tech;
	[Export] Label MaxDamageLabel;
	[Export] Label MinDamageLabel;
	[Export] Label Accuracy;
	[Export] OptionButton AttackerOpt;
	[Export] OptionButton DefenderOpt;
	[Export] SpinBox AttackValue;
	[Export] SpinBox DefendValue;
	[Export] SpinBox SpeedAtkValue;
	[Export] SpinBox SpeedDefValue;
	[Export] OptionButton TechOpt;
	[Export] Label MaxDamageLabelValue;
	[Export] Label MinDamageLabelValue;
	[Export] Label AccuracyValue;
	[Export] CheckBox isBlocking;
	[Export] CheckBox isFrozen;
	[Export] CheckBox isBurnt;
	[Export] CheckBox isShocked;

	int[] typeDamages = new int[49], techBoostList, techBoostedList;
	int noType = 10, cAttack = 1, cDefense = 1, cSpdAtk = 1, cSpdDef = 1, digimonSel = -1, defSel = -1, techSel = -1, maxDef = 500, range = 20, min = 90, extra = 0,
	extraBoost = 0, isVice = 0, maxFinisher = 100, minFinisher = 100;
	uint typeDamageOffset = 0x14D669F8;
	bool techBoost = false, hardcore = false, ultra = false, newVice = false, isBaby = false;
	TechStuff parent;
	DataCheck mainParent;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AttackerTitle.Text = Tr("AttackerCalcCheckT");
		DefenderTitle.Text = Tr("DefenderCalcCheckT");
		Attacker.Text = Tr("AttackerCalcCheck");
		Defender.Text = Tr("DefenderCalcCheck");
		Attack.Text = Tr("AttackCalcCheck");
		Defend.Text = Tr("DefenseCalcCheck");
		SpeedAtk.Text = Tr("SpeedAtkCalcCheck");
		SpeedDef.Text = Tr("SpeedDefCalcCheck");
		Tech.Text = Tr("TechCalcCheck");
		MaxDamageLabel.Text = Tr("MaxDamageCheck");
		MinDamageLabel.Text = Tr("MinDamageCheck");
		Accuracy.Text = Tr("AccuracyCalcCheck");
		isBlocking.Text = Tr("BlockingCalcCheck");
		isFrozen.Text = Tr("FreezeEffect");
		isBurnt.Text = Tr("BurnEffect");
		isShocked.Text = Tr("ShockEffect");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetupData(System.IO.Stream bin, System.IO.BinaryReader reader, TechStuff p, DataCheck mainp, bool vice)
	{
		parent = p;
		mainParent = mainp;
		cAttack = cDefense = cSpdAtk = cSpdDef = 1;
		AttackValue.SetValueNoSignal(1);
		DefendValue.SetValueNoSignal(1);
		SpeedAtkValue.SetValueNoSignal(1);
		SpeedDefValue.SetValueNoSignal(1);
		isBlocking.SetPressedNoSignal(false);
		isFrozen.SetPressedNoSignal(false);
		isBurnt.SetPressedNoSignal(false);
		isShocked.SetPressedNoSignal(false);

		AttackerOpt.Clear();
		DefenderOpt.Clear();
		TechOpt.Clear();

		bin.Position = 0x14B586A0;

		if (bin.ReadByte() != 0xD0) //checks if the Attack function still exists there
			newVice = true;
		else
			newVice = false;

		bin.Position = typeDamageOffset;
		for (int i = 0; i < 49; i++)
		{
			int typeDamage = bin.ReadByte();
			typeDamages[i] = typeDamage;
		}
		if (!newVice)
			bin.Position = 0x14B58744;
		else
			bin.Position = 0x14CC0E68;
		noType = bin.ReadByte();

		for (int i = 1; i < 180; i++)
		{
			if (i < 114 || i > 128)
			{
				string name = mainParent.GetDigimonData(i).name;
				if (i < 114)
				{
					AttackerOpt.AddIconItem(mainParent.GetDigimonData(i).digimonSprite, name, i);
					DefenderOpt.AddIconItem(mainParent.GetDigimonData(i).digimonSprite, name, i);
				}
				else
				{
					AttackerOpt.AddIconItem(mainParent.GetDigimonData(i).digimonSprite, name + " NPC", i);
					DefenderOpt.AddIconItem(mainParent.GetDigimonData(i).digimonSprite, name + " NPC", i);
				}
			}
		}
		AttackerOpt.AddIconItem(mainParent.GetDigimonData(115).digimonSprite, mainParent.GetDigimonData(115).name + " NPC", 115);
		DefenderOpt.AddIconItem(mainParent.GetDigimonData(115).digimonSprite, mainParent.GetDigimonData(115).name + " NPC", 115);

		AttackerOpt.Selected = -1;
		DefenderOpt.Selected = -1;
		TechOpt.Selected = -1;


		bin.Position = 0x14B58848; //Check if there is change in the attack code
		if (reader.ReadInt16() != 6177)
		{
			techBoost = true;
			bin.Position = 0x14CF1308;
			if (bin.ReadByte() == 219)  //check if there is a jump in the render attack values function
			{
				if (!newVice)
					bin.Position = 0x14CED42C; //check if a hard difficulty is enabled
				else
					bin.Position = 0x14CC0F86;
				if (bin.ReadByte() == 1)
				{
					if (!newVice)
						bin.Position = 0x14CCFE14;
					else
						bin.Position = 0x14CC0F90;
					extraBoost = reader.ReadInt16();
				}
				else
					extraBoost = 0;
				ultra = false;
			}
			else
			{
				if (!newVice)
					bin.Position = 0x14CCFE34;
				else
					bin.Position = 0x14CC0FAC;
				extraBoost = reader.ReadInt16();
				ultra = true;
			}
			techBoostList = new int[66];
			techBoostedList = new int[66];
			uint startOffset = 0x14D62802, attackOffset = 0x14D627FF;
			long pos, attPos;
			bool jumped = false;
			for (int i = 0; i < 66; i++)
			{
				if (!jumped)
				{
					pos = i * 0x1C + startOffset;
					attPos = i * 0x1C + attackOffset;
				}
				else
				{
					pos = i * 0x1C + startOffset + 0x130;
					attPos = i * 0x1C + attackOffset + 0x130;
				}

				if (pos > 0x14D62A38 && !jumped)
				{
					jumped = true;
					pos = i * 0x1C + startOffset + 0x130;
					attPos = i * 0x1C + attackOffset + 0x130;
				}
				bin.Position = pos;
				techBoostList[i] = reader.ReadInt16();
				bin.Position = attPos;
				techBoostedList[i] = bin.ReadByte();
			}
		}
		else
			techBoost = false;

		if (vice)
		{
			isVice = 1;
			isFrozen.Visible = true;
			isBurnt.Visible = true;
			isShocked.Visible = true;
		}
		else
		{
			isVice = 0;
			isFrozen.Visible = false;
			isBurnt.Visible = false;
			isShocked.Visible = false;
		}

		

		if (!newVice)
		{
			bin.Position = 0x14B5883C;
			range = reader.ReadInt16() - 1;
			bin.Position = 0x14B5886C;
			min = reader.ReadInt16();

			bin.Position = 0x14B58960;
			maxFinisher = reader.ReadInt16();
			bin.Position = 0x14B58964;
			minFinisher = reader.ReadInt16();

			bin.Position = 0x14B58818;
			maxDef = reader.ReadInt16() - 1;
		}

		else
		{
			bin.Position = 0x14CC0F60;
			range = reader.ReadInt16() - 1;
			bin.Position = 0x14CC0FE4;
			min = reader.ReadInt16();

			bin.Position = 0x14CC10D8;
			maxFinisher = reader.ReadInt16();
			bin.Position = 0x14CC10DC;
			minFinisher = reader.ReadInt16();

			bin.Position = 0x14CC0F3C;
			maxDef = reader.ReadInt16() - 1;
		}
	}

	void AttackChanged(float value)
	{
		cAttack = (int)value;
		Calculate();
	}

	void DefenseChanged(float value)
	{
		cDefense = (int)value;
		Calculate();
	}

	void SpdAtkChanged(float value)
	{
		cSpdAtk = (int)value;
		Calculate();
	}

	void SpdDefChanged(float value)
	{
		cSpdDef = (int)value;
		Calculate();
	}

	void DigimonSelected(int selected)
	{
		TechOpt.Clear();
		foreach (int tech in mainParent.GetDigimonData(AttackerOpt.GetItemId(selected)).Attacks)
		{
			TechOpt.AddIconItem(mainParent.GetTechsSprites(parent.GetTechData(tech).type), parent.GetTechData(tech).name, tech);
		}
		TechOpt.AddIconItem(mainParent.GetTechsSprites(parent.GetTechData(121).type), parent.GetTechData(121).name, 121);
		TechOpt.Selected = -1;
		digimonSel = AttackerOpt.GetItemId(selected);		
		Calculate();
	}

	void DefenderSelected(int selected)
	{
		defSel = DefenderOpt.GetItemId(selected);
		
		var attacks = mainParent.GetDigimonData(AttackerOpt.GetItemId(selected)).Attacks;
		if (attacks.Count > 0 && attacks[0] > 100)
			isBaby = true;
		else
			isBaby = false;

		Calculate();
	}

	void TechSelected(int tech)
	{
		techSel = TechOpt.GetItemId(tech);
		Calculate();
	}

	void BlockToggled(bool toggled)
	{
		Calculate();
	}

	void Calculate()
	{
		if (digimonSel != -1 && defSel != -1 && techSel != -1)
		{

			int damage = 0;
			if (techBoost)
			{
				if (ultra)
				{
					if (digimonSel > 65)
						damage = damage + parent.GetTechData(techSel).power + extraBoost;
					else
						damage = damage + parent.GetTechData(techSel).power;
				}
				else
				{
					if (digimonSel > 65)
						damage = damage + parent.GetTechData(techSel).power + extraBoost;
					else if (techBoostedList[digimonSel] == techSel)
						damage = damage + parent.GetTechData(techSel).power + techBoostList[digimonSel];
					else
						damage = damage + parent.GetTechData(techSel).power;
				}
				
			}
			else
				damage = damage + parent.GetTechData(techSel).power;

			if (techSel < 58 - isVice || techSel > 112)
			{
				int sumDamage = cDefense;
				if (techSel == 45)
					sumDamage = sumDamage * 3 / 10;
				sumDamage = cAttack - sumDamage;

				if (sumDamage > maxDef)
					sumDamage = maxDef;
				else if (sumDamage < -maxDef)
					sumDamage = -maxDef;

				damage = sumDamage * damage / 500 + damage;
				if (damage <= 0)
					damage = 1;
			}
			int type1 = mainParent.GetDigimonData(defSel).Types[0];
			if (type1 == 255)
				type1 = noType;
			else			
				type1 = typeDamages[parent.GetTechData(techSel).type * 7 + type1];
			int type2 = mainParent.GetDigimonData(defSel).Types[1];
			if (type2 == 255)
				type2 = noType;
			else			
				type2 = typeDamages[parent.GetTechData(techSel).type * 7 + type2];
			int type3 = mainParent.GetDigimonData(defSel).Types[2];
			if (type3 == 255)
				type3 = noType;
			else			
				type3 = typeDamages[parent.GetTechData(techSel).type * 7 + type3];

			damage = damage * (type1 + type2 + type3);

			int moveAccuracy = parent.GetTechData(techSel).accuracy;
			int blockChance = moveAccuracy / 2 * (cSpdDef - (cSpdAtk / 10)) / 1998;
			if (isBlocking.ButtonPressed)
				blockChance = blockChance * 6 / 5;

			if (isShocked.ButtonPressed)
				blockChance = blockChance / 2;

			if (!isBaby)
				AccuracyValue.Text = (moveAccuracy - blockChance).ToString();
			else
				AccuracyValue.Text = "100";

				int maxDamage = 1, minDamage = 1;
			if (techSel < 58 - isVice || techSel > 112)
			{
				maxDamage = damage / 30 * (min + range) / 100;									
				minDamage = damage / 30 * min / 100;
				if (isBurnt.ButtonPressed)
				{
					maxDamage = maxDamage - maxDamage / 4;
					minDamage = minDamage - minDamage / 4;
				}
				if (maxDamage <= 0)
					maxDamage = 1;
				if (minDamage <= 0)
					minDamage = 1;
				if (isFrozen.ButtonPressed)
				{
					maxDamage = maxDamage / 4 + maxDamage;
					minDamage = minDamage / 4 + minDamage;
				}
				
				if (maxDamage > 9999)
					maxDamage = 9999;
				if (minDamage > 9999)
					minDamage = 9999;
				MaxDamageLabelValue.Text = maxDamage.ToString();
				MinDamageLabelValue.Text = minDamage.ToString();
			}
			else
			{
				if (digimonSel < 66)
				{
					maxDamage = damage / 30 * 200 / 100 * (90 + 20) / 100;
					minDamage = damage / 30 * 90 / 100;
					
					if (isBurnt.ButtonPressed)
					{
						maxDamage = maxDamage - maxDamage / 4;
						minDamage = minDamage - minDamage / 4;
					}
					if (maxDamage <= 0)
						maxDamage = 1;
					if (minDamage <= 0)
						minDamage = 1;
						
					if (isFrozen.ButtonPressed)
					{
						maxDamage = maxDamage / 4 + maxDamage;
						minDamage = minDamage / 4 + minDamage;
					}

					if (maxDamage > 9999)
						maxDamage = 9999;
					if (minDamage > 9999)
						minDamage = 9999;
					MaxDamageLabelValue.Text = maxDamage.ToString();
					MinDamageLabelValue.Text = minDamage.ToString();
				}
				else
				{
					maxDamage = damage / 30 * (minFinisher + maxFinisher) / 100 * (90 + 20) / 100;					
					minDamage = damage / 30 * minFinisher / 100 * 90 / 100;
					if (isBurnt.ButtonPressed)
					{
						maxDamage = maxDamage - maxDamage / 4;
						minDamage = minDamage - minDamage / 4;
					}
					if (maxDamage <= 0)
						maxDamage = 1;
					if (minDamage <= 0)
						minDamage = 1;

					if (isFrozen.ButtonPressed)
					{
						maxDamage = maxDamage / 4 + maxDamage;
						minDamage = minDamage / 4 + minDamage;
					}
					if (maxDamage > 9999)
						maxDamage = 9999;
					if (minDamage > 9999)
						minDamage = 9999;
					MaxDamageLabelValue.Text = maxDamage.ToString();
					MinDamageLabelValue.Text = minDamage.ToString();
				}
				AccuracyValue.Text = "100";
			}
			

			

		}
	}

	
}
