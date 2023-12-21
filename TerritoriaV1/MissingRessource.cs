using Godot;
using System;
/// <summary>
/// un message de confirmation utilisé pour valider le changement de tour
/// </summary>
public partial class MissingRessource : ConfirmationDialog
{/// <summary>
/// defini le message
/// </summary>
/// <param name="message">le message en question</param>
	public void SetMessageMissingRessource(string message){
		DialogText = message;
	}

}
