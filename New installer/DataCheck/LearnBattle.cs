using Godot;
using System;

public partial class LearnBattle : Control
{
	[Export] Label TechTitle;
	[Export] Label ChanceTitle;
	[Export] Label Type1;
	[Export] Label Type2;
	[Export] Label Type3;
	[Export] Label SelectedTitle;
	[Export] OptionButton SelectedData;
	[Export] Control LastTech;
	[Export] Control LearnTech;
	[Export] Control TechOrder;
	[Export] Control GivenTechs;
	[Export] TextureRect[] Types;
	[Export] Label[] Names;
	[Export] Label[] Chance1;
	[Export] Label[] Chance2;
	[Export] Label[] Chance3;
	[Export] Label[] TechOrderLabels;
	[Export] Label[] TechLearnOrder;
	[Export] Label TechOrderTitle;
	[Export] Label[] TechOrderType;
	[Export] TextureRect[] TypesOrder;
	[Export] TextureRect[] TypesGiven;
	[Export] Label TechGivenTitle;
	[Export] Label[] TechGivenLabels;
	[Export] Label[] TechGivenDescriptions;
	[Export] Control[] TechsGivenVice;
	[Export] Label TechGivenDescTitle;

	private int[] chancesBattle = new int[174];
	private int[] chanceBrain = new int[24];
	private int[] orderBrain = new int[56];

	private uint[] offsetsTechsGiven = { 0x14029E3F, 0x13FE219B, 0x13FE21E5, 0x13FE2233, 0x140109F3, 0x1402D473, 0x1402D4D1, 0x1402D52D };

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		for (int i = 0; i < TechOrderLabels.Length; i++)
		{
			TechOrderLabels[i].Text = Tr("TechOrder" + i);
		}
		for (int i = 0; i < TechOrderType.Length; i++)
		{
			TechOrderType[i].Text = Tr("TechOrderType" + i);
		}

		for (int i = 0; i < TechOrderLabels.Length; i++)
		{
			TechGivenDescriptions[i].Text = Tr("TechGivenDesc" + i);
		}

		TechOrderTitle.Text = Tr("TechOrderTitle");
		TechGivenTitle.Text = Tr("Techniques_T");
		TechTitle.Text = Tr("Techniques_T");
		ChanceTitle.Text = Tr("ChanceLearnTechCheck");
		TechGivenDescTitle.Text = Tr("TechGivenDescT");
		Type1.Text = Tr("Type1LearnCheck");
		Type2.Text = Tr("Type2LearnCheck");
		Type3.Text = Tr("Type3LearnCheck");

		SelectedData.SetItemText(0, Tr("LearnBattleCheck"));
		SelectedData.SetItemText(1, Tr("LearnBrainsCheck"));
		SelectedData.SetItemText(2, Tr("LearnOrderCheck"));
		SelectedData.SetItemText(3, Tr("LearnGivenCheck"));
		
		SelectedTitle.Text = Tr("LearnTechOptionsCheck");

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetupData(System.IO.Stream bin, DataCheck mainParent, TechStuff parent, bool vice)
	{
		//battle chances
		bin.Position = 0x14D66A2C;
		for (int i = 0; i < 174; i++)
		{
			chancesBattle[i] = bin.ReadByte();
			if (bin.Position == 0x14D66A88)
				bin.Position = bin.Position + 0x130;
		}
		bin.Position = 0x14C8E58C;
		for (int i = 0; i < 24; i++)
			chanceBrain[i] = bin.ReadByte();

		bin.Position = 0x14C8E554;
		for (int i = 0; i < 56; i++)
			orderBrain[i] = bin.ReadByte();

		for (int i = 0; i < TechLearnOrder.Length; i++)
		{
			TechLearnOrder[i].Text = parent.GetTechData(orderBrain[i]).name;
		}

		for (int i = 0; i < TypesOrder.Length; i++)
		{
			TypesOrder[i].Texture = mainParent.GetTechsSprites(i);
		}

		for (int i = 0; i < Names.Length; i++)
		{
			Names[i].Text = parent.GetTechData(i).name;
			Types[i].Texture = mainParent.GetTechsSprites(parent.GetTechData(i).type);
			if (i == 48 || i == 57)
				Names[i].Text = Names[i].Text + " V2";
		}
		for (int i = 0; i < 4; i++)
		{
			bin.Position = offsetsTechsGiven[i];
			int techValue = bin.ReadByte();
			TechGivenLabels[i].Text = parent.GetTechData(techValue).name;
			TypesGiven[i].Texture = mainParent.GetTechsSprites(parent.GetTechData(techValue).type);
		}
		if (vice)
		{
			LastTech.Visible = false;
			for (int i = 4; i < 8; i++)
			{
				bin.Position = offsetsTechsGiven[i];
				int techValue = bin.ReadByte();
				TechGivenLabels[i].Text = parent.GetTechData(techValue).name;
				TypesGiven[i].Texture = mainParent.GetTechsSprites(parent.GetTechData(techValue).type);
				TechsGivenVice[i - 4].Visible = true;
			}
		}
		else
		{
			for (int i = 0; i < TechsGivenVice.Length; i++)
				TechsGivenVice[i].Visible = false;

		}
	}

	void OptionSelected(int option)
	{
		if (option < 2)
		{
			LearnTech.Visible = true;
			SetTechLearnChances(option == 1);
		}
		else
			LearnTech.Visible = false;
		TechOrder.Visible = option == 2;
		GivenTechs.Visible = option == 3;
	}

	void SetTechLearnChances(bool brains)
	{
		if (!brains)
		{
			for (int i = 0; i < 58; i++)
			{
				Chance1[i].Text = chancesBattle[i * 3].ToString() + "%";
				Chance2[i].Text = chancesBattle[i * 3 + 1].ToString() + "%";
				Chance3[i].Text = chancesBattle[i * 3 + 2].ToString() + "%";
			}
		}
		else
		{
			int extraTech = 0;
			for (int i = 0; i < 48; i++)
			{
				int currentTech = orderBrain[i];
				Chance1[currentTech].Text = chanceBrain[i % 8 * 3].ToString() + "%";
				Chance2[currentTech].Text = chanceBrain[i % 8 * 3 + 1].ToString() + "%";
				Chance3[currentTech].Text = chanceBrain[i % 8 * 3 + 2].ToString() + "%";
				if (currentTech == 44)
					extraTech = i;
			}
			Chance1[48].Text = chanceBrain[extraTech % 8 * 3].ToString() + "%"; ;
			Chance2[48].Text = chanceBrain[extraTech % 8 * 3 + 1].ToString() + "%";
			Chance3[48].Text = chanceBrain[extraTech % 8 * 3 + 2].ToString() + "%";

			for (int i = 0; i < 8; i++)
			{
				int currentTech = orderBrain[i + 48];
				Chance1[currentTech].Text = chanceBrain[i * 3].ToString() + "%";
				Chance2[currentTech].Text = chanceBrain[i * 3 + 1].ToString() + "%";
				Chance3[currentTech].Text = chanceBrain[i * 3 + 2].ToString() + "%";
				if (currentTech == 55)
					extraTech = i;
			}
			Chance1[57].Text = chanceBrain[extraTech * 3].ToString() + "%"; ;
			Chance2[57].Text = chanceBrain[extraTech * 3 + 1].ToString() + "%";
			Chance3[57].Text = chanceBrain[extraTech * 3 + 2].ToString() + "%";

		}
	}

	public void RestartData()
	{
		LearnTech.Visible = false;
		TechOrder.Visible = false;
		GivenTechs.Visible = false;
		SelectedData.Selected = -1;
	}
}
