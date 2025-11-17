using Godot;
using Godot.NativeInterop;
using System;
using System.Drawing;
using PatcherData;
using System.IO;
using System.Text.Json;
using System.Runtime.CompilerServices;

public partial class VicePatcherContainer : SubViewportContainer
{

	public enum ViceDifficulty
	{
		CHALLENGE = 0,
		HARDCORE = 1,
		HARDMODE = 2,
		NONE = 3,
	}

	[Serializable]
	class ViceSaveData
	{
		public ViceDifficulty viceDifficulty { get; set; }
		public bool myotismon { get; set; }
		public bool vermillimon { get; set; }
		public bool filth { get; set; }
		public bool restoreFilth { get; set; }
		public bool superHardcore { get; set; }
		public bool hardMono { get; set; }
		public bool hardTourney { get; set; }
		public bool betterBattleTech { get; set; }
		public bool betterBrainTechs { get; set; }
		public bool betterDrop { get; set; }
		public bool superBonus { get; set; }
		public bool tanemon { get; set; }
		public bool rookieOnly { get; set; }
		public bool ultraBonus { get; set; }
		public bool dirtReduction { get; set; }
		public bool sDirtReduction { get; set; }
		public bool shortIntro { get; set; }
		public bool statsGains { get; set; }
		public bool multipleTechs { get; set; }
		public bool evoItem { get; set; }
		public bool helpfulItems { get; set; }
		public bool lessMono { get; set; }
		public bool nerfIce { get; set; }
		public bool curlingRandomizer { get; set; }
		public bool betterRestaurant { get; set; }
		public bool progression { get; set; }
		public bool itemSpawns { get; set; }
		public bool raise { get; set; }
		public bool drimogemon { get; set; }
		public bool cards { get; set; }
		public bool merit { get; set; }
		public bool fishing { get; set; }
		public bool usefulItems2 { get; set; }
		public bool curling { get; set; }
		public bool trainingBoost { get; set; }
		public bool insaneDamage { get; set; }
		public bool trueHardcore { get; set; }
		public bool noOrders { get; set; }
		public bool medals { get; set; }
		public bool seadramon { get; set; }
		public bool rareSpawns { get; set; }
		public bool hyperMono { get; set; }
		public bool extraInput { get; set; }
		public bool removeEvoInfo { get; set; }
		public bool originalType { get; set; }
		public bool newMono { get; set; }
		public bool realMetal { get; set; }
		public bool vendingMachine { get; set; }
		public bool easyStart { get; set; }
		public bool moreItemDrops { get; set; }
		public bool MoreItemSpawn { get; set; }
		public bool insaneBattle { get; set; }
		public bool restoreLifetime { get; set; }
		public bool removeTechBoost { get; set; }
		public bool unlockAreas { get; set; }
		public bool mapColour { get; set; }
		public bool ultraHardcore { get; set; }
		public bool WarGreymon { get; set; }
		public bool MetalGaru { get; set; }
		public bool Tentomon { get; set; }
		public bool restorePanjyamon { get; set; }
		public bool starters2 { get; set; }
		public bool kunemon { get; set; }
		public bool removeTelephone { get; set; }
		public bool easyTechs { get; set; }
		public bool BlackWere { get; set; }
		public bool quickText { get; set; }
		public bool digitalClock { get; set; }
		public bool noRNG { get; set; }
		public bool boostItems { get; set; }
		public bool nerfTechBoost { get; set; }
		public int StatsValue { get; set; }
		public int RareSpawnValue { get; set; }

		public bool dropItemsBattle { get; set; }
		public bool nerfEffect { get; set; }
		public bool removeEffect { get; set; }
		public bool removeNewEffects { get; set; }
		public bool addTimer { get; set; }
		public bool allBattles { get; set; }
		public bool removeExtraInfo { get; set; }
		public bool moreEffects { get; set; }

		public ViceSaveData(ViceDifficulty viceDifficulty, bool myotismon, bool vermillimon, bool filth, bool restoreFilth, bool superHardcore, bool hardMono, bool hardTourney,
		bool betterBattleTech, bool betterBrainTechs, bool betterDrop, bool superBonus, bool tanemon, bool rookieOnly, bool ultraBonus, bool dirtReduction,
		bool sDirtReduction, bool shortIntro, bool statsGains, bool multipleTechs, bool evoItem, bool helpfulItems, bool lessMono, bool nerfIce,
		bool curlingRandomizer, bool betterRestaurant, bool progression, bool itemSpawns, bool raise, bool drimogemon, bool cards, bool merit, bool fishing,
		bool usefulItems2, bool curling, bool trainingBoost, bool insaneDamage, bool trueHardcore, bool noOrders, bool medals, bool seadramon, bool rareSpawns,
		bool hyperMono, bool extraInput, bool removeEvoInfo, bool originalType, bool newMono, bool realMetal, bool vendingMachine, bool easyStart,
		bool moreItemDrops, bool MoreItemSpawn, bool insaneBattle, bool restoreLifetime, bool removeTechBoost, bool unlockAreas, bool mapColour, bool ultraHardcore,
		bool WarGreymon, bool MetalGaru, bool Tentomon, bool restorePanjyamon, bool starters2, bool kunemon, bool removeTelephone, bool easyTechs,
		bool BlackWere, bool quickText, bool digitalClock, bool noRNG, bool boostItems, bool nerfTechBoost, int StatsValue, int RareSpawnValue, bool dropItemsBattle = false,
		bool nerfEffect = false, bool removeEffect = false, bool removeNewEffects = false, bool addTimer = false, bool allBattles = false, bool removeExtraInfo = false,
		bool moreEffects = false)
		{
			this.viceDifficulty = viceDifficulty;
			this.myotismon = myotismon;
			this.vermillimon = vermillimon;
			this.filth = filth;
			this.restoreFilth = restoreFilth;
			this.superHardcore = superHardcore;
			this.hardMono = hardMono;
			this.hardTourney = hardTourney;
			this.betterBattleTech = betterBattleTech;
			this.betterBrainTechs = betterBrainTechs;
			this.betterDrop = betterDrop;
			this.superBonus = superBonus;
			this.tanemon = tanemon;
			this.rookieOnly = rookieOnly;
			this.ultraBonus = ultraBonus;
			this.dirtReduction = dirtReduction;
			this.sDirtReduction = sDirtReduction;
			this.shortIntro = shortIntro;
			this.statsGains = statsGains;
			this.multipleTechs = multipleTechs;
			this.evoItem = evoItem;
			this.helpfulItems = helpfulItems;
			this.lessMono = lessMono;
			this.nerfIce = nerfIce;
			this.curlingRandomizer = curlingRandomizer;
			this.betterRestaurant = betterRestaurant;
			this.progression = progression;
			this.itemSpawns = itemSpawns;
			this.raise = raise;
			this.drimogemon = drimogemon;
			this.cards = cards;
			this.merit = merit;
			this.fishing = fishing;
			this.usefulItems2 = usefulItems2;
			this.curling = curling;
			this.trainingBoost = trainingBoost;
			this.insaneDamage = insaneDamage;
			this.trueHardcore = trueHardcore;
			this.noOrders = noOrders;
			this.medals = medals;
			this.seadramon = seadramon;
			this.rareSpawns = rareSpawns;
			this.hyperMono = hyperMono;
			this.extraInput = extraInput;
			this.removeEvoInfo = removeEvoInfo;
			this.originalType = originalType;
			this.newMono = newMono;
			this.realMetal = realMetal;
			this.vendingMachine = vendingMachine;
			this.easyStart = easyStart;
			this.moreItemDrops = moreItemDrops;
			this.MoreItemSpawn = MoreItemSpawn;

			this.insaneBattle = insaneBattle;
			this.restoreLifetime = restoreLifetime;
			this.removeTechBoost = removeTechBoost;
			this.unlockAreas = unlockAreas;
			this.mapColour = mapColour;
			this.ultraHardcore = ultraHardcore;
			this.WarGreymon = WarGreymon;
			this.MetalGaru = MetalGaru;
			this.Tentomon = Tentomon;
			this.restorePanjyamon = restorePanjyamon;
			this.starters2 = starters2;
			this.kunemon = kunemon;
			this.removeTelephone = removeTelephone;
			this.easyTechs = easyTechs;
			this.BlackWere = BlackWere;
			this.quickText = quickText;
			this.digitalClock = digitalClock;
			this.noRNG = noRNG;
			this.boostItems = boostItems;
			this.nerfTechBoost = nerfTechBoost;

			this.StatsValue = StatsValue;
			this.RareSpawnValue = RareSpawnValue;

			this.dropItemsBattle = dropItemsBattle;
			this.nerfEffect = nerfEffect;
			this.removeEffect = removeEffect;
			this.removeNewEffects = removeNewEffects;
			this.addTimer = addTimer;
			this.allBattles = allBattles;
			this.removeExtraInfo = removeExtraInfo;
			this.moreEffects = moreEffects;
		}
	}
	//main stuff

	[Export]
	private Label mainTitle;

	[Export]
	private PanelContainer difficultyContainer;

	[Export]
	private PanelContainer digimonContainer;

	[Export]
	private PanelContainer miscContainer;

	[Export]
	private PanelContainer usefulContainer;

	[Export]
	private PanelContainer techContainer;

	[Export]
	private Panel useful1;

	[Export]
	private Panel useful2;

	[Export]
	private Button Difficulty;

	[Export]
	private Button Digimon;

	[Export]
	private Button Miscellaneous;

	[Export]
	private Button Useful;

	[Export]
	private Button Techniques;

	[Export]
	private Button StartPatch;

	[Export]
	private TextureButton ExitInstaller;

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
	private Button chooseFolder;

	[Export]
	private Label FileNameL;

	[Export]
	private Label patchConfirmTitle;

	[Export]
	private LineEdit FolderPath;

	[Export]
	private LineEdit FileNameSet;

	[Export]
	private Sprite2D WarFinish;

	[Export]
	private AnimatedSprite2D WarWait;

	[Export]
	private Label patchingLoading;

	[Export]
	private ConfirmationDialog PatchingWait;

	[Export]
	private Panel difficulty1;

	[Export]
	private Panel difficulty2;

