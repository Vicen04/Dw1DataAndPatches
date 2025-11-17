using Godot;
using System;
using System.Collections.Generic;
using System.Dynamic;

using System.IO;

public partial class EvolutionCalculator : Control
{
	class EvolutionTargets { public List<int> evoTargets; }

	[Export] private Label SelectLevel;
	[Export] private Label SelectDigimon;
	[Export] private OptionButton DigimonOptions;
	[Export] private Label BasicReq;
	[Export] private Label BonusReq;
	[Export] private Label OffenseDig;
	[Export] private Label DefenseDig;
	[Export] private Label SpeedDig;
	[Export] private Label BrainsDig;
	[Export] private Label CareDig;
	[Export] private Label WeightDig;
	[Export] private Label HappyDig;
	[Export] private Label DiscDig;
	[Export] private Label BattlesDig;
	[Export] private Label TechsDig;
	[Export] private Label DigimonTitle;
	[Export] private Label ScoreTitle;
	[Export] private Label StatsTitle;
	[Export] private Label CareWeightTitle;
	[Export] private Label BonusTitle;
	[Export] private Label BonusDigTitle;
	[Export] private Control[] Evolutions;
	[Export] private CheckBox[] Obtained;
	[Export] private TextureRect[] Evolution;
	[Export] private Label[] Score;
	[Export] private TextureRect[] HPReq;
	[Export] private TextureRect[] MPReq;
	[Export] private TextureRect[] OffenseReq;
	[Export] private TextureRect[] DefenseReq;
	[Export] private TextureRect[] SpeedReq;
	[Export] private TextureRect[] BrainsReq;
	[Export] private TextureRect[] CareReq;
	[Export] private TextureRect[] WeightReq;
	[Export] private TextureRect[] HappyReq;
	[Export] private TextureRect[] DiscReq;
	[Export] private TextureRect[] BattleReq;
	[Export] private TextureRect[] TechsReq;
	[Export] private TextureRect[] BonusDigiReq;
	[Export] private Panel[] StatsPanels;
	[Export] private Panel[] MistakesPanels;
	[Export] private Panel[] WeightPanels;
	[Export] private Panel[] BonusPanels;
	[Export] private Panel[] BonusDigiPanels;
	[Export] AtlasTexture InfoIcons;

	[Export] SpinBox[] AllValues;

	[Export] CheckBox HideEvos;
	[Export] Panel errorPanel;
	[Export] Label ErrorLabel;


	private string[] DigimonNames = new string[66];
	private int[] DigimonLevels = new int[66];
	private EvolutionTargets[] evolutionTargets;
	private short[] HPRequired = new short[66];
	private short[] MPRequired = new short[66];
	private short[] OffenseRequired = new short[66];
	private short[] DefenseRequired = new short[66];
	private short[] SpeedRequired = new short[66];
	private short[] BrainsRequired = new short[66];
	private short[] WeightRequired = new short[66];
	private short[] CareRequired = new short[66];
	private short[] HappinessRequired = new short[66];
	private short[] DisciplineRequired = new short[66];
	private short[] BattlesRequired = new short[66];
	private short[] TechsRequired = new short[66];
	private short[] FlagValues = new short[66];
	private short[] BonusDigimon = new short[66];
	private Texture2D[] digimonSprites = new Texture2D[66];
	private Texture2D[] digimonCareSprites = new Texture2D[66];

	//private AtlasTexture DigimonIcons, TrainingStuff;
	private int currentHP = 0, currentMP = 0, currentOff = 0, currentDef = 0, currentSpd = 0, currentbrains = 0, currentMistakes = 0, currentHappiness = 0, currentDiscipline = 0,
	currentWeight = 0, currentBattles = 0, totalTechs = 0, currentDigimon = -1, minWeight = -5, maxWeight = 5;

	bool Maeson = false, vanilla = false;

	System.IO.Stream bin;
	BinaryReader reader;
	string filePath;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SelectLevel.Text = Tr("SelectDigLevel");
		SelectDigimon.Text = Tr("SelectedDig");
		BasicReq.Text = Tr("BasicReq");
		BasicReq.TooltipText = Tr("BasicReqInfo");
		BonusReq.Text = Tr("BonusC");
		BonusReq.TooltipText = Tr("BonusCInfo");
		OffenseDig.Text = Tr("OffenseC");
		DefenseDig.Text = Tr("DefenseC");
		SpeedDig.Text = Tr("SpeedC");
		BrainsDig.Text = Tr("BrainsC");
		CareDig.Text = Tr("CareC");
		CareDig.TooltipText = Tr("CareCInfo");
		WeightDig.Text = Tr("WeightC");
		HappyDig.Text = Tr("HappinessC");
		DiscDig.Text = Tr("DisciplineC");
		BattlesDig.Text = Tr("BattlesC");
		TechsDig.Text = Tr("TechsC");
		ScoreTitle.Text = Tr("ScoreDig");
		ScoreTitle.TooltipText = Tr("ScoreDigInfo");
		StatsTitle.Text = Tr("StatReq");
		CareWeightTitle.Text = Tr("Mis&W");
		BonusTitle.Text = Tr("BonusReq");
		BonusDigTitle.Text = Tr("BonusDig");
		HideEvos.Text = Tr("HideEvo");
		HideEvos.TooltipText = Tr("HideEvoInfo");
		DigimonTitle.TooltipText = Tr("DigimonObt");
		ErrorLabel.Text = Tr("CalculatorError");

