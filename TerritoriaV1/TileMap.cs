using Godot;
using System;
using System.Collections.Generic;
using TerritoriaV1;

public partial class TileMap : Godot.TileMap, VillageObserver
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
		TileSet tileSet = GD.Load<TileSet>("res://Sol.tres");
		TileSet = tileSet;
		/*
		Vector2I atlas = new Vector2I(0,0);
		SetCell(0,new Vector2I(2,2),0,atlas);
		SetCell(0,new Vector2I(2,3),1,atlas);
		*/
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
	public void reactToResourcesChange(Godot.Collections.Dictionary<ResourceType, int> resources)
	{
		
	}
	//Allows you to change the floor
	public void reactToTilesChange(TileType[][] tiles)
	{
		for (int i = 0; i < tiles.Length; i++)
		{
			for (int j = 0; j < tiles[i].Length; j++)
			{
				TileType tile = tiles[i][j];
				switch (tile)
				{
					case TileType.GRASS:SetCell(0,new Vector2I(i,j),0,new Vector2I(0,0));
						break;
					default:SetCell(0,new Vector2I(i,j),1,new Vector2I(0,0));
						break;
				} 
			}
		}
	}
	//Allows you to place the buildings that are there when you start the game
	public void reactToPlaceableChange(List<Placeable> placeables)
	{
		for (int i = 0; i<placeables.Count;i++){
			int ID = -1;
			if (GetCellSourceId(1,placeables[i].getPosition())==-1){
				if (placeables[i].getPlaceableType() == PlaceableType.HOUSE){
					ID=7;
				}
				else if(placeables[i].getPlaceableType() == PlaceableType.BAR){
					ID=5;
				}
				else if(placeables[i].getPlaceableType() == PlaceableType.FIELD){
					ID=4;
				}
				else if(placeables[i].getPlaceableType() == PlaceableType.TRAIN_STATION){
					ID=6;
				}
				else if(placeables[i].getPlaceableType() == PlaceableType.SAWMILL){
					ID=3;
				}
				else if(placeables[i].getPlaceableType() == PlaceableType.FOREST){
					ID=2;
				}
				Console.WriteLine("je place un "+placeables[i]);
				SetCell(1,placeables[i].getPosition(),ID,new Vector2I(0,0));
			}
		}
	}
}