	[Export] DifficultyContainer difficultyScript;
	[Export] DigimonContainer digimonScript;
	[Export] MiscContainer miscScript;
	[Export] UsefulContainer usefulScript;
	[Export] TechContainer techScript;
	[Export] Button OpenViceSettings;
	[Export] Button RecommendedSettings;
	[Export] Button SaveSettings;
	[Export] Button LoadSettings;
	[Export] Button BackSaveSettings;
	[Export] Button BackRecommendedSettings;
	[Export] private PanelContainer ViceSettings;
	[Export] Button[] Saves;
	[Export] Button Recommended;
	[Export] Button Easy;
	[Export] Button HardcoreSettings;
	[Export] Button UltraHardcoreSettings;
	[Export] Button Speedrun;
	[Export] Button SpeedrunHard;
	[Export] Button Vanilla;
	[Export] Button UltraSettings;
	[Export] private Panel ViceMainSettings;
	[Export] private Panel ViceSave;
	[Export] private Panel ViceRecommended;
	[Export] private Label ViceMainSettingsLabel;
	[Export] private Label ViceSaveLabel;
	[Export] private Label ViceRecommendedLabel;
	[Export] private ConfirmationDialog ViceSaveConfirm;
	[Export] private LineEdit SaveName;
	[Export] private Label SaveLabel;
	[Export] private Label ViceSaveConfirmTitle;
	[Export] Button LatestOptions;



	System.IO.BinaryReader ppf;
	System.IO.Stream bin;
	patchType currentPatcher;

	string filePath, newFilePath, fileDirectory;
	ViceSaveData saveData;

	string[] saveNames = new string[8];

	byte[] statHexValues1 = {0x21, 0x18, 0x0, 0x1, 0x18, 0x0, 0x83, 0x0, 0x12, 0x18, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x18, 0x0, 0x62, 0x0, 0x12, 0x18, 0x0, 0x0,
	0x0, 0x0, 0x0, 0x0, 0x64, 0x0, 0x2, 0x24, 0x1a, 0x0, 0x62, 0x0, 0x12, 0x18, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x2a, 0x8, 0x82, 0x0};
	byte[] statHexValues2 = { 0x19, 0x0, 0x60, 0x10 };
	string newSaveName = null;
	int currentSave = -1;

	ViceDifficulty currentViceDifficulty;

	Base_script parent_script;

	bool myotismon, vermillimon, filth, restoreFilth, hardmode, superHardcore, hardMono, hardTourney, betterBattleTech, betterBrainTechs, betterDrop, superBonus, tanemon, rookieOnly,
		 ultraBonus, dirtReduction, sDirtReduction, shortIntro, statsGains, multipleTechs, evoItem, helpfulItems, lessMono, nerfIce,
		 curlingRandomizer, betterRestaurant, progression, itemSpawns, raise, drimogemon, cards, merit, fishing, usefulItems2, curling,
		 trainingBoost, insaneDamage, trueHardcore, noOrders, medals, seadramon, rareSpawns, hyperMono, extraInput, removeEvoInfo, originalType, newMono, realMetal,
		 vendingMachine, easyStart, moreItemDrops, MoreItemSpawn;

	//Vice exclusive
	bool insaneBattle, restoreLifetime, removeTechBoost, unlockAreas, mapColour, ultraHardcore, WarGreymon, MetalGaru, Tentomon, DropItemsBattle,
		 restorePanjyamon, starters2, kunemon, removeTelephone, easyTechs, BlackWere, quickText, digitalClock, noRNG, boostItems, nerfTechBoost,
		 removeEffectBoost, nerfEffectBoost, removeNewEffects, poisonTimer, allBattles, removeExtraInfo, moreEffects;

	int StatsValue = 1, RareSpawnValue = 10;

	bool warFrame = false, warError = false;

	System.Threading.Thread waiting;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Difficulty.Toggled += DifficultyActive;
		Digimon.Toggled += DigimonActive;
		Miscellaneous.Toggled += MiscActive;
		Useful.Toggled += UsefulActive;
		Techniques.Toggled += TechniqueActive;
		ExitInstaller.Pressed += ExitViceInstaller;
		StartPatch.Pressed += OnPatch;
		selectFolder.DirSelected += OnFolderSelected;
		confirmationPatch.Confirmed += OnPatchConfirmed;
		FileNameSet.TextChanged += OnNameSet;
		confirmationPatch.Canceled += OnPatchCanceled;
		confirmationPatch.CloseRequested += OnPatchCanceled;
		chooseFolder.Pressed += _on_folderButton_pressed;
		PatchingWait.Canceled += HandleError;
		for (int i = 0; i < Saves.Length; i++)
		{
			int temp = i;
			Saves[i].Toggled += (bool saving) =>
			{
				if (saving)
				{
					currentSave = temp;
					SaveName.PlaceholderText = "Save " + (temp + 1);
					SetSave();
				}
				else
				{
					LoadSaveData(Saves[temp].Text);
				}
			};
		}
		OpenViceSettings.Pressed += OpenViceSave;
		RecommendedSettings.Pressed += OpenRecommended;
		SaveSettings.Pressed += SavePressed;
		LoadSettings.Pressed += LoadPressed;
		BackSaveSettings.Pressed += BackSaves;
		BackRecommendedSettings.Pressed += BackRecommended;
		Recommended.Pressed += RecommendedVice;
		Easy.Pressed += EasyVice;
		HardcoreSettings.Pressed += Hardcore;
		UltraHardcoreSettings.Pressed += UltraHardcore;
		Speedrun.Pressed += SpeedrunMode;
		SpeedrunHard.Pressed += SpeedHardcore;
		Vanilla.Pressed += VanillaMode;
		UltraSettings.Pressed += MasochistMode;
		ViceSaveConfirm.Confirmed += ConfirmSave;
		ViceSaveConfirm.Canceled += CloseSave;
		ViceSaveConfirm.CloseRequested += CloseSave;
		LatestOptions.Pressed += LoadLatest;

		patchingLoading.Text = Tr("Patching");
		mainTitle.Text = Tr("OptionalTitle");
		Difficulty.Text = Tr("Difficulty_T");
		Miscellaneous.Text = Tr("Miscellaneous_T");
		Techniques.Text = Tr("Techniques_T");
		Useful.Text = Tr("Useful_T");
		chooseFolder.Text = Tr("SFolder");
		FileNameL.Text = Tr("FName");
		FolderPath.PlaceholderText = Tr("SFolderT");
		patchConfirmTitle.Text = Tr("PatchTitle");
		StartPatch.Text = Tr("PatchButton");
		PatchingWait.GetOkButton().Visible = false;
		PatchingWait.GetOkButton().Text = Tr("PatchToRando");
		PatchingWait.GetCancelButton().Text = Tr("CancelButton");
		confirmationPatch.GetOkButton().Text = Tr("PatchButton");
		confirmationPatch.GetCancelButton().Text = Tr("CancelButton");
		OpenViceSettings.Text = Tr("ViceSettingsButton");
		OpenViceSettings.TooltipText = Tr("ViceSettingsButtonInfo");
		RecommendedSettings.Text = Tr("RecommendedSettings");
		ViceRecommendedLabel.Text = Tr("RecommendedSettings");
		ViceMainSettingsLabel.Text = Tr("SaveSettingsTitle");
		SaveSettings.Text = Tr("SaveSettingsLabel");
		LoadSettings.Text = Tr("LoadSettingsLabel");
		BackSaveSettings.Text = Tr("BackButtonSave");
		BackRecommendedSettings.Text = Tr("BackButtonSave");
		SaveLabel.Text = Tr("SaveConfirmLabel");
		ViceSaveConfirmTitle.Text = Tr("SaveTitle");
		Recommended.Text = Tr("ViceRecommended");
		Recommended.TooltipText = Tr("ViceRecommendedInfo");
		Easy.Text = Tr("EasyMode");
		Easy.TooltipText = Tr("EasyModeInfo");
		HardcoreSettings.TooltipText = Tr("HardcoreModeInfo");
		UltraHardcoreSettings.TooltipText = Tr("UltraModeInfo");
		Speedrun.TooltipText = Tr("SpeedInfo");
		SpeedrunHard.TooltipText = Tr("SpeedHardInfo");
		Vanilla.Text = Tr("VanillaMode");
		Vanilla.TooltipText = Tr("VanillaModeInfo");
		UltraSettings.Text = Tr("PainfulMode");
		UltraSettings.TooltipText = Tr("PainfulModeInfo");
		LatestOptions.Text = Tr("LatestOptions");
		LatestOptions.TooltipText = Tr("LatestOptionsInfo");

		RestartBoolsInstaller();
		CheckSaveFiles();

		waiting = new System.Threading.Thread(StartWaiting);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetStartingData(patchType selectedPatcher, string BINfilePath, Base_script OGScript)
	{
		if (selectedPatcher == patchType.OPTIONAL)
		{
			Difficulty.Disabled = true;
			Difficulty.Visible = false;
			FileNameSet.PlaceholderText = "Digimon World Vice Optionals";
			OpenViceSettings.Visible = false;
		}
		else
			FileNameSet.PlaceholderText = "Digimon World Vice";

		currentPatcher = selectedPatcher;
		filePath = BINfilePath;
		parent_script = OGScript;
	}

	void DifficultyActive(bool pressed)
	{
		if (pressed)
		{
			difficultyContainer.Visible = true;
			digimonContainer.Visible = false;
			miscContainer.Visible = false;
			usefulContainer.Visible = false;
			techContainer.Visible = false;
			Difficulty.Disabled = true;
			Digimon.Disabled = false;
			Digimon.ButtonPressed = false;
			Miscellaneous.Disabled = false;
			Miscellaneous.ButtonPressed = false;
			Useful.Disabled = false;
			Useful.ButtonPressed = false;
			Techniques.Disabled = false;
			Techniques.ButtonPressed = false;
			ViceSettings.Visible = false;
			OpenViceSettings.Disabled = false;

			difficulty1.Visible = true;
			difficulty2.Visible = false;
		}
	}

