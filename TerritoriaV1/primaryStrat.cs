using Godot;
using System;

public partial class PrimaryStrat : BuildingStrategy    // g enlevé extend noeud ici, pas que ca casse tout
{

    // le principe de cette strategie est d'ignorer le systeme d'import export. elle considere que toute les ressources viennent 
    // de nos batiment de production et qu'elles vont toutes aller dans nos batiments consommateurs

    // l'objectif du joueur vis à vis de cette stratégie est d'importer le minimum pour lancer la production puis d'attendre simplement de generer de l'argent 

    // cette stratégie doit disparaitre des les premiers tours et ne jamais revenir

    // la gare de niveau 1 est liée à cette stratégie

    // cette stratégie prend fin quand [...] ( attendre X euro ?)

    
    public override void executeStrategie() // appelée au changement de tour je suppose 
    {
        
    }
}
