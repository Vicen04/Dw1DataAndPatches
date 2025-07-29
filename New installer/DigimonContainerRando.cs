using Godot;
using System;

public partial class DigimonContainerRando : PanelContainer
{
	[Export]
	private Label DigimonNPC;

	[Export]
	private Label StarterDigimon;

	[Export]
	private Label OtherPatches;

	[Export]
	private CheckBox Difficulty;

	[Export]
	private CheckBox Digimon;

	[Export]
	private CheckBox NPCStats;

	[Export]
	private CheckBox NPCTechs;

	[Export]
	private CheckBox NPCMoney;

	[Export]
	private CheckBox Bosses;

	[Export]
	private CheckBox Starter;

	[Export]
	private CheckBox StarterTech;

	[Export]
	private CheckBox StarterLevel;

	[Export]
	private CheckBox StarterStats;

	[Export]
	private CheckBox TournamentsNPC;

	[Export]
	private CheckBox Recruitments;

	[Export]
	private OptionButton DifficultyOpt;

	[Export]
	private OptionButton DigimonOpt;

	[Export]
	private OptionButton NPCStatsOpt;

	[Export]
	private OptionButton NPCMoneyOpt;

	[Export]
	private OptionButton StarterTechOpt;

	[Export]
	private OptionButton StarterLevelOpt;

	[Export]
	private OptionButton StarterStatsOpt;

	[Export]
	private OptionButton TournamentsNPCOpt;

	[Export]
	private OptionButton RecruitmentsOpt;

	[Export]
	private RandomizerContainer baseScript;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SetUpButtonsTranslations();
		SetUpOptionsTranslations();
		SetUpButtons();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	void DifficultyToggled(bool toggled)
	{
		DifficultyOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetDifficulty(toggled, DifficultyOpt.Selected);
		else
			baseScript.SetDifficulty(toggled, -1);
	}

	void DigimonToggled(bool toggled)
	{
		DigimonOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetDigimonNPC(toggled, DigimonOpt.Selected);
		else
			baseScript.SetDigimonNPC(toggled, -1);
	}

	void NPCStatsToggled(bool toggled)
	{
		NPCStatsOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetStatsNPC(toggled, NPCStatsOpt.Selected);
		else
			baseScript.SetStatsNPC(toggled, -1);
	}

	void NPCTechsToggled(bool toggled)
	{
		baseScript.SetTechNPC(toggled);

	}

	void NPCMoneyToggled(bool toggled)
	{
		NPCMoneyOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetMoneyNPC(toggled, NPCMoneyOpt.Selected);
		else
			baseScript.SetMoneyNPC(toggled, -1);
	}

	void BossesToggled(bool toggled)
	{
		baseScript.SetBosses(toggled);
	}

	void StarterToggled(bool toggled)
	{
		baseScript.SetStarter(toggled);
		if (toggled)
		{
			StarterTech.Disabled = false;
			StarterLevel.Disabled = false;
			StarterStats.Disabled = false;
		}
		else
		{
			StarterTech.Disabled = true;
			StarterTech.ButtonPressed = false;
			StarterLevel.Disabled = true;
			StarterLevel.ButtonPressed = false;
			StarterStats.Disabled = true;
			StarterStats.ButtonPressed = false;
		}
	}

	void StarterTechToggled(bool toggled)
	{
		StarterTechOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetStarterTech(toggled, StarterTechOpt.Selected);
		else
			baseScript.SetStarterTech(toggled, -1);
	}

	void StarterLevelToggled(bool toggled)
	{
		StarterLevelOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetStarterLevel(toggled, StarterLevelOpt.Selected);
		else
			baseScript.SetStarterLevel(toggled, -1);
	}

	void StarterStatsToggled(bool toggled)
	{
		StarterStatsOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetStarterStats(toggled, StarterStatsOpt.Selected);
		else
			baseScript.SetStarterStats(toggled, -1);
	}

	void TournamentsNPCToggled(bool toggled)
	{
		TournamentsNPCOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetTournamentNPC(toggled, TournamentsNPCOpt.Selected);
		else
			baseScript.SetTournamentNPC(toggled, -1);
	}

	void RecruitmentsToggled(bool toggled)
	{
		RecruitmentsOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetRecruits(toggled, RecruitmentsOpt.Selected);
		else
			baseScript.SetRecruits(toggled, -1);
	}

	void DifficultyOptSelected(int value)
	{
		baseScript.SetDifficultyOpt(value);
	}

	void NPCStatsOptelected(int value)
	{
		baseScript.SetStatsNPCOpt(value);
	}

	void DigimonOptSelected(int value)
	{
		baseScript.SetDigimonNPCOpt(value);
	}

	void MoneyOptSelected(int value)
	{
		baseScript.SetMoneyNPCOpt(value);
	}

	void StarterTechOptSelected(int value)
	{
		baseScript.SetStarterTechOpt(value);
	}

	void StarterLevelOptSelected(int value)
	{
		baseScript.SetStarterLevelOpt(value);
	}

	void StarterStatsOptSelected(int value)
	{
		baseScript.SetStarterStatsOpt(value);
	}

	void TournamentsNPCOptSelected(int value)
	{
		baseScript.SetTournamentNPCOpt(value);
	}

	void RecruitmentsOptSelected(int value)
	{
		baseScript.SetRecruitsOpt(value);
	}

