using Godot;
using System;
/// <summary>
/// l'ecran qui s'affiche quand on perd ou gagne
/// </summary>
public partial class end_screen : Panel
{
    /// <summary>
    /// change le texte affiché
    /// </summary>
    /// <param name="text">le texte en question</param>
    public void setText(string text)
    {
          Label label = (Label)GetNode("Label");
          label.Text = text;
    }

/// <summary>
/// relance le jeu le joueur clique apres une défaite
/// </summary>
    public void _on_button_pressed()
    {
        GetTree().ReloadCurrentScene();
    }
}
