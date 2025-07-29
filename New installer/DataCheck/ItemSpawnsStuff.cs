using Godot;
using System;
using System.Collections.Generic;

public partial class ItemSpawnsStuff : Control
{
	class MapData
	{
		public List<uint> offsets { get; set; }
		public List<uint> chanceOffsets { get; set; }
		public string mapName { get; set; }
		public List<int> itemIds { get; set; }
		public List<int> itemChances { get; set; }

		public MapData(List<uint> itemOffset, List<uint> chancesOff, string name)
		{
			offsets = itemOffset;
			chanceOffsets = chancesOff;
			mapName = name;
		}
	}

	class AreasData
	{
		public List<MapData> maps { get; set; }
		public string AreaName { get; set; }

		public AreasData(List<MapData> mapList, string name)
		{
			maps = mapList;
			AreaName = name;
		}
	}
	[Export] OptionButton Areas;
	[Export] OptionButton Maps;
	[Export] TextureRect[] ItemIcons;
	[Export] Label[] ItemNames;
	[Export] Label[] ItemChances;
	[Export] Control[] Items;
	[Export] VBoxContainer ItemsContainer;
	[Export] VBoxContainer CentaItems;
	[Export] VBoxContainer CentaItems2;
	[Export] TextureRect[] CentaItemIcons;
	[Export] Label[] CentaItemNames;
	[Export] Label[] CentaItemChances;

	List<AreasData> allAreas;

