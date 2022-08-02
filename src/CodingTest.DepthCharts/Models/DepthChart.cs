using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

public class DepthChart
{
    private Dictionary<string, List<Player>> depthMap;
    private HashSet<int> _allPlayerIds;
    public DepthChart()
    {
        depthMap = new Dictionary<string, List<Player>>();
        _allPlayerIds = new HashSet<int>();
    }
    public void AddPlayer(Player player, Positions position, int? positionDepth = null)
    {
        /*
        uncomment if we need to make sure that only one player can be added
        if (_allPlayerIds.Contains(player.Id))
        {
           throw new SamePlayerAddedToTeam($"Same player {player.Name} with id {player.Id} added twice.") ;
        }
        */
        if (depthMap.TryGetValue(position.ToString(), out List<Player> playersDepth))
        {
            if (IsPositionDepthValid(positionDepth, playersDepth))
            {
                // valid position just add it
                playersDepth.Insert(positionDepth.GetValueOrDefault(), player);
            }
            else
            {
                // Here just add at the last slot either it is null or last anyways
                playersDepth.Add(player);
            }
        }
        else
        {
            playersDepth = new List<Player>();
            playersDepth.Add(player);
            depthMap.Add(position.ToString(), playersDepth);
        }
        // finally add this to the hashset
        _allPlayerIds.Add(player.Id);
    }

    internal int PlayerCount()
    {
        return _allPlayerIds.Count();
    }

    private bool IsPositionDepthValid(int? positionDepth, List<Player> playersDepth)
    {
        return positionDepth != null
                && positionDepth <= playersDepth.Count - 1
                && positionDepth >= 0;
    }

    public string GetPrintableDepthChart()
    {
        var answer = new StringBuilder();
        foreach (var pos in depthMap)
        {
            // Skip if no values available for position 
            if (!pos.Value.Any())
            {
                continue;
            }

            // Return string of comma seperated ids
            var playerIds = string.Join(",", pos.Value.Select(x => x.Id));

            answer.Append($"{pos.Key}: [{playerIds}]");
            answer.Append(Environment.NewLine);

        }
        return answer.ToString().Trim();
    }
    public void RemovePlayerFromDepthChart(Player player, Positions position)
    {
        if (depthMap.TryGetValue(position.ToString(), out List<Player> values))
        {
            values.Remove(player);
        }
    }
    public string GetPlayersUnderPlayerInDepthChart(Player player, string position)
    {
        StringBuilder sb = new StringBuilder();

        if (depthMap.TryGetValue(position, out List<Player> values))
        {
            var index = values.IndexOf(player);

            // Return string of comma seperated playerIds
            var playerIds = string.Join(",", values.Skip(index + 1).Select(x => x.Id));

            sb.Append($"{position}: [{playerIds}]");
        }

        return sb.ToString().Trim();
    }


}