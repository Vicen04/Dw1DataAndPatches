
using Godot;
using System;
using System.Collections.Generic;
using System.Threading;

public partial class Bosses2 : Control
{
	public class DigimonNPCData
	{
		public List<uint> offsets { get; set; }
		public string mapName { get; set; }
		public List<int> digimonIds { get; set; }
		public List<int> HP { get; set; }
		public List<int> MP { get; set; }
		public List<int> cHP { get; set; }
		public List<int> cMP { get; set; }
		public List<int> Off { get; set; }
		public List<int> Def { get; set; }
		public List<int> Spd { get; set; }
		public List<int> Brn { get; set; }
		public List<int> Money { get; set; }
		public List<int> Attack1 { get; set; }
		public List<int> Attack2 { get; set; }
		public List<int> Attack3 { get; set; }
		public List<int> Attack4 { get; set; }
		public List<int> Attack1C { get; set; }
		public List<int> Attack2C { get; set; }
		public List<int> Attack3C { get; set; }
		public List<int> Attack4C { get; set; }

		public DigimonNPCData(List<uint> NpcOffset, string name)
		{
			offsets = NpcOffset;
			mapName = name;
			digimonIds = new List<int>();
			HP = new List<int>();
			MP = new List<int>();
			cHP = new List<int>();
			cMP = new List<int>();
			Off = new List<int>();
			Def = new List<int>();
			Spd = new List<int>();
			Brn = new List<int>();
			Money = new List<int>();
			Attack1 = new List<int>();
			Attack2 = new List<int>();
			Attack3 = new List<int>();
			Attack4 = new List<int>();
			Attack1C = new List<int>();
			Attack2C = new List<int>();
			Attack3C = new List<int>();
			Attack4C = new List<int>();
		}
	}

	public class AreasNPCData
	{
		public List<DigimonNPCData> maps { get; set; }
		public string AreaName { get; set; }

		public AreasNPCData(List<DigimonNPCData> mapList, string name)
		{
			maps = mapList;
			AreaName = name;
		}
	}

	[Export] Control ByMap;
	[Export] Control ByItem;
	[Export] OptionButton Areas;
	[Export] OptionButton Maps;
	[Export] VBoxContainer DigimonContainer;
	[Export] Label AreasLabel;
	[Export] Label MapsLabel;
	[Export] Label[] Attacks;
	[Export] Label[] ChanceLabel;
	[Export] Label MoneyLabel;
	[Export] Button SetByMap;
	[Export] Button SetByTech;
	[Export] OptionButton AllTechs;
	[Export] Label[] AttacksTech;
	[Export] Label MoneyLabelTech;
	[Export] Label MapNameLabel;
	[Export] VBoxContainer TechSearchList;

	DigimonStuff digimonData;
	DataCheck mainP;

	int currentArea = -1;

	List<MapDigimon> digimonMap;
	List<MapDigimon> searchDigimonMap;

	List<AreasNPCData> NPCsData;

	bool hardcore = false, truehardcore = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SetByMap.Pressed += MapSelected;
		SetByTech.Pressed += TechSelected;
		for (int i = 0; i < Attacks.Length; i++)
		{
			Attacks[i].Text = Tr("AttackText" + i);
			AttacksTech[i].Text = Tr("AttackText" + i);
		}

