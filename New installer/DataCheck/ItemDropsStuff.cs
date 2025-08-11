using Godot;
using System;
using System.Collections.Generic;

public partial class ItemDropsStuff : Control
{

	[Export] OptionButton currentOption;
	[Export] Label CurrentOptionLabel;
	[Export] VBoxContainer PlayableDigimonContainer;
	[Export] VBoxContainer NPCDigimonContainer;

	[Export] ScrollContainer Playables;
	[Export] ScrollContainer NPCs;

	[Export] Label ItemLabel;
	[Export] Label ChanceLabel;

	DigimonDropsSetup[] playableDigimon = new DigimonDropsSetup[66];
	DigimonDropsSetup[] NPCDigimon = new DigimonDropsSetup[180 - 66];
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Playables.Visible = true;
		NPCs.Visible = false;
		currentOption.Selected = 0;
		CurrentOptionLabel.Text = Tr("OptionDropsCheckLabel");
		ItemLabel.Text = Tr("ItemCheckLabel");
		ChanceLabel.Text = Tr("ChanceCheckLabel");
		currentOption.SetItemText(0, Tr("PlayableDrop"));
		currentOption.SetItemText(1, Tr("NOPlayableDrop"));
	}

	public void SetupData(DataCheck main, ItemsStuff parent)
	{
		if (playableDigimon[0] != null)
		{
			for (int i = 0; i < playableDigimon.Length; i++)
				playableDigimon[i].QueueFree();
		}

		if (NPCDigimon[0] != null)
		{
			for (int i = 0; i < NPCDigimon.Length; i++)
				NPCDigimon[i].QueueFree();
		}

		for (int i = 0; i < playableDigimon.Length; i++)
		{
			var scene = GD.Load<PackedScene>("res://Items/digimonDrops.tscn");
			playableDigimon[i] = scene.Instantiate() as DigimonDropsSetup;
			if (main.GetDigimonData(i).itemID != 0xFF)
				playableDigimon[i].Setup(main.GetDigimonData(i).name, main.GetDigimonData(i).digimonSprite, main.GetItemTex(main.GetDigimonData(i).itemID),
			parent.GetItemData(main.GetDigimonData(i).itemID).name, main.GetDigimonData(i).itemChance);
			else
				playableDigimon[i].Setup(main.GetDigimonData(i).name, main.GetDigimonData(i).digimonSprite, null, "", 0);
			PlayableDigimonContainer.AddChild(playableDigimon[i]);
		}

		for (int i = 66; i < 180; i++)
		{
			var scene = GD.Load<PackedScene>("res://Items/digimonDrops.tscn");
			NPCDigimon[i - 66] = scene.Instantiate() as DigimonDropsSetup;
			NPCDigimon[i - 66].Setup(main.GetDigimonData(i).name, main.GetDigimonData(i).digimonSprite, main.GetItemTex(main.GetDigimonData(i).itemID),
			parent.GetItemData(main.GetDigimonData(i).itemID).name, main.GetDigimonData(i).itemChance);
			NPCDigimonContainer.AddChild(NPCDigimon[i - 66]);
		}
	}

	void TypeSelected(int option)
	{
		Playables.Visible = option == 0;
		NPCs.Visible = option == 1;
	}

	public void RestartData()
	{
		Playables.Visible = true;
		NPCs.Visible = false;
		currentOption.Selected = 0;
	}
}
