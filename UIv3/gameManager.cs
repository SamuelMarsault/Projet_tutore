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

	public void funcname(RESSOURCES ressource, int newValue)
	{
		if(ressourcesDictionnary.ressourcesQuantity[RESSOURCES.MONEY] >= newValue*ressourcesDictionnary.ressourcesTradCost[ressource])
		ressourcesDictionnary.ressourcesQuantity[ressource] += newValue;
		notifyObserverRessourcesUpdate(ressource,newValue);
		ressourcesDictionnary.ressourcesQuantity[RESSOURCES.MONEY] -= newValue*ressourcesDictionnary.ressourcesTradCost[ressource];
	}

		public static void LoadDictionnaryQuantity() // a revoir
	{
		foreach(RESSOURCES resource in Enum.GetValues(typeof(RESSOURCES)))
    	{
        	ressourcesDictionnary.ressourcesQuantity.Add(resource, 0);
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