		InfoIcons.Region = new Rect2(0, 0, 44, 44);
		Texture2D HPTexture = ImageTexture.CreateFromImage(InfoIcons.GetImage());
		InfoIcons.Region = new Rect2(44, 0, 44, 44);
		Texture2D MPTexture = ImageTexture.CreateFromImage(InfoIcons.GetImage());
		InfoIcons.Region = new Rect2(88, 0, 44, 44);
		Texture2D OffTex = ImageTexture.CreateFromImage(InfoIcons.GetImage());
		InfoIcons.Region = new Rect2(132, 0, 44, 44);
		Texture2D DefTex = ImageTexture.CreateFromImage(InfoIcons.GetImage());
		InfoIcons.Region = new Rect2(176, 0, 44, 44);
		Texture2D SpeedTex = ImageTexture.CreateFromImage(InfoIcons.GetImage());
		InfoIcons.Region = new Rect2(220, 0, 40, 44);
		Texture2D BrainsTex = ImageTexture.CreateFromImage(InfoIcons.GetImage());
		InfoIcons.Region = new Rect2(0, 44, 44, 44);
		Texture2D HappyTex = ImageTexture.CreateFromImage(InfoIcons.GetImage());
		InfoIcons.Region = new Rect2(44, 44, 44, 44);
		Texture2D DiscTex = ImageTexture.CreateFromImage(InfoIcons.GetImage());
		InfoIcons.Region = new Rect2(88, 44, 44, 44);
		Texture2D WeightTex = ImageTexture.CreateFromImage(InfoIcons.GetImage());
		InfoIcons.Region = new Rect2(132, 44, 44, 44);
		Texture2D BattleTex = ImageTexture.CreateFromImage(InfoIcons.GetImage());
		InfoIcons.Region = new Rect2(176, 44, 44, 44);
		Texture2D TechsTex = ImageTexture.CreateFromImage(InfoIcons.GetImage());
		for (int i = 0; i < 6; i++)
		{
			HPReq[i].Texture = HPTexture;
			MPReq[i].Texture = MPTexture;
			OffenseReq[i].Texture = OffTex;
			DefenseReq[i].Texture = DefTex;
			SpeedReq[i].Texture = SpeedTex;
			BrainsReq[i].Texture = BrainsTex;
			HappyReq[i].Texture = HappyTex;
			DiscReq[i].Texture = DiscTex;
			BattleReq[i].Texture = BattleTex;
			TechsReq[i].Texture = TechsTex;
			WeightReq[i].Texture = WeightTex;
			Obtained[i].TooltipText = Tr("CheckboxEvo");
			Obtained[i].Toggled += OnObtainedChanged;
		}
	}

	public void StartEvoCalculator(string path, ToolsHandler tools)
	{
		filePath = path;
		try
		{
			bin = System.IO.File.Open(filePath, FileMode.Open, System.IO.FileAccess.ReadWrite);
		}
		catch (System.ArgumentException ex)
		{
			GD.Print(ex.Message);
			errorPanel.Visible = true;
			ExitCalculator();
		}
		catch (System.IO.FileNotFoundException ex)
		{
			GD.Print(ex.Message);
			errorPanel.Visible = true;
			ExitCalculator();
		}
		catch (System.IO.IOException ex)
		{
			GD.Print(ex.Message);
			errorPanel.Visible = true;
			ExitCalculator();
		}

		reader = new BinaryReader(bin);

		try
		{
			SetupData(tools);
		}
		catch (System.ArgumentException ex)
		{
			GD.Print(ex.Message);
			errorPanel.Visible = true;
			if (reader != null)
				reader.Dispose();
			bin.Dispose();
			ExitCalculator();
		}
		catch (System.IO.FileNotFoundException)
		{
			GD.Print("file not found");
			errorPanel.Visible = true;
			if (reader != null)
				reader.Dispose();
			bin.Dispose();
			ExitCalculator();
		}
		catch (System.IO.IOException ex)
		{
			GD.Print(ex.Message);
			errorPanel.Visible = true;
			if (reader != null)
				reader.Dispose();
			bin.Dispose();
			ExitCalculator();
		}
	}

	void SetupData(ToolsHandler parent)
	{
		uint startOffset = 0x14D6CDF9, jumpOffset = 0x14D6CF98;

		evolutionTargets = new EvolutionTargets[66];

		for (int i = 0; i < 63; i++)
		{
			evolutionTargets[i] = new EvolutionTargets();
			evolutionTargets[i].evoTargets = new List<int>();
			if (i == 0)
				continue;
			for (int j = 0; j < 6; j++)
			{
				bin.Position = 11 * i + startOffset + 5 + j;
				if (bin.Position > jumpOffset)
					bin.Position = bin.Position + 0x130;
				int digimon = bin.ReadByte();

				if (digimon != 0xFF)
					evolutionTargets[i].evoTargets.Add(digimon);
			}
		}
		evolutionTargets[63] = new EvolutionTargets();
		evolutionTargets[63].evoTargets = evolutionTargets[62].evoTargets;

		uint LevelInitialOffset = 0x14D6E9F9, currentOffset = LevelInitialOffset;
		bin.Position = LevelInitialOffset;
		for (int i = 0; i < 66; i++)
		{

			int value = bin.ReadByte();
			currentOffset = currentOffset + 0x34;
			if (CheckIfECC((int)currentOffset))
				currentOffset = currentOffset + 0x130;

			bin.Position = currentOffset;

			DigimonLevels[i] = value;
		}

		currentOffset = 0x14D6E9DC;
		for (int i = 0; i < 66; i++)
		{
			bin.Position = currentOffset;

			DigimonNames[i] = System.Text.Encoding.Default.GetString(reader.ReadBytes(20));

			currentOffset = currentOffset + 0x34;
			if (CheckIfECC((int)currentOffset))
				currentOffset = currentOffset + 0x130;

		}

		startOffset = 0x14D6C254;
		jumpOffset = 0x14D6C668;

		for (int i = 0; i < 63; i++)
		{
			bin.Position = i * 28 + startOffset;
			if (bin.Position > jumpOffset)
				bin.Position = bin.Position + 0x130;

			BonusDigimon[i] = reader.ReadInt16();
			HPRequired[i] = reader.ReadInt16();
			MPRequired[i] = reader.ReadInt16();
			OffenseRequired[i] = reader.ReadInt16();
			JumpECC();
			DefenseRequired[i] = reader.ReadInt16();
			SpeedRequired[i] = reader.ReadInt16();
			BrainsRequired[i] = reader.ReadInt16();
			CareRequired[i] = reader.ReadInt16();
			WeightRequired[i] = reader.ReadInt16();
			DisciplineRequired[i] = reader.ReadInt16();
			HappinessRequired[i] = reader.ReadInt16();
			BattlesRequired[i] = reader.ReadInt16();
			TechsRequired[i] = reader.ReadInt16();
			FlagValues[i] = reader.ReadInt16();
		}

		//check if this is vanilla
		bin.Position = 0x14D19840;
		vanilla = bin.ReadByte() == 0x10;


		//check if this is Maeson
		bin.Position = 0x14D19A84;
		if (bin.ReadByte() == 0x3E)
		{
			Maeson = true;
			vanilla = false;
		}
		else				
			Maeson = false;
		

		//Set Maeson extra Digimon
		if (Maeson)
		{
			for (int i = 63; i < 66; i++)
			{
				BonusDigimon[i] = BonusDigimon[62];
				HPRequired[i] = HPRequired[62];
				MPRequired[i] = MPRequired[62];
				OffenseRequired[i] = OffenseRequired[62];
				DefenseRequired[i] = DefenseRequired[62];
				SpeedRequired[i] = SpeedRequired[62];
				BrainsRequired[i] = BrainsRequired[62];
				CareRequired[i] = CareRequired[62];
				WeightRequired[i] = WeightRequired[62];
				DisciplineRequired[i] = DisciplineRequired[62];
				HappinessRequired[i] = HappinessRequired[62];
				BattlesRequired[i] = BattlesRequired[62];
				TechsRequired[i] = TechsRequired[62];
				FlagValues[i] = FlagValues[62];
			}

			minWeight = -3;
			maxWeight = 2;
		}
		else if (!vanilla)
		{
			//Set up the extra Vice digimon
			int[] extraRequirements = new int[3];
			bin.Position = 0x14D19840;
			extraRequirements[0] = bin.ReadByte();
			bin.Position = 0x14D1984C;
			extraRequirements[1] = bin.ReadByte();
			bin.Position = 0x14D19858;
			extraRequirements[2] = bin.ReadByte();
			for (int i = 0; i < 3; i++)
			{
				BonusDigimon[i + 63] = BonusDigimon[extraRequirements[i]];
				HPRequired[i + 63] = HPRequired[extraRequirements[i]];
				MPRequired[i + 63] = MPRequired[extraRequirements[i]];
				OffenseRequired[i + 63] = OffenseRequired[extraRequirements[i]];
				DefenseRequired[i + 63] = DefenseRequired[extraRequirements[i]];
				SpeedRequired[i + 63] = SpeedRequired[extraRequirements[i]];
				BrainsRequired[i + 63] = BrainsRequired[extraRequirements[i]];
				CareRequired[i + 63] = CareRequired[extraRequirements[i]];
				WeightRequired[i + 63] = WeightRequired[extraRequirements[i]];
				DisciplineRequired[i + 63] = DisciplineRequired[extraRequirements[i]];
				HappinessRequired[i + 63] = HappinessRequired[extraRequirements[i]];
				BattlesRequired[i + 63] = BattlesRequired[extraRequirements[i]];
				TechsRequired[i + 63] = TechsRequired[extraRequirements[i]];
				FlagValues[i + 63] = FlagValues[extraRequirements[i]];
			}
		}

		for (int i = 0; i < 8; i++)
		{
			for (int j = 0; j < 16; j++)
			{
				if ((i * 16) + j > 65)
					break;

				digimonSprites[(i * 16) + j] = parent.GetDigimonTexture(j * 32, i * 64);
				digimonCareSprites[(i * 16) + j] = parent.GetDigimonTexture(j * 32, 32 + i * 64);
			}
		}

		bin.Position = 0x14D6F5C8;
		if (bin.ReadByte() == 86)
		{
			digimonSprites[47] = parent.GetDigimonExtraTexture(0, 0);
			digimonCareSprites[47] = parent.GetDigimonExtraTexture(0, 32);
		}

		bin.Position = 0x14D6F908;
		if (bin.ReadByte() == 87)
		{
			bin.Position = 0x14D6F924;

			if (bin.ReadByte() != 3)
			{
				digimonSprites[63] = digimonSprites[62];
				digimonCareSprites[63] = digimonCareSprites[62];
			}
			else
			{
				digimonSprites[63] = parent.GetDigimonExtraTexture(64, 0);
				digimonCareSprites[63] = parent.GetDigimonExtraTexture(64, 32);
			}
		}

		bin.Position = 0x14D6ED98;
		if (bin.ReadByte() != 3)
		{
			digimonSprites[12] = parent.GetDigimonExtraTexture(32, 0);
			digimonCareSprites[12] = parent.GetDigimonExtraTexture(32, 32);
		}

		bin.Position = 0x14D6F8D5;
		int byteCheck = bin.ReadByte();
		if (byteCheck != 97)
		{
			if (byteCheck == 121)
			{
				digimonSprites[62] = parent.GetDigimonTexture(64, 256);
				digimonCareSprites[62] = parent.GetDigimonTexture(64, 256 + 32);
			}
			else if (byteCheck == 108)
			{
				digimonSprites[62] = parent.GetDigimonTexture(288, 256);
				digimonCareSprites[62] = parent.GetDigimonTexture(288, 256 + 32);
			}
		}
		else
		{
			digimonSprites[62] = parent.GetDigimonTexture(96, 448);
			digimonCareSprites[62] = parent.GetDigimonTexture(96, 448 + 32);
		}

		for (int i = 0; i < DigimonNames.Length; i++)
		{
			if (DigimonLevels[i] == 3)
				DigimonOptions.AddIconItem(digimonSprites[i], DigimonNames[i], i);
		}

		DigimonOptions.Selected = -1;

		reader.Close();
		reader.Dispose();
		bin.Close();
		bin.Dispose();
	}

	void CheckEvolutions(int digimonID)
	{
		if (digimonID == -1)
			return;
		if (DigimonLevels[digimonID] == 1) //if the digimon is a baby
		{
			Evolutions[0].Visible = true;
			Evolution[0].Texture = digimonSprites[evolutionTargets[digimonID].evoTargets[0]];
			Evolution[0].TooltipText = DigimonNames[evolutionTargets[digimonID].evoTargets[0]];
			StyleBoxFlat StyleboxUpdate = Score[0].GetThemeStylebox("normal").Duplicate() as StyleBoxFlat;
			StyleboxUpdate.DrawCenter = true;
			StyleboxUpdate.BgColor = new Godot.Color(0, 1, 0.502f);
			Score[0].AddThemeStyleboxOverride("normal", StyleboxUpdate);
			Score[0].Text = "100";
			HPReq[0].Visible = false;
			MPReq[0].Visible = false;
			OffenseReq[0].Visible = false;
			DefenseReq[0].Visible = false;
			SpeedReq[0].Visible = false;
			BrainsReq[0].Visible = false;
			CareReq[0].Visible = false;
			WeightReq[0].Visible = false;
			HappyReq[0].Visible = false;
			DiscReq[0].Visible = false;
			BattleReq[0].Visible = false;
			TechsReq[0].Visible = false;
			BonusDigiReq[0].Visible = false;
			for (int i = 1; i < 6; i++)
				Evolutions[i].Visible = false;
		}
		else
		{
			CalculateEvolution(digimonID);
			int evolutions = 0;
			CareReq[0].Visible = true;
			WeightReq[0].Visible = true;
			for (; evolutions < evolutionTargets[digimonID].evoTargets.Count; evolutions++)
			{
				Obtained[evolutions].SetPressedNoSignal(false);
				int targetID = evolutionTargets[digimonID].evoTargets[evolutions];
				if (!HideEvos.ButtonPressed)
				{
					Evolution[evolutions].TooltipText = DigimonNames[targetID];
					Evolution[evolutions].Texture = digimonSprites[targetID];
					Evolution[evolutions].Visible = true;
				}
				else
					Evolution[evolutions].Visible = false;

				CareReq[evolutions].Texture = digimonCareSprites[digimonID];
				Evolutions[evolutions].Visible = true;
				HPReq[evolutions].Visible = true;
				MPReq[evolutions].Visible = true;
				OffenseReq[evolutions].Visible = true;
				DefenseReq[evolutions].Visible = true;
				SpeedReq[evolutions].Visible = true;
				BrainsReq[evolutions].Visible = true;
				HappyReq[evolutions].Visible = true;
				DiscReq[evolutions].Visible = true;
				BattleReq[evolutions].Visible = true;
				TechsReq[evolutions].Visible = true;
				BonusDigiReq[evolutions].Visible = true;

				if (HPRequired[targetID] > 0)
					HPReq[evolutions].TooltipText = HPRequired[targetID] * 10 + " HP";
				else
					HPReq[evolutions].Visible = false;
				if (MPRequired[targetID] > 0)
					MPReq[evolutions].TooltipText = MPRequired[targetID] * 10 + " MP";
				else
					MPReq[evolutions].Visible = false;
				if (OffenseRequired[targetID] > 0)
					OffenseReq[evolutions].TooltipText = OffenseRequired[targetID] + " " + Tr("OffenseC");
				else
					OffenseReq[evolutions].Visible = false;
				if (DefenseRequired[targetID] > 0)
					DefenseReq[evolutions].TooltipText = DefenseRequired[targetID] + " " + Tr("DefenseC");
				else
					DefenseReq[evolutions].Visible = false;
				if (SpeedRequired[targetID] > 0)
					SpeedReq[evolutions].TooltipText = SpeedRequired[targetID] + " " + Tr("SpeedC");
				else
					SpeedReq[evolutions].Visible = false;
				if (BrainsRequired[targetID] > 0)
					BrainsReq[evolutions].TooltipText = BrainsRequired[targetID] + " " + Tr("BrainsC");
				else
					BrainsReq[evolutions].Visible = false;

				CareReq[evolutions].TooltipText = CareRequired[targetID] + " ";
				if (FlagValues[targetID] / 16 == 0)
					CareReq[evolutions].TooltipText = CareReq[evolutions].TooltipText + Tr("CareMistakesH");
				else
					CareReq[evolutions].TooltipText = CareReq[evolutions].TooltipText + Tr("CareMistakesL");

				WeightReq[evolutions].TooltipText = Tr("BetweenC") + (WeightRequired[targetID] + minWeight) + Tr("ANDC") + (WeightRequired[targetID] + maxWeight) + Tr("WeightReqC");

				if (HappinessRequired[targetID] != -1)
					HappyReq[evolutions].TooltipText = HappinessRequired[targetID] + " " + Tr("HappinessC");
				else
					HappyReq[evolutions].Visible = false;

				if (DisciplineRequired[targetID] != -1)
					DiscReq[evolutions].TooltipText = DisciplineRequired[targetID] + " " + Tr("DisciplineC");
				else
					DiscReq[evolutions].Visible = false;


				if (BattlesRequired[targetID] != -1)
				{
					if (!Maeson)
					{
						BattleReq[evolutions].TooltipText = BattlesRequired[targetID] + " ";

						if (FlagValues[targetID] % 1 == 0)
							BattleReq[evolutions].TooltipText = BattleReq[evolutions].TooltipText + Tr("BattlesH");
						else
							BattleReq[evolutions].TooltipText = BattleReq[evolutions].TooltipText + Tr("BattlesL");
					}
					else
					{
						BattleReq[evolutions].TooltipText = Tr("BetweenC") + (BattlesRequired[targetID] - 3) + Tr("ANDC") + BattlesRequired[targetID] + " " + Tr("BattlesC");
					}
				}
				else
					BattleReq[evolutions].Visible = false;

				if (TechsRequired[targetID] != -1)
					TechsReq[evolutions].TooltipText = TechsRequired[targetID] + " " + Tr("TechsNumberC");
				else
					TechsReq[evolutions].Visible = false;

				if (BonusDigimon[targetID] != -1)
				{
					BonusDigiReq[evolutions].TooltipText = DigimonNames[BonusDigimon[targetID]];
					BonusDigiReq[evolutions].Texture = digimonSprites[BonusDigimon[targetID]];
				}
				else
					BonusDigiReq[evolutions].Visible = false;
			}

			if (evolutions < 6)
				for (; evolutions < 6; evolutions++)
					Evolutions[evolutions].Visible = false;
		}
	}


	void CalculateEvolution(int digimonID)
	{
		int currentTarget, score, sum, reqCount, bestScore, finalTarget;

		reqCount = 0;
		sum = 0;
		bestScore = 0;
		finalTarget = -1;
		for (int i = 0; i < evolutionTargets[digimonID].evoTargets.Count; i++)
		{
			currentTarget = evolutionTargets[digimonID].evoTargets[i];
			if (currentTarget != -1)
			{
				score = CalculateRequirementScore(digimonID, currentTarget, FlagValues[currentTarget] / 16, FlagValues[currentTarget] % 1, finalTarget, Obtained[i].ButtonPressed, i);
				if (score > 2)
				{
					if (HPRequired[currentTarget] != -1)
					{
						sum = sum + currentHP / 10;
						reqCount++;
					}
					if (MPRequired[currentTarget] != -1)
					{
						sum = sum + currentMP / 10;
						reqCount++;
					}
					if (OffenseRequired[currentTarget] != -1)
					{
						sum = sum + currentOff;
						reqCount++;
					}
					if (DefenseRequired[currentTarget] != -1)
					{
						sum = sum + currentDef;
						reqCount++;
					}
					if (SpeedRequired[currentTarget] != -1)
					{
						sum = sum + currentSpd;
						reqCount++;
					}
					if (BrainsRequired[currentTarget] != -1)
					{
						sum = sum + currentbrains;
						reqCount++;
					}
					score = sum / reqCount;
					sum = score;
					if (bestScore < sum)
					{
						bestScore = score;
						finalTarget = i;
						reqCount = 0;
						sum = 0;
					}
					if (!vanilla)
					{
						reqCount = 0;
						sum = 0;
					}
					Score[i].Text = score.ToString();
					StyleBoxFlat StyleboxUpdate = Score[i].GetThemeStylebox("normal").Duplicate() as StyleBoxFlat;
					StyleboxUpdate.DrawCenter = false;
					Score[i].AddThemeStyleboxOverride("normal", StyleboxUpdate);
				}
				else
				{
					Score[i].Text = "0";
					StyleBoxFlat StyleboxUpdate = Score[i].GetThemeStylebox("normal").Duplicate() as StyleBoxFlat;
					StyleboxUpdate.DrawCenter = true;
					StyleboxUpdate.BgColor = new Godot.Color(1, 0, 0);
					Score[i].AddThemeStyleboxOverride("normal", StyleboxUpdate);
				}
			}

		}
		if (finalTarget != -1)
		{
			StyleBoxFlat StyleboxUpdate = Score[finalTarget].GetThemeStylebox("normal").Duplicate() as StyleBoxFlat;
			StyleboxUpdate.DrawCenter = true;
			StyleboxUpdate.BgColor = new Godot.Color(0, 1, 0.502f);
			Score[finalTarget].AddThemeStyleboxOverride("normal", StyleboxUpdate);
		}
	}

	int CalculateRequirementScore(int current, int target, int isMaxCM, int isMaxBattles, int currentBest, bool obtained, int currentLoop)
	{
		int highestStat;
		int reqPoints;
		int finalScore;
		int isBonusFulfilled;
		int[] statsArray = new int[6];
		int[] reqArray = new int[6];
		short battlesRequirement;
		bool isHighestStat;

		StyleBoxFlat StyleboxUpdate;

		reqPoints = 0;
		if (isMaxCM == 0 && CareRequired[target] <= currentMistakes)
			reqPoints = 1;
		else if (isMaxCM == 1 && currentMistakes <= CareRequired[target])
			reqPoints = 1;

		StyleboxUpdate = MistakesPanels[currentLoop].GetThemeStylebox("panel").Duplicate() as StyleBoxFlat;
		if (reqPoints == 1)
			StyleboxUpdate.BgColor = new Godot.Color(1, 0.894f, 0, 0.5f);
		else
			StyleboxUpdate.BgColor = new Godot.Color(0.6f, 0.6f, 0.6f, 0.5f);
		MistakesPanels[currentLoop].AddThemeStyleboxOverride("panel", StyleboxUpdate);

		StyleboxUpdate = WeightPanels[currentLoop].GetThemeStylebox("panel").Duplicate() as StyleBoxFlat;
		if ((WeightRequired[target] + minWeight <= currentWeight) && (currentWeight <= WeightRequired[target] + maxWeight))
		{
			reqPoints++;
			StyleboxUpdate.BgColor = new Godot.Color(1, 0.549f, 0, 0.5f);

		}
		else
			StyleboxUpdate.BgColor = new Godot.Color(0.6f, 0.6f, 0.6f, 0.5f);
		WeightPanels[currentLoop].AddThemeStyleboxOverride("panel", StyleboxUpdate);
		//Evolve to Rookie
		if (DigimonLevels[target] == 3)
		{
			statsArray[0] = currentHP / 10;
			statsArray[1] = currentMP / 10;
			statsArray[2] = currentOff;
			statsArray[3] = currentDef;
			statsArray[4] = currentSpd;
			statsArray[5] = currentbrains;
			reqArray[0] = HPRequired[target];
			reqArray[1] = MPRequired[target];
			reqArray[2] = OffenseRequired[target];
			reqArray[3] = DefenseRequired[target];
			reqArray[4] = SpeedRequired[target];
			reqArray[5] = BrainsRequired[target];

			highestStat = 0;
			for (int i = 0; i < 6; i = i + 1)
			{
				isHighestStat = true;
				for (int j = 0; j < 6; j = j + 1)
				{
					if (statsArray[i] < statsArray[j])
					{
						isHighestStat = false;
					}

				}
				if (isHighestStat)
					highestStat = i;

			}
			StyleboxUpdate = StatsPanels[currentLoop].GetThemeStylebox("panel").Duplicate() as StyleBoxFlat;
			if (reqArray[highestStat] > 0)
			{
				reqPoints++;
				StyleboxUpdate.BgColor = new Godot.Color(0, 0, 1, 0.5f);

			}
			else
				StyleboxUpdate.BgColor = new Godot.Color(0.6f, 0.6f, 0.6f, 0.5f);


			StatsPanels[currentLoop].AddThemeStyleboxOverride("panel", StyleboxUpdate);

		}
		//Evolve to Champion or Ultimate
		else
		{
			StyleboxUpdate = StatsPanels[currentLoop].GetThemeStylebox("panel").Duplicate() as StyleBoxFlat;
			if (HPRequired[target] <= currentHP / 10 && MPRequired[target] <= currentMP / 10 && OffenseRequired[target] <= currentOff &&
				DefenseRequired[target] <= currentDef && SpeedRequired[target] <= currentSpd && BrainsRequired[target] <= currentbrains)
			{
				reqPoints++;
				StyleboxUpdate.BgColor = new Godot.Color(0, 0, 1, 0.5f);
			}
			else
				StyleboxUpdate.BgColor = new Godot.Color(0.6f, 0.6f, 0.6f, 0.5f);
			StatsPanels[currentLoop].AddThemeStyleboxOverride("panel", StyleboxUpdate);

		}
		isBonusFulfilled = 0;
		if ((BonusDigimon[target] != -1) && (current == BonusDigimon[target]))
			isBonusFulfilled = 1;

		if ((DisciplineRequired[target] != -1) && (DisciplineRequired[target] <= currentDiscipline))
			isBonusFulfilled = 1;

		if ((HappinessRequired[target] != -1) && (HappinessRequired[target] <= currentHappiness))
			isBonusFulfilled = 1;

		battlesRequirement = BattlesRequired[target];
		if (battlesRequirement != -1)
		{
			if (!Maeson)
			{
				if (isMaxBattles == 0 && battlesRequirement <= currentBattles)
					isBonusFulfilled = 1;
				else if (isMaxBattles == 1 && currentBattles <= battlesRequirement)
					isBonusFulfilled = 1;
			}
			else
			{
				if (currentBattles <= battlesRequirement && battlesRequirement - 3 <= currentBattles)
					isBonusFulfilled = 1;
			}

		}
		if ((TechsRequired[target] != -1) && (TechsRequired[target] <= totalTechs))
			isBonusFulfilled = 1;

		StyleboxUpdate = BonusPanels[currentLoop].GetThemeStylebox("panel").Duplicate() as StyleBoxFlat;
		if (isBonusFulfilled > 0)
			StyleboxUpdate.BgColor = new Godot.Color(0.867f, 0.235f, 0.867f, 0.5f);
		else
			StyleboxUpdate.BgColor = new Godot.Color(0.6f, 0.6f, 0.6f, 0.5f);
		BonusPanels[currentLoop].AddThemeStyleboxOverride("panel", StyleboxUpdate);
		BonusDigiPanels[currentLoop].AddThemeStyleboxOverride("panel", StyleboxUpdate);

		finalScore = reqPoints + isBonusFulfilled;
		if ((2 < finalScore) && (currentBest != -1))
		{
			if (obtained && !Obtained[currentBest].ButtonPressed)
				finalScore = 0;

			if (!obtained && Obtained[currentBest].ButtonPressed)
				finalScore = finalScore++;

		}
		return finalScore;
	}

	void JumpECC()
	{
		int position = (int)bin.Position;
		position = position - 24;
		position = position % 0x930;

		if (position >= 0x800)
			bin.Position = bin.Position + 0x130;
	}
	bool CheckIfECC()
	{
		int position = (int)bin.Position;
		position = position - 24;
		position = position % 0x930;

		if (position >= 0x800)
		{
			return true;
		}
		return false;
	}

	bool CheckIfECC(int position)
	{
		position = position - 24;
		position = position % 0x930;

		if (position >= 0x800)
		{
			return true;
		}
		return false;
	}

	void OnDigiLevel(int selected)
	{
		DigimonOptions.Clear();
		for (int i = 0; i < DigimonNames.Length; i++)
		{
			if (DigimonLevels[i] == selected + 1)
				DigimonOptions.AddIconItem(digimonSprites[i], DigimonNames[i], i);
		}
		DigimonOptions.Selected = 0;
		CheckEvolutions(DigimonOptions.GetItemId(0));
	}

	void OnDigimonSelected(int selected)
	{
		currentDigimon = DigimonOptions.GetItemId(selected);
		CheckEvolutions(currentDigimon);
	}

	void HPChanged(float value)
	{
		currentHP = (int)value;
		CalculateEvolution(currentDigimon);
	}

	void MPChanged(float value)
	{
		currentMP = (int)value;
		CalculateEvolution(currentDigimon);
	}

	void OffenseChanged(float value)
	{
		currentOff = (int)value;
		CalculateEvolution(currentDigimon);
	}

	void DefenseChanged(float value)
	{
		currentDef = (int)value;
		CalculateEvolution(currentDigimon);
	}

	void SpeedChanged(float value)
	{
		currentSpd = (int)value;
		CalculateEvolution(currentDigimon);
	}

	void BrainsChanged(float value)
	{
		currentbrains = (int)value;
		CalculateEvolution(currentDigimon);
	}

	void CareChanged(float value)
	{
		currentMistakes = (int)value;
		CalculateEvolution(currentDigimon);
	}

	void WeightChanged(float value)
	{
		currentWeight = (int)value;
		CalculateEvolution(currentDigimon);
	}

	void HappyChanged(float value)
	{
		currentHappiness = (int)value;
		CalculateEvolution(currentDigimon);
	}

	void DiscChanged(float value)
	{
		currentDiscipline = (int)value;
		CalculateEvolution(currentDigimon);
	}

	void BattlesChanged(float value)
	{
		currentBattles = (int)value;
		CalculateEvolution(currentDigimon);
	}

	void TechsChanged(float value)
	{
		totalTechs = (int)value;
		CalculateEvolution(currentDigimon);
	}

	void OnObtainedChanged(bool changed)
	{
		CalculateEvolution(currentDigimon);
	}

	void ExitCalculator()
	{
		DigimonOptions.Clear();
		evolutionTargets = null;
		currentHP = 0;
		currentMP = 0;
		currentOff = 0;
		currentDef = 0;
		currentSpd = 0;
		currentbrains = 0;
		currentMistakes = 0;
		currentHappiness = 0;
		currentDiscipline = 0;
		currentWeight = 0;
		currentBattles = 0;
		totalTechs = 0;
		currentDigimon = -1;
		minWeight = -5;
		maxWeight = 5;
		foreach (Control evo in Evolutions)
			evo.Visible = false;

		foreach (SpinBox value in AllValues)
			value.SetValueNoSignal(0);
	}

	void ErrorLoading()
	{
		errorPanel.Visible = false;
	}
}
