using Godot;
using System;
/// <summary>
/// permet l'affichage du tour actuel ( recylcé en affichage du nombre de citoyen aussi )
/// </summary>
public partial class turnNB : Control
{
    /// <summary>
    /// change le nombre affiché
    /// </summary>
    /// <param name="currentTurnNb">le nombre en question </param>
    
    public void updateCurrentTurn(int currentTurnNb)
    {
        Label l = (Label)GetNode("Panel/VBoxContainer/HBoxContainer/labelCurrentTurn");
        l.Text = currentTurnNb.ToString();  
    }

    /// <summary>
    /// change le texte affiché
    /// </summary>
    /// <param name="txt">le texte en question</param>
    public void updateLabel(string txt)
    {
        Label label = (Label)GetNode("Panel/VBoxContainer/Label");
        label.Text = txt;
    }
}


