using System;

namespace CodingTest.DepthCharts
{
    class Program
    {
        static void Main(string[] args)
        {
            var bob = new Player()
            {
                Name = "Bob",
                Id = 1
            };
            var alice = new Player()
            {
                Name = "Alice",
                Id = 2
            };
            var charlie = new Player()
            {
                Name = "Charlie",
                Id = 3
            };
            var leagueFactory = new LeagueFactory();
            // create league
            var nflLeague = leagueFactory.GetLeague("nfl");
            // create team            
            var team1 = new Team(nflLeague);
            // add players to team
            team1.AddPlayerToDepthChart(bob, Positions.QB, 0);
            team1.AddPlayerToDepthChart(alice, Positions.WR, 0);
            // sample operations
            Console.WriteLine($" NFL leage allow QB? ==> {nflLeague.AllowedPosition(Positions.QB)}");
            Console.WriteLine($" depthChart ==> {team1.GetFullDepthChart()}");
            Console.WriteLine($" players under alice zero here ==> {team1.GetPlayersUnderPlayerInDepthChart(alice, Positions.WR)}");
            team1.AddPlayerToDepthChart(bob, Positions.WR, 1);
            Console.WriteLine($" players under alice should be 1 here ==> {team1.GetPlayersUnderPlayerInDepthChart(alice, Positions.WR)}");
            // remove player
            team1.RemovePlayerFromDepthChart(alice, Positions.WR);
            Console.WriteLine($"depthChart after removing alice ==> {team1.GetFullDepthChart()}");
            Console.WriteLine($" players under alice zero here ==> {team1.GetPlayersUnderPlayerInDepthChart(alice, Positions.WR)}");
        }
    }
}