	void DigimonActive(bool pressed)
	{
		if (pressed)
		{
			difficultyContainer.Visible = false;
			digimonContainer.Visible = true;
			miscContainer.Visible = false;
			usefulContainer.Visible = false;
			techContainer.Visible = false;
			Difficulty.Disabled = false;
			Difficulty.ButtonPressed = false;
			Digimon.Disabled = true;
			Miscellaneous.Disabled = false;
			Miscellaneous.ButtonPressed = false;
			Useful.Disabled = false;
			Useful.ButtonPressed = false;
			Techniques.Disabled = false;
			Techniques.ButtonPressed = false;
			ViceSettings.Visible = false;
			OpenViceSettings.Disabled = false;
		}
	}

	void MiscActive(bool pressed)
	{
		if (pressed)
		{
			difficultyContainer.Visible = false;
			digimonContainer.Visible = false;
			miscContainer.Visible = true;
			usefulContainer.Visible = false;
			techContainer.Visible = false;
			Difficulty.Disabled = false;
			Difficulty.ButtonPressed = false;
			Digimon.Disabled = false;
			Digimon.ButtonPressed = false;
			Miscellaneous.Disabled = true;
			Useful.Disabled = false;
			Useful.ButtonPressed = false;
			Techniques.Disabled = false;
			Techniques.ButtonPressed = false;
			ViceSettings.Visible = false;
			OpenViceSettings.Disabled = false;
		}
	}

	void UsefulActive(bool pressed)
	{
		if (pressed)
		{
			difficultyContainer.Visible = false;
			digimonContainer.Visible = false;
			miscContainer.Visible = false;
			usefulContainer.Visible = true;
			techContainer.Visible = false;
			Difficulty.Disabled = false;
			Difficulty.ButtonPressed = false;
			Digimon.Disabled = false;
			Digimon.ButtonPressed = false;
			Miscellaneous.Disabled = false;
			Miscellaneous.ButtonPressed = false;
			Useful.Disabled = true;
			Techniques.Disabled = false;
			Techniques.ButtonPressed = false;
			ViceSettings.Visible = false;
			OpenViceSettings.Disabled = false;

			useful1.Visible = true;
			useful2.Visible = false;
		}
	}

	void TechniqueActive(bool pressed)
	{
		if (pressed)
		{
			difficultyContainer.Visible = false;
			digimonContainer.Visible = false;
			miscContainer.Visible = false;
			usefulContainer.Visible = false;
			techContainer.Visible = true;
			Difficulty.Disabled = false;
			Difficulty.ButtonPressed = false;
			Digimon.Disabled = false;
			Digimon.ButtonPressed = false;
			Miscellaneous.Disabled = false;
			Miscellaneous.ButtonPressed = false;
			Useful.Disabled = false;
			Useful.ButtonPressed = false;
			Techniques.Disabled = true;
			ViceSettings.Visible = false;
			OpenViceSettings.Disabled = false;
		}

	}

	void ExitViceInstaller()
	{
		parent_script.SetVisibleAgain();
		this.QueueFree();
	}

	void Information(string info, string title, string link, string buttonText = null)
	{
		InformationWindow.Title = title;
		if (buttonText != null)
			InformationButton.Text = buttonText;
		InformationText.Text = info;
		InformationButton.Uri = link;
	}

	void SetDifficultyInfo()
	{
		StyleBoxFlat StyleboxUpdate = InformationWindow.GetThemeStylebox("embedded_border").Duplicate() as StyleBoxFlat;
		StyleboxUpdate.BgColor = new Godot.Color(1, 0, 0);
		InformationWindow.AddThemeStyleboxOverride("embedded_border", StyleboxUpdate);
		InformationWindow.AddThemeColorOverride("title_color", new Godot.Color(1, 1, 1));
		InformationWindow.Visible = true;
	}

	void _on_window_close_requested()
	{
		InformationWindow.Visible = false;
		InformationButton.Visible = true;
	}

	void _on_qhardmode_pressed()
	{
		Information(Tr("HARDMODE"), "Hardmode information", "https://docs.google.com/spreadsheets/d/14wAuaMaLK6YZqwGXQSaG4RKCBfH6JWnrXg2cVAL_KW4/edit?usp=sharing");
		SetDifficultyInfo();
	}

	void _on_qprogression_pressed()
	{
		OpenLink("https://docs.google.com/spreadsheets/d/147dr8Uq_LT1X0STYwuvS59PwbZ_l4hK97GsVCvzJISU/edit?usp=sharing");
	}

	void _on_q_flith_pressed()
	{
		Information(Tr("FILTH"), "Filth Challenge", "https://docs.google.com/spreadsheets/d/1I0rPMIKg5Q7H1EwDlyiOBTGRkKlFxaXM_O4I5mxMdfg/edit?usp=sharing");
		SetDifficultyInfo();
	}

	void _on_qhardcore_pressed()
	{
		Information(Tr("HARDCORE"), "Hardcore information", "https://docs.google.com/spreadsheets/d/13hKiq2UGXikMRRLJfKpkudDk9RZ4BRvlCYma1Sof11A/edit?usp=sharing");
		SetDifficultyInfo();
	}

	void SetDigimonInfo()
	{
		StyleBoxFlat StyleboxUpdate = InformationWindow.GetThemeStylebox("embedded_border").Duplicate() as StyleBoxFlat;
		StyleboxUpdate.BgColor = new Godot.Color(1, 1, 0.525f);
		InformationWindow.AddThemeStyleboxOverride("embedded_border", StyleboxUpdate);
		InformationWindow.AddThemeColorOverride("title_color", new Godot.Color(0, 0, 0));
		InformationButton.Visible = false;
		InformationWindow.Visible = true;
	}

	void _on_qmtgr_pressed()
	{
		Information(Tr("RMTGR"), "Real MetalGreymon", "");
		SetDigimonInfo();
	}

	void _on_qb_were_pressed()
	{
		Information(Tr("BWERE"), "Black WereGarurumon", "");
		SetDigimonInfo();
	}

	void _on_q_curling_pressed()
	{
		Information(Tr("CURLING"), "Curling randomizer", "");
		SetDigimonInfo();
	}

	void _on_q_mono_pressed()
	{
		Information(Tr("MONOCHROMON"), "New Monochromon", "https://docs.google.com/spreadsheets/d/1dYhxP6BNmiXRcwR9djcCMr7fpn3joppjbWun5_Pk-fQ/edit?usp=sharing");
		StyleBoxFlat StyleboxUpdate = InformationWindow.GetThemeStylebox("embedded_border").Duplicate() as StyleBoxFlat;
		StyleboxUpdate.BgColor = new Godot.Color(1, 1, 0.525f);
		InformationWindow.AddThemeStyleboxOverride("embedded_border", StyleboxUpdate);
		InformationWindow.AddThemeColorOverride("title_color", new Godot.Color(0, 0, 0));
		InformationWindow.Visible = true;
	}

	void _on_q_useful_pressed()
	{
		OpenLink("https://docs.google.com/spreadsheets/d/1Wi1Cg0uHVHaEwUeSRae2neZoD93dYVkoHmjdqvDd9Ko/edit?usp=sharing");
	}

	void _on_q_useful2_pressed()
	{
		OpenLink("https://docs.google.com/spreadsheets/d/1Wi1Cg0uHVHaEwUeSRae2neZoD93dYVkoHmjdqvDd9Ko/edit?usp=sharing");
	}

	void _on_q_techs_pressed()
	{
		OpenLink("https://docs.google.com/spreadsheets/d/1OBg9Ke_JZ_8TjA62ldYacz5GYiKS0L-56FoG0JQe60Y/edit?usp=sharing");
	}

