using Godot;
using System;

public partial class ItemsContainer : PanelContainer
{

	[Export]
	private Label ItemSpawn;

	[Export]
	private Label ItemDrop;

	[Export]
	private Label Chests;

	[Export]
	private Label Shops;

	[Export]
	private Label Aditional;

	[Export]
	private CheckBox ItemSpawned;

	[Export]
	private CheckBox ItemSpawnRate;

	[Export]
	private CheckBox ItemDropped;

	[Export]
	private CheckBox ItemDropRate;

	[Export]
	private CheckBox ChestItems;

	[Export]
	private CheckBox ShopItems;

	[Export]
	private CheckBox ShopPrices;

	[Export]
	private CheckBox Mojyamon;

	[Export]
	private CheckBox Merit;

	[Export]
	private CheckBox MeritPrices;

	[Export]
	private CheckBox Tournaments;

	[Export]
	private CheckBox Tokomon;

	[Export]
	private CheckBox KeyItems;

	[Export]
	private CheckBox CurlingRewards;

	[Export]
	private OptionButton ItemSpawnedOpt;

	[Export]
	private OptionButton ItemSpawnRateOpt;

	[Export]
	private OptionButton ItemDroppedOpt;

	[Export]
	private OptionButton ItemDropRateOpt;

	[Export]
	private OptionButton ChestItemsOpt;

	[Export]
	private OptionButton ShopItemsOpt;

	[Export]
	private OptionButton ShopPricesOpt;

	[Export]
	private OptionButton MojyamonOpt;

	[Export]
	private OptionButton MeritOpt;

	[Export]
	private OptionButton MeritPricesOpt;

	[Export]
	private OptionButton TournamentsOpt;

	[Export]
	private OptionButton TokomonOpt;

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

	void ItemSpawnToggled(bool toggled)
	{
		ItemSpawnedOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetItemSpawns(toggled, ItemSpawnedOpt.Selected);
		else
			baseScript.SetItemSpawns(toggled, -1);
	}

	void ItemSpawnChanceToggled(bool toggled)
	{
		ItemSpawnRateOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetItemSpawnRate(toggled, ItemSpawnRateOpt.Selected);

		else
			baseScript.SetItemSpawnRate(toggled, -1);
	}

	void ItemDropToggled(bool toggled)
	{
		ItemDroppedOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetItemDrops(toggled, ItemDroppedOpt.Selected);
		else
			baseScript.SetItemDrops(toggled, -1);


	}

	void ItemDropRateToggled(bool toggled)
	{
		ItemDropRateOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetDropRate(toggled, ItemDropRateOpt.Selected);
		else
			baseScript.SetDropRate(toggled, -1);
	}

	void ChestsToggled(bool toggled)
	{
		ChestItemsOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetChests(toggled, ChestItemsOpt.Selected);
		else
			baseScript.SetChests(toggled, -1);

	}

	void ShopsToggled(bool toggled)
	{
		ShopItemsOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetShops(toggled, ShopItemsOpt.Selected);
		else
			baseScript.SetShops(toggled, -1);

	}

	void ShopPricesToggled(bool toggled)
	{
		ShopPricesOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetShopsPrices(toggled, ShopPricesOpt.Selected);
		else
			baseScript.SetShopsPrices(toggled, -1);

	}

	void MojyamonToggled(bool toggled)
	{
		MojyamonOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetMojyamon(toggled, MojyamonOpt.Selected);
		else
			baseScript.SetMojyamon(toggled, -1);
	}

	void MeritToggled(bool toggled)
	{
		MeritOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetMeritItems(toggled, MeritOpt.Selected);
		else
			baseScript.SetMeritItems(toggled, -1);

	}

	void MeritPricesToggled(bool toggled)
	{
		MeritPricesOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetMeritPrices(toggled, MeritPricesOpt.Selected);
		else
			baseScript.SetMeritPrices(toggled, -1);

	}

	void TournamentsToggled(bool toggled)
	{
		TournamentsOpt.Disabled = !toggled;
		if (toggled)
			baseScript.SetTournamentItems(toggled, TournamentsOpt.Selected);
		else
			baseScript.SetTournamentItems(toggled, -1);

	}

	void TokomonToggled(bool toggled)
	{
		if (!baseScript.hasEasyStart())
			TokomonOpt.Disabled = !toggled;
		else
			TokomonOpt.Selected = 1;

		if (toggled)
			baseScript.SetTokomon(toggled, TokomonOpt.Selected);
		else
			baseScript.SetTokomon(toggled, -1);

	}

	void KeyItemsToggled(bool toggled)
	{
		baseScript.SetKeyItems(toggled);
	}

