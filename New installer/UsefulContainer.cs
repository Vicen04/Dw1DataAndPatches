using Godot;
using System;

public partial class UsefulContainer : PanelContainer
{
	[Export] private Panel Panel1;
	[Export] private Panel Panel2;
	[Export] private Label Title;
	[Export] private Label Title2;
	[Export] private CheckBox StatGains;
	[Export] private CheckBox RareSpawns;
	[Export] private CheckBox ShortIntro;
	[Export] private CheckBox EasyStart;
	[Export] private CheckBox UsefulRigging;
	[Export] private CheckBox UltraLucky;
	[Export] private CheckBox TrainingBoost;
	[Export] private CheckBox Dirt;
	[Export] private CheckBox SuperDirt;
	[Export] private CheckBox Treasure;
	[Export] private CheckBox LowMono;
	[Export] private CheckBox Seadramon;
	[Export] private CheckBox EvoItem;
	[Export] private CheckBox MoreItemDrops;
	[Export] private CheckBox BetterItemDrops;
	[Export] private CheckBox Raise;
	[Export] private CheckBox MoreItemSpawns;
	[Export] private CheckBox BetterItemSpawns;
	[Export] private CheckBox Easymedals;
	[Export] private CheckBox VendingMachines;
	[Export] private CheckBox BetterCurling;
	[Export] private CheckBox Restaurant;
	[Export] private CheckBox Cards;
	[Export] private CheckBox Merit;
	[Export] private CheckBox Fishing;
	[Export] private CheckBox Useful;
	[Export] private CheckBox Useful2;
	[Export] private HSlider StatSlider;
	[Export] private HSlider RareSlider;
	[Export] private LineEdit StatValue;
	[Export] private LineEdit RareValue;

	[Export] private VicePatcherContainer VicePatcher;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SetupTextTranslation();
		SetupButtons();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	void StatGains_Toggled(bool toggled)
	{
		VicePatcher.SetStatsGains(toggled);
		if (toggled)
		{
			StatSlider.Editable = true;
			StatValue.Editable = true;
		}
		else
		{
			StatSlider.Editable = false;
			StatValue.Editable = false;
		}
	}

	void RareSpawns_Toggled(bool toggled)
	{
		VicePatcher.SetRareSpawns(toggled);
		if (toggled)
		{
			RareSlider.Editable = true;
			RareValue.Editable = true;
		}
		else
		{
			RareSlider.Editable = false;
			RareValue.Editable = false;
		}
	}

