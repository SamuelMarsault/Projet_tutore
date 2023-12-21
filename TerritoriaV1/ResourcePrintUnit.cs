using Godot;

public partial class ResourcePrintUnit : Control
{
	private Label valueLabel;
	[Export] private Texture2D icon;

	/// <summary>
	/// Récupère ses enfants pour mettre la bonne icône
	/// </summary>
	public override void _Ready()
	{
		valueLabel = this.FindChild("ResourceLabel", true, true) as Label;
		valueLabel.Text = 500.ToString();
		TextureRect textureRect = this.FindChild("ResourceTexture", true, true) as TextureRect;
		textureRect.Texture = icon;

	}
	
	/// <summary>
	/// Setter sur la quantité de ressource affichée
	/// </summary>
	/// <param name="value">La nouvelle valeur à afficher</param>
	public void SetValue(int value){
		valueLabel.Text = value.ToString();
	}

	/// <summary>
	/// Un signal pour annoncer le changement du total
	/// </summary>
	/// <param name="total">Le nouveau total</param>
	[Signal] public delegate void TotalChangedEventHandler(int total);

	/// <summary>
	/// Setter sur la quantité de ressource affichée
	/// </summary>
	/// <param name="newValue">La nouvelle valeur à afficher</param>
	public void SetNewRessources(int newValue){
		valueLabel.Text = newValue.ToString();
	}

	/// <summary>
	/// Getter sur la valeur affichée
	/// </summary>
	/// <returns>La valeur affichée</returns>
	public int GetRessources(){
		return int.Parse(valueLabel.Text);
	}
	
}
