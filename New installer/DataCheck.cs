using Godot;
using System;
using System.IO;
using System.Collections.Generic;

public partial class DataCheck : Control
{
	public class DigimonData
	{
		public string name { get; set; }
		public int itemID { get; set; }
		public int itemChance { get; set; }
		public int[] Types { get; set; }
		public List<byte> Attacks { get; set; }
		public Texture2D digimonSprite { get; set; }
	}
	[Export] AtlasTexture ItemSprites;
	[Export] Texture2D WeirdSlime;

	[Export] AtlasTexture TechSprites;

	[Export] ItemsStuff itemsScript;

	[Export] DigimonStuff digimonScript;

	[Export] EvolutionStuff evolutionScript;

	[Export] TechStuff techsScript;

	[Export] MapsStuff mapsScript;

	[Export] Button ItemsActive;
	[Export] Button DigimonActive;
	[Export] Button EvolutionActive;
	[Export] Button TechsActive;
	[Export] Button MapsActive;
	private Texture2D[] itemsTex = new Texture2D[128];

	private Texture2D[] typeSprites = new Texture2D[7];

	private DigimonData[] digimonData = new DigimonData[180];
	private int[] AreaNamesID = new int[256];
	private string[] AreaNames = new string[256];	

	bool Maeson = false, vanilla = false;

	System.IO.Stream bin;
	BinaryReader reader;
	string filePath;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ItemsActive.Text = Tr("Items_T");
		EvolutionActive.Text = Tr("Evolution_T");
		TechsActive.Text = Tr("Techniques_T");
		MapsActive.Text = Tr("Maps_T");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void StartDataCheck(string path, ToolsHandler parent)
	{
		filePath = path;
		try
		{
			bin = System.IO.File.Open(filePath, FileMode.Open, System.IO.FileAccess.ReadWrite);
		}
		catch (System.ArgumentException ex)
		{
			GD.Print(ex.Message);

		}
		catch (System.IO.FileNotFoundException ex)
		{
			GD.Print(ex.Message);

		}
		catch (System.IO.IOException ex)
		{
			GD.Print(ex.Message);

		}
		reader = new BinaryReader(bin);

		try
		{
			SetupData(parent);
		}
		catch (System.ArgumentException ex)
		{
			GD.Print(ex.Message);
			if (reader != null)
				reader.Dispose();
			bin.Dispose();

		}
		catch (System.IO.FileNotFoundException)
		{
			GD.Print("file not found");
			if (reader != null)
				reader.Dispose();
			bin.Dispose();

		}
		catch (System.IO.IOException ex)
		{
			GD.Print(ex.Message);
			if (reader != null)
				reader.Dispose();
			bin.Dispose();

		}
	}