	void ShortIntro_Toggled(bool toggled) {VicePatcher.SetShortIntro(toggled);}
	void EasyStart_Toggled(bool toggled) {VicePatcher.SetEasyStart(toggled);}
	void UsefulRigging_Toggled(bool toggled)
	{
		VicePatcher.SetSuperBonus(toggled);
		if (toggled)
		UltraLucky.ButtonPressed = false;
	}
	void UltraLucky_Toggled(bool toggled)
	{
		VicePatcher.SetUltraBonus(toggled);
		if (toggled)
		UsefulRigging.ButtonPressed = false;
	}
	void TrainingBoost_Toggled(bool toggled) {VicePatcher.SetTrainingBoost(toggled);}
	void Dirt_Toggled(bool toggled)
	{
		VicePatcher.SetDirtReduction(toggled);
		if (toggled)
		SuperDirt.ButtonPressed = false;
	}
	void SuperDirt_Toggled(bool toggled)
	{
		VicePatcher.SetSDirtReduction(toggled);
		if (toggled)
		Dirt.ButtonPressed = false;
	}
	void Treasure_Toggled(bool toggled) {VicePatcher.SetDrimogemon(toggled);}
	void LowMono_Toggled(bool toggled) {VicePatcher.SetLessMono(toggled);}
	void Seadramon_Toggled(bool toggled) {VicePatcher.SetSeadramon(toggled);}
	void EvoItem_Toggled(bool toggled) {VicePatcher.SetEvoItem(toggled);}
	void MoreItemDrops_Toggled(bool toggled)
	{
		VicePatcher.SetMoreDrops(toggled);
		if (toggled)
		BetterItemDrops.ButtonPressed = false;
	}
	void BetterItemDrops_Toggled(bool toggled)
	{
		VicePatcher.SetBetterDrops(toggled);
		if (toggled)
		MoreItemDrops.ButtonPressed = false;
	}
	void Raise_Toggled(bool toggled) {VicePatcher.SetRaise(toggled);}
	void MoreItemSpawns_Toggled(bool toggled)
	{
		VicePatcher.SetMoreItemSpawn(toggled);
		if (toggled)
		BetterItemSpawns.ButtonPressed = false;
	}
	void BetterItemSpawns_Toggled(bool toggled)
	{
		VicePatcher.SetItemSpawns(toggled);
		if (toggled)
		MoreItemSpawns.ButtonPressed = false;
	}
	void Easymedals_Toggled(bool toggled) {VicePatcher.SetEasyMedals(toggled);}
	void VendingMachines_Toggled(bool toggled) {VicePatcher.SetVendingMachines(toggled);}
	void BetterCurling_Toggled(bool toggled) {VicePatcher.SetCurling(toggled);}
	void Restaurant_Toggled(bool toggled) {VicePatcher.SetRestaurant(toggled);}
	void Cards_Toggled(bool toggled) {VicePatcher.SetCards(toggled);}
	void Merit_Toggled(bool toggled) {VicePatcher.SetMerit(toggled);}
	void Fishing_Toggled(bool toggled) {VicePatcher.SetFishing(toggled);}
	void Useful_Toggled(bool toggled) {VicePatcher.SetHelpfulItems(toggled);}
	void Useful2_Toggled(bool toggled) {VicePatcher.SetUseful2(toggled);}
	void StatSlider_ValueChanged(double value)
	{
		VicePatcher.SetStatsValue((int)value);
		StatValue.Text = ((int)value).ToString();
	}
	void RareSlider_ValueChanged(double value)
	{
		VicePatcher.SetRareSpawnValue((int)value);
		RareValue.Text = ((int)value).ToString();
	}

	void StatValue_TextChanged(string text)
	{
		if (text != null && text != "")
		{
			int value;
			try { value = text.ToInt(); }
			catch (FormatException) { value = (int)StatSlider.MaxValue; }
			
			if (value > (int)StatSlider.MaxValue || value == 0)
				value = (int)StatSlider.MaxValue;
			else if(value < (int)StatSlider.MinValue)
			    value = (int)StatSlider.MinValue;
			StatValue.Text = value.ToString();
			StatSlider.SetValueNoSignal(value);
			VicePatcher.SetStatsValue((int)value);
		}
	}

	void RareValue_TextChanged(string text)
	{
		if (text != null && text != "")
		{
			int value;
			try { value = text.ToInt(); }
			catch (FormatException) { value = (int)StatSlider.MaxValue; }
			if (value > (int)RareSlider.MaxValue)
				value = (int)RareSlider.MaxValue;
			else if (value < (int)RareSlider.MinValue)
				value = (int)RareSlider.MinValue;
			RareValue.Text = value.ToString();
			RareSlider.SetValueNoSignal(value);
			VicePatcher.SetRareSpawnValue((int)value);
		}
	}

	void _on_page_2_pressed()
	{
		Panel1.Visible = false;
		Panel2.Visible = true;
	}

	void _on_page_1_pressed()
	{
		Panel1.Visible = true;
		Panel2.Visible = false;
	}