	DataCheck mainParent;
	ItemsStuff parent;
	int areaSelected;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetUpData(System.IO.Stream bin, DataCheck main, ItemsStuff items, bool vice)
	{
		allAreas = new List<AreasData>();

		List<uint> mayo04AC = [0x13FDF0E5, 0x13FDF0FB, 0x13FDF111, 0x13FDF127];
		List<uint> mayo04A = [0x13FDF0ED, 0x13FDF103, 0x13FDF119, 0x13FDF12F];
		List<uint> gias6A = [0x14010701, 0x14010717];
		List<uint> gias6AC = [0x140106F9, 0x1401070F];
		if (vice)
		{
			mayo04AC = [0x13FDF0E9, 0x13FDF0FB, 0x13FDF111];
			mayo04A = [0x13FDF0F1, 0x13FDF103, 0x13FDF119];
			gias6A = [0x14010709, 0x1401071F];
			gias6AC = [0x14010701, 0x14010717];
		}

		bin.Position = 0x13FFCEA8;

		List<uint> gcan09 = [0x13FFCEA9, 0x13FFCEBF, 0x13FFCED5];
		List<uint> gcan09C = [0x13FFCEA1, 0x13FFCEB7, 0x13FFCECD];

		if (bin.ReadByte() != 116)
		{
			gcan09 = [0x13FFCEAD, 0x13FFCEC3, 0x13FFCED9];
			gcan09C = [0x13FFCEA5, 0x13FFCEBB, 0x13FFCED1];
		}


		allAreas.AddRange(
			[new AreasData(
				[
					new MapData([0x1402B6FD, 0x1402B713, 0x1402B729], [0x1402B6F5, 0x1402B70B, 0x1402B721], "MAYO00"),
					new MapData([0x13FDD565, 0x13FDD57B, 0x13FDD591, 0x13FDD597, 0x13FDD59D, 0x13FDD5B3, 0x13FDD5C9], [0x13FDD55D, 0x13FDD573, 0x13FDD589, 0x13FDD589, 0x13FDD589, 0x13FDD5AB, 0x13FDD5C1], "MAYO01"),
					new MapData([0x1402C92D, 0x1402C943, 0x1402C959], [0x1402C925, 0x1402C93B, 0x1402C951], "MAYO01_2"),
					new MapData([0x13FDDE89, 0x13FDDE9F], [0x13FDDE81, 0x13FDDE97], "MAYO02"),
					new MapData([0x1402D25D, 0x1402D273, 0x1402D289], [0x1402D255, 0x1402D26B, 0x1402D281], "MAYO02_2"),
					new MapData([0x13FDE7B5, 0x13FDE7CB, 0x13FDE7E1], [0x13FDE7AD, 0x13FDE7C3, 0x13FDE7D9], "MAYO03"),
					new MapData([0x13FDE7B5, 0x13FDE7CB, 0x13FDE7E1], [0x13FDE7AD, 0x13FDE7C3, 0x13FDE7D9], "MAYO03"),
					new MapData(mayo04A, mayo04AC, "MAYO04A")
				]
				, "Native Forest"),
				new AreasData([new MapData([0x13FE0349, 0x13FE035F, 0x13FE0375, 0x13FE038B], [0x13FE0341, 0x13FE0357, 0x13FE036D, 0x13FE0383], "MAYO05")], "Coela Point"),
				new AreasData(
				[
					new MapData([0x13FE1599], [0x13FE1591], "MAYO06"),
					new MapData([0x13FE3135, 0x13FE314B], [0x13FE312D, 0x13FE3143], "MAYO10")
				]
				, "Dragon Eye Lake"),
				new AreasData(
				[
					new MapData([0x13FE43D9, 0x13FE43EF], [0x13FE43D1, 0x13FE43E7], "MAYO08A"),
					new MapData([0x13FE563D, 0x13FE5653], [0x13FE5635, 0x13FE564B], "MAYO08B")
				]
				, "Digimon Bridge"),
				new AreasData([new MapData([0x13FE2801], [0x13FE27F9], "MAYO11")], "Drill Tunnel Entrance"),
				new AreasData(
				[
					new MapData([0x13FE5F29, 0x13FE5F3F], [0x13FE5F21, 0x13FE5F37], "TROP00"),
					new MapData([0x13FE6861, 0x13FE6877, 0x13FE688D, 0x13FE68A3], [0x13FE6859, 0x13FE686F, 0x13FE6885, 0x13FE689B], "TROP01"),
					new MapData([0x13FE7189, 0x13FE719F, 0x13FE71B5], [0x13FE7181, 0x13FE7197, 0x13FE71AD], "TROP02"),
					new MapData([0x13FE7AB5, 0x13FE7ACB], [0x13FE7AAD, 0x13FE7AC3], "TROP03")
				]
				, "Tropical Jungle"),
				new AreasData([new MapData([0x13FE8D11, 0x13FE8D27], [0x13FE8D09, 0x13FE8D1F], "TROP04")], "Mangrove Region"),
				new AreasData([new MapData([0x13FE9FDD, 0x13FE9FF3, 0x13FEA009, 0x13FEA01F, 0x13FEA035, 0x13FEA04B, 0x13FEA061, 0x13FEA077, 0x13FEA08D, 0x13FEA0A3, 0x13FEA0B9, 0x13FEA0CF, 0x13FEA0E5, 0x13FEA0FB, 0x13FEA10F],
											[0x13FE9FD5, 0x13FE9FEB, 0x13FEA001, 0x13FEA017, 0x13FEA02D, 0x13FEA043, 0x13FEA059, 0x13FEA06F, 0x13FEA085, 0x13FEA09B, 0x13FEA0B1, 0x13FEA0C7, 0x13FEA0DD, 0x13FEA0F3, 0x13FEA107], "TROP06")], "Amida Forest"),
				new AreasData(
				[
					new MapData([0x13FEC445, 0x13FEC45B, 0x13FEC471], [0x13FEC43D, 0x13FEC453, 0x13FEC469], "MIHA00"),
					new MapData([0x13FED695, 0x13FED6AB, 0x13FED6C1], [0x13FED68D, 0x13FED6A3, 0x13FED6B9], "MIHA02")
				]
				, "Path Thru Mt. Panorama"),
				new AreasData([new MapData([0x13FECD65, 0x13FECD7B, 0x13FECD91], [0x13FECD5D, 0x13FECD73, 0x13FECD89], "MIHA01")], "Mt. Panorama Plains"),
				new AreasData(
				[
					new MapData([0x13FEF231, 0x13FEF247, 0x13FEF25D], [0x13FEF229, 0x13FEF23F, 0x13FEF255], "MIHA04A"),
					new MapData([0x13FEFB65, 0x13FEFB7B, 0x13FEFB91], [0x13FEFB5D, 0x13FEFB73, 0x13FEFB89], "MIHA04B")
				]
				, "Foot of Mt. Panorama"),
				new AreasData(
				[
					new MapData([0x1407B3D5, 0x1407B3EB, 0x1407B401, 0x1407B417, 0x1407B42D], [0x1407B3CD, 0x1407B3E3, 0x1407B3F9, 0x1407B40F, 0x1407B425], "MIHA05"),
					new MapData([0x1407BD09, 0x1407BD0F, 0x1407BD15, 0x1407BD2B, 0x1407BD41], [0x1407BD01, 0x1407BD01, 0x1407BD01, 0x1407BD23, 0x1407BD39], "MIHA06")
				]
				, "Mt. Panorama Spore Area"),
				new AreasData([new MapData([0x13FF72D1, 0x13FF72E7, 0x13FF72FD], [0x13FF72C9, 0x13FF72DF, 0x13FF72F5], "DGHA01")], "Overdell"),
				new AreasData([new MapData([0x13FF7C01, 0x13FF7C17, 0x13FF7C2D, 0x13FF7C43], [0x13FF7BF9, 0x13FF7C0F, 0x13FF7C25, 0x13FF7C3B], "DGHA02")], "Overdell Cemetery"),
				new AreasData([new MapData([0x13FF852D, 0x13FF8543, 0x13FF8559, 0x13FF856F], [0x13FF8525, 0x13FF853B, 0x13FF8551, 0x13FF8567], "GCAN01")], "Great Canyon Entrance"),
				new AreasData(
				[
					new MapData([0x13FF8E69, 0x13FF8E7F, 0x13FF8E95, 0x13FF8EAB], [0x13FF8E61, 0x13FF8E77, 0x13FF8E8D, 0x13FF8EA3], "GCAN02"),
					new MapData([0x13FF9795, 0x13FF97AB, 0x13FF97C1, 0x13FF97D7], [0x13FF978D, 0x13FF97A3, 0x13FF97B9, 0x13FF97CF], "GCAN03"),
					new MapData([0x13FFC585, 0x13FFC59B], [0x13FFC57D, 0x13FFC593], "GCAN08_1"),
					new MapData([0x14039C8D, 0x14039CA3], [0x14039C85, 0x14039C9B], "GCAN08_2"),
					new MapData(gcan09, gcan09C, "GCAN09")
				]
				, "Great Canyon Top Area"),
				new AreasData(
				[
					new MapData([0x13FFB321, 0x13FFB337], [0x13FFB319, 0x13FFB32F], "GCAN06"),
					new MapData([0x13FFBC4D, 0x13FFBC63, 0x13FFBC79], [0x13FFBC45, 0x13FFBC5B, 0x13FFBC71], "GCAN07")
				]
				, "Great Canyon Bot. Area"),
				new AreasData([new MapData([0x13FFA9D9, 0x13FFA9EF, 0x13FFAA05, 0x13FFAA1B], [0x13FFA9D1, 0x13FFA9E7, 0x13FFA9FD, 0x13FFAA13], "GCAN05")], "Fortress Entrance"),
				new AreasData(
				[
					new MapData([0x1400C6B5, 0x1400C6CB, 0x1400C6E1], [0x1400C6AD, 0x1400C6C3, 0x1400C6D9], "GIAS00"),
					new MapData([0x1400CFED, 0x1400D003, 0x1400D019, 0x1400D02F], [0x1400CFE5, 0x1400CFFB, 0x1400D011, 0x1400D027], "GIAS01"),
					new MapData([0x1400D921, 0x1400D937, 0x1400D94D], [0x1400D919, 0x1400D92F, 0x1400D945], "GIAS02"),
					new MapData([0x1400E245, 0x1400E25B, 0x1400E271], [0x1400E23D, 0x1400E253, 0x1400E269], "GIAS03"),
					new MapData([0x1400EB9D, 0x1400EBB3, 0x1400EBC9, 0x1400EBDF], [0x1400EB95, 0x1400EBAB, 0x1400EBC1, 0x1400EBD7], "GIAS04"),
					new MapData([0x1400FDE1, 0x1400FDF7, 0x1400FE0D, 0x1400FE23], [0x1400FDD9, 0x1400FDEF, 0x1400FE05, 0x1400FE1B], "GIAS05"),
					new MapData(gias6A, gias6AC, "GIAS06A"),
					new MapData([0x1403B80D, 0x1403B823], [0x1403B805, 0x1403B81B], "GIAS06B"),
					new MapData([0x14011031, 0x14011047, 0x1401105D, 0x14011073], [0x14011029, 0x1401103F, 0x14011055, 0x1401106B], "GIAS07"),
					new MapData([0x14011969, 0x1401197F], [0x14011961, 0x14011977], "GIAS08"),
					new MapData([0x14012291, 0x140122A7, 0x140122BD], [0x14012289, 0x1401229F, 0x140122B5], "GIAS09")
				]
				, "Gear Savanna"),
				new AreasData([new MapData([0x140134F5, 0x1401350B], [0x140134ED, 0x14013503], "KODA00")], "Ancient Dino Region"),
				new AreasData(
				[
					new MapData([0x14014759, 0x1401476F, 0x14014785, 0x1401479B], [0x14014751, 0x14014767, 0x1401477D, 0x14014793], "KODA01"),
					new MapData([0x14015089, 0x1401509F, 0x140150B5], [0x14015081, 0x14015097, 0x140150AD], "KODA02"),
					new MapData([0x140162DD, 0x140162F3, 0x14016309, 0x1401631F], [0x140162DD, 0x140162F3, 0x14016309, 0x1401631F], "KODA03")
				]
				, "Ancient Glacial Region"),
				new AreasData([new MapData([0x140190C9], [0x140190C1], "KODA07")], "Ancient Speedy Region"),
				new AreasData(
				[
					new MapData([0x1401AC6D, 0x1401AC83, 0x1401AC99], [0x1401AC65, 0x1401AC7B, 0x1401AC91], "FRZL01"),
					new MapData([0x1401B595, 0x1401B5AB], [0x1401B58D, 0x1401B5A3], "FRZL02"),
					new MapData([0x1401BEC5, 0x1401BEDB, 0x1401BEF1], [0x1401BEBD, 0x1401BED3, 0x1401BEE9], "FRZL03"),
					new MapData([0x1401C7FD, 0x1401C813, 0x1401C829], [0x1401C7F5, 0x1401C80B, 0x1401C821], "FRZL04"),
					new MapData([0x1401DA61, 0x1401DA77, 0x1401DA8D], [0x1401DA59, 0x1401DA6F, 0x1401DA85], "FRZL06"),
					new MapData([0x1401E389, 0x1401E39F, 0x1401E3B5], [0x1401E381, 0x1401E397, 0x1401E3AD], "FRZL07"),
					new MapData([0x1401ECB9, 0x1401ECCF, 0x1401ECE5, 0x1401ECFB], [0x1401ECB1, 0x1401ECC7, 0x1401ECDD, 0x1401ECF3], "FRZL08"),
					new MapData([0x1401FF15, 0x1401FF2B], [0x1401FF0D, 0x1401FF23], "FRZL12"),
					new MapData([0x1403C145, 0x1403C15B], [0x1403C13D, 0x1403C153], "FRZL13"),
					new MapData([0x1403CA6D, 0x1403CA83], [0x1403CA65, 0x1403CA7B], "FRZL14"),
					new MapData([0x1403DCD1], [0x1403DCC9], "FRZL15"),
					new MapData([0x1403E605], [0x1403E5FD], "FRZL16")
				]
				, "Freezeland"),
				new AreasData([new MapData([0x140251D9], [0x140251D1], "BETL01")], "Bettle Land"),
				new AreasData(
				[
					new MapData([0x140312B9, 0x140312CF, 0x140312E5, 0x140312FB], [0x140312B1, 0x140312C7, 0x140312DD, 0x140312F3], "MIST01"),
					new MapData([0x14031BF5, 0x14031C0B, 0x14031C21], [0x14031BED, 0x14031C03, 0x14031C19], "MIST02"),
					new MapData([0x14032531, 0x14032547, 0x1403255D, 0x14032573,], [0x14032529, 0x1403253F, 0x14032555, 0x1403256B], "MIST03"),
					new MapData([0x1403377D, 0x14033793, 0x140337A9], [0x14033775, 0x1403378B, 0x140337A1], "MIST04"),
					new MapData([0x14034099, 0x140340AF, 0x140340C5, 0x140340DB], [0x14034091, 0x140340A7, 0x140340BD, 0x140340D3], "MIST05"),
					new MapData([0x14035C35, 0x14035C4B, 0x14035C61], [0x14035C2D, 0x14035C43, 0x14035C59], "MIST06"),
					new MapData([0x14036579, 0x1403658F, 0x140365A5, 0x140365BB], [0x14036571, 0x14036587, 0x1403659D, 0x140365B3], "MIST07")
				]
				, "Misty Trees"),
								new AreasData(
				[
					new MapData([0x140413F9, 0x1404140F, 0x14041425, 0x1404143B], [0x140413F1, 0x14041407, 0x1404141D, 0x14041433], "STIC01"),
					new MapData([0x14041D25, 0x14041D3B, 0x14041D51], [0x14041D1D, 0x14041D33, 0x14041D49], "STIC02")
				]
				, "Geko Swamp")
		]);

		foreach (var area in allAreas)
		{
			Areas.AddItem(area.AreaName);
		}

		for (int i = 0; i < allAreas.Count; i++)
		{
			if (i == 8)
				continue;
			for (int j = 0; j < allAreas[i].maps.Count; j++)
			{
				for (int h = 0; h < allAreas[i].maps[j].offsets.Count; h++)
				{
					bin.Position = allAreas[i].maps[j].offsets[h];
					allAreas[i].maps[j].itemIds.Add(bin.ReadByte());
					bin.Position = allAreas[i].maps[j].chanceOffsets[h];
					allAreas[i].maps[j].itemChances.Add(bin.ReadByte() + 1);
				}
			}
		}

		for (int i = 0; i < allAreas[8].maps[0].offsets.Count; i++)
		{
			bin.Position = allAreas[8].maps[0].offsets[i];
			int itemID = bin.ReadByte();
			ItemIcons[i].Texture = mainParent.GetItemTex(itemID);
			ItemNames[i].Text = parent.GetItemData(itemID).name;
			if (i < 12 && i % 2 == 0)
			{
				bin.Position = allAreas[8].maps[0].chanceOffsets[i];
				int itemChance = bin.ReadByte();
				CentaItemChances[i].Text = (itemChance + 1).ToString();
				bin.Position = allAreas[8].maps[0].chanceOffsets[i + 1];
				itemChance = bin.ReadByte() - itemChance;
				CentaItemChances[i + 1].Text = itemChance.ToString();
			}
			else if (i == 12)
			{
				bin.Position = allAreas[8].maps[0].chanceOffsets[i];
				int firstChance = bin.ReadByte();
				CentaItemChances[i].Text = (firstChance + 1).ToString();
				bin.Position = allAreas[8].maps[0].chanceOffsets[i + 1];
				int secondChance = bin.ReadByte();
				CentaItemChances[i + 1].Text = (secondChance - firstChance).ToString();
				bin.Position = allAreas[8].maps[0].chanceOffsets[i + 2];
				secondChance = bin.ReadByte() - secondChance;
				CentaItemChances[i + 2].Text = secondChance.ToString();
			}
		}
		if (vice)
		{
			allAreas.Add(new AreasData([new MapData([0x13FEDFB1, 0x13FEDFB7, 0x13FEDFBD, 0x13FEDFC3, 0x13FEDFD5, 0x13FEE00D, 0x13FEE005], [], "MIHA03")], "Bonus Area"));
			for (int i = 0; i < allAreas[allAreas.Count - 1].maps[0].offsets.Count; i++)
			{
				bin.Position = allAreas[allAreas.Count - 1].maps[0].offsets[i];
				allAreas[allAreas.Count - 1].maps[0].itemIds.Add(bin.ReadByte());
				allAreas[allAreas.Count - 1].maps[0].itemChances.Add(100);
			}
		}
		mainParent = main;
		parent = items;
	}