	void CurlingRewardsToggled(bool toggled)
	{
		baseScript.SetCurlingRewards(toggled);
	}

	void ItemSpawnValue(int value)
	{
		baseScript.SetItemSpawnsOpt(value);
	}

	void ItemSpawnRateValue(int value)
	{
		baseScript.SetItemSpawnsRateOpt(value);
	}

	void ItemDropValue(int value)
	{
		baseScript.SetItemDropsOpt(value);
	}

	void ItemDropRateValue(int value)
	{
		baseScript.SetDropRateOpt(value);
	}

	void ChestsValue(int value)
	{
		baseScript.SetChestsOpt(value);
	}

	void ShopItemsValue(int value)
	{
		baseScript.SetShopsOpt(value);
	}

	void ShopPricesValue(int value)
	{
		baseScript.SetShopsPricesOpt(value);
	}

	void MeritValue(int value)
	{
		baseScript.SetMeritItemsOpt(value);
	}

	void MeritPricesValue(int value)
	{
		baseScript.SetMeritPricesOpt(value);
	}

	void MojyamonValue(int value)
	{
		baseScript.SetMojyamonOpt(value);
	}

	void TournamentsValue(int value)
	{
		baseScript.SetTournamentNPCOpt(value);
	}

	void TokomonValue(int value)
	{
		baseScript.SetShopsPricesOpt(value);
	}

	void SetUpButtonsTranslations()
	{
		ItemSpawn.Text = Tr("Item_T");
		ItemSpawn.TooltipText = Tr("Item_info");
		ItemDrop.Text = Tr("Drop_T");
		ItemDrop.TooltipText = Tr("Drop_info");
		Chests.Text = Tr("Chest_T");
		Chests.TooltipText = Tr("Chest_info");
		Shops.Text = Tr("Shop_T");
		Shops.TooltipText = Tr("Shop_info");
		Aditional.Text = Tr("ItemAdditional_T");
		Aditional.TooltipText = Tr("ItemAdditional_info");
		ItemSpawned.Text = Tr("Items_T");
		ItemSpawned.TooltipText = Tr("ItemsSpawn_info");
		ItemSpawnRate.Text = Tr("ItemSpawnRate_T");
		ItemSpawnRate.TooltipText = Tr("ItemSpawnRate_info");
		ItemDropped.Text = Tr("Items_T");
		ItemDropped.TooltipText = Tr("ItemsDrops_info");
		ItemDropRate.Text = Tr("ItemDropRate_T");
		ItemDropRate.TooltipText = Tr("ItemDropRate_info");
		ChestItems.Text = Tr("Items_T");
		ChestItems.TooltipText = Tr("Chests_info");
		ShopItems.Text = Tr("ShopItems_T");
		ShopItems.TooltipText = Tr("ShopItems_info");
		ShopPrices.Text = Tr("ShopPrices_T");
		ShopPrices.TooltipText = Tr("ShopPrices_info");
		Mojyamon.Text = Tr("MojyamonR_T");
		Mojyamon.TooltipText = Tr("MojyamonR_info");
		Merit.Text = Tr("MeritR_T");
		Merit.TooltipText = Tr("MeritR_info");
		MeritPrices.Text = Tr("MeritPrices_T");
		MeritPrices.TooltipText = Tr("MeritPrices_info");
		Tournaments.Text = Tr("TournamentsDR_T");
		Tournaments.TooltipText = Tr("TournamentsDR_info");
		Tokomon.Text = Tr("Tokomon_T");
		Tokomon.TooltipText = Tr("Tokomon_info");
		KeyItems.Text = Tr("KeyItems_T");
		KeyItems.TooltipText = Tr("KeyItems_info");
		CurlingRewards.Text = Tr("CurlingRewards_T");
		CurlingRewards.TooltipText = Tr("CurlingRewards_info");
	}