	void _on_hardcore_toggled(bool toggled)
	{
		if (toggled)
		{
			StatSlider.TooltipText = Tr("StatGains_SinfoH");
			UsefulRigging.Disabled = true;
			UsefulRigging.ButtonPressed = false;
			UltraLucky.Disabled = true;
			UltraLucky.ButtonPressed = false;
			StatSlider.MaxValue = 10;
			if (StatSlider.Value > 10)
				StatSlider.Value = 10;
		}
		else
		{
			if (!VicePatcher.GetUltraHardcore())
			{
				UsefulRigging.Disabled = false;
				UltraLucky.Disabled = false;
				StatSlider.MaxValue = 20;
				StatSlider.TooltipText = Tr("StatGains_Sinfo");
			}
		}
	}

	void _on_ultra_hardcore_toggled(bool toggled)
	{
		if (toggled)
		{
			UsefulRigging.Disabled = true;
			UsefulRigging.ButtonPressed = false;
			UltraLucky.Disabled = true;
			UltraLucky.ButtonPressed = false;
			EasyStart.ButtonPressed = false;
			EasyStart.Disabled = true;
			BetterItemDrops.Disabled = true;
			BetterItemSpawns.Disabled = true;
			BetterItemDrops.ButtonPressed = false;
			BetterItemSpawns.ButtonPressed = false;
			StatSlider.MaxValue = 1;
			if (StatSlider.Value > 1)
				StatSlider.Value = 1;
			StatSlider.TooltipText = Tr("StatGains_SinfoUH");
		}
		else
		{
			EasyStart.Disabled = false;
			BetterItemDrops.Disabled = false;
			BetterItemSpawns.Disabled = false;
			if (VicePatcher.GetViceDifficulty() == VicePatcherContainer.viceDifficulty.HARDCORE)
			{
				StatSlider.MaxValue = 10;
				StatSlider.TooltipText = Tr("StatGains_SinfoH");
			}
			else
			{
				UsefulRigging.Disabled = false;
				UltraLucky.Disabled = false;
				StatSlider.MaxValue = 20;
				StatSlider.TooltipText = Tr("StatGains_Sinfo");
			}
		}
	}

	void SetupTextTranslation()
	{
		Title.Text = Tr("UsefulTitle_L");
		Title.TooltipText = Tr("UsefulTitle_info");
		Title2.Text = Tr("Useful_L");
		Title2.TooltipText = Tr("Useful_info");
		StatGains.Text = Tr("StatGains_L");
		StatGains.TooltipText = Tr("StatGains_info");
		StatSlider.TooltipText = Tr("StatGains_Sinfo");
		RareSpawns.Text = Tr("RareSpawns_L");
		RareSpawns.TooltipText = Tr("RareSpawns_info");
		RareSlider.TooltipText = Tr("RareSpawns_Sinfo");
		ShortIntro.Text = Tr("ShortIntro_L");
		ShortIntro.TooltipText = Tr("ShortIntro_info");
		EasyStart.Text = Tr("EasyStart_L");
		EasyStart.TooltipText = Tr("EasyStart_info");
		UsefulRigging.Text = Tr("UsefulRigging_L");
		UsefulRigging.TooltipText = Tr("UsefulRigging_info");
		UltraLucky.Text = Tr("UltraLucky_L");
		UltraLucky.TooltipText = Tr("UltraLucky_info");
		TrainingBoost.Text = Tr("TrainingBoost_L");
		TrainingBoost.TooltipText = Tr("TrainingBoost_info");
		Dirt.Text = Tr("Dirt_L");
		Dirt.TooltipText = Tr("Dirt_info");
		SuperDirt.Text = Tr("SuperDirt_L");
		SuperDirt.TooltipText = Tr("SuperDirt_info");
		Treasure.Text = Tr("Treasure_L");
		Treasure.TooltipText = Tr("Treasure_info");
		LowMono.Text = Tr("LowMono_L");
		LowMono.TooltipText = Tr("LowMono_info");
		Seadramon.Text = Tr("Seadramon_L");
		Seadramon.TooltipText = Tr("Seadramon_info");
		EvoItem.Text = Tr("EvoItem_L");
		EvoItem.TooltipText = Tr("EvoItem_info");
		MoreItemDrops.Text = Tr("MoreItemDrops_L");
		MoreItemDrops.TooltipText = Tr("MoreItemDrops_info");
		BetterItemDrops.Text = Tr("BetterItemDrops_L");
		BetterItemDrops.TooltipText = Tr("BetterItemDrops_info");
		Raise.Text = Tr("Raise_L");
		Raise.TooltipText = Tr("Raise_info");
		MoreItemSpawns.Text = Tr("MoreItemSpawns_L");
		MoreItemSpawns.TooltipText = Tr("MoreItemSpawns_info");
		BetterItemSpawns.Text = Tr("BetterItemSpawns_L");
		BetterItemSpawns.TooltipText = Tr("BetterItemSpawns_info");
		Easymedals.Text = Tr("Easymedals_L");
		Easymedals.TooltipText = Tr("Easymedals_info");
		VendingMachines.Text = Tr("VendingMachines_L");
		VendingMachines.TooltipText = Tr("VendingMachines_info");
		BetterCurling.Text = Tr("BetterCurling_L");
		BetterCurling.TooltipText = Tr("BetterCurling_info");
		Restaurant.Text = Tr("Restaurant_L");
		Restaurant.TooltipText = Tr("Restaurant_info");
		Cards.Text = Tr("Cards_L");
		Cards.TooltipText = Tr("Cards_info");
		Merit.Text = Tr("Merit_L");
		Merit.TooltipText = Tr("Merit_info");
		Fishing.Text = Tr("Fishing_L");
		Fishing.TooltipText = Tr("Fishing_info");
		Useful.Text = Tr("Useful_L");
		Useful.TooltipText = Tr("Useful_info");
		Useful2.Text = Tr("Useful2_L");
		Useful2.TooltipText = Tr("Useful2_info");
	}

