using Godot;
using System;
using System.Collections.Generic;

public partial class DigimonTournament : Control
{
	[Export] Panel Border;
	[Export] TextureRect OGImage;

	public void Setup(List<int> digimons, DataCheck parent, bool vice, bool part)
	{
		int size = digimons.Count / 3 * 50 + 50;
		Border.Size = new Vector2(150, size);
		this.CustomMinimumSize = new Vector2(146, size);
		if (digimons.Count > 0)
		{
			OGImage.Texture = parent.GetDigimonData(digimons[0]).digimonSprite;
			OGImage.TooltipText = parent.GetDigimonData(digimons[0]).name;
			if (digimons.Count > 1)
			{
				for (int i = 1; i < digimons.Count; i++)
				{
					int digi = digimons[i];
					if (digi == 62 && part)
						digi = 115;
					int posX = i % 3 * 50 + 5;
					int posY = i / 3 * 50 + 5;
					TextureRect DigimonIcon = OGImage.Duplicate() as TextureRect;
					DigimonIcon.Texture = parent.GetDigimonData(digi).digimonSprite;
					DigimonIcon.TooltipText = parent.GetDigimonData(digi).name;
					Border.AddChild(DigimonIcon);
					DigimonIcon.Position = new Vector2(posX, posY);

				}
			}
		}
		else
		{
			size = 111 / 3 * 50 + 50;
			Border.Size = new Vector2(150, size);
			this.CustomMinimumSize = new Vector2(146, size);
			if (vice)
			{
				OGImage.Texture = parent.GetDigimonData(115).digimonSprite;
				OGImage.TooltipText = parent.GetDigimonData(115).name;
			}
			else
			{
				OGImage.Texture = parent.GetDigimonData(0).digimonSprite;
				OGImage.TooltipText = parent.GetDigimonData(0).name;
			}
			for (int i = 1; i < 112; i++)
			{
				int posX = i % 3 * 50 + 5;
				int posY = i / 3 * 50 + 5;
				TextureRect DigimonIcon = OGImage.Duplicate() as TextureRect;
				DigimonIcon.Texture = parent.GetDigimonData(i).digimonSprite;
				DigimonIcon.TooltipText = parent.GetDigimonData(i).name;
				Border.AddChild(DigimonIcon);
				DigimonIcon.Position = new Vector2(posX, posY);
			}
		}	
	}
}