	void SetupData(ToolsHandler parent)
	{
		for (int i = 0; i < 8; i++)
		{
			for (int j = 0; j < 16; j++)
			{
				ItemSprites.Region = new Rect2(j * 24, i * 24, 24, 24);
				itemsTex[(i * 16) + j] = ImageTexture.CreateFromImage(ItemSprites.GetImage());
			}
		}

		uint ItemInitialOffset = 0x14D6E9FA, currentOffset = ItemInitialOffset;
		bin.Position = ItemInitialOffset;
		for (int i = 0; i < 180; i++)
		{
			digimonData[i] = new DigimonData();
			digimonData[i].Types = new int[3];
			for (int j = 0; j < 3; j++)
			{
				digimonData[i].Types[j] = bin.ReadByte();
			}

			int value = bin.ReadByte();
			if (CheckIfECC((int)bin.Position))
			{
				currentOffset = currentOffset + 0x130;
				bin.Position = bin.Position + 0x130;
			}

			digimonData[i].itemID = value;
			value = bin.ReadByte();

			digimonData[i].Attacks = new List<byte>();

			if (bin.Position != 0x14D6F452)
				for (int j = 0; j < 16; j++)
				{
					int attackID = bin.ReadByte();
					if (attackID == 0xFF)
						break;
					digimonData[i].Attacks.Add((byte)attackID);
				}
			else
			{
				for (int j = 0; j < 6; j++)
				{
					int attackID = bin.ReadByte();
					digimonData[i].Attacks.Add((byte)attackID);
				}
				digimonData[i].Attacks.AddRange([0xE, 0xF, 0x46]);
			}

			currentOffset = currentOffset + 0x34;
			if (CheckIfECC((int)currentOffset))
				currentOffset = currentOffset + 0x130;
			digimonData[i].itemChance = value;

			bin.Position = currentOffset;


		}

		currentOffset = 0x14D6E9DC;
		for (int i = 0; i < 180; i++)
		{
			bin.Position = currentOffset;
			
			if (i != 85)
				digimonData[i].name = System.Text.Encoding.Default.GetString(reader.ReadBytes(20));
			else
				digimonData[i].name = "Piddomon";


			/*if (i > 127)
			{			
				if (i < 177)
					digimonData[i].name = "NPC " + digimonData[i].name;
				else if (i == 177)
					digimonData[i].name = "Mansion " + digimonData[i].name;
				else
					digimonData[i].name = "Arena " + digimonData[i].name;
			}
			else if (i == 125)
			{
				digimonData[i].name = "Shop " + digimonData[i].name;
			}*/
			currentOffset = currentOffset + 0x34;
			if (CheckIfECC((int)currentOffset))
				currentOffset = currentOffset + 0x130;

		}

		for (int i = 0; i < 8; i++)
		{
			for (int j = 0; j < 16; j++)
			{
				digimonData[(i * 16) + j].digimonSprite = parent.GetDigimonTexture(j * 32, i * 64);
			}
		}

		int extra = 4;

		for (int i = 0; i < 47; i++)
		{
			digimonData[i + 128].digimonSprite = digimonData[i + extra].digimonSprite;
			if (i == 10 || i == 22 || i == 34)
				extra = extra + 2;
		}

		bin.Position = 0x14D6F5C8;
		if (bin.ReadByte() == 86)
			digimonData[47].digimonSprite = parent.GetDigimonExtraTexture(0, 0);


		bin.Position = 0x14D6F908;
		if (bin.ReadByte() == 87)
		{
			bin.Position = 0x14D6F924;

			if (bin.ReadByte() != 3)
				digimonData[63].digimonSprite = digimonData[62].digimonSprite;
			else
				digimonData[63].digimonSprite = parent.GetDigimonExtraTexture(64, 0);
		}

		bin.Position = 0x14D6ED98;
		if (bin.ReadByte() != 3)
			digimonData[12].digimonSprite = parent.GetDigimonExtraTexture(32, 0);


		bin.Position = 0x14D6F8D5;
		int byteCheck = bin.ReadByte();
		if (byteCheck != 97)
		{
			if (byteCheck == 121)
				digimonData[62].digimonSprite = parent.GetDigimonTexture(64, 256);
			else if (byteCheck == 108)
				digimonData[62].digimonSprite = parent.GetDigimonTexture(288, 256);
		}
		else
			digimonData[62].digimonSprite = parent.GetDigimonTexture(96, 448);

		bin.Position = 0x14D688E4;

		if (bin.ReadByte() == 87)
		{
			itemsTex[126] = WeirdSlime;
		}

		for (int i = 0; i < 7; i++)
		{
			TechSprites.Region = new Rect2(i * 24, 0, 24, 24);
			typeSprites[i] = ImageTexture.CreateFromImage(TechSprites.GetImage());
		}

		digimonData[175].digimonSprite = digimonData[58].digimonSprite;
		digimonData[176].digimonSprite = digimonData[57].digimonSprite;
		digimonData[177].digimonSprite = digimonData[66].digimonSprite;
		digimonData[178].digimonSprite = parent.GetDigimonTexture(160, 0);
		digimonData[179].digimonSprite = parent.GetDigimonTexture(384, 0);

		bin.Position = 0x14D19840;
		if (bin.ReadByte() == 0x10)
			vanilla = true;
		else
			vanilla = false;


		//check if this is Maeson
		bin.Position = 0x14D19A84;
		if (bin.ReadByte() == 0x3E)
		{
			Maeson = true;
			vanilla = false;
		}
		else
			Maeson = false;
		
		if (!Maeson && !vanilla)
        {
            digimonData[136].digimonSprite = parent.GetDigimonTexture(64, 448);
			digimonData[148].digimonSprite = parent.GetDigimonTexture(128, 448);
        }

		int areaJump = 0x14D6AAD8, areaJump2 = 0x14D6B408;

		uint AreaDataOff = 0x14D6A5AC;	

		for (int i = 0; i < 255; i++)
		{
			bin.Position = AreaDataOff + i * 16;

			if (bin.Position > areaJump)
				bin.Position = bin.Position + 0x130;

			if (bin.Position > areaJump2)
				bin.Position = bin.Position + 0x130;

			AreaNames[i] = System.Text.Encoding.Default.GetString(reader.ReadBytes(10));

			bin.Position = AreaDataOff + i * 16 + 15;

			if (bin.Position > areaJump)
				bin.Position = bin.Position + 0x130;

			if (bin.Position > areaJump2)
				bin.Position = bin.Position + 0x130;

			AreaNamesID[i] = bin.ReadByte();
		}

		AreaNames[255] = "NO VALUE";
		AreaNamesID[255] = 0;

		if (!Maeson && !vanilla) AreaNames[255] = "ISCA08";

		itemsScript.SetupData(bin, reader, this, !Maeson && !vanilla);
		techsScript.SetupData(bin, reader, this, !Maeson && !vanilla);
		evolutionScript.SetupData(bin, reader, this, !Maeson && !vanilla);
		digimonScript.SetupData(bin, reader, this, !Maeson && !vanilla);

		reader.Close();
		reader.Dispose();
		bin.Close();
		bin.Dispose();
	}

