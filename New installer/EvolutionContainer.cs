using Godot;
using System;

public partial class EvolutionContainer : PanelContainer
{

	[Export]
	private Label Normal;

	[Export]
	private Label Special;

	[Export]
	private CheckBox Path;

	[Export]
	private CheckBox Time;

	[Export]
	private CheckBox StatGains;

	[Export]
	private CheckBox Requirements;

	[Export]
	private CheckBox Items;

	[Export]
	private CheckBox SpeEvolution;

	[Export]
	private CheckBox SpeChance;

	[Export]
	private CheckBox SpeRequirements;

	[Export]
	private CheckBox Factorial;

	[Export]
	private CheckBox Sukamon;

	[Export]
	private OptionButton PathOpt;

	[Export]
	private OptionButton TimeOpt;

	[Export]
	private OptionButton StatGainsOpt;

	[Export]
	private OptionButton RequirementsOpt;

	[Export]
	private OptionButton ItemsOpt;

	[Export]
	private OptionButton SpeEvolutionOpt;

	[Export]
	private OptionButton SpeChanceOpt;

	[Export]
	private OptionButton SpeRequirementsOpt;

	[Export]
	private OptionButton FactorialOpt;

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

	void PathToggled(bool toggled)
	{
		PathOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetEvoTree(toggled, PathOpt.Selected);
		else
			baseScript.SetEvoTree(toggled, -1);
	}

	void TimeToggled(bool toggled)
	{
		TimeOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetEvoTime(toggled, TimeOpt.Selected);
		else
			baseScript.SetEvoTime(toggled, -1);
	}

	void StatGainsToggled(bool toggled)
	{
		StatGainsOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetStatGains(toggled, StatGainsOpt.Selected);
		else
			baseScript.SetStatGains(toggled, -1);
	}

	void RequirementsToggled(bool toggled)
	{
		RequirementsOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetRequirementsEvo(toggled, RequirementsOpt.Selected);
		else
			baseScript.SetRequirementsEvo(toggled, -1);
	}

	void ItemsToggled(bool toggled)
	{
		ItemsOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetEvoItems(toggled, ItemsOpt.Selected);
		else
			baseScript.SetEvoItems(toggled, -1);
	}

	void SpeEvolutionToggled(bool toggled)
	{
		SpeEvolutionOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetSpecialEvo(toggled, SpeEvolutionOpt.Selected);
		else
			baseScript.SetSpecialEvo(toggled, -1);
	}

	void SpeChanceToggled(bool toggled)
	{
		SpeChanceOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetSpecialChance(toggled, SpeChanceOpt.Selected);
		else
			baseScript.SetSpecialChance(toggled, -1);
	}

	void SpeRequirementsToggled(bool toggled)
	{
		SpeRequirementsOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetSpecialEvoReq(toggled, SpeRequirementsOpt.Selected);
		else
			baseScript.SetSpecialEvoReq(toggled, -1);
	}

	void FactorialToggled(bool toggled)
	{
		FactorialOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetFactorial(toggled, FactorialOpt.Selected);
		else
			baseScript.SetFactorial(toggled, -1);
	}

	void SukamonToggled(bool toggled)
	{
		baseScript.SetSukamon(toggled);
	}

	void PathOptSelected(int value)
	{
		baseScript.SetEvoTreeOpt(value);
	}

	void TimeOptSelected(int value)
	{
		baseScript.SetEvoTimeOpt(value);
	}

	void StatGainsOptSelected(int value)
	{
		baseScript.SetStatGainsOpt(value);
	}

	void RequirementsOptOptSelected(int value)
	{
		baseScript.SetRequirementsEvoOpt(value);
	}

	void ItemsOptSelected(int value)
	{
		baseScript.SetEvoItemsOpt(value);
	}

	void SpeEvolutionOptSelected(int value)
	{
		baseScript.SetSpecialEvoOpt(value);
	}

	void SpeChanceOptSelected(int value)
	{
		baseScript.SetSpecialChanceOpt(value);
	}

	void SpeRequirementsOptSelected(int value)
	{
		baseScript.SetSpecialEvoReqOpt(value);
	}

	void FactorialOptSelected(int value)
	{
		baseScript.SetFactorialOpt(value);
	}

