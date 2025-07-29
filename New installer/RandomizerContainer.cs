using Godot;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.Json;

public partial class RandomizerContainer : SubViewportContainer
{

	class RandoSaveData
	{
		public bool itemsSpawn { get; set; }
		public bool spawnRateItems { get; set; }
		public bool  itemDrops { get; set; }
		public bool  dropRate { get; set; }
		public bool  chests { get; set; }
		public bool  shops { get; set; }
		public bool  shopsPrices { get; set; }
		public bool  Mojyamon { get; set; }
		public bool  meritItems { get; set; }
		public bool  meritPrices { get; set; }
		public bool  tournamentItems { get; set; }
		public bool  keyItems { get; set; }
		public bool  tokomon { get; set; }
		public bool  curlingRewards { get; set; }
		public bool difficulty { get; set; }
		public bool  digimonNPC { get; set; }
		public bool  statsNPC { get; set; }
		public bool  techNPC { get; set; }
		public bool  moneyNPC { get; set; }
		public bool  bosses { get; set; }
		public bool  starter { get; set; }
		public bool  starterTech { get; set; }
		public bool  starterStats { get; set; }
		public bool  starterLevel { get; set; }
		public bool  tournamentNPC { get; set; }
		public bool  recruits { get; set; }
		public bool restaurant { get; set; }
		public bool  birdramon { get; set; }
		public bool  boost { get; set; }
		public bool  healing { get; set; }
		public bool  devil { get; set; }
		public bool  chips { get; set; }
		public bool  seadramon { get; set; }
		public bool  fish { get; set; }
		public bool  tournamentSchedule { get; set; }
		public bool  food { get; set; }
		public bool  rareSpawns { get; set; }
		public bool  chaosItems { get; set; }
		public bool tree { get; set; }
		public bool  time { get; set; }
		public bool  statGains { get; set; }
		public bool  requirementsEvo { get; set; }
		public bool  specialEvo { get; set; }
		public bool  specialChance { get; set; }
		public bool  evoItems { get; set; }
		public bool  speEvoReq { get; set; }
		public bool  factorial { get; set; }
		public bool  sukamon { get; set; }
		public bool damageTech { get; set; }
		public bool  MPtech { get; set; }
		public bool  damageType { get; set; }
		public bool  accuracy { get; set; }
		public bool  status { get; set; }
		public bool  statusChance { get; set; }
		public bool  finishers { get; set; }
		public bool  boostedTech { get; set; }
		public bool  boostedTechValue { get; set; }
		public bool  learnBattle { get; set; }
		public bool  learnBrains { get; set; }
		public bool  givenTechs { get; set; }
		public int itemsSpawnOpt { get; set; }
		public int spawnRateItemsOpt { get; set; }
		public int  itemDropsOpt { get; set; }
		public int  dropRateOpt { get; set; }
		public int  chestsOpt { get; set; }
		public int  shopsOpt { get; set; }
		public int  shopsPricesOpt { get; set; }
		public int  MojyamonOpt { get; set; }
		public int  meritItemsOpt { get; set; }
		public int  meritPricesOpt { get; set; }
		public int  tournamentItemsOpt { get; set; }
		public int  tokomonOpt { get; set; }
		public int difficultyOpt { get; set; }
		public int  digimonNPCOpt { get; set; }
		public int  statsNPCOpt { get; set; }
		public int  moneyNPCOpt { get; set; }
		public int  starterTechOpt { get; set; }
		public int  starterStatsOpt { get; set; }
		public int  starterLevelOpt { get; set; }
		public int  tournamentNPCOpt { get; set; }
		public int  recruitsOpt { get; set; }
		public int restaurantOpt { get; set; }
		public int  birdramonOpt { get; set; }
		public int  boostOpt { get; set; }
		public int  healingOpt { get; set; }
		public int  devilOpt { get; set; }
		public int  chipsOpt { get; set; }
		public int  fishOpt { get; set; }
		public int  tournamentScheduleOpt { get; set; }
		public int  foodOpt { get; set; }
		public int  rareSpawnsOpt { get; set; }
		public int treeOpt { get; set; }
		public int  timeOpt { get; set; }
		public int  statGainsOpt { get; set; }
		public int  requirementsEvoOpt { get; set; }
		public int  specialEvoOpt { get; set; }
		public int  specialChanceOpt { get; set; }
		public int  evoItemsOpt { get; set; }
		public int  speEvoReqOpt { get; set; }
		public int  factorialOpt { get; set; }
		public int damageTechOpt { get; set; }
		public int  MPtechOpt { get; set; }
		public int  damageTypeOpt { get; set; }
		public int  accuracyOpt { get; set; }
		public int  statusOpt { get; set; }
		public int  statusChanceOpt { get; set; }
		public int  finishersOpt { get; set; }
		public int  boostedTechValueOpt { get; set; }
		public int  learnBattleOpt { get; set; }
		public int  learnBrainsOpt { get; set; }

		public RandoSaveData(bool itemsSpawn, bool spawnRateItems, bool itemDrops, bool dropRate, bool chests, bool shops, bool shopsPrices, bool Mojyamon, bool meritItems,
		bool meritPrices, bool tournamentItems, bool keyItems, bool tokomon, bool curlingRewards, bool difficulty, bool digimonNPC, bool statsNPC, bool techNPC, bool moneyNPC,
		bool bosses, bool starter, bool starterTech, bool starterStats, bool starterLevel, bool tournamentNPC, bool recruits, bool restaurant, bool birdramon, bool boost,
		bool healing, bool devil, bool chips, bool seadramon, bool fish, bool tournamentSchedule, bool food, bool rareSpawns, bool chaosItems, bool tree, bool time, bool statGains,
		bool requirementsEvo, bool specialEvo, bool specialChance, bool evoItems, bool speEvoReq, bool factorial, bool sukamon, bool damageTech, bool MPtech, bool damageType,
		bool accuracy, bool status, bool statusChance, bool finishers, bool boostedTech, bool boostedTechValue, bool learnBattle, bool learnBrains, bool givenTechs,
		int itemsSpawnOpt, int spawnRateItemsOpt, int itemDropsOpt, int dropRateOpt, int chestsOpt, int shopsOpt, int shopsPricesOpt, int MojyamonOpt, int meritItemsOpt,
		int meritPricesOpt, int tournamentItemsOpt, int tokomonOpt, int difficultyOpt, int digimonNPCOpt, int statsNPCOpt, int moneyNPCOpt, int starterTechOpt, int starterStatsOpt,
		int starterLevelOpt, int tournamentNPCOpt, int recruitsOpt, int restaurantOpt, int birdramonOpt, int boostOpt, int healingOpt, int devilOpt, int chipsOpt, int fishOpt,
		int tournamentScheduleOpt, int foodOpt, int rareSpawnsOpt, int treeOpt, int timeOpt, int statGainsOpt, int requirementsEvoOpt, int specialEvoOpt, int specialChanceOpt,
		int evoItemsOpt, int speEvoReqOpt, int factorialOpt, int damageTechOpt, int MPtechOpt, int damageTypeOpt, int accuracyOpt, int statusOpt, int statusChanceOpt,
		int finishersOpt, int boostedTechValueOpt, int learnBattleOpt, int learnBrainsOpt)
		{
			this.itemsSpawn = itemsSpawn;
			this.spawnRateItems = spawnRateItems;
			this.itemDrops = itemDrops;
			this.dropRate = dropRate;
			this.chests = chests;
			this.shops = shops;
			this.shopsPrices = shopsPrices;
			this.Mojyamon = Mojyamon;
			this.meritItems = meritItems;
			this.meritPrices = meritPrices;
			this.tournamentItems = tournamentItems;
			this.keyItems = keyItems;
			this.tokomon = tokomon;
			this.curlingRewards = curlingRewards;
		 	this.difficulty = difficulty;
			this.digimonNPC = digimonNPC;
			this.statsNPC = statsNPC;
			this.techNPC = techNPC;
			this.moneyNPC = moneyNPC;
			this.bosses = bosses;
			this.starter = starter;
			this.starterTech = starterTech;
			this.starterStats = starterStats;
			this.starterLevel = starterLevel;
			this.tournamentNPC = tournamentNPC;
			this.recruits = recruits;
			this.restaurant = restaurant;
			this.birdramon = birdramon;
			this.boost = boost;
			this.healing = healing;
			this.devil = devil;
			this.chips = chips;
			this.seadramon = seadramon;
			this.fish = fish;
			this.tournamentSchedule = tournamentSchedule;
			this.food = food;
			this.rareSpawns = rareSpawns;
			this.chaosItems = chaosItems;
			this.tree = tree;
			this.time = time;
			this.statGains = statGains;
			this.requirementsEvo = requirementsEvo;
			this.specialEvo = specialEvo;
			this.specialChance = specialChance;
			this.evoItems = evoItems;
			this.speEvoReq = speEvoReq;
			this.factorial = factorial;
			this.sukamon = sukamon;
			this.damageTech = damageTech;
			this.MPtech = MPtech;
			this.damageType = damageType;
			this.accuracy = accuracy;
			this.status = status;
			this.statusChance = statusChance;
			this.finishers = finishers;
			this.boostedTech = boostedTech;
			this.boostedTechValue = boostedTechValue;
			this.learnBattle = learnBattle;
			this.learnBrains = learnBrains;
			this.givenTechs = givenTechs;
		 	this.itemsSpawnOpt = itemsSpawnOpt;
			this.spawnRateItemsOpt = spawnRateItemsOpt;
			this.itemDropsOpt = itemDropsOpt;
			this.dropRateOpt = dropRateOpt;
			this.chestsOpt = chestsOpt;
			this.shopsOpt = shopsOpt;
			this.shopsPricesOpt = shopsPricesOpt;
			this.MojyamonOpt = MojyamonOpt;
			this.meritItemsOpt = meritItemsOpt;
			this.meritPricesOpt = meritPricesOpt;
			this.tournamentItemsOpt = tournamentItemsOpt;
			this.tokomonOpt = tokomonOpt;
			this.difficultyOpt = difficultyOpt;
			this.digimonNPCOpt = digimonNPCOpt;
			this.statsNPCOpt = statsNPCOpt;
			this.moneyNPCOpt = moneyNPCOpt;
			this.starterTechOpt = starterTechOpt;
			this.starterStatsOpt = starterStatsOpt;
			this.starterLevelOpt = starterLevelOpt;
			this.tournamentNPCOpt = tournamentNPCOpt;
			this.recruitsOpt = recruitsOpt;
			this.restaurantOpt = restaurantOpt;
			this.birdramonOpt = birdramonOpt;
			this.boostOpt = boostOpt;
			this.healingOpt = healingOpt;
			this.devilOpt = devilOpt;
			this.chipsOpt = chipsOpt;
			this.fishOpt = fishOpt;
			this.tournamentScheduleOpt = tournamentScheduleOpt;
			this.foodOpt = foodOpt;
			this.rareSpawnsOpt = rareSpawnsOpt;
		 	this.treeOpt = treeOpt;
			this.timeOpt = timeOpt;
			this.statGainsOpt = statGainsOpt;
			this.requirementsEvoOpt = requirementsEvoOpt;
			this.specialEvoOpt = specialEvoOpt;
			this.specialChanceOpt = specialChanceOpt;
			this.evoItemsOpt = evoItemsOpt;
			this.speEvoReqOpt = speEvoReqOpt;
			this.factorialOpt = factorialOpt;
		 	this.damageTechOpt = damageTechOpt;
			this.MPtechOpt = MPtechOpt;
			this.damageTypeOpt = damageTypeOpt;
			this.accuracyOpt = accuracyOpt;
			this.statusOpt = statusOpt;
			this.statusChanceOpt = statusChanceOpt;
			this.finishersOpt = finishersOpt;
			this.boostedTechValueOpt = boostedTechValueOpt;
			this.learnBattleOpt = learnBattleOpt;
			this.learnBrainsOpt = learnBrainsOpt;
		}
	}
	//main stuff
	[Export]
	private Label mainTitle;

	[Export]
	private PanelContainer itemsContainer;

	[Export]
	private PanelContainer digimonContainer;

	[Export]
	private PanelContainer miscContainer;

	[Export]
	private PanelContainer evolutionContainer;

	[Export]
	private PanelContainer techContainer;

	[Export]
	private Button Items;

	[Export]
	private Button Digimon;

	[Export]
	private Button Miscellaneous;

	[Export]
	private Button Evolution;

	[Export]
	private Button Techniques;

	[Export]
	private Button StartRandomizer;

	[Export]
	private TextureButton ExitRandomizer;

	[Export]
	private Window InformationWindow;

	[Export]
	private LinkButton InformationButton;

	[Export]
	private TextEdit InformationText;

	[Export]
	private ConfirmationDialog confirmationPatch;

	[Export]
	private FileDialog selectFolder;

	[Export]
	private Button randomSeeder;

	[Export]
	private SpinBox Seed;

	[Export]
	private Label patchConfirmTitle;

	[Export]
	private Button chooseFolder;

	[Export]
	private Label FileNameL;

	[Export]
	private LineEdit FolderPath;

	[Export]
	private LineEdit FileNameSet;

	[Export]
	private Sprite2D MetalFinish;

	[Export]
	private AnimatedSprite2D MetalWait;

	[Export]
	private Label patchingLoading;

	[Export]
	private AcceptDialog PatchingWait;

	[Export]
	private CheckBox Difficulty;
	[Export]
	private OptionButton DifficultyChoice;

	[Export] ItemsContainer itemsScript;
	[Export] DigimonContainerRando digimonScript;
	[Export] MiscContainerRando miscScript;
	[Export] EvolutionContainer evoScript;
	[Export] TechContainerRando techScript;
	[Export] Button SaveDataButton;

	int randomSeed, currentSukamon = 39;

	Random numberGenerator;

	System.IO.Stream bin;

	BinaryWriter writter;
	BinaryReader reader;

	//transference
	bool filth = false, hardcore = false, trueHardcore = false, ultraHardcore = false, merit = false, removeTechBoost = false, easyStart = false, tanemon = false, rookieOnly = false;

	//Options
	bool itemsSpawn, spawnRateItems, itemDrops, dropRate, chests, shops, shopsPrices, Mojyamon, meritItems, meritPrices, tournamentItems, keyItems, tokomon, curlingRewards,
		 difficulty, digimonNPC, statsNPC, techNPC, moneyNPC, bosses, starter, starterTech, starterStats, starterLevel, tournamentNPC, recruits,
		 restaurant, birdramon, boost, healing, devil, chips, seadramon, fish, tournamentSchedule, food, rareSpawns, chaosItems,
		 tree, time, statGains, requirementsEvo, specialEvo, specialChance, evoItems, speEvoReq, factorial, sukamon,
		 damageTech, MPtech, damageType, accuracy, status, statusChance, finishers, boostedTech, boostedTechValue, learnBattle, learnBrains, givenTechs;
	int itemsSpawnOpt, spawnRateItemsOpt, itemDropsOpt, dropRateOpt, chestsOpt, shopsOpt, shopsPricesOpt, MojyamonOpt, meritItemsOpt, meritPricesOpt, tournamentItemsOpt, tokomonOpt,
		 difficultyOpt, digimonNPCOpt, statsNPCOpt, moneyNPCOpt, starterTechOpt, starterStatsOpt, starterLevelOpt, tournamentNPCOpt, recruitsOpt,
		 restaurantOpt, birdramonOpt, boostOpt, healingOpt, devilOpt, chipsOpt, fishOpt, tournamentScheduleOpt, foodOpt, rareSpawnsOpt,
		 treeOpt, timeOpt, statGainsOpt, requirementsEvoOpt, specialEvoOpt, specialChanceOpt, evoItemsOpt, speEvoReqOpt, factorialOpt,
		 damageTechOpt, MPtechOpt, damageTypeOpt, accuracyOpt, statusOpt, statusChanceOpt, finishersOpt, boostedTechValueOpt, learnBattleOpt, learnBrainsOpt;

	Base_script parent_script;

	string folderPath, filePath, newFilename;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Seed.MinValue = int.MinValue;
		Seed.MaxValue = int.MaxValue;
		GenerateRandomSeed();
		Items.Toggled += ItemsActive;
		Digimon.Toggled += DigimonActive;
		Miscellaneous.Toggled += MiscActive;
		Evolution.Toggled += EvolutionActive;
		Techniques.Toggled += TechniqueActive;
		ExitRandomizer.Pressed += ExitRandomizerPressed;
		StartRandomizer.Pressed += OnRandomize;
		selectFolder.DirSelected += OnFolderSelected;
		confirmationPatch.Confirmed += OnRandomizerConfirmed;
		FileNameSet.TextChanged += OnNameSet;
		confirmationPatch.Canceled += OnRandomizedCanceled;
		confirmationPatch.CloseRequested += OnRandomizedCanceled;
		chooseFolder.Pressed += _on_folderButton_pressed;
		randomSeeder.Pressed += _on_seed_random_pressed;
		SaveDataButton.Pressed += LoadSaveData;

		patchingLoading.Text = Tr("Randomizing");
		mainTitle.Text = Tr("RandoTitle");
		Items.Text = Tr("Items_T");
		Miscellaneous.Text = Tr("Miscellaneous_T");
		Techniques.Text = Tr("Techniques_T");
		Evolution.Text = Tr("Evolution_T");
		chooseFolder.Text = Tr("SFolder");
		FileNameL.Text = Tr("FName");
		randomSeeder.Text = Tr("Seed");
		FolderPath.PlaceholderText = Tr("SFolderT");
		patchConfirmTitle.Text = Tr("PatchTitle");
		StartRandomizer.Text = Tr("RandomizeButton");
		Seed.TooltipText = Tr("Seed_info");
		PatchingWait.GetOkButton().Text = Tr("CancelButton");
		PatchingWait.Confirmed += HandleError;
		confirmationPatch.GetOkButton().Text = Tr("RandomizeButton");
		confirmationPatch.GetCancelButton().Text = Tr("CancelButton");
		SaveDataButton.Text = Tr("RandoSettings");
		SaveDataButton.TooltipText = Tr("RandoSettingsInfo");

		RestartStuffRandomizer();
		if (!File.Exists(OS.GetExecutablePath().GetBaseDir() + "/SaveData/RandoSave"))
			SaveDataButton.Disabled = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetBasicData(string fileP, Base_script OGScript)
	{
		filePath = fileP;
		parent_script = OGScript;
	}

	public void SetStartingData(bool filthR, bool hardcoreR, bool trueHardcoreR, bool ultraHardcoreR, bool meritR, bool removeTechBoostR, bool easyStartR, bool tanemonR, bool RookieR, string folderP, string fileP, Base_script OGScript)
	{
		filth = filthR;
		hardcore = hardcoreR;
		ultraHardcore = ultraHardcoreR;
		trueHardcore = trueHardcoreR;
		merit = meritR;
		removeTechBoost = removeTechBoostR;
		easyStart = easyStartR;
		folderPath = folderP;
		filePath = fileP;
		FolderPath.Text = folderP;
		parent_script = OGScript;
		tanemon = tanemonR;
		rookieOnly = RookieR;

		tokomonOpt = easyStartR ? 1 : 0;
		if (hardcore)
		{
			Difficulty.Disabled = true;
			Difficulty.SetPressedNoSignal(true);
			difficulty = true;
			difficultyOpt = 0;
			if (trueHardcore)
			{
				DifficultyChoice.Selected = 1;
				difficultyOpt = 1;
			}
		}

		if (filth || tanemonR || RookieR)
			Evolution.Visible = false;
	}

	void ItemsActive(bool pressed)
	{
		if (pressed)
		{
			itemsContainer.Visible = true;
			digimonContainer.Visible = false;
			miscContainer.Visible = false;
			evolutionContainer.Visible = false;
			techContainer.Visible = false;
			Items.Disabled = true;
			Digimon.Disabled = false;
			Digimon.ButtonPressed = false;
			Miscellaneous.Disabled = false;
			Miscellaneous.ButtonPressed = false;
			Evolution.Disabled = false;
			Evolution.ButtonPressed = false;
			Techniques.Disabled = false;
			Techniques.ButtonPressed = false;
		}
	}

	void DigimonActive(bool pressed)
	{
		if (pressed)
		{
			itemsContainer.Visible = false;
			digimonContainer.Visible = true;
			miscContainer.Visible = false;
			evolutionContainer.Visible = false;
			techContainer.Visible = false;
			Items.Disabled = false;
			Items.ButtonPressed = false;
			Digimon.Disabled = true;
			Miscellaneous.Disabled = false;
			Miscellaneous.ButtonPressed = false;
			Evolution.Disabled = false;
			Evolution.ButtonPressed = false;
			Techniques.Disabled = false;
			Techniques.ButtonPressed = false;
		}
	}

	void MiscActive(bool pressed)
	{
		if (pressed)
		{
			itemsContainer.Visible = false;
			digimonContainer.Visible = false;
			miscContainer.Visible = true;
			evolutionContainer.Visible = false;
			techContainer.Visible = false;
			Items.Disabled = false;
			Items.ButtonPressed = false;
			Digimon.Disabled = false;
			Digimon.ButtonPressed = false;
			Miscellaneous.Disabled = true;
			Evolution.Disabled = false;
			Evolution.ButtonPressed = false;
			Techniques.Disabled = false;
			Techniques.ButtonPressed = false;
		}
	}

	void EvolutionActive(bool pressed)
	{
		if (pressed)
		{
			itemsContainer.Visible = false;
			digimonContainer.Visible = false;
			miscContainer.Visible = false;
			evolutionContainer.Visible = true;
			techContainer.Visible = false;
			Items.Disabled = false;
			Items.ButtonPressed = false;
			Digimon.Disabled = false;
			Digimon.ButtonPressed = false;
			Miscellaneous.Disabled = false;
			Miscellaneous.ButtonPressed = false;
			Evolution.Disabled = true;
			Techniques.Disabled = false;
			Techniques.ButtonPressed = false;
		}
	}

	void TechniqueActive(bool pressed)
	{
		if (pressed)
		{
			itemsContainer.Visible = false;
			digimonContainer.Visible = false;
			miscContainer.Visible = false;
			evolutionContainer.Visible = false;
			techContainer.Visible = true;
			Items.Disabled = false;
			Items.ButtonPressed = false;
			Digimon.Disabled = false;
			Digimon.ButtonPressed = false;
			Miscellaneous.Disabled = false;
			Miscellaneous.ButtonPressed = false;
			Evolution.Disabled = false;
			Evolution.ButtonPressed = false;
			Techniques.Disabled = true;

		}

	}

	void OnNameSet(string name)
	{
		if (name == null || name == "")
			FileNameSet.Text = FileNameSet.PlaceholderText;        
	}

	void OnRandomize()
	{
		StartRandomizer.Visible = false;
		confirmationPatch.Visible = true;
		GenerateRandomSeed();
	}

	void OnFolderSelected(string folder)
	{
		FolderPath.Text = folder;
		confirmationPatch.Visible = true;
	}

	void OnRandomizerConfirmed()
	{
		if (FileNameSet.Text != null && FolderPath.Text != null && FileNameSet.Text != "" && FolderPath.Text != "")
			CreateRandomizedFile(FolderPath.Text, FileNameSet.Text);
		else if (FolderPath.Text != null && FolderPath.Text != "")
			CreateRandomizedFile(FolderPath.Text, FileNameSet.PlaceholderText);
		else
			OnRandomizedCanceled();
	}

	void ExitRandomizerPressed()
	{
		parent_script.SetVisibleAgain();
		this.QueueFree();
	}

	public void RestartStuffRandomizer()
	{
		itemsSpawn = spawnRateItems = itemDrops = dropRate = chests = shops = shopsPrices = Mojyamon = meritItems = meritPrices = tournamentItems = keyItems = tokomon = curlingRewards =
		digimonNPC = statsNPC = techNPC = moneyNPC = bosses = starter = starterTech = starterStats = starterLevel = tournamentNPC = recruits =
		restaurant = birdramon = boost = healing = devil = chips = seadramon = fish = tournamentSchedule = food = rareSpawns = chaosItems =
		tree = time = statGains = requirementsEvo = specialEvo = specialChance = evoItems = factorial = sukamon = speEvoReq =
		damageTech = MPtech = damageType = accuracy = status = statusChance = finishers = boostedTech = boostedTechValue = learnBattle = learnBrains = givenTechs = false;
	}

	void _on_folderButton_pressed()
	{
		confirmationPatch.Visible = false;
		selectFolder.Visible = true;
	}

	void OnRandomizedCanceled()
	{
		StartRandomizer.Visible = true;
		confirmationPatch.Visible = false;
	}

	void HandleError()
	{
		PatchingWait.Visible = false;
		patchingLoading.Text = Tr("Randomizing");
		StartRandomizer.Visible = true;

	}

