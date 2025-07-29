using Godot;
using System;

public partial class MiscContainerRando : PanelContainer
{
	[Export]
	private Label Title;

	[Export]
	private CheckBox Restaurant;

	[Export]
	private CheckBox Birdramon;

	[Export]
	private CheckBox Boost;

	[Export]
	private CheckBox Healing;

	[Export]
	private CheckBox Devil;

	[Export]
	private CheckBox Chips;

	[Export]
	private CheckBox Seadramon;

	[Export]
	private CheckBox Fish;

	[Export]
	private CheckBox TournamentSchedule;

	[Export]
	private CheckBox Food;

	[Export]
	private CheckBox RareSpawns;

	[Export]
	private CheckBox ChaosItems;

	[Export]
	private OptionButton RestaurantOpt;

	[Export]
	private OptionButton BirdramonOpt;

	[Export]
	private OptionButton BoostOpt;

	[Export]
	private OptionButton HealingOpt;

	[Export]
	private OptionButton DevilOpt;

	[Export]
	private OptionButton ChipsOpt;

	[Export]
	private OptionButton FishOpt;

	[Export]
	private OptionButton TournamentScheduleOpt;

	[Export]
	private OptionButton FoodOpt;

	[Export]
	private OptionButton RareSpawnsOpt;

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

	void RestaurantToggled(bool toggled)
	{
		RestaurantOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetRestaurant(toggled, RestaurantOpt.Selected);
		else
			baseScript.SetRestaurant(toggled, -1);
	}

	void BirdramonToggled(bool toggled)
	{
		BirdramonOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetBirdramon(toggled, BirdramonOpt.Selected);
		else
			baseScript.SetBirdramon(toggled, -1);
	}

	void BoostToggled(bool toggled)
	{
		BoostOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetItemBoost(toggled, BoostOpt.Selected);
		else
			baseScript.SetItemBoost(toggled, -1);
	}

	void HealingToggled(bool toggled)
	{
		HealingOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetHealing(toggled, HealingOpt.Selected);
		else
			baseScript.SetHealing(toggled, -1);
	}

	void DevilToggled(bool toggled)
	{
		DevilOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetDevilChips(toggled, DevilOpt.Selected);
		else
			baseScript.SetDevilChips(toggled, -1);
	}

	void ChipsToggled(bool toggled)
	{
		ChipsOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetChips(toggled, ChipsOpt.Selected);
		else
			baseScript.SetChips(toggled, -1);
	}

	void SeadramonToggled(bool toggled)
	{
		baseScript.SetSeadramon(toggled);
	}

	void FishToggled(bool toggled)
	{
		FishOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetFish(toggled, FishOpt.Selected);
		else
			baseScript.SetFish(toggled, -1);
	}

	void TournamentScheduleToggled(bool toggled)
	{
		TournamentScheduleOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetTournamentSchedule(toggled, TournamentScheduleOpt.Selected);
		else
			baseScript.SetTournamentSchedule(toggled, -1);
	}

	void FoodToggled(bool toggled)
	{
		FoodOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetFood(toggled, FoodOpt.Selected);
		else
			baseScript.SetFood(toggled, -1);
	}

	void RareSpawnsToggled(bool toggled)
	{
		RareSpawnsOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetRareSpawns(toggled, RestaurantOpt.Selected);
		else
			baseScript.SetRareSpawns(toggled, -1);
	}

	void ChaosItemsToggled(bool toggled)
	{
		baseScript.SetChaosItems(toggled);
		if (toggled)
		{
			Food.Disabled = true;
			Boost.ButtonPressed = false;
			Boost.Disabled = true;
			Food.ButtonPressed = false;
			Chips.Disabled = true;
			Chips.ButtonPressed = false;
			Healing.Disabled = true;
			Healing.ButtonPressed = false;
		}
		else
		{
			Food.Disabled = false;
			Boost.Disabled = false;
			Chips.Disabled = false;
			Healing.Disabled = false;
		}
	}

	void RestaurantOptSelected(int option)
	{
		baseScript.SetRestaurantOpt(option);
	}

	void BirdramonOptSelected(int option)
	{
		baseScript.SetBirdramonOpt(option);
	}

	void BoostOptSelected(int option)
	{
		baseScript.SetItemBoostOpt(option);
	}

	void HealingOptSelected(int option)
	{
		baseScript.SetHealingOpt(option);
	}

	void DevilOptSelected(int option)
	{
		baseScript.SetDevilChipsOpt(option);
	}

	void ChipsOptSelected(int option)
	{
		baseScript.SetChipsOpt(option);
	}

	void FishOptSelected(int option)
	{
		baseScript.SetFishOpt(option);
	}

	void TournamentScheduleOptSelected(int option)
	{
		baseScript.SetTournamentScheduleOpt(option);
	}

	void FoodOptSelected(int option)
	{
		baseScript.SetFoodOpt(option);
	}

	void RareSpawnsOptSelected(int option)
	{
		baseScript.SetRareSpawnsOpt(option);
	}

