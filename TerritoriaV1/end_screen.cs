using Godot;
using System;

public partial class end_screen : Panel
{
    public void setText(string text)
    {
          Label label = (Label)GetNode("Label");
          label.Text = text;
    }

    public void _on_button_pressed()
    {
        GetTree().ReloadCurrentScene();
    }
}
