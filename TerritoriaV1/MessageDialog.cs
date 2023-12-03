using Godot;
using System;

public partial class MessageDialog : AcceptDialog
{
	 [Signal]
    public delegate void DialogClosedEventHandler();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetErrorMessage(string errorMessage)
    {
        DialogText = errorMessage;
    }

	 // Method called when the window is closed (e.g., in response to "OK" button)
    private void OnDialogClosedButtonPressed()
    {
        EmitSignal("DialogClosed");
        QueueFree();  // Free the resources of the window
    }
}