	public Texture2D GetItemTex(int id) { if (id > 127) return null; return itemsTex[id]; }
	public DigimonData GetDigimonData(int id) { if (id > 179) return digimonData[0];  return digimonData[id]; }
	public Texture2D GetTechsSprites(int id) { if (id > 6) return null; return typeSprites[id]; }



	void JumpECC()
	{
		int position = (int)bin.Position;
		position = position - 24;
		position = position % 0x930;

		if (position >= 0x800)
			bin.Position = bin.Position + 0x130;
	}
	bool CheckIfECC()
	{
		int position = (int)bin.Position;
		position = position - 24;
		position = position % 0x930;

		if (position >= 0x800)
		{
			return true;
		}
		return false;
	}

	bool CheckIfECC(int position)
	{
		position = position - 24;
		position = position % 0x930;

		if (position >= 0x800)
		{
			return true;
		}
		return false;
	}

	void CloseDataCheck()
	{
		itemsScript.RestartData();
		techsScript.RestartData();
		evolutionScript.RestartData();
		digimonScript.RestartData();
		itemsScript.Visible = false;
		techsScript.Visible = false;
		evolutionScript.Visible = false;
		digimonScript.Visible = false;
		mapsScript.Visible = false;
		mapsScript.CloseMap();
	}

	void ItemsPressed()
	{
		itemsScript.Visible = true;
		techsScript.Visible = false;
		evolutionScript.Visible = false;
		digimonScript.Visible = false;
		mapsScript.Visible = false;	
	}

	void TechsPressed()
	{
		itemsScript.Visible = false;
		techsScript.Visible = true;
		evolutionScript.Visible = false;
		digimonScript.Visible = false;
		mapsScript.Visible = false;	
	}

	void EvoPressed()
	{
		itemsScript.Visible = false;
		techsScript.Visible = false;
		evolutionScript.Visible = true;
		digimonScript.Visible = false;
		mapsScript.Visible = false;	
	}
	
