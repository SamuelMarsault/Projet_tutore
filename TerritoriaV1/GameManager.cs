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
		villageManager = new VillageManager(GetNode<TileMap>("Map"),GetNode<Printer>("Printer"),GetNode<Trader>("Trader"));	
		EvolutionOfVillage evolutionOfVillage = new EvolutionOfVillage();
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
}
