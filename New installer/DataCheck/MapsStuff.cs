using Godot;
using System;

public partial class MapsStuff : Control
{
	[Export] Button[] mapButtons;
	[Export] Texture2D[] mapImages;

	[Export] Control Buttons;

	[Export] Control ImageMap;

	[Export] TextureRect currentMap;

	[Export] Button back;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
        /*for(int i = 0; i < mapButtons.Length; i++)
        {
            int cat = i;
			mapButtons[i].Pressed += () => {ButtonPressed(cat);};
        }
		back.Text = Tr("BackButtonSave");
		back.Pressed += CloseMap;*/
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	void ButtonPressed(int value)
    {
        Buttons.Visible = false;
		currentMap.Texture = mapImages[value];
		ImageMap.Visible = true;
    }

	public void CloseMap()
    {
		ImageMap.Visible = false;
        Buttons.Visible = true;		
    }
}
