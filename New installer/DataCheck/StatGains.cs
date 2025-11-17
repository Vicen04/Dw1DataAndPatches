using Godot;
using System;
using System.Collections.Generic;

public partial class StatGains : Control
{
	[Export] VBoxContainer Digimons;
	[Export] VBoxContainer Special;
	[Export] Control NormalStat;
	[Export] Control SpecialStat;
	[Export] OptionButton StatSelected;
	[Export] Label StatSelectedLabel;
	[Export] Label AllStatsLabel;
	[Export] Label GainsLabel;

	List<StatGainsDigimon> stats;
	List<SpecialGains> statsSpe;
	uint offset = 0x14D6CA68;

	int[] exceptions = new int[12] { 0, 1, 2, 11, 15, 16, 29, 30, 43, 44, 53, 0 };
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		StatSelectedLabel.Text = Tr("StatSelectedCheck");
		AllStatsLabel.Text = Tr("AllStatsCheck");
		GainsLabel.Text = Tr("GainsLabelCheck");
		StatSelected.SetItemText(0, Tr("NormalStatsCheck"));
		StatSelected.SetItemText(1, Tr("SpecialStatsCheck"));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetupData(System.IO.Stream bin, System.IO.BinaryReader reader, DataCheck parent, bool vice)
	{
		if (vice)
			exceptions[0] = 0;
		else
			exceptions[0] = 6;

		bin.Position = 0x14B97048;
		exceptions[11] = reader.ReadInt16();
		if (stats != null)
		{
			foreach (StatGainsDigimon stat in stats)
				stat.QueueFree();
			stats.Clear();
		}
		stats = new List<StatGainsDigimon>();

		if (statsSpe != null)
		{
			foreach (SpecialGains stat in statsSpe)
				stat.QueueFree();
			statsSpe.Clear();
		}
		statsSpe = new List<SpecialGains>();

		int currentExc = 3;

		int count = 0;
		for (int i = 3; i < 66; i++)
		{
			bin.Position = i * 14 + offset;
			if (exceptions[0] != i && exceptions[11] != i && exceptions[currentExc] != i)
			{
				var scene = GD.Load<PackedScene>("res://Items/stat_gains_digimon.tscn");
				stats.Add(scene.Instantiate() as StatGainsDigimon);
				stats[count].Setup(parent.GetDigimonData(i).digimonSprite, parent.GetDigimonData(i).name,
				reader.ReadInt16(), reader.ReadInt16(), reader.ReadInt16(), reader.ReadInt16(), reader.ReadInt16(), reader.ReadInt16());
				Digimons.AddChild(stats[count]);
				count++;
			}
			else if (exceptions[currentExc] == i && currentExc < 11)
			{
				currentExc++;
			}
		}
		count = 0;
		for (int i = 0; i < exceptions.Length; i++)
		{
			if (exceptions[i] == 0)
			{
				count++;
				continue;
			}
			bin.Position = exceptions[i] * 14 + offset + 10;
			var scene = GD.Load<PackedScene>("res://Items/SpecialGains.tscn");
			statsSpe.Add(scene.Instantiate() as SpecialGains);
			int brains = reader.ReadInt16();
			statsSpe[i - count].Setup(parent.GetDigimonData(exceptions[i]).digimonSprite, parent.GetDigimonData(exceptions[i]).name, brains, (brains - 10) * 10);
			Special.AddChild(statsSpe[i - count]);
		}
	}

	void TypeSelected(int sel)
	{
		NormalStat.Visible = sel == 0;
		SpecialStat.Visible = sel == 1;
	}

	void RestartData()
	{
		NormalStat.Visible = false;
		SpecialStat.Visible = false;
	}
}
