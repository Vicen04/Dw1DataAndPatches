using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public partial class OtherItemsStuff : Control
{
	[Export] Button ItemsEffects;
	[Export] Button MiscItems;
	[Export] Label OtherItemsLabel;
	[Export] Label ItemsEffectsLabel;
	[Export] OptionButton ItemsEffectsOpt;
	[Export] OptionButton MiscItemsOpt;
	[Export] Control AllItemsEffects;
	[Export] Control AllMiscItems;
	[Export] Control BuffItems;
	[Export] Control HealingItems;
	[Export] Control Chips;
	[Export] Control Food;
	[Export] Control Curling;
	[Export] Control Treasure;
	[Export] Control Restaurant;

	[Export] Label foodItem;
	[Export] Label foodEnergy;
	[Export] Label foodWeight;
	[Export] Label foodTiredness;
	[Export] Label foodHappiness;
	[Export] Label foodDiscipline;
	[Export] Label foodLife;
	[Export] Label foodSickness;
	[Export] Label foodBuff;
	[Export] Label foodBuffValue;
	[Export] Label foodBuffTime;
	[Export] Label[] foodNames;
	[Export] TextureRect[] foodIcons;
	[Export] Label[] Weights;
	[Export] Label[] Energy;
	[Export] Label[] FoodEffects;
	[Export] Control SteakA;
	[Export] Control SteakB;
	[Export] TextureRect SteakBIcon;
	[Export] Label SteakBInfo;
	[Export] Label[] FoodBuffTypes;
	[Export] Label statItemslabel;
	[Export] Label[] StatsEffects;
	[Export] Label[] statItems;
	[Export] TextureRect[] statsIcons;
	[Export] Label boostItemslabel;
	[Export] Label[] BoostEffects;
	[Export] Label[] boostItems;
	[Export] Label[] OmniDisk;
	[Export] TextureRect[] boostIcons;
	[Export] Label healItemslabel;
	[Export] Label healHP;
	[Export] Label healMP;
	[Export] Label[] HealingEffects;
	[Export] Label[] healingItems;
	[Export] TextureRect[] healingIcons;
	[Export] Control healingSteak;
	[Export] Label curlingItemslabel;
	[Export] Label curlingItemslabel2;
	[Export] Label curlingChancelabel;
	[Export] Label curlingChancelabel2;
	[Export] Label[] curlingChances;
	[Export] Label[] curlingItems;
	[Export] TextureRect[] curlingIcons;
	[Export] Label Day3Plan;
	[Export] Label Day10Plan;
	[Export] Label TreasureChance;
	[Export] Label TreasureItem;
	[Export] Label TreasureChance2;
	[Export] Label TreasureItem2;
	[Export] Label NormalTreasure;
	[Export] Label GabumonTreasure;
	[Export] Label NormalTreasure2;
	[Export] Label GabumonTreasure2;
	[Export] Label[] DrimogemonChances;
	[Export] Label[] DrimogemonItems;
	[Export] TextureRect[] DrimogemonIcons;
	[Export] Label[] DrimogemonChancesGabu;
	[Export] Label[] DrimogemonItemsGabu;
	[Export] TextureRect[] DrimogemonIconsGabu;
	[Export] Label Meal;
	[Export] Label MealEnergy;
	[Export] Label MealWeight;
	[Export] Label MealTiredness;
	[Export] Label MealHappiness;
	[Export] Label MealDiscipline;
	[Export] TextureRect[] DigimonRestaurant;
	[Export] Label[] RestaurantEnergy;
	[Export] Label[] RestaurantWeight;
	[Export] Label[] RestaurantTiredness;
	[Export] Label[] RestaurantHappiness;
	[Export] Label[] RestaurantDiscipline;
	[Export] Label[] RestaurantHP;
	[Export] Label[] RestaurantMP;
	[Export] Label[] RestaurantOff;
	[Export] Label[] RestaurantDef;
	[Export] Label[] RestaurantSpd;
	[Export] Label[] RestaurantBrn;


	enum CHIPBYTES
	{
		AUTOPILOT = 76,
		OFFCHIP = 124,
		DEFCHIP = 140,
		BRAINCHIP = 156,
		QUICKCHIP = 172,
		HPCHIP = 188,
		MPCHIP = 204,
		DVCHIPA = 220,
		DVCHIPD = 0,
		DVCHIPE = 36,
		PORTAPOTTY = 72
	}

	enum DISKBYTES
	{
		OFF = 60,
		DEF = 160,
		SPD = 4,
		OMNI = 104,
		SOFF = 232,
		SDEF = 76,
		SSPD = 176,
	}

	enum SUPERDISKBYTES
	{
		OFF = 244,
		DEF = 88,
		SPD = 188,
		OMNI = 32,
		SOFF = 64,
		SDEF = 164,
		SSPD = 8,
	}


	uint[] curlingRewardsOffsets =
	{
		0x14096AF0, 0x14096B08, 0x14096B20, 0x14096B38, 0x14096B50, 0x14096B68, //Penguinmon
		0x1409739A, 0x140973B2, 0x140973CA, 0x140973E2, 0x140973FA, 0x14097412, 0x1409742A, 0x14097442 //MetalMamemon
	};

	uint[] curlingRewardsChances =
	{
		0x14096AE7, 0x14096AFF, 0x14096B17, 0x14096B2F, 0x14096B47, 0x14096B5F, //Penguinmon
		0x14097391, 0x140973A9, 0x140973C1, 0x140973D9, 0x140973F1, 0x14097409, 0x14097421, 0x14097439 //MetalMamemon
	};

	uint[] DrimogemonTreasureOffsets =
	{
		0x140672E8, 0x14067300, 0x14067318, 0x14067330, 0x14067348, 0x14067360, 0x14067378, 0x14067390, 0x140673A8, 0x140673C0, 0x140673D8, 0x140673F0, //3 day normal
		0x140675A2, 0x140675BA, 0x140675D2, 0x140675EA, 0x14067602, 0x1406761A, 0x14067632, 0x1406764A, 0x14067662, 0x1406767A, 0x14067692, 0x140676AA,// 10 days normal
	};

	uint[] DrimogemonTreasureOffsetsGabu =
	{
		0x14067408, 0x14067420, 0x14067438, 0x14067450, 0x14067468, 0x14067480, 0x14067498, 0x140674B0, 0x140674C8, 0x140674E0, 0x140674F8, 0x14067510, //3 day Gabumon		
		0x140676C2, 0x140676DA, 0x140676F2, 0x1406770A, 0x14067722, 0x1406773A, 0x14067752, 0x1406776A, 0x14067782, 0x1406779A, 0x140677B2, 0x140677CA // 10 days Gabumon
	};

	uint[] DrimogemonTreasureChances =
	{
		0x140672DF, 0x140672F7, 0x1406730F, 0x14067327, 0x1406733F, 0x14067357, 0x1406736F, 0x14067387, 0x1406739F, 0x140673B7, 0x140673CF, 0x140673E7,//3 day normal
		0x14067599, 0x140675B1, 0x140675C9, 0x140675E1, 0x140675F9, 0x14067611, 0x14067629, 0x14067641, 0x14067659, 0x14067671, 0x14067689, 0x140676A1, // 10 days normal
	};
	uint[] DrimogemonTreasureChancesGabu =
	{
		0x140673FF, 0x14067417, 0x1406742F, 0x14067447, 0x1406745F, 0x14067477, 0x1406748F, 0x140674A7, 0x140674BF, 0x140674D7, 0x140674EF, 0x14067507,//3 day Gabumon
		0x140676B9, 0x140676D1, 0x140676E9, 0x14067701, 0x14067719, 0x14067731, 0x14067749, 0x14067761, 0x14067779, 0x14067791, 0x140677A9, 0x140677C1 // 10 days Gabumon
	};

	uint offsetValuesFood = 0x14CF5C44;
	uint[] offsetEffects = { 0x14CF5E08, 0x14CF5E10, 0x14CF5E54, 0x14CF5E70, 0x14CF5EAC, 0x14CF5EC8, 0x14CF5EE8, 0x14CF5F2C, 0x14CF600C, 0x14CF6028, 0x14CF6048, 0x14CF6070, 0x14CF6070,
							 0x14CF6070, 0x14CF64D8, 0x14CF64F4, 0x14CF6520, 0x14CF6548, 0x14CF6548, 0x14CF6548, 0x14CF6590, 0x14CF6584, 0x14CF6584, 0x14CF65AC, 0x14CF65F4 },
	offsetWeight = { 0x14CF5DD4, 0x14CF5DF0, 0x14CF5E10, 0x14CF5E78, 0x14CF5ED0, 0x14CF5EF4, 0x14CF5F14, 0x14CF5F48, 0x14CF5FD8, 0x14CF6030, 0x14CF6054, 0x14CF6094, 0x14CF60BC,
					 0x14CF60D8, 0x14CF60FC, 0x14CF6124, 0x14CF6144, 0x14CF616C, 0x14CF61AC, 0x14CF630C, 0x14CF633C, 0x14CF636C, 0x14CF6388, 0x14CF63B0, 0x14CF63D0, 0x14CF63EC,
					 0x14CF6408, 0x14CF6498, 0x14CF64B4, 0x14CF64FC, 0x14CF652C, 0x14CF6558, 0x14CF65B4},
	offsetEnergy = { 0x14CF5DD0, 0x14CF5DEC, 0x14CF5E14, 0x14CF5E48, 0x14CF5EA0, 0x14CF5EF0, 0x14CF5F10, 0x14CF5F3C, 0x14CF5F70, 0x14CF6000, 0x14CF6050, 0x14CF6088, 0x14CF60B8,
					 0x14CF60D4, 0x14CF60F8, 0x14CF6120, 0x14CF6140, 0x14CF6168, 0x14CF618C, 0x14CF61BC, 0x14CF631C, 0x14CF634C, 0x14CF6384, 0x14CF63AC, 0x14CF63CC, 0x14CF63E8,
					 0x14CF6404, 0x14CF6430, 0x14CF64B0, 0x14CF64E8, 0x14CF6528, 0x14CF6554, 0x14CF6584, 0x14CF65F0},
	offsetsLifetimeChips = { 0x14CF68E0, 0x14CF6904, 0x14CF6928 },
	offsetsItemsStats = { 0x14CF5D64, 0x14CF5CEC, 0x14CF5CE0, 0x14CF5CD4, 0x14CF5CC8, 0x14CF5CBC, 0x14CF5CB0, 0x14CF5C80 };
	uint[] offsetStats = { 0x14CF6874, 0x14CF6884, 0x14CF6894, 0x14CF68A4, 0x14CF68B4, 0x14CF68C4, 0x14CF68D4, 0x14CF68D4, 0x14CF68F8, 0x14CF68F8, 0x14CF691C, 0x14CF691C, //chips
	 					   0x14CF5FCC, 0x14CF5FCC, 0x14CF5F60, 0x14CF5F60, 0x14CF5F60, 0x14CF5F60, 0x14CF618C, 0x14CF61BC, 0x14CF631C, 0x14CF634C, 0x14CF637C, 0x14CF63A4,
						   0x14CF648C, 0x14CF648C, 0x14CF6420, 0x14CF6420, 0x14CF6420, 0x14CF6420}; //food

	uint[] offsetHealing = { 0x14D77054, 0x14D77056, 0x14D77058, 0x14D7705A, 0x14D77054, 0x14D77056, 0x14D77058, 0x14CF6DA4, 0x14CF6DBC, 0x14CF6DA4, 0x14CF6DBC, 0x14CF6D00, 0x14CF6D00, //items
							 0x14CF60F0, 0x14CF6118, 0x14CF615C, 0x14CF615C, 0x14CF64CC, 0x14CF64CC, 0x14CF65D8, 0x14CF65E8}, //food

	offsetsHealingFood = { 0x14CF5D1C, 0x14CF5D10, 0x14CF5CF8, 0x14CF5C68, 0x14CF5C38, 0x14CF5C2C };
	uint offsetsPtrChips = 0x14D52C64;
	uint ptrOffsetBoost = 0x14D53558;
	uint[] offsetsBoost = { 0x14D294A8, 0x14D2950C, 0x14D29570,  0x14D294C8, 0x14D2952C, 0x14D29590,
							0x14D295F4, 0x14D29658, 0x14D296BC};

	uint[] offsetsRestaurant = { 0x140AD588, 0x140AD790, 0x140AD998, //Meramon
					   			 0x140ADDE6, 0x140ADFCC, 0x140AE1BA, //Tyrannomon
					   			 0x140AE602, 0x140AE7D0, 0x140AE9E2, //Garurumon
					   			 0x140AF0B6, 0x140AF262, 0x140AF412, //Frigimon
					   			 0x140AF7F8, 0x140AFA72, 0x140AFD02, //Vademon
					   			 0x140B01B8, 0x140B02D6, 0x140B03F6, //Boiled Egg
					   			 0x140B0686, 0x140B07D2, 0x140B0A28, //Omelet
					  			 0x140B0CBC, 0x140B0E10, 0x140B0F66 }; //Egg Bowl


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		FoodBuffTypes[0].Text = Tr("BuffFood");
		FoodBuffTypes[1].Text = Tr("BuffFood2");
		FoodBuffTypes[2].Text = Tr("AllBuffFood");
		SteakBInfo.Text = Tr("SteakSpInfo");
		Meal.Text = Tr("RestaurantMeal");
		ItemsEffects.Text = Tr("ItemEffectsCheck");
		MiscItems.Text = Tr("MiscItemsCheck");
		ItemsEffectsLabel.Text = Tr("ItemEffectsLabel");
		ItemsEffectsOpt.SetItemText(2, Tr("BuffItemsOpt"));
		ItemsEffectsOpt.SetItemText(0, Tr("HealingItemspt"));
		ItemsEffectsOpt.SetItemText(1, Tr("ChipsOpt"));
		ItemsEffectsOpt.SetItemText(3, Tr("FoodOpt"));
		MiscItemsOpt.SetItemText(0, Tr("CurlingCheckOpt"));
		MiscItemsOpt.SetItemText(1, Tr("TreasureCheckOpt"));
		MiscItemsOpt.SetItemText(2, Tr("RestaurantCheckOpt"));
		foodItem.Text = Tr("SearchItemCheckLabel");
		foodEnergy.Text = Tr("foodEnergyCheck");
		foodEnergy.TooltipText = Tr("foodEnergyCheckInfo");
		foodWeight.Text = Tr("foodWeightCheck");
		foodWeight.TooltipText = Tr("foodWeightCheckInfo");
		foodTiredness.Text = Tr("foodTirednessCheck");
		foodTiredness.TooltipText = Tr("foodTirednessCheckInfo");
		foodHappiness.Text = Tr("foodHappinessCheck");
		foodHappiness.TooltipText = Tr("foodHappinessCheckInfo");
		foodDiscipline.Text = Tr("foodDisciplineCheck");
		foodDiscipline.TooltipText = Tr("foodDisciplineCheckInfo");
		foodLife.Text = Tr("foodLifeCheck");
		foodLife.TooltipText = Tr("foodLifeCheckInfo");
		foodSickness.Text = Tr("foodSicknessCheck");
		foodSickness.TooltipText = Tr("foodSicknessCheckInfo");
		foodBuff.Text = Tr("foodBuffCheck");
		foodBuff.TooltipText = Tr("foodBuffCheckInfo");
		foodBuffValue.Text = Tr("foodBuffValueCheck");
		foodBuffValue.TooltipText = Tr("foodBuffValueCheckInfo");
		foodBuffTime.Text = Tr("foodBuffTimeCheck");
		foodBuffTime.TooltipText = Tr("foodBuffTimeCheckInfo");
		statItemslabel.Text = Tr("SearchItemCheckLabel");
		boostItemslabel.Text = Tr("SearchItemCheckLabel");
		healItemslabel.Text = Tr("SearchItemCheckLabel");
		healHP.Text = Tr("HealHPCheck");
		healMP.Text = Tr("HealMPCheck");
		curlingItemslabel.Text = Tr("ItemsSpawnSearchLabel");
		curlingItemslabel2.Text = Tr("ItemsSpawnSearchLabel");
		curlingChancelabel.Text = Tr("ChanceCheckOtherLabel");
		curlingChancelabel2.Text = Tr("ChanceCheckOtherLabel");
		MealEnergy.Text = Tr("foodEnergyCheck");
		MealEnergy.TooltipText = Tr("foodEnergyCheckInfo");
		MealWeight.Text = Tr("foodWeightCheck");
		MealWeight.TooltipText = Tr("restaurantWeightCheckInfo");
		MealTiredness.Text = Tr("foodTirednessCheck");
		MealTiredness.TooltipText = Tr("foodTirednessCheckInfo");
		MealHappiness.Text = Tr("foodHappinessCheck");
		MealHappiness.TooltipText = Tr("foodHappinessCheckInfo");
		MealDiscipline.Text = Tr("foodDisciplineCheck");
		MealDiscipline.TooltipText = Tr("foodDisciplineCheckInfo");
		TreasureChance.Text = Tr("ChanceCheckOtherLabel");
		TreasureChance2.Text = Tr("ChanceCheckOtherLabel");
		TreasureItem.Text = Tr("ItemsSpawnSearchLabel");
		TreasureItem2.Text = Tr("ItemsSpawnSearchLabel");
		NormalTreasure.Text = Tr("NormalTreasureCheck");
		NormalTreasure2.Text = Tr("NormalTreasureCheck");
		GabumonTreasure.Text = Tr("GabumonTreasureCheck");
		GabumonTreasure2.Text = Tr("GabumonTreasureCheck");
		OtherItemsLabel.Text = Tr("OtherItemsCheck");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetupData(System.IO.Stream bin, BinaryReader reader, DataCheck mainParent, ItemsStuff parent, bool vice)
	{
		//SetupFood
		bin.Position = 0x14CF5C2C;
		List<int> foodValues = [reader.ReadInt16()];
		for (int i = 0; i < 33; i++)
		{
			bin.Position = 12 * i + offsetValuesFood;
			foodValues.Insert(0, reader.ReadInt16());
		}

		for (int i = 0; i < foodValues.Count; i++)
		{
			if (i < offsetWeight.Length)
			{
				bin.Position = offsetWeight[i];
				Weights[i + 3].Text = reader.ReadInt16().ToString();
			}
			bin.Position = offsetEnergy[i];
			Energy[i + 3].Text = reader.ReadInt16().ToString();
			foodNames[i + 3].Text = parent.GetItemData(foodValues[i]).name;
			foodIcons[i + 3].Texture = mainParent.GetItemTex(foodValues[i]);
		}

		for (int i = 0; i < offsetEffects.Length; i++)
		{
			bin.Position = offsetEffects[i];
			FoodEffects[i + 3].Text = reader.ReadInt16().ToString();
		}
		for (int i = 0; i < offsetsLifetimeChips.Length; i++)
		{
			bin.Position = offsetsLifetimeChips[i];
			FoodEffects[i].Text = reader.ReadInt16().ToString();
		}

		for (int i = 0; i < 11; i++)
		{
			bin.Position = offsetsPtrChips + i * 4;
			int currentByte = bin.ReadByte();
			if (currentByte == 220)
			{
				foodNames[0].Text = parent.GetItemData(i + 22).name;
				foodIcons[0].Texture = mainParent.GetItemTex(i + 22);
			}
			else if (currentByte == 0)
			{
				foodNames[1].Text = parent.GetItemData(i + 22).name;
				foodIcons[1].Texture = mainParent.GetItemTex(i + 22);
			}
			else if (currentByte == 36)
			{
				foodNames[2].Text = parent.GetItemData(i + 22).name;
				foodIcons[2].Texture = mainParent.GetItemTex(i + 22);
			}
		}

		bin.Position = 0x14CF65F0;
		if (bin.ReadByte() == 0)
		{
			SteakA.Visible = false;
			SteakB.Visible = true;
			SteakBIcon.Texture = mainParent.GetItemTex(122);
			healingSteak.Visible = false;
		}

		bin.Position = 0x14CF6570;

		if (bin.ReadByte() != 3)
		{
			bin.Position = 0x14CF6548;
			FoodEffects[22].Text = reader.ReadInt16().ToString();

			bin.Position = 0x14CF6554;
			int effect = reader.ReadInt16();
			FoodEffects[20].Text = effect.ToString();
			FoodEffects[21].Text = (effect * 2).ToString();
			Energy[34].Text = effect.ToString();
		}

		//Setup chips
		for (int i = 0; i < 11; i++)
		{
			bin.Position = offsetsPtrChips + i * 4;
			int currentChip = GetChipValue((CHIPBYTES)bin.ReadByte());
			statItems[i].Text = parent.GetItemData(currentChip + 22).name;
			statsIcons[i].Texture = mainParent.GetItemTex(currentChip + 22);
		}

		for (int i = 0; i < 8; i++)
		{
			bin.Position = offsetsItemsStats[i];
			int currentFood = bin.ReadByte();
			statItems[i + 11].Text = parent.GetItemData(currentFood).name;
			statsIcons[i + 11].Texture = mainParent.GetItemTex(currentFood);
		}

		for (int i = 0; i < StatsEffects.Length; i++)
		{
			bin.Position = offsetStats[i];
			StatsEffects[i].Text = reader.ReadInt16().ToString();
		}

		//Setup healing

		for (int i = 0; i < offsetHealing.Length; i++)
		{
			if (i == 11)
				continue;
			bin.Position = offsetHealing[i];
			HealingEffects[i].Text = reader.ReadInt16().ToString();
		}

		bin.Position = offsetHealing[11];
		HealingEffects[11].Text = (reader.ReadInt16() / 2).ToString();


		for (int i = 0; i < 8; i++)
		{
			healingItems[i].Text = parent.GetItemData(i).name;
			healingIcons[i].Texture = mainParent.GetItemTex(i);
		}
		healingItems[8].Text = parent.GetItemData(9).name;
		healingIcons[8].Texture = mainParent.GetItemTex(9);

		healingItems[9].Text = parent.GetItemData(11).name;
		healingIcons[9].Texture = mainParent.GetItemTex(11);
		healingItems[10].Text = parent.GetItemData(12).name;
		healingIcons[10].Texture = mainParent.GetItemTex(12);

		for (int i = 0; i < offsetsHealingFood.Length; i++)
		{
			bin.Position = offsetsHealingFood[i];
			int foodHeal = bin.ReadByte();
			healingItems[i + 11].Text = parent.GetItemData(foodHeal).name;
			healingIcons[i + 11].Texture = mainParent.GetItemTex(foodHeal);
		}



		//Setup Boost
		bin.Position = 0x14D292E4;
		int[] OmniStats = { 0, 0, 0 };
		if (bin.ReadByte() == 0x40)
		{
			offsetsBoost = new uint[] { 0x14D294F0, 0x14D29554, 0x14D295B8, 0x14D2969C, 0x14D29700, 0x14D29764 };

			for (int i = 0; i < 7; i++)
			{
				bin.Position = i * 4 + ptrOffsetBoost;
				int currentBoost = GetDiskValue(bin.ReadByte(), false);
				boostItems[i].Text = parent.GetItemData(currentBoost + 15).name;
				boostIcons[i].Texture = mainParent.GetItemTex(currentBoost + 15);
				if (i < 3 && currentBoost != 3)
				{
					if (currentBoost > 2)
						currentBoost--;
					bin.Position = offsetsBoost[currentBoost];
					if (currentBoost % 4 == 0)
						OmniStats[0] = OmniStats[0] + reader.ReadInt16();
					else if (currentBoost % 4 == 1)
						OmniStats[1] = OmniStats[1] + reader.ReadInt16();
					else if (currentBoost % 4 == 2)
						OmniStats[2] = OmniStats[2] + reader.ReadInt16();
				}
			}
		}
		else
		{
			offsetsBoost = new uint[] { 0x14D294A8, 0x14D2950C, 0x14D29570, 0x14D295F4, 0x14D29658, 0x14D296BC };
			bin.Position = ptrOffsetBoost;

			for (int i = 0; i < 7; i++)
			{
				bin.Position = i * 4 + ptrOffsetBoost;
				int currentBoost = GetDiskValue(bin.ReadByte(), true);
				boostItems[i].Text = parent.GetItemData(currentBoost + 15).name;
				boostIcons[i].Texture = mainParent.GetItemTex(currentBoost + 15);
				if (i < 3 && currentBoost != 3)
				{
					if (currentBoost > 2)
						currentBoost--;
					bin.Position = offsetsBoost[currentBoost];
					if (currentBoost % 4 == 0)
						OmniStats[0] = OmniStats[0] + reader.ReadInt16();
					else if (currentBoost % 4 == 1)
						OmniStats[1] = OmniStats[1] + reader.ReadInt16();
					else if (currentBoost % 4 == 2)
						OmniStats[2] = OmniStats[2] + reader.ReadInt16();
				}
			}
		}



		for (int i = 0; i < offsetsBoost.Length; i++)
		{
			
			bin.Position = offsetsBoost[i];
			BoostEffects[i].Text = reader.ReadInt16().ToString();
		}			

		OmniDisk[0].Text = OmniStats[0].ToString();
		OmniDisk[1].Text = OmniStats[1].ToString();
		OmniDisk[2].Text = OmniStats[2].ToString();

		//curling
		int previousChance = -1;
		for (int i = 0; i < 14; i++)
		{
			if (i == 6)
				previousChance = -1;

			bin.Position = curlingRewardsOffsets[i];
			int item = bin.ReadByte();
			curlingItems[i].Text = parent.GetItemData(item).name;
			curlingIcons[i].Texture = mainParent.GetItemTex(item);
			bin.Position = curlingRewardsChances[i];
			int chance = bin.ReadByte();
			curlingChances[i].Text = ((chance - previousChance) * 2).ToString() + "%";
			previousChance = chance;
		}

		//Treasure Drimogemon

		bin.Position = 0x14066911;
		int multiplier = 2;
		if (bin.ReadByte() != 3)
		{
			multiplier = 1;
			Day3Plan.Text = Tr("1DayPlan");
			Day10Plan.Text = Tr("3DayPlan");
		}
		else
		{
			Day3Plan.Text = Tr("3DayPlan");
			Day10Plan.Text = Tr("10DayPlan");
		}

		previousChance = -1;
		int previousChanceGabu = -1;
		for (int i = 0; i < 24; i++)
		{
			if (i == 12)
			{
				previousChanceGabu = -1;
				previousChance = -1;
			}
			bin.Position = DrimogemonTreasureOffsets[i];
			int item = bin.ReadByte();
			DrimogemonItems[i].Text = parent.GetItemData(item).name;
			DrimogemonIcons[i].Texture = mainParent.GetItemTex(item);
			bin.Position = DrimogemonTreasureOffsetsGabu[i];
			item = bin.ReadByte();
			DrimogemonItemsGabu[i].Text = parent.GetItemData(item).name;
			DrimogemonIconsGabu[i].Texture = mainParent.GetItemTex(item);
			bin.Position = DrimogemonTreasureChances[i];
			int chance = bin.ReadByte();
			DrimogemonChances[i].Text = ((chance - previousChance) * multiplier).ToString() + "%";
			previousChance = chance;
			bin.Position = DrimogemonTreasureChancesGabu[i];
			chance = bin.ReadByte();
			DrimogemonChancesGabu[i].Text = ((chance - previousChanceGabu) * multiplier).ToString() + "%";
			previousChanceGabu = chance;
		}

		//RestaurantData
		int[] digimonRestaurant = { 133, 132, 144, 145, 150, 174, 174, 174 };

		for (int i = 0; i < offsetsRestaurant.Length; i++)
		{
			bin.Position = offsetsRestaurant[i];

			RestaurantOff[i].Text = GetRestaurantValue(bin, reader);

			RestaurantDef[i].Text = GetRestaurantValue(bin, reader);

			RestaurantSpd[i].Text = GetRestaurantValue(bin, reader);

			RestaurantBrn[i].Text = GetRestaurantValue(bin, reader);

			RestaurantHP[i].Text = GetRestaurantValue(bin, reader);

			RestaurantMP[i].Text = GetRestaurantValue(bin, reader);

			RestaurantTiredness[i].Text = GetRestaurantValue(bin, reader);

			RestaurantHappiness[i].Text = GetRestaurantValue(bin, reader);

			RestaurantDiscipline[i].Text = GetRestaurantValue(bin, reader);

			RestaurantEnergy[i].Text = GetRestaurantValue(bin, reader);

			RestaurantWeight[i].Text = GetRestaurantValue(bin, reader);

			DigimonRestaurant[i].Texture = mainParent.GetDigimonData(digimonRestaurant[i / 3]).digimonSprite;
		}

	}

	public void RestartData()
	{
		ItemsEffectsOpt.Selected = -1;
		MiscItemsOpt.Selected = -1;
		BuffItems.Visible = false;
		HealingItems.Visible = false;
		Chips.Visible = false;
		Food.Visible = false;
		Curling.Visible = false;
		Treasure.Visible = false;
		Restaurant.Visible = false;
		AllItemsEffects.Visible = false;
		AllMiscItems.Visible = false;
	}

	void SetOption(int option)
	{
		BuffItems.Visible = option == 2;
		HealingItems.Visible = option == 0;
		Chips.Visible = option == 1;
		Food.Visible = option == 3;
	}

	void SetOptionOther(int option)
	{
		Curling.Visible = option == 0;
		Treasure.Visible = option == 1;
		Restaurant.Visible = option == 2;
	}

	int GetChipValue(CHIPBYTES chipByte)
	{
		switch (chipByte)
		{
			case CHIPBYTES.AUTOPILOT:
				return 0;
			case CHIPBYTES.OFFCHIP:
				return 1;
			case CHIPBYTES.DEFCHIP:
				return 2;
			case CHIPBYTES.QUICKCHIP:
				return 4;
			case CHIPBYTES.BRAINCHIP:
				return 3;
			case CHIPBYTES.HPCHIP:
				return 5;
			case CHIPBYTES.MPCHIP:
				return 6;
			case CHIPBYTES.DVCHIPA:
				return 7;
			case CHIPBYTES.DVCHIPD:
				return 8;
			case CHIPBYTES.DVCHIPE:
				return 9;
			case CHIPBYTES.PORTAPOTTY:
				return 10;
			default:
				return 0;
		}
	}

	int GetDiskValue(int diskByte, bool super)
	{
		if (!super)
			switch ((DISKBYTES)diskByte)
			{
				case DISKBYTES.OFF:
					return 0;
				case DISKBYTES.DEF:
					return 1;
				case DISKBYTES.SPD:
					return 2;
				case DISKBYTES.OMNI:
					return 3;
				case DISKBYTES.SOFF:
					return 4;
				case DISKBYTES.SDEF:
					return 5;
				case DISKBYTES.SSPD:
					return 6;
				default:
					return 0;
			}
		else
		{
			switch ((SUPERDISKBYTES)diskByte)
			{
				case SUPERDISKBYTES.OFF:
					return 0;
				case SUPERDISKBYTES.DEF:
					return 1;
				case SUPERDISKBYTES.SPD:
					return 2;
				case SUPERDISKBYTES.OMNI:
					return 3;
				case SUPERDISKBYTES.SOFF:
					return 4;
				case SUPERDISKBYTES.SDEF:
					return 5;
				case SUPERDISKBYTES.SSPD:
					return 6;
				default:
					return 0;
			}
		}
	}

	string GetRestaurantValue(System.IO.Stream bin, BinaryReader reader)
	{
		int value = bin.ReadByte();
		bin.ReadByte();
		if (value == 53)
			return reader.ReadInt16().ToString();
		else
			return "-" + reader.ReadInt16().ToString();
	}

	void ItemEffectsSelected()
	{
		AllItemsEffects.Visible = true;
		AllMiscItems.Visible = false;
	}

	void MiscItemsSelected()
	{
		AllItemsEffects.Visible = false;
		AllMiscItems.Visible = true;
	}

}
