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
		public List<byte> Attacks { get; set; }

		public Texture2D digimonSprite { get; set; }
	}
	[Export] AtlasTexture ItemSprites;
	[Export] Texture2D WeirdSlime;

	[Export] AtlasTexture TechSprites;

	[Export] ItemsStuff itemsScript;

	//[Export] ItemsStuff digimonScript;

	//[Export] ItemsStuff evolutionScript;

	//[Export] ItemsStuff techsScript;
	private Texture2D[] itemsTex = new Texture2D[128];

	private Texture2D[] typeSprites = new Texture2D[7];

	private DigimonData[] digimonData = new DigimonData[180];

	bool Maeson = false, vanilla = false;

	System.IO.Stream bin;
	BinaryReader reader;
	string filePath;

	

	uint[] chestItemOffsets =
		{
			/*MAYO10*/   0x13FE3119,
			/*TROP01*/   0x13FE6845,
			/*MIHA03*/   0x13FEE011, //This is the Machi/Devil Coder 
			/*TUNN08*/   0x13FF4DE9, 0x13FF4DF5, //Meramon room
			/*TUNN10*/   0x13FF6979, 0x13FF6985,
			/*GCAN04*/   0x13FFA099, //Shellmon mountain down
			/*OGRE00*/   0x13FFD7BD,
			/*OGRE01*/   0x13FFE0F1,
			/*OGRE03*/   0x13FFF35D,
			/*YAKA02*/   0x14000EDD, 0x14000EE9, 0x14000EF5,
            /*YAKA12*/   0x14003399, 0x140033A5,
			/*YAKA14*/   0x14005869,
			/*YAKA17*/   0x140073E9, 0x140073F5,
			/*YAKA23*/   0x14008F7D,
			/*ISCA02*/   0x14021169, 0x14021175,
            /*ISCA05*/   0x14022D05,
			/*ISCA06*/   0x14023625, 0x14023631,
			/*ISCA07*/   0x14023F55, 0x14023F61,
			/*LEOM02*/   0x14030965,
			/*TUNN07_3*/ 0x140377A9,
			/*TUNN08_2*/ 0x140380D5, 0x140380E1,  //Meramon screen lava hardened
			/*TUNN08_3*/ 0x14038A05,
			/*GCAN04_2*/ 0x14039339, //Shellmon screen mountain up
			/*OGRE04*/   0x1403AEC5, 0x1403AED1, 0x1403AEDD, 0x1403AEE9,
			/*OGRE11*/   0x14045425,
            /*OMOC08*/   0x1404A6DD,
			/*FACT09*/   0x140539ED, 0x140539F9,
			/*FACT10*/   0x1405430D, 0x14054319, 0x14054325,
			/*MGEN01*/   0x14058371,  //different from vanilla
			/*MGEN02*/   0x14058C9D,
            /*MGEN03*/   0x14067B7D,
			/*MGEN04*/   0x1406970D,
			/*MGEN05*/   0x14073335,
			/*MGEN11*/   0x14078F1D,
			/*MGEN12*/   0x14079849, 0x14079855, 0x14079861, 0x1407986D,
            /*MGEN13*/   0x1407A179, 0x1407A185,
			/*YAKA25*/   0x1407AA95, 0x1407AAA1, 0x1407AAAD, 0x1407AAB9,
			/*MIHA06*/   0x1407BD47, 0x1407BD53, 0x1407BD5F,
			/*MGEN06*/   0x1407F431,
			/*MGEN07*/   0x1407FD55,
			/*MGEN08*/   0x14080689,
			/*MGEN09*/   0x14080FB5,
			/*MGEN10*/   0x140818F5, 0x14081901
		};

	uint[] tournamentItemsOffsets =
		{
			0x140A6ED4, 0x140A6F22,//Losing reward

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
			0x140A8B04, 0x140A8B52, //Humanoid Cup Reward

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
			0x140AA626, 0x140AA674, //Humanoid Cup Second prize

            0x140266B0, 0x14026710, //Bug losing prize
			0x140267E8, 0x14026826, //Bug tournament reward
			0x1402690A, 0x14026948, 0x14026A1A, 0x14026A58  //Bug tournament second prize

		};

		uint[] curlingRewardsOffsets =
		{
			0x14096AF0, 0x14096B08, 0x14096B20, 0x14096B38, 0x14096B50, 0x14096B68, //Penguinmon
			0x1409739A, 0x140973B2, 0x140973CA, 0x140973E2, 0x140973FA, 0x14097412, 0x1409742A, 0x14097442 //MetalMamemon
		};

		uint[] DrimogemonTreasureOffsets =
		{
			0x14096AF0, 0x14096B08, 0x14096B20, 0x14096B38, 0x14096B50, 0x14096B68, //Penguinmon
			0x1409739A, 0x140973B2, 0x140973CA, 0x140973E2, 0x140973FA, 0x14097412, 0x1409742A, 0x14097442 //MetalMamemon
		};
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{


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
				ItemSprites.Region = new Rect2(i * 24, j * 24, 24, 24);
				itemsTex[(i * 16) + j] = ImageTexture.CreateFromImage(ItemSprites.GetImage());
			}
		}

		uint ItemInitialOffset = 0x14D6E9FD, currentOffset = ItemInitialOffset;
		bin.Position = ItemInitialOffset;
		for (int i = 0; i < 180; i++)
		{
			digimonData[i] = new DigimonData();
			int value = bin.ReadByte();
			currentOffset = currentOffset + 0x34;
			if (CheckIfECC((int)currentOffset))
			{
				currentOffset = currentOffset + 0x130;
				bin.Position = bin.Position + 0x130;
			}

			digimonData[i].itemID = value;
			value = bin.ReadByte();
			if (CheckIfECC((int)currentOffset))
				currentOffset = currentOffset + 0x130;
			digimonData[i].itemChance = value;

			bin.Position = currentOffset;
		}

		currentOffset = 0x14D6E9DC;
		for (int i = 0; i < 180; i++)
		{
			bin.Position = currentOffset;

			digimonData[i].name = System.Text.Encoding.Default.GetString(reader.ReadBytes(20));


			if (i > 127)
			{
				if (i < 177)
					digimonData[i].name = digimonData[i].name + " NPC";
				else if (i == 177)
					digimonData[i].name = digimonData[i].name + " Mansion";
				else
					digimonData[i].name = digimonData[i].name + " Arena";
			}
			else if (i == 125)
			{
				digimonData[i].name = digimonData[i].name + " Shop";
			}
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
		digimonData[179].digimonSprite = parent.GetDigimonTexture(344, 0);

		itemsScript.SetupData(bin, reader, this, !Maeson && !vanilla);

		reader.Close();
		reader.Dispose();
		bin.Close();
		bin.Dispose();
	}

	public Texture2D GetItemTex(int id) {return itemsTex[id];}
	public DigimonData GetDigimonData(int id) {return digimonData[id];}


	
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
}
