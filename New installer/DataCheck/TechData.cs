using Godot;
using System;
using System.Collections.Generic;

public partial class TechData : Control
{
	[Export] Label TechChosen;
	[Export] Label TechTitle;
	[Export] Label PowerTitle;
	[Export] Label AccTitle;
	[Export] Label StatusTitle;
	[Export] Label ChanceTitle;
	[Export] OptionButton TechOptions;
	[Export] VBoxContainer NormalTechs;
	[Export] VBoxContainer Finisher;
	[Export] ScrollContainer NormalTechsHolder;
	[Export] ScrollContainer FinisherHolder;

	List<TechInfo> allTechs;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		TechTitle.Text = Tr("Techniques_T");
		PowerTitle.Text = Tr("PowerTechCheck");
		AccTitle.Text = Tr("AccTechCheck");
		AccTitle.TooltipText = Tr("AccTechCheckInfo");
		StatusTitle.Text = Tr("StatusTechCheck");
		StatusTitle.TooltipText = Tr("StatusTechCheckInfo");
		ChanceTitle.Text = Tr("StatusChaTechCheck");
		ChanceTitle.TooltipText = Tr("StatusChaTechCheckInfo");
		TechChosen.Text = Tr("techInfoChoCheck");
		TechOptions.SetItemText(0, Tr("NormalTechCheck"));
		TechOptions.SetItemText(1, Tr("FinisherTechCheck"));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetupData(DataCheck mainParent, TechStuff parent, bool vice)
	{

		int isVice = 0;
		if (vice) isVice = 1;
		if (allTechs != null)
		{
			foreach (TechInfo tech in allTechs)
				tech.QueueFree();
			allTechs.Clear();
		}
		allTechs = new List<TechInfo>();
		for (int i = 0; i < 122; i++)
		{
			var scene = GD.Load<PackedScene>("res://Items/TechData.tscn");
			allTechs.Add(scene.Instantiate() as TechInfo);
			allTechs[i].SetupTech(mainParent.GetTechsSprites(parent.GetTechData(i).type), parent.GetTechData(i).name, parent.GetTechData(i).power,
			parent.GetTechData(i).MP, parent.GetTechData(i).accuracy, parent.GetTechData(i).effect, parent.GetTechData(i).effectChance);

			if (i < 58 - isVice)
				NormalTechs.AddChild(allTechs[i]);
			else
				Finisher.AddChild(allTechs[i]);


		}
	}

	void TechTypeSelected(int opt)
	{
		NormalTechsHolder.Visible = opt == 0;
		FinisherHolder.Visible = opt == 1;
	}

	public void RestartData()
	{
		NormalTechsHolder.Visible = false;
		FinisherHolder.Visible = false;
		TechOptions.Selected = -1;
	}
}
