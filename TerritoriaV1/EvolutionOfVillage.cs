using Godot;
using Godot.Collections;

namespace TerritoriaV1;
/// <summary>
/// permet de choisir quel strategies de développement le village va appliquer
/// </summary>
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

/// <summary>
/// permet de determiner la strategie qu'utilisera le village, parmi la premiere, la deuxieme et la troisième ( cf leurs doc respectives)
/// </summary>
    public void DetermineStrategy() 
    {
        ressources = village.GetResources();
        neededRessources = village.GetNeededRessourcesPublic();
        this.NBPlaceables = village.getNBPlaceables();
        
        if(this.NBPlaceables[(int)PlaceableType.SAWMILL] == 0 && 
        this.NBPlaceables[(int)PlaceableType.FIELD] == 0 &&
        NBPlaceables[(int)PlaceableType.ICE_USINE]  == 0
        && alreadySecondary == true && alreadyTertiary == false
        && NBPlaceables[(int)PlaceableType.HOUSE]>20) 
        {
            alreadyTertiary = true;

            village.SetBuildingStrategy(factory.createTertiaryStrategy(village.GetPlaceables(),village.GetTiles()));
		    gameManager.printMessage("Le village a atteint une phase de tertiarisation : il se délaisse de la production et compte sur l'import pour satisfaire la consommation");
        }
        else if(turn > 10 && alreadyTertiary == false && alreadySecondary == false)
        {
            alreadySecondary = true;

            village.SetBuildingStrategy(factory.createSecondaryStrategy(village.GetPlaceables(),village.GetTiles()));
		    gameManager.printMessage("Le village a atteint une phase de deterritorialisation: des habitants viennent y vivrent, et certaines usines de ressources primaires commencent à fermer, au profit de l'import des ressources nécéssaire à son dévellopement");
        }
        turn++;
    }
/// <summary>
/// simple setter pour l'attribut village
/// </summary>
/// <param name="village">le village en question</param>
    public void SetVillage(Village village) 
    {
        this.village = village;
    }
}
