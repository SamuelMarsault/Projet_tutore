using System.Security.Cryptography;
using Godot;
using TerritoriaV1;

/// <summary>
/// Administre la partie et vérifie les conditions de fin de partie
/// </summary>
public partial class GameManager : Node2D
{

	private VillageManager villageManager;
	private EvolutionOfVillage evolutionOfVillage;

	turnNB turn;
	turnNB citizen;

	end_screen end_Screen;

	int nbMaxTurn = 50;
	int currentTurnNb = 1;

	private Printer print;
	private Trader trade;

	MessageDialog acd;

	private Button button;
	private Panel afficheMessage;

	/// <summary>
	/// Récupère et relie toutes les classes pour démarrer le jeu
	/// </summary>
	public override void _Ready()
	{
		TextureRect textureRect = GetNode<TextureRect>("StartMenu");
		textureRect.Visible = true; 
		this.GetWindow().MinSize = this.GetWindow().Size; 
		Button Button = GetNode<Button>("Printer/ChangeMessageNeedResources");
		this.button = Button;

		acd = GetNode<MessageDialog>("AcceptDialogEND");

		var panel = GetNode<Panel>("AfficheMessages");
		panel.Visible = false;
		afficheMessage = panel;

		turn = GetNode<turnNB>("t");
		turn.updateLabel("tour actuel : ");
		turn.updateCurrentTurn(1);
		turn.Visible = false;

		citizen = GetNode<turnNB>("citizens");
		citizen.updateLabel("citoyens");
		citizen.updateCurrentTurn(10);
		citizen.Visible = false;

		end_Screen = GetNode<end_screen>("endScreen");
		end_Screen.Visible = false;

	
		MissingRessource missingResource = GetNode<MissingRessource>("MissingRessource");
		var printer = GetNode<Printer>("Printer");
		printer.SetMessageWindow(missingResource);	

		var trader = GetNode<Trader>("Trader");
		TileMap tileMap = GetNode<TileMap>("Map");
		Control infoCard = GetNode<Control>("InfoCard");
		tileMap.setInfoCard(infoCard);
		
		evolutionOfVillage = new EvolutionOfVillage(this);
		if(evolutionOfVillage != null) 
			villageManager = new VillageManager(GetNode<TileMap>("Map"),printer,trader,evolutionOfVillage);

		this.print = printer;	
		this.trade = trader;
		tileMap.setVillageManager(villageManager);
	}
	
	/// <summary>
	/// Demande à passer au tour prochain, et vérifie si le jeu doit s'arrêter ou non
	/// </summary>
	/// <param name="export">Les exports de ce tour</param>
	/// <param name="import">Les imports de ce tour</param>
	/// <param name="money">Les montants de ce tour</param>
	public void nextTurn(int[] export, int[] import, int[] money)
	{
		
		
		
		villageManager.NextTurn(export, import, money, currentTurnNb);
		if(currentTurnNb >= nbMaxTurn && villageManager.GetVillage().IsStratTertiary())
		{
			EndGame("Félicitations,\n vous avez fait progresser le village à travers les phases de son développement urbain :\n vous avez gagné !", Colors.Green);
			return;
		}

		if(!villageManager.IsVillageOk())
		{
			EndGame("Vous avez perdu !\nTous les habitants ont quittés votre village...",Colors.Red);
			return;
		}
		currentTurnNb++;
		turn.updateCurrentTurn(currentTurnNb);
		citizen.updateCurrentTurn(villageManager.GetNumberCitizen());
	}
		
	/// <summary>
	/// Termine le partie et affiche au joueur le message de fin
	/// </summary>
	/// <param name="message">Message à afficher au joueur</param>
	/// <param name="color">Défini la couleur du texte</param>
	public void EndGame(string message, Color color)
	{
		Printer printer  = (Printer)GetNode<Printer>("Printer");
		print.SetVisibility(false);
		var trader = GetNode<Trader>("Trader");
		trader.SetVisibility(false);
		turn.Visible = false;
		citizen.Visible = false;
		end_Screen.setText(message, color);
		end_Screen.Visible = true;
	}

	/// <summary>
	/// Annule le passage du tour
	/// </summary>
	public void _on_missing_ressource_canceled(){
		currentTurnNb--;
		turn.updateCurrentTurn(currentTurnNb);
		villageManager.applyNextTurn(false);
	}

	/// <summary>
	/// Confirme le passage de tour
	/// </summary>
	public void _on_missing_ressource_confirmed(){
		villageManager.applyNextTurn(true);
	}

	/// <summary>
	/// Affiche un message au joueur
	/// </summary>
	/// <param name="message">Le message à afficher</param>
	/// <returns> </returns>
	public void printMessage(string message)
	{
		var messageDialog = new MessageDialog();
		messageDialog.SetErrorMessage(message,false);
		AddChild(messageDialog);
		messageDialog.PopupCentered();
	}

	/// <summary>
	/// Affiche le message de bienvenue
	/// </summary>
	public void _on_start_pressed(){
		var menu = GetNode<TextureRect>("StartMenu");
		this.trade.SetVisibility(true);
		this.print.SetVisibility(true);
		this.button.Visible = true;
		turn.Visible = true;
		menu.Visible = false;
		citizen.Visible = true;
		printMessage("Bienvenue au village de Territoria ! \n\n Vous êtes responsables de l'importation et de l'exportation des ressources de notre village.\n Nous comptons sur vous.");
	}

	/// <summary>
	/// Ferme le jeu
	/// </summary>
	public void _on_exit_pressed(){
		GetTree().Quit();
	}

	/// <summary>
	/// Redémarre l'application (et donc la partie)
	/// </summary>
	public void _on_accept_dialog_end_canceled()
	{
		GetTree().ReloadCurrentScene();
	}
	
	
	/// <summary>
	/// Redémarre l'application (et donc la partie)
	/// </summary>
	public void _on_accept_dialog_end_confirmed()
	{
		GetTree().ReloadCurrentScene();
	}

	/// <summary>
	/// Actualise le message sur le bouton d'affichage des ressources manquantes
	/// </summary>
	public void _on_change_message_need_resources_pressed(){
		if(this.button.ButtonPressed){
			this.button.Text = "Affichage des ressources manquantes : OUI";
			villageManager.SetMessage(true);
		}
		else{
			this.button.Text = "Affichage des ressources manquantes : NON";
			villageManager.SetMessage(false);
		}
	}

	/// <summary>
	/// Ouvre un navigateur et amène vers le dépôt du projet
	/// </summary>
	private void _on_info_pressed()
	{
		// Ouvrir le navigateur avec le lien spécifique
		OS.ShellOpen("https://git.unistra.fr/miniotti/han23-t3-a/-/blob/main/WikiDescription.MD?ref_type=heads");
	}

	public void setMessage(string message)
	{
		Label messageStrat = GetNode<Label>("AfficheMessages/TextStrat");
		GD.Print(messageStrat.Name);
		messageStrat.Text = message;
		afficheMessage.Visible = true;
	}

	public void _on_stop_text_pressed(){
		afficheMessage.Visible = false;
	}
}
