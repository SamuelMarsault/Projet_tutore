using Godot;
using System;

/// <summary>
/// utiliser pour communiquer via pop up au joueur
/// </summary>
public partial class MessageDialog : AcceptDialog
{
	/// <summary>
	/// defini le message a transmettre
	/// </summary>
	/// <param name="errorMessage"> le message d'erreur a transmettre</param>
	/// <param name="end">ignorer le svp</param>
	public void SetErrorMessage(string errorMessage, bool end)
	{
		DialogText = errorMessage;
	}
}
