using Godot;
using System;


namespace PatcherData
{
	public enum patchType
	{
		VICEHACK = 0,
		OPTIONAL = 1
	}
}

public partial class Base_script : Node2D
{
	string filePath, newFilePath, fileDirectory;

	[Export]
	private Button viceInstaller;

	[Export]
	private Button randomizer;

	[Export]
	private Button optionalInstaller;

	[Export]
	private SubViewportContainer BaseContainer;

	[Export]
	private FileDialog fileSearch;

	[Export]
	private TextureButton Settings;

	[Export]
	private Label ViceExplain;

	[Export]
	private RichTextLabel ViceInfo;
	[Export]
	private TextureButton Spreadsheet;

	[Export]
	private Label LanguageText;

	[Export]
	private Button Credits;

	[Export]
	private TextureButton Support;

	[Export]
	private Label WindowSize;

	[Export]
	private Button WindowSizeNormal;

	[Export]
	private Button WindowSizeBig;

	[Export]
	private Label CreditsTitle;

	[Export]
	private RichTextLabel CreditsInfo;

	[Export]
	private SubViewportContainer ViceInfoContainer;

	[Export]
	private SubViewportContainer SettingsContainer;

	[Export]
	private SubViewportContainer CreditsContainer;

	[Export]
	private Button Start;

	[Export]
	private TextureButton CloseSettings;

	[Export]
	private Label Disclaimer;

	[Export]
	private Button Tools;

	int installerSelected = -1;
	int screenX = 1800, screenY = 950;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (DisplayServer.ScreenGetSize().Y < 900 || DisplayServer.ScreenGetSize().X < 1800)
		{
			DisplayServer.WindowSetSize(new Vector2I(900, 425));
			//DisplayServer.WindowSetPosition(new Vector2I(0,25));
			this.GetWindow().MoveToCenter();
			screenX = 900; screenY = 425;
		}
		viceInstaller.Pressed += SetViceInstaller;
		optionalInstaller.Pressed += SetOptionalInstaller;
		randomizer.Pressed += SetRandomizer;
		Settings.Pressed += OpenSettings;
		Credits.Pressed += OpenCredits;
		Support.Pressed += OpenSupport;
		Spreadsheet.Pressed += OpenSpreadsheet;
		Start.Pressed += GoToMainMenu;
		CloseSettings.Pressed += ExitSettings;
		Tools.Pressed += ToolsPressed;
		fileSearch.FileSelected += OpenProgram;

		viceInstaller.Text = Tr("ViceButton");
		viceInstaller.TooltipText = Tr("ViceButton_info");
		optionalInstaller.Text = Tr("OptionalButton");
		optionalInstaller.TooltipText = Tr("OptionalButton_info");
		randomizer.Text = Tr("RandoButton");
		randomizer.TooltipText = Tr("RandoButton_info");
		Settings.TooltipText = Tr("Settings");
		ViceInfo.Text = Tr("VICE");
		LanguageText.Text = Tr("LanguageS");
		Credits.Text = Tr("Credits");
		CreditsTitle.Text = Tr("Credits");
		WindowSize.Text = Tr("SizeS");
		ViceExplain.Text = Tr("WhatVice");
		CreditsInfo.Text = Tr("Credits_text");
		Start.Text = Tr("StartInstaller_L");
		Disclaimer.Text = Tr("Disclaimer_L");
		Tools.Text = Tr("ToolsL");
		Tools.TooltipText = Tr("ToolsInfo");

		WindowSizeNormal.Text = Tr("WindowS");
		WindowSizeBig.Text = Tr("WindowL");

		TranslationServer.SetLocale("EN");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	void SetViceInstaller()
	{
		installerSelected = 0;
		fileSearch.Visible = true;
	}

	void SetOptionalInstaller()
	{
		installerSelected = 2;
		fileSearch.Visible = true;
	}

	void SetRandomizer()
	{
		installerSelected = 1;
		fileSearch.Visible = true;
	}

	void OpenProgram(string path)
	{
		switch(installerSelected)
		{
			case 0:
				OpenViceInstaller(path);
				break;
			case 1:
				OpenRandomizer(path);
				break;
			case 2:
				OpenOptionalInstaller(path);
				break;
			default:
				break;
		}

	}

