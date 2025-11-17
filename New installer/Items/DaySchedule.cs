using Godot;
using System;

public partial class DaySchedule : Control
{
	[Export] Label Day;
	[Export] Label Cup1;
	[Export] Label Cup2;
	[Export] Label Cup3;
	[Export] Label Cup4;
	[Export] Label Cup5;
	[Export] Label Cup6;
	// Called when the node enters the scene tree for the first time.
	public void SetupDay(int day, int first, int second, int third, int fourth, int fifth, int sixth, TournamentsData data)
	{
		Day.Text = (day + 1).ToString();
		Cup1.Text = data.ReturnCup(first);
		Cup1.TooltipText = data.ReturnCupName(first);
		Cup2.Text = data.ReturnCup(second);
		Cup2.TooltipText = data.ReturnCupName(second);
		Cup3.Text = data.ReturnCup(third);
		Cup3.TooltipText = data.ReturnCupName(third);
		Cup4.Text = data.ReturnCup(fourth);
		Cup4.TooltipText = data.ReturnCupName(fourth);
		Cup5.Text = data.ReturnCup(fifth);
		Cup5.TooltipText = data.ReturnCupName(fifth);
		Cup6.Text = data.ReturnCup(sixth);
		Cup6.TooltipText = data.ReturnCupName(sixth);

	}	
}
