using Godot;
using System;
using System.Collections.Generic;

public partial class ChestStuff : Control
{
	class ChestData
	{
		public List<uint> offsets { get; set; }
		public string mapName { get; set; }
		public List<int> itemIds { get; set; }

		public ChestData(List<uint> itemOffset, string name)
		{
			offsets = itemOffset;
			mapName = name;
			itemIds = new List<int>();
		}
	}

	class AreasData
	{
		public List<ChestData> maps { get; set; }
		public string AreaName { get; set; }

		public AreasData(List<ChestData> mapList, string name)
		{
			maps = mapList;
			AreaName = name;
		}
	}

	[Export] Control ByMap;
	[Export] Control ByItem;
	[Export] OptionButton Areas;
	[Export] OptionButton Maps;
	[Export] TextureRect[] ItemIcons;
	[Export] Label[] ItemNames;
	[Export] Control[] Items;
	[Export] VBoxContainer ItemsContainer;
	[Export] Label AreasLabel;
	[Export] Label MapsLabel;
	[Export] Label ItemLabel;
	[Export] VBoxContainer ItemSearchList;
	[Export] Button SetByMap;
	[Export] Button SetByItem;
	[Export] OptionButton AllItems;
	[Export] Label ItemsLabel;
	[Export] Label MapNameLabel;
	[Export] Label SearchItemLabel;

	List<AreasData> allAreas;

	DataCheck mainParent;
	ItemsStuff parent;
	int areaSelected;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AreasLabel.Text = Tr("AreasSpawnLabel");
		MapsLabel.Text = Tr("MapSpawnLabel");
		ItemLabel.Text = Tr("ItemCheckLabel");
		SearchItemLabel.Text = Tr("ItemsSpawnSearchLabel");
		MapNameLabel.Text = Tr("MapCheckNameLabel");
		ItemsLabel.Text = Tr("SearchItemCheckLabel");
		SetByMap.Text = Tr("SearchSpawnMap");
		SetByItem.Text = Tr("SearchSpawnItem");
		SetByMap.Pressed += SetByMapPressed;
		SetByItem.Pressed += SetByItemPressed;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


	public void SetUpData(System.IO.Stream bin, DataCheck main, ItemsStuff items, bool vice)
	{
		mainParent = main;
		parent = items;

		ItemsContainer.Visible = false;

		allAreas = new List<AreasData>();

		uint offsetBD = 0x1405836D;
		if (vice)
			offsetBD = 0x14058371;




		allAreas.AddRange(
			[   new AreasData([new ChestData([0x13FE3119], "MAYO10")], "Dragon Eye Lake"),
				new AreasData([new ChestData([0x13FE6845], "TROP01")], "Tropical Jungle"),
				new AreasData(
				[
					new ChestData([0x140377A9], "TUNN07_3"),
					new ChestData([0x13FF4DE9, 0x13FF4DF5], "TUNN08"),
					new ChestData([0x140380D5, 0x140380E1], "TUNN08_2"),
					new ChestData([0x14038A05], "TUNN08_3"),
					new ChestData([0x13FF6979, 0x13FF6985], "TUNN10")
				]
				, "Lava Cave"),
				new AreasData([new ChestData([0x1407BD47, 0x1407BD53, 0x1407BD5F], "MIHA06")], "Mt. Panorama Spore Area"),
				new AreasData(
				[
					new ChestData([0x13FFA099], "GCAN04"),
					new ChestData([0x14039339], "GCAN04_2"),
				]
				, "Great Canyon Bridge"),
				new AreasData(
				[
					new ChestData([0x13FFD7BD], "OGRE00"),
					new ChestData([0x13FFE0F1], "OGRE01"),
					new ChestData([0x13FFF35D], "OGRE03"),
					new ChestData([0x1403AEC5, 0x1403AED1, 0x1403AEDD, 0x1403AEE9], "OGRE04")
				]
				, "Ogre Fortress"),
				new AreasData(
				[
					new ChestData([0x14000EDD, 0x14000EE9, 0x14000EF5], "YAKA02"),
					new ChestData([0x14003399, 0x140033A5], "YAKA12"),
					new ChestData([0x14005869], "YAKA14"),
					new ChestData([0x140073E9, 0x140073F5], "YAKA17"),
					new ChestData([0x14008F7D], "YAKA23"),
					new ChestData([0x1407AA95, 0x1407AAA1, 0x1407AAAD, 0x1407AAB9], "YAKA25")
				]
				, "Grey Lord's Mansion"),
				new AreasData(
				[
					new ChestData([0x14021169, 0x14021175], "ISCA02"),
					new ChestData([0x14022D05], "ISCA05"),
					new ChestData([0x14023625, 0x14023631], "ISCA06"),
					new ChestData([0x14023F55, 0x14023F61], "ISCA07")
				]
				, "Ice Sanctuary"),
				new AreasData([new ChestData([0x14030965], "LEOM02")], "Leomon's Ancestor Cave"),
				new AreasData([new ChestData([0x14045425], "OGRE11")], "Secret Beach Cave"),
				new AreasData([new ChestData([0x1404A6DD], "OMOC08")], "Toy Mansion"),
				new AreasData(
				[
					new ChestData([0x140539ED, 0x140539F9], "FACT09"),
					new ChestData([0x1405430D, 0x14054319, 0x14054325], "FACT10")
				]
				, "Factorial Town"),

				new AreasData(
				[
					new ChestData([offsetBD], "MGEN01"),
					new ChestData([0x14058C9D], "MGEN02"),
					new ChestData([0x14067B7D], "MGEN03"),
					new ChestData([0x1406970D], "MGEN04"),
					new ChestData([0x14073335], "MGEN05"),
					new ChestData([0x1407F431], "MGEN06"),
					new ChestData([0x1407FD55], "MGEN07"),
					new ChestData([0x14080689], "MGEN08"),
					new ChestData([0x14080FB5], "MGEN09"),
					new ChestData([0x140818F5, 0x14081901], "MGEN10")
				]
				, "Mt.Infinity"),
								new AreasData(
				[
					new ChestData([0x14078F1D], "MGEN11"),
					new ChestData([0x14079849, 0x14079855, 0x14079861, 0x1407986D], "MGEN12"),
					new ChestData([0x1407A179, 0x1407A185], "MGEN13")
				]
				, "Back Dimension")
		]);

		for (int i = 0; i < allAreas.Count; i++)
		{
			for (int j = 0; j < allAreas[i].maps.Count; j++)
			{
				for (int h = 0; h < allAreas[i].maps[j].offsets.Count; h++)
				{
					bin.Position = allAreas[i].maps[j].offsets[h];
					allAreas[i].maps[j].itemIds.Add(bin.ReadByte());
				}
			}
		}


		if (vice)
		{
			allAreas.Add(new AreasData([new ChestData([0x13FEE011], "MIHA03")], "Bonus Area"));

			bin.Position = 0x13FEE011;
			allAreas[allAreas.Count - 1].maps[0].itemIds.Add(bin.ReadByte());

		}

		Areas.Clear();
		foreach (var area in allAreas)
		{
			Areas.AddItem(area.AreaName);
		}
		Areas.Selected = -1;

		AllItems.Clear();
		for (int i = 0; i < 128; i++)
		{
			AllItems.AddIconItem(mainParent.GetItemTex(i), parent.GetItemData(i).name, i);
		}

		AllItems.Selected = -1;

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
	}

