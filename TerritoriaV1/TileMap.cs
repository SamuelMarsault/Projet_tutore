using Godot;
using TerritoriaV1;
/// <summary>
/// la tile map, vue qui represente le village pour le joueur
/// </summary>
public partial class TileMap : Godot.TileMap, VillageObserver
{
    VillageManager villageManager;

    private Control infoCard;
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

	public void setInfoCard(Control ic)
    {
        this.infoCard = ic;
        infoCard.Hide();
    }

	public void ReactToResourcesChange(int[] resources)
	{
		
	}
	/// <summary>
	/// ce qui se passe quand un tiles de la tilemap change
	/// </summary>
	/// <param name="tiles">le tile qui a changé</param>
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
	
	/// <summary>
	/// ce qui se passe quand un placeable change : le fait apparaitre sur la tilemap
	/// </summary>
	/// <param name="placeables">le nouveau tableau de placeables</param>
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
					SetCell(1,new Vector2I(i,j),ID,new Vector2I(0,0));
				}
				else if(placeables[i,j] == null)
				{
					EraseCell(1,new Vector2I(i,j));
				}
			}
		}
	}
	public void ReactToImpossibleTransaction(int[] missingResources) {}
	public void ReactToExchangesRatesChange(int[,] exchangesRates) {}

	public void setVillageManager(VillageManager villageManager)
    {
        this.villageManager = villageManager;
    }

	public override void _Input(InputEvent @event) {
    		// L'action LeftClick est associée au clic gauche de la souris
    		if (@event.IsActionPressed("LeftClick"))
    		{
    		    // On récupère la position de la souris dans la fenêtre
                Vector2 mousePos = GetLocalMousePosition();
                // On récupère les coordonnées de la case cliquée
                Vector2I tileCoords = LocalToMap(mousePos);
                // On affiche la carte d'information
                DisplayInfoCard(tileCoords);
            }
    }

    private void DisplayInfoCard(Vector2I tileCoords)
    {
        // On récupère x et y à partir de tileCoords
        int x = tileCoords.X;
        int y = tileCoords.Y;

        // On récupère le placeable à la position tileCoords
        Placeable placeable = villageManager.GetPlaceable(x, y);

        if (placeable == null)
        {
            infoCard.Hide();
            return;
        }

        // On récupère le noeud enfant "Info" de la carte d'information
        Node info = infoCard.GetNode("Info");

        // On récupère le type de placeable
        PlaceableType placeableType = placeable.getPlaceableType();

        // On récupère le nom du placeable
        string placeableName = placeableType.ToString();

        // On modifie le texte de l'élément PlaceableName de la carte d'information
        info.GetNode<Label>("PlaceableName").Text = placeableName;

        // On récupère la description et le chemin de l'image du placeable avec un switch
        string description, imagePath;

        switch (placeableType)
        {
            case PlaceableType.HOUSE: description = "Héberge les citoyens, ils partiront s'ils n'ont pas de bière à boire"; imagePath="img/House.png"; break;
            case PlaceableType.BAR: description = "Vend les bières a vos citoyens"; imagePath="img/Bar.png"; break;
            case PlaceableType.FIELD: description = "Produit le houblon nécessaire à l'élaborations des bières"; imagePath="img/Field.png"; break;
            case PlaceableType.TRAIN_STATION: description = "Permet le transport des marchandises. les taux de change s'améliorent avec votre village"; imagePath="img/Gare.png"; break;
            case PlaceableType.SAWMILL: description = "Produit le bois nécessaire à la construction des autres bâtiments"; imagePath="img/SawMill.png"; break;
            case PlaceableType.BEER_USINE: description = "Produit des bières avec le houblon et les glaçons"; imagePath="img/BeerUsine.png"; break;
            case PlaceableType.ICE_USINE: description = "Produit les glaçons nécessaires à l'élaborations des bières"; imagePath="img/IceUsine.png"; break;
            default: description = "Un truc bizarre !"; imagePath="img/House.png"; break;
        }

        // On modifie le texte de l'élément PlaceableDescription de la carte d'information
        info.GetNode<Label>("Description").Text = description;

        // On modifie l'image de l'élément PlaceableTextureRect de la carte d'information
        TextureRect placeableTextureRect = info.GetNode<TextureRect>("PlaceableTextureRect");
        placeableTextureRect.Texture = (Texture2D)GD.Load<Texture>(imagePath);

        // On récupère les ressources d'input et d'output *
        int[] inputResources = placeable.getResourceInputs();
        int[] outputResources = placeable.getResourceOutputs();

        int childCount = info.GetChildCount();

        for (int i = 0; i < 5; i++)
        {
            // On récupère la valeur de la ressource d'input i
            int inputResource = inputResources[i];
            TextureRect inputResourceTextureRect = (TextureRect)info.GetChild(i+4);
            Color currentColor = inputResourceTextureRect.Modulate;
            if (inputResource >= 1)
            {
                // On met la transparence de l'élément à 1
                inputResourceTextureRect.Modulate = new Color(currentColor.R, currentColor.G, currentColor.B, 1);
            } else {
                // On met la transparence de l'élément à 0.3
                inputResourceTextureRect.Modulate = new Color(currentColor.R, currentColor.G, currentColor.B, 0.3f);
            }
        }

        for (int i = 0; i < 5; i++)
        {
            // On récupère la valeur de la ressource d'output i
            int outputResource = outputResources[i];
            TextureRect outputResourceTextureRect = (TextureRect)info.GetChild(i+10);
            Color currentColor = outputResourceTextureRect.Modulate;
            if (outputResource >= 1)
            {
                // On met la transparence de l'élément à 1
                outputResourceTextureRect.Modulate = new Color(currentColor.R, currentColor.G, currentColor.B, 1);
            } else {
                // On met la transparence de l'élément à 0.3
                outputResourceTextureRect.Modulate = new Color(currentColor.R, currentColor.G, currentColor.B, 0.3f);
            }
        }

        // On affiche la carte d'information
        infoCard.Show();
    }
}
