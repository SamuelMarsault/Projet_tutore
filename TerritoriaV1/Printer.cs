using Godot;
using System;
using System.Collections.Generic;
using TerritoriaV1;

public partial class Printer : Node, VillageObserver
{
	// Called when the node enters the scene tree for the first time.
	private List<ResourcePrintUnit> resourcePrintUnits = new ();
	[Export] private GameManager parent;
	public override void _Ready()
	{
		Node container = this.GetNode("HBoxContainer");
		foreach (ResourcePrintUnit resourcePrintUnit in container.GetChildren())
		{
			resourcePrintUnits.Add(resourcePrintUnit);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	private void updateResources(ResourceType resources, int[] quantities, int numResource) {
		resourcePrintUnits[numResource].SetNewRessources(quantities[numResource]);
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
		
	}

	public void ReactToTilesChange(TileType[,] tiles)
	{
		
	}

	public int GetRessource(int numResource){
		return resourcePrintUnits[numResource].GetRessources();
	}
	public void Defeat()
	{
		// Create an instance of the message dialog window
		var messageDialog = new MessageDialog();

		// Set the defeat message
		messageDialog.SetErrorMessage("You have lost.");

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
		parent.Defeat();
	}

	public void ReactToImpossibleTransaction(int[] missingRessources)
	{
		// Créez une instance de la fenêtre de dialogue
		var messageDialog = new MessageDialog();

		string message = "Vous n'avez pas assez de ressources, il vous manque : \n";

		for (int i = 0; i<missingRessources.Length ; i++){
			if (missingRessources[i] != 0){
				switch(i){
					case 0:
						message = message+"	\u2022 "+missingRessources[i]+" de bois\n";
						break;
					case 1:
						message = message+"	\u2022 "+missingRessources[i]+" de hoop\n";
						break;
					case 2:
						message = message+"	\u2022 "+missingRessources[i]+" de glace\n";
						break;
					case 3:
						message = message+"	\u2022 "+missingRessources[i]+" de bière\n";
						break;
					case 4:
						message = message+"	\u2022 "+missingRessources[i]+" d'argent\n";
						break;
					default:
						break;
				}
			}
		}

		// Définissez le message d'erreur
		messageDialog.SetErrorMessage(message);

		// Ajoutez la fenêtre de dialogue à la scène
		GetTree().Root.AddChild(messageDialog);
		
		// Affichez la fenêtre de dialogue
		messageDialog.PopupCentered();
	}
	public void ReactToExchangesRatesChange(int[,] exchangesRates) {}
}
