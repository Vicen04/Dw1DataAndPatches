using Godot;
using System;

public partial class DifficultyContainer : PanelContainer
{
	[Export] private Label Title;
	[Export] private Label Expanded;
	[Export] private Label Filth;
	[Export] private Label Extra;
	[Export] private CheckBox Challenge;
	[Export] private CheckBox Hardmode;
	[Export] private CheckBox Hardcore;
	[Export] private CheckBox TrueHardcore;
	[Export] private CheckBox Progression;
	[Export] private CheckBox UltraHardcore;
	[Export] private CheckBox FilthChallenge;
	[Export] private CheckBox RestoreFilth;
	[Export] private CheckBox FairBattles;
	[Export] private CheckBox Tournaments;
	[Export] private CheckBox RNG;
	[Export] private CheckBox Mono4K;
	[Export] private CheckBox Mono8K;
	[Export] private CheckBox Rookie;
	[Export] private CheckBox Tanemon;
	[Export] private CheckBox AllBattles;
	[Export] private VicePatcherContainer VicePatcher;
	[Export] private Panel Difficulty1;
	[Export] private Panel Difficulty2;


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

	void Challenge_Toggled(bool toggled)
	{
		if (toggled)
		{
			VicePatcher.SetViceDifficulty(VicePatcherContainer.ViceDifficulty.CHALLENGE);
			Hardmode.ButtonPressed = false;
			Hardcore.ButtonPressed = false;
			UltraHardcore.Disabled = true;
			FairBattles.Disabled = true;
			UltraHardcore.ButtonPressed = false;
			FairBattles.ButtonPressed = false;
		}
		else
		{
			if (Hardmode.ButtonPressed || Hardcore.ButtonPressed)
				UltraHardcore.Disabled = false;
			FairBattles.Disabled = false;

			if (!Hardcore.ButtonPressed && !Hardcore.ButtonPressed)
				VicePatcher.SetViceDifficulty(VicePatcherContainer.ViceDifficulty.NONE);
		}
	}

	void Hardmode_Toggled(bool toggled)
	{
		if (toggled)
		{
			VicePatcher.SetViceDifficulty(VicePatcherContainer.ViceDifficulty.HARDMODE);
			Challenge.ButtonPressed = false;
			Hardcore.ButtonPressed = false;
			UltraHardcore.Disabled = false;
		}
		else
		{
			if (!Hardcore.ButtonPressed)
				UltraHardcore.Disabled = true;
			if (!Hardcore.ButtonPressed && !Challenge.ButtonPressed)
				VicePatcher.SetViceDifficulty(VicePatcherContainer.ViceDifficulty.NONE);
		}

	}

	void Hardcore_Toggled(bool toggled)
	{

		if (toggled)
		{
			VicePatcher.SetViceDifficulty(VicePatcherContainer.ViceDifficulty.HARDCORE);
			Hardmode.ButtonPressed = false;
			Challenge.ButtonPressed = false;
			UltraHardcore.Disabled = false;
			TrueHardcore.Disabled = false;
		}
		else
		{
			if (!Hardmode.ButtonPressed)
				UltraHardcore.Disabled = true;
			TrueHardcore.Disabled = true;
			TrueHardcore.ButtonPressed = false;

			if (!Hardmode.ButtonPressed && !Challenge.ButtonPressed)
				VicePatcher.SetViceDifficulty(VicePatcherContainer.ViceDifficulty.NONE);
		}
	}

	void TrueHardcore_Toggled(bool toggled)
	{
		VicePatcher.SetTrueHardcore(toggled);
		if (toggled)
		{
			AllBattles.ButtonPressed = false;
			AllBattles.Disabled = true;
		}
		else
			AllBattles.Disabled = false;

	}

	void UltraHardcore_Toggled(bool toggled)
	{
		VicePatcher.SetUltraHardcore(toggled);
		if (toggled)
			FairBattles.ButtonPressed = false;
	}

	void Progression_Toggled(bool toggled) { VicePatcher.SetProgression(toggled); }

