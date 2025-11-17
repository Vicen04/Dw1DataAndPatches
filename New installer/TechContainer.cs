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
	[Export] private CheckBox RemoveTech;
	[Export] private CheckBox NerfTech;
	[Export] private CheckBox RemoveNewStatus;
	[Export] private CheckBox RemoveEffect;
	[Export] private CheckBox NerfEffect;
	[Export] private CheckBox PoisonTimer;
	[Export] private CheckBox AddEffects;
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

	void BetterBattle_Toggled(bool toggled) { VicePatcher.SetBetterBattleTech(toggled); }

	void BetterBrains_Toggled(bool toggled) { VicePatcher.SetBetterBrainTech(toggled); }

	void LearnMulti_Toggled(bool toggled) { VicePatcher.SetMultipleTechs(toggled); }

	void InsaneDamage_Toggled(bool toggled) { VicePatcher.SetInsaneDamage(toggled); }

	void Telepathy_Toggled(bool toggled) { VicePatcher.SetEasyTech(toggled); }

	void SkipOrders_Toggled(bool toggled) { VicePatcher.SetOrders(toggled); }

	void NerfStatue_Toggled(bool toggled) { VicePatcher.SetIceNerf(toggled); }

	void NerfTech_Toggled(bool toggled)
	{
		VicePatcher.SetNerfBoost(toggled);
		if (toggled)
			RemoveTech.ButtonPressed = false;
	}
	void RemoveTech_Toggled(bool toggled)
	{
		VicePatcher.SetRemoveTechBoost(toggled);
		if (toggled)
			NerfTech.ButtonPressed = false;
	}

	void NerfEffect_Toggled(bool toggled)
	{
		VicePatcher.SetNerfEffect(toggled);
		if (toggled)
			RemoveEffect.ButtonPressed = false;
	}

	void RemoveEffect_Toggled(bool toggled)
	{
		VicePatcher.SetRemoveEffect(toggled);
		if (toggled)
		{
			NerfEffect.ButtonPressed = false;
			AddEffects.ButtonPressed = false;
			AddEffects.Disabled = true;
		}
		else
			AddEffects.Disabled = false;
	}
	void RemoveNewEffect_Toggled(bool toggled)	
	{
		VicePatcher.SetRemoveNewEffect(toggled);
		if (toggled)
			AddEffects.ButtonPressed = false;
	}
	void PoisonTimer_Toggled(bool toggled) { VicePatcher.SetPoisonTimer(toggled); }

	void AddEffects_Toggled(bool toggled) 
	{
		VicePatcher.SetMoreEffects(toggled); 
		if (toggled)
			RemoveNewStatus.ButtonPressed = false;
	}

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
		RemoveTech.Text = Tr("RemoveTech_L");
		RemoveTech.TooltipText = Tr("RemoveTech_info");
		NerfTech.Text = Tr("NerfTech_L");
		NerfTech.TooltipText = Tr("NerfTech_info");
		RemoveEffect.Text = Tr("RemoveEffect_L");
		RemoveEffect.TooltipText = Tr("RemoveEffect_info");
		NerfEffect.Text = Tr("NerfEffect_L");
		NerfEffect.TooltipText = Tr("NerfEffect_info");
		RemoveNewStatus.Text = Tr("RemoveNewEffect_L");
		RemoveNewStatus.TooltipText = Tr("RemoveNewEffect_info");
		PoisonTimer.Text = Tr("PoisonTimer_L");
		PoisonTimer.TooltipText = Tr("PoisonTimer_info");
		AddEffects.Text = Tr("MoreEffects_L");
		AddEffects.TooltipText = Tr("MoreEffects_info");
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
		RemoveTech.Toggled += RemoveTech_Toggled;
		NerfTech.Toggled += NerfTech_Toggled;
		RemoveNewStatus.Toggled += RemoveNewEffect_Toggled;
		RemoveEffect.Toggled += RemoveEffect_Toggled;
		NerfEffect.Toggled += NerfEffect_Toggled;
		PoisonTimer.Toggled += PoisonTimer_Toggled;
		AddEffects.Toggled += AddEffects_Toggled;
	}

	public void LoadSaveData(bool BetterBattleS, bool BetterBrainsS, bool LearnMultiS, bool InsaneDamageS, bool TelepathyS, bool SkipOrdersS, bool NerfStatueS,
	bool NerfTechS, bool RemoveTechS, bool removeNewEffect, bool nerfEffect, bool removeEffect, bool poisonTimer, bool addEffects)
	{
		BetterBattle.ButtonPressed = BetterBattleS;
		BetterBrains.ButtonPressed = BetterBrainsS;
		LearnMulti.ButtonPressed = LearnMultiS;
		InsaneDamage.ButtonPressed = InsaneDamageS;
		Telepathy.ButtonPressed = TelepathyS;
		SkipOrders.ButtonPressed = SkipOrdersS;
		NerfStatue.ButtonPressed = NerfStatueS;
		NerfTech.ButtonPressed = NerfTechS;
		RemoveTech.ButtonPressed = RemoveTechS;
		RemoveNewStatus.ButtonPressed = removeNewEffect;
		RemoveEffect.ButtonPressed = removeEffect;
		NerfEffect.ButtonPressed = nerfEffect;
		PoisonTimer.ButtonPressed = poisonTimer;
		AddEffects.ButtonPressed = addEffects;
	}

	public void RestartSelection()
	{
		BetterBattle.ButtonPressed = false;
		BetterBrains.ButtonPressed = false;
		LearnMulti.ButtonPressed = false;
		InsaneDamage.ButtonPressed = false;
		Telepathy.ButtonPressed = false;
		SkipOrders.ButtonPressed = false;
		NerfStatue.ButtonPressed = false;
		NerfTech.ButtonPressed = false;
		RemoveTech.ButtonPressed = false;
		RemoveNewStatus.ButtonPressed = false;
		RemoveEffect.ButtonPressed = false;
		NerfEffect.ButtonPressed = false;
		PoisonTimer.ButtonPressed = false;
		AddEffects.ButtonPressed = false;
	}
}