	void SetError()
	{
		patchingLoading.Text = "Error";
		if (reader != null)
		{
			reader.Close();
			reader.Dispose();
		}
		if (writter != null)
		{
			writter.Close();
			writter.Dispose();
		}
		if (bin != null)
		{
			bin.Close();
			bin.Dispose();
		}
	}

	void _on_choose_folder_canceled()
	{
		selectFolder.Visible = false;
		confirmationPatch.Visible = true;
	}

	public void SetItemSpawns(bool active, int id) { itemsSpawn = active; itemsSpawnOpt = id; }
	public void SetItemSpawnsOpt(int id) { itemsSpawnOpt = id; }
	public void SetItemSpawnRate(bool active, int id) { spawnRateItems = active; spawnRateItemsOpt = id; }
	public void SetItemSpawnsRateOpt(int id) { spawnRateItemsOpt = id; }
	public void SetItemDrops(bool active, int id) { itemDrops = active; itemDropsOpt = id; }
	public void SetItemDropsOpt(int id) { itemDropsOpt = id; }
	public void SetDropRate(bool active, int id) { dropRate = active; dropRateOpt = id; }
	public void SetDropRateOpt(int id) { dropRateOpt = id; }
	public void SetChests(bool active, int id) { chests = active; chestsOpt = id; }
	public void SetChestsOpt(int id) { chestsOpt = id; }
	public void SetShops(bool active, int id) { shops = active; shopsOpt = id; }
	public void SetShopsOpt(int id) { shopsOpt = id; }
	public void SetShopsPrices(bool active, int id) { shopsPrices = active; shopsPricesOpt = id; }
	public void SetShopsPricesOpt(int id) { shopsPricesOpt = id; }
	public void SetMojyamon(bool active, int id) { Mojyamon = active; MojyamonOpt = id; }
	public void SetMojyamonOpt(int id) { MojyamonOpt = id; }
	public void SetMeritItems(bool active, int id) { meritItems = active; meritItemsOpt = id; }
	public void SetMeritItemsOpt(int id) { meritItemsOpt = id; }
	public void SetMeritPrices(bool active, int id) { meritPrices = active; meritPricesOpt = id; }
	public void SetMeritPricesOpt(int id) { meritPricesOpt = id; }
	public void SetTournamentItems(bool active, int id) { tournamentItems = active; tournamentItemsOpt = id; }
	public void SetTournamentItemsOpt(int id) { tournamentItemsOpt = id; }
	public void SetKeyItems(bool active) { keyItems = active; }
	public void SetCurlingRewards(bool active) { curlingRewards = active; }
	public void SetTokomon(bool active, int id) { tokomon = active; tokomonOpt = id; }
	public void SetTokomonOpt(int id) { tokomonOpt = id; }
	public void SetDigimonNPC(bool active, int id) { digimonNPC = active; digimonNPCOpt = id; }
	public void SetDigimonNPCOpt(int id) { digimonNPCOpt = id; }
	public void SetStatsNPC(bool active, int id) { statsNPC = active; statsNPCOpt = id; }
	public void SetStatsNPCOpt(int id) { statsNPCOpt = id; }
	public void SetTechNPC(bool active) { techNPC = active; }
	public void SetMoneyNPC(bool active, int id) { moneyNPC = active; moneyNPCOpt = id; }
	public void SetMoneyNPCOpt(int id) { moneyNPCOpt = id; }
	public void SetBosses(bool active) { bosses = active; }
	public void SetStarter(bool active) { starter = active; }
	public void SetStarterTech(bool active, int id) { starterTech = active; starterTechOpt = id; }
	public void SetStarterTechOpt(int id) { starterTechOpt = id; }
	public void SetStarterLevel(bool active, int id) { starterLevel = active; starterLevelOpt = id; }
	public void SetStarterLevelOpt(int id) { starterLevelOpt = id; }
	public void SetStarterStats(bool active, int id) { starterStats = active; starterStatsOpt = id; }
	public void SetStarterStatsOpt(int id) { starterStatsOpt = id; }
	public void SetTournamentNPC(bool active, int id) { tournamentNPC = active; tournamentNPCOpt = id; }
	public void SetTournamentNPCOpt(int id) { tournamentNPCOpt = id; }
	public void SetRecruits(bool active, int id) { recruits = active; recruitsOpt = id; }
	public void SetRecruitsOpt(int id) { recruitsOpt = id; }
	public void SetRestaurant(bool active, int id) { restaurant = active; restaurantOpt = id; }
	public void SetRestaurantOpt(int id) { restaurantOpt = id; }
	public void SetBirdramon(bool active, int id) { birdramon = active; birdramonOpt = id; }
	public void SetBirdramonOpt(int id) { birdramonOpt = id; }
	public void SetItemBoost(bool active, int id) { boost = active; boostOpt = id; }
	public void SetItemBoostOpt(int id) { boostOpt = id; }
	public void SetHealing(bool active, int id) { healing = active; healingOpt = id; }
	public void SetHealingOpt(int id) { healingOpt = id; }
	public void SetDevilChips(bool active, int id) { devil = active; devilOpt = id; }
	public void SetDevilChipsOpt(int id) { devilOpt = id; }
	public void SetChips(bool active, int id) { chips = active; chipsOpt = id; }
	public void SetChipsOpt(int id) { chipsOpt = id; }
	public void SetFish(bool active, int id) { fish = active; fishOpt = id; }
	public void SetFishOpt(int id) { fishOpt = id; }
	public void SetTournamentSchedule(bool active, int id) { tournamentSchedule = active; tournamentScheduleOpt = id; }
	public void SetTournamentScheduleOpt(int id) { tournamentScheduleOpt = id; }
	public void SetFood(bool active, int id) { food = active; foodOpt = id; }
	public void SetFoodOpt(int id) { foodOpt = id; }
	public void SetRareSpawns(bool active, int id) { rareSpawns = active; rareSpawnsOpt = id; }
	public void SetRareSpawnsOpt(int id) { rareSpawnsOpt = id; }
	public void SetSeadramon(bool active) { seadramon = active; }
	public void SetChaosItems(bool active) { chaosItems = active; }
	public void SetEvoTree(bool active, int id) { tree = active; treeOpt = id; }
	public void SetEvoTreeOpt(int id) { treeOpt = id; }
	public void SetEvoTime(bool active, int id) { time = active; timeOpt = id; }
	public void SetEvoTimeOpt(int id) { timeOpt = id; }
	public void SetStatGains(bool active, int id) { statGains = active; statGainsOpt = id; }
	public void SetStatGainsOpt(int id) { statGainsOpt = id; }
	public void SetRequirementsEvo(bool active, int id) { requirementsEvo = active; requirementsEvoOpt = id; }
	public void SetRequirementsEvoOpt(int id) { requirementsEvoOpt = id; }
	public void SetSpecialEvo(bool active, int id) { specialEvo = active; specialEvoOpt = id; }
	public void SetSpecialEvoOpt(int id) { specialEvoOpt = id; }
	public void SetSpecialChance(bool active, int id) { specialChance = active; specialChanceOpt = id; }
	public void SetSpecialChanceOpt(int id) { specialChanceOpt = id; }
	public void SetEvoItems(bool active, int id) { evoItems = active; evoItemsOpt = id; }
	public void SetEvoItemsOpt(int id) { evoItemsOpt = id; }
	public void SetFactorial(bool active, int id) { factorial = active; factorialOpt = id; }
	public void SetFactorialOpt(int id) { factorialOpt = id; }
	public void SetSpecialEvoReq(bool active, int id) { speEvoReq = active; speEvoReqOpt = id; }
	public void SetSpecialEvoReqOpt(int id) { speEvoReqOpt = id; }
	public void SetSukamon(bool active) { sukamon = active; }
	public void SetdamageTech(bool active, int id) { damageTech = active; damageTechOpt = id; }
	public void SetdamageTechOpt(int id) { damageTechOpt = id; }
	public void SetMPTech(bool active, int id) { MPtech = active; MPtechOpt = id; }
	public void SetMPTechOpt(int id) { MPtechOpt = id; }
	public void SetDamageType(bool active, int id) { damageType = active; damageTypeOpt = id; }
	public void SetDamageTypeOpt(int id) { damageTypeOpt = id; }
	public void SetAccuracy(bool active, int id) { accuracy = active; accuracyOpt = id; }
	public void SetAccuracyOpt(int id) { accuracyOpt = id; }
	public void SetStatus(bool active, int id) { status = active; statusOpt = id; }
	public void SetStatusOpt(int id) { statusOpt = id; }
	public void SetStatusChance(bool active, int id) { statusChance = active; statusChanceOpt = id; }
	public void SetStatusChanceOpt(int id) { statusChanceOpt = id; }
	public void SetFinishers(bool active, int id) { finishers = active; finishersOpt = id; }
	public void SetFinishersOpt(int id) { finishersOpt = id; }
	public void SetBoostedTech(bool active) { boostedTech = active;}
	public void SetBoostedTechValue(bool active, int id) { boostedTechValue = active; boostedTechValueOpt = id; }
	public void SetBoostedTechValueOpt(int id) { boostedTechValueOpt = id; }
	public void SetLearnBattle(bool active, int id) { learnBattle = active; learnBattleOpt = id; }
	public void SetLearnBattleOpt(int id) { learnBattleOpt = id; }
	public void SetLearnBrains(bool active, int id) { learnBrains = active; learnBrainsOpt = id; }
	public void SetLearnBrainsOpt(int id) { learnBrainsOpt = id; }
	public void SetGivenTechs(bool active) { givenTechs = active; }

	public void SetDifficulty(bool active, int id) { difficulty = active; difficultyOpt = id;}
	public void SetDifficultyOpt(int id) { difficultyOpt = id;}

	public bool hasEasyStart() { return easyStart; }

	void GenerateRandomSeed()
	{
		using (System.Security.Cryptography.RandomNumberGenerator rg = System.Security.Cryptography.RandomNumberGenerator.Create())
		{
			byte[] rno = new byte[5];
			rg.GetBytes(rno);
			randomSeed = BitConverter.ToInt32(rno, 0);
		}
		Seed.Value = randomSeed;
	}

	void _on_seed_random_pressed()
	{
		GenerateRandomSeed();
	}

	void CreateRandomizedFile(string folderDestination, string newFilename)
	{
		confirmationPatch.Visible = false;
		PatchingWait.Visible = true;
		MetalWait.Play();
		folderPath = folderDestination;
		newFilename = System.IO.Path.Combine(folderDestination, newFilename);
		newFilename = newFilename + System.IO.Path.GetExtension(filePath);
		System.IO.File.Copy(filePath, newFilename, true);
		try
		{
			bin = System.IO.File.Open(newFilename, FileMode.Open, System.IO.FileAccess.ReadWrite);
		}
		catch (System.ArgumentException)
		{
			SetError();

		}
		catch (System.IO.FileNotFoundException)
		{
			SetError();
		}
		catch (System.IO.IOException)
		{
			SetError();
		}
		writter = new BinaryWriter(bin);		
		reader = new BinaryReader(bin);
		numberGenerator = new Random((int)Seed.Value);

		try
		{
			StartRandomizing();
		}
		catch (System.ArgumentException ex)
		{
			GD.Print(ex.Message);
			SetError();			
		}
		catch (System.IO.FileNotFoundException)
		{
			GD.Print("file not found");
			SetError();
		}
		catch (System.IO.IOException ex)
		{
			GD.Print(ex.Message);
			SetError();
		}
	}

	void StartRandomizing()
	{
		if (itemsSpawn)
			RandomizeItemSpawns();
		if (spawnRateItems)
			RandomizeItemSpawnRate();
		if (itemDrops)
			RandomizeItemDrops();
		if (dropRate)
			RandomizeItemDropRate();
		if (chests)
			RandomizeChests();
		if (shops)
			RandomizeShops();
		if (shopsPrices)
			RandomizeShopPrices();
		if (Mojyamon)
			RandomizeMojyamon();
		if (meritItems)
			RandomizeMerit();
		if (meritPrices)
			RandomizeMeritPrices();
		if (tournamentItems)
			RandomizeTournamentPrizes();
		if (tokomon)
			RandomizeTokomon();
		if (curlingRewards)
			RandomizeCurlingRewards();

		if (damageTech)
			RandomizeTechDamage();
		if (MPtech)
			RandomizeMP();
		if (damageType)
			RandomizeTypeDamage();
		if (accuracy)
			RandomizeAccuracy();
		if (status)
			RandomizeStatus();
		if (statusChance)
			RandomizeStatusChance();
		if (boostedTech)
			RandomizeTechBoost();
		if (boostedTechValue)
			RandomizeTechBoostPower();
		if (learnBattle)
			RandomizeLearningChancesBattle();
		if (learnBrains)
			RandomizeLearningChancesBrains();
		if (givenTechs)
			RandomizeGivenTechs();

        if (specialEvo)
			RandomizeSpecialEvo();
		if (tree)
			RandomizeEvoTree();
		if (time)
			RandomizeEvoTime();
		if (statGains)
			RandomizeStatGains();
		if (requirementsEvo)
			RandomizeRequirements();		
		if (specialChance)
			RandomizeSpecialChance();
		if (speEvoReq)
			RandomizeSpecialReq();
		if (factorial)
			RandomizeFactorial();
		if (sukamon)
			RandomizeSukamon();

		if (restaurant)
			randomizeRestaurant();
		if (birdramon)
			RandomizeBirdramon();
		if (seadramon)
			randomizeSeadramon();
		if (fish)
			RandomizeFishBait();
		if (tournamentSchedule)
			RandomizeTournamentSchedule();
		if (rareSpawns)
			RandomizeRareSpawns();
		if (devil)
			RandomizeMonochromon();

		if (chaosItems)
			CreateChaosItems();
		else
		{
			if (healing)
				RandomizeHealing();
			if (food)
				RandomizeFood();
			if (chips)
				RandomizeChips();
			if (boost)
				RandomizeBoost();
			if (evoItems)
				randomizeEvoItems();
		}
		if (keyItems)
			UnlockKeyItems();

		DigimonRandomizer digimonRando = new DigimonRandomizer(hardcore, trueHardcore, ref bin, reader);

		if (digimonNPC)
			digimonRando.RandomizeDigimonNPC(digimonNPCOpt, bosses, numberGenerator, writter);
		if (statsNPC)
			digimonRando.RandomizeNPCStats(statsNPCOpt, bosses, numberGenerator, reader, writter);
		if (moneyNPC)
			digimonRando.RandomizeNPCMoney(moneyNPCOpt, bosses, numberGenerator, reader, writter);
		if (techNPC)
			digimonRando.RandomizeNPCTechs(bosses, digimonNPC, numberGenerator, writter);
		if (!filth && !tanemon && !rookieOnly)
		{
			if (starter)
			{
				if (starterLevel)
					digimonRando.RandomizeStarter(starterLevelOpt, numberGenerator);
				else
					digimonRando.RandomizeStarter(2, numberGenerator);
				if (starterTech)
					digimonRando.StarterTech(starterTechOpt, numberGenerator);
				if (starterStats)
					digimonRando.StarterStats(starterStatsOpt, writter, reader, numberGenerator);
			}
		}
		if (tournamentNPC)
			digimonRando.TournamentNPC(tournamentNPCOpt, reader, writter, numberGenerator);
		if (recruits)
			digimonRando.Recruitments(recruitsOpt, numberGenerator, writter);
		reader.Close();
		reader.Dispose();
		writter.Close();
		writter.Dispose();
		bin.Close();
		bin.Dispose();
		CreateRandoTxt();
	}

	void RandomizeItemSpawns()
	{
		uint[] itemSpawnsOffsets =
		{
			/*MAYO00*/   0x1402B6FD, 0x1402B713, 0x1402B729,
			/*MAYO01*/   0x13FDD565, 0x13FDD57B, 0x13FDD591, 0x13FDD597, 0x13FDD59D, 0x13FDD5B3, 0x13FDD5C9,
			/*MAYO01_2*/ 0x1402C92D, 0x1402C943, 0x1402C959,
			/*MAYO02*/   0x13FDDE89, 0x13FDDE9F,
			/*MAYO02_2*/ 0x1402D25D, 0x1402D273, 0x1402D289,
			/*MAYO03*/   0x13FDE7B5, 0x13FDE7CB, 0x13FDE7E1,
			/*MAYO04A*/  0x13FDF0F1, 0x13FDF103, 0x13FDF119, //one less than in vanilla
			/*MAYO05*/   0x13FE0349, 0x13FE035F, 0x13FE0375, 0x13FE038B,
			/*MAYO06*/   0x13FE1599,
			/*MAYO08A*/  0x13FE43D9, 0x13FE43EF,
			/*MAYO08B*/  0x13FE563D, 0x13FE5653,
			/*MAYO10*/   0x13FE3135, 0x13FE314B, 
			/*MAYO11*/   0x13FE2801,
			/*TROP00*/   0x13FE5F29, 0x13FE5F3F,
			/*TROP01*/   0x13FE6861, 0x13FE6877, 0x13FE688D, 0x13FE68A3,
			/*TROP02*/   0x13FE7189, 0x13FE719F, 0x13FE71B5,
			/*TROP03*/   0x13FE7AB5, 0x13FE7ACB,
			/*TROP04*/   0x13FE8D11, 0x13FE8D27,
			/*TROP06*/   0x13FE9FDD, 0x13FE9FF3, 0x13FEA009, 0x13FEA01F, 0x13FEA035, 0x13FEA04B, 0x13FEA061, 0x13FEA077, 0x13FEA08D, 0x13FEA0A3, 0x13FEA0B9, 0x13FEA0CF, 0x13FEA0E5, 0x13FEA0FB, 0x13FEA10F,
			/*MIHA00*/   0x13FEC445, 0x13FEC45B, 0x13FEC471,
			/*MIHA01*/   0x13FECD65, 0x13FECD7B, 0x13FECD91,
			/*MIHA02*/   0x13FED695, 0x13FED6AB, 0x13FED6C1,
			/*MIHA03*/   0x13FEDFB1, 0x13FEDFB7, 0x13FEDFBD, 0x13FEDFC3, 0x13FEDFD5, 0x13FEE00D, 0x13FEE005,
			/*MIHA04A*/  0x13FEF231, 0x13FEF247, 0x13FEF25D,
			/*MIHA04B*/  0x13FEFB65, 0x13FEFB7B, 0x13FEFB91,
			/*MIHA05*/   0x1407B3D5, 0x1407B3EB, 0x1407B401, 0x1407B417, 0x1407B42D,
			/*MIHA06*/   0x1407BD09, 0x1407BD0F, 0x1407BD15, 0x1407BD2B, 0x1407BD41,
			/*DGHA01*/   0x13FF72D1, 0x13FF72E7, 0x13FF72FD,
			/*DGHA02*/   0x13FF7C01, 0x13FF7C17, 0x13FF7C2D, 0x13FF7C43,
			/*GCAN01*/   0x13FF852D, 0x13FF8543, 0x13FF8559, 0x13FF856F,
			/*GCAN02*/   0x13FF8E69, 0x13FF8E7F, 0x13FF8E95, 0x13FF8EAB,
			/*GCAN03*/   0x13FF9795, 0x13FF97AB, 0x13FF97C1, 0x13FF97D7,
			/*GCAN05*/   0x13FFA9D9, 0x13FFA9EF, 0x13FFAA05, 0x13FFAA1B,
			/*GCAN06*/   0x13FFB321, 0x13FFB337,
			/*GCAN07*/   0x13FFBC4D, 0x13FFBC63, 0x13FFBC79,
			/*GCAN08_1*/ 0x13FFC585, 0x13FFC59B,
			/*GCAN08_2*/ 0x14039C8D, 0x14039CA3,
			/*GIAS00*/   0x1400C6B5, 0x1400C6CB, 0x1400C6E1,
			/*GIAS01*/   0x1400CFED, 0x1400D003, 0x1400D019, 0x1400D02F,
			/*GIAS02*/   0x1400D921, 0x1400D937, 0x1400D94D,
			/*GIAS03*/   0x1400E245, 0x1400E25B, 0x1400E271,
			/*GIAS04*/   0x1400EB9D, 0x1400EBB3, 0x1400EBC9, 0x1400EBDF,
			/*GIAS05*/   0x1400FDE1, 0x1400FDF7, 0x1400FE0D, 0x1400FE23,
			/*GIAS06A*/  0x14010709, 0x1401071F,  //different than in vanilla
			/*GIAS06B*/  0x1403B80D, 0x1403B823,
			/*GIAS07*/   0x14011031, 0x14011047, 0x1401105D, 0x14011073,
			/*GIAS08*/   0x14011969, 0x1401197F,
			/*GIAS09*/   0x14012291, 0x140122A7, 0x140122BD,
			/*KODA00*/   0x140134F5, 0x1401350B,
			/*KODA01*/   0x14014759, 0x1401476F, 0x14014785, 0x1401479B,
			/*KODA02*/   0x14015089, 0x1401509F, 0x140150B5,
			/*KODA03*/   0x140162DD, 0x140162F3, 0x14016309, 0x1401631F,
			/*KODA07*/   0x140190C9,
			/*FRZL01*/   0x1401AC6D, 0x1401AC83, 0x1401AC99,
			/*FRZL02*/   0x1401B595, 0x1401B5AB,
			/*FRZL03*/   0x1401BEC5, 0x1401BEDB, 0x1401BEF1,
			/*FRZL04*/   0x1401C7FD, 0x1401C813, 0x1401C829, 
			/*FRZL06*/   0x1401DA61, 0x1401DA77, 0x1401DA8D,
			/*FRZL07*/   0x1401E389, 0x1401E39F, 0x1401E3B5,
			/*FRZL08*/   0x1401ECB9, 0x1401ECCF, 0x1401ECE5, 0x1401ECFB,
			/*FRZL12*/   0x1401FF15, 0x1401FF2B,
			/*FRZL13*/   0x1403C145, 0x1403C15B,
			/*FRZL14*/   0x1403CA6D, 0x1403CA83,
			/*FRZL15*/   0x1403DCD1,
			/*FRZL16*/   0x1403E605,
			/*BETL01*/   0x140251D9,
			/*MIST01*/   0x140312B9, 0x140312CF, 0x140312E5, 0x140312FB,
			/*MIST02*/   0x14031BF5, 0x14031C0B, 0x14031C21,
			/*MIST03*/   0x14032531, 0x14032547, 0x1403255D, 0x14032573,
			/*MIST04*/   0x1403377D, 0x14033793, 0x140337A9,
			/*MIST05*/   0x14034099, 0x140340AF, 0x140340C5, 0x140340DB,
			/*MIST06*/   0x14035C35, 0x14035C4B, 0x14035C61,
			/*MIST07*/   0x14036579, 0x1403658F, 0x140365A5, 0x140365BB,
			/*STIC01*/   0x140413F9, 0x1404140F, 0x14041425, 0x1404143B,
			/*STIC02*/   0x14041D25, 0x14041D3B, 0x14041D51
		},
		exceptions =
		{
			/*GCAN09*/  0x13FFCEA9, 0x13FFCEBF, 0x13FFCED5
		},
		exceptionsHardcore =
		{
			/*GCAN09*/  0x13FFCEAD, 0x13FFCEC3, 0x13FFCED9
		};

		bin.Position = 0x13FFCE9A;
		int check = bin.ReadByte();

		switch (itemsSpawnOpt)
		{
			case 0:
				List<int> items = new List<int>();
				foreach (uint offset in itemSpawnsOffsets)
				{
					bin.Position = offset;
					items.Add(bin.ReadByte());
				}

				if (check == 0xFF)
				{
					foreach (uint offset in exceptionsHardcore)
						bin.Position = offset;
					items.Add(bin.ReadByte());
				}
				else
				{
					foreach (uint offset in exceptions)
						bin.Position = offset;
					items.Add(bin.ReadByte());
				}

				int[] shuffledItems = items.ToArray();
				numberGenerator.Shuffle<int>(shuffledItems);
				for (int i = 0; i < itemSpawnsOffsets.Length; i++)
				{
					bin.Position = itemSpawnsOffsets[i];
					if (shuffledItems.Length > i)
						bin.WriteByte((byte)shuffledItems[i]);
					else
						bin.WriteByte((byte)numberGenerator.Next(71));
				}
				break;
			case 1:
				foreach (uint offset in itemSpawnsOffsets)
				{
					bin.Position = offset;
					byte rando = (byte)numberGenerator.Next(73);
					if (rando == 71)
						rando = 121;
					else if (rando == 72)
						rando = 122;
					bin.WriteByte(rando);
				}
				if (check == 0xFF)
				{
					foreach (uint offset in exceptionsHardcore)
					{
						bin.Position = offset;
						byte rando = (byte)numberGenerator.Next(73);
						if (rando == 71)
							rando = 121;
						else if (rando == 72)
							rando = 122;
						bin.WriteByte(rando);
					}
				}
				else
				{
					foreach (uint offset in exceptions)
					{
						bin.Position = offset;
						byte rando = (byte)numberGenerator.Next(73);
						if (rando == 71)
							rando = 121;
						else if (rando == 72)
							rando = 122;
						bin.WriteByte(rando);
					}
				}
				break;
			case 2:
				foreach (uint offset in itemSpawnsOffsets)
				{
					bin.Position = offset;
					byte random = (byte)numberGenerator.Next(128);
					bin.WriteByte(random);
				}
				if (check == 0xFF)
				{
					foreach (uint offset in exceptionsHardcore)
					{
						bin.Position = offset;
						byte random = (byte)numberGenerator.Next(128);
						bin.WriteByte(random);
					}
				}
				else
				{
					foreach (uint offset in exceptions)
					{
						bin.Position = offset;
						byte random = (byte)numberGenerator.Next(128);
						bin.WriteByte(random);
					}
				}
				break;
		}
	}

