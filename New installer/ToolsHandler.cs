using Godot;
using System;

public partial class ToolsHandler : SubViewportContainer
{
	[Export] Control Main;
	[Export] Control evoTool;
	[Export] EvolutionCalculator evoScript;
	[Export] FileDialog SelectFile;
	[Export] AtlasTexture DigimonSprites;
	[Export] AtlasTexture ExtraDigimonSprites;
	[Export] Button EvoCalc;
	[Export] Button Bingo;
	[Export] Button GameCheck;
	Base_script parent_script;

	public override void _Ready()
	{
		EvoCalc.Text = Tr("EvoCalculatorL");
		EvoCalc.TooltipText = Tr("EvoCalculator");
		Bingo.Text = Tr("BingoL");
		Bingo.TooltipText = Tr("BingoInfo");
		GameCheck.Text = Tr("GameCheckerL");
		GameCheck.TooltipText = Tr("GameChecker");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetParent(Base_script OGScript)
	{
		parent_script = OGScript;
	}

	void OpenFileDialog()
	{
		SelectFile.Visible = true;
	}

	void StartEvoTool(string path)
	{
		SelectFile.Visible = false;
		Main.Visible = false;
		evoTool.Visible = true;
		evoScript.StartEvoCalculator(path, this);
	}

	void ExitTools()
	{
		parent_script.SetVisibleAgain();
		this.QueueFree();
	}

	void ExitEvoTool()
	{
		Main.Visible = true;
		evoTool.Visible = false;
	}

	public Texture2D GetDigimonTexture(float posX, float posY)
	{
		DigimonSprites.Region = new Rect2(posX, posY, 32, 32);
		return ImageTexture.CreateFromImage(DigimonSprites.GetImage());
	}

	public Texture2D GetDigimonExtraTexture(float posX, float posY)
	{
		ExtraDigimonSprites.Region = new Rect2(posX, posY, 32, 32);
		return ImageTexture.CreateFromImage(ExtraDigimonSprites.GetImage());
	}
}
