using Godot;
using System.Collections.Generic;

public partial class EvoChart : Control
{
	[Export] Control BigChart;
	[Export] Control SmallChart;
	[Export] TextureButton[] Digimons;
	[Export] Panel[] PreEvo;
	[Export] Panel[] PreEvo2;
	[Export] Panel[] Evo;
	[Export] Panel[] Evo2;
	[Export] TextureRect[] PreEvoIcon;
	[Export] TextureRect[] PreEvo2Icon;
	[Export] TextureRect[] EvoIcon;
	[Export] TextureRect[] Evo2Icon;
	[Export] TextureRect Selected;
	[Export] Control PreEvos;
	[Export] Control PreEvos2;
	[Export] Control Evos;
	[Export] Control Evos2;
	[Export] Label DigiName;
	[Export] Label DigiLevel;
	[Export] Control Extra;
	[Export] Panel Last;

	List<List<int>> preEvolutions;
	List<List<int>> evolutions;
	List<int> digiLevels;

	DataCheck parent;
	uint startOffset = 0x14D6CDF9, jumpOffset = 0x14D6CF98;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		for (int i = 0; i < Digimons.Length; i++)
		{
			int temp = i + 1;
			Digimons[i].Pressed += () =>
			{				
				OpenSmallChart(temp);
			};
		}

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public void SetupData(System.IO.Stream bin, DataCheck pa, bool vice)
	{
		if (preEvolutions != null || evolutions != null)
		{
			foreach (List<int> pre in preEvolutions)
				pre.Clear();
			preEvolutions.Clear();

			foreach (List<int> evo in evolutions)
				evo.Clear();
			evolutions.Clear();
		}

		preEvolutions = new List<List<int>>();
		evolutions = new List<List<int>>();

		parent = pa;


		for (int i = 0; i < Digimons.Length; i++)
		{
			Digimons[i].TextureNormal = parent.GetDigimonData(i + 1).digimonSprite;
		}

		preEvolutions.Add(new List<int>());
		evolutions.Add(new List<int>());

		for (int i = 1; i < 63; i++)
		{
			preEvolutions.Add(new List<int>());
			evolutions.Add(new List<int>());
			bin.Position = i * 11 + startOffset;

			if (bin.Position > jumpOffset)
				bin.Position = bin.Position + 0x130;

			for (int j = 0; j < 5; j++)			
					preEvolutions[i].Add(bin.ReadByte());
			
			if (i != 37)
				for (int j = 0; j < 6; j++)				
					evolutions[i].Add(bin.ReadByte());				
			else
			{
				for (int j = 0; j < 6; j++)
				{
					evolutions[i].Add(bin.ReadByte());
					if (j == 2)
						bin.Position = bin.Position + 0x130;
				}
			}
		}
		bin.Position = 0x14D6E9F9;
		if (digiLevels != null)
			digiLevels.Clear();
		digiLevels = new List<int>();
		for (int i = 0; i < 67; i++)
		{
			digiLevels.Add(bin.ReadByte());
			bin.Position = bin.Position + 0x33;

			if (bin.Position % 0x930 > 0x800)
				bin.Position = bin.Position + 0x130;
		}
		
		bin.Position = 0x14CEA0A8;
		if (bin.ReadByte() == 9)
		{			
			Last.Visible = true;
			Extra.Visible = true;

			bin.Position = 0x14D19D3C;
			if (vice)
				bin.Position = 0x14D19CC8;

			for (int i = 0; i < 3; i++)
			{
				preEvolutions.Add(new List<int>());
				evolutions.Add(new List<int>());
				for (int j = 0; j < 5; j++)				
					preEvolutions[i + 63].Add(bin.ReadByte());
				
			}
			if (digiLevels[63] < 5)
			{
				evolutions[63] = evolutions[62];
				evolutions[62].Clear();
			}
		}
		else
		{
			Last.Visible = false;
			Extra.Visible = false;
		}

	}

	void OpenSmallChart(int digimon)
	{
		BigChart.Visible = false;
		SmallChart.Visible = true;

		Selected.Texture = parent.GetDigimonData(digimon).digimonSprite;
		DigiName.Text = parent.GetDigimonData(digimon).name;
		DigiLevel.Text = GetLevelName(digiLevels[digimon]);

		int evoCount = 0;
		foreach (int evo in preEvolutions[digimon])
		{
			if (evo != 0xFF)
				evoCount++;
		}

		if (evoCount % 2 == 1)
			{
				PreEvos.Visible = true;
				PreEvos2.Visible = false;
				for (int i = 0; i < PreEvo.Length; i++)
				{
					if (preEvolutions[digimon][i] != 0xFF)
					{
						PreEvo[i].Visible = true;
						PreEvoIcon[i].Texture = parent.GetDigimonData(preEvolutions[digimon][i]).digimonSprite;
					}
					else
						PreEvo[i].Visible = false;
				}
			}
			else
			{
				if (evoCount > 0)
				{
					PreEvos.Visible = false;
					PreEvos2.Visible = true;
					for (int i = 0; i < 4; i++)
					{
						if (preEvolutions[digimon][i] != 0xFF)
						{
							PreEvo2[i].Visible = true;
							PreEvo2Icon[i].Texture = parent.GetDigimonData(preEvolutions[digimon][i]).digimonSprite;
						}
						else
							PreEvo2[i].Visible = false;
					}
				}
				else
				{
					PreEvos.Visible = false;
					PreEvos2.Visible = false;
				}
			}

		evoCount = 0;
		foreach (int evo in evolutions[digimon])
		{
			if (evo != 0xFF)
				evoCount++;
		}

		if (evoCount % 2 == 1)
		{
			Evos.Visible = true;
			Evos2.Visible = false;
			for (int i = 0; i < 5; i++)
			{
				if (evolutions[digimon][i] != 0xFF)
				{
					Evo[i].Visible = true;
					EvoIcon[i].Texture = parent.GetDigimonData(evolutions[digimon][i]).digimonSprite;
				}
				else
					Evo[i].Visible = false;
			}
		}
		else
		{
			if (evoCount > 0)
			{
				Evos.Visible = false;
				Evos2.Visible = true;
				for (int i = 0; i < 6; i++)
				{
					if (evolutions[digimon][i] != 0xFF)
					{
						Evo2[i].Visible = true;
						Evo2Icon[i].Texture = parent.GetDigimonData(evolutions[digimon][i]).digimonSprite;
					}
					else
						Evo2[i].Visible = false;
				}
			}
			else
			{
				Evos.Visible = false;
				Evos2.Visible = false;
			}
		}
	}

	void CloseSmallChart()
	{
		SmallChart.Visible = false;
		BigChart.Visible = true;
	}

	string GetLevelName(int level)
	{
		switch (level)
		{	
			case 1:
				return "BABY";
			case 2:
				return "IN-TRAINING";
			case 3:
				return "ROOKIE";
			case 4:
				return "CHAMPION";
			case 5:
				return "ULTIMATE";
			default:
				return "UNDEFINED";
		}
	}
}
