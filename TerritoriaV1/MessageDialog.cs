using Godot;

public partial class MessageDialog : AcceptDialog
{

	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	/// <summary>
	/// Permet de définir les messages d'erreurs ou normaux
	/// </summary>
	/// <param name="errorMessage"></param>
	/// <param name="end"></param>
	public void SetErrorMessage(string errorMessage, bool end)
	{
		DialogText = errorMessage;
	}
}
