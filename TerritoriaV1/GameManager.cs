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
		villageManager = new VillageManager(GetNode<TileMap>("Map"));	
		EvolutionOfVillage evolutionOfVillage = new EvolutionOfVillage();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	public void nextTurn(int[] export, int[] import, int total)
	{
		villageManager.NextTurn(export, import, total);
	}

	public void updateGraphics()
	{
		
	}
}
