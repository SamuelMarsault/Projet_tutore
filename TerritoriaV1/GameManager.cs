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

		villageManager = new VillageManager(GetNode<TileMap>("Map"),GetNode<Printer>("Printer"),GetNode<Trader>("Trader"));	
		EvolutionOfVillage evolutionOfVillage = new EvolutionOfVillage();
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
		//turn.updateCurrentTurn(currentTurnNb);
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

}