	void RandomizeItemSpawnRate()
	{
		uint[] itemSpawnRateOffsets =
		{ 
			/*MAYO00*/   0x1402B6F5, 0x1402B70B, 0x1402B721,
			/*MAYO01*/	 0x13FDD55D, 0x13FDD573, 0x13FDD589, 0x13FDD5AB, 0x13FDD5C1,
			/*MAYO01_2*/ 0x1402C925, 0x1402C93B, 0x1402C951,
			/*MAYO02*/	 0x13FDDE81, 0x13FDDE97,
			/*MAYO02_2*/ 0x1402D255, 0x1402D26B, 0x1402D281,
			/*MAYO03*/	 0x13FDE7AD, 0x13FDE7C3, 0x13FDE7D9,
			/*MAYO04A*/	 0x13FDF0E9, 0x13FDF0FB, 0x13FDF111,  //one less than in vanilla
			/*MAYO05*/   0x13FE0341, 0x13FE0357, 0x13FE036D, 0x13FE0383,
			/*MAYO06*/   0x13FE1591,
			/*MAYO08A*/  0x13FE43D1, 0x13FE43E7,
			/*MAYO08B*/  0x13FE5635, 0x13FE564B,
			/*MAYO10*/   0x13FE312D, 0x13FE3143,
			/*MAYO11*/   0x13FE27F9,
			/*TROP00*/   0x13FE5F21, 0x13FE5F37,
			/*TROP01*/   0x13FE6859, 0x13FE686F, 0x13FE6885, 0x13FE689B,
			/*TROP02*/   0x13FE7181, 0x13FE7197, 0x13FE71AD,
			/*TROP03*/   0x13FE7AAD, 0x13FE7AC3,
			/*TROP04*/   0x13FE8D09, 0x13FE8D1F,
			/*MIHA00*/   0x13FEC43D, 0x13FEC453, 0x13FEC469,		
			/*MIHA01*/   0x13FECD5D, 0x13FECD73, 0x13FECD89,
			/*MIHA02*/   0x13FED68D, 0x13FED6A3, 0x13FED6B9,
			/*MIHA03*/   //All of these are guaranteed spwans
			/*MIHA04A*/  0x13FEF229, 0x13FEF23F, 0x13FEF255,
			/*MIHA04B*/  0x13FEFB5D, 0x13FEFB73, 0x13FEFB89,
			/*MIHA05*/   0x1407B3CD, 0x1407B3E3, 0x1407B3F9, 0x1407B40F, 0x1407B425,
			/*MIHA06*/   0x1407BD01, 0x1407BD23, 0x1407BD39, 
			/*DGHA01*/   0x13FF72C9, 0x13FF72DF, 0x13FF72F5,
			/*DGHA02*/   0x13FF7BF9, 0x13FF7C0F, 0x13FF7C25, 0x13FF7C3B,
			/*GCAN01*/   0x13FF8525, 0x13FF853B, 0x13FF8551, 0x13FF8567,
			/*GCAN02*/   0x13FF8E61, 0x13FF8E77, 0x13FF8E8D, 0x13FF8EA3,
			/*GCAN03*/   0x13FF978D, 0x13FF97A3, 0x13FF97B9, 0x13FF97CF,
			/*GCAN05*/   0x13FFA9D1, 0x13FFA9E7, 0x13FFA9FD, 0x13FFAA13,
			/*GCAN06*/   0x13FFB319, 0x13FFB32F, 
			/*GCAN07*/   0x13FFBC45, 0x13FFBC5B, 0x13FFBC71,
			/*GCAN08_1*/ 0x13FFC57D, 0x13FFC593,
			/*GCAN08_2*/ 0x14039C85, 0x14039C9B,			
			/*GIAS00*/   0x1400C6AD, 0x1400C6C3, 0x1400C6D9,
			/*GIAS01*/   0x1400CFE5, 0x1400CFFB, 0x1400D011, 0x1400D027,
			/*GIAS02*/   0x1400D919, 0x1400D92F, 0x1400D945,
			/*GIAS03*/   0x1400E23D, 0x1400E253, 0x1400E269,
			/*GIAS04*/   0x1400EB95, 0x1400EBAB, 0x1400EBC1, 0x1400EBD7,
			/*GIAS05*/   0x1400FDD9, 0x1400FDEF, 0x1400FE05, 0x1400FE1B,
			/*GIAS06A*/  0x14010701, 0x14010717,  //different than in vanilla
			/*GIAS06B*/  0x1403B805, 0x1403B81B,
			/*GIAS07*/   0x14011029, 0x1401103F, 0x14011055, 0x1401106B,
			/*GIAS08*/   0x14011961, 0x14011977,
			/*GIAS09*/   0x14012289, 0x1401229F, 0x140122B5,
			/*KODA00*/   0x140134ED, 0x14013503,
			/*KODA01*/   0x14014751, 0x14014767, 0x1401477D, 0x14014793,
			/*KODA02*/   0x14015081, 0x14015097, 0x140150AD,
			/*KODA03*/   0x140162DD, 0x140162F3, 0x14016309, 0x1401631F,
			/*KODA07*/   0x140190C1,
			/*FRZL01*/   0x1401AC65, 0x1401AC7B, 0x1401AC91,
			/*FRZL02*/   0x1401B58D, 0x1401B5A3, 
			/*FRZL03*/   0x1401BEBD, 0x1401BED3, 0x1401BEE9,
			/*FRZL04*/   0x1401C7F5, 0x1401C80B, 0x1401C821,
			/*FRZL06*/   0x1401DA59, 0x1401DA6F, 0x1401DA85,
			/*FRZL07*/   0x1401E381, 0x1401E397, 0x1401E3AD,
			/*FRZL08*/   0x1401ECB1, 0x1401ECC7, 0x1401ECDD, 0x1401ECF3,
			/*FRZL12*/   0x1401FF0D, 0x1401FF23,
			/*FRZL13*/   0x1403C13D, 0x1403C153,
			/*FRZL14*/   0x1403CA65, 0x1403CA7B,
			/*FRZL15*/   0x1403DCC9,
			/*FRZL16*/   0x1403E5FD,
			/*BETL01*/   0x140251D1, 
			/*MIST01*/   0x140312B1, 0x140312C7, 0x140312DD, 0x140312F3,
			/*MIST02*/   0x14031BED, 0x14031C03, 0x14031C19,
			/*MIST03*/   0x14032529, 0x1403253F, 0x14032555, 0x1403256B,
			/*MIST04*/   0x14033775, 0x1403378B, 0x140337A1, 
			/*MIST05*/   0x14034091, 0x140340A7, 0x140340BD, 0x140340D3,
			/*MIST06*/   0x14035C2D, 0x14035C43, 0x14035C59,
			/*MIST07*/   0x14036571, 0x14036587, 0x1403659D, 0x140365B3,
			/*STIC01*/   0x140413F1, 0x14041407, 0x1404141D, 0x14041433,
			/*STIC02*/   0x14041D1D, 0x14041D33, 0x14041D49
		},
		centarumonItems =
		{
			/*TROP06*/  
			0x13FE9FD5, 0x13FE9FEB,  //first pair
			0x13FEA001, 0x13FEA017,  //second pair
			0x13FEA02D, 0x13FEA043,  //third pair
			0x13FEA059, 0x13FEA06F,  //fourth pair
			0x13FEA085, 0x13FEA09B,  //fifth pair
			0x13FEA0F3, 0x13FEA107,  //sixth pair
			0x13FEA0B1, 0x13FEA0C7, 0x13FEA0DD,	 //last items		
		},
		exceptions =
		{
			/*GCAN09*/  0x13FFCEA1, 0x13FFCEB7, 0x13FFCECD
		},
		exceptionsHardcore =
		{
			/*GCAN09*/  0x13FFCEA5, 0x13FFCEBB, 0x13FFCED1
		};

        bin.Position = 0x13FFCE9A;
		int check = bin.ReadByte();
		switch (spawnRateItemsOpt)
		{
			case 0:
				List<int> items = new List<int>();
				foreach (uint offset in itemSpawnRateOffsets)
				{
					bin.Position = offset;
					items.Add(bin.ReadByte());
				}

				if (check == 0xFF)
				{
					foreach (uint offset in exceptionsHardcore)
						bin.Position = offset;
					items.Add(bin.ReadByte());
				}
				else
				{
					foreach (uint offset in exceptions)
						bin.Position = offset;
					items.Add(bin.ReadByte());
				}

				int[] shuffledChances = items.ToArray();
				numberGenerator.Shuffle<int>(shuffledChances);
				for (int i = 0; i < itemSpawnRateOffsets.Length; i++)
				{
					bin.Position = itemSpawnRateOffsets[i];
					if (shuffledChances.Length > i)
						bin.WriteByte((byte)shuffledChances[i]);
					else
						bin.WriteByte((byte)numberGenerator.Next(100));
				}
				break;
			case 1:
				foreach (uint offset in itemSpawnRateOffsets)
				{
					bin.Position = offset;
					byte random = (byte)numberGenerator.Next(100);
					bin.WriteByte(random);
				}
				if (check == 0xFF)
				{
					foreach (uint offset in exceptionsHardcore)
					{
						bin.Position = offset;
						byte random = (byte)numberGenerator.Next(100);
						bin.WriteByte(random);
					}
				}
				else
				{
					foreach (uint offset in exceptions)
					{
						bin.Position = offset;
						byte random = (byte)numberGenerator.Next(100);
						bin.WriteByte(random);
					}
				}
				break;
		}

		for (int i = 0; i < centarumonItems.Length; i++)
		{
			bin.Position = centarumonItems[i];

			if (i < 12)
			{
				byte random = (byte)numberGenerator.Next(99);
				bin.WriteByte(random);
				i++;
				bin.Position = centarumonItems[i];
				random = (byte)(random + (byte)numberGenerator.Next(100));
				if (random > 100)
					random = 100;
				bin.WriteByte(random);
			}
			else
			{
				byte random = (byte)numberGenerator.Next(50);
				bin.WriteByte(random);
				i++;
				bin.Position = centarumonItems[i];
				random = (byte)(random + (byte)numberGenerator.Next(50));
				if (random > 98)
					random = 98;
				bin.WriteByte(random);
				i++;
				bin.Position = centarumonItems[i];
				random = (byte)(random + (byte)numberGenerator.Next(50));
				if (random > 100)
					random = 100;
				bin.WriteByte(random);
				break;
			}
		}
	}

	void RandomizeItemDrops()
	{
		uint itemDropsInitialOffset = 0x14D6E9FD, currentOffset = itemDropsInitialOffset;
		uint[] jumpOffsets = { 0x14D6EB28, 0x14D6F458, 0x14D6FD88, 0x14D706B8, 0x14D70FE8, 0x79999999 };
		int jumpValue = 0;

		bin.Position = itemDropsInitialOffset;
		switch (itemsSpawnOpt)
		{
			case 0:
				int shuffleIterator = 0;
				List<int> itemDrops = new List<int>();
				for (int i = 0; i < 180; i++)
				{
					int value = bin.ReadByte();
					currentOffset = currentOffset + 0x34;
					if (currentOffset > jumpOffsets[jumpValue])
					{
						currentOffset = currentOffset + 0x130;
						jumpValue++;
					}
					bin.Position = currentOffset;

					if (value == 0 && i != 3 && i != 83)
						continue;
					itemDrops.Add(value);
				}

				int[] shuffledValues = itemDrops.ToArray();
				numberGenerator.Shuffle<int>(shuffledValues);

				currentOffset = itemDropsInitialOffset;
				jumpValue = 0;
				bin.Position = itemDropsInitialOffset;

				for (int i = 0; i < 180; i++)
				{
					int value = bin.ReadByte();
					bin.Position = currentOffset;
					currentOffset = currentOffset + 0x34;
					if (currentOffset > jumpOffsets[jumpValue])
					{
						currentOffset = currentOffset + 0x130;
						jumpValue++;
					}
					if (value == 0 && i != 3 && i != 83)
						continue;
					if (shuffleIterator < shuffledValues.Length)
					{
						bin.WriteByte((byte)shuffledValues[shuffleIterator]);
						shuffleIterator++;
					}
					else
						bin.WriteByte((byte)numberGenerator.Next(71));
				}
				break;
			case 1:
				for (int i = 0; i < 180; i++)
				{
					bin.Position = currentOffset;
					currentOffset = currentOffset + 0x34;
					if (currentOffset > jumpOffsets[jumpValue])
					{
						currentOffset = currentOffset + 0x130;
						jumpValue++;
					}
					byte rando = (byte)numberGenerator.Next(73);
					if (rando == 71)
						rando = 121;
					else if (rando == 72)
						rando = 122;
					bin.WriteByte(rando);
				}
				break;
			case 2:
				RandomizeDigimonDataByteOnlyMax(0x14D6E9FD, 128);
				break;
		}
	}

	void RandomizeItemDropRate()
	{
		switch (dropRateOpt)
		{
			case 0:
				uint itemDropRateInitialOffset = 0x14D6E9FE, currentOffset = itemDropRateInitialOffset;
				uint[] jumpOffsets = { 0x14D6EB28, 0x14D6F458, 0x14D6FD88, 0x14D706B8, 0x14D70FE8, 0x79999999 };
				int jumpValue = 0;

				bin.Position = itemDropRateInitialOffset;
				int shuffleIterator = 0;
				List<int> itemDrops = new List<int>();
				for (int i = 0; i < 180; i++)
				{
					int value = bin.ReadByte();
					currentOffset = currentOffset + 0x34;
					if (currentOffset > jumpOffsets[jumpValue])
					{
						currentOffset = currentOffset + 0x130;
						jumpValue++;
					}
					bin.Position = currentOffset;

					if (value == 0)
						continue;
					itemDrops.Add(value);
				}

				int[] shuffledValues = itemDrops.ToArray();
				numberGenerator.Shuffle<int>(shuffledValues);

				currentOffset = itemDropRateInitialOffset;
				jumpValue = 0;
				bin.Position = itemDropRateInitialOffset;

				for (int i = 0; i < 180; i++)
				{
					int value = bin.ReadByte();
					bin.Position = currentOffset;
					currentOffset = currentOffset + 0x34;
					if (currentOffset > jumpOffsets[jumpValue])
					{
						currentOffset = currentOffset + 0x130;
						jumpValue++;
					}
					if (value == 0)
						continue;
					if (shuffleIterator < shuffledValues.Length)
					{
						bin.WriteByte((byte)shuffledValues[shuffleIterator]);
						shuffleIterator++;
					}
					else
						bin.WriteByte((byte)numberGenerator.Next(100));
				}
				break;
			case 1:
				RandomizeDigimonDataByteOnlyMax(0x14D6E9FE, 100);
				break;
		}
	}

	void RandomizeChests()
	{
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
        int machiChest = numberGenerator.Next(chestItemOffsets.Length);
		switch (chestsOpt)
		{
			case 0:
				List<int> items = new List<int>();
				foreach (uint offset in chestItemOffsets)
				{
					bin.Position = offset;
					items.Add(bin.ReadByte());
				}

				int[] shuffledItems = items.ToArray();
				numberGenerator.Shuffle<int>(shuffledItems);
				for (int i = 0; i < chestItemOffsets.Length; i++)
				{
					bin.Position = chestItemOffsets[i];
					if (shuffledItems.Length > i)
						bin.WriteByte((byte)shuffledItems[i]);
					else
						bin.WriteByte((byte)numberGenerator.Next(115));
				}
				break;
			case 1:				
				for (int offset = 0; offset < chestItemOffsets.Length; offset++)
				{

					bin.Position = chestItemOffsets[offset];
					byte random;
					if (machiChest != offset)
					{
						random = (byte)numberGenerator.Next(120);
						if (random == 115)
							random = 121;
						else if (random == 116)
							random = 122;
						else if (random == 117)
							random = 125;
						else if (random == 118)
							random = 126;
						else if (random == 119)
							random = 127;
					}
					else
						random = 124;
						bin.WriteByte(random);
				}
				break;
			case 2:			    
				for (int offset = 0; offset < chestItemOffsets.Length; offset++)
				{
					bin.Position = chestItemOffsets[offset];
					byte random;
					if (machiChest != offset)
						random = (byte)numberGenerator.Next(128);
					else
						random = 124;
					bin.WriteByte(random);
				}
				break;
		}
	}

	void RandomizeShops()
	{
		uint[] normalShopsOffsets =
		{
			0x1403BDFE, 0x1403BE02, 0x1403BE06, 0x1403BE0A, 0x1403BE0E, 0x1403BE12, 0x1403BE16, //Gear Savanna Shop
			0x1405CD6A, 0x1405CD6E, 0x1405CD72, 0x1405CD76, 0x1405CD7A, 0x1405CD7E, //Coelamon + Betamon shop speak to Betamon
			0x1405CDA6, 0x1405CDAA, 0x1405CDAE, //Coelamon or Betamon only shop speak to Betamon
			0x1405E42E, 0x1405E432, 0x1405E436, 0x1405E43A, 0x1405E43E, 0x1405E442, //Coelamon + Betamon shop interact with the shop
			0x1405E46A, 0x1405E46E, 0x1405E472, //Coelamon or Betamon only shop interact with the shop
			0x14070322, 0x14070326, //Numemon shop
			0x1407034A, 0x1407034E, //Mojyamon shop
			0x14070372, 0x14070376, 0x1407037A, 0x1407037E, //Mamemon shop
			0x140703AA, 0x140703AE, 0x140703B2,  //Devimon shop
			0x1407C74E, 0x1407C752, 0x1407C756, 0x1407C75A, 0x1407C75E, 0x1407C762 //Gekkomon shop
		},
		normalShopsRemovalOffsets =
		{
			0x1403BE24, 0x1403BE28, 0x1403BE2C, 0x1403BE30, 0x1403BE34, 0x1403BE38, 0x1403BE3C, //Gear Savanna Shop
			0x1405CD8C, 0x1405CD90, 0x1405CD94, 0x1405CD98, 0x1405CD9C, 0x1405CDA0, //Coelamon + Betamon shop speak to Betamon
			0x1405CDBC, 0x1405CDC0, 0x1405CDC4, //Coelamon or Betamon only shop speak to Betamon
			0x1405E450, 0x1405E454, 0x1405E458, 0x1405E45C, 0x1405E460, 0x1405E464, //Coelamon + Betamon shop interact with the shop
			0x1405E494, 0x1405E498, 0x1405E49C, //Coelamon or Betamon only shop interact with the shop
			0x14070334, 0x14070338, //Numemon shop
			0x1407035C, 0x14070360, //Mojyamon shop
			0x1407038C, 0x14070390, 0x14070394, 0x14070398, //Mamemon shop
			0x140703C0, 0x140703C4, 0x140703C8,  //Devimon shop
			0x1407C770, 0x1407C774, 0x1407C778, 0x1407C77C, 0x1407C780, 0x1407C784  //Gekkomon shop
		},
		shopItemsCount = { 7, 6, 3, 6, 3, 2, 2, 4, 3, 6 };
		int currentShop = 0, currentShopIteration = 0;
		List<int> shopCurrentItems = new List<int>();
		CreateRandomShopItems(ref shopCurrentItems, shopItemsCount[currentShop]);

		for (int offset = 0; offset < normalShopsOffsets.Length; offset++)
		{
			bin.Position = normalShopsOffsets[offset];
			writter.Write((short)(shopCurrentItems[currentShopIteration] + 384));
			bin.Position = normalShopsRemovalOffsets[offset];
			writter.Write((short)(shopCurrentItems[currentShopIteration] + 384));
			currentShopIteration++;

			if (currentShopIteration >= shopCurrentItems.Count)
			{
				currentShop++;
				shopCurrentItems.Clear();
				if (currentShop >= shopItemsCount.Length)
					break;
				CreateRandomShopItems(ref shopCurrentItems, shopItemsCount[currentShop]);
				currentShopIteration = 0;
			}
		}
		uint bigShopRemovalOffset = 0x1406E65A;
		shopCurrentItems.Clear();

		CreateRandomShopItems(ref shopCurrentItems, 19);

		List<uint[]> bigShopItems = new List<uint[]>()
		{
  			new uint[] { 0x1406E484, 0x1406E500, 0x1406E57C, 0x1406E604 },
   			new uint[] { 0x1406E488, 0x1406E504, 0x1406E580, 0x1406E608 },
			new uint[] { 0x1406E48C, 0x1406E508, 0x1406E584, 0x1406E60C },
			new uint[] { 0x1406E490, 0x1406E50C, 0x1406E588, 0x1406E610 },
			new uint[] { 0x1406E494, 0x1406E510, 0x1406E58C, 0x1406E614 },
			new uint[] { 0x1406E498, 0x1406E514, 0x1406E590, 0x1406E618 },
			new uint[] { 0x1406E49C, 0x1406E518, 0x1406E594, 0x1406E61C },
			new uint[] { 0x1406E4A0, 0x1406E51C, 0x1406E598, 0x1406E620 },
			new uint[] { 0x1406E4A4, 0x1406E520, 0x1406E59C, 0x1406E624 },
			new uint[] { 0x1406E4A8, 0x1406E524, 0x1406E5A0, 0x1406E628 },
			new uint[] { 0x1406E4B8, 0x1406E53C, 0x1406E5C4, 0x1406E640 },
			new uint[] { 0x1406E4C8, 0x1406E54C, 0x1406E5D4, 0x1406E644 },
			new uint[] { 0x1406E4D8, 0x1406E55C, 0x1406E5E4, 0x1406E64C },
			new uint[] { 0x1406E4E8, 0x1406E56C, 0x1406E5F4, 0x1406E648 },
			new uint[] { 0x1406E528, 0x1406E5A4, 0x1406E62C},
			new uint[] { 0x1406E52C, 0x1406E5A8, 0x1406E630},
			new uint[] { 0x1406E5AC, 0x1406E634},
			new uint[] { 0x1406E5B0, 0x1406E638},
			new uint[] { 0x1406E5B4, 0x1406E63C}
		};

		for (int i = 0; i < shopCurrentItems.Count; i++)
		{
			bin.Position = bigShopRemovalOffset;
			writter.Write((short)(shopCurrentItems[i] + 384));
			bigShopRemovalOffset = bigShopRemovalOffset + 4;

			foreach (uint offset in bigShopItems[i])
			{
				bin.Position = offset;
				writter.Write((short)(shopCurrentItems[i] + 384));
			}
		}
		shopCurrentItems.Clear();

		bin.Position = 0x1402FCA0;

		if (bin.ReadByte() == 0x1C) //Check if helpful items 2 is active
		{
			currentShopIteration = 0;
			CreateRandomShopItems(ref shopCurrentItems, 11);

			uint[] gymShop =
			{
				0x1402FCA2, 0x1402FCA6, 0x1402FCAA, 0x1402FCAE, 0x1402FCB2, 0x1402FCB6, 0x1402FCBA, 0x1402FCBE, 0x1402FCC2, 0x1402FCC6, 0x1402FCCA,  //Shop 1
				0x1402FDAC, 0x1402FDB0, 0x1402FDB4, 0x1402FDB8, 0x1402FDBC, 0x1402FDC0, 0x1402FDC4, 0x1402FDC8, 0x1402FDCC, 0x1402FDD0, 0x1402FDD4, 0x1402FDD8 //Shop 2
			},
			gymShopRemoval =
			{
				0x1402FCD8, 0x1402FCDC, 0x1402FCE0, 0x1402FCE4, 0x1402FCE8, 0x1402FCEC, 0x1402FCF0, 0x1402FCF4, 0x1402FCF8, 0x1402FCFC, 0x1402FD00, //Shop 1
				0x1402FDEA, 0x1402FDEE, 0x1402FDF2, 0x1402FDF6, 0x1402FDFA, 0x1402FDFE, 0x1402FE02, 0x1402FE06, 0x1402FE0A, 0x1402FE0E, 0x1402FE12, 0x1402FE16 //Shop 2
			};

			for (int offset = 0; offset < gymShop.Length; offset++)
			{
				bin.Position = gymShop[offset];
				writter.Write((short)(shopCurrentItems[currentShopIteration] + 384));
				bin.Position = gymShopRemoval[offset];
				writter.Write((short)(shopCurrentItems[currentShopIteration] + 384));
				currentShopIteration++;

				if (currentShopIteration >= shopCurrentItems.Count)
				{
					shopCurrentItems.Clear();
					CreateRandomShopItems(ref shopCurrentItems, 12);
					currentShopIteration = 0;
				}
			}
		}
		shopCurrentItems.Clear();
	}