		for (int i = 0; i < ChanceLabel.Length; i++)
			ChanceLabel[i].Text = Tr("StatusChaTechCheck");
		MoneyLabel.Text = Tr("DigimonMoney_T");
		MoneyLabelTech.Text = Tr("DigimonMoney_T");
		MapNameLabel.Text = Tr("MapLabelSearch");
		MapsLabel.Text = Tr("MapSpawnLabel");
		AreasLabel.Text = Tr("AreasSpawnLabel");
		SetByMap.Text = Tr("SearchSpawnMap");
		SetByTech.Text = Tr("SearchByTech");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public void SetupBosses(System.IO.Stream bin, System.IO.BinaryReader reader, DataCheck main, DigimonStuff digimons, bool vice, bool bosses)
	{
		if (NPCsData != null)
		{
			for (int i = 0; i < NPCsData.Count; i++)
				NPCsData[i].maps.Clear();
			NPCsData.Clear();
		}

		NPCsData = new List<AreasNPCData>();

		bin.Position = 0x6580D42;
		if (bin.ReadByte() != 0x80)
		{
			hardcore = true;

			bin.Position = 0x723F7EE;
			if (bin.ReadByte() == 0x2F)
				truehardcore = true;
		}
		if (bosses)
			SetupBosses(vice);
		else
			SetupNPC(vice);

		foreach (AreasNPCData area in NPCsData)
		{
			foreach (DigimonNPCData DigimonData in area.maps)
			{
				for (int i = 0; i < DigimonData.offsets.Count; i++)
				{

					bin.Position = DigimonData.offsets[i];
					DigimonData.digimonIds.Add(reader.ReadInt16());

					bin.Position = DigimonData.offsets[i] + 22;
					if (CheckIfECC(bin.Position)) bin.Position = bin.Position + 0x130;
					DigimonData.HP.Add(reader.ReadInt16());
					if (CheckIfECC(bin.Position)) bin.Position = bin.Position + 0x130;
					DigimonData.MP.Add(reader.ReadInt16());
					if (CheckIfECC(bin.Position)) bin.Position = bin.Position + 0x130;
					DigimonData.cHP.Add(reader.ReadInt16());
					if (CheckIfECC(bin.Position)) bin.Position = bin.Position + 0x130;
					DigimonData.cMP.Add(reader.ReadInt16());
					if (CheckIfECC(bin.Position)) bin.Position = bin.Position + 0x130;
					DigimonData.Off.Add(reader.ReadInt16());
					if (CheckIfECC(bin.Position)) bin.Position = bin.Position + 0x130;
					DigimonData.Def.Add(reader.ReadInt16());
					if (CheckIfECC(bin.Position)) bin.Position = bin.Position + 0x130;
					DigimonData.Spd.Add(reader.ReadInt16());
					if (CheckIfECC(bin.Position)) bin.Position = bin.Position + 0x130;
					DigimonData.Brn.Add(reader.ReadInt16());
					if (CheckIfECC(bin.Position)) bin.Position = bin.Position + 0x130;
					DigimonData.Money.Add(reader.ReadInt16());

					if (CheckIfECC(bin.Position)) bin.Position = bin.Position + 0x130;
					reader.ReadInt32();
					if (CheckIfECC(bin.Position)) bin.Position = bin.Position + 0x130;

					DigimonData.Attack1.Add(reader.ReadInt16());
					if (CheckIfECC(bin.Position)) bin.Position = bin.Position + 0x130;
					DigimonData.Attack2.Add(reader.ReadInt16());
					if (CheckIfECC(bin.Position)) bin.Position = bin.Position + 0x130;
					DigimonData.Attack3.Add(reader.ReadInt16());
					if (CheckIfECC(bin.Position)) bin.Position = bin.Position + 0x130;
					DigimonData.Attack4.Add(reader.ReadInt16());
					if (CheckIfECC(bin.Position)) bin.Position = bin.Position + 0x130;
					DigimonData.Attack1C.Add(reader.ReadInt16());
					if (CheckIfECC(bin.Position)) bin.Position = bin.Position + 0x130;
					DigimonData.Attack2C.Add(reader.ReadInt16());
					if (CheckIfECC(bin.Position)) bin.Position = bin.Position + 0x130;
					DigimonData.Attack3C.Add(reader.ReadInt16());
					if (CheckIfECC(bin.Position)) bin.Position = bin.Position + 0x130;
					DigimonData.Attack4C.Add(reader.ReadInt16());

					int attackCount = 0;
					if (DigimonData.digimonIds[i] < 176 && DigimonData.digimonIds[i] > -1)
						attackCount = main.GetDigimonData(DigimonData.digimonIds[i]).Attacks.Count;
					else
						DigimonData.digimonIds[i] = 0;
					int attack;
					if (DigimonData.Attack1[i] >= 0x2E)
					{
						attack = DigimonData.Attack1[i] - 0x2E;
						if (attack < attackCount)
							DigimonData.Attack1[i] = main.GetDigimonData(DigimonData.digimonIds[i]).Attacks[attack];
						else
							DigimonData.Attack1[i] = 0xFF;
					}
					if (DigimonData.Attack2[i] >= 0x2E)
					{
						attack = DigimonData.Attack2[i] - 0x2E;
						if (attack < attackCount)
							DigimonData.Attack2[i] = main.GetDigimonData(DigimonData.digimonIds[i]).Attacks[DigimonData.Attack2[i] - 0x2E];
						else
							DigimonData.Attack2[i] = 0xFF;
					}
					if (DigimonData.Attack3[i] >= 0x2E)
					{
						attack = DigimonData.Attack3[i] - 0x2E;
						if (attack < attackCount)
							DigimonData.Attack3[i] = main.GetDigimonData(DigimonData.digimonIds[i]).Attacks[DigimonData.Attack3[i] - 0x2E];
						else
							DigimonData.Attack3[i] = 0xFF;
					}
					if (DigimonData.Attack4[i] >= 0x2E)
					{
						attack = DigimonData.Attack4[i] - 0x2E;
						if (attack < attackCount)
							DigimonData.Attack4[i] = main.GetDigimonData(DigimonData.digimonIds[i]).Attacks[DigimonData.Attack4[i] - 0x2E];
						else
							DigimonData.Attack4[i] = 0xFF;
					}
				}
			}
		}

		if (digimonMap != null || searchDigimonMap != null)
		{
			foreach (MapDigimon digi in digimonMap)
				digi.QueueFree();
			digimonMap.Clear();
			foreach (MapDigimon digi in searchDigimonMap)
				digi.QueueFree();
			searchDigimonMap.Clear();
		}
		digimonMap = new List<MapDigimon>();
		searchDigimonMap = new List<MapDigimon>();

		digimonData = digimons;
		mainP = main;
		Areas.Clear();

		foreach (AreasNPCData area in NPCsData)
			Areas.AddItem(area.AreaName);

		AllTechs.Clear();

		int extra = 0;
		if (vice) extra = 1;

		for (int i = 0; i < 58 - extra; i++)
			AllTechs.AddIconItem(main.GetTechsSprites(digimonData.GetTechData().GetTechData(i).type), digimonData.GetTechData().GetTechData(i).name);

		Areas.Selected = -1;
		currentArea = -1;
		AllTechs.Selected = -1;
	}


	void AreaSelected(int selected)
	{
		Maps.Clear();
		foreach (DigimonNPCData DigimonData in NPCsData[selected].maps)
			Maps.AddItem(DigimonData.mapName);
		currentArea = selected;
		Maps.Selected = -1;
		foreach (MapDigimon digi in digimonMap)
			digi.QueueFree();
		digimonMap.Clear();
	}

