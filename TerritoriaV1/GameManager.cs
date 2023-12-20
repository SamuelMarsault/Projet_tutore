using Godot;
using System;
using System.Security.Principal;
using System.Threading;
using TerritoriaV1;

public partial class GameManager : Node2D
{
	private VillageManager villageManager;
	private EvolutionOfVillage evolutionOfVillage;

	turnNB turn;
	int nbMaxTurn = 50;
	int currentTurnNb = 1;

	private Printer print;
	private Trader trade;

	MessageDialog acd;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		acd = GetNode<MessageDialog>("AcceptDialogEND");

		turn = GetNode<turnNB>("t");
		turn.updateCurrentTurn(1);
		turn.Visible = false;
	
		MissingRessource missingResource = GetNode<MissingRessource>("MissingRessource");
		var printer = GetNode<Printer>("Printer");
		printer.setMessageWindow(missingResource);	

		var trader = GetNode<Trader>("Trader");
		
		evolutionOfVillage = new EvolutionOfVillage(this);
		if(evolutionOfVillage != null)
	
		villageManager = new VillageManager(GetNode<TileMap>("Map"),printer,trader,evolutionOfVillage);	

		this.print = printer;	
		this.trade = trader;
	}
	
	public void nextTurn(int[] export, int[] import, int[] money)
	{
		
		currentTurnNb++;
		turn.updateCurrentTurn(currentTurnNb);

		if(currentTurnNb > nbMaxTurn || !villageManager.IsVillageOk())
		{
			EndGame("felicitation, vous avez fait progresser le village à travers les phases de son dévellopement urbain : vous avez gagné");
			return;
		}
		
		villageManager.NextTurn(export, import, money);
	}

	public void updateGraphics()
	{
		
	}

	public void EndGame(string message)
	{
		acd.SetErrorMessage(message,true);
		acd.PopupCentered();

	}

	public void Victory(){
		//TODO
	}

	public void _on_missing_ressource_canceled(){
		currentTurnNb--;
		turn.updateCurrentTurn(currentTurnNb);
		villageManager.applyNextTurn(false);
	}

	public void _on_missing_ressource_confirmed(){
		villageManager.applyNextTurn(true);
	}

	public void printMessage(string message)
	{
		var messageDialog = new MessageDialog();
		messageDialog.SetErrorMessage(message,false);
		AddChild(messageDialog);
		messageDialog.PopupCentered();
	}

	public void _on_start_pressed(){
		var menu = GetNode<TextureRect>("StartMenu");
		this.trade.setVisibility();
		this.print.setVisibility();
		turn.Visible = true;
		menu.Visible = false;
		printMessage("Bienvenue ! Vous êtes responsables de l'import et de l'export des ressources de notre village. Nous comptons sur vous.");
	}

	public void _on_exit_pressed(){
		GetTree().Quit();
	}

	public void _on_accept_dialog_end_confirmed()
	{
		GetTree().ReloadCurrentScene();
	}

	public void _on_accept_dialog_end_canceled()
	{
		GetTree().ReloadCurrentScene();	
	}

}
