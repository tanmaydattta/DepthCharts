/*

*/
using System;
using System.Collections.Generic;

public class LeagueFactory
{
    public ILeague GetLeague(string leagueName){
        switch (leagueName.ToLower())
        {
            case "nfl":
            return new NFLLeague();
            case "mlb":
            return new MLBLeague();
        }
        throw new KeyNotFoundException("Not a valid league");
    }
}


public class NFLLeague : ILeague
{
    public LeagueNames name => LeagueNames.NFL;
    public HashSet<Positions> validPosition => new HashSet<Positions>{

        Positions.QB,Positions.WR, Positions.RB, Positions.TE, Positions.K, Positions.P, Positions.KR, Positions.PR
    };


    public bool AllowedPosition(Positions position){
        {
        return validPosition.Contains(position); 
    }

}
}

public class MLBLeague : ILeague
{
    public LeagueNames name => LeagueNames.MLB;
    public HashSet<Positions> validPosition => new HashSet<Positions>{

    Positions.SP,

    Positions.RP,

    Positions.C,

    Positions.SS,

    Positions.LF,

    Positions.RF,

    Positions.CF,

    Positions.DH,

    Positions._1B,

    Positions._2B,

    Positions._3B
    };

    public bool AllowedPosition(Positions position){
        return validPosition.Contains(position); 
    }
}