	void MapSelected(int selected)
	{
		foreach (MapDigimon digi in digimonMap)
			digi.QueueFree();
		digimonMap.Clear();
		var data = NPCsData[currentArea].maps[selected];
		for (int i = 0; i < data.offsets.Count; i++)
		{

			var scene = GD.Load<PackedScene>("res://Items/MapDigimon.tscn");
			digimonMap.Add(scene.Instantiate() as MapDigimon);
			digimonMap[i].SetupDigimon(mainP.GetDigimonData(data.digimonIds[i]).digimonSprite, mainP.GetDigimonData(data.digimonIds[i]).name, data.HP[i], data.MP[i],
									   data.cHP[i], data.cMP[i], data.Off[i], data.Def[i], data.Spd[i], data.Brn[i], data.Money[i], digimonData.GetTechData().GetTechData(data.Attack1[i]).name,
									   digimonData.GetTechData().GetTechData(data.Attack2[i]).name, digimonData.GetTechData().GetTechData(data.Attack3[i]).name,
									   digimonData.GetTechData().GetTechData(data.Attack4[i]).name, data.Attack1C[i], data.Attack2C[i], data.Attack3C[i], data.Attack4C[i]);
			DigimonContainer.AddChild(digimonMap[i]);

		}
	}

	void TechSelected(int selected)
	{
		foreach (MapDigimon digi in searchDigimonMap)
			digi.QueueFree();
		searchDigimonMap.Clear();
		foreach (AreasNPCData area in NPCsData)
		{
			foreach (DigimonNPCData DigimonData in area.maps)
			{
				for (int i = 0; i < DigimonData.Attack1.Count; i++)
				{
					if (DigimonData.Attack1[i] == selected || DigimonData.Attack2[i] == selected || DigimonData.Attack3[i] == selected || DigimonData.Attack4[i] == selected)
					{
						var scene = GD.Load<PackedScene>("res://Items/MapDigimon.tscn");
						searchDigimonMap.Add(scene.Instantiate() as MapDigimon);
						searchDigimonMap[searchDigimonMap.Count -1].SetupDigimon(mainP.GetDigimonData(DigimonData.digimonIds[i]).digimonSprite, mainP.GetDigimonData(DigimonData.digimonIds[i]).name, DigimonData.HP[i], DigimonData.MP[i],
						DigimonData.cHP[i], DigimonData.cMP[i], DigimonData.Off[i], DigimonData.Def[i], DigimonData.Spd[i], DigimonData.Brn[i], DigimonData.Money[i], digimonData.GetTechData().GetTechData(DigimonData.Attack1[i]).name,
						digimonData.GetTechData().GetTechData(DigimonData.Attack2[i]).name, digimonData.GetTechData().GetTechData(DigimonData.Attack3[i]).name,
						digimonData.GetTechData().GetTechData(DigimonData.Attack4[i]).name, DigimonData.Attack1C[i], DigimonData.Attack2C[i], DigimonData.Attack3C[i], DigimonData.Attack4C[i], DigimonData.mapName);
						TechSearchList.AddChild(searchDigimonMap[searchDigimonMap.Count -1]);
					}
				}
			}
		}

	}

	void MapSelected()
	{
		ByMap.Visible = true;
		ByItem.Visible = false;
		SetByMap.Disabled = true;
		SetByTech.Disabled = false;
	}

	void TechSelected()
	{
		ByMap.Visible = false;
		ByItem.Visible = true;
		SetByMap.Disabled = false;
		SetByTech.Disabled = true;
	}

