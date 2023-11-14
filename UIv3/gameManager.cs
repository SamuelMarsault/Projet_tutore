using Godot;
using System;
using System.Collections.Generic;

public partial class gameManager : Node2D
{
	
	private List<gameObserver> observers = new List<gameObserver>();


	Node UIview;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		UIview = GetNode("UIView");
		addObserver(UIview as gameObserver);
		LoadDictionnaryQuantity();
		LoadDictionnaryTradCost();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	public void addObserver(gameObserver observer)
	{
		this.observers.Add(observer);
	}

	private void notifyObserverRessourcesUpdate(RESSOURCES ressource, int newValue)
	{
		foreach(gameObserver observer in observers)
		{
			observer.reactToRessourcesUpdate(ressource,newValue);
		}
	}

	public void funcname(RESSOURCES ressource, int quantity)
	{
		
    	int tradCost = ressourcesDictionnary.ressourcesTradCost[ressource];

    	if (quantity > 0) // Buying
    	{
        	int maxBuyableQuantity = ressourcesDictionnary.ressourcesQuantity[RESSOURCES.MONEY] / tradCost;
        	int actualQuantity = Math.Min(quantity, maxBuyableQuantity);

        	int totalCost = actualQuantity * tradCost;
        	ressourcesDictionnary.ressourcesQuantity[ressource] += actualQuantity;
        	notifyObserverRessourcesUpdate(ressource, actualQuantity);

        	ressourcesDictionnary.ressourcesQuantity[RESSOURCES.MONEY] -= totalCost;
        	notifyObserverRessourcesUpdate(RESSOURCES.MONEY, -totalCost);
    	}
    	else if (quantity < 0) // Selling
    	{
	        int availableQuantity = ressourcesDictionnary.ressourcesQuantity[ressource];
    	    int actualQuantity = Math.Min(-quantity, availableQuantity);

        	int totalGain = actualQuantity * (tradCost / 2);
        	ressourcesDictionnary.ressourcesQuantity[ressource] -= actualQuantity;
        	notifyObserverRessourcesUpdate(ressource, -actualQuantity);

        	ressourcesDictionnary.ressourcesQuantity[RESSOURCES.MONEY] += totalGain;
        	notifyObserverRessourcesUpdate(RESSOURCES.MONEY, totalGain);
    	}
    // else: Do nothing for quantity == 0, as it's an invalid trade

    // Additional checks or actions can be added as needed
}

	public static void LoadDictionnaryQuantity()
	{
		foreach(RESSOURCES ressource in Enum.GetValues(typeof(RESSOURCES)))
    	{
        	ressourcesDictionnary.ressourcesQuantity.Add(ressource, 100);
    	}
	}

	public static void LoadDictionnaryTradCost()
	{	
		ressourcesDictionnary.ressourcesTradCost.Add(RESSOURCES.WOOD,2);
		ressourcesDictionnary.ressourcesTradCost.Add(RESSOURCES.BEER,5);
		ressourcesDictionnary.ressourcesTradCost.Add(RESSOURCES.ICE,3);
		ressourcesDictionnary.ressourcesTradCost.Add(RESSOURCES.HOP,1);
	}
}
