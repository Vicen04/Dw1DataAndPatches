using Godot;
using System;

public partial class TechContainerRando : PanelContainer
{

	[Export]
	private Label Techs;

	[Export]
	private Label Boost;

	[Export]
	private Label Extra;

	[Export]
	private CheckBox Damage;

	[Export]
	private CheckBox MP;

	[Export]
	private CheckBox TypeDamage;

	[Export]
	private CheckBox Accuracy;

	[Export]
	private CheckBox Status;

	[Export]
	private CheckBox StatusChance;

	[Export]
	private CheckBox Finishers;

	[Export]
	private CheckBox BoostTech;

	[Export]
	private CheckBox BoostPower;

	[Export]
	private CheckBox LearnBattle;

	[Export]
	private CheckBox LearnBrains;

	[Export]
	private CheckBox GivenTechs;

	[Export]
	private OptionButton DamageOpt;

	[Export]
	private OptionButton MPOpt;

	[Export]
	private OptionButton TypeDamageOpt;

	[Export]
	private OptionButton AccuracyOpt;

	[Export]
	private OptionButton StatusOpt;

	[Export]
	private OptionButton StatusChanceOpt;

	[Export]
	private OptionButton FinishersOpt;

	[Export]
	private OptionButton BoostPowerOpt;

	[Export]
	private OptionButton LearnBattleOpt;

	[Export]
	private OptionButton LearnBrainsOpt;

	[Export]
	private RandomizerContainer baseScript;

	[Export]
	private CheckBox BoostStatus;

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

	void DamageToggled(bool toggled)
	{
		DamageOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetdamageTech(toggled, DamageOpt.Selected);
		else
			baseScript.SetdamageTech(toggled, -1);
	}

	void MPToggled(bool toggled)
	{
		MPOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetMPTech(toggled, MPOpt.Selected);
		else
			baseScript.SetMPTech(toggled, -1);
	}

	void TypeDamageToggled(bool toggled)
	{
		TypeDamageOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetDamageType(toggled, TypeDamageOpt.Selected);
		else
			baseScript.SetDamageType(toggled, -1);
	}

	void AccuracyToggled(bool toggled)
	{
		AccuracyOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetAccuracy(toggled, AccuracyOpt.Selected);
		else
			baseScript.SetAccuracy(toggled, -1);
	}

	void StatusToggled(bool toggled)
	{
		StatusOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetStatus(toggled, StatusOpt.Selected);
		else
			baseScript.SetStatus(toggled, -1);
	}

	void StatusChanceToggled(bool toggled)
	{
		StatusChanceOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetStatusChance(toggled, StatusChanceOpt.Selected);
		else
			baseScript.SetStatusChance(toggled, -1);
	}

	void FinishersToggled(bool toggled)
	{
		FinishersOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetFinishers(toggled, FinishersOpt.Selected);
		else
			baseScript.SetFinishers(toggled, -1);
	}

	void BoostTechToggled(bool toggled)
	{
		baseScript.SetBoostedTech(toggled);
	}

	void BoostPowerToggled(bool toggled)
	{
		BoostPowerOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetBoostedTechValue(toggled, BoostPowerOpt.Selected);
		else
			baseScript.SetBoostedTechValue(toggled, -1);
	}

	void LearnBattleToggled(bool toggled)
	{
		LearnBattleOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetLearnBattle(toggled, LearnBattleOpt.Selected);
		else
			baseScript.SetLearnBattle(toggled, -1);
	}

	void LearnBrainsToggled(bool toggled)
	{
		LearnBrainsOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetLearnBrains(toggled, LearnBrainsOpt.Selected);
		else
			baseScript.SetLearnBrains(toggled, -1);
	}

	void GivenTechsToggled(bool toggled)
	{
		baseScript.SetGivenTechs(toggled);
	}

	void DamageOptSelected(int value)
	{
		baseScript.SetdamageTechOpt(value);
	}

