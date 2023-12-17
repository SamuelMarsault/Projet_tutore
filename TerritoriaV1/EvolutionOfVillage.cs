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
        if(village == null)// test de village, car le EoV est crée par le GM, 
         //qui le passe ensuite à VM. mais comme c'est VM qui crée Village, on le passe par setter a EoV 
        {
            GD.Print("EoV - village est null");
        }

        ressources = village.GetResources();
        neededRessources = village.GetNeededRessourcesPublic();
        this.NBPlaceables = village.getNBPlaceables();

        if(this.NBPlaceables[(int)PlaceableType.SAWMILL] == 0 && 
        this.NBPlaceables[(int)PlaceableType.FIELD] == 0 &&
        NBPlaceables[(int)PlaceableType.ICE_USINE]  == 0
        && alreadySecondary == true) 
        {
            //GD.Print("tertiary");
            alreadyTertiary = true;

            village.SetBuildingStrategy(factory.createTertiaryStrategy(village.GetPlaceables(),village.GetTiles()));
		    gameManager.printMessage("le village a atteint une phase de tertiarisation : il se délaisse de la production et compte sur l'import pour satisfaire la consommation");
        }
        else if(ressources[(int)ResourceType.MONEY]>10000 && alreadyTertiary == false)
        {
            //GD.Print("secondary");
            alreadySecondary = true;

            village.SetBuildingStrategy(factory.createSecondaryStrategy(village.GetPlaceables(),village.GetTiles()));
		    gameManager.printMessage("le village a atteint une phase de deterritorialisation: des habitants viennent y vivrent, et certaines usines de ressources primaires commencent à fermer, au profit de l'import des ressources nécéssaire à son dévellopement");
	
        }
        else if(alreadyTertiary == false && alreadySecondary == false)
        {
            //village.SetBuildingStrategy(factory.createPrimaryStrategy(village.GetPlaceables(),village.GetTiles()));
            // garde la stratégie primaire actuelle, pas besoin de la rechanger
        }
        else
        {
            GD.Print("on ne devrais pas être ici");
        }
    }

    public void SetVillage(Village village) 
    {
        this.village = village;
    }
}