	void RandomizeShopPrices()
	{
		uint shopPricesInitialOffset = 0x14D676D8, currentOffset = shopPricesInitialOffset;
		uint[] jumpOffsets = { 0x14D67CE8, 0x14D68618, 0x79999999 };
		int jumpValue = 0;

		bin.Position = shopPricesInitialOffset;

		switch (shopsPricesOpt)
		{
			case 0:
				List<int> prices = new List<int>();
				for (int i = 0; i < 128; i++)
				{
					int value = reader.ReadInt32();
					currentOffset = currentOffset + 0x20;
					if (currentOffset > jumpOffsets[jumpValue])
					{
						currentOffset = currentOffset + 0x130;
						jumpValue++;
					}
					bin.Position = currentOffset;
					prices.Add(value);
				}

				int[] shuffledValues = prices.ToArray();
				numberGenerator.Shuffle<int>(shuffledValues);

				currentOffset = shopPricesInitialOffset;
				jumpValue = 0;
				bin.Position = shopPricesInitialOffset;

				for (int i = 0; i < 128; i++)
				{
					bin.Position = currentOffset;
					currentOffset = currentOffset + 0x20;
					if (currentOffset > jumpOffsets[jumpValue])
					{
						currentOffset = currentOffset + 0x130;
						jumpValue++;
					}

					if (i < shuffledValues.Length)
						writter.Write(shuffledValues[i]);
					else
					{
						int value = numberGenerator.Next(1000) * 10;

						if (value == 0)
							value = 9999;
							
						writter.Write(value);
					}
						
				}
				break;
			case 1:
				for (int i = 0; i < 128; i++)
				{
					bin.Position = currentOffset;
					currentOffset = currentOffset + 0x20;
					if (currentOffset > jumpOffsets[jumpValue])
					{
						currentOffset = currentOffset + 0x130;
						jumpValue++;
					}
					int value = numberGenerator.Next(1000) * 10;

					if (value == 0)
						value = 9999;
							
					writter.Write(value);
				}
				break;
		}
	}

	void RandomizeMojyamon()
	{
		uint MojyamonOffset = 0x14D727B0;
		bin.Position = MojyamonOffset;
		switch (MojyamonOpt)
		{
			case 0:
				for (int i = 0; i < 9; i++)
				{
					byte rando = (byte)numberGenerator.Next(73);
					if (rando == 71)
						rando = 121;
					else if (rando == 72)
						rando = 122;
					bin.Position = MojyamonOffset + 12;
					rando = (byte)numberGenerator.Next(73);
					if (rando == 71)
						rando = 121;
					else if (rando == 72)
						rando = 122;
					MojyamonOffset++;
					bin.Position = MojyamonOffset;
				}
				break;
			case 1:
				for (int i = 0; i < 9; i++)
				{
					bin.WriteByte((byte)numberGenerator.Next(128));
					bin.Position = MojyamonOffset + 12;
					bin.WriteByte((byte)numberGenerator.Next(128));
					MojyamonOffset++;
					bin.Position = MojyamonOffset;
				}
				break;
		}
	}

	void RandomizeMerit()
	{
		uint meritInitialOffset = 0x14D676DC, currentOffset = meritInitialOffset;
		uint[] jumpOffsets = {0x14D67CE8, 0x14D68618, 0x79999999 }, allOffsets = new uint[128];
		int jumpValue = 0;
	
		List<short> meritItems = new List<short>();
		bin.Position = currentOffset;
		for (int i = 0; i < 128; i++)
		{
			short value = reader.ReadInt16();
			if (value != 0)
			{
				meritItems.Add(value);
				bin.Position = currentOffset;
				writter.Write((short)0);
			}
			allOffsets[i] = currentOffset;
			currentOffset = currentOffset + 0x20;
			if (currentOffset > jumpOffsets[jumpValue])
			{
				currentOffset = currentOffset + 0x130;
				jumpValue++;
			}
			bin.Position = currentOffset;
		}

		List<int> itemId = new List<int>() { 117 };

		switch (meritItemsOpt)
		{
			case 0:
				while (itemId.Count < meritItems.Count)
				{
					byte random = (byte)numberGenerator.Next(120);
					if (random == 115)
						random = 121;
					else if (random == 116)
						random = 122;
					else if (random == 117)
						random = 125;
					else if (random == 118)
						random = 126;
					else if (random == 119)
						random = 127;

					bool valid = true;
					foreach (int item in itemId)
					{
						if (item == random)
						{
							valid = false;
							break;
						}
					}
					if (valid)
						itemId.Add(random);
				}
				break;
			case 1:
				while (itemId.Count < meritItems.Count)
				{
					byte random = (byte)numberGenerator.Next(128);

					bool valid = true;
					foreach (int item in itemId)
					{
						if (item == random)
						{
							valid = false;
							break;
						}
					}
					if (valid)
						itemId.Add(random);
				}
				break;
		}

		for (int i = 0; i < meritItems.Count; i++)
		{
			bin.Position = allOffsets[itemId[i]];
			writter.Write(meritItems[i]);
		}
	}

	void RandomizeMeritPrices()
	{
		uint meritInitialOffset = 0x14D676DC, currentOffset = meritInitialOffset;
		uint[] jumpOffsets = { 0x14D67CE8, 0x14D68618, 0x79999999 };
		List<uint> meritOffsets = new List<uint>();
		int jumpValue = 0;

		switch (meritPricesOpt)
		{
			case 0:
				if (meritItems)
					break;
				List<short> meritItemsP = new List<short>();
				bin.Position = currentOffset;
				for (int i = 0; i < 128; i++)
				{
					short value = reader.ReadInt16();
					if (value != 0)
					{
						meritItemsP.Add(value);
						meritOffsets.Add(currentOffset);
					}
					currentOffset = currentOffset + 0x20;
					if (currentOffset > jumpOffsets[jumpValue])
					{
						currentOffset = currentOffset + 0x130;
						jumpValue++;
					}
					bin.Position = currentOffset;

				}

				short[] shuffledValues = meritItemsP.ToArray();
				numberGenerator.Shuffle<short>(shuffledValues);

				for (int i = 0; i < meritOffsets.Count; i++)
				{
					bin.Position = meritOffsets[i];
					if (i < shuffledValues.Length)
						writter.Write(shuffledValues[i]);
					else
						writter.Write((short)(numberGenerator.Next(2000) + 1));
				}
				break;
			case 1:
				for (int i = 0; i < 128; i++)
				{
					bin.Position = currentOffset;
					short value = reader.ReadInt16();
					if (value != 0)
					{
						bin.Position = currentOffset;
						writter.Write((short)(numberGenerator.Next(2000) + 1));
					}
					currentOffset = currentOffset + 0x20;
					if (currentOffset > jumpOffsets[jumpValue])
					{
						currentOffset = currentOffset + 0x130;
						jumpValue++;
					}

				}
				break;
		}
	}

	void RandomizeTournamentPrizes()
	{
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

		switch (tournamentItemsOpt)
		{
			case 0:
				List<int> items = new List<int>();
				foreach (uint offset in tournamentItemsOffsets)
				{
					bin.Position = offset;
					items.Add(bin.ReadByte());
				}

				int[] shuffledItems = items.ToArray();
				numberGenerator.Shuffle<int>(shuffledItems);
				for (int i = 0; i < tournamentItemsOffsets.Length; i++)
				{
					bin.Position = tournamentItemsOffsets[i];
					if (shuffledItems.Length > i)
						bin.WriteByte((byte)shuffledItems[i]);
					else
						bin.WriteByte((byte)numberGenerator.Next(115));
				}
				break;
			case 1:
				foreach (uint offset in tournamentItemsOffsets)
				{
					bin.Position = offset;
					byte random = (byte)numberGenerator.Next(128);
					bin.WriteByte(random);
				}
				break;
		}
	}

	void RandomizeTokomon()
	{
		uint TokomonInitialOffset = 0x14071066, itemCount = 6;
		switch (tokomonOpt)
		{
			case 0:
				TokomonInitialOffset = 0x14071066;
				itemCount = 6;
				break;
			case 1:
				bin.Position = 0x14071054;
				if (bin.ReadByte() != 0x28)
				{
					tokomonOpt = 0;
					itemCount = 6;
					TokomonInitialOffset = 0x14071066;
					break;
				}				
				itemCount = 10;
				break;
		}
		bin.Position = TokomonInitialOffset;
		for (int i = 0; i < itemCount; i++)
		{
			bin.WriteByte((byte)numberGenerator.Next(128));
			bin.WriteByte((byte)(numberGenerator.Next(10) + 1));
			bin.Position = bin.Position + 2;
		}
	}

	void UnlockKeyItems()
	{
		uint currentOffset = 0x14D676E1;
		uint[] jumpOffsets = { 0x14D67CE8, 0x14D68618, 0x79999999 };
		int jumpValue = 0;
		bin.Position = currentOffset;

		for (int i = 0; i < 128; i++)
		{
			bin.WriteByte(1);
			currentOffset = currentOffset + 0x20;
			if (currentOffset > jumpOffsets[jumpValue])
			{
				currentOffset = currentOffset + 0x130;
				jumpValue++;
			}
			bin.Position = currentOffset;
		}

	}

	void RandomizeCurlingRewards()
	{
		uint[] curlingRewardsOffsets =
		{
			0x14096AF0, 0x14096B08, 0x14096B20, 0x14096B38, 0x14096B50, 0x14096B68, //Penguinmon
			0x1409739A, 0x140973B2, 0x140973CA, 0x140973E2, 0x140973FA, 0x14097412, 0x1409742A, 0x14097442 //MetalMamemon
		};

		foreach (uint offset in curlingRewardsOffsets)
		{
			bin.Position = offset;
			byte random = (byte)numberGenerator.Next(128);
			bin.WriteByte(random);
		}
	}

	void RandomizeTechDamage()
	{
		uint startingOffset = 0x14D66DF8, currentOffset = startingOffset, jumpOffset = 0x14D673B8;
		bool jumped = false;


		bin.Position = startingOffset;
		switch (damageTechOpt)
		{
			case 0:
				if (finishers && finishersOpt == 0)
				{
					ShuffleTechPower(122, startingOffset, jumpOffset);
				}
				else
				{
					ShuffleTechPower(57, startingOffset, jumpOffset);
				}

				if (finishers && finishersOpt == 1)
				{
					ShuffleTechPower(65, 0x14D67188, jumpOffset);
				}
				break;
			case 1:
				for (int i = 0; i < 57; i++)
				{
					bin.Position = currentOffset;
					currentOffset = currentOffset + 0x10;
					if (i == 21 || i == 30 || i == 34 || i == 41 || i == 42)
						continue;
					writter.Write((short)(numberGenerator.Next(751) + 50));
				}

				if (finishers)
				for (int i = 0; i < 65; i++)
				{
					bin.Position = currentOffset;
					currentOffset = currentOffset + 0x10;
					if (!jumped && currentOffset >= jumpOffset)
					{
						currentOffset = currentOffset + 0x130;
						jumped = true;
					}
					if (i == 47)
						continue;
					writter.Write((short)(numberGenerator.Next(301) + 100));
				}

				break;
			case 2:
				for (int i = 0; i < 57; i++)
				{
					bin.Position = currentOffset;
					currentOffset = currentOffset + 0x10;
					if (i == 21 || i == 30 || i == 34 || i == 41 || i == 42)
						continue;
					writter.Write((short)(numberGenerator.Next(999) + 1));
				}
				
				if (finishers)
				for (int i = 0; i < 65; i++)
				{
					bin.Position = currentOffset;
					currentOffset = currentOffset + 0x10;
					if (!jumped && currentOffset >= jumpOffset)
					{
						currentOffset = currentOffset + 0x130;
						jumped = true;
					}
					writter.Write((short)(numberGenerator.Next(999) + 1));
				}
				break;
		}
	}

	void RandomizeMP()
	{
		uint startingOffset = 0x14D66DFA, currentOffset = startingOffset, jumpOffset = 0x14D673B8;

		bin.Position = startingOffset;
		switch (MPtechOpt)
		{
			case 0:
				ShuffleTechDataByte(57, startingOffset, jumpOffset);
				break;
			case 1:
				for (int i = 0; i < 57; i++)
				{
					bin.Position = currentOffset;
					currentOffset = currentOffset + 0x10;
					bin.WriteByte((byte)(numberGenerator.Next(136) + 5));
				}
				break;
			case 2:
				for (int i = 0; i < 57; i++)
				{
					bin.Position = currentOffset;
					currentOffset = currentOffset + 0x10;
					bin.WriteByte((byte)(numberGenerator.Next(255) + 1));
				}
				break;
		}
	}

	void RandomizeTypeDamage()
	{
		byte[] values = { 1, 2, 5, 10, 15, 20, 30, 40 };
		uint offset = 0x14D669F8;

		bin.Position = offset;

		switch (damageTypeOpt)
		{
			case 0:
				List <byte> typeDamages = new List<byte>();
				for (int i = 0; i < 49; i++)				
					typeDamages.Add((byte)bin.ReadByte());

				byte[] shuffledValues = typeDamages.ToArray();
				numberGenerator.Shuffle<byte>(shuffledValues);

				bin.Position = offset;
				for (int i = 0; i < 49; i++)
					bin.WriteByte(shuffledValues[i]);
				break;
			case 1:
				for (int i = 0; i < 49; i++)				
					bin.WriteByte(values[numberGenerator.Next(5) + 1]);
				
				break;
			case 2:
				for (int i = 0; i < 49; i++)				
					bin.WriteByte(values[numberGenerator.Next(values.Length)]);

				bin.Position = 0x14B5CC8C;
				bin.WriteByte(0xa);
				bin.WriteByte(0);
				bin.WriteByte(0x2);
				bin.WriteByte(0x24);

				bin.Position = 0x14B5CC58;
				bin.WriteByte(0x3);
				bin.WriteByte(0);
				bin.WriteByte(0x61);
				bin.WriteByte(0x28);
				bin.WriteByte(0x14);
				bin.WriteByte(0);
				bin.WriteByte(0x1);
				bin.WriteByte(0x14);

				bin.Position = 0x14CA30CC;
				bin.WriteByte(0xa);
				bin.WriteByte(0);
				bin.WriteByte(0x2);
				bin.WriteByte(0x24);

				bin.Position = 0x14CA3098;
				bin.WriteByte(0x3);
				bin.WriteByte(0);
				bin.WriteByte(0x61);
				bin.WriteByte(0x28);
				bin.WriteByte(0x14);
				bin.WriteByte(0);
				bin.WriteByte(0x1);
				bin.WriteByte(0x14);

				bin.Position = 0x14C6A1B0;
				bin.WriteByte(0xa);
				bin.WriteByte(0);
				bin.WriteByte(0x2);
				bin.WriteByte(0x24);

				bin.Position = 0x14C6A17C;
				bin.WriteByte(0x3);
				bin.WriteByte(0);
				bin.WriteByte(0x61);
				bin.WriteByte(0x28);
				bin.WriteByte(0x14);
				bin.WriteByte(0);
				bin.WriteByte(0x1);
				bin.WriteByte(0x14);

				bin.Position = 0x14B5C83A;
				bin.WriteByte(0x41);
				bin.WriteByte(0x28);
				bin.Position = 0x14B5C83E;
				bin.WriteByte(1);

				bin.Position = 0x14B5C818;
				bin.WriteByte(0x3);
				bin.WriteByte(0);
				bin.WriteByte(0x41);
				bin.WriteByte(0x28);
				bin.WriteByte(0x14);
				bin.WriteByte(0);
				bin.WriteByte(0x1);
				bin.WriteByte(0x14);
				break;
		}
	}

	void RandomizeAccuracy()
	{
		uint startingOffset = 0x14D66DFF, currentOffset = startingOffset, jumpOffset = 0x14D673B8;

		bin.Position = startingOffset;
		switch (accuracyOpt)
		{
			case 0:
				ShuffleTechDataByte(57, startingOffset, jumpOffset);
				break;
			case 1:
				for (int i = 0; i < 57; i++)
				{
					bin.Position = currentOffset;
					currentOffset = currentOffset + 0x10;
					bin.WriteByte((byte)(numberGenerator.Next(61) + 40));
				}
				break;
			case 2:
				for (int i = 0; i < 57; i++)
				{
					bin.Position = currentOffset;
					currentOffset = currentOffset + 0x10;
					bin.WriteByte((byte)numberGenerator.Next(121));
				}
				break;
		}
	}

	void RandomizeStatus()
	{
		uint startingOffset = 0x14D66DFE, currentOffset = startingOffset;

		bin.Position = startingOffset;
		switch (statusOpt)
		{
			case 0:
				List<byte> statusEffects = new List<byte>(), effectChances = new List<byte>();
				List<uint> offsets = new List<uint>();
				for (int i = 0; i < 57; i++)
				{
					bin.Position = currentOffset;
					byte value = (byte)bin.ReadByte();
					if (value != 0)
					{
						statusEffects.Add(value);
						bin.Position = currentOffset + 2;
						effectChances.Add((byte)bin.ReadByte());
						offsets.Add(currentOffset);
					}
					currentOffset = currentOffset + 0x10;
				}

				uint[] shuffledOffsets = offsets.ToArray();
				numberGenerator.Shuffle<uint>(shuffledOffsets);

				for (int i = 0; i < shuffledOffsets.Length; i++)
				{
					bin.Position = shuffledOffsets[i];
					if (statusEffects.Count < i)					
						bin.WriteByte(statusEffects[i]);
					else
						bin.WriteByte((byte)numberGenerator.Next(5));
					bin.Position = shuffledOffsets[i] + 2;
					if (effectChances.Count < i)
						bin.WriteByte(effectChances[i]);
					else
						bin.WriteByte((byte)numberGenerator.Next(101));
					
				}
				break;
			case 1:
				for (int i = 0; i < 57; i++)
				{
					if (i == 21 || i == 30 || i == 34 || i == 41 || i == 42)
						continue;
					bin.Position = currentOffset;
					int selectedStatus = numberGenerator.Next(20);
					if (selectedStatus < 4)					
						bin.WriteByte(2);									
					else if (selectedStatus < 7)
						bin.WriteByte(1);
					else if (selectedStatus < 10)
						bin.WriteByte(4);
					else if (selectedStatus < 12)
						bin.WriteByte(3);
					else
						bin.WriteByte(0);

					bin.Position = currentOffset + 2;
					if (selectedStatus < 12)					
						bin.WriteByte((byte)(numberGenerator.Next(40) + 1));					
					else
						bin.WriteByte(0);
						currentOffset = currentOffset + 0x10;					
				}
				break;
			case 2:
				for (int i = 0; i < 57; i++)
				{
					if (i == 21 || i == 30 || i == 34 || i == 41 || i == 42)
						continue;
					bin.Position = currentOffset;
					bin.WriteByte((byte)numberGenerator.Next(5));
					bin.Position = currentOffset + 2;
					bin.WriteByte((byte)(numberGenerator.Next(100) + 1));
					currentOffset = currentOffset + 0x10;
				}
				break;
		}
	}

	void RandomizeStatusChance()
	{
		uint startingOffset = 0x14D66E00, currentOffset = startingOffset;

		bin.Position = startingOffset;
		switch (statusOpt)
		{
			case 0:
				List<byte>  effectChances = new List<byte>();
				List<uint> offsets = new List<uint>();
				for (int i = 0; i < 57; i++)
				{
					bin.Position = currentOffset;
					byte value = (byte)bin.ReadByte();
					if (value != 0)
					{
						bin.Position = currentOffset;
						effectChances.Add((byte)bin.ReadByte());
						offsets.Add(currentOffset);
					}
					currentOffset = currentOffset + 0x10;
				}

				uint[] shuffledOffsets = offsets.ToArray();
				numberGenerator.Shuffle<uint>(shuffledOffsets);

				for (int i = 0; i < shuffledOffsets.Length; i++)
				{
					bin.Position = shuffledOffsets[i];
					if (effectChances.Count < i)	
						bin.WriteByte(effectChances[i]);
					else
						bin.WriteByte((byte)numberGenerator.Next(101));					
				}
				break;
			case 1:
				for (int i = 0; i < 57; i++)
				{
					bin.Position = currentOffset;
					byte value = (byte)bin.ReadByte();
					if (value != 0)
					{
						bin.Position = currentOffset;
						bin.WriteByte((byte)(numberGenerator.Next(40) + 1));
					}				
					currentOffset = currentOffset + 0x10;					
				}
				break;
			case 2:
				for (int i = 0; i < 57; i++)
				{
					bin.Position = currentOffset;
					bin.WriteByte((byte)(numberGenerator.Next(100) + 1));
					currentOffset = currentOffset + 0x10;
				}
				break;
		}
	}

	void RandomizeTechBoost()
	{
		uint startOffset = 0x14D627FF, currentOffset = startOffset;
		int[] exceptions = { 0, 1, 2, 15, 16, 29, 30, 43, 44, 255};
		uint techsInitialOffset = 0x14D6E9FF, currentOffsetDigimon = techsInitialOffset;
		uint[] jumpOffsets = { 0x14D6EB28, 0x14D6F458, 0x14D6FD88, 0x14D706B8, 0x14D70FE8, 0x79999999 };
		uint jumpOffsetBoost = 0x14D62A38;
		bool jumped = false;
		int jumpValue = 0, currentException = 0;
		List<byte> digimonTechs = new List<byte>();
		byte currentTech = 0;


		for (int i = 0; i < 66; i++)
		{
			if (i == exceptions[currentException])
				currentException++;
			else
			{
				bin.Position = currentOffsetDigimon;
				while (digimonTechs.Count < 16)
				{
					if (bin.Position >= jumpOffsets[jumpValue])
					{
						bin.Position = bin.Position + 0x130;
						currentOffsetDigimon = currentOffsetDigimon + 0x130;
						jumpValue++;
					}

					currentTech = (byte)bin.ReadByte();
					if (currentTech != 0xFF)
					{
						if (currentTech > 56 || currentTech == 21 || currentTech == 30 || currentTech == 34 || currentTech == 41 || currentTech == 42)
							continue;
						digimonTechs.Add(currentTech);
					}
					else
						break;
				}
				bin.Position = currentOffset;
				if (digimonTechs.Count > 0)
					bin.WriteByte(digimonTechs[numberGenerator.Next(digimonTechs.Count)]);
				else
					bin.WriteByte(0);
				digimonTechs.Clear();
			}
			currentOffset = currentOffset + 0x1C;
			if (!jumped && currentOffset >= jumpOffsetBoost)
			{
				currentOffset = currentOffset + 0x130;
				jumped = true;
			}
			currentOffsetDigimon = currentOffsetDigimon + 0x34;
			if (currentOffsetDigimon >= jumpOffsets[jumpValue])
			{
				currentOffsetDigimon = currentOffsetDigimon + 0x130;
				jumpValue++;
			}
			
		}
	}

	void RandomizeTechBoostPower()
	{		
		int[] RookiesPower = { 3, 4, 17, 18, 31, 32, 45, 46, 57 },
		ChampionsPower = { 5, 6, 7, 8, 9, 10, 11, 19, 20, 21, 22, 23, 24, 25, 33, 34, 35, 36, 37, 38, 39, 47, 48, 49, 50, 51, 52, 53, 58 },
		UltimatesPower = { 12, 13, 14, 26, 27, 28, 40, 41, 42, 54, 55, 56, 59, 60, 61, 62, 63, 64, 65 };

		switch (boostedTechValueOpt)
		{
			case 0:
				ShuffleBoostPower(RookiesPower);	
				ShuffleBoostPower(ChampionsPower);	
				ShuffleBoostPower(UltimatesPower);					
				break;
			case 1:
				randomizeBoostPower(RookiesPower, 151, 50);
				randomizeBoostPower(ChampionsPower, 301, 100);
				randomizeBoostPower(UltimatesPower, 551, 100);
				break;
			case 2:
				randomizeBoostPower(RookiesPower, 1000, 1);
				randomizeBoostPower(ChampionsPower, 1000, 1);
				randomizeBoostPower(UltimatesPower, 1000, 1);
				break;

		}
	}

