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
		ressourcesDictionnary.LoadDictionnary();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		notifyObserverRessourcesUpdate(RESSOURCES.WOOD,1000);
		notifyObserverRessourcesUpdate(RESSOURCES.BEER,1);
		notifyObserverRessourcesUpdate(RESSOURCES.HOP,500);
		notifyObserverRessourcesUpdate(RESSOURCES.ICE,250);
		notifyObserverRessourcesUpdate(RESSOURCES.MONEY,10);
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
		ressourcesDictionnary.ressourcesQuantity[ressource] = newValue;
	}
}