	void MPOptSelected(int value)
	{
		baseScript.SetMPTechOpt(value);
	}

	void TypeDamageOptSelected(int value)
	{
		baseScript.SetDamageTypeOpt(value);
	}

	void AccuracyOptSelected(int value)
	{
		baseScript.SetAccuracyOpt(value);
	}

	void StatusOptSelected(int value)
	{
		baseScript.SetStatusOpt(value);
	}

	void StatusChanceOptSelected(int value)
	{
		baseScript.SetStatusChanceOpt(value);
	}

	void FinishersOptSelected(int value)
	{
		baseScript.SetFinishersOpt(value);
	}

	void BoostPowerOptSelected(int value)
	{
		baseScript.SetBoostedTechValueOpt(value);
	}

	void LearnBattleOptSelected(int value)
	{
		baseScript.SetLearnBattleOpt(value);
	}

	void LearnBrainsOptSelected(int value)
	{
		baseScript.SetLearnBrainsOpt(value);
	}

	void BoostStatusToggled(bool toggled){ baseScript.SetStatusBoost(toggled); }


	void SetUpButtonsTranslations()
	{
		Techs.Text = Tr("TechsR_L");
		Techs.TooltipText = Tr("TechsR_info");
		Boost.Text = Tr("TechBoostR_L");
		Boost.TooltipText = Tr("TechBoostR_info");
		Extra.Text = Tr("TechsExtraR_L");
		Extra.TooltipText = Tr("TechsExtraR_info");
		Damage.Text = Tr("TechDamageR_L");
		Damage.TooltipText = Tr("TechDamageR_info");
		MP.TooltipText = Tr("TechMP_info");
		TypeDamage.Text = Tr("TypeDamageR_L");
		TypeDamage.TooltipText = Tr("TypeDamageR_info");
		Accuracy.Text = Tr("AccuracyR_L");
		Accuracy.TooltipText = Tr("AccuracyR_info");
		Status.Text = Tr("TechStatus_L");
		Status.TooltipText = Tr("TechStatus_info");
		StatusChance.Text = Tr("StatusChance_L");
		StatusChance.TooltipText = Tr("StatusChance_info");
		Finishers.Text = Tr("FinishersR_L");
		Finishers.TooltipText = Tr("FinishersR_info");
		BoostTech.Text = Tr("BoostTechR_L");
		BoostTech.TooltipText = Tr("BoostTechR_info");
		BoostPower.Text = Tr("BoostPowerR_L");
		BoostPower.TooltipText = Tr("BoostPowerR_info");
		LearnBattle.Text = Tr("LearnBattleR_L");
		LearnBattle.TooltipText = Tr("LearnBattleR_info");
		LearnBrains.Text = Tr("LearnBrainsR_L");
		LearnBrains.TooltipText = Tr("LearnBrainsR_info");
		GivenTechs.Text = Tr("GivenTechs_L");
		GivenTechs.TooltipText = Tr("GivenTechs_info");
		BoostStatus.Text = Tr("BoostStatusR_L");
		BoostStatus.TooltipText = Tr("BoostStatusR_info");
	}