	void RandomizeLearningChancesBattle()
	{
		uint offset = 0x14D66A2C, jumpOffset = 0x14D66A88;

		bin.Position = offset;
		switch (learnBattleOpt)
		{
			case 0:
				List<byte> learnChances = new List<byte>();
				for (int i = 0; i < 174; i++)
				{
					learnChances.Add((byte)bin.ReadByte());
					if (bin.Position == jumpOffset)
						bin.Position = bin.Position + 0x130;									
				}

				byte[] shuffledChances = learnChances.ToArray();
				numberGenerator.Shuffle<byte>(shuffledChances);	

				bin.Position = offset;
				for (int i = 0; i < 174; i++)
				{
					bin.WriteByte(shuffledChances[i]);
					if (bin.Position == jumpOffset)
						bin.Position = bin.Position + 0x130;	
				}
				learnChances.Clear();
				break;
			case 1:
			for (int i = 0; i < 174; i++)
				{
					switch (i % 3)
					{
						case 0:
							bin.WriteByte((byte)(numberGenerator.Next(71) + 10));
							break;
						case 1:
							bin.WriteByte((byte)(numberGenerator.Next(49) + 2));
							break;
						case 2:
							bin.WriteByte((byte)numberGenerator.Next(31));
							break;
					}
					if (bin.Position == jumpOffset)
								bin.Position = bin.Position + 0x130;									
				}
				break;
			case 2:
			for (int i = 0; i < 174; i++)
				{
					bin.WriteByte((byte)numberGenerator.Next(101));
					if (bin.Position == jumpOffset)
						bin.Position = bin.Position + 0x130;									
				}
				break;
		}
	}

	void RandomizeLearningChancesBrains()
	{
		uint OffsetChances = 0x14C8E58C, offsetTechs = 0x14C8E554;

		//shuffle tech learn order

		bin.Position = offsetTechs;
		List<byte[]> brainsTechs = new List<byte[]>();
		for (int i = 0; i < 7; i++)
		{
			brainsTechs.Add
			(new byte[] {(byte)bin.ReadByte(), (byte)bin.ReadByte(), (byte)bin.ReadByte(), (byte)bin.ReadByte(), (byte)bin.ReadByte(),
			(byte)bin.ReadByte(), (byte)bin.ReadByte(), (byte)bin.ReadByte()});											
		}
				

		foreach (byte[] shuffledTechs in brainsTechs)
		numberGenerator.Shuffle<byte>(shuffledTechs);	

		bin.Position = offsetTechs;
		for (int i = 0; i < 7; i++)
		{
			foreach (byte shuffleTech in brainsTechs[i])
			bin.WriteByte(shuffleTech);					
		}
		brainsTechs.Clear();


		//change tech learn chance
		bin.Position = OffsetChances;

		switch (learnBrainsOpt)
		{
			case 0:
				List<byte> learnChances = new List<byte>();
				for (int i = 0; i < 24; i++)
					learnChances.Add((byte)bin.ReadByte());

				byte[] shuffledChances = learnChances.ToArray();
				numberGenerator.Shuffle<byte>(shuffledChances);

				bin.Position = OffsetChances;
				for (int i = 0; i < 24; i++)
					bin.WriteByte(shuffledChances[i]);

				learnChances.Clear();
				break;
			case 1:
				for (int i = 0; i < 24; i++)
				{
					switch (i % 3)
					{
						case 0:
							bin.WriteByte((byte)(numberGenerator.Next(66) + 15));
							break;
						case 1:
							bin.WriteByte((byte)(numberGenerator.Next(39) + 2));
							break;
						case 2:
							bin.WriteByte((byte)numberGenerator.Next(21));
							break;
					}
				}
				break;
			case 2:
				for (int i = 0; i < 24; i++)
					bin.WriteByte((byte)numberGenerator.Next(101));
				break;
		}
	}

	void RandomizeGivenTechs()
	{
		uint[] offsetsTechs = { 0x14029E3F, 0x13FE219B, 0x13FE21E5, 0x13FE2233, 0x140109F3, 0x1402D473, 0x1402D4D1, 0x1402D52D },
		offsetsChecks = { 0x14029B32, 0x13FE2192, 0x13FE21DC, 0x13FE222A, 0x14010964, 0x1402D46A, 0x1402D4C8, 0x1402D524 };

		for (int i = 0; i < offsetsTechs.Length; i++)
		{
			byte randTech = (byte)numberGenerator.Next(57);
			bin.Position = offsetsTechs[i];
			bin.WriteByte(randTech);
			bin.Position = offsetsChecks[i];
			bin.WriteByte(randTech);
		}
	}

	void RandomizeEvoTree()
	{
		uint startOffset = 0x14D6CDF9, jumpOffset = 0x14D6CF98; //not really the start, but this is for the calculations
		byte[] Baby = { 1, 15, 29, 43 },
		inTraining = { 2, 16, 30, 44 },
		Rookies = { 3, 4, 17, 18, 31, 32, 45, 46, 57 },
		Champions = { 5, 6, 7, 8, 9, 10, 11, 19, 20, 21, 22, 23, 24, 25, 33, 34, 35, 36, 37, 38, 39, 47, 48, 49, 50, 51, 52, 53, 58 },
		Ultimates = { 12, 13, 14, 26, 27, 28, 40, 41, 42, 54, 55, 56, 59, 60, 61, 62, 63, 64, 65 };
		List<List<byte>> digimonlists = new List<List<byte>>();
		for (int i = 0; i < 66; i++)
		{ 
			digimonlists.Add(new List<byte>());
		}
		
		uint babyCode = 0x14D18F88;

		switch (treeOpt)
		{
			case 0:
				byte[] inTrainingShuffled = inTraining;
				numberGenerator.Shuffle(inTrainingShuffled);

				for (int i = 0; i < Baby.Length; i++)
				{
					bin.Position = 11 * Baby[i] + startOffset + 7;
					if (bin.Position > jumpOffset)
						bin.Position = bin.Position + 0x130;
					bin.WriteByte(inTrainingShuffled[i]);
					bin.Position = 0x10 * i + babyCode;
					bin.WriteByte(inTrainingShuffled[i]);
					digimonlists[inTrainingShuffled[i]].Add(Baby[i]);
				}

				List<List<byte>> inTrainingEvos = new List<List<byte>>();

				for (int i = 0; i < inTraining.Length; i++)
				{
					bin.Position = 11 * inTraining[i] + startOffset + 7;
					if (bin.Position > jumpOffset)
						bin.Position = bin.Position + 0x130;
					inTrainingEvos.Add(new List<byte>() { (byte)bin.ReadByte(), (byte)bin.ReadByte() });
					inTrainingEvos[i].Remove((byte)currentSukamon);
				}

				for (int i = 0; i < inTrainingShuffled.Length; i++)
				{
					bin.Position = 11 * inTrainingShuffled[i] + startOffset + 7;
					foreach (byte evo in inTrainingEvos[i])
					{
						bin.WriteByte(evo);
						digimonlists[evo].Add(inTrainingShuffled[i]);
					}
				}

				ShuffleEvolution(Rookies, startOffset, ref digimonlists);		
				ShuffleEvolution(Champions, startOffset, ref digimonlists);
				SetPreviousDigimon(digimonlists, startOffset);
				break;
			case 1:
				byte[] inTrainingShuffle = inTraining;
				numberGenerator.Shuffle(inTrainingShuffle);

				for (int i = 0; i < Baby.Length; i++)
				{
					bin.Position = 11 * Baby[i] + startOffset + 7;
					if (bin.Position > jumpOffset)
						bin.Position = bin.Position + 0x130;
					bin.WriteByte(inTrainingShuffle[i]);
					bin.Position = 0x10 * i + babyCode;
					bin.WriteByte(inTrainingShuffle[i]);
					digimonlists[inTrainingShuffle[i]].Add(Baby[i]);
				}				

				List<byte> rookies = new List<byte>(), rookies2 = new List<byte>();
				rookies.AddRange(Rookies);

				for (int i = 0; i < inTraining.Length; i++)
				{
					if (rookies.Count == 0)
					{
						rookies.AddRange(Rookies);
						rookies2.Clear();
					}
					bin.Position = 11 * inTraining[i] + startOffset + 7;
					int evoCount = numberGenerator.Next(2) + 2;
					if (evoCount == 3)
						bin.Position = bin.Position - 1;
					if (bin.Position > jumpOffset)
						bin.Position = bin.Position + 0x130;
					for (int j = 0; j < evoCount; j++)
					{
						if (rookies.Count > 0)
						{
							int rand = numberGenerator.Next(rookies.Count());
							digimonlists[rookies[rand]].Add(inTraining[i]);
							bin.WriteByte(rookies[rand]);
							rookies2.Add(rookies[rand]);
							rookies.RemoveAt(rand);
						}
						else
						{
							bin.WriteByte(rookies2.First());
							rookies2.RemoveAt(0);
						}
					}					
				}

				RandomizeEvolution(Rookies, Champions, startOffset, ref digimonlists, 4, 3);
				RandomizeEvolution(Champions, Ultimates, startOffset, ref digimonlists, 6, 1);

				SetPreviousDigimon(digimonlists, startOffset);
				break;
			case 2:
				List<byte> checkUltimates = new List<byte>();
				checkUltimates.AddRange(Ultimates);
				
				List<byte> checkBabies = new List<byte>();
				checkBabies.AddRange(Baby);
				for (int i = 1; i < 62; i++)
				{
					if (checkUltimates.Count > 0 && checkUltimates.First() == i)
					{
						checkUltimates.RemoveAt(0);
						continue;
					}
					bin.Position = i * 11 + startOffset + 5;
					if (bin.Position > jumpOffset)
						bin.Position = bin.Position + 0x130;
					if (checkBabies.Count > 0 && checkBabies.First() == i)
					{
						bin.WriteByte(0xff);
						if (bin.Position == jumpOffset)
							bin.Position = bin.Position + 0x130;
						bin.WriteByte(0xff);
							if (bin.Position == jumpOffset)
							bin.Position = bin.Position + 0x130;
						byte currentDigimon = (byte)(numberGenerator.Next(65) + 1);
						while (currentDigimon == currentSukamon || currentDigimon == i)
							currentDigimon = (byte)(numberGenerator.Next(65) + 1);
						bin.WriteByte(currentDigimon);
						digimonlists[currentDigimon].Add((byte)i);
						bin.Position = babyCode;
						bin.WriteByte(currentDigimon);
						babyCode = babyCode + 0x10;
						checkBabies.RemoveAt(0);
					}
					else
					{
						int evoCount = numberGenerator.Next(6);
						if (evoCount != 0)
						{
							if (evoCount < 5)
							{
								bin.WriteByte(0xff);
								if (bin.Position == jumpOffset)
									bin.Position = bin.Position + 0x130;
							}
							if (evoCount < 3)
							{
								bin.WriteByte(0xff);
								if (bin.Position == jumpOffset)
									bin.Position = bin.Position + 0x130;
							}
							for (int j = 0; j < evoCount; j++)
							{
								byte currentDigimon = (byte)(numberGenerator.Next(65) + 1);
								while (currentDigimon == currentSukamon || currentDigimon == i)
									currentDigimon = (byte)(numberGenerator.Next(65) + 1);
								bin.WriteByte(currentDigimon);
								digimonlists[currentDigimon].Add((byte)i);
								if (bin.Position == jumpOffset)
									bin.Position = bin.Position + 0x130;
							}
						}
					}
				}

				SetPreviousDigimon(digimonlists, startOffset);
				break;
		}
	}

	void RandomizeEvoTime()
	{
		uint[] timeOffsets = { 0x14CD747C, 0x14CD74AC, 0x14CD74DC, 0x14CD750C };

		switch (timeOpt)
		{
			case 0:
				short[] timeValues = { 6, 24, 72, 144 };
				numberGenerator.Shuffle(timeValues);

				for (int i = 0; i < timeValues.Length; i++)
				{
					bin.Position = timeOffsets[i];
					writter.Write(timeValues[i]);

					if (i == 2)
					{
						bin.Position = 0x14D19AB0;
						writter.Write((short)(timeValues[i] + 24));
					}
				}
				break;
			case 1:
				bin.Position = timeOffsets[0];
				writter.Write((short)(numberGenerator.Next(10) + 1));

				bin.Position = timeOffsets[1];
				writter.Write((short)(numberGenerator.Next(25) + 12));

				bin.Position = timeOffsets[2];
				short rookieValue = (short)(numberGenerator.Next(121) + 24);
				writter.Write(rookieValue);
				bin.Position = 0x14D19AB0;
				writter.Write((short)(rookieValue + 24));

				bin.Position = timeOffsets[3];
				writter.Write((short)(numberGenerator.Next(241) + 48));				
				
				break;
			case 2:
				for (int i = 0; i < timeOffsets.Length; i++)
				{
					short value = (short)(numberGenerator.Next(360) + 1);
					bin.Position = timeOffsets[i];
					writter.Write(value);

					if (i == 2)
					{
						bin.Position = 0x14D19AB0;
						value = (short)(value + numberGenerator.Next(39) + 10);
						writter.Write(value);
					}
				}
				break;
				
		}
	}

	void RandomizeStatGains()
	{
		uint startOffset = 0x14D6CA68;
		byte[] Rookies = { 3, 4, 17, 18, 31, 32, 45, 46, 57 },
		Champions = { 5, 6, 7, 8, 9, 10, 19, 20, 21, 22, 23, 24, 25, 33, 34, 35, 36, 37, 38, 39, 47, 48, 49, 50, 51, 52, 58 },
		Ultimates = { 12, 13, 14, 26, 27, 28, 40, 41, 42, 54, 55, 56, 59, 60, 61, 62, 63, 64, 65 };

		switch (statGainsOpt)
		{
			case 0:
				ShuffleStatGains(Rookies, startOffset);
				ShuffleStatGains(Champions, startOffset);
				ShuffleStatGains(Ultimates, startOffset);
				break;
			case 1:
				RandomStatGains(Rookies, startOffset, 71, 50);
				RandomStatGains(Champions, startOffset, 101, 150);
				RandomStatGains(Ultimates, startOffset, 601, 300);
				break;
			case 2:

				List<byte> normalDigimon = new List<byte>();
				normalDigimon.AddRange(Rookies);
				normalDigimon.AddRange(Champions);
				normalDigimon.AddRange(Ultimates);
				for (int i = 1; i < normalDigimon.Count; i++)
				{
					bin.Position = normalDigimon[i] * 14 + startOffset;
					if (normalDigimon[i] != currentSukamon)
						for (int j = 0; j < 6; j++)
						{
							if (j < 2)
								writter.Write((short)((numberGenerator.Next(1000) + 1) * 10));
							else
								writter.Write((short)(numberGenerator.Next(999) + 1));
						}
				}

				uint[] exceptions = { 0, 1, 2, 11, 15, 16, 29, 30, 43, 44, 53};

				for (int i = 1; i < exceptions.Length; i++)
				{
					bin.Position = exceptions[i] * 14 + startOffset + 10;	
					if (exceptions[i] != currentSukamon)				
					writter.Write((short)(numberGenerator.Next(20) + 1));						
				}
				break;
		}
	}

	void RandomizeRequirements()
	{
		uint startOffset = 0x14D6C254, jumpOffset = 0x14D6C668;
		byte[] Rookies = { 3, 4, 17, 18, 31, 32, 45, 46, 57 },
		Champions = { 5, 6, 7, 8, 9, 10, 11, 19, 20, 21, 22, 23, 24, 25, 33, 34, 35, 36, 37, 38, 39, 47, 48, 49, 50, 51, 52, 53, 58 },
		Ultimates = { 1, 12, 13, 14, 15, 26, 27, 28, 29, 40, 41, 42, 54, 55, 56, 59, 60, 61, 62 };

		switch (requirementsEvoOpt)
		{
			case 0:
				ShuffleRequirements(Rookies, startOffset, jumpOffset);
				ShuffleRequirements(Champions, startOffset, jumpOffset);
				ShuffleRequirements(Ultimates, startOffset, jumpOffset);
				break;
			case 1:
				RandomizeRequirements(Champions, startOffset, jumpOffset, Rookies, false);
				RandomizeRequirements(Ultimates, startOffset, jumpOffset, Champions, true);
				break;
			case 2:
				for (int i = 1; i < 63; i++)
				{
					bin.Position = i * 28 + startOffset;
					if (bin.Position > jumpOffset)
						bin.Position = bin.Position = 0x130;
					int choosenStat = numberGenerator.Next(6) + 1;
					for (int j = 0; j < 14; j++)
					{
						if (j == 0)						
							writter.Write((short)(numberGenerator.Next(65) + 1));						
						else if (j < 7)
							if (numberGenerator.Next(2) == 1 || j == choosenStat)
							{
								writter.Write((short)(numberGenerator.Next(950) + 50));
								if (bin.Position == jumpOffset)
									bin.Position = bin.Position + 0x130;
							}
							else if (j < 8)							
								writter.Write((short)numberGenerator.Next(100));							
							else if (i < 9)							
								writter.Write((short)(numberGenerator.Next(99) + 1));							
							else if (j < 11)							
								if (numberGenerator.Next(2) == 1)
									writter.Write((short)numberGenerator.Next(100));							
							else if (j < 12)							
								if (numberGenerator.Next(2) == 1)
									writter.Write((short)numberGenerator.Next(1000));							
							else if (j < 13)							
								writter.Write((short)numberGenerator.Next(56));							
							else
								writter.Write((short)(numberGenerator.Next(2) * 16 + numberGenerator.Next(2)));
					}		
				}
				break;
				
		}
	}

	void RandomizeSpecialEvo()
	{
		List<uint> digimonOffsets = new List<uint> { 0x140A2E11, 0x140A2E5D, 0x140A2EA9, 0x140A2EFB, 0x14D19FEC, 0x14D1A01C, 0x14D1A054,
								  0x14D1A098, 0x14D1A0AC, 0x14D1A0F8, 0x14D1A114, 0x14D1A148, 0x14D19AC8, 0x14046841 };
								  
		if (filth)
			digimonOffsets.Remove(0x14046841);
		
		switch (specialEvoOpt)
			{
				case 0:
					List<byte> digimons = new List<byte> { 39 };
					foreach (uint offset in digimonOffsets)
					{
						bin.Position = offset;
						digimons.Add((byte)bin.ReadByte());
					}

					byte[] shuffled = digimons.ToArray();
					numberGenerator.Shuffle<byte>(shuffled);

				for (int i = 0; i < digimonOffsets.Count; i++)
				{
					bin.Position = digimonOffsets[i];
					bin.WriteByte(shuffled[i]);
				}

					bin.Position = 0x14CD7584;
					bin.WriteByte(shuffled.Last());
					currentSukamon = shuffled.Last();

					SetSukamonToyEvolution();
					break;
				case 1:
					bin.Position = 0x14CD7584;
					currentSukamon = numberGenerator.Next(63) + 3;
				if (currentSukamon == 15 || currentSukamon == 16 || currentSukamon == 29 || currentSukamon == 30 || currentSukamon == 43 || currentSukamon == 44)
					currentSukamon = currentSukamon + 2;
					bin.WriteByte((byte)currentSukamon);
					bin.Position = currentSukamon * 14 + 0x14D6CA68;
				    writter.Write(0);
					writter.Write(0);
					writter.Write((short)0);
					writter.Write((short)5);
					foreach (uint offset in digimonOffsets)
				{
					bin.Position = offset;
					byte rand = (byte)(numberGenerator.Next(65) + 1);
					while (rand == currentSukamon)
						rand = (byte)(numberGenerator.Next(65) + 1);
					bin.WriteByte(rand);
				}
					
				
					SetSukamonToyEvolution();
				
					break;
			}
	}

	void RandomizeSpecialChance()
	{
		uint[] chanceOffsets = { 0x140A2E07, 0x140A2E53, 0x140A2E9F, 0x140A2EEB, 0x14D19FA8, 0x14D1A060, 0x14D1A0EC };

		switch (specialChanceOpt)
		{
			case 0:
				List<byte> digimons = new List<byte>();
				foreach (uint offset in chanceOffsets)
				{
					bin.Position = offset;
					digimons.Add((byte)bin.ReadByte());
				}

				byte[] shuffled = digimons.ToArray();
				numberGenerator.Shuffle<byte>(shuffled);

				for (int i = 0; i < chanceOffsets.Length; i++)
				{
					bin.Position = chanceOffsets[i];
					bin.WriteByte(shuffled[i]);
				}
				break;
			case 1:						
				foreach (uint offset in chanceOffsets)
				{
					bin.Position = offset;
					bin.WriteByte((byte)numberGenerator.Next(101));
				}
				break;
		}
	}

	void RandomizeSpecialReq()
	{
		List<uint> digimonOffsets = new List<uint>{ 0x140A2C65, 0x140A2C69, 0x140A2C6D, 0x140A2C71, 0x140A2DB1, 0x140A2DB5, 0x140A2DB9, 0x140A2DBD, 0x140A2DD1,
							      0x140A2DCD, 0x140A2DE1, 0x140A2DF1, 0x14D19FB4, 0x14D19FC0, 0x14D19FF8, 0x14D1A028, 0x14D1A0BC, 0x14D1A0C8, 0x140467EF };

		if (filth)
			digimonOffsets.Remove(0x140467E);

		switch (speEvoReqOpt)
			{
				case 0:
					List<byte> digimons = new List<byte>();
					foreach (uint offset in digimonOffsets)
					{
						bin.Position = offset;
						digimons.Add((byte)bin.ReadByte());
					}

					byte[] shuffled = digimons.ToArray();
					numberGenerator.Shuffle<byte>(shuffled);

					for (int i = 0; i < digimonOffsets.Count; i++)
					{
						bin.Position = digimonOffsets[i];
						bin.WriteByte(shuffled[i]);
					}
					break;
				case 1:
					/*
						uint specialOffset = 0x140A2EEE;
						bin.Position = specialOffset;
						byte randomNumber = (byte)numberGenerator.Next(22);
						short randomValue;
						if (randomNumber < 4)
							randomValue = (short)numberGenerator.Next(1000);
						else if (randomNumber < 8)
							randomValue = (short)numberGenerator.Next(10000);
						else if (randomNumber < 12)
							randomValue = (short)numberGenerator.Next(100);
						else if (randomNumber < 13)
							randomValue = (short)numberGenerator.Next(16);
						else if (randomNumber < 20)
							randomValue = (short)numberGenerator.Next(999);
						else if (randomNumber < 21)
							randomValue = (short)numberGenerator.Next(100);
						else
							randomValue = (short)numberGenerator.Next(10);
						bin.WriteByte(randomNumber);
						bin.WriteByte((byte)numberGenerator.Next(7));
						writter.Write(randomValue);*/

					foreach (uint offset in digimonOffsets)
					{
						bin.Position = offset;
						bin.WriteByte((byte)(numberGenerator.Next(65) + 1));
					}

					//Remove Champion requirement from special evo
					bin.Position = 0x14D1A0D4;
					bin.WriteByte(10);
					bin.Position = 0x14D1A0DB;
					bin.WriteByte(0x16);
					break;
			}
	}

	void RandomizeFactorial()
	{
		uint[] upgradeOffsets = {0x14054453, 0x140546DB, 0x140545E1, 0x14054457, 0x140546EF, 0x140545F5, 0x1405445B, 0x14054703, 0x14054609, 0x1405445F, 0x14054717,
								 0x1405461D, 0x1405446F, 0x1405472B, 0x14054631, 0x14054473, 0x1405473F, 0x14054645, 0x14054477, 0x14054753, 0x14054659 },
		specialUpgrade = { 0x1405447B, 0x14054815, 0x14054819, 0x1405481D, 0x14054821, 0x14054825 },
		evolutions = {0x140546E5, 0x140546F9, 0x1405470D, 0x14054721, 0x14054735, 0x14054749, 0x1405475D, 0x14054765, 0x140545EB,
				      0x140545FF, 0x14054613, 0x14054627, 0x1405463B, 0x1405464F, 0x14054663, 0x1405466B, 0x1405495D};

		List<byte> digimonList = new List<byte>();

		switch (factorialOpt)
		{
			case 0:
				RandomizeFactorialNormalUpgrades(ref digimonList, upgradeOffsets);
				RandomizeFactoriaSpecialUpgrades(ref digimonList, specialUpgrade);
				break;
			case 1:
				digimonList = new List<byte> { 13, 5, 6, 10, 18, 42, 48};
				foreach (uint offset in specialUpgrade)
				{
					bin.Position = offset;
					digimonList.Add((byte)bin.ReadByte());
				}
				RandomizeFactorialEvolutions(digimonList, evolutions);
				break;
			case 2:
				RandomizeFactorialNormalUpgrades(ref digimonList, upgradeOffsets);
				RandomizeFactoriaSpecialUpgrades(ref digimonList, specialUpgrade);
				RandomizeFactorialEvolutions(digimonList, evolutions);
				break;
		}
		
	}