	void DigimonPressed()
	{
		itemsScript.Visible = false;
		techsScript.Visible = false;
		evolutionScript.Visible = false;
		digimonScript.Visible = true;	
		mapsScript.Visible = false;	
	}

	void MapsPressed()
	{
		itemsScript.Visible = false;
		techsScript.Visible = false;
		evolutionScript.Visible = false;
		digimonScript.Visible = false;
		mapsScript.Visible = true;
		mapsScript.CloseMap();
	}

	public int GetMapIDName(int value) {return AreaNamesID[value];}
	public string GetMapName(int value) {return AreaNames[value];}

	public string returnAreaName(int value, int area = 0)
	{
		if (area == 255)
		return "WereGarurumon secret";
		switch(value)
		{
			case 0:
			return "Native Forest";
			case 1:
			return "Coela Point";
			case 2:
			return "Dragon Eye Lake";
			case 3:
			return "Drill Tunnel Entrance";
			case 4:
			return "Digimon Bridge";
			case 5:
			return "Tropical Jungle";
			case 6:
			return "Mangrove Region";
			case 7:
			return "Path Thru Mt. Panorama";
			case 8:
			return "Entrance to File City";
			case 9:
			return "Mt. Panorama Plains";
			case 10:
			return "Foot of Mt. Panorama";
			case 11:
			return "Mt. Panorama Spore Area";
			case 12:
			return "Drill Tunnel";
			case 13:
			return "Drill Tunnel 2nd floor";
			case 14:
			return "Drill Tunnel 3rd floor";
			case 15:
			return "Residential Area";
			case 16:
			return "Underground Pond";
			case 17:
			return "Lava Cave";
			case 18:
			return "Overdell";
			case 19:
			return "Overdell Cemetery";
			case 20:
			return "Great Canyon Entrance";
			case 21:
			return "Great Canyon Top Area";
			case 22:
			return "Great Canyon Bridge";
			case 23:
			return "Fortress Entrance";
			case 24:
			return "Great Canyon Bot. Area";
			case 25:
			return "Ogre Fortress";
			case 26:
			return "Monochrome Shop";
			case 27:
			return "Grey Lord's Mansion";
			case 28:			
			return "Mansion Basement";
			case 29:
			return "Underground Lab";
			case 30:
			return "Gear Savanna";
			case 31:
			return "Ancient Dino Region";
			case 32:
			return "Ancient Glacial Region";
			case 33:
			return "Ancient Speedy Region";
			case 34:
			case 40:
			return "Freezeland";
			case 35:
			return "Ice Sanctuary";
			case 36:
			return "Green Gym";
			case 37:
			return "Leomon Ancestor's Cave";
			case 38:
			return "Misty Trees";
			case 39:
			return "Great Canyon";
			case 41:
			return "Geko Swamp";
			case 42:
			return "Volume Villa";
			case 43:
			return "File City";
			case 44:
			return "Item Keeper";
			case 45:
			return "Centar Clinic";
			case 46:
			return "Restaurant";
			case 47:
			return "Item Shop";
			case 48:
			return "Jijimon's house";
			case 49:
			return "Secret Item Shop";
			case 50:
			return "Toy Town";
			case 51:
			return "Secret Beach Cave";
			case 52:
			return "Factorial Town";
			case 53:
			return "Birdra Transport";
			case 54:
			return "Arena Lobby";
			case 55:
			return "Treasure Hunt";
			case 56:
			return "Trash Mountain";
			case 57:
			return "Sewer";
			case 58:
			return "Beetle Land";
			case 59:
			return "Mt. Infinity";
			case 60:
			return "Digimon Curling";
			case 61:
			return "Toy Mansion";
			case 62:
			return "Costume House";
			case 63:
			return "Robot House";
			case 64:
			return "Mansion 2nd floor";
			case 65:
			return "Mansion Attic";
			case 66:
			return "Tree?";
			case 67:
			return "Back Dimension";
			case 68:
			return "Kunemon's Bed";
			case 69:
			return "Amida Forest";
			default:
			return "Native Forest";
		}
	}
}
