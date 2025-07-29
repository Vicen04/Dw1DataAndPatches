using Godot;
using System;

public partial class DigimonContainer : PanelContainer
{

	[Export] private Label Title;
	[Export] private CheckBox Myotismon;
	[Export] private CheckBox Vermillimon;
	[Export] private CheckBox Panjyamon;
	[Export] private CheckBox Starters2;
	[Export] private CheckBox Kunemon;
	[Export] private CheckBox Curling;
	[Export] private CheckBox RMTGR;
	[Export] private CheckBox BWere;

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

	void Myotismon_Toggled(bool toggled) { VicePatcher.SetMyotismon(toggled); }

	void Panjyamon_Toggled(bool toggled) { VicePatcher.SetRestorePanjya(toggled); }

	void Vermillimon_Toggled(bool toggled) { VicePatcher.SetVermillimon(toggled); }

	void Starters2_Toggled(bool toggled)
	{
		VicePatcher.SetStarters2(toggled);
		if (toggled)
			Kunemon.ButtonPressed = false;
	}

	void Kunemon_Toggled(bool toggled)
	{
		VicePatcher.SetMyotismon(toggled);
		if (toggled)
			Starters2.ButtonPressed = false;
	}

	void Curling_Toggled(bool toggled) { VicePatcher.SetCurlingRandomizer(toggled); }

	void RMTGR_Toggled(bool toggled) { VicePatcher.SetRMetal(toggled); }

	void BWere_Toggled(bool toggled) { VicePatcher.SetBWere(toggled); }

	void _on_filth_challenge_toggled(bool toggled)
	{
		if (toggled)
		{
			Myotismon.Disabled = true;
			Starters2.Disabled = true;
			Kunemon.Disabled = true;
			Myotismon.ButtonPressed = false;
			Starters2.ButtonPressed = false;
			Kunemon.ButtonPressed = false;
		}
		else
		{
			Myotismon.Disabled = false;
			Starters2.Disabled = false;
			Kunemon.Disabled = false;
		}
	}

	void SetupTextTranslation()
	{
		Title.Text = Tr("Digimon_L");
		Title.TooltipText = Tr("Digimon_info");
		Panjyamon.Text = Tr("Panjyamon_L");
		Panjyamon.TooltipText = Tr("Panjyamon_info");
		Starters2.Text = Tr("Starters2_L");
		Starters2.TooltipText = Tr("Starters2_info");
		Vermillimon.TooltipText = Tr("Vermillimon_info");
		Kunemon.TooltipText = Tr("Kunemon_info");
		Curling.TooltipText = Tr("CurlingR_info");
		RMTGR.TooltipText = Tr("RMTGR_info");
		BWere.TooltipText = Tr("BWere_info");
		Myotismon.TooltipText = Tr("Myotismon_info");
	}

	void SetupButtons()
	{
		Myotismon.Toggled += Myotismon_Toggled;
		Panjyamon.Toggled += Panjyamon_Toggled;
		Starters2.Toggled += Starters2_Toggled;
		Vermillimon.Toggled += Vermillimon_Toggled;
		Kunemon.Toggled += Kunemon_Toggled;
		Curling.Toggled += Curling_Toggled;
		RMTGR.Toggled += RMTGR_Toggled;
		BWere.Toggled += BWere_Toggled;
	}

	public void LoadSaveData(bool MyotismonS, bool PanjyamonS, bool VermillimonS, bool Starters2S, bool KunemonS, bool CurlingS, bool RMTGRS, bool BWereS, bool WarGreymonS,
	bool TentomonS, bool MetalGaruS)
	{
		Myotismon.ButtonPressed = MyotismonS;
		Panjyamon.ButtonPressed = PanjyamonS;
		Vermillimon.ButtonPressed = VermillimonS;
		Starters2.ButtonPressed = Starters2S;
		Kunemon.ButtonPressed = KunemonS;
		Curling.ButtonPressed = CurlingS;
		RMTGR.ButtonPressed = RMTGRS;
		BWere.ButtonPressed = BWereS;
	}
	
	public void RestartSelection()
	{
		Myotismon.ButtonPressed = false;
		Panjyamon.ButtonPressed = false;	
		Vermillimon.ButtonPressed = false;
		Starters2.ButtonPressed = false;
		Kunemon.ButtonPressed = false;
		Curling.ButtonPressed = false;
		RMTGR.ButtonPressed = false;
		BWere.ButtonPressed = false;
	}
}