	void RandomizeSukamon()
	{
		bin.Position = 0x14D1596C;
		byte[] buffer = { 0xb5, 0x8d, 0x2, 0x0c, 0x41, 0, 0x4, 0x24, 0x1, 0, 0x42, 0x24, 0x5e, 0, 0, 0x10, 0x24, 0x93, 0x82, 0xa7 };
		bin.Write(buffer);

		//This would be the code itself
		/*  iVar1 = ReturnRandom(0x41); 
    		EvoValue = (short)iVar1 + 1;
    		goto LAB_800df7dc;*/
	}

	void randomizeRestaurant()
	{
		uint[] offsets = { 0x140AD588, 0x140AD790, 0x140AD998, 0x140ADDE6, 0x140ADFCC, 0x140AE1BA, 0x140AE602, 0x140AE7D0,
						   0x140AE9E2, 0x140AF0B6, 0x140AF262, 0x140AF412, 0x140AF7F8, 0x140AFA72, 0x140AFD02,
						   0x140B01B8, 0x140B02D6, 0x140B03F6, 0x140B0686, 0x140B07D2, 0x140B0A28, 0x140B0CBC, 0x140B0E10, 0x140B0F66 };

		switch (restaurantOpt)
		{
			case 0:
				for (int i = 0; i < offsets.Length; i++)
				{
					bin.Position = offsets[i] + 2;
					for (int j = 0; j < 11; j++)
					{
						if (numberGenerator.Next(2) == 1)
						{
							if (j == 4 || j == 5)
								writter.Write((short)((numberGenerator.Next(30) + 1) * 10));
							else if (j == 10)
								writter.Write((short)numberGenerator.Next(11));
							else
								writter.Write((short)(numberGenerator.Next(30) + 1));
						}
						else
							writter.Write((short)0);
						reader.ReadInt16();
					}
				}
				break;
			case 1:
				for (int i = 0; i < offsets.Length; i++)
				{
					bin.Position = offsets[i];
					for (int j = 0; j < 11; j++)
					{
						if (numberGenerator.Next(2) == 1)
						{
							if (numberGenerator.Next(2) == 1)
								bin.WriteByte(0x35);
							else
								bin.WriteByte(0x36);
							bin.ReadByte();
							
							if (j == 4 || j == 5)
								writter.Write((short)((numberGenerator.Next(50) + 1) * 10));
							else if (j == 10)
								writter.Write((short)numberGenerator.Next(100));
							else
								writter.Write((short)(numberGenerator.Next(50) + 1));
						}
						else
						{
							reader.ReadInt16();
							writter.Write((short)0);
						}
						;
					}
				}
				break;
		}
	}

	void RandomizeBirdramon()
	{
		uint offset = 0x14D725C4;

		for (int i = 0; i < 8; i++)
		{
			bin.Position = i * 6 + offset;

			byte selectedMap = 0;

			short moneyCost = 1;

			switch (birdramonOpt)
			{
				case 0:
					selectedMap = (byte)numberGenerator.Next(162);
					if (selectedMap == 21)
						selectedMap = 162;
					else if (selectedMap == 50)
						selectedMap = 163;
					else if (selectedMap == 61)
						selectedMap = 164;
					else if (selectedMap == 65)
						selectedMap = 165;

					moneyCost = (short)(numberGenerator.Next(5000) + 1);
					break;
					
				case 1:
					selectedMap = (byte)numberGenerator.Next(248);
					if (selectedMap > 239)
					{
						if (selectedMap < 246)
							selectedMap = (byte)(selectedMap + 4);
						else
							selectedMap = (byte)(selectedMap + 7);

					}
					moneyCost = (short)(numberGenerator.Next(9999) + 1);
					break;
			}			
			bin.WriteByte(selectedMap);
			bin.WriteByte(0);
			reader.ReadInt16();
			writter.Write(moneyCost);
		}
	}

	void randomizeSeadramon()
	{
		bin.Position = 0x14BA4DF8;
		bin.WriteByte((byte)(numberGenerator.Next(100) + 1));
	}

	void RandomizeFishBait()
	{
		uint baitOffset = 0x14BA9F54, favouriteOffset = 0x14BAA6D4, itemFished = 0x14BA9D5C;

		bin.Position = baitOffset;

		switch (fishOpt)
		{
			case 0:
				List<byte> baitValues = new List<byte>();

				for (int i = 0; i < 198; i++)								
					baitValues.Add((byte)bin.ReadByte());
				

				byte[] shuffled = baitValues.ToArray();
				numberGenerator.Shuffle<byte>(shuffled);
				bin.Position = baitOffset;

				foreach (byte value in baitValues)				
					bin.WriteByte(value);
				
				bin.Position = itemFished;
				baitValues.Clear();
				for (int i = 0; i < 6; i++)
				{
					baitValues.Add((byte)bin.ReadByte());
					bin.ReadByte();
				}
				shuffled = baitValues.ToArray();
				numberGenerator.Shuffle<byte>(shuffled);
				bin.Position = itemFished;

				foreach (byte value in baitValues)
				{
					bin.WriteByte(value);
					bin.ReadByte();
				}

				break;
			case 1:
				for (int i = 0; i < 198; i++)								
					bin.WriteByte((byte)numberGenerator.Next(101));

				for (int i = 0; i < 6; i++)
				{
					bin.Position = i * 24 + favouriteOffset;

					writter.Write((short)(numberGenerator.Next(34) + 38));
				}
				bin.Position = itemFished;
				for (int i = 0; i < 6; i++)
				{
					bin.WriteByte((byte)numberGenerator.Next(128));
					if (i < 5)
						bin.WriteByte((byte)(i * 16 + numberGenerator.Next(20)));
					else
						bin.WriteByte(99);
				}
				break;
		}
		
	}

	void RandomizeTournamentSchedule()
	{
		uint schedule = 0x14D72510;

		bin.Position = schedule;

		switch (tournamentScheduleOpt)
		{
			case 0:
				List<List<byte>> tournaments = new List<List<byte>>();
				for (int i = 0; i < 30; i++)
				{
					tournaments.Add(new List<byte>());
					for (int j = 0; j < 6; j++)					
						tournaments[i].Add((byte)bin.ReadByte());					
				}

				bin.Position = schedule;
				for (int i = 0; i < 30; i++)
				{
					int randomValue = numberGenerator.Next(tournaments.Count);
					for (int j = 0; j < 6; j++)
						bin.WriteByte(tournaments[randomValue][j]);
					tournaments.RemoveAt(randomValue);
				}
				break;
			case 1:
				List<byte> tournamentValues = new List<byte>(), values2 = new List<byte>();
				for (int i = 0; i < 22; i++)
					tournamentValues.Add((byte)i);

				for (int i = 0; i < 30; i++)
				{
					int numberCups = numberGenerator.Next(6) + 1;

					if (tournamentValues.Count < 1)
					{
						tournamentValues.Clear();
						for (int w = 0; w < 22; w++)
							tournamentValues.Add((byte)w);
						values2.Clear();
					}


					for (int j = 0; j < 6; j++)
					{
						if (j < numberCups)
						{
							if (tournamentValues.Count > 0)
							{
								int randomCup = numberGenerator.Next(tournamentValues.Count);
								bin.WriteByte(tournamentValues[randomCup]);
								values2.Add(tournamentValues[randomCup]);
								tournamentValues.RemoveAt(randomCup);
							}
							else
							{
								bin.WriteByte(values2.First());
								values2.RemoveAt(0);
							}
						}
						else
							bin.WriteByte(0xFF);												
					}								
				}
				break;
		}
	}

	void RandomizeRareSpawns()
	{
		byte RareSpawnValue, RareSpawnValue2, RareSpawnValue3, RareSpawnValue4;

		switch (rareSpawnsOpt)
		{
			case 0:
				RareSpawnValue = RareSpawnValue2 = RareSpawnValue3 = RareSpawnValue4 = (byte)numberGenerator.Next(100);
				break;
			case 1:
				RareSpawnValue = (byte)numberGenerator.Next(100);
				RareSpawnValue2 = (byte)numberGenerator.Next(100);
				RareSpawnValue3 = (byte)numberGenerator.Next(100);
				RareSpawnValue4 = (byte)numberGenerator.Next(100);
				break;
			default:
				RareSpawnValue = RareSpawnValue2 = RareSpawnValue3 = RareSpawnValue4 = (byte)numberGenerator.Next(100);
				break;
		}


		//Piximon
		bin.Position = 0x13FD64DB;
		bin.WriteByte(RareSpawnValue);
		bin.Position = 0x140B765B;
		bin.WriteByte(RareSpawnValue);

		//Mamemon
		bin.Position = 0x13FD678F;
		bin.WriteByte(RareSpawnValue2);
		bin.Position = 0x140B790F;
		bin.WriteByte(RareSpawnValue2);

		//MetalMamemon
		bin.Position = 0x13FD831F;
		bin.WriteByte(RareSpawnValue3);
		bin.Position = 0x140B949F;
		bin.WriteByte(RareSpawnValue3);

		//Otamamon
		bin.Position = 0x13FD7F47;
		bin.WriteByte(RareSpawnValue4);
		bin.Position = 0x140B90C7;
		bin.WriteByte(RareSpawnValue4);
		
	}

	void RandomizeBoost(bool ignoreShuffle = false)
	{
		uint ptrOffset = 0x14D53558;

		if (!ignoreShuffle)
		{
			List<int> pointers = new List<int>();

			for (int i = 0; i < 7; i++)
			{
				bin.Position = i * 4 + ptrOffset;
				pointers.Add(reader.ReadInt32());
			}

			int[] shuffledPtr = pointers.ToArray();
			numberGenerator.Shuffle<int>(shuffledPtr);

			for (int i = 0; i < 7; i++)
			{
				bin.Position = i * 4 + ptrOffset;
				writter.Write(shuffledPtr[i]);
			}
		}

		uint[] offsets;
		switch (boostOpt)
		{

			case 1:
				bin.Position = 0x14D292E4;
				if (bin.ReadByte() == 0x40)
				{
					offsets = new uint[] { 0x14D294F0, 0x14D29510, 0x14D29554, 0x14D29574, 0x14D295B8, 0x14D295D8,
										   0x14D2969C, 0x14D296BC, 0x14D29700, 0x14D29720, 0x14D29764, 0x14D29784 };
					RandomizeBuffs(offsets, 81, 20);
				}
				else
				{
					offsets = new uint[] { 0x14D294A8, 0x14D294C8, 0x14D2950C, 0x14D2952C, 0x14D29570, 0x14D29590,
										   0x14D295F4, 0x14D29614, 0x14D29658, 0x14D29678, 0x14D296BC, 0x14D296DC };
					RandomizeBuffs(offsets, 81, 20);
				}
				break;
			case 2:
				bin.Position = 0x14D29290;
				bin.WriteByte(0x80);

				bin.Position = 0x14D292E4;
				if (bin.ReadByte() == 0x40)
				{
					bin.Position = 0x14D292E4;
					bin.WriteByte(0x80);
					bin.Position = 0x14D29338;
					bin.WriteByte(0x80);

					offsets = new uint[] { 0x14D294F0, 0x14D29510, 0x14D29554, 0x14D29574, 0x14D295B8, 0x14D295D8,
										   0x14D2969C, 0x14D296BC, 0x14D29700, 0x14D29720, 0x14D29764, 0x14D29784 };

					RandomizeBuffs(offsets, 251, -100);
				}
				else
				{
					bin.Position = 0x14D292CC;
					bin.WriteByte(0x80);
					bin.Position = 0x14D29308;
					bin.WriteByte(0x80);
					
					offsets = new uint[] { 0x14D294A8, 0x14D294C8, 0x14D2950C, 0x14D2952C, 0x14D29570, 0x14D29590,
										   0x14D295F4, 0x14D29614, 0x14D29658, 0x14D29678, 0x14D296BC, 0x14D296DC };

					RandomizeBuffs(offsets, 251, -100);			
				}

				break;
		}
	}

	void RandomizeHealing()
	{
		uint healingValues = 0x14D77054;
		bin.Position = healingValues;

		switch (healingOpt)
		{
			case 0:
				short[] values = { 500, 1500, 5000, 9999, 1500, 1500, 9999 };
				numberGenerator.Shuffle(values);

				for (int i = 0; i < 4; i++)
					writter.Write(values[i]);
				//double flop
				bin.Position = 0x14CF6DA4;
				writter.Write(values[4]);
				bin.Position = 0x14CF6DBC;
				writter.Write(values[5]);
				bin.Position = 0x14CF6DEC;
				writter.Write(values[4]);
				bin.Position = 0x14CF6E0C;
				writter.Write(values[5]);

				//restores
				bin.Position = 0x14CF6D00;
				writter.Write(values[6]);

				break;
			case 1:
				for (int i = 0; i < 4; i++)
					writter.Write((short)(numberGenerator.Next(9500) + 500));

				bin.Position = 0x14CF6DA4;
				short randomValue = (short)(numberGenerator.Next(9500) + 500);
				writter.Write(randomValue);
				bin.Position = 0x14CF6DEC;
				writter.Write(randomValue);

				randomValue = (short)(numberGenerator.Next(9500) + 500);
				bin.Position = 0x14CF6DBC;
				writter.Write(randomValue);
				bin.Position = 0x14CF6E0C;
				writter.Write(randomValue);

				bin.Position = 0x14CF6D00;
				writter.Write((short)(numberGenerator.Next(9500) + 500));
				
				break;
			case 2:
				for (int i = 0; i < 4; i++)
					writter.Write((short)(numberGenerator.Next(19999) - 9999));

				bin.Position = 0x14CF6DA4;
				short rand = (short)(numberGenerator.Next(19999) - 9999);
				writter.Write(rand);
				bin.Position = 0x14CF6DEC;
				writter.Write(rand);

				rand = (short)(numberGenerator.Next(19999) - 9999);
				bin.Position = 0x14CF6DBC;
				writter.Write(rand);
				bin.Position = 0x14CF6E0C;
				writter.Write(rand);

				bin.Position = 0x14CF6D00;
				writter.Write((short)(numberGenerator.Next(9999) + 1));
				break;
		}
	}

	void RandomizeFood()
	{
		uint offsetValues = 0x14CF5C44;
		uint[] offsetEnHaDiTir= { 0x14CF5DD0, 0x14CF5DEC, 0x14CF5E14, 0x14CF5E48, 0x14CF5EA0, 0x14CF5EF0, 0x14CF5F10, 0x14CF5F3C, 0x14CF5F70, 0x14CF6000, 0x14CF6050,
							 	  0x14CF6088, 0x14CF60B8, 0x14CF60D4, 0x14CF60F8, 0x14CF6120, 0x14CF6140, 0x14CF6168, 0x14CF6384, 0x14CF63AC, 0x14CF63CC, 0x14CF63E8,
								  0x14CF6404, 0x14CF6430, 0x14CF64B0, 0x14CF64E8, 0x14CF6528, 0x14CF6554, 0x14CF65F0, 0x14CF6554, 0x14CF6584, 0x14CF5E08, 0x14CF5EE8,
								  0x14CF5F2C, 0x14CF6048, 0x14CF6070},
		offsetWeight = { 0x14CF5DD4, 0x14CF5E10, 0x14CF5E78, 0x14CF5ED0, 0x14CF5EF4, 0x14CF5F14, 0x14CF5F48, 0x14CF5FD8, 0x14CF6030, 0x14CF6054, 0x14CF6094, 0x14CF60BC,
						 0x14CF60D8, 0x14CF60FC, 0x14CF6124, 0x14CF6144, 0x14CF616C, 0x14CF61AC, 0x14CF630C, 0x14CF633C, 0x14CF636C, 0x14CF6388, 0x14CF63B0, 0x14CF6498,
						 0x14CF63D0, 0x14CF63EC, 0x14CF6408, 0x14CF6498, 0x14CF64B4, 0x14CF64FC, 0x14CF652C, 0x14CF6558, 0x14CF65B4},
		offsetHPMP = {0x14CF5FCC, 0x14CF60F0, 0x14CF6118, 0x14CF615C, 0x14CF648C, 0x14CF64CC, 0x14CF65D8},
		offsetParameters = { 0x14CF618C, 0x14CF61BC, 0x14CF631C, 0x14CF634C, 0x14CF5F60, 0x14CF6420},
		offsetBuffValue = { 0x14CF5E54, 0x14CF5EAC, 0x14CF600C},
		offsetBuffTime = { 0x14CF5E70, 0x14CF5EC8, 0x14CF6028};
		

		List<byte> values = new List<byte>();

		for (int i = 70; i > 37; i--)
		{
			values.Add((byte)i);
		}

		byte[] shuffledValues = values.ToArray();
		numberGenerator.Shuffle(shuffledValues);

		for (int i = 0; i < shuffledValues.Length; i++)
		{
			bin.Position = i * 12 + offsetValues;
			bin.WriteByte(shuffledValues[i]);
		}

		switch (foodOpt)
		{
			case 1:
				RandomizeShortswithRange(offsetEnHaDiTir, 100, 1);
				RandomizeShortswithRange(offsetWeight, 16, -5);
				RandomizeShortswithRange(offsetHPMP, 51, 1, 10);
				RandomizeShortswithRange(offsetParameters, 51, 1);
				RandomizeShortswithRange(offsetBuffValue, 7, 12);
				RandomizeShortswithRange(offsetBuffTime, 10, 1);
				bin.Position = 0x14CF6590;
				writter.Write((short)(numberGenerator.Next(24) + 1));
				bin.Position = 0x14CF64D8;
				writter.Write((short)(numberGenerator.Next(24) + 1));
				break;
			case 2:
				RandomizeShortswithRange(offsetEnHaDiTir, 201, -100);
				RandomizeShortswithRange(offsetWeight, 31, -10);
				RandomizeShortswithRange(offsetHPMP, 151, -50, 10);
				RandomizeShortswithRange(offsetParameters, 151, -50);
				RandomizeShortswithRange(offsetBuffValue, 26, 1);
				RandomizeShortswithRange(offsetBuffTime, 21, 1);
				bin.Position = 0x14CF6590;
				writter.Write((short)(numberGenerator.Next(73) - 24));
				bin.Position = 0x14CF64D8;
				writter.Write((short)(numberGenerator.Next(73) - 24));
				break;
		}
	}

	void RandomizeChips(bool ignoreShuffle = false)
	{
		uint[] offsetStats = { 0x14CF68B4, 0x14CF68C4, 0x14CF691C, 0x14CF6874, 0x14CF6884, 0x14CF6894, 0x14CF68A4, 0x14CF68D4, 0x14CF68F8 };
		uint[] offsetsLifetime = { 0x14CF68E0, 0x14CF6904, 0x14CF6928 };
		uint offsetsPtr = 0x14D52C68;

		if (!ignoreShuffle)
		{
			List<int> pointers = new List<int>();

			bin.Position = offsetsPtr;
			for (int i = 0; i < 9; i++)
				pointers.Add(reader.ReadInt32());

			int[] ptrShuffle = pointers.ToArray();
			numberGenerator.Shuffle(ptrShuffle);

			bin.Position = offsetsPtr;
			foreach (int ptr in ptrShuffle)
				writter.Write(ptr);
		}

		switch (chipsOpt)
			{
				case 0:
					for (int i = 0; i < offsetStats.Length; i++)
					{
						bin.Position = offsetStats[i];
						if (i < 3)
							writter.Write((short)((numberGenerator.Next(141) + 10) * 10));
						else
							writter.Write((short)(numberGenerator.Next(141) + 10));
					}

					foreach (uint offset in offsetsLifetime)
					{
						bin.Position = offset;
						writter.Write((short)(-16 - numberGenerator.Next(15)));
					}
					break;
				case 1:
					for (int i = 0; i < offsetStats.Length; i++)
					{
						bin.Position = offsetStats[i];
						if (i < 3)
							writter.Write((short)((numberGenerator.Next(351) - 100) * 10));
						else
							writter.Write((short)(numberGenerator.Next(351) - 100));
					}

					foreach (uint offset in offsetsLifetime)
					{
						bin.Position = offset;
						writter.Write((short)(-1 - numberGenerator.Next(72)));
					}
					break;
			}
	}

	void RandomizeMonochromon()
	{
		uint bitsOffset = 0x140007B1, halfWayOffset = 0x140006D3;

		byte randomBits = 0;

		switch (devilOpt)
		{
			case 0:
				randomBits = (byte)(numberGenerator.Next(12) + 4);
				break;
			case 1:
				randomBits = (byte)numberGenerator.Next(28);
				break;
		}

		bin.Position = bitsOffset;
		bin.WriteByte(randomBits);
		bin.Position = halfWayOffset;
		bin.WriteByte((byte)(randomBits / 2));
	}

	void CreateChaosItems()
	{
		boostOpt = healingOpt =  foodOpt = evoItemsOpt = 2;
		chipsOpt = 1;

		/*uint Ptr1 = 0x14D52C48, Ptr2 = 0x14D53558, changeOffset = 0x14D676DE;

		Dictionary<int,int> pointers = new Dictionary<int,int>();
		List<int> keys = new List<int>();
		int currentValue = 8;

		for (int i = 0; i < 18; i++)
		{
			if (i == 3 || i == 4)
				continue;
			bin.Position = i * 4 + Ptr1;
			pointers.Add(currentValue + i, reader.ReadInt32());
			keys.Add(currentValue + i);
			if (i == 6)
				currentValue = 15;
			
		}
		bin.Position = Ptr2;
		currentValue = 15;
		for (int i = 0; i < 7; i++)
		{
			pointers.Add(currentValue + i, reader.ReadInt32());
			keys.Add(currentValue + i);
		}

		currentValue = 0;

		int[] shuffledKeys = keys.ToArray();
		numberGenerator.Shuffle(shuffledKeys);

		for (int i = 0; i < 18; i++)
		{
			if (i == 3 || i == 4)
				continue;
			bin.Position = i * 4 + Ptr1;
			int item = shuffledKeys[currentValue];
			int newValue = 0;
			pointers.Remove(item, out newValue);
			writter.Write(newValue);

			if (i < 7)
			{
				bin.Position = 32 * item + changeOffset;
				bin.WriteByte(1);
				bin.Position = 32 * item + changeOffset + 2;
				if (i == 0 || i == 2)
					bin.WriteByte(1);
				else if (i == 1)
					bin.WriteByte(2);
				else
					bin.WriteByte(0);

			}
			else
			{
				bin.Position = 32 * item + changeOffset;
				if (i == 7 || i == 17)
					bin.WriteByte(5);
				else
					bin.WriteByte(4);
				bin.Position = 32 * item + changeOffset + 2;
				bin.WriteByte(0);

			}
			currentValue++;
		}

		bin.Position = Ptr2;
		for (int i = 0; i < 7; i++)
		{
			bin.Position = i * 4 + Ptr2 ;
			int item = shuffledKeys[currentValue];
			int newValue = 0;
			pointers.Remove(item, out newValue);
			writter.Write(newValue);

			bin.Position = 32 * item + changeOffset;
			bin.WriteByte(3);
			bin.Position = 32 * item + changeOffset + 2;		
			bin.WriteByte(1);				
			
			currentValue++;
		}*/

		uint offsetsPtr = 0x14D52C64;


			List<int> pointers = new List<int>();

			bin.Position = offsetsPtr;
			for (int i = 0; i < 11; i++)
				pointers.Add(reader.ReadInt32());

			int[] ptrShuffle = pointers.ToArray();
			numberGenerator.Shuffle(ptrShuffle);

			bin.Position = offsetsPtr;
			foreach (int ptr in ptrShuffle)
				writter.Write(ptr);
		


		RandomizeBoost();
		RandomizeFood();
		RandomizeChips(true);
		RandomizeHealing();
		randomizeEvoItems();

	}

