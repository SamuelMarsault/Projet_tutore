using Godot;
using System;

public partial class GameManager : Node2D
{
	private VillageManager villagemanager;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		villagemanager = new VillageManager(GetNode<TileMap>("Map"));	
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void nextTurn()
	{
	}

	public void updateGraphics()
	{
		
	}
}
