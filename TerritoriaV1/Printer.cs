using Godot;
using System;
using System.Collections.Generic;
using TerritoriaV1;

public partial class Printer : Node, VillageObserver
{
	// Called when the node enters the scene tree for the first time.
	private List<ResourcePrintUnit> resourcePrintUnits = new ();
	private MissingRessource windowMissingRessource;
	private BoxContainer boxContainer;
	[Export] private GameManager parent;
	public override void _Ready()
	{
		BoxContainer container = this.GetNode<BoxContainer>("HBoxContainer");
		foreach (ResourcePrintUnit resourcePrintUnit in container.GetChildren())
		{
			resourcePrintUnits.Add(resourcePrintUnit);
		}
		this.boxContainer = container;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	public void setVisibility(){
		this.boxContainer.Visible = true;
	}

	private void updateResources(ResourceType resources, int[] quantities, int numResource) {
		resourcePrintUnits[numResource].SetNewRessources(quantities[numResource]);
	}

	
	public void setMessageWindow(MissingRessource missingRessource){
		this.windowMissingRessource = missingRessource;
	}

	public void ReactToResourcesChange(int[] resources)
	{
		if (resources.Length == resourcePrintUnits.Count && resources.Length == Enum.GetNames(typeof(ResourceType)).Length){
			int quelRessource = 0;
			foreach (ResourceType resourceType in (ResourceType[])Enum.GetValues(typeof(ResourceType)))
			{
				updateResources(resourceType,resources,quelRessource);
				quelRessource++;
			}
		}
		else{
			GD.Print("PB taille");
		}		
	}

	public void ReactToPlaceableChange(Placeable[,] placeables)
	{
		return;
	}

	public void ReactToTilesChange(TileType[,] tiles)
	{
		return;
	}

	public int GetRessource(int numResource){
		return resourcePrintUnits[numResource].GetRessources();
	}
	public void Defeat()
	{
		// Create an instance of the message dialog window
		var messageDialog = new MessageDialog();

		// Set the defeat message
		messageDialog.SetErrorMessage("You have lost.",true);

		// Add the window to the scene
		GetTree().Root.AddChild(messageDialog);

		// Display the window
		messageDialog.PopupCentered();

		DefeatParent();
	}

	// Méthode appelée lorsque la fenêtre de dialogue est fermée
	private void DefeatParent()
	{
		// Appeler la méthode Defeat du parent
		
	}

	public void ReactToImpossibleTransaction(int[] missingRessources)
	{
		// Créez une instance de la fenêtre de dialogue

		string message = "Vous n'avez pas assez de ressources, il vous manque : \n";

		for (int i = 0; i < missingRessources.Length; i++)
		{
			if (missingRessources[i] != 0)
			{
				string resourceType = "";
				switch (i)
				{
					case 0:
						resourceType = "de bois";
						break;
					case 1:
						resourceType = "de houblon";
						break;
					case 2:
						resourceType = "de glace";
						break;
					case 3:
						resourceType = "de bière";
						break;
					case 4:
						resourceType = "d'argent";
						break;
					default:
						break;
				}

				// Determine the range based on the missing resources value
				string range = DetermineRange(missingRessources[i]);

				// Append the message with the resource type and range
				message += $"\u2022 {range} {resourceType}\n";
			}
		}

		// Définissez le message d'erreur
		windowMissingRessource.SetMessageMissingRessource(message);

		windowMissingRessource.PopupCentered();
	}
	public void ReactToExchangesRatesChange(int[,] exchangesRates) {return;}

	// Determine the range based on the missing resources value
	private string DetermineRange(int value)
	{
		if (value <= 50)
			return "entre 0 et 50";
		else if (value <= 100)
			return "entre 51 et 100";
		else if (value <= 200)
			return "entre 101 et 200";
		else if (value <= 300)
			return "entre 201 et 300";
		else if (value <= 400)
			return "entre 301 et 400";
		else if (value <= 500)
			return "entre 401 et 500";
		else if (value <= 1000)
			return "entre 501 et 1000";
		else if (value <= 2000)
			return "entre 1001 et 2000";
		else if (value <= 3000)
			return "entre 2001 et 3000";
		else if (value <= 4000)
			return "entre 3001 et 4000";
		else if (value <= 5000)
			return "entre 4001 et 5000";
		else
			return "plus de 5000";
	}
}
