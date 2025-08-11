using Godot;
using System;

public partial class CupItemStuff : Control
{
	uint[] WinOffsets =
		{
			0x140A6FD6, 0x140A7024, //Grade R Reward
			0x140A70E0, 0x140A712E, //Grade D/C Reward
			0x140A71E8, 0x140A7236, //Grade B Reward
			0x140A735C, 0x140A73AA, //Grade A Reward
			0x140A74CE, 0x140A751C, 0x140A75D2, 0x140A7750, 0x140A7802, 0x140A7850, //Grade S Rewards
			0x140A7972, 0x140A79C0, //Version 1 Reward
			0x140A7A78, 0x140A7AC6, //Version 2 Reward
			0x140A7B7A, 0x140A7BC8, //Version 3 Reward
			0x140A7C88, 0x140A7CD6, //Version 4 Reward
			0x140A7D86, 0x140A7DD4, //Version 0 Reward
			0x140A7E86, 0x140A7ED4, //Fire Cup Reward 
			0x140A80BA, 0x140A8108, //Grapple Cup Reward
			0x140A81B8, 0x140A8206, //Thunder Cup Reward
			0x140A82BC, 0x140A830A, //Nature Cup Reward
			0x140A83C0, 0x140A840E, //Cool Cup Reward
			0x140A84C6, 0x140A8514, //Machine Cup Reward
			0x140A85CA, 0x140A8618, //Flith Cup Reward
			0x140A86CC, 0x140A871A, //Dino Cup Reward
			0x140A87CE, 0x140A881C, //Wing Cup Reward
			0x140A8A02, 0x140A8A50, //Animal Cup Reward
			0x140A8B04, 0x140A8B52,  //Humanoid Cup Reward
			0x140267E8, 0x14026826 //Bug tournament reward
		},
		SecondOffsets =
		{
			0x140A8C1A, 0x140A8C68, //Grade R Second prize
			0x140A8D24, 0x140A8D72, //Grade D/C Second prize
			0x140A8E3E, 0x140A8E8C, //Grade B Second prize
			0x140A8F5A, 0x140A8FA8, //Grade A Second prize
			0x140A9080, 0x140A90CE, //Grade S Second prize
			0x140A92D6, 0x140A9324, //Version 1 Second prize
			0x140A93FC, 0x140A944A, //Version 2 Second prize
			0x140A9522, 0x140A9570, //Version 3 Second prize
			0x140A9648, 0x140A9696, //Version 4 Second prize
			0x140A976E, 0x140A97BC, //Version 0 Second prize
			0x140A9890, 0x140A98DE, //Fire Cup Second prize
			0x140A99B2, 0x140A9A00, //Grapple Cup Second prize
			0x140A9C04, 0x140A9C52, //Thunder Cup Second prize
			0x140A9D26, 0x140A9D74, //Nature Cup Second prize
			0x140A9E48, 0x140A9E96, //Cool Cup Second prize
			0x140A9F6A, 0x140A9FB8, //Machine Cup Second prize
			0x140AA08E, 0x140AA0DC, //Flith Cup Second prize
			0x140AA1A8, 0x140AA1F6, //Dino Cup Second prize
			0x140AA2C2, 0x140AA310, //Wing Cup Second prize
			0x140AA3DC, 0x140AA55A, //Animal Cup Second prize
			0x140AA626, 0x140AA674,  //Humanoid Cup Second prize
			0x1402690A, 0x14026948, 0x14026A1A, 0x14026A58  //Bug tournament second prize           		
		},
		losingOffsets =
		{
			0x140A6ED4, 0x140A6F22, //City losing prize
			0x140266B0, 0x14026710  //Bug losing prize
		};

	[Export] Label PrizeChooseLabel;
	[Export] OptionButton prizeType;
	[Export] Label CupLabel;
	[Export] Label PrizeLabel;
	[Export] Label PrizeBagLabel;
	[Export] TextureRect[] tournamentPrizesIcons;
	[Export] TextureRect[] secondPrizesIcons;
	[Export] Label[] tournamentPrizes;
	[Export] Label[] secondPrizes;
	[Export] Label[] LosingPrize;
	[Export] TextureRect[] LosingPrizeIcons;
	[Export] ScrollContainer Winners;
	[Export] ScrollContainer Seconds;
	[Export] VBoxContainer Losers;
	[Export] Label GradeD;
	[Export] Label GradeC;
	[Export] Label GradeD2;
	[Export] Label GradeC2;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CupLabel.Text = Tr("CupPrizeTitle");
		PrizeLabel.Text = Tr("FirstPrizeSearch");
		PrizeBagLabel.Text = Tr("PrizeBagSearch");
		PrizeChooseLabel.Text = Tr("CupPrizeSearch");

		prizeType.SetItemText(0, Tr("FirstPrizeSearch"));
		prizeType.SetItemText(1, Tr("SecondPrizeSearch"));
		prizeType.SetItemText(2, Tr("ConsolationPrizeSearch"));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetupData(System.IO.Stream bin, DataCheck main, ItemsStuff items, bool vice)
	{
		int prize = 0;

		if (!vice)
		{
			bin.Position = 0x14D19A84;
			if (bin.ReadByte() == 0x3E)
			{
				GradeD.Text = "Grade R";
				GradeC.Text = "Grade D & C";
				GradeD2.Text = "Grade R";
				GradeC2.Text = "Grade D & C";
			}
			else
			{
				GradeD.Text = "Grade D";
				GradeC.Text = "Grade C";
				GradeD2.Text = "Grade D";
				GradeC2.Text = "Grade C";
			}
		}
		else
		{
			GradeD.Text = "Grade R";
			GradeC.Text = "Grade D & C";
			GradeD2.Text = "Grade R";
			GradeC2.Text = "Grade D & C";
		}

		for (int i = 0; i < tournamentPrizes.Length; i++)
		{
			bin.Position = WinOffsets[i];
			prize = bin.ReadByte();
			tournamentPrizes[i].Text = items.GetItemData(prize).name;
			tournamentPrizesIcons[i].Texture = main.GetItemTex(prize);
		}

		for (int i = 0; i < secondPrizes.Length; i++)
		{
			bin.Position = SecondOffsets[i];
			prize = bin.ReadByte();
			secondPrizes[i].Text = items.GetItemData(prize).name;
			secondPrizesIcons[i].Texture = main.GetItemTex(prize);
		}

		for (int i = 0; i < LosingPrize.Length; i++)
		{
			bin.Position = losingOffsets[i];
			prize = bin.ReadByte();
			LosingPrize[i].Text = items.GetItemData(prize).name;
			LosingPrizeIcons[i].Texture = main.GetItemTex(prize);
		}
	}

	void OnPrizeSelected(int prizeSelected)
	{
		Winners.Visible = prizeSelected == 0;
		Seconds.Visible = prizeSelected == 1;
		Losers.Visible = prizeSelected == 2;

		CupLabel.Text = Tr("CupPrizeTitle");
		if (prizeSelected == 0)
			PrizeLabel.Text = Tr("FirstPrizeSearch");
		else if (prizeSelected == 1)
			PrizeLabel.Text = Tr("SecondPrizeSearch");
		else
			PrizeLabel.Text = Tr("ConsolationPrizeSearch");
	}

	public void RestartData()
	{
		Winners.Visible = false;
		Seconds.Visible = false;
		Losers.Visible = false;
		prizeType.Selected = -1;
	}
}
