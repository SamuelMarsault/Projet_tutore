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

	public void reactToPlaceableChange(List<Placeable> placeables)
	{
		
	}
    public void reactToTilesChangesTiles(Vector2I setTile, int layer,int ID)
	{
		SetCell(layer,setTile,0,new Vector2I(0,0));
	}

    public void reactToInitialisePlaceable(List<Placeable> placeables)
    {
						GD.Print("test");
        for (int i = 0; i<placeables.Count;i++){
			if (GetCellSourceId(1,placeables[i].getPosition())==-1){
				SetCell(1,placeables[i].getPosition(),placeables[i].getID(),new Vector2I(0,0));
			}
		}
    }
}
