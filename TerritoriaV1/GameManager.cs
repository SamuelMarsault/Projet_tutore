using Godot;
using System;

public partial class GameManager : Node2D
{
	private VillageManager villageManager;
	private TileMap map;
	private Printer printer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.map = GetNode<TileMap>("Map");
		this.printer = GetNode<Printer>("Printer");
		this.villageManager = new VillageManager(map, printer);	
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