	void SetUpButtonsTranslations()
	{
		Normal.Text = Tr("EvoNormal_L");
		Normal.TooltipText = Tr("EvoNormal_info");
		Special.Text = Tr("EvoSpecial_L");
		Special.TooltipText = Tr("EvoSpecial_info");
		Path.Text = Tr("EvoPath_L");
		Path.TooltipText = Tr("EvoPath_info");
		Time.Text = Tr("EvoTime_L");
		Time.TooltipText = Tr("EvoTime_info");
		StatGains.Text = Tr("EvoStatGains_L");
		StatGains.TooltipText = Tr("EvoStatGains_info");
		Requirements.Text = Tr("EvoRequirements_L");
		Requirements.TooltipText = Tr("EvoRequirements_info");
		Items.Text = Tr("EvoItems_L");
		Items.TooltipText = Tr("EvoItems_info");
		SpeEvolution.Text = Tr("SpeEvolution_L");
		SpeEvolution.TooltipText = Tr("SpeEvolution_info");
		SpeChance.Text = Tr("SpeChance_L");
		SpeChance.TooltipText = Tr("SpeChance_info");
		SpeRequirements.Text = Tr("SpeRequirements_L");
		SpeRequirements.TooltipText = Tr("SpeRequirements_info");
		Factorial.Text = Tr("FactorialR_L");
		Factorial.TooltipText = Tr("FactorialR_info");
		Sukamon.Text = Tr("SukamonR_L");
		Sukamon.TooltipText = Tr("SukamonR_info");
	}

	void SetUpOptionsTranslations()
	{
		PathOpt.SetItemText(0, Tr("Shuffle_T"));
		PathOpt.SetItemText(1, Tr("Random_T"));
		PathOpt.SetItemText(2, Tr("Chaos_T"));

		TimeOpt.SetItemText(0, Tr("Shuffle_T"));
		TimeOpt.SetItemText(1, Tr("Random_T"));
		TimeOpt.SetItemText(2, Tr("Chaos_T"));

		StatGainsOpt.SetItemText(0, Tr("Shuffle_T"));
		StatGainsOpt.SetItemText(1, Tr("Random_T"));
		StatGainsOpt.SetItemText(2, Tr("Chaos_T"));

		RequirementsOpt.SetItemText(0, Tr("Shuffle_T"));
		RequirementsOpt.SetItemText(1, Tr("Random_T"));
		RequirementsOpt.SetItemText(2, Tr("Chaos_T"));

		ItemsOpt.SetItemText(0, Tr("Shuffle_T"));
		ItemsOpt.SetItemText(1, Tr("Random_T"));
		ItemsOpt.SetItemText(2, Tr("Chaos_T"));

		SpeEvolutionOpt.SetItemText(0, Tr("Shuffle_T"));
		SpeEvolutionOpt.SetItemText(1, Tr("Random_T"));

		SpeChanceOpt.SetItemText(0, Tr("Shuffle_T"));
		SpeChanceOpt.SetItemText(1, Tr("Random_T"));

		SpeRequirementsOpt.SetItemText(0, Tr("Shuffle_T"));
		SpeRequirementsOpt.SetItemText(1, Tr("Random_T"));

		FactorialOpt.SetItemText(0, Tr("Upgradable_T"));
		FactorialOpt.SetItemText(1, Tr("EvolutionF_T"));
		FactorialOpt.SetItemText(2, Tr("RandomAll_T"));
	}

	void SetUpButtons()
	{
		Path.Toggled += PathToggled;
		Time.Toggled += TimeToggled;
		StatGains.Toggled += StatGainsToggled;
		Requirements.Toggled += RequirementsToggled;
		Items.Toggled += ItemsToggled;
		SpeEvolution.Toggled += SpeEvolutionToggled;
		SpeChance.Toggled += SpeChanceToggled;
		SpeRequirements.Toggled += SpeRequirementsToggled;
		Factorial.Toggled += FactorialToggled;
		Sukamon.Toggled += SukamonToggled;
	}

	public void LoadSaveData(bool PathS, bool TimeS, bool StatGainsS, bool RequirementsS, bool ItemsS, bool SpeEvolutionS, bool SpeChanceS, bool SpeRequirementsS,
	bool FactorialS, bool SukamonS, int PathValue, int TimeValue, int StatGainsValue, int RequirementsValue, int ItemsValue, int SpeEvolutionValue,
	int SpeChanceValue, int SpeRequirementsValue, int FactorialValue)
	{
		PathOpt.Selected = PathValue;
		TimeOpt.Selected = TimeValue;
		StatGainsOpt.Selected = StatGainsValue;
		RequirementsOpt.Selected = RequirementsValue;
		ItemsOpt.Selected = ItemsValue;
		SpeEvolutionOpt.Selected = SpeEvolutionValue;
		SpeChanceOpt.Selected = SpeChanceValue;
		SpeRequirementsOpt.Selected = SpeRequirementsValue;
		FactorialOpt.Selected = FactorialValue;
		Path.ButtonPressed = PathS;
		Time.ButtonPressed = TimeS;
		StatGains.ButtonPressed = StatGainsS;
		Requirements.ButtonPressed = RequirementsS;
		Items.ButtonPressed = ItemsS;
		SpeEvolution.ButtonPressed = SpeEvolutionS;
		SpeChance.ButtonPressed = SpeChanceS;
		SpeRequirements.ButtonPressed = SpeRequirementsS;
		Factorial.ButtonPressed = FactorialS;
		Sukamon.ButtonPressed = SukamonS;
	}
}
