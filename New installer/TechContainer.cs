using Godot;
using System;

public partial class TechContainer : PanelContainer
{

	[Export] private Label Title;
	[Export] private CheckBox BetterBattle;
	[Export] private CheckBox BetterBrains;
	[Export] private CheckBox LearnMulti;
	[Export] private CheckBox InsaneDamage;
	[Export] private CheckBox Telepathy;
	[Export] private CheckBox SkipOrders;
	[Export] private CheckBox NerfStatue;

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

	void BetterBattle_Toggled(bool toggled)	{VicePatcher.SetBetterBattleTech(toggled);	}

	void BetterBrains_Toggled(bool toggled)	{VicePatcher.SetBetterBrainTech(toggled);	}

	void LearnMulti_Toggled(bool toggled)	{VicePatcher.SetMultipleTechs(toggled);	}

	void InsaneDamage_Toggled(bool toggled)	{VicePatcher.SetInsaneDamage(toggled);	}

	void Telepathy_Toggled(bool toggled)	{VicePatcher.SetEasyTech(toggled);	}

	void SkipOrders_Toggled(bool toggled)	{VicePatcher.SetOrders(toggled);	}

	void NerfStatue_Toggled(bool toggled)	{VicePatcher.SetIceNerf(toggled);	}

	void SetupTextTranslation()
	{
		Title.Text = Tr("Techniques_L");
		Title.TooltipText = Tr("Techniques_info");
		BetterBattle.Text = Tr("BetterBattle_L");
		BetterBattle.TooltipText = Tr("BetterBattle_info");
		BetterBrains.Text = Tr("BetterBrains_L");
		BetterBrains.TooltipText = Tr("BetterBrains_info");
		LearnMulti.Text = Tr("LearnMulti_L");
		LearnMulti.TooltipText = Tr("LearnMulti_info");
		InsaneDamage.Text = Tr("InsaneDamage_L");
		InsaneDamage.TooltipText = Tr("InsaneDamage_info");
		Telepathy.Text = Tr("Telepathy_L");
		Telepathy.TooltipText = Tr("Telepathy_info");
		SkipOrders.Text = Tr("SkipOrders_L");
		SkipOrders.TooltipText = Tr("SkipOrders_info");
		NerfStatue.Text = Tr("NerfStatue_L");
		NerfStatue.TooltipText = Tr("NerfStatue_info");
	}

	void SetupButtons()
	{
		BetterBattle.Toggled += BetterBattle_Toggled;
		BetterBrains.Toggled += BetterBrains_Toggled;
		LearnMulti.Toggled += LearnMulti_Toggled;
		InsaneDamage.Toggled += InsaneDamage_Toggled;
		Telepathy.Toggled += Telepathy_Toggled;
		SkipOrders.Toggled += SkipOrders_Toggled;
		NerfStatue.Toggled += NerfStatue_Toggled;
	}
}
