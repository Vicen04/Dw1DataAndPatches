using Godot;
using System;

public partial class MiscContainer : PanelContainer
{

	[Export] private Label Title;
	[Export] private CheckBox Clock;
	[Export] private CheckBox Input;
	[Export] private CheckBox Areas;
	[Export] private CheckBox BattleText;
	[Export] private CheckBox BoostItems;
	[Export] private CheckBox NerfTech;
	[Export] private CheckBox OGLife;
	[Export] private CheckBox RemoveTech;
	[Export] private CheckBox RemoveEvo;
	[Export] private CheckBox NewMono;
	[Export] private CheckBox InsaneBattles;
	[Export] private CheckBox MapColour;
	[Export] private CheckBox OgreTel;
	[Export] private CheckBox OGType;

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

	void Clock_Toggled(bool toggled)
	{
		VicePatcher.SetDigitalClock(toggled);
	}

	void Input_Toggled(bool toggled)
	{
		VicePatcher.SetExtraInput(toggled);
	}

	void Areas_Toggled(bool toggled)
	{
		VicePatcher.SetUnlockAreas(toggled);
	}

	void BattleText_Toggled(bool toggled)
	{
		VicePatcher.SetQuickText(toggled);
	}

	void BoostItems_Toggled(bool toggled)
	{
		VicePatcher.SetSuperBoostItems(toggled);
	}

	void NerfTech_Toggled(bool toggled)
	{
		VicePatcher.SetNerfBoost(toggled);
		if (toggled)
			RemoveTech.ButtonPressed = false;
	}

	void OGLife_Toggled(bool toggled)
	{
		VicePatcher.SetRestoreLifetime(toggled);
	}

	void RemoveTech_Toggled(bool toggled)
	{
		VicePatcher.SetRemoveTechBoost(toggled);
		if (toggled)
			NerfTech.ButtonPressed = false;
	}

	void RemoveEvo_Toggled(bool toggled)
	{
		VicePatcher.SetEvoInfo(toggled);
	}

	void NewMono_Toggled(bool toggled)
	{
		VicePatcher.SetNewMono(toggled);
	}

	void InsaneBattles_Toggled(bool toggled)
	{
		VicePatcher.SetInsaneBattle(toggled);
	}

	void MapColour_Toggled(bool toggled)
	{
		VicePatcher.SetMapColour(toggled);
	}

	void OgreTel_Toggled(bool toggled)
	{
		VicePatcher.SetTelephone(toggled);
	}

	void OGType_Toggled(bool toggled)
	{
		VicePatcher.SetOriginalType(toggled);
	}


	void SetupTextTranslation()
	{
		Title.Text = Tr("Miscellaneous_L");
		Title.TooltipText = Tr("Miscellaneous_info");
		Clock.Text = Tr("Clock_L");
		Clock.TooltipText = Tr("Clock_info");
		Input.Text = Tr("Input_L");
		Input.TooltipText = Tr("Input_info");
		Areas.Text = Tr("Areas_L");
		Areas.TooltipText = Tr("Areas_info");
		BattleText.Text = Tr("BattleText_L");
		BattleText.TooltipText = Tr("BattleText_info");
		BoostItems.Text = Tr("BoostItems_L");
		BoostItems.TooltipText = Tr("BoostItems_info");
		NerfTech.Text = Tr("NerfTech_L");
		NerfTech.TooltipText = Tr("NerfTech_info");
		OGLife.Text = Tr("OGLife_L");
		OGLife.TooltipText = Tr("OGLife_info");
		RemoveTech.Text = Tr("RemoveTech_L");
		RemoveTech.TooltipText = Tr("RemoveTech_info");
		RemoveEvo.Text = Tr("RemoveEvo_L");
		RemoveEvo.TooltipText = Tr("RemoveEvo_info");
		NewMono.Text = Tr("NewMono_L");
		NewMono.TooltipText = Tr("NewMono_info");
		InsaneBattles.Text = Tr("InsaneBattles_L");
		InsaneBattles.TooltipText = Tr("InsaneBattles_info");
		MapColour.Text = Tr("MapColour_L");
		MapColour.TooltipText = Tr("MapColour_info");
		OgreTel.Text = Tr("OgreTel_L");
		OgreTel.TooltipText = Tr("OgreTel_info");
		OGType.Text = Tr("OGType_L");
		OGType.TooltipText = Tr("OGType_info");
	}

	void SetupButtons()
	{
		Clock.Toggled += Clock_Toggled;
		Input.Toggled += Input_Toggled;
		Areas.Toggled += Areas_Toggled;
		BattleText.Toggled += BattleText_Toggled;
		BoostItems.Toggled += BoostItems_Toggled;
		NerfTech.Toggled += NerfTech_Toggled;
		OGLife.Toggled += OGLife_Toggled;
		RemoveTech.Toggled += RemoveTech_Toggled;
		RemoveEvo.Toggled += RemoveEvo_Toggled;
		NewMono.Toggled += NewMono_Toggled;
		InsaneBattles.Toggled += InsaneBattles_Toggled;
		MapColour.Toggled += MapColour_Toggled;
		OgreTel.Toggled += OgreTel_Toggled;
		OGType.Toggled += OGType_Toggled;
	}

	public void LoadSaveData(bool ClockS, bool InputS, bool AreasS, bool BattleTextS, bool BoostItemsS, bool NerfTechS, bool OGLifeS, bool RemoveTechS, bool RemoveEvoS,
	bool NewMonoS, bool InsaneBattlesS, bool MapColourS, bool OgreTelS, bool OGTypeS)
	{
		Clock.ButtonPressed = ClockS;
		Input.ButtonPressed = InputS;
		Areas.ButtonPressed = AreasS;
		BattleText.ButtonPressed = BattleTextS;
		BoostItems.ButtonPressed = BoostItemsS;
		NerfTech.ButtonPressed = NerfTechS;
		OGLife.ButtonPressed = OGLifeS;
		RemoveTech.ButtonPressed = RemoveTechS;
		RemoveEvo.ButtonPressed = RemoveEvoS;
		NewMono.ButtonPressed = NewMonoS;
		InsaneBattles.ButtonPressed = InsaneBattlesS;
		MapColour.ButtonPressed = MapColourS;
		OgreTel.ButtonPressed = OgreTelS;
		OGType.ButtonPressed = OGTypeS;
	}

	public void RestartSelection()
	{
		Clock.ButtonPressed = false;
		Input.ButtonPressed = false;
		Areas.ButtonPressed = false;
		BattleText.ButtonPressed = false;
		BoostItems.ButtonPressed = false;
		NerfTech.ButtonPressed = false;
		OGLife.ButtonPressed = false;
		RemoveTech.ButtonPressed = false;
		RemoveEvo.ButtonPressed = false;
		NewMono.ButtonPressed = false;
		InsaneBattles.ButtonPressed = false;
		MapColour.ButtonPressed = false;
		OgreTel.ButtonPressed = false;
		OGType.ButtonPressed = false;
	}
}