	void OpenLink(string url)
	{
		try
		{
			System.Diagnostics.Process.Start(url);
		}
		catch
		{
			if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
			{
				url = url.Replace("&", "^&");
				System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(url) { UseShellExecute = true });
			}
			else if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux))
			{
				System.Diagnostics.Process.Start("xdg-open", url);
			}
			else if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.OSX))
			{
				System.Diagnostics.Process.Start("open", url);
			}
			else
			{
				throw;
			}
		}
	}

	void OnFolderSelected(string folder)
	{
		FolderPath.Text = folder;
		confirmationPatch.Visible = true;
	}

	void OnNameSet(string name)
	{
		if (name == null || name == "")
			FileNameSet.Text = FileNameSet.PlaceholderText;
	}

	void OnPatch()
	{
		StartPatch.Visible = false;
		confirmationPatch.Visible = true;
	}

	void _on_folderButton_pressed()
	{
		confirmationPatch.Visible = false;
		selectFolder.Visible = true;
	}

	void _on_choose_folder_canceled()
	{
		selectFolder.Visible = false;
		confirmationPatch.Visible = true;
	}

	void OnPatchConfirmed()
	{
		if (FileNameSet.Text != null && FolderPath.Text != null && FileNameSet.Text != "" && FolderPath.Text != "")
			CreatePatchedFile(FolderPath.Text, FileNameSet.Text);
		else if (FolderPath.Text != null && FolderPath.Text != "")
			CreatePatchedFile(FolderPath.Text, FileNameSet.PlaceholderText);
		else
			OnPatchCanceled();
	}

	void OnPatchCanceled()
	{
		StartPatch.Visible = true;
		confirmationPatch.Visible = false;
	}

	void _on_patching_loader_confirmed()
	{
		parent_script.SetPatchingData(fileDirectory, newFilePath);
		parent_script.SetRandomizerTransference(filth, currentViceDifficulty == ViceDifficulty.HARDCORE, trueHardcore, ultraHardcore, merit, removeTechBoost, easyStart, tanemon, rookieOnly);
		this.QueueFree();
	}

	void _on_patching_loader_canceled() { ExitViceInstaller(); }

	void HandleError()
	{
		PatchingWait.Visible = false;
		patchingLoading.Text = Tr("Patching");
		StartPatch.Visible = true;
	}

	void SetError()
	{
		patchingLoading.Text = "Error";
		if (bin != null)
		{
			bin.Close();
			bin.Dispose();
		}
	}

	public void RestartBoolsInstaller()
	{
		myotismon = vermillimon = filth = restoreFilth = superHardcore = hardMono = hardTourney = betterBattleTech = betterBrainTechs = betterDrop
		= superBonus = ultraBonus = dirtReduction = sDirtReduction = shortIntro = statsGains = multipleTechs = evoItem = tanemon = rookieOnly
		= helpfulItems = lessMono = curlingRandomizer = betterRestaurant = progression = curling = raise = itemSpawns = usefulItems2 =
		drimogemon = fishing = merit = cards = nerfIce = insaneDamage = trainingBoost = trueHardcore = noOrders = medals = rareSpawns = seadramon =
		hyperMono = extraInput = removeEvoInfo = originalType = newMono = realMetal = vendingMachine = easyStart = moreItemDrops = MoreItemSpawn = false;

		insaneBattle = restoreLifetime = removeTechBoost = unlockAreas = mapColour = ultraHardcore = DropItemsBattle = removeEffectBoost = nerfEffectBoost = removeNewEffects = 
		restorePanjyamon = kunemon = starters2 = removeTelephone = easyTechs = BlackWere = quickText = digitalClock = noRNG = boostItems = nerfTechBoost = poisonTimer =
		allBattles = removeExtraInfo = moreEffects = false;

		currentViceDifficulty = ViceDifficulty.NONE;
	}


	public void SetMyotismon(bool enabled) { myotismon = enabled; }
	public void SetViceDifficulty(ViceDifficulty difficulty) { currentViceDifficulty = difficulty; }
	public ViceDifficulty GetViceDifficulty() { return currentViceDifficulty; }
	public void SetFilth(bool enabled) { filth = enabled; }
	public void SetRestoreFilth(bool enabled) { restoreFilth = enabled; }
	public void SetVermillimon(bool enabled) { vermillimon = enabled; }
	public void SetSuperHardcore(bool enabled) { superHardcore = enabled; }
	public void SetHardMono(bool enabled) { hardMono = enabled; }
	public void SetHardTourney(bool enabled) { hardTourney = enabled; }
	public void SetBetterBattleTech(bool enabled) { betterBattleTech = enabled; }
	public void SetBetterBrainTech(bool enabled) { betterBrainTechs = enabled; }
	public void SetBetterDrops(bool enabled) { betterDrop = enabled; }
	public void SetSuperBonus(bool enabled) { superBonus = enabled; }
	public void SetUltraBonus(bool enabled) { ultraBonus = enabled; }
	public void SetSDirtReduction(bool enabled) { sDirtReduction = enabled; }
	public void SetDirtReduction(bool enabled) { dirtReduction = enabled; }
	public void SetMapColour(bool enabled) { mapColour = enabled; }
	public void SetShortIntro(bool enabled) { shortIntro = enabled; }
	public void SetStatsGains(bool enabled) { statsGains = enabled; }
	public void SetMultipleTechs(bool enabled) { multipleTechs = enabled; }
	public void SetEvoItem(bool enabled) { evoItem = enabled; }
	public void SetHelpfulItems(bool enabled) { helpfulItems = enabled; }
	public void SetLessMono(bool enabled) { lessMono = enabled; }
	public void SetCurlingRandomizer(bool enabled) { curlingRandomizer = enabled; }
	public void SetInsaneBattle(bool enabled) { insaneBattle = enabled; }
	public void SetRestoreLifetime(bool enabled) { restoreLifetime = enabled; }
	public void SetRemoveTechBoost(bool enabled) { removeTechBoost = enabled; }
	public void SetUnlockAreas(bool enabled) { unlockAreas = enabled; }
	public void SetUltraHardcore(bool enabled) { ultraHardcore = enabled; }
	public bool GetUltraHardcore() { return ultraHardcore; }
	public void SetRestaurant(bool enabled) { betterRestaurant = enabled; }
	public void SetCurling(bool enabled) { curling = enabled; }
	public void SetRaise(bool enabled) { raise = enabled; }
	public void SetCards(bool enabled) { cards = enabled; }
	public void SetMerit(bool enabled) { merit = enabled; }
	public void SetUseful2(bool enabled) { usefulItems2 = enabled; }
	public void SetDrimogemon(bool enabled) { drimogemon = enabled; }
	public void SetItemSpawns(bool enabled) { itemSpawns = enabled; }
	public void SetFishing(bool enabled) { fishing = enabled; }
	public void SetProgression(bool enabled) { progression = enabled; }
	public void SetRestorePanjya(bool enabled) { restorePanjyamon = enabled; }
	public void SetStarters2(bool enabled) { starters2 = enabled; }
	public void SetKunemon(bool enabled) { kunemon = enabled; }
	public void SetIceNerf(bool enabled) { nerfIce = enabled; }
	public void SetTrainingBoost(bool enabled) { trainingBoost = enabled; }
	public void SetInsaneDamage(bool enabled) { insaneDamage = enabled; }
	public void SetTelephone(bool enabled) { removeTelephone = enabled; }
	public void SetOrders(bool enabled) { noOrders = enabled; }
	public void SetSeadramon(bool enabled) { seadramon = enabled; }
	public void SetRareSpawns(bool enabled) { rareSpawns = enabled; }
	public void SetEasyMedals(bool enabled) { medals = enabled; }
	public void SetEasyTech(bool enabled) { easyTechs = enabled; }
	public void SetNewMono(bool enabled) { newMono = enabled; }
	public void SetOriginalType(bool enabled) { originalType = enabled; }
	public void Set8kMono(bool enabled) { hyperMono = enabled; }
	public void SetExtraInput(bool enabled) { extraInput = enabled; }
	public void SetEvoInfo(bool enabled) { removeEvoInfo = enabled; }
	public void SetTrueHardcore(bool enabled) { trueHardcore = enabled; }
	public void SetBWere(bool enabled) { BlackWere = enabled; }
	public void SetRMetal(bool enabled) { realMetal = enabled; }
	public void SetDigitalClock(bool enabled) { digitalClock = enabled; }
	public void SetQuickText(bool enabled) { quickText = enabled; }
	public void SetRNG(bool enabled) { noRNG = enabled; }
	public void SetEasyStart(bool enabled) { easyStart = enabled; }
	public void SetSuperBoostItems(bool enabled) { boostItems = enabled; }
	public void SetVendingMachines(bool enabled) { vendingMachine = enabled; }
	public void SetNerfBoost(bool enabled) { nerfTechBoost = enabled; }
	public void SetMoreDrops(bool enabled) { moreItemDrops = enabled; }
	public void SetMoreItemSpawn(bool enabled) { MoreItemSpawn = enabled; }
	public void SetRareSpawnValue(int value) { RareSpawnValue = value; }
	public void SetStatsValue(int value) { StatsValue = value; }

	public void SetRookie(bool enabled) { rookieOnly = enabled; }
	public void SetTanemon(bool enabled) { tanemon = enabled; }
	
	public void SetDropItems(bool enabled) { DropItemsBattle = enabled; }
	public void SetRemoveEffect(bool enabled) { removeEffectBoost = enabled; }
	public void SetNerfEffect(bool enabled) { nerfEffectBoost = enabled; }
	public void SetRemoveNewEffect(bool enabled) { removeNewEffects = enabled; }
	public void SetPoisonTimer(bool enabled) { poisonTimer = enabled; }
	public void SetAllBattles(bool enabled) { allBattles = enabled; }
	public void SetRemoveExtraInfo(bool enabled) { removeExtraInfo = enabled; }
	public void SetMoreEffects(bool enabled) { moreEffects = enabled; }

	public void CreatePatchedFile(string folderDestination, string newFilename)
	{
		confirmationPatch.Visible = false;
		PatchingWait.Visible = true;
		WarWait.Play();


		fileDirectory = folderDestination;
		newFilePath = System.IO.Path.Combine(folderDestination, newFilename);
		newFilePath = newFilePath + System.IO.Path.GetExtension(filePath);

		waiting.Start();

		WarWait.FrameChanged += CheckFrame;



	}

	public void InstallVicePatch()
	{
		string path = "Patches/ViceHack/";
		//main patch
		SetPatch(path + "DigimonWorldVice_2.2.ppf");

		//optional exclusive patches
		switch (currentViceDifficulty)
		{
			case ViceDifficulty.CHALLENGE:
				SetPatch(path + "ChallengePatch.ppf");
				break;
			case ViceDifficulty.HARDMODE:
				SetPatch(path + "ViceHardmode.ppf");
				break;
			case ViceDifficulty.HARDCORE:
				if (!progression)
					SetPatch(path + "HardcoreVice 1.3.ppf");
				else
					SetPatch(path + "HardcoreViceP 1.2.ppf");

				if (trueHardcore)
				{
					SetPatch(path + "TrueHardcoreVice 1.2.ppf");
					if (restorePanjyamon)
					{
						bin.Position = 0x9303F20;
						bin.WriteByte(0x2E);
						bin.Position = 0x9303F22;
						bin.WriteByte(0x32);
						bin.Position = 0x9303F24;
						bin.WriteByte(0x33);
						bin.Position = 0x9303F26;
						bin.WriteByte(0x38);
					}
					else if (BlackWere)
					{
						bin.Position = 0x9303F20;
						bin.WriteByte(0x31);
					}
				}
				break;
			default:
				break;
		}

		if (ultraHardcore)
		{
			if (currentViceDifficulty == ViceDifficulty.HARDCORE)
			{
				if (trueHardcore)
					SetPatch(path + "UltraHardcoreT.ppf");
				else
					SetPatch(path + "UltraHardcoreH.ppf");
			}
			else
				SetPatch(path + "UltraHardcoreEnabler.ppf");
		}

		InstallOptionalPatches();
	}

	public void ExtraInstall()
	{
		InstallOptionalPatches();
	}

	void InstallOptionalPatches()
	{
		string path = "Patches/OptionalPatches/";

		if (curlingRandomizer && restorePanjyamon)
			SetPatch("Patches/ViceHack/CurlingRandomizerViceP.ppf");
		else if (curlingRandomizer && !restorePanjyamon)
			SetPatch("Patches/ViceHack/CurlingRandomizerViceHack2.ppf");

		if (restorePanjyamon)
			SetPatch("Patches/ViceHack/RestorePanjyamon.ppf");

		if (starters2)
			SetPatch("Patches/ViceHack/Starter2.ppf");
		else if (kunemon)
			SetPatch("Patches/ViceHack/KunemonStart.ppf");

		if (myotismon)
			SetPatch("Patches/ViceHack/MyotismonPatchVice2.ppf");

		if (filth)
		{
			SetPatch("Patches/ViceHack/FilthChallengeViceHack 1.4.2.ppf");

			if (restoreFilth)
				SetPatch(path + "Difficulty patches/RestoreFilthDigimon.ppf");
		}
		else if (rookieOnly)
			SetPatch("Patches/ViceHack/RookieOnly.ppf");
		else if (tanemon)
			SetPatch("Patches/ViceHack/Tanemon.ppf");
		if (insaneBattle)
			SetPatch("Patches/ViceHack/InsaneBattle.ppf");

		if (restoreLifetime)
			SetPatch("Patches/ViceHack/RemoveExtraLifetimeEvo.ppf");

		if (removeTechBoost)
		{
			if (currentViceDifficulty == ViceDifficulty.NONE)
				SetPatch("Patches/ViceHack/RemoveTechBoost.ppf");
			else if (currentViceDifficulty != ViceDifficulty.CHALLENGE && !ultraHardcore)			
				SetPatch("Patches/ViceHack/RemovePartnerTechBoost.ppf");					
		}

		if (removeEffectBoost)
		{
			if (currentViceDifficulty == ViceDifficulty.NONE)
				SetPatch("Patches/ViceHack/RemoveBoostEffect.ppf");
			else if (currentViceDifficulty != ViceDifficulty.CHALLENGE && !ultraHardcore)			
				SetPatch("Patches/ViceHack/RemovePartnerEffectBoost.ppf");				
		}

		if (unlockAreas)
			SetPatch("Patches/ViceHack/FullyUnlockAreas.ppf");

		if (mapColour)
			SetPatch("Patches/ViceHack/BreakColour.ppf");

		if (removeTelephone)
			SetPatch("Patches/ViceHack/NoTelephoneOgre.ppf");

		if (easyTechs)
		{
			if (currentViceDifficulty != ViceDifficulty.HARDCORE)
				SetPatch("Patches/ViceHack/TelepathyBattle.ppf");
			else
				SetPatch("Patches/ViceHack/TelepathyBattleHard.ppf");
		}

		if (removeEvoInfo)
			SetPatch("Patches/ViceHack/RemoveEvoInfo.ppf");

		if (originalType)
			SetPatch("Patches/ViceHack/RestoreTypes.ppf");

		if (extraInput && !noRNG)
			SetPatch("Patches/ViceHack/ExtraInput.ppf");

		if (progression && currentViceDifficulty != ViceDifficulty.HARDCORE && !allBattles)
			SetPatch(path + "Difficulty patches/ProgressionPatch.ppf");
		
		if (allBattles && !trueHardcore)
		{
			if (currentViceDifficulty == ViceDifficulty.HARDCORE)
				SetPatch("Patches/ViceHack/HardcoreVice_AllBattles.ppf");
			else if (progression)
				SetPatch("Patches/ViceHack/AllBattlesP.ppf");
			else
				SetPatch("Patches/ViceHack/AllBattles.ppf");
        }

		if (BlackWere)
			SetPatch("Patches/ViceHack/BWereGaru.ppf");

		if (quickText)
			SetPatch("Patches/ViceHack/QuickBattleText.ppf");

		if (digitalClock)
			SetPatch("Patches/ViceHack/DigitalClock.ppf");

		if (noRNG)
		{
			if (extraInput)
				SetPatch("Patches/ViceHack/ExtraInputRNG.ppf");
			else
				SetPatch("Patches/ViceHack/noRNG.ppf");
		}
		if (removeExtraInfo)
			SetPatch("Patches/ViceHack/RemoveLifetimeInfo.ppf");

		if (boostItems)
			SetPatch("Patches/ViceHack/SuperDiskBuff.ppf");

		if (removeNewEffects)
			SetPatch("Patches/ViceHack/RemoveNewStatus.ppf");

		if (nerfTechBoost && !ultraHardcore)
			SetPatch("Patches/ViceHack/NerfTechBoost.ppf");
		
		if ((nerfEffectBoost || nerfTechBoost) && !ultraHardcore)
			SetPatch("Patches/ViceHack/NerfEffectBoost.ppf");	
		
		if (DropItemsBattle)
			SetPatch("Patches/ViceHack/DropItemsBattle.ppf");

		if (poisonTimer)
			SetPatch("Patches/ViceHack/NerfEffects.ppf");

		if (vermillimon)
			SetPatch(path + "MonochromonToVermilimon.ppf");

		if (realMetal)
			SetPatch(path + "RealMTGRVice.ppf");

		if (superHardcore)
			SetPatch(path + "Difficulty patches/SuperHardcoreEnabler.ppf");

		if (hardMono)
			SetPatch(path + "Difficulty patches/IncreaseMonochromonGoal.ppf");

		if (hardTourney)
			SetPatch(path + "Difficulty patches/MoreDifficultTournaments.ppf");

		if (hyperMono)
			SetPatch(path + "Difficulty patches/8KMono.ppf");

		if (betterBattleTech)
			SetPatch(path + "Tech patches/BetterBattleTechChances.ppf");

		if (betterBrainTechs)
			SetPatch(path + "Tech patches/BetterBrainsTechChances.ppf");

		if (multipleTechs)
			SetPatch(path + "Tech patches/LearnMoreTechs.ppf");

		if (nerfIce)
			SetPatch(path + "Tech patches/NerfIceStatue.ppf");

		if (insaneDamage)
			SetPatch(path + "Tech patches/InsaneDamage.ppf");

		if (noOrders)
			SetPatch(path + "Tech patches/BrainsNoOrders.ppf");

		if (betterDrop)
			SetPatch(path + "Useful patches/BetterDrops.ppf");
		else if (moreItemDrops)
			SetPatch(path + "Useful patches/MoreDrops.ppf");

		if (superBonus)
			SetPatch(path + "Useful patches/BonusTrySuperHelpful 1.2.ppf");
		else if (ultraBonus)
			SetPatch(path + "Useful patches/BonusTryUltraLucky 1.2.ppf");

		if (dirtReduction)
			SetPatch(path + "Useful patches/DirtReductionHalf.ppf");
		else if (sDirtReduction)
			SetPatch(path + "Useful patches/SuperDirtReduction.ppf");

		if (shortIntro)
			SetPatch(path + "Useful patches/ShortIntro.ppf");

		if (statsGains)
		{
			SetPatch(path + "Useful patches/NewStatsGains.ppf");
			if (StatsValue != 1)
				SetStatGains();
		}

		if (evoItem)
			SetPatch(path + "Useful patches/EnableStatGainsEvoItems.ppf");

		if (lessMono)
			SetPatch(path + "Useful patches/LowerMonochromonBits.ppf");

		if (betterRestaurant)
			SetPatch(path + "Useful patches/BetterRestaurant.ppf");

		if (drimogemon)
			SetPatch(path + "Useful patches/BetterDrimogemon.ppf");

		if (fishing)
			SetPatch(path + "Useful patches/BetterFishing.ppf");

		if (cards)
			SetPatch(path + "Useful patches/BetterCards.ppf");

		if (merit)
			SetPatch(path + "Useful patches/BetterMeritShop.ppf");

		if (usefulItems2)
			SetPatch(path + "Useful patches/UsefulItems2.ppf");

		if (curling)
			SetPatch(path + "Useful patches/BetterCurlingRewards.ppf");

		if (raise)
			SetPatch(path + "Useful patches/BetterRaise.ppf");
		

		if (itemSpawns)
			{
				SetPatch(path + "Useful patches/BetterItemSpawns.ppf");
				if (trueHardcore)
				{
					bin.Position = 0x13FFCEA1;
					bin.WriteByte(0);
					bin.Position = 0x13FFCEB7;
					bin.WriteByte(0);
					bin.Position = 0x13FFCECD;
					bin.WriteByte(0);
					bin.Position = 0x13FFCEA5;
					bin.WriteByte(49);
					bin.Position = 0x13FFCEBB;
					bin.WriteByte(49);
					bin.Position = 0x13FFCED1;
					bin.WriteByte(49);
				}
				bin.Position = 0x1401631F;
				bin.WriteByte(19);
			}

			else if (MoreItemSpawn)
			{
				SetPatch(path + "Useful patches/MoreItemSpawn.ppf");
				if (trueHardcore)
				{
					bin.Position = 0x13FFCEA1;
					bin.WriteByte(0);
					bin.Position = 0x13FFCEB7;
					bin.WriteByte(0);
					bin.Position = 0x13FFCECD;
					bin.WriteByte(0);
					bin.Position = 0x13FFCEA5;
					bin.WriteByte(29);
					bin.Position = 0x13FFCEBB;
					bin.WriteByte(29);
					bin.Position = 0x13FFCED1;
					bin.WriteByte(9);
				}
				bin.Position = 0x1401631F;
				bin.WriteByte(9);
			}

		if (helpfulItems)
			SetPatch(path + "Useful patches/HelpfulItems.ppf");

		if (trainingBoost)
			SetPatch(path + "Useful patches/TrainingBoostB.ppf");

		if (rareSpawns)
			SetRareSpawns();

		if (seadramon)
			SetPatch(path + "Useful patches/FullSeadramon.ppf");

		if (medals)
			SetPatch(path + "Useful patches/EasierMedals.ppf");

		if (newMono)
			SetPatch(path + "Useful patches/NewMonochromon.ppf");

		if (easyStart)
			SetPatch(path + "Useful patches/EasyStart.ppf");

		if (vendingMachine)
			SetPatch(path + "Useful patches/VendingMachines.ppf");

		if (moreEffects)
			SetPatch("Patches/ViceHack/EffectsCommon.ppf");

		bin.Close();
		bin.Dispose();
		warFrame = true;
	}


	void SetPatch(string patchPath)
	{
		try
		{
			if (OS.HasFeature("editor"))
			{
				patchPath = "res://" + patchPath;
				patchPath = ProjectSettings.GlobalizePath(patchPath);
				ppf = new System.IO.BinaryReader(System.IO.File.Open(patchPath, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite));
			}
			else
			{
				//var file = Godot.FileAccess.(patchPath, Godot.FileAccess.ModeFlags.ReadWrite);
				ppf = new System.IO.BinaryReader(new MemoryStream(Godot.FileAccess.GetFileAsBytes(patchPath)));
				if (ppf == null)
				{
					warError = true;
					return;
				}
			}

		}
		catch (System.IO.FileNotFoundException)
		{
			warError = true;
			return;
		}
		catch (System.IO.IOException)
		{
			warError = true;
			return;
		}


		ApplyPPF3Patch();
	}

	void ApplyPPF3Patch()
	{
		ppf.BaseStream.Position = 60;
		int offset, change;


		while (ppf.BaseStream.Position < ppf.BaseStream.Length)
		{
			offset = ppf.ReadInt32();
			ppf.ReadInt32();
			change = ppf.ReadByte();
			bin.Position = offset;

			while (change > 0)
			{
				bin.WriteByte(ppf.ReadByte());
				change--;
			}

		}
		ppf.Close();
		ppf.Dispose();
	}

	void SetRareSpawns()
	{
		RareSpawnValue--;
		//Piximon
		bin.Position = 0x13FD64AF;
		bin.WriteByte((byte)RareSpawnValue);
		bin.Position = 0x140B762F;
		bin.WriteByte((byte)RareSpawnValue);

		//Mamemon
		bin.Position = 0x13FD678F;
		bin.WriteByte((byte)RareSpawnValue);
		bin.Position = 0x140B790F;
		bin.WriteByte((byte)RareSpawnValue);

		//MetalMamemon
		bin.Position = 0x13FD831F;
		bin.WriteByte((byte)RareSpawnValue);
		bin.Position = 0x140B949F;
		bin.WriteByte((byte)RareSpawnValue);

		if (RareSpawnValue > 33)
		{
			//Otamamon
			bin.Position = 0x13FD7F47;
			bin.WriteByte((byte)RareSpawnValue);
			bin.Position = 0x140B90C7;
			bin.WriteByte((byte)RareSpawnValue);
		}

	}

	void SetStatGains()
	{
		bin.Position = 0x14D252F4;
		bin.WriteByte((byte)StatsValue);
		bin.Position = 0x14D252F8;
		for (int i = 0; i < statHexValues1.Length; i++)
		{
			bin.WriteByte(statHexValues1[i]);
		}

		bin.Position = 0x14D25458;
		for (int i = 0; i < statHexValues2.Length; i++)
		{
			bin.WriteByte(statHexValues2[i]);
		}
	}

	void CreateTxt()
	{
		string filename = "";

		switch (currentPatcher)
		{
			case patchType.VICEHACK:
				filename = Tr("NameVice_txt") + ".txt";
				break;
			case patchType.OPTIONAL:
				filename = Tr("NameOptional_txt") + ".txt";
				break;
		}
		string path = System.IO.Path.Combine(fileDirectory, filename);
		System.IO.Stream txt = System.IO.File.OpenWrite(path);

		System.IO.StreamWriter txtWritter = new System.IO.StreamWriter(txt);

		switch (currentPatcher)
		{
			case patchType.VICEHACK:
				txtWritter.Write(Tr("Title_txt"));
				txtWritter.WriteLine();
				txtWritter.WriteLine();
				txtWritter.Write(Tr("Difficulty_N"));
				txtWritter.WriteLine();
				switch (currentViceDifficulty)
				{
					case ViceDifficulty.CHALLENGE:
						txtWritter.Write("- " + Tr("Challenge_L"));
						break;
					case ViceDifficulty.HARDMODE:
						txtWritter.Write("- " + Tr("Hardmode_L"));
						break;
					case ViceDifficulty.HARDCORE:
						txtWritter.Write("- " + Tr("Hardcore_L"));
						break;
					default:
						break;
				}
				break;
			case patchType.OPTIONAL:
				txtWritter.Write(Tr("Optional_txt"));
				txtWritter.WriteLine();
				txtWritter.WriteLine();
				break;
		}

		txtWritter.WriteLine();

		if (trueHardcore)
		{
			txtWritter.Write("- " + Tr("THardcore_L"));
			txtWritter.WriteLine();
		}

		if (filth)
		{
			txtWritter.Write("- " + Tr("FilthChallenge_L"));
			txtWritter.WriteLine();
		}
		else if (rookieOnly)
		{
			txtWritter.Write("- " + Tr("Rookie_L"));
			txtWritter.WriteLine();
		}
		else if (tanemon)
		{
			txtWritter.Write("- " + Tr("Tanemon_L"));
			txtWritter.WriteLine();
		}

		if (superHardcore)
		{
			txtWritter.Write("- " + Tr("FairBattles_L"));
			txtWritter.WriteLine();
		}

		if (hardMono)
		{
			txtWritter.Write("- 4K Monochromon");
			txtWritter.WriteLine();
		}

		if (hardTourney)
		{
			txtWritter.Write("- " + Tr("Tournaments_L"));
			txtWritter.WriteLine();
		}

		if (ultraHardcore)
		{
			txtWritter.Write("- " + Tr("UHardcore_L"));
			txtWritter.WriteLine();
		}

		if (progression)
		{
			txtWritter.Write("- " + Tr("Progression_L"));
			txtWritter.WriteLine();
		}

		if (hyperMono)
		{
			txtWritter.Write("- 8K Monochromon");
			txtWritter.WriteLine();
		}

		if (noRNG)
		{
			txtWritter.Write("- " + Tr("RNG_L"));
			txtWritter.WriteLine();
		}

		if (allBattles)
		{
			txtWritter.Write("- " + Tr("AllBattles_L"));
			txtWritter.WriteLine();
		}

		txtWritter.WriteLine();
		txtWritter.Write(Tr("Digimon_N"));
		txtWritter.WriteLine();

		if (myotismon)
		{
			txtWritter.Write("- Myotismon Vice");
			txtWritter.WriteLine();
		}

		if (curlingRandomizer)
		{
			txtWritter.Write("- Curling randomizer");
			txtWritter.WriteLine();
		}

		if (vermillimon)
		{
			txtWritter.Write("- Vermillimon");
			txtWritter.WriteLine();
		}

		if (starters2)
		{
			txtWritter.Write("- " + Tr("Starters2_L"));
			txtWritter.WriteLine();
		}

		if (kunemon)
		{
			txtWritter.Write("- Kunemon");
			txtWritter.WriteLine();
		}

		if (realMetal)
		{
			txtWritter.Write("- Real MetalGreymon");
			txtWritter.WriteLine();
		}

		if (BlackWere)
		{
			txtWritter.Write("- Black WereGarurumon");
			txtWritter.WriteLine();
		}

		if (WarGreymon)
		{
			txtWritter.Write("- WarGreymon");
			txtWritter.WriteLine();
		}


		if (MetalGaru)
		{
			txtWritter.Write("- MetalGarurumon");
			txtWritter.WriteLine();
		}


		if (Tentomon)
		{
			txtWritter.Write("- Tentomon");
			txtWritter.WriteLine();
		}


		txtWritter.WriteLine();
		txtWritter.Write(Tr("Miscellaneous_N"));
		txtWritter.WriteLine();

		if (newMono)
		{
			txtWritter.Write("- " + Tr("NewMono_L"));
			txtWritter.WriteLine();
		}

		if (insaneBattle)
		{
			txtWritter.Write("- " + Tr("InsaneBattles_L"));
			txtWritter.WriteLine();
		}

		if (restoreLifetime)
		{
			txtWritter.Write("- " + Tr("OGLife_L"));
			txtWritter.WriteLine();
		}

		if (unlockAreas)
		{
			txtWritter.Write("- " + Tr("Areas_L"));
			txtWritter.WriteLine();
		}

		if (mapColour)
		{
			txtWritter.Write("- " + Tr("MapColour_L"));
			txtWritter.WriteLine();
		}


		if (removeTelephone)
		{
			txtWritter.Write("- " + Tr("OgreTel_L"));
			txtWritter.WriteLine();
		}

		if (originalType)
		{
			txtWritter.Write("- " + Tr("OGType_L"));
			txtWritter.WriteLine();
		}

		if (extraInput)
		{
			txtWritter.Write("- " + Tr("Input_L"));
			txtWritter.WriteLine();
		}

		if (removeEvoInfo)
		{
			txtWritter.Write("- " + Tr("RemoveEvo_L"));
			txtWritter.WriteLine();
		}

		if (quickText)
		{
			txtWritter.Write("- " + Tr("BattleText_L"));
			txtWritter.WriteLine();
		}

		if (digitalClock)
		{
			txtWritter.Write("- " + Tr("Clock_L"));
			txtWritter.WriteLine();
		}

		if (boostItems)
		{
			txtWritter.Write("- " + Tr("BoostItems_L"));
			txtWritter.WriteLine();
		}

		if (DropItemsBattle)
		{
			txtWritter.Write("- " + Tr("DropBattle_L"));
			txtWritter.WriteLine();
		}

		if (removeExtraInfo)
		{
			txtWritter.Write("- " + Tr("RemoveLifeEvo_L"));
			txtWritter.WriteLine();
		}


		txtWritter.WriteLine();
		txtWritter.Write(Tr("Useful_N"));
		txtWritter.WriteLine();

		if (betterDrop)
		{
			txtWritter.Write("- " + Tr("BetterItemDrops_L"));
			txtWritter.WriteLine();
		}
		else if (moreItemDrops)
		{
			txtWritter.Write("- " + Tr("MoreItemDrops_L"));
			txtWritter.WriteLine();
		}

		if (superBonus)
		{
			txtWritter.Write("- " + Tr("UsefulRigging_L"));
			txtWritter.WriteLine();
		}
		else if (ultraBonus)
		{
			txtWritter.Write("- " + Tr("UltraLucky_L"));
			txtWritter.WriteLine();
		}

		if (dirtReduction)
		{
			txtWritter.Write("- " + Tr("Dirt_L"));
			txtWritter.WriteLine();
		}
		else if (sDirtReduction)
		{
			txtWritter.Write("- " + Tr("SuperDirt_L"));
			txtWritter.WriteLine();
		}

		if (shortIntro)
		{
			txtWritter.Write("- " + Tr("ShortIntro_L"));
			txtWritter.WriteLine();
		}

		if (statsGains)
		{
			txtWritter.Write("- " + Tr("StatGains_L") + ": ");
			txtWritter.Write(StatsValue);
			txtWritter.Write("%");
			txtWritter.WriteLine();
		}

		if (evoItem)
		{
			txtWritter.Write("- " + Tr("EvoItem_L"));
			txtWritter.WriteLine();
		}

		if (helpfulItems)
		{
			txtWritter.Write("- " + Tr("Useful_L"));
			txtWritter.WriteLine();
		}

		if (lessMono)
		{
			txtWritter.Write("- " + Tr("LowMono_L"));
			txtWritter.WriteLine();
		}

		if (cards)
		{
			txtWritter.Write("- " + Tr("Cards_L"));
			txtWritter.WriteLine();
		}

		if (curling)
		{
			txtWritter.Write("- " + Tr("BetterCurling_L"));
			txtWritter.WriteLine();
		}

		if (drimogemon)
		{
			txtWritter.Write("- " + Tr("Treasure_L"));
			txtWritter.WriteLine();
		}

		if (fishing)
		{
			txtWritter.Write("- " + Tr("Fishing_L"));
			txtWritter.WriteLine();
		}

		if (itemSpawns)
		{
			txtWritter.Write("- " + Tr("BetterItemSpawns_L"));
			txtWritter.WriteLine();
		}
		else if (MoreItemSpawn)
		{
			txtWritter.Write("- " + Tr("MoreItemSpawns_L"));
			txtWritter.WriteLine();
		}

		if (merit)
		{
			txtWritter.Write("- " + Tr("Merit_L"));
			txtWritter.WriteLine();
		}

		if (raise)
		{
			txtWritter.Write("- " + Tr("Raise_L"));
			txtWritter.WriteLine();
		}

		if (betterRestaurant)
		{
			txtWritter.Write("- " + Tr("Restaurant_L"));
			txtWritter.WriteLine();
		}

		if (usefulItems2)
		{
			txtWritter.Write("- " + Tr("Useful2_L"));
			txtWritter.WriteLine();
		}

		if (trainingBoost)
		{
			txtWritter.Write("- " + Tr("TrainingBoost_L"));
			txtWritter.WriteLine();
		}

		if (medals)
		{
			txtWritter.Write("- " + Tr("Easymedals_L"));
			txtWritter.WriteLine();
		}

		if (seadramon)
		{
			txtWritter.Write("- " + Tr("Seadramon_L"));
			txtWritter.WriteLine();
		}

		if (rareSpawns)
		{
			txtWritter.Write("- " + Tr("RareSpawns_L") + ": ");
			txtWritter.Write(RareSpawnValue + 1);
			txtWritter.Write("%");
			txtWritter.WriteLine();
		}

		if (easyStart)
		{
			txtWritter.Write("- " + Tr("EasyStart_L"));
			txtWritter.WriteLine();
		}

		if (vendingMachine)
		{
			txtWritter.Write("- " + Tr("VendingMachines_L"));
			txtWritter.WriteLine();
		}

		txtWritter.WriteLine();
		txtWritter.Write(Tr("Techniques_N"));
		txtWritter.WriteLine();

		if (betterBattleTech)
		{
			txtWritter.Write("- " + Tr("BetterBattle_L"));
			txtWritter.WriteLine();
		}

		if (betterBrainTechs)
		{
			txtWritter.Write("- " + Tr("BetterBrains_L"));
			txtWritter.WriteLine();
		}

		if (multipleTechs)
		{
			txtWritter.Write("- " + Tr("LearnMulti_L"));
			txtWritter.WriteLine();
		}

		if (nerfIce)
		{
			txtWritter.Write("- " + Tr("NerfStatue_L"));
			txtWritter.WriteLine();
		}

		if (insaneDamage)
		{
			txtWritter.Write("- " + Tr("InsaneDamage_L"));
			txtWritter.WriteLine();
		}

		if (noOrders)
		{
			txtWritter.Write("- " + Tr("SkipOrders_L"));
			txtWritter.WriteLine();
		}

		if (easyTechs)
		{
			txtWritter.Write("- " + Tr("Telepathy_L"));
			txtWritter.WriteLine();
		}

		if (nerfTechBoost)
		{
			txtWritter.Write("- " + Tr("NerfTech_L"));
			txtWritter.WriteLine();
		}

		if (removeTechBoost)
		{
			txtWritter.Write("- " + Tr("RemoveTech_L"));
			txtWritter.WriteLine();
		}

		if (nerfEffectBoost)
		{
			txtWritter.Write("- " + Tr("NerfEffect_L"));
			txtWritter.WriteLine();
		}

		if (removeEffectBoost)
		{
			txtWritter.Write("- " + Tr("RemoveEffect_L"));
			txtWritter.WriteLine();
		}

		if (removeNewEffects)
		{
			txtWritter.Write("- " + Tr("RemoveNewEffect_L"));
			txtWritter.WriteLine();
		}

		if (poisonTimer)
		{
			txtWritter.Write("- " + Tr("PoisonTimer_L"));
			txtWritter.WriteLine();
		}

		if (moreEffects)
		{
			txtWritter.Write("- " + Tr("MoreEffects_L"));
			txtWritter.WriteLine();
		}

		txtWritter.Close();
		txtWritter.Dispose();
		txt.Close();
		txt.Dispose();



		WarFinish.Visible = true;
		WarWait.Visible = false;
		patchingLoading.Text = Tr("FinishedP");
		if (currentPatcher != patchType.OPTIONAL)
			PatchingWait.GetOkButton().Visible = true;
		PatchingWait.GetCancelButton().Text = Tr("ExitButton");
		SaveData();
		PatchingWait.Canceled -= HandleError;
		PatchingWait.Canceled += _on_patching_loader_canceled;


	}

	void OpenViceSave()
	{
		ViceSettings.Visible = true;
		difficultyContainer.Visible = false;
		digimonContainer.Visible = false;
		miscContainer.Visible = false;
		usefulContainer.Visible = false;
		techContainer.Visible = false;
		Difficulty.Disabled = false;
		Difficulty.ButtonPressed = false;
		Digimon.Disabled = false;
		Miscellaneous.Disabled = false;
		Miscellaneous.ButtonPressed = false;
		Useful.Disabled = false;
		Useful.ButtonPressed = false;
		Techniques.Disabled = false;
		Techniques.ButtonPressed = false;
		ViceMainSettings.Visible = true;
		ViceSave.Visible = false;
		ViceRecommended.Visible = false;
		OpenViceSettings.Disabled = true;
	}

	void SetSave()
	{
		ViceSaveConfirm.Visible = true;
	}

	void ConfirmSave()
	{
		string name;
		if (SaveName.Text != null && SaveName.Text != "")
			name = SaveName.Text;
		else
			name = SaveName.PlaceholderText;
		SaveData(name, saveNames[currentSave]);
		ViceSaveConfirm.Visible = false;
		ViceSave.Visible = false;
		ViceMainSettings.Visible = true;
	}

	void CloseSave()
	{
		ViceSaveConfirm.Visible = false;
		Saves[currentSave].SetPressedNoSignal(false);
	}

	void BackSaves()
	{
		ViceSave.Visible = false;
		ViceMainSettings.Visible = true;
	}

	void BackRecommended()
	{
		ViceRecommended.Visible = false;
		ViceMainSettings.Visible = true;
	}

	void OpenRecommended()
	{
		ViceMainSettings.Visible = false;
		ViceRecommended.Visible = true;
	}

	void SavePressed()
	{
		for (int i = 0; i < Saves.Length; i++)
		{
			Saves[i].SetPressedNoSignal(false);
			Saves[i].Disabled = false;
		}
		SaveLabel.Text = Tr("SaveSettingsLabel");

		ViceMainSettings.Visible = false;
		ViceSave.Visible = true;
	}

	void LoadPressed()
	{
		for (int i = 0; i < Saves.Length; i++)
		{
			Saves[i].SetPressedNoSignal(true);
			if (saveNames[i] == null)
				Saves[i].Disabled = true;
		}
		SaveLabel.Text = Tr("LoadSettingsLabel");

		ViceMainSettings.Visible = false;
		ViceSave.Visible = true;
	}

	void SaveData(string saveName = null, string oldName = null)
	{
		saveData = new ViceSaveData(currentViceDifficulty, myotismon, vermillimon, filth, restoreFilth, superHardcore, hardMono, hardTourney, betterBattleTech, betterBrainTechs, betterDrop,
		superBonus, tanemon, rookieOnly, ultraBonus, dirtReduction, sDirtReduction, shortIntro, statsGains, multipleTechs, evoItem, helpfulItems, lessMono, nerfIce,
		curlingRandomizer, betterRestaurant, progression, itemSpawns, raise, drimogemon, cards, merit, fishing, usefulItems2, curling,
		trainingBoost, insaneDamage, trueHardcore, noOrders, medals, seadramon, rareSpawns, hyperMono, extraInput, removeEvoInfo, originalType, newMono, realMetal,
		vendingMachine, easyStart, moreItemDrops, MoreItemSpawn, insaneBattle, restoreLifetime, removeTechBoost, unlockAreas, mapColour, ultraHardcore, WarGreymon, MetalGaru,
		Tentomon, restorePanjyamon, starters2, kunemon, removeTelephone, easyTechs, BlackWere, quickText, digitalClock, noRNG, boostItems, nerfTechBoost,
		StatsValue, RareSpawnValue, DropItemsBattle, nerfEffectBoost, removeEffectBoost, removeNewEffects, poisonTimer);

		Directory.CreateDirectory(OS.GetExecutablePath().GetBaseDir() + "/SaveData");

		var bytes = JsonSerializer.SerializeToUtf8Bytes(saveData);
		if (saveName != null)
		{
			string name;
			if (oldName != null)
				name = "/SaveData/" + oldName + ".ViceSave";
			else
				name = "/SaveData/" + saveName + ".ViceSave";

			using var saveFile = Godot.FileAccess.Open(OS.GetExecutablePath().GetBaseDir() + name, Godot.FileAccess.ModeFlags.Write);

			saveFile.StoreBuffer(bytes);
			saveFile.Close();
			if (oldName != null)
				File.Move(OS.GetExecutablePath().GetBaseDir() + name, OS.GetExecutablePath().GetBaseDir() + "/SaveData/" + saveName + ".ViceSave");

			saveNames[currentSave] = saveName;
			Saves[currentSave].Text = saveName;
		}
		else
		{
			using var saveFile = Godot.FileAccess.Open(OS.GetExecutablePath().GetBaseDir() + "/SaveData/LatestSave", Godot.FileAccess.ModeFlags.Write);
			saveFile.StoreBuffer(bytes);
			saveFile.Close();
		}


	}
	void LoadSaveData(string saveName = null)
	{
		string name;
		if (saveName == null)
			name = "/SaveData/LatestSave";
		else
			name = "/SaveData/" + saveName + ".ViceSave";

		if (File.Exists(OS.GetExecutablePath().GetBaseDir() + name))
		{
			using var saveFile = System.IO.File.Open(OS.GetExecutablePath().GetBaseDir() + name, System.IO.FileMode.Open);
			saveData = JsonSerializer.Deserialize<ViceSaveData>(saveFile);
			saveFile.Close();

			if (saveData != null)
			{

				digimonScript.LoadSaveData(saveData.myotismon, saveData.restorePanjyamon, saveData.vermillimon, saveData.starters2, saveData.kunemon, saveData.curlingRandomizer, saveData.realMetal,
				saveData.BlackWere, saveData.WarGreymon, saveData.Tentomon, saveData.MetalGaru);

				miscScript.LoadSaveData(saveData.digitalClock, saveData.extraInput, saveData.unlockAreas, saveData.quickText, saveData.boostItems, saveData.restoreLifetime,
				saveData.removeEvoInfo, saveData.newMono, saveData.insaneBattle, saveData.mapColour, saveData.removeTelephone, saveData.originalType, saveData.dropItemsBattle,
				saveData.removeExtraInfo);

				usefulScript.LoadSaveData(saveData.statsGains, saveData.rareSpawns, saveData.shortIntro, saveData.easyStart, saveData.superBonus, saveData.ultraBonus, saveData.trainingBoost,
				saveData.dirtReduction, saveData.sDirtReduction, saveData.drimogemon, saveData.lessMono, saveData.seadramon, saveData.evoItem, saveData.moreItemDrops, saveData.betterDrop,
				saveData.raise, saveData.MoreItemSpawn, saveData.itemSpawns, saveData.medals, saveData.vendingMachine, saveData.curling, saveData.betterRestaurant, saveData.cards, saveData.merit,
				saveData.fishing, saveData.helpfulItems, saveData.usefulItems2, saveData.StatsValue, saveData.RareSpawnValue);

				techScript.LoadSaveData(saveData.betterBattleTech, saveData.betterBrainTechs, saveData.multipleTechs, saveData.insaneDamage, saveData.easyTechs, saveData.noOrders,
				saveData.nerfIce, saveData.nerfTechBoost, saveData.removeTechBoost, saveData.removeNewEffects, saveData.nerfEffect, saveData.removeEffect, saveData.addTimer,
				saveData.moreEffects);

				difficultyScript.LoadSaveData(saveData.viceDifficulty == ViceDifficulty.CHALLENGE, saveData.viceDifficulty == ViceDifficulty.HARDMODE, saveData.viceDifficulty == ViceDifficulty.HARDCORE,
				saveData.trueHardcore, saveData.ultraHardcore, saveData.progression, saveData.filth, saveData.restoreFilth, saveData.superHardcore, saveData.hardTourney, saveData.noRNG,
				saveData.hardMono, saveData.hyperMono, saveData.rookieOnly, saveData.tanemon, saveData.allBattles);
			}
		}

		ViceSave.Visible = false;
		ViceMainSettings.Visible = true;
	}

	void CheckSaveFiles()
	{
		if (Directory.Exists(OS.GetExecutablePath().GetBaseDir() + "/SaveData"))
		{
			int saves = 0;
			foreach (string file in DirAccess.GetFilesAt(OS.GetExecutablePath().GetBaseDir() + "/SaveData"))
			{
				if (file.GetExtension() == "ViceSave")
				{
					saveNames[saves] = file.GetBaseName();
					saves++;
					if (saves > 7)
						break;
				}
			}
		}

		for (int i = 0; i < saveNames.Length; i++)
		{
			if (saveNames[i] != null)
				Saves[i].Text = saveNames[i];
		}

		if (!File.Exists(OS.GetExecutablePath().GetBaseDir() + "/SaveData/LatestSave"))
			LatestOptions.Disabled = true;

	}

	void RecommendedVice()
	{
		digimonScript.LoadSaveData(false, false, false, false, false, true, true, false, false, false, false);

		miscScript.LoadSaveData(true, true, false, false, true, false, false, true, false, false, false, false, false, false);

		usefulScript.LoadSaveData(true, false, true, false, false, false, false, true, false, true, false, false, false, true, false, false, true, false, false, true,
		true, true, false, false, true, true, true, 1, 10);

		techScript.LoadSaveData(true, true, true, false, false, false, true, false, false, false, false, false, false, false);

		difficultyScript.LoadSaveData(false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);

		ViceRecommended.Visible = false;
		ViceMainSettings.Visible = true;
	}

	void EasyVice()
	{
		digimonScript.LoadSaveData(true, false, false, false, false, false, true, false, false, false, false);

		miscScript.LoadSaveData(true, true, true, false, true, false, false, true, false, false, true, false, false, false);

		usefulScript.LoadSaveData(true, true, true, true, false, true, true, false, true, true, true, true, true, false, true, true, false, true, true, true, true, true, true, true,
		true, true, true, 20, 100);

		techScript.LoadSaveData(true, true, true, false, true, true, false, false, false, false, false, false, false, false);

		difficultyScript.LoadSaveData(false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);

		ViceRecommended.Visible = false;
		ViceMainSettings.Visible = true;
	}

	void Hardcore()
	{
		digimonScript.LoadSaveData(false, false, false, false, false, false, true, false, false, false, false);

		miscScript.LoadSaveData(true, true, false, false, true, false, false, true, false, false, false, false, false, false);

		usefulScript.LoadSaveData(true, false, true, false, false, false, false, true, false, true, false, false, false, true, false, false, true, false, false, true,
		true, true, false, false, true, true, true, 1, 10);

		techScript.LoadSaveData(true, true, true, false, false, true, false, false, false, false, false, false, false, false);

		difficultyScript.LoadSaveData(false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false);

		ViceRecommended.Visible = false;
		ViceMainSettings.Visible = true;
	}

	void UltraHardcore()
	{

		digimonScript.LoadSaveData(true, false, false, false, false, false, true, true, false, false, false);

		miscScript.LoadSaveData(true, true, false, false, true, false, false, true, false, false, false, false, false, false);

		usefulScript.LoadSaveData(true, false, true, false, false, false, false, true, false, true, false, false, false, true, false, false, true, false, false, true,
		true, true, false, false, true, true, true, 1, 10);

		techScript.LoadSaveData(false, false, false, false, false, false, true, false, false, false, false, false, false, false);

		difficultyScript.LoadSaveData(false, false, true, false, true, false, false, false, false, true, true, true, false, false, false, false);

		ViceRecommended.Visible = false;
		ViceMainSettings.Visible = true;
	}

	void SpeedrunMode()
	{
		digimonScript.LoadSaveData(true, false, false, false, false, false, true, false, false, false, false);

		miscScript.LoadSaveData(true, true, true, true, true, false, false, true, false, false, true, false, true, false);

		usefulScript.LoadSaveData(true, true, true, true, false, true, true, false, true, true, true, true, true, false, true, true, false, true, true, true, true, true, true, true,
		true, true, true, 20, 100);

		techScript.LoadSaveData(true, true, true, false, true, true, false, false, false, false, false, false, false, false);

		difficultyScript.LoadSaveData(false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);

		ViceRecommended.Visible = false;
		ViceMainSettings.Visible = true;
	}

	void SpeedHardcore()
	{
		digimonScript.LoadSaveData(true, false, false, false, false, false, true, false, false, false, false);

		miscScript.LoadSaveData(true, true, true, true, true, false, false, true, false, false, true, false, true, false);

		usefulScript.LoadSaveData(true, true, true, true, false, false, true, false, true, true, true, true, true, false, true, true, false, true, true, true, true, true, true, true,
		true, true, true, 10, 100);

		techScript.LoadSaveData(true, true, true, false, true, true, false, false, false, false, false, false, false, false);

		difficultyScript.LoadSaveData(false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false);

		ViceRecommended.Visible = false;
		ViceMainSettings.Visible = true;
	}

	void VanillaMode()
	{
		digimonScript.LoadSaveData(false, true, false, false, false, false, false, false, false, false, false);

		miscScript.LoadSaveData(false, false, false, false, false, true, true, false, false, false, false, true, false, true);

		usefulScript.LoadSaveData(false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
		false, false, false, false, false, false, false, 1, 3);

		techScript.LoadSaveData(false, false, false, false, false, false, false, false, true, false, true, true, false, false);

		difficultyScript.LoadSaveData(false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);

		ViceRecommended.Visible = false;
		ViceMainSettings.Visible = true;
	}

	void MasochistMode()
	{
		digimonScript.LoadSaveData(false, false, false, false, false, false, false, true, false, false, false);

		miscScript.LoadSaveData(false, false, false, false, false, true, true, false, true, true, false, false, false, true);

		usefulScript.LoadSaveData(false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
		false, false, false, false, false, false, false, 1, 1);

		techScript.LoadSaveData(false, false, false, true, false, false, false, false, true, false, false, false, false, false);

		difficultyScript.LoadSaveData(false, false, true, true, true, true, false, false, false, true, true, false, true, false, false, false);

		ViceRecommended.Visible = false;
		ViceMainSettings.Visible = true;
	}

	void LoadLatest() { LoadSaveData(); }

	void StartWaiting()
	{
		warError = false;
		System.IO.File.Copy(filePath, newFilePath, true);
		try
		{
			bin = System.IO.File.OpenWrite(newFilePath);
		}
		catch (System.ArgumentException)
		{
			warError = true;
		}
		catch (System.IO.FileNotFoundException)
		{
			warError = true;
		}
		catch (System.IO.IOException)
		{
			warError = true;
		}

		switch (currentPatcher)
		{
			case patchType.VICEHACK:
				InstallVicePatch();
				break;
			case patchType.OPTIONAL:
				ExtraInstall();
				break;
		}
	}

	void CheckFrame()
	{
		if (warFrame)
		{
			WarWait.FrameChanged -= CheckFrame;
			CreateTxt();
		}
		else if(warError)
		{
			WarWait.FrameChanged -= CheckFrame;
			SetError();
		}
	}
}
