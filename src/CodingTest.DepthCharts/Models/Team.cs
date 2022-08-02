/*
Team is prepared to play for a League. 
has number of players arranged in a depthChart 
*/

using System;

public class Team{
    ILeague league {get; set;}
    DepthChart depthChart{get; set;}
    public Team(ILeague assginedLeague){
        league = assginedLeague;
        depthChart = new DepthChart();
    }
    public void AddPlayerToDepthChart(Player player, Positions position, int? depthPosition=null){
        if (league.AllowedPosition(position)){

        this.depthChart.AddPlayer(player, position, depthPosition);
        }
        else
        {
            throw new LeageDoesNotAllowPositionException($"League {league.name} does not allow {position}");
        }
    }
    public string GetFullDepthChart()
    {
        return this.depthChart.GetPrintableDepthChart();
    }
    public int PlayerCount()
    {
        return this.depthChart.PlayerCount();
    }

    public string GetPlayersUnderPlayerInDepthChart(Player player, Positions position)
    {
        return this.depthChart.GetPlayersUnderPlayerInDepthChart(player, position.ToString());
    }
    public void RemovePlayerFromDepthChart(Player player, Positions position)
    {
        this.depthChart.RemovePlayerFromDepthChart(player, position);
    }
}