	void randomizeEvoItems()
	{
		uint evoItemsOffset = 0x14D68BA4, descriptionsPtr = 0x14D68A44;
		

		bin.Position = 0x14D66419;
		bin.WriteByte(0);
		bin.WriteByte(0);

		for (int i = 0; i < 43; i++)
		{
			bin.Position = descriptionsPtr;
			bin.WriteByte(0x88);
			bin.WriteByte(0x59);

			descriptionsPtr = descriptionsPtr + 4;
		}		

		
		switch (evoItemsOpt)
		{
			case 0:
				bin.Position = evoItemsOffset;
				List<byte> evoValues = new List<byte>();
				for (int i = 0; i < 44; i++)			
					evoValues.Add((byte)bin.ReadByte());

				byte[] shuffledValues = evoValues.ToArray();
				numberGenerator.Shuffle<byte>(shuffledValues);
				
				bin.Position = evoItemsOffset;	
				int SukamonValue = 0;			

				for (int i = 0; i < 44; i++)
				{
					bin.WriteByte(shuffledValues[i]);
					if (shuffledValues[i] == currentSukamon)
						SukamonValue = i;
				}

				bin.Position = 0x14D19AFC;
				bin.WriteByte((byte)(SukamonValue + 71));
				evoValues.Clear();
				break;
			case 1:
				SetExtraItemsDescriptions();
				bin.Position = evoItemsOffset;
				RandomizeNormalEvoItems();
						
				byte[] Champions = { 5, 6, 7, 8, 9, 10, 11, 19, 20, 21, 22, 23, 24, 25, 33, 34, 35, 36, 37, 38, 39, 47, 48, 49, 50, 51, 52, 53, 58 },
		              Ultimate = { 12, 13, 14, 26, 27, 28, 40, 41, 42, 54, 55, 56, 59, 60, 61, 62, 63, 64, 65 };

				bin.Position = 0x14CF5A44;
				bin.WriteByte(Champions[numberGenerator.Next(Champions.Length)]);

				bin.Position = 0x14CF5A50;
				bin.WriteByte(Ultimate[numberGenerator.Next(Ultimate.Length)]);

				bin.Position = 0x14CF5A5C;
				bin.WriteByte(Ultimate[numberGenerator.Next(Ultimate.Length)]);

				bin.Position = 0x14CF5A74;
				bin.WriteByte((byte)(numberGenerator.Next(65) + 1));
			
				break;
			case 2:
			// all digimon except Machinedramon/Myotismon won't refuse evolution items
				bin.Position = 0x14CD5198;
				bin.WriteByte(0x15);
				bin.WriteByte(0x80);
				bin.WriteByte(0x1);
				bin.WriteByte(0x3c);
				bin.WriteByte(0xa8);
				bin.WriteByte(0x57);
				bin.WriteByte(0x23);
				bin.WriteByte(0x8c);
				bin.WriteByte(0x3e);
				bin.WriteByte(0);
				bin.WriteByte(0x2);
				bin.WriteByte(0x24);
				bin.WriteByte(0x8a);
				bin.WriteByte(0);
				bin.WriteByte(0x43);
				bin.WriteByte(0x10);
				writter.Write((int)0);
				bin.WriteByte(0x48);
				bin.WriteByte(0);
				bin.WriteByte(0);
				bin.WriteByte(0x10);
				writter.Write((int)0);	

				//Disable stat gains and extended lifespan
				bin.Position = 0x14CF5AFC;
				bin.WriteByte(1);

				//Enable any digimon to evolve with the items
				bin.Position = 0x14CF5AF0;
				writter.Write((int)0);
				SetExtraItemsDescriptions();
				bin.Position = evoItemsOffset;
				RandomizeNormalEvoItems();


				SetRandomNoSukamon(0x14CF5A44);
				SetRandomNoSukamon(0x14CF5A50);
				SetRandomNoSukamon(0x14CF5A5C);
				SetRandomNoSukamon(0x14CF5A74);
				break;
		}
	}

	void RandomizeDigimonDataByteOnlyMax(uint startPosition, int maxValueRando)
	{
		uint currentOffset = startPosition;
		uint[] jumpOffsets = { 0x14D6EB28, 0x14D6F458, 0x14D6FD88, 0x14D706B8, 0x14D70FE8, 0x79999999 };
		int jumpValue = 0;

		bin.Position = startPosition;
		for (int i = 0; i < 180; i++)
		{
			bin.Position = currentOffset;
			currentOffset = currentOffset + 0x34;
			if (currentOffset > jumpOffsets[jumpValue])
			{
				currentOffset = currentOffset + 0x130;
				jumpValue++;
			}
			bin.WriteByte((byte)numberGenerator.Next(maxValueRando));
		}
	}

	void RandomizeShortswithRange(uint[] offsets, int maxValue, int minValue, int mult = 1)
	{
		for (int i = 0; i < offsets.Length; i++)
		{
			bin.Position = offsets[i];
			writter.Write((short)((numberGenerator.Next(maxValue) + minValue) * mult));
		}
	}

	void CreateRandomShopItems(ref List<int> shopCurrentItems, uint numItems)
	{
		while (shopCurrentItems.Count < numItems)
		{
			byte rando;
			switch (shopsOpt)
			{
				case 0:
					rando = (byte)numberGenerator.Next(73);
					if (rando == 71)
						rando = 121;
					else if (rando == 72)
						rando = 122;
					break;
				case 1:
					rando = (byte)numberGenerator.Next(128);
					break;
				default:
					rando = (byte)numberGenerator.Next(10);
					break;
			}

			if (shopCurrentItems.Count == 0)
				shopCurrentItems.Add(rando);
			else
			{
				bool sameValue = false;
				foreach (int shop in shopCurrentItems)
				{
					if (rando == shop)
					{
						sameValue = true;
						break;
					}
				}
				if (sameValue)
					continue;
				shopCurrentItems.Add(rando);
			}
		}
	}

	void ShuffleTechPower(int techCount, uint startingOffset, uint jumpOffset)
	{
		uint currentOffset = startingOffset;
		bool jumped = false;
		int shuffleIterator = 0;
		List<short> techPower = new List<short>();
		for (int i = 0; i < techCount; i++)
		{
			short value = reader.ReadInt16();
			currentOffset = currentOffset + 0x10;
			if (!jumped && currentOffset >= jumpOffset)
			{
				currentOffset = currentOffset + 0x130;
				jumped = true;
			}
			bin.Position = currentOffset;
			if (i == 21 || i == 30 || i == 34 || i == 41 || i == 42)
				continue;
			techPower.Add(value);
		}

		short[] shuffledValues = techPower.ToArray();
		numberGenerator.Shuffle<short>(shuffledValues);

		currentOffset = startingOffset;
		jumped = false;
		bin.Position = startingOffset;

		for (int i = 0; i < techCount; i++)
		{
			bin.Position = currentOffset;
			currentOffset = currentOffset + 0x10;
			if (!jumped && currentOffset >= jumpOffset)
			{
				currentOffset = currentOffset + 0x130;
				jumped = true;
			}
			if (i == 21 || i == 30 || i == 34 || i == 41 || i == 42)
				continue;
			if (shuffleIterator < shuffledValues.Length)
			{
				writter.Write(shuffledValues[shuffleIterator]);
				shuffleIterator++;
			}
			else
				writter.Write((short)numberGenerator.Next(750));
		}
		techPower.Clear();
	}

	void ShuffleTechDataByte(int techCount, uint startingOffset, uint jumpOffset)
	{
		uint currentOffset = startingOffset;
		bool jumped = false;
		int shuffleIterator = 0;
		List<byte> techData = new List<byte>();
		for (int i = 0; i < techCount; i++)
		{
			byte value = (byte)bin.ReadByte();
			currentOffset = currentOffset + 0x10;
			if (!jumped && currentOffset >= jumpOffset)
			{
				currentOffset = currentOffset + 0x130;
				jumped = true;
			}
			bin.Position = currentOffset;
			techData.Add(value);
		}

		byte[] shuffledValues = techData.ToArray();
		numberGenerator.Shuffle<byte>(shuffledValues);

		currentOffset = startingOffset;
		jumped = false;
		bin.Position = startingOffset;

		for (int i = 0; i < techCount; i++)
		{
			bin.Position = currentOffset;
			currentOffset = currentOffset + 0x10;
			if (!jumped && currentOffset >= jumpOffset)
			{
				currentOffset = currentOffset + 0x130;
				jumped = true;
			}
			if (shuffleIterator < shuffledValues.Length)
			{
				bin.WriteByte(shuffledValues[shuffleIterator]);
				shuffleIterator++;
			}
			else
				bin.WriteByte((byte)numberGenerator.Next(255));
		}
		techData.Clear();
	}

	void ShuffleBoostPower(int[] Values)
	{
		uint startOffset = 0x14D62802, jumpOffsetBoost = 0x14D62A38;

		List<short> TechBoostPowers = new List<short>();
		foreach (int value in Values)
		{
			uint finalOffset = (uint)(value * 0x1C + startOffset);
			if (finalOffset >= jumpOffsetBoost)
				finalOffset = finalOffset + 0x130;
			bin.Position = finalOffset;
			TechBoostPowers.Add(reader.ReadInt16());
		}

		short[] shuffledPower = TechBoostPowers.ToArray();
		numberGenerator.Shuffle<short>(shuffledPower);

		for (int i = 0;  i < shuffledPower.Length; i++)
		{
			uint finalOffset = (uint)(Values[i] * 0x1C + startOffset);
			if (finalOffset >= jumpOffsetBoost)
				finalOffset = finalOffset + 0x130;
			bin.Position = finalOffset;
			writter.Write(shuffledPower[i]);
		}

		TechBoostPowers.Clear();
	}

	void randomizeBoostPower(int[] Values, int maxValue, int minValue)
	{
		uint startOffset = 0x14D62802, jumpOffsetBoost = 0x14D62A38;

		foreach (int value in Values)
		{
			uint finalOffset = (uint)(value * 0x1C + startOffset);
			if (finalOffset >= jumpOffsetBoost)
				finalOffset = finalOffset + 0x130;
			bin.Position = finalOffset;
			writter.Write((short)(numberGenerator.Next(maxValue) + minValue));
		}
	}

	void ShuffleEvolution(byte[] digimonList, uint startOffset, ref List<List<byte>> digimonlists)
	{
		List<List<byte>> Evos = new List<List<byte>>();

		for (int i = 0; i < digimonList.Length; i++)
		{
			bin.Position = 11 * digimonList[i] + startOffset + 5;
			if (bin.Position > 0x14D6CF98)
				bin.Position = bin.Position + 0x130;
			Evos.Add(new List<byte>());
			for (int j = 0; j < 6; j++)
			{
				byte evo = (byte)bin.ReadByte();
				if (bin.Position == 0x14D6CF98)
					bin.Position = bin.Position + 0x130;
				if (evo < 66 && evo != currentSukamon)
					Evos[i].Add(evo);
			}
		}

		byte[] Shuffled = digimonList;
		numberGenerator.Shuffle(Shuffled);

		for (int i = 0; i < Shuffled.Length; i++)
		{
			bin.Position = 11 * Shuffled[i] + startOffset + 5;
			if (bin.Position > 0x14D6CF98)
				bin.Position = bin.Position + 0x130;
			for (int j = 0; j < 6; j++)
			{				
				bin.WriteByte(0xff);
				if (bin.Position == 0x14D6CF98)
				bin.Position = bin.Position + 0x130;
			}

			bin.Position = 11 * Shuffled[i] + startOffset + 5;
			if (bin.Position > 0x14D6CF98)
				bin.Position = bin.Position + 0x130;

			if (Evos[i].Count < 5)
			{
				bin.WriteByte(0xff);
				if (bin.Position == 0x14D6CF98)
				bin.Position = bin.Position + 0x130;
			}

			if (Evos[i].Count < 3)
			{
				bin.WriteByte(0xff);
				if (bin.Position == 0x14D6CF98)
					bin.Position = bin.Position + 0x130;
			}
			if (Evos[i].Count > 0)
				for (int j = 0; j < Evos[i].Count; j++)
				{
					bin.WriteByte(Evos[i][j]);
					digimonlists[Evos[i][j]].Add(Shuffled[i]);
					if (bin.Position == 0x14D6CF98)
					bin.Position = bin.Position + 0x130;
				}
		}
	}

	void RandomizeEvolution(byte[] digimonList, byte[] evoList, uint startOffset, ref List<List<byte>> digimonLists, int randMax, int minMax)
	{
		List<byte> evos = new List<byte>(), evos2 = new List<byte>();
		evos.AddRange(evoList);

		for (int i = 0; i < digimonList.Length; i++)
		{
			if (evos.Count == 0)
			{
				evos.AddRange(evoList);
				evos2.Clear();
			}
			bin.Position = 11 * digimonList[i] + startOffset + 5;
			if (bin.Position > 0x14D6CF98)
				bin.Position = bin.Position + 0x130;
			int evoCount = numberGenerator.Next(randMax) + minMax;

			for (int j = 0; j < 6; j++)
			{
				bin.WriteByte(0xff);
				if (bin.Position == 0x14D6CF98)
				bin.Position = bin.Position + 0x130;
			}
			bin.Position = 11 * digimonList[i] + startOffset + 5;
			if (bin.Position > 0x14D6CF98)
				bin.Position = bin.Position + 0x130;

			if (evoCount < 5)
			{
				bin.WriteByte(0xff);
				if (bin.Position == 0x14D6CF98)
				bin.Position = bin.Position + 0x130;
			}
			if (evoCount < 3)
			{
				bin.WriteByte(0xff);
				if (bin.Position == 0x14D6CF98)
				bin.Position = bin.Position + 0x130;
			}
			for (int j = 0; j < evoCount; j++)
			{
				if (evos.Count > 0)
				{
					int rand = numberGenerator.Next(evos.Count());
					bin.WriteByte(evos[rand]);
					evos2.Add(evos[rand]);
					digimonLists[evos[rand]].Add(digimonList[i]);
					evos.RemoveAt(rand);
					if (bin.Position == 0x14D6CF98)
					bin.Position = bin.Position + 0x130;
				}
				else
				{
					bin.WriteByte(evos2.First());
					digimonLists[evos2.First()].Add(digimonList[i]);
					evos2.RemoveAt(0);
					if (bin.Position == 0x14D6CF98)
					bin.Position = bin.Position + 0x130;
				}
			}
		}
	}

	void SetPreviousDigimon(List<List<byte>> digimonlists, uint digimonDataOffset)
	{
		for (int i = 1; i < 63; i++)
		{
			bin.Position = i * 11 + digimonDataOffset;
			if (bin.Position > 0x14D6CF98)
				bin.Position = bin.Position + 0x130;
			for (int j = 0; j < 5; j++)
			{
				bin.WriteByte(0xff);
				if (bin.Position == 0x14D6CF98)
					bin.Position = bin.Position + 0x130;
			}
			bin.Position = i * 11 + digimonDataOffset;
			if (bin.Position > 0x14D6CF98)
				bin.Position = bin.Position + 0x130;
			if (digimonlists[i].Count != 0)
			{
				if (digimonlists[i].Count < 4)
				{
					bin.WriteByte(0xff);
					if (bin.Position == 0x14D6CF98)
						bin.Position = bin.Position + 0x130;
				}
				if (digimonlists[i].Count < 2)
				{
					bin.WriteByte(0xff);
					if (bin.Position == 0x14D6CF98)
						bin.Position = bin.Position + 0x130;
				}
				for (int j = 0; j < digimonlists[i].Count; j++)
				{
					bin.WriteByte(digimonlists[i][j]);
					if (bin.Position == 0x14D6CF98)
						bin.Position = bin.Position + 0x130;
					if (j == 4)
						break;
				}

			}
		}

		uint startOffset = 0x14D19CC8;
		for (int i = 0; i < 3; i++)
		{
			bin.Position = i * 5 + startOffset;
			for (int j = 0; j < 5; j++)
				bin.WriteByte(0xff);

			bin.Position = i * 5 + startOffset;
			if (digimonlists[63 + i].Count != 0)
			{
				if (digimonlists[63 + i].Count < 4)
					bin.WriteByte(0xff);
				if (digimonlists[63 + i].Count < 2)
					bin.WriteByte(0xff);
				for (int j = 0; j < digimonlists[63 + i].Count; j++)
				{
					bin.WriteByte(digimonlists[63 + i][j]);
					if (j == 4)
						break;
				}
			}
		}	
	}

	void ShuffleStatGains(byte[] digimonList, uint startOffset)
	{
		List<List<short>> digimonStats = new List<List<short>>();

		foreach (byte digimon in digimonList)
		{
			if (digimon == currentSukamon)
				continue;
			bin.Position = digimon * 14 + startOffset;
			digimonStats.Add(new List<short>());
			for (int i = 0; i < 6; i++)
				digimonStats.Last().Add(reader.ReadInt16());

		}

		byte[] shuffled = digimonList;
		numberGenerator.Shuffle(shuffled);

		int currentListValue = 0;

		for (int i = 0; i < shuffled.Length; i++)
		{
			if (shuffled[i] == currentSukamon)
				continue;
			bin.Position = shuffled[i] * 14 + startOffset;
			foreach (short stat in digimonStats[currentListValue])
				writter.Write(stat);
			currentListValue++;
		}
	}

	void RandomStatGains(byte[] digimonList, uint startOffset, int randomMax, int randomMin)
	{	
		for (int i = 0; i < digimonList.Length; i++)
		{
			if (digimonList[i] == currentSukamon)
				continue;

			bin.Position = digimonList[i] * 14 + startOffset;
			for (int j = 0; j < 6; j++)
			{
				if (j < 2)
					writter.Write((short)((numberGenerator.Next(randomMax) + randomMin) * 10));
				else
					writter.Write((short)(numberGenerator.Next(randomMax) + randomMin));
			}		
		}
	}

	void ShuffleRequirements(byte[] digimonList, uint startOffset, uint jumpOffset)
	{
		List<List<short>> digimonRequirements = new List<List<short>>();
		foreach (byte digimon in digimonList)
		{
			bin.Position = digimon * 28 + startOffset;
			if (bin.Position > jumpOffset)
				bin.Position = bin.Position + 0x130;
			digimonRequirements.Add(new List<short>());
			for (int i = 0; i < 14; i++)
			{
				digimonRequirements.Last().Add(reader.ReadInt16());
				if (bin.Position == jumpOffset)
					bin.Position = bin.Position + 0x130;
			}

		}

		byte[] shuffled = digimonList;
		numberGenerator.Shuffle(shuffled);

		for (int i = 0; i < shuffled.Length; i++)
		{
			bin.Position = shuffled[i] * 28 + startOffset;
			if (bin.Position > jumpOffset)
				bin.Position = bin.Position + 0x130;
			foreach (short requirement in digimonRequirements[i])
			{				
				writter.Write(requirement);
				if (bin.Position == jumpOffset)
					bin.Position = bin.Position + 0x130;
			}
		}
	}

	void RandomizeRequirements(byte[] digimonList, uint startOffset, uint jumpOffset, byte[] previousList, bool ultimate)
	{		
		foreach (byte digimon in digimonList)
		{
			bin.Position = digimon * 28 + startOffset;
			if (bin.Position > jumpOffset)
				bin.Position = bin.Position + 0x130;
			int choosenStat = numberGenerator.Next(6) + 1;
			for (int i = 0; i < 14; i++)
			{
				if (i == 0)
				{
					if (numberGenerator.Next(2) == 1 || i == choosenStat)
						writter.Write((short)previousList[numberGenerator.Next(previousList.Length)]);
					else
						writter.Write((short)-1);
				}
				else if (i < 7)
				{
					if (numberGenerator.Next(2) == 1 || i == choosenStat)
					{
						if (ultimate)
							writter.Write((short)(numberGenerator.Next(401) + 300));
						else
							writter.Write((short)(numberGenerator.Next(41) + 80));
					}
					else
						writter.Write((short)-1);
					if (bin.Position == jumpOffset)
						bin.Position = bin.Position + 0x130;
				}
				else if (i < 8)
				{
					if (ultimate)
						writter.Write((short)numberGenerator.Next(31));
					else
						writter.Write((short)numberGenerator.Next(16));
				}
				else if (i < 9)
				{
					if (ultimate)
						writter.Write((short)(numberGenerator.Next(99) + 1));
					else
						writter.Write((short)(numberGenerator.Next(60) + 1));

				}
				else if (i < 11)
				{
					if (numberGenerator.Next(2) == 1)
						writter.Write((short)numberGenerator.Next(100));
					else
						writter.Write((short)-1);
				}
				else if (i < 12)
				{
					if (numberGenerator.Next(2) == 1)
					{
						if (ultimate)
							writter.Write((short)numberGenerator.Next(101));
						else
							writter.Write((short)numberGenerator.Next(16));
					}
					else
						writter.Write((short)-1);
				}
				else if (i < 13)				
					writter.Write((short)numberGenerator.Next(56));
				else
					writter.Write((short)(numberGenerator.Next(2) * 16 + numberGenerator.Next(2)));
			}		
		}		
	}

	void RandomizeFactorialNormalUpgrades(ref List<byte> digimonList, uint[] upgradeOffsets)
	{
		byte randomDigimon = 1;		
		for (int i = 0; i < upgradeOffsets.Length; i++)
		{
			if (i % 3 == 0)
			{
				randomDigimon = (byte)(numberGenerator.Next(65) + 1);
				while (randomDigimon == currentSukamon)
					randomDigimon = (byte)(numberGenerator.Next(65) + 1);

				if (digimonList.Count == 0)
					digimonList.Add(randomDigimon);
				else
				{
					int currentAmount = digimonList.Count;
					while (currentAmount == digimonList.Count)
					{
						bool sameValue = false;
						foreach (byte digimon in digimonList)
						{
							if (digimon == randomDigimon)
							{
								sameValue = true;
								break;
							}
						}
						if (sameValue)
						{
							randomDigimon = (byte)(numberGenerator.Next(65) + 1);
							while (randomDigimon == currentSukamon)
								randomDigimon = (byte)(numberGenerator.Next(65) + 1);
							continue;
						}
						digimonList.Add(randomDigimon);
					}
				}
			}
			bin.Position = upgradeOffsets[i];
			bin.WriteByte(randomDigimon);
		}
	}

	void RandomizeFactoriaSpecialUpgrades(ref List<byte> digimonList, uint[] upgradeOffsets)
	{
		byte randomDigimon = 1;		
		for (int i = 0; i < upgradeOffsets.Length; i++)
		{
			randomDigimon = (byte)(numberGenerator.Next(65) + 1);
			while (randomDigimon == currentSukamon)
				randomDigimon = (byte)(numberGenerator.Next(65) + 1);

			if (digimonList.Count == 0)
				digimonList.Add(randomDigimon);
			else
			{
				int currentAmount = digimonList.Count;
				while (currentAmount == digimonList.Count)
				{
					bool sameValue = false;
					foreach (byte digimon in digimonList)
					{
						if (digimon == randomDigimon)
						{
							sameValue = true;
							break;
						}
					}
					if (sameValue)
					{
						randomDigimon = (byte)(numberGenerator.Next(65) + 1);
						while (randomDigimon == currentSukamon)
							randomDigimon = (byte)(numberGenerator.Next(65) + 1);
						continue;
					}
					digimonList.Add(randomDigimon);
				}
			}			
		    bin.Position = upgradeOffsets[i];
			bin.WriteByte(randomDigimon);
		}
	}

	void RandomizeFactorialEvolutions(List<byte> digimonList, uint[] evolutionOffsets)
	{
		byte randomDigimon = 1;
		for (int i = 0; i < evolutionOffsets.Length; i++)
		{	
			bool sameValue;
			do
			{
				sameValue = false;
				randomDigimon = (byte)(numberGenerator.Next(65) + 1);
				while (randomDigimon == currentSukamon)
					randomDigimon = (byte)(numberGenerator.Next(65) + 1);
				foreach (byte digimon in digimonList)
				{
					if (digimon == randomDigimon)
					{
						sameValue = true;
						break;
					}
				}			
			} while (sameValue);
			bin.Position = evolutionOffsets[i];
			bin.WriteByte(randomDigimon);
		}
	}

	void SetSukamonToyEvolution()
	{
		bin.Position = 0x14055EFD;
		bin.WriteByte((byte)currentSukamon);
		bin.Position = 0x14057F6D;
		bin.WriteByte((byte)currentSukamon);
		bin.Position = 0x14B97048;
		bin.WriteByte((byte)currentSukamon);
		bin.Position = 0x14B97078;
		bin.WriteByte((byte)currentSukamon);

		bin.Position = 0x140479ED;
		int check = bin.ReadByte();
		if (check == 14)
		{
			bin.Position = 0x14046841;
			byte unlockToy = (byte)bin.ReadByte();
			bin.Position = 0x140479ED;
			bin.WriteByte(unlockToy);			
		}
	}

