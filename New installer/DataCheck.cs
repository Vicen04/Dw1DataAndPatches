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

	[Export] Button ItemsActive;
	[Export] Button DigimonActive;
	[Export] Button EvolutionActive;
	[Export] Button TechsActive;
	private Texture2D[] itemsTex = new Texture2D[128];

	private Texture2D[] typeSprites = new Texture2D[7];

	private DigimonData[] digimonData = new DigimonData[180];

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
	public DigimonData GetDigimonData(int id) { return digimonData[id]; }
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
	}

	void ItemsPressed()
	{
		itemsScript.Visible = true;
		techsScript.Visible = false;
		evolutionScript.Visible = false;
		digimonScript.Visible = false;
	}

	void TechsPressed()
	{
		itemsScript.Visible = false;
		techsScript.Visible = true;
		evolutionScript.Visible = false;
		digimonScript.Visible = false;
	}

	void EvoPressed()
	{
		itemsScript.Visible = false;
		techsScript.Visible = false;
		evolutionScript.Visible = true;
		digimonScript.Visible = false;
	}
	
	void DigimonPressed()
	{
		itemsScript.Visible = false;
		techsScript.Visible = false;
		evolutionScript.Visible = false;
		digimonScript.Visible = true;
	}
}
