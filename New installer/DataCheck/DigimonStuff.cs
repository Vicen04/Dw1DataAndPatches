using Godot;
using System;
public partial class DigimonStuff : Control
{
	[Export] Button NPCActive;
	[Export] Button BossesActive;
	[Export] Button TournamentActive;
	[Export] Button RecruitActive;
	[Export] Bosses2 NPCs;
	[Export] Bosses2 Bosses;
	[Export] TournamentsData Tournaments;
	[Export] Recruits Recruitments;
	[Export] TechStuff techInfo;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		NPCActive.Text = Tr("NPCsSearch");
		BossesActive.Text = Tr("BossesSearch");
		TournamentActive.Text = Tr("TournamentNPCsSearch");
		RecruitActive.Text = Tr("RecruitSearch");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetupData(System.IO.Stream bin, System.IO.BinaryReader reader, DataCheck parent, bool vice)
	{
		NPCs.SetupBosses(bin, reader, parent, this, vice, false);	
		Bosses.SetupBosses(bin, reader, parent, this, vice, true);	
		Recruitments.SetupData(bin, reader, parent, vice);
		Tournaments.SetupTournaments(bin, reader, parent, vice);
		this.Visible = false;
	}

	void NPCPressed()
	{
		NPCActive.Disabled = true;
		BossesActive.Disabled = false;
		TournamentActive.Disabled = false;
		RecruitActive.Disabled = false;
		NPCs.Visible = true;
		Bosses.Visible = false;
		Tournaments.Visible = false;
		Recruitments.Visible = false;
	}

	void BossesPressed()
	{
		NPCActive.Disabled = false;
		BossesActive.Disabled = true;
		TournamentActive.Disabled = false;
		RecruitActive.Disabled = false;
		NPCs.Visible = false;
		Bosses.Visible = true;
		Tournaments.Visible = false;
		Recruitments.Visible = false;
	}

	void TournamentPressed()
	{
		NPCActive.Disabled = false;
		BossesActive.Disabled = false;
		TournamentActive.Disabled = true;
		RecruitActive.Disabled = false;
		NPCs.Visible = false;
		Bosses.Visible = false;
		Tournaments.Visible = true;
		Recruitments.Visible = false;
	}

	void RecruitPressed()
	{
		NPCActive.Disabled = false;
		BossesActive.Disabled = false;
		TournamentActive.Disabled = false;
		RecruitActive.Disabled = true;
		NPCs.Visible = false;
		Bosses.Visible = false;
		Tournaments.Visible = false;
		Recruitments.Visible = true;
	}

	public void RestartData()
	{
		NPCActive.Disabled = false;
		BossesActive.Disabled = false;
		TournamentActive.Disabled = false;
		RecruitActive.Disabled = false;
		NPCs.Visible = false;
		Bosses.Visible = false;
		Tournaments.Visible = false;
		Recruitments.Visible = false;
		Tournaments.RestartData();
	}
	public TechStuff GetTechData() { return techInfo; }
}