	void SetExtraItemsDescriptions()
	{
		bin.Position = 0x14D68B18;
		bin.WriteByte(0x88);
		bin.WriteByte(0x59);

		bin.Position = 0x14D68B1C;
		bin.WriteByte(0x88);
		bin.WriteByte(0x59);

		bin.Position = 0x14D66996;
		bin.WriteByte(0x44);
		bin.WriteByte(0x69);
		bin.WriteByte(0x67);
		bin.WriteByte(0x69);
		bin.WriteByte(0x76);
		bin.WriteByte(0x6F);
		bin.WriteByte(0x6C);
		bin.WriteByte(0x76);
		bin.WriteByte(0x65);
		bin.WriteByte(0);
		bin.WriteByte(0);
	}

	void RandomizeNormalEvoItems()
	{
		int SukamonValue = 0;
		bool sukamonSet = false;

		for (int i = 0; i < 44; i++)
		{
			byte randomDigimon = (byte)(numberGenerator.Next(65) + 1);
			if (randomDigimon == currentSukamon)
			{
				if (!sukamonSet)
				{
					SukamonValue = i;
					sukamonSet = true;
				}
				else
					while (randomDigimon == currentSukamon)
						randomDigimon = (byte)(numberGenerator.Next(65) + 1);
			}
			bin.WriteByte(randomDigimon);
		}

		if (sukamonSet)
		{
			bin.Position = 0x14D19AFC;
			bin.WriteByte((byte)(SukamonValue + 71));
		}					
	}

	void SetRandomNoSukamon(uint offset)
	{
		byte rand = (byte)(numberGenerator.Next(65) + 1);

		while (rand == currentSukamon)
			rand = (byte)(numberGenerator.Next(65) + 1);

		bin.Position = offset;
		bin.WriteByte(rand);
	}

	void RandomizeBuffs(uint[] offsets, int maxRNG, int minValue)
	{
		short value = 0;
		for(int i = 0; i < offsets.Length; i++)
		{
			if (i % 2 == 0)
				value = (short)(numberGenerator.Next(maxRNG) + minValue);
						
			bin.Position = offsets[i];
			writter.Write(value);
		}		
	}

	void CreateRandoTxt()
	{
		string filename = Tr("Rando_txt") + ".txt";

		string path = System.IO.Path.Combine(folderPath, filename);
		System.IO.Stream txt = System.IO.File.OpenWrite(path);

		System.IO.StreamWriter txtWritter = new System.IO.StreamWriter(txt);

		txtWritter.Write(Tr("Seed") + ": " + Seed.Value);
		txtWritter.WriteLine();

		txtWritter.Write(Tr("items_txt"));
		txtWritter.WriteLine();

		if (itemsSpawn)
		{
			txtWritter.Write("- " + Tr("Item_T") + ":");
			switch (itemsSpawnOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Shuffle_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("Random_T"));
					break;
				case 2:
					txtWritter.Write(" " + Tr("Chaos_T"));
					break;
			}
			txtWritter.WriteLine();
		}

		if (spawnRateItems)
		{
			txtWritter.Write("- " + Tr("ItemSpawnRate_T") + ":");
			switch (spawnRateItemsOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Shuffle_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("Random_T"));
					break;
			}
			txtWritter.WriteLine();
		}

		if (itemDrops)
		{
			txtWritter.Write("- " + Tr("Drop_T") + ":");
			switch (itemDropsOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Shuffle_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("Random_T"));
					break;
				case 2:
					txtWritter.Write(" " + Tr("Chaos_T"));
					break;
			}
			txtWritter.WriteLine();
		}

		if (dropRate)
		{
			txtWritter.Write("- " + Tr("ItemDropRate_T") + ":");
			switch (dropRateOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Shuffle_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("Random_T"));
					break;
			}
			txtWritter.WriteLine();
		}

		if (chests)
		{
			txtWritter.Write("- " + Tr("Chest_T") + ":");
			switch (chestsOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Shuffle_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("Random_T"));
					break;
				case 2:
					txtWritter.Write(" " + Tr("Chaos_T"));
					break;
			}
			txtWritter.WriteLine();
		}

		if (shops)
		{
			txtWritter.Write("- " + Tr("ShopItems_T") + ":");
			switch (shopsOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Random_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("Chaos_T"));
					break;
			}
			txtWritter.WriteLine();
		}

		if (shopsPrices)
		{
			txtWritter.Write("- " + Tr("ShopPrices_T") + ":");
			switch (shopsPricesOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Shuffle_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("Random_T"));
					break;
			}
			txtWritter.WriteLine();
		}

		if (Mojyamon)
		{
			txtWritter.Write("- " + Tr("MojyamonR_T") + ":");
			switch (MojyamonOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Random_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("Chaos_T"));
					break;
			}
			txtWritter.WriteLine();
		}

		if (meritItems)
		{
			txtWritter.Write("- " + Tr("MeritR_T") + ":");
			switch (meritItemsOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Random_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("Chaos_T"));
					break;
			}
			txtWritter.WriteLine();
		}

		if (meritPrices)
		{
			txtWritter.Write("- " + Tr("MeritPrices_T") + ":");
			switch (meritPricesOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Shuffle_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("Random_T"));
					break;
			}
			txtWritter.WriteLine();
		}

		if (tournamentItems)
		{
			txtWritter.Write("- " + Tr("TournamentsDR_T") + ":");
			switch (tournamentItemsOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Shuffle_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("Random_T"));
					break;
			}
			txtWritter.WriteLine();
		}

		if (tokomon)
		{
			txtWritter.Write("- " + Tr("Tokomon_T") + ":");
			switch (tokomonOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Random_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("EasyStartR_T"));
					break;
			}
			txtWritter.WriteLine();
		}

		if (keyItems)
		{
			txtWritter.Write("- " + Tr("KeyItems_T"));
			txtWritter.WriteLine();
		}

		if (curlingRewards)
		{
			txtWritter.Write("- " + Tr("CurlingRewards_T"));
			txtWritter.WriteLine();
		}


		txtWritter.WriteLine();
		txtWritter.Write(Tr("digimon_txt"));
		txtWritter.WriteLine();

		if (difficulty)
		{
			txtWritter.Write("- " + Tr("DigimonDif_T") + ":");
			switch (difficultyOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Hardcore_L"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("THardcore_L"));
					break;
			}
			txtWritter.WriteLine();
		}

		if (digimonNPC)
		{
			txtWritter.Write("- Digimon:");
			switch (digimonNPCOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Shuffle_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("Random_T"));
					break;
				case 2:
					txtWritter.Write(" " + Tr("Chaos_T"));
					break;
			}
			txtWritter.WriteLine();
		}

		if (statsNPC)
		{
			txtWritter.Write("- " + Tr("DigimonStats_T") + ":");
			switch (statsNPCOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Shuffle_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("Random_T"));
					break;
				case 2:
					txtWritter.Write(" " + Tr("Chaos_T"));
					break;
			}
			txtWritter.WriteLine();
		}

		if (techNPC)
		{
			txtWritter.Write("- " + Tr("DigimonTechs_T"));
			txtWritter.WriteLine();
		}

		if (moneyNPC)
		{
			txtWritter.Write("- " + Tr("DigimonMoney_T") + ":");
			switch (moneyNPCOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Shuffle_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("Random_T"));
					break;
			}
			txtWritter.WriteLine();
		}

		if (bosses)
		{
			txtWritter.Write("- " + Tr("Bosses_T"));
			txtWritter.WriteLine();
		}

		if (starter)
		{
			txtWritter.Write("- " + Tr("DigimonStarter_T"));
			txtWritter.WriteLine();
		}

		if (starterTech)
		{
			txtWritter.Write("- " + Tr("StarterTech_T") + ":");
			switch (starterTechOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Weak_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("Random_T"));
					break;
			}
			txtWritter.WriteLine();
		}

		if (starterLevel)
		{
			txtWritter.Write("- " + Tr("StarterLevel_T") + ":");
			switch (starterLevelOpt)
			{
				case 0:
					txtWritter.Write(" Baby");
					break;
				case 1:
					txtWritter.Write(" In-Training");
					break;
				case 2:
					txtWritter.Write(" Rookie");
					break;
				case 3:
					txtWritter.Write(" Champion");
					break;
				case 4:
					txtWritter.Write(" Ultimate");
					break;
				case 5:
					txtWritter.Write(" " + Tr("RandomLevel_T"));
					break;
			}
			txtWritter.WriteLine();
		}

		if (starterStats)
		{
			txtWritter.Write("- " + Tr("StarterStats_T") + ":");
			switch (starterStatsOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Random_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("Chaos_T"));
					break;
			}
			txtWritter.WriteLine();
		}

		if (tournamentNPC)
		{
			txtWritter.Write("- " + Tr("TournamentsNPC_T") + ":");
			switch (tournamentNPCOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Shuffle_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("Random_T"));
					break;
			}
			txtWritter.WriteLine();
		}

		if (recruits)
		{
			txtWritter.Write("- " + Tr("Recruits_T") + ":");
			switch (recruitsOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Shuffle_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("Chaos_T"));
					break;
			}
			txtWritter.WriteLine();
		}

		txtWritter.WriteLine();
		txtWritter.Write(Tr("Misc_txt"));
		txtWritter.WriteLine();

		if (restaurant)
		{
			txtWritter.Write("- " + Tr("Restaurant_L") + ":");
			switch (restaurantOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Random_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("Chaos_T"));
					break;
			}
			txtWritter.WriteLine();
		}

		if (birdramon)
		{
			txtWritter.Write("- Birdramon:");
			switch (restaurantOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Random_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("Chaos_T"));
					break;
			}
			txtWritter.WriteLine();
		}

		if (boost)
		{
			txtWritter.Write("- " + Tr("BoostR_L") + ":");
			switch (boostOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Shuffle_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("Random_T"));
					break;
				case 2:
					txtWritter.Write(" " + Tr("Chaos_T"));
					break;
			}
			txtWritter.WriteLine();
		}

		if (healing)
		{
			txtWritter.Write("- " + Tr("Healing_L") + ":");
			switch (healingOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Shuffle_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("Random_T"));
					break;
				case 2:
					txtWritter.Write(" " + Tr("Chaos_T"));
					break;
			}
			txtWritter.WriteLine();
		}

		if (devil)
		{
			txtWritter.Write("- " + Tr("Devil_L") + ":");
			switch (devilOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Random_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("Chaos_T"));
					break;
			}
			txtWritter.WriteLine();
		}

		if (chips)
		{
			txtWritter.Write("- " + Tr("Chips_L") + ":");
			switch (chipsOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Random_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("Chaos_T"));
					break;
			}
			txtWritter.WriteLine();
		}

		if (seadramon)
		{
			txtWritter.Write("- Seadramon:");
			txtWritter.WriteLine();
		}

		if (fish)
		{
			txtWritter.Write("- " + Tr("FishBait_L") + ":");
			switch (fishOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Shuffle_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("Random_T"));
					break;
			}
			txtWritter.WriteLine();
		}

		if (tournamentSchedule)
		{
			txtWritter.Write("- " + Tr("TournamentSchedule_L") + ":");
			switch (tournamentScheduleOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Shuffle_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("Random_T"));
					break;
			}
			txtWritter.WriteLine();
		}

		if (food)
		{
			txtWritter.Write("- " + Tr("FoodR_L") + ":");
			switch (foodOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Shuffle_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("Random_T"));
					break;
				case 2:
					txtWritter.Write(" " + Tr("Chaos_T"));
					break;
			}
			txtWritter.WriteLine();
		}

		if (rareSpawns)
		{
			txtWritter.Write("- " + Tr("RareSpawnsR_L") + ":");
			switch (rareSpawnsOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Shared_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("Unique_T"));
					break;
			}
			txtWritter.WriteLine();
		}

		if (chaosItems)
		{
			txtWritter.Write("- " + Tr("ChaosItems_L"));
			txtWritter.WriteLine();
		}

		txtWritter.WriteLine();
		txtWritter.Write(Tr("evolution_txt"));
		txtWritter.WriteLine();

		if (tree)
		{
			txtWritter.Write("- " + Tr("EvoPath_L") + ":");
			switch (treeOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Shuffle_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("Random_T"));
					break;
				case 2:
					txtWritter.Write(" " + Tr("Chaos_T"));
					break;
			}
			txtWritter.WriteLine();
		}

		if (time)
		{
			txtWritter.Write("- " + Tr("EvoTime_L") + ":");
			switch (timeOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Shuffle_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("Random_T"));
					break;
				case 2:
					txtWritter.Write(" " + Tr("Chaos_T"));
					break;
			}
			txtWritter.WriteLine();
		}
		
		if (statGains)
		{
			txtWritter.Write("- " + Tr("EvoStatGains_L") + ":");
			switch (statGainsOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Shuffle_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("Random_T"));
					break;
				case 2:
					txtWritter.Write(" " + Tr("Chaos_T"));
					break;
			}
			txtWritter.WriteLine();
		}

		if (requirementsEvo)
		{
			txtWritter.Write("- " + Tr("EvoRequirements_L") + ":");
			switch (requirementsEvoOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Shuffle_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("Random_T"));
					break;
				case 2:
					txtWritter.Write(" " + Tr("Chaos_T"));
					break;
			}
			txtWritter.WriteLine();
		}

		if (evoItems)
		{
			txtWritter.Write("- " + Tr("EvoItems_L") + ":");
			switch (evoItemsOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Shuffle_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("Random_T"));
					break;
				case 2:
					txtWritter.Write(" " + Tr("Chaos_T"));
					break;
			}
			txtWritter.WriteLine();
		}

		if (specialEvo)
		{
			txtWritter.Write("- " + Tr("SpeEvolution_L") + ":");
			switch (specialEvoOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Shuffle_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("Random_T"));
					break;
			}
			txtWritter.WriteLine();
		}

		if (specialChance)
		{
			txtWritter.Write("- " + Tr("SpeChance_L") + ":");
			switch (specialChanceOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Shuffle_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("Random_T"));
					break;
			}
			txtWritter.WriteLine();
		}

		if (speEvoReq)
		{
			txtWritter.Write("- " + Tr("SpeRequirements_L") + ":");
			switch (speEvoReqOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Shuffle_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("Random_T"));
					break;
			}
			txtWritter.WriteLine();
		}

		if (factorial)
		{
			txtWritter.Write("- " + Tr("FactorialR_L") + ":");
			switch (statGainsOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Upgradable_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("EvolutionF_T"));
					break;
				case 2:
					txtWritter.Write(" " + Tr("RandomAll_T"));
					break;
			}
			txtWritter.WriteLine();
		}

		if (sukamon)
		{
			txtWritter.Write("- " + Tr("SukamonR_L"));
			txtWritter.WriteLine();
		}

		txtWritter.WriteLine();
		txtWritter.Write(Tr("Techs_txt"));
		txtWritter.WriteLine();

		if (damageTech)
		{
			txtWritter.Write("- " + Tr("TechDamageR_L") + ":");
			switch (damageTechOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Shuffle_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("Random_T"));
					break;
				case 2:
					txtWritter.Write(" " + Tr("Chaos_T"));
					break;
			}
			txtWritter.WriteLine();
		}

		if (MPtech)
		{
			txtWritter.Write("- " + Tr("MP") + ":");
			switch (MPtechOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Shuffle_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("Random_T"));
					break;
				case 2:
					txtWritter.Write(" " + Tr("Chaos_T"));
					break;
			}
			txtWritter.WriteLine();
		}
		
		if (damageType)
		{
			txtWritter.Write("- " + Tr("TypeDamageR_L") + ":");
			switch (damageTypeOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Shuffle_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("Random_T"));
					break;
				case 2:
					txtWritter.Write(" " + Tr("Chaos_T"));
					break;
			}
			txtWritter.WriteLine();
		}

		if (accuracy)
		{
			txtWritter.Write("- " + Tr("AccuracyR_L") + ":");
			switch (accuracyOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Shuffle_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("Random_T"));
					break;
				case 2:
					txtWritter.Write(" " + Tr("Chaos_T"));
					break;
			}
			txtWritter.WriteLine();
		}

		if (status)
		{
			txtWritter.Write("- " + Tr("TechStatus_L") + ":");
			switch (statusOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Shuffle_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("Random_T"));
					break;
				case 2:
					txtWritter.Write(" " + Tr("Chaos_T"));
					break;
			}
			txtWritter.WriteLine();
		}		

		if (statusChance)
		{
			txtWritter.Write("- " + Tr("StatusChance_L") + ":");
			switch (statusChanceOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Shuffle_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("Random_T"));
					break;
				case 2:
					txtWritter.Write(" " + Tr("Chaos_T"));
					break;
			}
			txtWritter.WriteLine();
		}	

		if (finishers)
		{
			txtWritter.Write("- " + Tr("FinishersR_L") + ":");
			switch (finishersOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Mixable_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("Unique_T"));
					break;
			}
			txtWritter.WriteLine();
		}

		if (boostedTech)
		{
			txtWritter.Write("- " + Tr("BoostTechR_L"));
			txtWritter.WriteLine();
		}
		

		if (boostedTechValue)
			{
				txtWritter.Write("- " + Tr("BoostPowerR_L") + ":");
				switch (boostedTechValueOpt)
				{
					case 0:
						txtWritter.Write(" " + Tr("Shuffle_T"));
						break;
					case 1:
						txtWritter.Write(" " + Tr("Random_T"));
						break;
					case 2:
						txtWritter.Write(" " + Tr("Chaos_T"));
						break;
				}
				txtWritter.WriteLine();
			}	

		if (learnBattle)
		{
			txtWritter.Write("- " + Tr("LearnBattleR_L") + ":");
			switch (learnBattleOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Shuffle_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("Random_T"));
					break;
				case 2:
					txtWritter.Write(" " + Tr("Chaos_T"));
					break;
			}
			txtWritter.WriteLine();
		}	

		if (learnBrains)
		{
			txtWritter.Write("- " + Tr("LearnBrainsR_L") + ":");
			switch (learnBrainsOpt)
			{
				case 0:
					txtWritter.Write(" " + Tr("Shuffle_T"));
					break;
				case 1:
					txtWritter.Write(" " + Tr("Random_T"));
					break;
				case 2:
					txtWritter.Write(" " + Tr("Chaos_T"));
					break;
			}
			txtWritter.WriteLine();
		}	

		if (givenTechs)
			txtWritter.Write("- " + Tr("GivenTechs_L"));

		txtWritter.Close();
		txtWritter.Dispose();
		txt.Close();
		txt.Dispose();		

		MetalFinish.Visible = true;
		MetalWait.Visible = false;
		patchingLoading.Text = Tr("RandomizedP");
		PatchingWait.GetOkButton().Text = Tr("ExitButton");
		SaveData();
		PatchingWait.Confirmed -= HandleError;
		PatchingWait.Confirmed += ExitRandomizerPressed;
	}

	void CheckSaveData()
	{
		if (!File.Exists(OS.GetExecutablePath().GetBaseDir() + "/SaveData/RandoSave"))
		{
			SaveDataButton.Disabled = true;
		}
	}

	void SaveData()
	{
		var saveData = new RandoSaveData(itemsSpawn, spawnRateItems, itemDrops, dropRate, chests, shops, shopsPrices, Mojyamon, meritItems, meritPrices, tournamentItems, keyItems, tokomon, curlingRewards,
		difficulty, digimonNPC, statsNPC, techNPC, moneyNPC, bosses, starter, starterTech, starterStats, starterLevel, tournamentNPC, recruits,
		restaurant, birdramon, boost, healing, devil, chips, seadramon, fish, tournamentSchedule, food, rareSpawns, chaosItems,
		tree, time, statGains, requirementsEvo, specialEvo, specialChance, evoItems, speEvoReq, factorial, sukamon,
		damageTech, MPtech, damageType, accuracy, status, statusChance, finishers, boostedTech, boostedTechValue, learnBattle, learnBrains, givenTechs,
		itemsSpawnOpt, spawnRateItemsOpt, itemDropsOpt, dropRateOpt, chestsOpt, shopsOpt, shopsPricesOpt, MojyamonOpt, meritItemsOpt, meritPricesOpt, tournamentItemsOpt, tokomonOpt,
		difficultyOpt, digimonNPCOpt, statsNPCOpt, moneyNPCOpt, starterTechOpt, starterStatsOpt, starterLevelOpt, tournamentNPCOpt, recruitsOpt,
		restaurantOpt, birdramonOpt, boostOpt, healingOpt, devilOpt, chipsOpt, fishOpt, tournamentScheduleOpt, foodOpt, rareSpawnsOpt,
		treeOpt, timeOpt, statGainsOpt, requirementsEvoOpt, specialEvoOpt, specialChanceOpt, evoItemsOpt, speEvoReqOpt, factorialOpt,
		damageTechOpt, MPtechOpt, damageTypeOpt, accuracyOpt, statusOpt, statusChanceOpt, finishersOpt, boostedTechValueOpt, learnBattleOpt, learnBrainsOpt);

		Directory.CreateDirectory(OS.GetExecutablePath().GetBaseDir() + "/SaveData");
		
		using var saveFile = Godot.FileAccess.Open(OS.GetExecutablePath().GetBaseDir() + "/SaveData/RandoSave", Godot.FileAccess.ModeFlags.Write);

		var bytes = JsonSerializer.SerializeToUtf8Bytes(saveData);
		
		saveFile.StoreBuffer(bytes);
	}

	void LoadSaveData()
	{
		if (File.Exists(OS.GetExecutablePath().GetBaseDir() + "/SaveData/RandoSave"))
		{
			using var saveFile = System.IO.File.Open(OS.GetExecutablePath().GetBaseDir() + "/SaveData/RandoSave", System.IO.FileMode.Open);
			var saveData = JsonSerializer.Deserialize<RandoSaveData>(saveFile);
			saveFile.Close();
			if (saveData != null)
			{
				itemsScript.LoadData(saveData.itemsSpawn, saveData.spawnRateItems, saveData.itemDrops, saveData.dropRate, saveData.chests, saveData.shops, saveData.shopsPrices,
				saveData.Mojyamon, saveData.meritItems, saveData.meritPrices, saveData.tournamentItems, saveData.tokomon, saveData.keyItems, saveData.curlingRewards,
				saveData.itemsSpawnOpt, saveData.spawnRateItemsOpt, saveData.itemDropsOpt, saveData.dropRateOpt, saveData.chestsOpt, saveData.shopsOpt, saveData.shopsPricesOpt,
				saveData.MojyamonOpt, saveData.meritItemsOpt, saveData.meritPricesOpt, saveData.tournamentItemsOpt, saveData.tokomonOpt);

				digimonScript.LoadSaveData(saveData.difficulty, saveData.digimonNPC, saveData.statsNPC, saveData.techNPC, saveData.moneyNPC, saveData.bosses, saveData.starter,
				saveData.starterTech, saveData.starterLevel, saveData.starterStats, saveData.tournamentNPC, saveData.recruits, saveData.difficultyOpt, saveData.digimonNPCOpt, saveData.statsNPCOpt,
				saveData.moneyNPCOpt, saveData.starterTechOpt, saveData.starterLevelOpt, saveData.starterStatsOpt, saveData.tournamentNPCOpt, saveData.recruitsOpt);

				miscScript.LoadSaveData(saveData.restaurant, saveData.birdramon, saveData.boost, saveData.healing, saveData.devil, saveData.chips, saveData.seadramon, saveData.fish,
				saveData.tournamentSchedule, saveData.food, saveData.rareSpawns, saveData.chaosItems, saveData.restaurantOpt, saveData.birdramonOpt, saveData.boostOpt,
				saveData.healingOpt, saveData.devilOpt, saveData.chipsOpt, saveData.fishOpt, saveData.tournamentScheduleOpt, saveData.foodOpt, saveData.rareSpawnsOpt);

				evoScript.LoadSaveData(saveData.tree, saveData.time, saveData.statGains, saveData.requirementsEvo, saveData.evoItems, saveData.specialEvo, saveData.specialChance,
				saveData.speEvoReq, saveData.factorial, saveData.sukamon, saveData.treeOpt, saveData.timeOpt, saveData.statGainsOpt, saveData.requirementsEvoOpt, saveData.evoItemsOpt,
				saveData.specialEvoOpt, saveData.specialChanceOpt, saveData.speEvoReqOpt, saveData.factorialOpt);

				techScript.LoadSaveData(saveData.damageTech, saveData.MPtech, saveData.damageType, saveData.accuracy, saveData.status, saveData.statusChance, saveData.finishers,
				saveData.boostedTech, saveData.boostedTechValue, saveData.learnBattle, saveData.learnBrains, saveData.givenTechs, saveData.damageTechOpt, saveData.MPtechOpt,
				saveData.damageTypeOpt, saveData.accuracyOpt, saveData.statusOpt, saveData.statusChanceOpt, saveData.finishersOpt, saveData.boostedTechValueOpt, saveData.learnBattleOpt,
				saveData.learnBrainsOpt);
			}
		}
	}
}
