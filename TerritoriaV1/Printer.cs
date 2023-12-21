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
	
	/// <summary>
	/// Initialise les composants d'affichage
	/// </summary>
	public override void _Ready()
	{
		BoxContainer container = this.GetNode<BoxContainer>("HBoxContainer");
		foreach (ResourcePrintUnit resourcePrintUnit in container.GetChildren())
		{
			resourcePrintUnits.Add(resourcePrintUnit);
		}
		this.boxContainer = container;
	}

	/// <summary>
	/// Setter sur la visibilité
	/// </summary>
	/// <param name="vis">Si oui ou non on peut le voir</param>
	public void SetVisibility(bool vis){
		this.boxContainer.Visible = vis;
	}

	/// <summary>
	/// Demande la mise à jour des ressources
	/// </summary>
	/// <param name="resources">Le type de ressource à récupérer</param>
	/// <param name="quantities">Les nouvelles ressources</param>
	/// <param name="numResource">L'index de la ressource à récupérer</param>
	private void UpdateResources(ResourceType resources, int[] quantities, int numResource) {
		resourcePrintUnits[numResource].SetNewRessources(quantities[numResource]);
	}

	/// <summary>
	/// Setter sur la fenêtre à afficher
	/// </summary>
	/// <param name="missingRessource">La nouvelle fenêtre</param>
	public void SetMessageWindow(MissingRessource missingRessource){
		this.windowMissingRessource = missingRessource;
	}

	/// <summary>
	/// Réagi aux changements des ressources, ici actualise l'affichage
	/// </summary>
	/// <param name="resources">Les nouvelles ressources</param>
	public void ReactToResourcesChange(int[] resources)
	{
		if (resources.Length == resourcePrintUnits.Count && resources.Length == Enum.GetNames(typeof(ResourceType)).Length){
			int quelRessource = 0;
			foreach (ResourceType resourceType in (ResourceType[])Enum.GetValues(typeof(ResourceType)))
			{
				UpdateResources(resourceType,resources,quelRessource);
				quelRessource++;
			}
		}
		else{
		}		
	}

	/// <summary>
	/// Réagi au changement des placeables, ici ne fait rien
	/// </summary>
	/// <param name="placeables">Les Placeable du village</para
	public void ReactToPlaceableChange(Placeable[,] placeables)
	{
		
	}

	/// <summary>
	/// Reagi aux changements du sol, ici ne fait rien
	/// </summary>
	/// <param name="tiles">Le sol du village</param>
	/// <returns>Le tableau de besoin en ressources</returns>
	public void ReactToTilesChange(TileType[,] tiles)
	{
		
	}

	/// <summary>
	/// Getter sur la quantité de ressource selon un index
	/// </summary>
	/// <returns>La quantité de ressource du type demandé</returns>
	public int GetRessource(int numResource){
		return resourcePrintUnits[numResource].GetRessources();
	}
	/// <summary>
	/// Mintre la défaite du joueur
	/// </summary>
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

	/// <summary>
	/// Préviens le parent de la défaite
	/// </summary>
	private void DefeatParent()
	{
		// Appeler la méthode Defeat du parent
		
	}

	/// <summary>
	/// Réagi à une transaction impossible, ici affiche les ressources manquantes
	/// </summary>
	/// <param name="missingResources">Les ressources manquantes</param>
	public void ReactToImpossibleTransaction(int[] missingResources)
	{
		// Créez une instance de la fenêtre de dialogue

		string message = "Vous n'avez pas assez de ressources, il vous manque : \n";

		for (int i = 0; i < missingResources.Length; i++)
		{
			if (missingResources[i] != 0)
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
				string range = DetermineRange(missingResources[i]);

				// Append the message with the resource type and range
				message += $"\u2022 {range} {resourceType}\n";
			}
		}

		// Définissez le message d'erreur
		windowMissingRessource.SetMessageMissingRessource(message);

		windowMissingRessource.PopupCentered();
	}
	/// <summary>
	/// Réagi au changement des taux de change, ici ne fait rien
	/// </summary>
	public void ReactToExchangesRatesChange(int[,] exchangesRates) {}

	/// <summary>
	/// Détermine l'intervalle dans lequel se situe le manque de ressource
	/// </summary>
	/// <returns>La châine à afficher</returns>
	private string DetermineRange(int value)
	{
		return value.ToString();
	}
}
