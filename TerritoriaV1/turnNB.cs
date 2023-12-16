using Godot;
using System;

public partial class turnNB : Control
{
    public void updateCurrentTurn(int currentTurnNb)
    {
        Label l = (Label)GetNode("Panel/VBoxContainer/HBoxContainer/labelCurrentTurn");
        l.Text = currentTurnNb.ToString();  
        GD.Print("turn");
    }
}


