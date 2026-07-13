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
	[Export] private Button Apply;

	[Export] private VicePatcherContainer VicePatcher;

	[Export] private TextureButton[] infoButtons;
	
	System.Collections.Generic.List<CheckBox> allCheckboxes;
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

	void Panjyamon_Toggled(bool toggled) 
	{ 
		VicePatcher.SetRestorePanjya(toggled); 
		if (toggled)
			BWere.ButtonPressed = false;
	}

	void Vermillimon_Toggled(bool toggled) { VicePatcher.SetVermillimon(toggled); }

	void Starters2_Toggled(bool toggled)
	{
		VicePatcher.SetStarters2(toggled);
		if (toggled)
			Kunemon.ButtonPressed = false;
	}

	void Kunemon_Toggled(bool toggled)
	{
		VicePatcher.SetKunemon(toggled);
		if (toggled)
			Starters2.ButtonPressed = false;
	}

	void Curling_Toggled(bool toggled) { VicePatcher.SetCurlingRandomizer(toggled); }

	void RMTGR_Toggled(bool toggled) { VicePatcher.SetRMetal(toggled); }

	void BWere_Toggled(bool toggled) 
	{ 
		VicePatcher.SetBWere(toggled); 
		if (toggled)
			Panjyamon.ButtonPressed = false;
	}

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
		Curling.TooltipText = Tr("CURLING");
		RMTGR.TooltipText = Tr("RMTGR");
		BWere.TooltipText = Tr("BWERE");
		Myotismon.TooltipText = Tr("Myotismon_info");
		Apply.Text = Tr("Apply_L");
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

		allCheckboxes = [Myotismon, Panjyamon, Starters2, Vermillimon, Kunemon, Curling, RMTGR, BWere];
		infoButtons[0].Pressed += () =>	{VicePatcher.OpenInfoWindowDigimon(allCheckboxes[0].TooltipText, allCheckboxes[0].Text, "https://docs.google.com/spreadsheets/d/1lG3aLJsLiCwcZXo5-OS18o21GngTVuyAiKA0liV_kpM/edit?gid=1719894242#gid=1719894242");};
		for (int i = 1; i < infoButtons.Length; i++)
		{
			int cat = i;
			infoButtons[i].Pressed += () =>
			{
				VicePatcher.OpenInfoWindowDigimon(allCheckboxes[cat].TooltipText, allCheckboxes[cat].Text, null);
			};
		}	
	}

	public void LoadSaveData(bool MyotismonS, bool PanjyamonS, bool VermillimonS, bool Starters2S, bool KunemonS, bool CurlingS, bool RMTGRS, bool BWereS)
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

	void CloseDigimon()
	{
		RestartSelection();
		VicePatcher.MainMenuVisible();
		this.Visible = false;
	}

	void ApplyDigimon()
	{
		VicePatcher.MainMenuVisible();
		this.Visible = false;
	}
}