	void SetupButtons()
	{
		StatGains.Toggled += StatGains_Toggled;
		RareSpawns.Toggled += RareSpawns_Toggled;
		ShortIntro.Toggled += ShortIntro_Toggled;
		EasyStart.Toggled += EasyStart_Toggled;
		UsefulRigging.Toggled += UsefulRigging_Toggled;
		UltraLucky.Toggled += UltraLucky_Toggled;
		TrainingBoost.Toggled += TrainingBoost_Toggled;
		Dirt.Toggled += Dirt_Toggled;
		SuperDirt.Toggled += SuperDirt_Toggled;
		Treasure.Toggled += Treasure_Toggled;
		LowMono.Toggled += LowMono_Toggled;
		Seadramon.Toggled += Seadramon_Toggled;
		EvoItem.Toggled += EvoItem_Toggled;
		MoreItemDrops.Toggled += MoreItemDrops_Toggled;
		BetterItemDrops.Toggled += BetterItemDrops_Toggled;
		Raise.Toggled += Raise_Toggled;
		MoreItemSpawns.Toggled += MoreItemSpawns_Toggled;
		BetterItemSpawns.Toggled += BetterItemSpawns_Toggled;
		Easymedals.Toggled += Easymedals_Toggled;
		VendingMachines.Toggled += VendingMachines_Toggled;
		BetterCurling.Toggled += BetterCurling_Toggled;
		Restaurant.Toggled += Restaurant_Toggled;
		Cards.Toggled += Cards_Toggled;
		Merit.Toggled += Merit_Toggled;
		Fishing.Toggled += Fishing_Toggled;
		Useful.Toggled += Useful_Toggled;
		Useful2.Toggled += Useful2_Toggled;
		RareSlider.ValueChanged += RareSlider_ValueChanged;
		StatSlider.ValueChanged += StatSlider_ValueChanged;
		StatValue.TextChanged += StatValue_TextChanged;
		RareValue.TextChanged += RareValue_TextChanged;
	}
}
