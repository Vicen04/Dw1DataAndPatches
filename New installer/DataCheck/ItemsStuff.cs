using Godot;
using System;
using System.IO;
using System.Collections.Generic;

public partial class ItemsStuff : Control
{
	public class ItemData
	{
		public string name { get; set; }
		public int price { get; set; }
		public short merit { get; set; }
	}

	[Export] Button Spawns;
	[Export] Button Drops;
	[Export] Button Chests;
	[Export] Button Tournaments;
	[Export] Button Others;
	[Export] Control SpawnsCheck;
	[Export] Control DropsCheck;
	[Export] Control ChestsCheck;
	[Export] Control TournamentsCheck;
	[Export] Control OthersCheck;

	private ItemData[] items = new ItemData[128];

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetupData(System.IO.Stream bin, BinaryReader reader, DataCheck parent, bool vice)
	{

		uint NamesOffset = 0x14D676C4, currentOffset = NamesOffset;
		uint[] jumpOffsets = { 0x14D67CE8, 0x14D68618, 0x79999999 };
		int jumpValue = 0;

		for (int i = 0; i < 128; i++)
		{
			items[i] = new ItemData();
			string currentName = System.Text.Encoding.Default.GetString(reader.ReadBytes(4));
			currentOffset = currentOffset + 0x20;
			if (currentOffset > jumpOffsets[jumpValue])
			{
				currentOffset = currentOffset + 0x130;
				bin.Position = bin.Position + 0x130;
				jumpValue++;
			}
			currentName = currentName + System.Text.Encoding.Default.GetString(reader.ReadBytes(7));
			bin.Position = currentOffset;
			items[i].name = currentName;
		}

		uint pricesOffset = 0x14D676D8;
		currentOffset = pricesOffset;
		jumpValue = 0;

		bin.Position = pricesOffset;


		for (int i = 0; i < 128; i++)
		{
			int value = reader.ReadInt32();
			short merit = reader.ReadInt16();
			currentOffset = currentOffset + 0x20;
			if (currentOffset > jumpOffsets[jumpValue])
			{
				currentOffset = currentOffset + 0x130;
				jumpValue++;
			}
			bin.Position = currentOffset;
			items[i].price = value;
			items[i].merit = merit;
		}
	}

	public ItemData GetItemData(int value) { return items[value]; }

	public void RestartData()
	{
		Spawns.SetPressedNoSignal(false);
		Drops.SetPressedNoSignal(false);
		Chests.SetPressedNoSignal(false);
		Tournaments.SetPressedNoSignal(false);
		Others.SetPressedNoSignal(false);
		Spawns.Disabled = false;
		Drops.Disabled = false;
		Chests.Disabled = false;
		Tournaments.Disabled = false;
		Others.Disabled = false;
		SpawnsCheck.Visible = false;
		DropsCheck.Visible = false;
		ChestsCheck.Visible = false;
		TournamentsCheck.Visible = false;
		OthersCheck.Visible = false;
	}

	/*bool CheckIfECC(int position)
	{		
		position = position - 24;
		position = position % 0x930;

		if (position >= 0x800)
		{
			return true;
		}
		return false;
	}*/
}
