/*
League has a name (NFL etc)
and a valid supporting positions. 

*/
using System.Collections.Generic;

public interface ILeague
    {
        LeagueNames name {get;}
        HashSet<Positions> validPosition { get; }

        bool AllowedPosition(Positions position);

}