	void OnMapSelected(int mapIndex)
	{
		ItemsContainer.Visible = true;
		int i = 0;
		for (; i < allAreas[areaSelected].maps[mapIndex].itemIds.Count; i++)
		{
			Items[i].Visible = true;
			ItemIcons[i].Texture = mainParent.GetItemTex(allAreas[areaSelected].maps[mapIndex].itemIds[i]);
			ItemNames[i].Text = parent.GetItemData(allAreas[areaSelected].maps[mapIndex].itemIds[i]).name;
		}
		if (i < ItemIcons.Length)
		{
			for (; i < ItemIcons.Length; i++)
			{
				Items[i].Visible = false;
			}
		}

	}

	void OnItemSelected(int itemIndex)
	{
		if (ItemSearchList.GetChildCount() != 0)
		{
			foreach (Node n in ItemSearchList.GetChildren())
			{
				ItemSearchList.RemoveChild(n);
				n.QueueFree();
			}
		}
		foreach (AreasData area in allAreas)
		{
			foreach (ChestData map in area.maps)
			{
				for (int i = 0; i < map.itemIds.Count; i++)
				{
					if (map.itemIds[i] == itemIndex)
					{
						var scene = GD.Load<PackedScene>("res://Items/ItemSearch.tscn");
						var child = scene.Instantiate() as ItemSearch;
						child.SetupData(mainParent.GetItemTex(itemIndex), map.mapName, parent.GetItemData(itemIndex).name);
						ItemSearchList.AddChild(child);
					}
				}
			}
		}
	}

	void SetByItemPressed()
	{
		ByMap.Visible = false;
		ByItem.Visible = true;
		SetByItem.Disabled = true;
		SetByMap.Disabled = false;
	}

	void SetByMapPressed()
	{
		ByMap.Visible = true;
		ByItem.Visible = false;
		SetByItem.Disabled = false;
		SetByMap.Disabled = true;
	}
	
	public void RestartData()
	{
		AllItems.Selected = -1;
		Areas.Selected = -1;
		ByMap.Visible = false;
		ByItem.Visible = false;
		SetByItem.Disabled = false;
		SetByMap.Disabled = false;
		ItemsContainer.Visible = false;
	}
}