	void SetUpOptionsTranslations()
	{
		DamageOpt.SetItemText(0, Tr("Shuffle_T"));
		DamageOpt.SetItemText(1, Tr("Random_T"));
		DamageOpt.SetItemText(2, Tr("Chaos_T"));

		MPOpt.SetItemText(0, Tr("Shuffle_T"));
		MPOpt.SetItemText(1, Tr("Random_T"));
		MPOpt.SetItemText(2, Tr("Chaos_T"));

		TypeDamageOpt.SetItemText(0, Tr("Shuffle_T"));
		TypeDamageOpt.SetItemText(1, Tr("Random_T"));
		TypeDamageOpt.SetItemText(2, Tr("Chaos_T"));

		AccuracyOpt.SetItemText(0, Tr("Shuffle_T"));
		AccuracyOpt.SetItemText(1, Tr("Random_T"));
		AccuracyOpt.SetItemText(2, Tr("Chaos_T"));

		StatusChanceOpt.SetItemText(0, Tr("Shuffle_T"));
		StatusChanceOpt.SetItemText(1, Tr("Random_T"));
		StatusChanceOpt.SetItemText(2, Tr("Chaos_T"));

		StatusOpt.SetItemText(0, Tr("Shuffle_T"));
		StatusOpt.SetItemText(1, Tr("Random_T"));
		StatusOpt.SetItemText(2, Tr("Chaos_T"));

		FinishersOpt.SetItemText(0, Tr("Mixable_T"));
		FinishersOpt.SetItemText(1, Tr("Unique_T"));

		BoostPowerOpt.SetItemText(0, Tr("Shuffle_T"));
		BoostPowerOpt.SetItemText(1, Tr("Random_T"));
		BoostPowerOpt.SetItemText(2, Tr("Chaos_T"));

		LearnBattleOpt.SetItemText(0, Tr("Shuffle_T"));
		LearnBattleOpt.SetItemText(1, Tr("Random_T"));
		LearnBattleOpt.SetItemText(2, Tr("Chaos_T"));

		LearnBrainsOpt.SetItemText(0, Tr("Shuffle_T"));
		LearnBrainsOpt.SetItemText(1, Tr("Random_T"));
		LearnBrainsOpt.SetItemText(2, Tr("Chaos_T"));
	}

	void SetUpButtons()
	{
		Damage.Toggled += DamageToggled;
		MP.Toggled += MPToggled;
		TypeDamage.Toggled += TypeDamageToggled;
		Accuracy.Toggled += AccuracyToggled;
		Status.Toggled += StatusToggled;
		StatusChance.Toggled += StatusChanceToggled;
		Finishers.Toggled += FinishersToggled;
		BoostTech.Toggled += BoostTechToggled;
		BoostPower.Toggled += BoostPowerToggled;
		LearnBattle.Toggled += LearnBattleToggled;
		LearnBrains.Toggled += LearnBrainsToggled;
		GivenTechs.Toggled += GivenTechsToggled;
		BoostStatus.Toggled += BoostStatusToggled;
	}

	public void LoadSaveData(bool DamageS, bool MPS, bool TypeDamageS, bool AccuracyS, bool StatusS, bool StatusChanceS, bool FinishersS, bool BoostTechS,
	bool BoostPowerS, bool LearnBattleS, bool LearnBrainsS, bool GivenTechsS, int DamageValue, int MPValue, int TypeValue, int AccuracyValue, int StatusValue, int statusChanceValue,
	int finisherValue, int boostPowerValue, int battleValue, int brainsValue, bool statusBoost)
	{
		DamageOpt.Selected = DamageValue;
		MPOpt.Selected = MPValue;
		TypeDamageOpt.Selected = TypeValue;
		AccuracyOpt.Selected = AccuracyValue;
		StatusOpt.Selected = StatusValue;
		StatusChanceOpt.Selected = statusChanceValue;
		FinishersOpt.Selected = finisherValue;
		BoostPowerOpt.Selected = boostPowerValue;
		LearnBattleOpt.Selected = battleValue;
		LearnBrainsOpt.Selected = brainsValue;
		Damage.ButtonPressed = DamageS;
		MP.ButtonPressed = MPS;
		TypeDamage.ButtonPressed = TypeDamageS;
		Accuracy.ButtonPressed = AccuracyS;
		Status.ButtonPressed = StatusS;
		StatusChance.ButtonPressed = StatusChanceS;
		Finishers.ButtonPressed = FinishersS;
		BoostTech.ButtonPressed = BoostTechS;
		BoostPower.ButtonPressed = BoostPowerS;
		LearnBattle.ButtonPressed = LearnBattleS;
		LearnBrains.ButtonPressed = LearnBrainsS;
		GivenTechs.ButtonPressed = GivenTechsS;
		BoostStatus.ButtonPressed = statusBoost;
	}
}
