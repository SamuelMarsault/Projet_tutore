using Godot;
using TerritoriaV1;

public partial class TileMap : Godot.TileMap, VillageObserver
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
		TileSet tileSet = GD.Load<TileSet>("res://Sol.tres");
		TileSet = tileSet;
	
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
	public void ReactToResourcesChange(int[] resources)
	{
		
	}
	//Allows you to change the floor
	public void ReactToTilesChange(TileType[,] tiles)
	{
		for (int i = 0; i < tiles.GetLength(0); i++)
		{
			for (int j = 0; j < tiles.GetLength(1); j++)
			{
				TileType tile = tiles[i,j];
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
	public void ReactToPlaceableChange(Placeable[,] placeables)
	{
		for (int i = 0; i < placeables.GetLength(0); i++)
		{
			for (int j = 0; j < placeables.GetLength(1); j++)
			{
				if (placeables[i,j]!=null)
				{
					int ID = -1;
					PlaceableType cPlaceableType = placeables[i,j].getPlaceableType();
					switch (cPlaceableType)
					{
						case PlaceableType.HOUSE: ID=3; break;
						case PlaceableType.BAR: ID = 9; break;
						case PlaceableType.FIELD: ID = 7; break;
						case PlaceableType.TRAIN_STATION: ID = 8; break;
						case PlaceableType.SAWMILL: ID = 4; break;
						case PlaceableType.FOREST: ID = 2; break;
						case PlaceableType.BEER_USINE: ID = 5; break;
						case PlaceableType.ICE_USINE: ID = 6;break;
					}
					//GD.Print("je place un "+placeables[i,j].getPlaceableType());
					SetCell(1,new Vector2I(i,j),ID,new Vector2I(0,0));
					GD.Print("map : nouveau "+ placeables[i,j].getPlaceableType()+" ajouté aux coordonnée" + i +","+j+" "+(TileType) GetCellSourceId(0,new Vector2I(i,j)));
				}
			}
		}
	}
	public void ReactToImpossibleTransaction(int[] missingRessources) {}
	public void ReactToExchangesRatesChange(int[,] exchangesRates) {}
}
