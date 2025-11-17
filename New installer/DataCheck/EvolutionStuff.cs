using Godot;
using System;

public partial class EvolutionStuff : Control
{
	[Export] Button EvoActive;
	[Export] Button StatsActive;
	[Export] Button SpecialActive;
	[Export] Button FactorialActive;
	[Export] Button EvoItemsActive;
	[Export] EvoChart evoChart;
	[Export] StatGains statGains;
	[Export] SpecialEvo specialEvo;
	[Export] FactorialUp Factorial;
	[Export] Control EvoItems;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		EvoActive.Text = Tr("EvoChartCheck");
		StatsActive.Text = Tr("StatsGainsCheck");
		SpecialActive.Text = Tr("SpecialEvoCheck");
		FactorialActive.Text = Tr("FactorialCheck");
		EvoItemsActive.Text = Tr("EvoItemsCheck");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetupData(System.IO.Stream bin, System.IO.BinaryReader reader, DataCheck parent, bool vice)
	{
		statGains.SetupData(bin, reader, parent, vice);
		Factorial.SetupData(bin, parent);
		specialEvo.SetupData(bin, parent, vice);
		evoChart.SetupData(bin, parent, vice);

		this.Visible = false;
	}


	public void RestartData()
	{
		evoChart.Visible = false;
		statGains.Visible = false;
		specialEvo.Visible = false;
		Factorial.Visible = false;
		EvoItems.Visible = false;
	}

	void EvoPressed()
	{
		evoChart.Visible = true;
		statGains.Visible = false;
		specialEvo.Visible = false;
		Factorial.Visible = false;
		EvoItems.Visible = false;
	}

	void StatsPressed()
	{
		evoChart.Visible = false;
		statGains.Visible = true;
		specialEvo.Visible = false;
		Factorial.Visible = false;
		EvoItems.Visible = false;
	}

	void SpecialPressed()
	{
		evoChart.Visible = false;
		statGains.Visible = false;
		specialEvo.Visible = true;
		Factorial.Visible = false;
		EvoItems.Visible = false;
	}

	void FactorialPressed()
	{
		evoChart.Visible = false;
		statGains.Visible = false;
		specialEvo.Visible = false;
		Factorial.Visible = true;
		EvoItems.Visible = false;
	}

	void EvoItemsPressed()
	{
		evoChart.Visible = false;
		statGains.Visible = false;
		specialEvo.Visible = false;
		Factorial.Visible = false;
		EvoItems.Visible = true;
	}
}