	void Filth_Toggled(bool toggled)
	{
		VicePatcher.SetFilth(toggled);
		if (toggled)
		{
			RestoreFilth.Disabled = false;
			Rookie.ButtonPressed = false;
			Tanemon.SetPressedNoSignal(false);
			VicePatcher.SetTanemon(false);
		}
		else
		{
			RestoreFilth.Disabled = true;
			RestoreFilth.ButtonPressed = false;
		}
	}

	void RFilth_Toggled(bool toggled) { VicePatcher.SetRestoreFilth(toggled); }

	void FairBattles_Toggled(bool toggled)
	{
		VicePatcher.SetSuperHardcore(toggled);
		if (toggled)
			UltraHardcore.ButtonPressed = false;
	}

	void Tournaments_Toggled(bool toggled) { VicePatcher.SetHardTourney(toggled); }

	void NoRNG_Toggled(bool toggled) { VicePatcher.SetRNG(toggled); }

	void Mono4K_Toggled(bool toggled)
	{
		VicePatcher.SetHardMono(toggled);
		if (toggled)
			Mono8K.ButtonPressed = false;
	}

	void Mono8K_Toggled(bool toggled)
	{
		VicePatcher.Set8kMono(toggled);
		if (toggled)
			Mono4K.ButtonPressed = false;
	}

	void Rookie_Toggled(bool toggled)
	{
		VicePatcher.SetRookie(toggled);
		if (toggled)
		{
			FilthChallenge.ButtonPressed = false;
			Tanemon.ButtonPressed = false;
		}
	}

	void Tanemon_Toggled(bool toggled)
	{
		VicePatcher.SetTanemon(toggled);
		if (toggled)
		{
			Rookie.ButtonPressed = false;
			FilthChallenge.SetPressedNoSignal(false);
			VicePatcher.SetFilth(false);
			RestoreFilth.Disabled = true;
			RestoreFilth.ButtonPressed = false;
		}
	}

	void AllBattles_Toggled(bool toggled)
    {
        VicePatcher.SetAllBattles(toggled);
    }

	void SetupTextTranslation()
	{
		Title.Text = Tr("Difficulty_L");
		Title.TooltipText = Tr("Difficulty_info");
		Expanded.Text = Tr("Expanded_L");
		Expanded.TooltipText = Tr("Expanded_info");
		Filth.Text = Tr("Filth_L");
		Filth.TooltipText = Tr("Filth_info");
		Extra.Text = Tr("Extra_L");
		Extra.TooltipText = Tr("Extra_info");
		Challenge.Text = Tr("Challenge_L");
		Challenge.TooltipText = Tr("Challenge_info");
		Hardmode.Text = Tr("Hardmode_L");
		Hardmode.TooltipText = Tr("Hardmode_info");
		Hardcore.Text = Tr("Hardcore_L");
		Hardcore.TooltipText = Tr("Hardcore_info");
		TrueHardcore.Text = Tr("THardcore_L");
		TrueHardcore.TooltipText = Tr("THardcore_info");
		Progression.Text = Tr("Progression_L");
		Progression.TooltipText = Tr("Progression_info");
		UltraHardcore.Text = Tr("UHardcore_L");
		UltraHardcore.TooltipText = Tr("UHardcore_info");
		FilthChallenge.Text = Tr("FilthChallenge_L");
		FilthChallenge.TooltipText = Tr("FilthChallenge_info");
		RestoreFilth.Text = Tr("RFilth_L");
		RestoreFilth.TooltipText = Tr("RFilth_info");
		FairBattles.Text = Tr("FairBattles_L");
		FairBattles.TooltipText = Tr("FairBattles_info");
		Tournaments.Text = Tr("Tournaments_L");
		Tournaments.TooltipText = Tr("Tournaments_info");
		RNG.Text = Tr("RNG_L");
		RNG.TooltipText = Tr("RNG_info");
		Mono4K.TooltipText = Tr("Mono4K_info");
		Mono8K.TooltipText = Tr("Mono8K_info");
		Rookie.Text = Tr("Rookie_L");
		Rookie.TooltipText = Tr("Rookie_info");
		Tanemon.Text = Tr("Tanemon_L");
		Tanemon.TooltipText = Tr("Tanemon_info");
		AllBattles.Text = Tr("AllBattles_L");
		AllBattles.TooltipText = Tr("AllBattles_info");
	}

