using Godot;
using System;

public partial class turnNB : Control
{
    public void updateCurrentTurn(int currentTurnNb)
    {
        Label l = (Label)GetNode("Panel/VBoxContainer/HBoxContainer/labelCurrentTurn");
        l.Text = currentTurnNb.ToString();  
    }

    public void updateLabel(string txt)
    {
        Label label = (Label)GetNode("Panel/VBoxContainer/Label");
        label.Text = txt;
    }
}