	void OnAreaSelected(int areaIndex)
	{
		Maps.Clear();
		areaSelected = areaIndex;
		foreach (var map in allAreas[areaIndex].maps)
		{
			Maps.AddItem(map.mapName);
		}
		Maps.Selected = -1;
		ItemsContainer.Visible = false;
		CentaItems.Visible = false;
		CentaItems2.Visible = false;
	}

	void OnMapSelected(int mapIndex)
	{
		if (areaSelected != 8)
		{
			ItemsContainer.Visible = true;
			int i = 0;
			for (; i < allAreas[areaSelected].maps[mapIndex].itemIds.Count; i++)
			{
				Items[i].Visible = true;
				ItemIcons[i].Texture = mainParent.GetItemTex(allAreas[areaSelected].maps[mapIndex].itemIds[i]);
				ItemNames[i].Text = parent.GetItemData(allAreas[areaSelected].maps[mapIndex].itemIds[i]).name;
				ItemChances[i].Text = allAreas[areaSelected].maps[mapIndex].itemChances[i].ToString();
			}
			if (i < ItemIcons.Length)
			{
				for (; i < ItemIcons.Length; i++)
				{
					Items[i].Visible = false;
				}
			}
		}
		else
		{
			CentaItems.Visible = true;
			CentaItems2.Visible = true;
		}
	}
}
