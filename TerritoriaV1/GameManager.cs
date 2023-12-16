using Godot;
using System;
using TerritoriaV1;

public partial class GameManager : Node2D
{
	private VillageManager villageManager;
	EvolutionOfVillage evolutionOfVillage;

	turnNB turn;

	int nbMaxTurn = 25;
	int currentTurnNb = 1;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		turn = GetNode<turnNB>("t");
		turn.updateCurrentTurn(1);

		if(turn == null)
		{
			GD.Print("turn null");
		}
	
		var printer = GetNode<Printer>("Printer");
		villageManager = new VillageManager(GetNode<TileMap>("Map"),GetNode<Printer>("Printer"),GetNode<Trader>("Trader"));
			
		evolutionOfVillage = new EvolutionOfVillage();
		MissingRessource missingResource = GetNode<MissingRessource>("MissingRessource");
		printer.setMessageWindow(missingResource);
		evolutionOfVillage.SetVillage(villageManager.GetVillage());
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	public void nextTurn(int[] export, int[] import)
	{
		
		currentTurnNb++;
		turn.updateCurrentTurn(currentTurnNb);

		if(currentTurnNb > nbMaxTurn)
		{
			EndGame(); return;
		}
		
		villageManager.NextTurn(export, import);
		evolutionOfVillage.DetermineStrategy();
	}

	public void updateGraphics()
	{
		
	}

	public void EndGame()
	{
		var messageDialog = new MessageDialog();
		messageDialog.SetErrorMessage("You have lost.");
		AddChild(messageDialog);
		messageDialog.PopupCentered();
		GetTree().Quit();
	}

	public void Victory(){
		//TODO
	}

	public void _on_missing_ressource_canceled(){
		villageManager.applyNextTurn(false);
	}

	public void _on_missing_ressource_confirmed(){
		villageManager.applyNextTurn(true);
	}
}
