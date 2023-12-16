using Godot;
using Godot.Collections;

namespace TerritoriaV1;

public class EvolutionOfVillage
{
    private Village village;

    private int[] ressources;
    private int[] neededRessources;    
    private int[] NBPlaceables;
    private BuildingStrategyFactory factory = new BuildingStrategyFactory();
    private GameManager gameManager;

    bool alreadySecondary = false;
    bool alreadyTertiary = false;

    public EvolutionOfVillage(GameManager gm)
    {
        this.gameManager = gm;
    }

    public void DetermineStrategy()
    {
        var messageDialog = gameManager.GetNode<MessageDialog>("messageDialog");
        messageDialog.SetErrorMessage("hello");

        messageDialog.PopupCentered();

        ressources = village.GetResources();
        neededRessources = village.GetNeededRessourcesPublic();
        this.NBPlaceables = village.getNBPlaceables();

        if(this.NBPlaceables[(int)PlaceableType.SAWMILL] == 0 && this.NBPlaceables[(int)PlaceableType.FIELD] == 0 && NBPlaceables[(int)PlaceableType.ICE_USINE]  == 0 && alreadySecondary == true)  // cette condition ne sera jamais remplit
        {
            alreadyTertiary = true;
            village.SetBuildingStrategy(factory.createTertiaryStrategy(village.GetPlaceables(),village.GetTiles()));
            messageDialog = gameManager.GetNode<MessageDialog>("messageDialog");
		    messageDialog.SetErrorMessage("le village a atteint une phase de tertiarisation : il se délaisse de la production et compte sur l'import pour satisfaire la consommation");
		    
		    messageDialog.PopupCentered();
        }
        else if(ressources[(int)ResourceType.MONEY]>10000 && alreadyTertiary == false)
        {
            alreadySecondary = true;
            village.SetBuildingStrategy(factory.createSecondaryStrategy(village.GetPlaceables(),village.GetTiles()));
              messageDialog = gameManager.GetNode<MessageDialog>("messageDialog");
		    messageDialog.SetErrorMessage("le village a atteint une phase de deterritorialisation: des habitants viennent y vivrent, et certaines usines de ressources primaires commencent à fermer, au profit de l'import des ressources nécéssaire à son dévellopement");
		    messageDialog.PopupCentered();
        }
        else if(alreadyTertiary == false && alreadyTertiary == false)
        {
            village.SetBuildingStrategy(factory.createPrimaryStrategy(village.GetPlaceables(),village.GetTiles()));
        }
    }

    public void SetVillage(Village village) 
    {
        this.village = village;
    }

    public void setGameManager(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
}
