using Godot;
using System;

public abstract class StratDecorator : BuildingStrategy
{

    // faire attention a les emboiter dans l'ordre inverse des priorité, pour qu'il dépense en premier l'argent dans les chose nécessaire. 

    BuildingStrategy strategy;
    public override void executeStrategie()
    {

        strategy.executeStrategie();
    }

    abstract public void ExecuteOwnStrat();
}