	void SetUpOptionsTranslations()
	{
		ItemSpawnedOpt.SetItemText(0, Tr("Shuffle_T"));
		ItemSpawnedOpt.SetItemText(1, Tr("Random_T"));
		ItemSpawnedOpt.SetItemText(2, Tr("Chaos_T"));

		ItemSpawnRateOpt.SetItemText(0, Tr("Shuffle_T"));
		ItemSpawnRateOpt.SetItemText(1, Tr("Random_T"));

		ItemDroppedOpt.SetItemText(0, Tr("Shuffle_T"));
		ItemDroppedOpt.SetItemText(1, Tr("Random_T"));
		ItemDroppedOpt.SetItemText(2, Tr("Chaos_T"));

		ItemDropRateOpt.SetItemText(0, Tr("Shuffle_T"));
		ItemDropRateOpt.SetItemText(1, Tr("Random_T"));

		ChestItemsOpt.SetItemText(0, Tr("Shuffle_T"));
		ChestItemsOpt.SetItemText(1, Tr("Random_T"));
		ChestItemsOpt.SetItemText(2, Tr("Chaos_T"));

		ShopItemsOpt.SetItemText(0, Tr("Random_T"));
		ShopItemsOpt.SetItemText(1, Tr("Chaos_T"));

		ShopPricesOpt.SetItemText(0, Tr("Shuffle_T"));
		ShopPricesOpt.SetItemText(1, Tr("Random_T"));

		MojyamonOpt.SetItemText(0, Tr("Random_T"));
		MojyamonOpt.SetItemText(1, Tr("Chaos_T"));

		MeritOpt.SetItemText(0, Tr("Random_T"));
		MeritOpt.SetItemText(1, Tr("Chaos_T"));

		MeritPricesOpt.SetItemText(0, Tr("Shuffle_T"));
		MeritPricesOpt.SetItemText(1, Tr("Random_T"));

		TournamentsOpt.SetItemText(0, Tr("Shuffle_T"));
		TournamentsOpt.SetItemText(1, Tr("Random_T"));

		TokomonOpt.SetItemText(0, Tr("Random_T"));
		TokomonOpt.SetItemText(1, Tr("EasyStartR_T"));
	}

	void SetUpButtons()
	{
		ItemSpawned.Toggled += ItemSpawnToggled;
		ItemSpawnRate.Toggled += ItemSpawnChanceToggled;
		ItemDropped.Toggled += ItemDropToggled;
		ItemDropRate.Toggled += ItemDropRateToggled;
		ChestItems.Toggled += ChestsToggled;
		ShopItems.Toggled += ShopsToggled;
		ShopPrices.Toggled += ShopPricesToggled;
		Mojyamon.Toggled += MojyamonToggled;
		Merit.Toggled += MeritToggled;
		MeritPrices.Toggled += MeritPricesToggled;
		Tournaments.Toggled += TournamentsToggled;
		Tokomon.Toggled += TokomonToggled;
		KeyItems.Toggled += KeyItemsToggled;
		CurlingRewards.Toggled += CurlingRewardsToggled;
	}

	public void LoadData(bool ItemSpawnedS, bool ItemSpawnRateS, bool ItemDroppedS, bool ItemDropRateS, bool ChestItemsS, bool ShopItemsS, bool ShopPricesS, bool MojyamonS, bool MeritS,
	bool MeritPricesS, bool TournamentsS, bool TokomonS, bool KeyItemsS, bool CurlingRewardsS, int ItemSpawnedOptS, int ItemSpawnRateOptS, int ItemDroppedOptS, int ItemDropRateOptS,
	int ChestItemsOptS, int ShopItemsOptS, int ShopPricesOptS, int MojyamonOptS, int MeritOptS, int MeritPricesOptS, int TournamentsOptS, int TokomonOptS)
	{
		ItemSpawnedOpt.Selected = ItemSpawnedOptS;
		ItemSpawnRateOpt.Selected = ItemSpawnRateOptS;
		ItemDroppedOpt.Selected = ItemDroppedOptS;
		ItemDropRateOpt.Selected = ItemDropRateOptS;
		ChestItemsOpt.Selected = ChestItemsOptS;
		ShopItemsOpt.Selected = ShopItemsOptS;
		ShopPricesOpt.Selected = ShopPricesOptS;
		MojyamonOpt.Selected = MojyamonOptS;
		MeritOpt.Selected = MeritOptS;
		MeritPricesOpt.Selected = MeritPricesOptS;
		TournamentsOpt.Selected = TournamentsOptS;
		TokomonOpt.Selected = TokomonOptS;
		ItemSpawned.ButtonPressed = ItemSpawnedS;
		ItemSpawnRate.ButtonPressed = ItemSpawnRateS;
		ItemDropped.ButtonPressed = ItemDroppedS;
		ItemDropRate.ButtonPressed = ItemDropRateS;
		ChestItems.ButtonPressed = ChestItemsS;
		ShopItems.ButtonPressed = ShopItemsS;
		ShopPrices.ButtonPressed = ShopPricesS;
		Mojyamon.ButtonPressed = MojyamonS;
		Merit.ButtonPressed = MeritS;
		MeritPrices.ButtonPressed = MeritPricesS;
		Tournaments.ButtonPressed = TournamentsS;
		Tokomon.ButtonPressed = TokomonS;
		KeyItems.ButtonPressed = KeyItemsS;
		CurlingRewards.ButtonPressed = CurlingRewardsS;
	}

}
