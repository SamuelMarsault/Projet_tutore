using Godot;
using System;
using TerritoriaV1;

public partial class GameManager : Node2D
{
	private VillageManager villageManager;
	EvolutionOfVillage evolutionOfVillage;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var printer = GetNode<Printer>("Printer");
		villageManager = new VillageManager(GetNode<TileMap>("Map"),printer,GetNode<Trader>("Trader"));	
		EvolutionOfVillage evolutionOfVillage = new EvolutionOfVillage();
		MissingRessource missingResource = GetNode<MissingRessource>("MissingRessource");
		printer.setMessageWindow(missingResource);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	public void nextTurn(int[] export, int[] import)
	{
		villageManager.NextTurn(export, import);
	}

	public void updateGraphics()
	{
		
	}

	public void Defeat(){
		GD.Print("Fin");
		//TODO
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