	void SetupButtons()
	{
		Challenge.Toggled += Challenge_Toggled;
		Hardmode.Toggled += Hardmode_Toggled;
		Hardcore.Toggled += Hardcore_Toggled;
		TrueHardcore.Toggled += TrueHardcore_Toggled;
		UltraHardcore.Toggled += UltraHardcore_Toggled;
		Progression.Toggled += Progression_Toggled;
		FilthChallenge.Toggled += Filth_Toggled;
		RestoreFilth.Toggled += RFilth_Toggled;
		FairBattles.Toggled += FairBattles_Toggled;
		Tournaments.Toggled += Tournaments_Toggled;
		RNG.Toggled += NoRNG_Toggled;
		Mono4K.Toggled += Mono4K_Toggled;
		Mono8K.Toggled += Mono8K_Toggled;
		Rookie.Toggled += Rookie_Toggled;
		Tanemon.Toggled += Tanemon_Toggled;
		AllBattles.Toggled += AllBattles_Toggled;
	}

	void ChangeToPage2()
	{
		Difficulty1.Visible = false;
		Difficulty2.Visible = true;
	}

	void ChangetoPage1()
	{
		Difficulty1.Visible = true;
		Difficulty2.Visible = false;
	}

	public void LoadSaveData(bool ChallengeS, bool HardmodeS, bool HardcoreS, bool TrueHardcoreS, bool UltraHardcoreS, bool ProgressionS, bool FilthS, bool RFilthS, bool FairBattlesS,
	bool TournamentsS, bool NoRNGS, bool Mono4KS, bool Mono8KS, bool RookieS, bool TanemonS, bool allBattles)
	{
		Challenge.ButtonPressed = ChallengeS;
		Hardmode.ButtonPressed = HardmodeS;
		Hardcore.ButtonPressed = HardcoreS;
		TrueHardcore.ButtonPressed = TrueHardcoreS;
		UltraHardcore.ButtonPressed = UltraHardcoreS;
		Progression.ButtonPressed = ProgressionS;
		FilthChallenge.ButtonPressed = FilthS;
		RestoreFilth.ButtonPressed = RFilthS;
		FairBattles.ButtonPressed = FairBattlesS;
		Tournaments.ButtonPressed = TournamentsS;
		RNG.ButtonPressed = NoRNGS;
		Mono4K.ButtonPressed = Mono4KS;
		Mono8K.ButtonPressed = Mono8KS;
		Rookie.ButtonPressed = RookieS;
		Tanemon.ButtonPressed = TanemonS;
		AllBattles.ButtonPressed = allBattles;
	}
	
	public void RestartSelection()
	{
		Challenge.ButtonPressed = false;
		Hardmode.ButtonPressed = false;	
		Hardcore.ButtonPressed = false;
		TrueHardcore.ButtonPressed = false;
		UltraHardcore.ButtonPressed = false;
		Progression.ButtonPressed = false;
		FilthChallenge.ButtonPressed = false;
		RestoreFilth.ButtonPressed = false;
		FairBattles.ButtonPressed = false;
		Tournaments.ButtonPressed = false;
		RNG.ButtonPressed = false;
		Mono4K.ButtonPressed = false;
		Mono8K.ButtonPressed = false;
		Rookie.ButtonPressed = false;
		Tanemon.ButtonPressed = false;
		AllBattles.ButtonPressed = false;
	}
	/*void _on_exit_installer_pressed()
	{
		Challenge.ButtonPressed = false;
		Hardmode.ButtonPressed = false;
		Hardcore.ButtonPressed = false;
	}*/
}