	void SetUpButtonsTranslations()
	{
		Title.Text = Tr("MiscellaneousR_L");
		Title.TooltipText = Tr("MiscellaneousR_info");
		Restaurant.Text = Tr("Restaurant_L");
		Restaurant.TooltipText = Tr("Restaurant_info");
		Birdramon.TooltipText = Tr("Birdramon_info");
		Boost.Text = Tr("BoostR_L");
		Boost.TooltipText = Tr("BoostR_info");
		Healing.Text = Tr("Healing_L");
		Healing.TooltipText = Tr("Healing_info");
		Devil.Text = Tr("Devil_L");
		Devil.TooltipText = Tr("Devil_info");
		Chips.Text = Tr("Chips_L");
		Chips.TooltipText = Tr("Chips_info");
		Seadramon.TooltipText = Tr("SeadramonR_info");
		Fish.Text = Tr("FishBait_L");
		Fish.TooltipText = Tr("FishBait_info");
		TournamentSchedule.Text = Tr("TournamentSchedule_L");
		TournamentSchedule.TooltipText = Tr("TournamentSchedule_info");
		Food.Text = Tr("FoodR_L");
		Food.TooltipText = Tr("FoodR_info");
		RareSpawns.Text = Tr("RareSpawnsR_L");
		RareSpawns.TooltipText = Tr("RareSpawnsR_info");
		ChaosItems.Text = Tr("ChaosItems_L");
		ChaosItems.TooltipText = Tr("ChaosItems_info");
	}

	void SetUpOptionsTranslations()
	{
		RestaurantOpt.SetItemText(0, Tr("Random_T"));
		RestaurantOpt.SetItemText(1, Tr("Chaos_T"));

		BirdramonOpt.SetItemText(0, Tr("Random_T"));
		BirdramonOpt.SetItemText(1, Tr("Chaos_T"));

		BoostOpt.SetItemText(0, Tr("Shuffle_T"));
		BoostOpt.SetItemText(1, Tr("Random_T"));
		BoostOpt.SetItemText(2, Tr("Chaos_T"));

		HealingOpt.SetItemText(0, Tr("Shuffle_T"));
		HealingOpt.SetItemText(1, Tr("Random_T"));
		HealingOpt.SetItemText(2, Tr("Chaos_T"));

		DevilOpt.SetItemText(0, Tr("Random_T"));
		DevilOpt.SetItemText(1, Tr("Chaos_T"));

		ChipsOpt.SetItemText(0, Tr("Random_T"));
		ChipsOpt.SetItemText(1, Tr("Chaos_T"));

		FishOpt.SetItemText(0, Tr("Shuffle_T"));
		FishOpt.SetItemText(1, Tr("Random_T"));

		TournamentScheduleOpt.SetItemText(0, Tr("Shuffle_T"));
		TournamentScheduleOpt.SetItemText(1, Tr("Random_T"));

		FoodOpt.SetItemText(0, Tr("Shuffle_T"));
		FoodOpt.SetItemText(1, Tr("Random_T"));
		FoodOpt.SetItemText(2, Tr("Chaos_T"));

		RareSpawnsOpt.SetItemText(0, Tr("Shared_T"));
		RareSpawnsOpt.SetItemText(1, Tr("Unique_T"));
	}

	void SetUpButtons()
	{
		Restaurant.Toggled += RestaurantToggled;
		Birdramon.Toggled += BirdramonToggled;
		Boost.Toggled += BoostToggled;
		Healing.Toggled += HealingToggled;
		Devil.Toggled += DevilToggled; //This is Monochromon
		Chips.Toggled += ChipsToggled;
		Seadramon.Toggled += SeadramonToggled;
		Fish.Toggled += FishToggled;
		TournamentSchedule.Toggled += TournamentScheduleToggled;
		Food.Toggled += FoodToggled;
		RareSpawns.Toggled += RareSpawnsToggled;
		ChaosItems.Toggled += ChaosItemsToggled;
	}

	public void LoadSaveData(bool RestaurantS, bool BirdramonS, bool BoostS, bool HealingS, bool DevilS, bool ChipsS, bool SeadramonS, bool FishS,
	bool TournamentScheduleS, bool FoodS, bool RareSpawnsS, bool ChaosItemsS, int RestaurantValue, int BirdramonValue, int BoostValue, int HealingValue, int DevilValue,
	int ChipsValue, int FishValue, int TournamentScheduleValue, int FoodValue, int RareSpawnsValue)
	{
		RestaurantOpt.Selected = RestaurantValue;
		BirdramonOpt.Selected = BirdramonValue;
		BoostOpt.Selected = BoostValue;
		HealingOpt.Selected = HealingValue;
		DevilOpt.Selected = DevilValue;
		ChipsOpt.Selected = ChipsValue;
		FishOpt.Selected = FishValue;
		TournamentScheduleOpt.Selected = TournamentScheduleValue;
		FoodOpt.Selected = FoodValue;
		RareSpawnsOpt.Selected = RareSpawnsValue;
		Restaurant.ButtonPressed = RestaurantS;
		Birdramon.ButtonPressed = BirdramonS;
		Boost.ButtonPressed = BoostS;
		Healing.ButtonPressed = HealingS;
		Devil.ButtonPressed = DevilS;
		Chips.ButtonPressed = ChipsS;
		Seadramon.ButtonPressed = SeadramonS;
		Fish.ButtonPressed = FishS;
		TournamentSchedule.ButtonPressed = TournamentScheduleS;
		Food.ButtonPressed = FoodS;
		RareSpawns.ButtonPressed = RareSpawnsS;
		ChaosItems.ButtonPressed = ChaosItemsS;
	}
}