	void OpenViceInstaller(string file)
	{
		var scene = GD.Load<PackedScene>("res://Scenes/VicePatcher.tscn");
		var patcher = scene.Instantiate() as VicePatcherContainer;
		fileSearch.Visible = false;
		BaseContainer.Visible = false;
		filePath = file;
		patcher.SetStartingData(PatcherData.patchType.VICEHACK, filePath, this);
		this.AddChild(patcher);
	}

	void OpenOptionalInstaller(string file)
	{
		var scene = GD.Load<PackedScene>("res://Scenes/VicePatcher.tscn");
		var patcher = scene.Instantiate() as VicePatcherContainer;
		fileSearch.Visible = false;
		BaseContainer.Visible = false;
		filePath = file;
		patcher.SetStartingData(PatcherData.patchType.OPTIONAL, filePath, this);
		this.AddChild(patcher);
	}

	void OpenRandomizer(string file)
	{
		var scene = GD.Load<PackedScene>("res://Scenes/SceneRandomizer.tscn");
		var patcher = scene.Instantiate() as RandomizerContainer;
		fileSearch.Visible = false;
		BaseContainer.Visible = false;
		filePath = file;
		patcher.SetBasicData(filePath, this);
		this.AddChild(patcher);
	}

	void OpenSettings()
	{
		BaseContainer.Visible = false;
		SettingsContainer.Visible = true;
	}

	void OpenCredits()
	{
		SettingsContainer.Visible = false;
		CreditsContainer.Visible = true;
	}

	void OpenViceInfo()
	{
		ViceInfo.Text = Tr("VICE");
		BaseContainer.Visible = false;
		ViceInfoContainer.Visible = true;
	}

	void SelectedLanguage(int selected)
	{
		switch (selected)
		{
			case 0:
				TranslationServer.SetLocale("EN");
				break;
			case 1:
				TranslationServer.SetLocale("ESP");
				break;
		}
	}

	void SetWindowsSizeNormal()
	{
		DisplayServer.WindowSetSize(new Vector2I(screenX, screenY));
		this.GetWindow().MoveToCenter();
	}

	void SetWindowSizeBig()
	{
		DisplayServer.WindowSetSize(new Vector2I(DisplayServer.ScreenGetSize().X - 100, DisplayServer.ScreenGetSize().Y - 100));
		this.GetWindow().MoveToCenter();
	}

	void ExitViceInfo()
	{
		ViceInfoContainer.Visible = false;
		BaseContainer.Visible = true;
	}

	void ExitCredits()
	{
		CreditsContainer.Visible = false;
		SettingsContainer.Visible = true;
	}

	void ExitSettings()
	{
		SettingsContainer.Visible = false;
		BaseContainer.Visible = true;
	}

	public void SetFilePath(string path) { filePath = path; }

	public void SetVisibleAgain() { BaseContainer.Visible = true; }

	public void SetPatchingData(string folder, string newFile) { fileDirectory = folder; newFilePath = newFile; }
	public void SetRandomizerTransference(bool filthR = false, bool hardcoreR = false, bool trueHardcoreR = false, bool ultraHardcoreR = false, bool meritR = false, bool removeTechBoostR = false, bool easyStartR = false, bool tanemon = false, bool rookie = false)
	{
		var scene = GD.Load<PackedScene>("res://Scenes/SceneRandomizer.tscn");
		var patcher = scene.Instantiate() as RandomizerContainer;
		patcher.SetStartingData(filthR, hardcoreR, trueHardcoreR, ultraHardcoreR, meritR, removeTechBoostR, easyStartR, tanemon, rookie, fileDirectory, newFilePath, this);
		this.AddChild(patcher);
	}

	void OpenSupport() { OpenLink("https://buymeacoffee.com/dwvice04"); }

	void OpenSpreadsheet() { OpenLink("https://docs.google.com/spreadsheets/d/1lG3aLJsLiCwcZXo5-OS18o21GngTVuyAiKA0liV_kpM/edit?usp=sharing"); }

	void GoToMainMenu()
	{
		Start.Visible = false;
		Credits.Visible = true;
		CloseSettings.Visible = true;
		ExitSettings();
	}

	void ToolsPressed()
	{
		var scene = GD.Load<PackedScene>("res://Scenes/ToolsScene.tscn");
		var Tools = scene.Instantiate() as ToolsHandler;
		fileSearch.Visible = false;
		BaseContainer.Visible = false;
		Tools.SetParent(this);
		this.AddChild(Tools);
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
}
