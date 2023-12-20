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

    int turn = 1;

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
        
        
        /*int barCap = NBPlaceables[PlaceableType.HOUSE.GetHashCode()] * 10;
        if (barCap > 100) barCap = 100;
        foreach (Placeable placeable in village.GetPlaceables())
        {
            if (placeable!=null && placeable.getPlaceableType() == PlaceableType.BAR)
            {
                placeable.setProductionCapacity(barCap);
            }
        }*/
        
        if(this.NBPlaceables[(int)PlaceableType.SAWMILL] == 0 && 
        this.NBPlaceables[(int)PlaceableType.FIELD] == 0 &&
        NBPlaceables[(int)PlaceableType.ICE_USINE]  == 0
        && alreadySecondary == true && alreadyTertiary == false
        && NBPlaceables[(int)PlaceableType.HOUSE]>20) 
        {
            GD.Print("tertiary");
            alreadyTertiary = true;

            village.SetBuildingStrategy(factory.createTertiaryStrategy(village.GetPlaceables(),village.GetTiles()));
		    gameManager.printMessage("Le village a atteint une phase de tertiarisation : il se délaisse de la production et compte sur l'import pour satisfaire la consommation");
        }
        else if(turn > 10 && alreadyTertiary == false && alreadySecondary == false)
        {
            GD.Print("secondary");
            alreadySecondary = true;

            village.SetBuildingStrategy(factory.createSecondaryStrategy(village.GetPlaceables(),village.GetTiles()));
		    gameManager.printMessage("Le village a atteint une phase de deterritorialisation: des habitants viennent y vivrent, et certaines usines de ressources primaires commencent à fermer, au profit de l'import des ressources nécéssaire à son dévellopement");
	
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
        turn++;
    }

    public void SetVillage(Village village) 
    {
        this.village = village;
    }
}
