using Godot;
using System;
using System.Collections.Generic;

public partial class TournamentsData : Control
{
	[Export] Control Schedule;
	[Export] Control GradeCups;
	[Export] Control VersionCups;
	[Export] Control TypeCups;
	[Export] Control SpecialCups;
	[Export] Label OptionsTitle;
	[Export] OptionButton Options;
	[Export] HBoxContainer scheduleList;
	[Export] Label daysTitle;
	[Export] Label[] cupsTitle;
	[Export] Label[] cupsNames;
	[Export] HBoxContainer[] registerLists;
	[Export] HBoxContainer[] participantLists;
	[Export] ScrollContainer[] registerCont;
	[Export] ScrollContainer[] participantCont;
	[Export] Button[] Register;
	[Export] Button[] Participants;
	uint[] tournamentPtr = { 0x140A49FC, 0x140A4AFA, 0x140A4BFC, 0x140A4CFE, 0x140A4E00, 0x140A4EFA, 0x140A502E, 0x140A529A, 0x140A53D6, 0x140A551E, 0x140A5660, 0x140A57C6,
								  0x140A5900, 0x140A5A4A, 0x140A5CC8, 0x140A5DFA, 0x140A5F46, 0x140A6080, 0x140A61B8, 0x140A62E8, 0x140A652A, 0x140A665E, 0x140A677E },
	joinPtr = { 0x140A49F8, 0x140A4AF6, 0x140A4BF8, 0x140A4CFA, 0x140A4DFC, 0x140A4EF6, 0x140A502A, 0x140A5296, 0x140A53D2, 0x140A551A, 0x140A565C, 0x140A57C2,
				0x140A58FC, 0x140A5A46, 0x140A5CC4, 0x140A5DF6, 0x140A5F42, 0x140A607C, 0x140A61B4, 0x140A62E4, 0x140A6526, 0x140A665A, 0x140A677A };
	uint schedule = 0x14D72510, digimonStart = 0x140A64C8;
	int currentCup = 0;
	List<DaySchedule> allDays;
	List<DigimonTournament> allJoining;
	List<DigimonTournament> allParticipants;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		daysTitle.Text = Tr("DaysCheck");
		Options.SetItemText(0, Tr("GradeCupsheck"));
		Options.SetItemText(1, Tr("VersionCupsCheck"));
		Options.SetItemText(2, Tr("TypeCupsCheck"));
		Options.SetItemText(3, Tr("SpecialCupsCheck"));
		Options.SetItemText(4, Tr("ScheduleCheck"));
		cupsTitle[0].Text = Tr("GradeCupsheck");
		cupsTitle[1].Text = Tr("VersionCupsCheck");
		cupsTitle[2].Text = Tr("TypeCupsCheck");
		cupsTitle[3].Text = Tr("SpecialCupsCheck");
		OptionsTitle.Text = Tr("LearnTechOptionsCheck");
		for (int i = 0; i < cupsNames.Length; i++)
		{
			cupsNames[i].Text = ReturnCup(i);
			cupsNames[i].TooltipText = ReturnCupName(i);
		}
		for (int i = 0; i < Participants.Length; i++)
		{
			Register[i].Text = Tr("TourneyRegCheck");
			Register[i].Disabled = true;
			registerCont[i].Visible = true;

			Participants[i].Text = Tr("TourneyPartCheck");
			Participants[i].Disabled = false;
			participantCont[i].Visible = false;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public void SetupTournaments(System.IO.Stream bin, System.IO.BinaryReader reader, DataCheck parent, bool vice)
	{
		currentCup = -1;
		Options.Selected = -1;
		if (allDays != null)
		{
			foreach (DaySchedule daySchedule in allDays)
				daySchedule.QueueFree();
			allDays.Clear();
		}
		allDays = new List<DaySchedule>();

		if (allJoining != null)
		{
			foreach (DigimonTournament digi in allJoining)
				digi.QueueFree();
			allJoining.Clear();
		}
		allJoining = new List<DigimonTournament>();

		if (allParticipants != null)
		{
			foreach (DigimonTournament digi in allParticipants)
				digi.QueueFree();
			allParticipants.Clear();
		}
		allParticipants = new List<DigimonTournament>();


		bin.Position = schedule;
		for (int i = 0; i < 30; i++)
		{
			var scene = GD.Load<PackedScene>("res://Items/DaySchedule.tscn");
			allDays.Add(scene.Instantiate() as DaySchedule);
			allDays[i].SetupDay(i, bin.ReadByte(), bin.ReadByte(), bin.ReadByte(), bin.ReadByte(), bin.ReadByte(), bin.ReadByte(), this);
			scheduleList.AddChild(allDays[i]);
		}

		for (int i = 0; i < joinPtr.Length; i++)
		{
			bin.Position = joinPtr[i];
			int pos = reader.ReadInt16() % 0x800;
			bin.Position = digimonStart + pos + 1;
			List<int> digimons = new List<int>();
			while (true)
			{
				int digimon = bin.ReadByte();
				if (digimon > 66)
					break;
				else if (digimon == 0)
					continue;
				digimons.Add(digimon);
			}
			var scene = GD.Load<PackedScene>("res://Items/DigimonTournament.tscn");
			allJoining.Add(scene.Instantiate() as DigimonTournament);
			allJoining[i].Setup(digimons, parent, vice, false);
			if (i < 6)
				registerLists[0].AddChild(allJoining[i]);
			else if (i < 11)
				registerLists[1].AddChild(allJoining[i]);
			else if (i < 18)
				registerLists[2].AddChild(allJoining[i]);
			else
				registerLists[3].AddChild(allJoining[i]);
		}

		for (int i = 0; i < tournamentPtr.Length; i++)
		{
			bin.Position = tournamentPtr[i];
			int pos = reader.ReadInt16() % 0x800;
			bin.Position = digimonStart + pos + 2;
			List<int> digimons = new List<int>();
			while (true)
			{
				int digimon = bin.ReadByte();
				if (digimon > 253)
					break;
				digimons.Add(digimon);
			}
			var scene = GD.Load<PackedScene>("res://Items/DigimonTournament.tscn");
			allParticipants.Add(scene.Instantiate() as DigimonTournament);
			allParticipants[i].Setup(digimons, parent, vice, true);
			if (i < 6)
				participantLists[0].AddChild(allParticipants[i]);
			else if (i < 11)
				participantLists[1].AddChild(allParticipants[i]);
			else if (i < 18)
				participantLists[2].AddChild(allParticipants[i]);
			else
				participantLists[3].AddChild(allParticipants[i]);
		}
	}

	void SetOption(int option)
	{
		GradeCups.Visible = option == 0;
		VersionCups.Visible = option == 1;
		TypeCups.Visible = option == 2;
		SpecialCups.Visible = option == 3;
		Schedule.Visible = option == 4;
		currentCup = option;
	}

	void OnRegisterPressed()
	{
		registerCont[currentCup].Visible = true;
		participantCont[currentCup].Visible = false;
		Register[currentCup].Disabled = true;
		Participants[currentCup].Disabled = false;
	}

	void OnParticipantPressed()
	{
		registerCont[currentCup].Visible = false;
		participantCont[currentCup].Visible = true;
		Register[currentCup].Disabled = false;
		Participants[currentCup].Disabled = true;
	}

	public string ReturnCup(int cup)
	{
		switch (cup)
		{
			case 0:
				return "D";
			case 1:
				return "C";
			case 2:
				return "B";
			case 3:
				return "A";
			case 4:
				return "S";
			case 5:
				return "R";
			case 6:
				return "H";
			case 7:
				return "I";
			case 8:
				return "J";
			case 9:
				return "K";
			case 10:
				return "L";
			case 11:
				return "F";
			case 12:
				return "G";
			case 13:
				return "Wt";
			case 14:
				return "N";
			case 15:
				return "O";
			case 16:
				return "M";
			case 17:
				return "T";
			case 18:
				return "Y";
			case 19:
				return "Ww";
			case 20:
				return "Z";
			case 21:
				return "X";
			case 22:
				return "Bug";
			default:
				return "";
		}
	}

	public string ReturnCupName(int cup)
	{
		switch (cup)
		{
			case 0:
				return "Grade D";
			case 1:
				return "Grade C";
			case 2:
				return "Grade B";
			case 3:
				return "Grade A";
			case 4:
				return "Grade S";
			case 5:
				return "Grade R";
			case 6:
				return "Version 1";
			case 7:
				return "Version 2";
			case 8:
				return "Version 3";
			case 9:
				return "Version 4";
			case 10:
				return "Version 0";
			case 11:
				return "Fire Cup";
			case 12:
				return "Grapple Cup";
			case 13:
				return "Thunder Cup";
			case 14:
				return "Nature Cup";
			case 15:
				return "Cool Cup";
			case 16:
				return "Mech Cup";
			case 17:
				return "Filth Cup";
			case 18:
				return "Dino Cup";
			case 19:
				return "Wing Cup";
			case 20:
				return "Animal Cup";
			case 21:
				return "Human Cup";
			case 22:
				return "Bug Tournament";
			default:
				return "";
		}
	}

	public void RestartData()
	{
		for (int i = 0; i < Participants.Length; i++)
		{
			Register[i].Disabled = true;
			registerCont[i].Visible = true;

			Participants[i].Disabled = false;
			participantCont[i].Visible = false;
		}

		GradeCups.Visible = false;
		VersionCups.Visible = false;
		TypeCups.Visible = false;
		SpecialCups.Visible = false;
		Schedule.Visible = false;
		currentCup = -1;
		Options.Selected = -1;
	}
}