	void SetupBosses(bool vice)
	{
		NPCsData.Add(
			new AreasNPCData(
				[
					new DigimonNPCData([0x9C9B63E], "MAYO00"),
					new DigimonNPCData([0x14D3A70], "MAYO02"),
					new DigimonNPCData([0x157DE1C], "MAYO03"),
					new DigimonNPCData([0x164339A], "MAYO04A")
				]
				, "Native Forest")
		);
		if (hardcore)
		{
			NPCsData.Add(new AreasNPCData([new DigimonNPCData([0x17C609E], "MAYO05")], "Coela Point")
		);
		}
		NPCsData.Add(new AreasNPCData([new DigimonNPCData([0x18D6692], "MAYO08A")], "Digimon Bridge"));

		if (!hardcore)
			NPCsData.Add(new AreasNPCData([new DigimonNPCData([0x1C01852], "TROP01")], "Tropical Forest"));
		else
		{
			NPCsData.AddRange([new AreasNPCData(
				[
					new DigimonNPCData([0x1C01852], "TROP01"),
					new DigimonNPCData([0x1D68AAE], "TROP03"),
				]
				, "Tropical Forest"),
			new AreasNPCData([new DigimonNPCData([0x6580D42], "TROP04")], "Mangrove Region"),
			new AreasNPCData([new DigimonNPCData([0x66F9FCA, 0x66FA024, 0x66FA07E], "TROP06"),], "Amida Forest"),
			new AreasNPCData([new DigimonNPCData([0x6C08F5A], "DGHA02")], "Overdell Cemetery")]);
		}

		NPCsData.Add(
			new AreasNPCData(
				[
					new DigimonNPCData([0x68440E4, 0x684413E, 0x6844198], "TUNN02"),
					new DigimonNPCData([0x69CECCA], "TUNN04"),
				]
				, "Drill Tunnel")
		);
		if (truehardcore)
			NPCsData[NPCsData.Count - 1].maps.Add(new DigimonNPCData([0x740DEC6, 0x740DF20], "TUNN09"));

		NPCsData.Add(new AreasNPCData([new DigimonNPCData([0x7351FCA], "TUNN08")], "Lava Cave"));

		if (truehardcore)
			NPCsData.Add(new AreasNPCData([new DigimonNPCData([0x780E4D2], "YAKA11A"),], "Grey Lord's Mansion"));

		NPCsData.Add(new AreasNPCData([new DigimonNPCData([0x7D6A690], "CHKA01"),], "Mansion Basement"));
		if (truehardcore)
			NPCsData[NPCsData.Count - 1].maps[0].offsets.Insert(0, 0x7D6A636);

		if (hardcore)
			NPCsData.Add(new AreasNPCData([new DigimonNPCData([0x6ED3D42], "GCAN04")], "Great Canyon Bridge"));

		NPCsData.Add(new AreasNPCData([new DigimonNPCData([0x6FA261C], "GCAN05")], "Fortress Entrance"));

		NPCsData.Add(
			new AreasNPCData([
				new DigimonNPCData([0x7195B30, 0x7195B8A, 0x7195BE4], "GCAN08_1"),
				new DigimonNPCData([0xA5B5F48, 0xA5B5FA2, 0xA5B5FFC], "GCAN08_2")
				], "Great Canyon Top Area"));

		if (truehardcore)
			NPCsData[NPCsData.Count - 1].maps.Add(new DigimonNPCData([0x723F7EE], "GCAN09"));

		NPCsData.Add(new AreasNPCData([new DigimonNPCData([0xA668D76], "GCAN10")], "Great Canyon"));

		NPCsData.Add(new AreasNPCData([new DigimonNPCData([0x8B162AA], "KODA00")], "Ancient Dino Region"));
		NPCsData.Add(
			new AreasNPCData(
				[
					new DigimonNPCData([0x8E50416], "KODA06"),
					new DigimonNPCData([0x8EEBE7A], "KODA07")
				]
				, "Ancient Speedy Region"));
		if (hardcore)
			NPCsData[NPCsData.Count - 1].maps[1].offsets.Add(0x8EEBED4);
		if (vice)
			NPCsData[NPCsData.Count - 1].maps[1].offsets.Add(0x8EEBF2E);

		NPCsData.Add(new AreasNPCData([new DigimonNPCData([0x61EF0C2], "MIHA01")], "Mt. Panorama Plains"));
		if (hardcore)
		{
			NPCsData.Add(
			new AreasNPCData(
				[
					new DigimonNPCData([0x5A8FA32], "MIHA05"),
					new DigimonNPCData([0x5B399C6], "MIHA06")
				]
				, "Mt. Panorama Spore Area"));
		}

		NPCsData.Add(
			new AreasNPCData(
				[
					new DigimonNPCData([0x76268AE, 0x7626908, 0x7626962, 0x76269BC], "OGRE02"),
					new DigimonNPCData([0x76B102E, 0x76B1088, 0x76B10E2, 0x76B113C], "OGRE03")
				]
				, "Ogre Fortress"));
		if (hardcore)
			NPCsData[NPCsData.Count - 1].maps[1].offsets.Add(0x76B1196);

		NPCsData.Add(new AreasNPCData([new DigimonNPCData([0x800E7FA], "GIAS03")], "Gear Savanna"));
		if (hardcore)
		{
			NPCsData[NPCsData.Count - 1].maps.Insert(0, new DigimonNPCData([0x7F6E32A], "GIAS02"));
			NPCsData[NPCsData.Count - 1].maps.Add(new DigimonNPCData([0x80D4B82], "GIAS04"));
		}

		if (vice)
			NPCsData[NPCsData.Count - 1].maps.Add(new DigimonNPCData([0x8836592, 0x88365EC], "GIAS06A"));

		if (hardcore)
			NPCsData[NPCsData.Count - 1].maps.Add(new DigimonNPCData([0x8A6330E], "GIAS09"));


		if (truehardcore)
			NPCsData.Add(new AreasNPCData([new DigimonNPCData([0x2EA2E6A, 0x2EA2EC4, 0x2EA2F1E], "GOMI02"),], "Trash Mountain"));

		NPCsData.Add(new AreasNPCData([new DigimonNPCData([0x254ABC2], "STIC02")], "Geko Swamp"));

		NPCsData.Add(new AreasNPCData([new DigimonNPCData([0x9303D0E, 0x9303EF4], "FRZL08")], "Freezeland"));

		if (truehardcore)
		{
			NPCsData[NPCsData.Count - 1].maps.AddRange(
					[
						new DigimonNPCData([0xA40BA06, 0xA40BA60, 0xA40BABA], "FRZL14"),
						new DigimonNPCData([0x1EC08C6], "FRZL17"),
						new DigimonNPCData([0x1F404D2], "FRZL18")
					]);
		}

		NPCsData.Add(new AreasNPCData([new DigimonNPCData([0x9858822], "ICSA08")], "Ice Sanctuary"));

		if (hardcore)
			NPCsData[NPCsData.Count - 1].maps.Insert(0, new DigimonNPCData([0x94851F6], "ICSA01"));

		NPCsData.Add(new AreasNPCData([new DigimonNPCData([0x2133FCA, 0x2134024, 0x213407E, 0x21340D8], "OGRE11")], "Secret Beach Cave"));
		if (vice)
			NPCsData[NPCsData.Count - 1].maps[0].offsets.Add(0x2134132);

		if (truehardcore)
			NPCsData[NPCsData.Count - 1].maps.Insert(0, new DigimonNPCData([0x20C7328], "OGRE10"));	

		NPCsData.Add(new AreasNPCData([
			new DigimonNPCData([0xA07377E], "MIST03"),
			new DigimonNPCData([0xA7EEA76], "MIST06")
			], "Misty Trees"));	

		NPCsData.Add(
			new AreasNPCData(
				[
					new DigimonNPCData([0x26D5A72], "FACT02"),
					new DigimonNPCData([0x2AF131C], "FACT08B"),
				]
				, "Factorial Town"));

		if (hardcore)
			NPCsData[NPCsData.Count - 1].maps.Insert(1, new DigimonNPCData([0x2A3286A], "FACT08A"));
		if (vice)
			NPCsData[NPCsData.Count - 1].maps.Insert(1, new DigimonNPCData([0x2929254], "FACT06"));


		NPCsData.Add(new AreasNPCData([new DigimonNPCData([0x4F4D9AE, 0x4F4DA0E], "FACT11B")], "Sewers"));
		if (hardcore)
			NPCsData[NPCsData.Count - 1].maps[0].offsets.Add(0x4F4DA6E);



		NPCsData.Add(new AreasNPCData([new DigimonNPCData([0x2E3C072], "OMOC08")], "Toy Mansion"));
		if (hardcore)
		{
			NPCsData[NPCsData.Count - 1].maps[0].offsets.Add(0x2E3C0CC);
			if (truehardcore)
				NPCsData[NPCsData.Count - 1].maps.Insert(0, new DigimonNPCData([0x23A3066], "OMOC04"));
		}

		NPCsData.Add(
			new AreasNPCData(
				[
					new DigimonNPCData([0x831290E], "SAIB02"),
					new DigimonNPCData([0x558834A], "SAIB03")
				]
				, "Underground Lab"));

		if (truehardcore)
			NPCsData[NPCsData.Count - 1].maps.Insert(0, new DigimonNPCData([0x825FCA2, 0x825FCFC], "SAIB01"));

		if (hardcore)
		{
			NPCsData.Add(new AreasNPCData([
				new DigimonNPCData([0x9AA152A, 0x9AA1584, 0x9AA15DE], "BETL03"),
				new DigimonNPCData([0x9B3AAC6, 0x9B3AB20, 0x9B3AB7A], "BETL04")
			], "Beetle Land"));
			if (truehardcore)
				NPCsData[NPCsData.Count - 1].maps.Insert(0, new DigimonNPCData([0x991F282, 0x991F2E8], "BETL01"));
			NPCsData.Add(new AreasNPCData([new DigimonNPCData([0x9BBF4FE], "LEOM01")], "Leomon's Ancestor Cave"));
		}

		NPCsData.Add(
			new AreasNPCData(
				[
					new DigimonNPCData([0x3085C02, 0x3085C5C], "TWNA02"),
					new DigimonNPCData([0x314D97A, 0x314D9D4], "TWNA03"),
					new DigimonNPCData([0x3214FBA, 0x3215014], "TWNA04"),
					new DigimonNPCData([0x32DC392, 0x32DC3EC], "TWNA05"),
					new DigimonNPCData([0x33A37DA, 0x33A3834], "TWNA06"),
					new DigimonNPCData([0x346AF4A, 0x346AFA4], "TWNA07"),
					new DigimonNPCData([0x35328BA, 0x3532914], "TWNA08"),
					new DigimonNPCData([0x35F9D02, 0x35F9D5C], "TWNA09"),
					new DigimonNPCData([0x36C1472, 0x36C14CC], "TWNA10"),
					new DigimonNPCData([0x3789012, 0x378919C], "TWNA11"),
					new DigimonNPCData([0x3850EBA, 0x3850F14], "TWNA12"),
					new DigimonNPCData([0x3918E2A, 0x3918E84], "TWNA13")
				]
				, "File City"));

		NPCsData.Add(
			new AreasNPCData(
				[
					new DigimonNPCData([0x5EBEA00], "MGEN06"),
					new DigimonNPCData([0x60AC7FE], "MGEN10"),
					new DigimonNPCData([0x517BBF6], "MGEN98"),
					new DigimonNPCData([0x59E5A5C, 0x59E5B6A], "MGEN99")
				]
				, "Mt. Infinity"));

		if (vice)
		{
			NPCsData.AddRange([
			new AreasNPCData(
					[
						new DigimonNPCData([0x57EE0C6, 0x57EE120], "MGEN14"),
						new DigimonNPCData([0x5860516, 0x5860570], "MGEN15"),
						new DigimonNPCData([0x59201CE, 0x5920228], "MGEN17"),
					]
					, "Back Dimension"),
			new AreasNPCData([new DigimonNPCData([0x9E286E0, 0x9E28740], "MAYO02_2"),], "Special Sukamon"),
			new AreasNPCData([new DigimonNPCData([0x63429FA, 0x6342A54, 0x6342AAE], "MIHA03"),], "Bonus Area"),
			new AreasNPCData([new DigimonNPCData([0x9858930], "ICSA08"),], "WereGarurumon")]);
		}
	}