	void SetUpButtonsTranslations()
	{
		DigimonNPC.TooltipText = Tr("DigimonNPCL_info");
		StarterDigimon.Text = Tr("StarterL_T");
		StarterDigimon.TooltipText = Tr("StarterL_info");
		OtherPatches.Text = Tr("OtherPatchesL_T");
		OtherPatches.TooltipText = Tr("OtherPatchesL_info");
		Difficulty.Text = Tr("DigimonDif_T");
		Difficulty.TooltipText = Tr("DigimonDif_info");
		Digimon.TooltipText = Tr("DigimonNPC_info");
		NPCStats.Text = Tr("DigimonStats_T");
		NPCStats.TooltipText = Tr("DigimonStats_info");
		NPCTechs.Text = Tr("DigimonTechs_T");
		NPCTechs.TooltipText = Tr("DigimonTechs_info");
		NPCMoney.Text = Tr("DigimonMoney_T");
		NPCMoney.TooltipText = Tr("DigimonMoney_info");
		Bosses.Text = Tr("Bosses_T");
		Bosses.TooltipText = Tr("Bosses_info");
		Starter.Text = Tr("DigimonStarter_T");
		Starter.TooltipText = Tr("DigimonStarter_info");
		StarterTech.Text = Tr("StarterTech_T");
		StarterTech.TooltipText = Tr("StarterTech_info");
		StarterLevel.Text = Tr("StarterLevel_T");
		StarterLevel.TooltipText = Tr("StarterLevel_info");
		StarterStats.Text = Tr("StarterStats_T");
		StarterStats.TooltipText = Tr("StarterStats_info");
		TournamentsNPC.Text = Tr("TournamentsNPC_T");
		TournamentsNPC.TooltipText = Tr("TournamentsNPC_info");
		Recruitments.Text = Tr("Recruits_T");
		Recruitments.TooltipText = Tr("Recruits_info");
	}

	void SetUpOptionsTranslations()
	{
		DifficultyOpt.SetItemText(0, Tr("Hardcore_L"));
		DifficultyOpt.SetItemText(1, Tr("THardcore_L"));

		DigimonOpt.SetItemText(0, Tr("Shuffle_T"));
		DigimonOpt.SetItemText(1, Tr("Random_T"));
		DigimonOpt.SetItemText(2, Tr("Chaos_T"));

		NPCStatsOpt.SetItemText(0, Tr("Shuffle_T"));
		NPCStatsOpt.SetItemText(1, Tr("Random_T"));
		NPCStatsOpt.SetItemText(2, Tr("Chaos_T"));

		NPCMoneyOpt.SetItemText(0, Tr("Shuffle_T"));
		NPCMoneyOpt.SetItemText(1, Tr("Random_T"));

		StarterTechOpt.SetItemText(0, Tr("Weak_T"));
		StarterTechOpt.SetItemText(1, Tr("Random_T"));

		StarterLevelOpt.SetItemText(5, Tr("RandomLevel_T"));

		StarterStatsOpt.SetItemText(0, Tr("Random_T"));
		StarterStatsOpt.SetItemText(1, Tr("Chaos_T"));

		TournamentsNPCOpt.SetItemText(0, Tr("Shuffle_T"));
		TournamentsNPCOpt.SetItemText(1, Tr("Random_T"));

		RecruitmentsOpt.SetItemText(0, Tr("Shuffle_T"));
		RecruitmentsOpt.SetItemText(1, Tr("Chaos_T"));
	}

	void SetUpButtons()
	{
		Difficulty.Toggled += DifficultyToggled;
		Digimon.Toggled += DigimonToggled;
		NPCStats.Toggled += NPCStatsToggled;
		NPCTechs.Toggled += NPCTechsToggled;
		NPCMoney.Toggled += NPCMoneyToggled;
		Bosses.Toggled += BossesToggled;
		Starter.Toggled += StarterToggled;
		StarterTech.Toggled += StarterTechToggled;
		StarterLevel.Toggled += StarterLevelToggled;
		StarterStats.Toggled += StarterStatsToggled;
		TournamentsNPC.Toggled += TournamentsNPCToggled;
		Recruitments.Toggled += RecruitmentsToggled;
	}

	public void LoadSaveData(bool DifficultyS, bool DigimonS, bool NPCStatsS, bool NPCTechsS, bool NPCMoneyS, bool BossesS, bool StarterS, bool StarterTechS,
	bool StarterLevelS, bool StarterStatsS, bool TournamentsNPCS, bool RecruitmentsS, int DifficultyValue, int DigimonValue, int NPCStatsValue, int NPCMoneyValue,
	int StarterTechValue, int StarterLevelValue, int StarterStatsValue, int TournamentsNPCValue, int RecruitmentsValue)
	{
		DifficultyOpt.Selected = DifficultyValue;
		DigimonOpt.Selected = DigimonValue;
		NPCStatsOpt.Selected = NPCStatsValue;
		NPCMoneyOpt.Selected = NPCMoneyValue;
		StarterTechOpt.Selected = StarterTechValue;
		StarterLevelOpt.Selected = StarterLevelValue;
		StarterStatsOpt.Selected = StarterStatsValue;
		TournamentsNPCOpt.Selected = TournamentsNPCValue;
		RecruitmentsOpt.Selected = RecruitmentsValue;
		Difficulty.ButtonPressed = DifficultyS;
		Digimon.ButtonPressed = DigimonS;
		NPCStats.ButtonPressed = NPCStatsS;
		NPCTechs.ButtonPressed = NPCTechsS;
		NPCMoney.ButtonPressed = NPCMoneyS;
		Bosses.ButtonPressed = BossesS;
		Starter.ButtonPressed = StarterS;
		StarterTech.ButtonPressed = StarterTechS;
		StarterLevel.ButtonPressed = StarterLevelS;
		StarterStats.ButtonPressed = StarterStatsS;
		TournamentsNPC.ButtonPressed = TournamentsNPCS;
		Recruitments.ButtonPressed = RecruitmentsS;

	}
}
