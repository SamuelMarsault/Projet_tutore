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
	turnNB citizen;
	int nbMaxTurn = 50;
	int currentTurnNb = 1;

	private Printer print;
	private Trader trade;

	MessageDialog acd;

	private Button button;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Button Button = GetNode<Button>("Printer/ChangeMessageNeedResources");
		this.button = Button;

		acd = GetNode<MessageDialog>("AcceptDialogEND");

		turn = GetNode<turnNB>("t");
		turn.updateLabel("tour actuel : ");
		turn.updateCurrentTurn(1);
		turn.Visible = false;

		citizen = GetNode<turnNB>("citizens");
		citizen.updateLabel("citoyens");
		citizen.updateCurrentTurn(10);
		citizen.Visible = false;
	
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
		


		if(currentTurnNb > nbMaxTurn)
		{
			EndGame("felicitation, vous avez fait progresser le village à travers les phases de son dévellopement urbain : vous avez gagné !");
			return;
		}

		if(!villageManager.IsVillageOk())
		{
			EndGame("vous avez perdu ! : tous les habitant ont quittés votre village");
			return;
		}

		if(villageManager.change == false && currentTurnNb > 2)
		{
			EndGame("vous avez perdu ! : il n'y a eu aucune activité économique dans votre village");
			return;
		}
		
		villageManager.NextTurn(export, import, money);
		citizen.updateCurrentTurn(villageManager.getNumberCitizen());
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
		this.button.Visible = true;
		turn.Visible = true;
		menu.Visible = false;
		citizen.Visible = true;
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

	public void _on_change_message_need_resources_pressed(){
		if(this.button.ButtonPressed){
			this.button.Text = "Affichage des ressources manquantes : OUI";
			villageManager.setMessage(true);
		}
		else{
			this.button.Text = "Affichage des ressources manquantes : NON";
			villageManager.setMessage(false);
		}
	}

}