	void SetupNPC(bool vice)
	{
		NPCsData.AddRange(
			[new AreasNPCData(
				[
					new DigimonNPCData([0x9C9B44E, 0x9C9B4AE, 0x9C9B69E, 0x9C9B6FE], "MAYO00"),
					new DigimonNPCData([0x1420C5C, 0x1420CC2, 0x1420D22, 0x1420D8E, 0x1420DFA, 0x1420E66, 0x1420ECC, 0x1420F38], "MAYO01"),
					new DigimonNPCData([0x14D3A16, 0x14D3ACA, 0x14D3B24, 0x14D3B7E], "MAYO02"),
					new DigimonNPCData([0x157DD56, 0x157DDB6, 0x157DE76, 0x157DED0], "MAYO03"),
					new DigimonNPCData([0x16433F4, 0x164344E, 0x16434AE, 0x1643508], "MAYO04A"),
				]
				, "Native Forest"),
				new AreasNPCData(
				[
					new DigimonNPCData([0x18D6626, 0x18D66EC], "MAYO08A"),
					new DigimonNPCData([0x199D106, 0x199D16C, 0x199D1D8, 0x199D232, 0x199D298, 0x199D304], "MAYO08B")
				], "Digimon Bridge"),
				new AreasNPCData(
				[
					new DigimonNPCData([0x1BA6246, 0x1BA62AC, 0x1BA6306, 0x1BA636C], "TROP00"),
					new DigimonNPCData([0x1C018B2, 0x1C0190C, 0x1C01978, 0x1C019D2], "TROP01"),
					new DigimonNPCData([0x1CAB7EA, 0x1CAB85C, 0x1CAB8C8, 0x1CAB922, 0x1CABAC4, 0x1CABB30], "TROP02"),
					new DigimonNPCData([0x1D68A54, 0x1D68B08, 0x1D68B62, 0x1D68BBC], "TROP03")
				], "Tropical Jungle"),
				new AreasNPCData(
				[
					new DigimonNPCData([0x6580D9C, 0x6580E02], "TROP04"),
					new DigimonNPCData([0x6633BA6, 0x6633C06, 0x6633C72, 0x6633CDE, 0x6633D3E, 0x6633DAA], "TROP05")
				], "Mangrove Region"),
				new AreasNPCData(
				[
					new DigimonNPCData([0x67AC99A], "TUNN01"),
					new DigimonNPCData([0x6843EA6, 0x6844030, 0x684408A], "TUNN02")
				], "Drill Tunnel"),
				new AreasNPCData([new DigimonNPCData([0x6B4270E, 0x6B4276E, 0x6B427DA, 0x6B42964, 0x6B429BE, 0x6B42A1E, 0x6B42A8A, 0x6B42AE4], "DGHA01"),], "Overdell"),
				new AreasNPCData([new DigimonNPCData([0x6C08FBA, 0x6C0901A, 0x6C0907A, 0x6C090E6, 0x6C09146, 0x6C091B2], "DGHA02")], "Overdell Cemetery"),
				new AreasNPCData(
				[
					new DigimonNPCData([0x77489B2, 0x7748A0C], "YAKA02"),
					new DigimonNPCData([0x7AFA3C2, 0x7AFA41C, 0x7AFA476], "YAKA14"),
					new DigimonNPCData([0x7C6E44E], "YAKA16"),
					new DigimonNPCData([0x7D0A3AE], "YAKA17")
				], "Grey's Lord Mansion"),
				new AreasNPCData([new DigimonNPCData([0x6CCE592, 0x6CCE5F8, 0x6CCE658, 0x6CCE6BE], "GCAN01"),], "Great Canyon Entrance"),
				new AreasNPCData(
				[
					new DigimonNPCData([0x6D9CA06, 0x6D9CA7E, 0x6D9CAF6, 0x6D9CB6E, 0x6D9CBE6, 0x6D9CC5E], "GCAN02"),
					new DigimonNPCData([0x6E0EF4E, 0x6E0EFBA, 0x6E0F01A, 0x6E0F07A, 0x6E0F0E6], "GCAN03"),
					new DigimonNPCData([0x7195A22, 0x7195A7C, 0x7195AD6], "GCAN08_1"),
					new DigimonNPCData([0xA5B5E3A, 0xA5B5E94, 0xA5B5EEE], "GCAN08_2"),
					new DigimonNPCData([0x723F662, 0x723F6C2, 0x723F728, 0x723F788], "GCAN09")
				], "Great Canyon Top Area"),
				new AreasNPCData(
				[
					new DigimonNPCData([0x6ED3D9C], "GCAN04"),
					new DigimonNPCData([0xA4F0BA8], "GCAN04_2")
				], "Great Canyon Bridge"),
				new AreasNPCData(
				[
					new DigimonNPCData([0x7070BC6, 0x7070C2C, 0x7070C86, 0x7070CE6, 0x7070D4C, 0x7070DA6], "GCAN06"),
					new DigimonNPCData([0x70FE5D6, 0x70FE630, 0x70FE696, 0x70FE6F0, 0x70FE74A, 0x70FE7B0], "GCAN07")
				], "Great Canyon Bot. Area"),
				new AreasNPCData([new DigimonNPCData([0x8B16304, 0x8B16370, 0x8B163CA, 0x8B16436], "KODA00"),], "Ancient Dino Region"),
				new AreasNPCData([new DigimonNPCData([0x8B846D2, 0x8B84738, 0x8B8479E, 0x8B847F8, 0x8B8485E, 0x8B848C4], "KODA01"),], "Ancient Glacial Region"),
				new AreasNPCData(
				[
					new DigimonNPCData([0x8D464D2, 0x8D46538, 0x8D46598, 0x8D465F2, 0x8D46658, 0x8D466B8], "KODA04"),
					new DigimonNPCData([0x8F3E146, 0x8F3E1B2, 0x8F3E34E, 0x8F3E3BA], "KODA08")
				], "Ancient Speedy Region"),
				new AreasNPCData(
				[
					new DigimonNPCData([0x615FE1E, 0x615FE78, 0x6160002, 0x6160062, 0x61600C2, 0x616011C, 0x6160176, 0x61601D6], "MIHA00"),
					new DigimonNPCData([0x62A221E, 0x62A2284, 0x62A22F0, 0x62A235C], "MIHA02")
				], "Path Thru Mt. Panorama"),
				new AreasNPCData([new DigimonNPCData([0x61EF122, 0x61EF182, 0x61EF1E2], "MIHA01"),], "Mt. Panorama Plains"),
				new AreasNPCData(
				[
					new DigimonNPCData([0x6407EFA, 0x6407F66, 0x6407FD8, 0x640803E, 0x64080B0, 0x640811C, 0x640818E, 0x64081F4], "MIHA04A"),
					new DigimonNPCData([0x64BADB6, 0x64BAE22, 0x64BAE94, 0x64BAEFA, 0x64BB09C, 0x64BB108, 0x64BB17A, 0x64BB1E0], "MIHA04B")
				], "Foot of Mt. Panorama"),
				new AreasNPCData(
				[
					new DigimonNPCData([0x7541886, 0x75418E6, 0x7541946], "OGRE00"),
					new DigimonNPCData([0x75A623E, 0x75A629E, 0x75A62FE], "OGRE01"),
					new DigimonNPCData([0x76B0F7A, 0x76B0FD4], "OGRE03")
				], "Ogre Fortress"),
				new AreasNPCData(
				[
					new DigimonNPCData([0x7DF0324, 0x7DF037E, 0x7DF03DE, 0x7DF044A], "GIAS00"),
					new DigimonNPCData([0x7EA7F7A, 0x7EA7FDA, 0x7EA8040, 0x7EA80B2, 0x7EA8112, 0x7EA8178], "GIAS01"),
					new DigimonNPCData([0x7F6E396, 0x7F6E3F6, 0x7F6E462, 0x7F6E4BC, 0x7F6E51C, 0x7F6E588], "GIAS02"),
					new DigimonNPCData([0x800E866, 0x800E8CC, 0x800E92C, 0x800E992], "GIAS03"),
					new DigimonNPCData([0x80D4BDC, 0x80D4D6C, 0x80D4DD8, 0x80D4E44, 0x80D4EA4, 0x80D4F10], "GIAS04"),
					new DigimonNPCData([0x819AB92, 0x819ABF2, 0x819AC52, 0x819ACB2, 0x819AD12, 0x819AD72], "GIAS05"),
					new DigimonNPCData([0x8836646, 0x88366B2, 0x883671E, 0x883678A], "GIAS06A"),
					new DigimonNPCData([0x88E959C, 0x88E9602,], "GIAS07"),
					new DigimonNPCData([0x899CB36, 0x899CB9C, 0x899CC02, 0x899CC68, 0x899CCCE, 0x899CD34], "GIAS08"),
				], "Gear Savanna"),
				new AreasNPCData([new DigimonNPCData([0x2D4BF48, 0x2D4BFA2, 0x2D4C008, 0x2D4C062, 0x2D4C0BC, 0x2D4C122], "GOMI01"),], "Trash Mountain"),
				new AreasNPCData(
				[
					new DigimonNPCData([0x2497646, 0x24976A6, 0x249770C, 0x249776C, 0x24977CC, 0x2497832], "STIC01"),
					new DigimonNPCData([0x254AA4E, 0x254AAA8, 0x254AB02, 0x254AB62], "STIC02")
				], "Geko Swamp"),
				new AreasNPCData(
				[
					new DigimonNPCData([0x86BCF7E, 0x86BCFD8, 0x86BD038, 0x86BD092], "FRLZ01"),
					new DigimonNPCData([0x87700E6, 0x8770146, 0x87701A6, 0x8770206], "FRLZ02"),
					new DigimonNPCData([0x9004E22, 0x9004E8E, 0x9004EF4, 0x9004F60], "FRLZ03"),
					new DigimonNPCData([0x90B824E, 0x90B82B4, 0x90B831A, 0x90B8386, 0x90B83EC, 0x90B8452], "FRLZ04"),
					new DigimonNPCData([0x919D74A, 0x919D7AA, 0x919D80A, 0x919D864, 0x919D8C4, 0x919D924], "FRLZ06"),
					new DigimonNPCData([0x92475CE, 0x924762E, 0x9247694, 0x92476F4], "FRLZ07"),
					new DigimonNPCData([0x9303D68, 0x9303DCE, 0x9303E2E, 0x9303E94], "FRLZ08"),
					new DigimonNPCData([0x93BFFF6, 0x93C0056, 0x93C00BC, 0x93C011C], "FRLZ12"),
					new DigimonNPCData([0xA346156, 0xA3461B6, 0xA346216, 0xA346276], "FRLZ13"),
					new DigimonNPCData([0x1E0A2C4, 0x1E0A378], "FRLZ16")
				], "Freezeland"),
				new AreasNPCData(
				[
					new DigimonNPCData([0x94F2E2A, 0x94F2EA2, 0x94F303E], "ICSA02"),
					new DigimonNPCData([0x963383E, 0x9633898, 0x96338F2], "ICSA04"),
					new DigimonNPCData([0x96CA9A2, 0x96CAA14, 0x96CAA7A, 0x96CAAE6], "ICSA05"),
					new DigimonNPCData([0x9774054, 0x97740AE, 0x9774108], "ICSA06"),
					new DigimonNPCData([0x97C191E, 0x97C1984], "ICSA07")
				], "Ice Sanctuary"),
				new AreasNPCData(
				[
					new DigimonNPCData([0x9EEBC16, 0x9EEBC76, 0x9EEBCD0, 0x9EEBD30, 0x9EEBD90, 0x9EEBDEA], "MIST01"),
					new DigimonNPCData([0x9FAF956, 0x9FAF9BC, 0x9FAFA22, 0x9FAFA88, 0x9FAFAE8, 0x9FAFB4E, 0x9FAFCDE], "MIST02"),
					new DigimonNPCData([0xA073802, 0xA073992, 0xA0739F2, 0xA073A52, 0xA073AB8, 0xA073B18, 0xA073B7E], "MIST03"),
					new DigimonNPCData([0xA1376B2, 0xA137712, 0xA137772, 0xA1377D2, 0xA137832], "MIST04"),
					new DigimonNPCData([0xA7EEAD0, 0xA7EEB36, 0xA7EEB96, 0xA7EEBFC], "MIST06"),
					new DigimonNPCData([0xA86D5FE, 0xA86D658, 0xA86D6B2, 0xA86D70C, 0xA86D896, 0xA86D8F0, 0xA86D94A, 0xA86D9A4], "MIST07")
				], "Misty Trees"),
				new AreasNPCData([new DigimonNPCData([0x991EF12, 0x991EF6C, 0x991F0FC, 0x991F162, 0x991F1C8, 0x991F222, 0x991F282, 0x991F2E8], "BETL01"),], "Beetle Land"),
				new AreasNPCData(
				[
					new DigimonNPCData([0x26198D6, 0x2619936, 0x2619990, 0x2619B20], "FACT01"),
					new DigimonNPCData([0x26D58FE, 0x26D595E, 0x26D59B8, 0x26D5A18], "FACT02"),
					new DigimonNPCData([0x279B38A, 0x279B3F0, 0x279B450, 0x279B4AA, 0x279B510, 0x279B570], "FACT03"),
					new DigimonNPCData([0x2860346, 0x28603A0, 0x2860406, 0x286046C, 0x28604C6, 0x286052C], "FACT04"),
					new DigimonNPCData([0x29924EE, 0x2992548, 0x29925AE, 0x2992608, 0x2992662, 0x29926C8], "FACT07"),
					new DigimonNPCData([0x2B7EC76, 0x2B7ECD0, 0x2B7ED30, 0x2B7ED8A, 0x2B7EDEA, 0x2B7EE50], "FACT09"),
					new DigimonNPCData([0x2C15F42, 0x2C15F9C, 0x2C15FF6], "FACT10")
				], "Factorial Town"),
				new AreasNPCData(
				[
					new DigimonNPCData([0x23FA8A4, 0x23FA8FE, 0x23FA958], "OMOC05"),
					new DigimonNPCData([0x2449580, 0x24495DA], "OMOC06"),
					new DigimonNPCData([0x2DF66A4, 0x2DF66FE, 0x2DF6758], "OMOC07")
				], "Toy Mansion"),
				new AreasNPCData([new DigimonNPCData([0x8312968, 0x83129C2, 0x8312A1C, 0x8312A76, 0x8312AD6], "SAIB02"),], "Underground Lab"),
				new AreasNPCData(
				[
					new DigimonNPCData([0x2F3084E, 0x2F308A8, 0x2F30902, 0x2F3095C, 0x2F309B6], "MGEN01"),
					new DigimonNPCData([0x2FBE59E, 0x2FBE5FE, 0x2FBE66A, 0x2FBE6CA, 0x2FBE724], "MGEN02"),
					new DigimonNPCData([0x4FD240E, 0x4FD246E, 0x4FD24C8, 0x4FD2522, 0x4FD257C], "MGEN03"),
					new DigimonNPCData([0x506015E, 0x50601B8, 0x5060212, 0x50602C6], "MGEN04"),
					new DigimonNPCData([0x50EDEAE, 0x50EDF0E, 0x50EDF68], "MGEN05"),
					new DigimonNPCData([0x5EBE83E, 0x5EBE898, 0x5EBE8F2, 0x5EBE94C, 0x5EBE9A6], "MGEN06"),
					new DigimonNPCData([0x5F02DCE, 0x5F02E28, 0x5F02E82], "MGEN07"),
					new DigimonNPCData([0x5F90B1E, 0x5F90B78, 0x5F90BD2, 0x5F90C2C], "MGEN08"),
					new DigimonNPCData([0x601E86E, 0x601E8C8, 0x601E928], "MGEN09"),
					new DigimonNPCData([0x60AC5BE, 0x60AC678, 0x60AC618, 0x60AC738, 0x60AC6D2, 0x60AC798 ], "MGEN10")
				], "Mt. Infinity"),
				new AreasNPCData(
				[
					new DigimonNPCData([0x56448EE, 0x5644948, 0x56449A2, 0x56449FC], "MGEN11"),
					new DigimonNPCData([0x56D263E, 0x56D2698, 0x56D26F2, 0x56D274C], "MGEN12"),
					new DigimonNPCData([0x576038E, 0x57603E8, 0x5760442, 0x576049C], "MGEN13")
				], "Back Dimension")
		]);


	}

	bool CheckIfECC(long position)
	{
		position = position - 24;
		position = position % 0x930;

		if (position >= 0x800)
			return true;
		return false;
	}		 
}
