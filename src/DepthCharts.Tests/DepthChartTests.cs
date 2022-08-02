using Xunit;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
namespace DepthCharts.Tests;

public class DepthChartTests
{
    [Fact]
    public void LeagueCanBeCreateFromFactory()
    {
        var leagueFactory = new LeagueFactory();
        var nflLeague = leagueFactory.GetLeague("nfl");
        nflLeague.Should().BeOfType<NFLLeague>();
        nflLeague.Should().NotBeOfType<MLBLeague>();
    }
    [Fact]
    public void LeagueDoesNotAllowTeamsToAddPositionsOtherThanDefined()
    {
        var leagueFactory = new LeagueFactory();
        var nflLeague = leagueFactory.GetLeague("nfl");
        var player1 = new Player()
        {
            Name = "Player1",
            // Position = Positions.QB,
            Id = 50
        };

        var team1 = new Team(nflLeague);

        // act
        Action act = () => team1.AddPlayerToDepthChart(player1, Positions.RF, 0);
        act.Should().Throw<LeageDoesNotAllowPositionException>();
    }
    // [Fact]
    // public void AddSamePlayerToDepthChartRaiseException()
    // {
    //     var player1 = new Player()
    //     {
    //         Name = "Player1",
    //         // Position = Positions.QB,
    //         Id = 50
    //     };
    //     var player2 = new Player()
    //     {
    //         Name = "Player2",
    //         // Position = Positions.WR,
    //         Id = 90
    //     };

    //     var leagueFactory = new LeagueFactory();
    //     var nflLeague = leagueFactory.GetLeague("nfl");
    //     var team1 = new Team(nflLeague);

    //     // Act
    //     team1.AddPlayerToDepthChart(player1, Positions.QB, 0);
    //     team1.AddPlayerToDepthChart(player2, Positions.WR, 0);
    //     // act
    //     Action act = () => team1.AddPlayerToDepthChart(player2, Positions.WR, 0);
    //     act.Should().Throw<SamePlayerAddedToTeam>();

    // }
    [Fact]
    public void AddPlayerToDepthChartForAGivenPosition()
    {
        // Arrange
        StringBuilder sb = new StringBuilder();
        sb.Append("QB: [50]");
        sb.Append(Environment.NewLine);
        sb.Append("WR: [90]");

        var expected = sb.ToString();

        var player1 = new Player()
        {
            Name = "Player1",
            // Position = Positions.QB,
            Id = 50
        };
        var player2 = new Player()
        {
            Name = "Player2",
            // Position = Positions.WR,
            Id = 90
        };

        var leagueFactory = new LeagueFactory();
        var nflLeague = leagueFactory.GetLeague("nfl");
        var team1 = new Team(nflLeague);

        // Act
        team1.AddPlayerToDepthChart(player1, Positions.QB, 0);
        team1.AddPlayerToDepthChart(player2, Positions.WR, 0);

        var actual = team1.GetFullDepthChart();

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void RemovePlayerFromDepthChart()
    {
        // Arrange
        StringBuilder sb = new StringBuilder();
        sb.Append("QB: [50]");
        sb.Append(Environment.NewLine);
        sb.Append("WR: [90]");

        var expected = sb.ToString();

        var player1 = new Player()
        {
            Name = "Player1",
            Id = 50
        };
        var player2 = new Player()
        {
            Name = "Player2",
            Id = 90
        };
        var player3 = new Player()
        {
            Name = "Player3",
            Id = 91
        };

        var leagueFactory = new LeagueFactory();
        var nflLeague = leagueFactory.GetLeague("nfl");
        var team1 = new Team(nflLeague);

        // Act
        team1.AddPlayerToDepthChart(player1, Positions.QB, 0);
        team1.AddPlayerToDepthChart(player2, Positions.WR, 0);
        team1.AddPlayerToDepthChart(player3, Positions.WR, 0);

        // remove player3
        team1.RemovePlayerFromDepthChart(player3, Positions.WR);
        var actual = team1.GetFullDepthChart();

        // Assert
        actual.Should().Be(expected);
    }
    [Fact]
    public void ShouldReturnCorrectPlayerCountInATeam()
    {
        // Given
        var player1 = new Player()
        {
            Name = "Player1",
            Id = 50
        };
        var player2 = new Player()
        {
            Name = "Player2",
            Id = 90
        };
        var player3 = new Player()
        {
            Name = "Player3",
            Id = 91
        };

        var leagueFactory = new LeagueFactory();
        var nflLeague = leagueFactory.GetLeague("nfl");
        var team1 = new Team(nflLeague);

    
        // When
        team1.AddPlayerToDepthChart(player1, Positions.QB);
        team1.AddPlayerToDepthChart(player2, Positions.QB);
    
        // Then
        team1.PlayerCount().Should().Be(2);
    }

    [Fact]
    public void SamePositionAnotherPlayerIntoChartGivesCorrectOrder()
    {
        // Arrange
        StringBuilder sb = new StringBuilder();
        sb.Append("QB: [50]");
        sb.Append(Environment.NewLine);
        sb.Append("WR: [91,90]");

        var expected = sb.ToString();

        var player1 = new Player()
        {
            Name = "Player1",
            Id = 50
        };
        var player2 = new Player()
        {
            Name = "Player2",
            Id = 90
        };
        var player3 = new Player()
        {
            Name = "Player3",
            Id = 91
        };

        var leagueFactory = new LeagueFactory();
        var nflLeague = leagueFactory.GetLeague("nfl");
        var team1 = new Team(nflLeague);

        // When
        team1.AddPlayerToDepthChart(player1, Positions.QB, 0);
        team1.AddPlayerToDepthChart(player2, Positions.WR, 0);
        team1.AddPlayerToDepthChart(player3, Positions.WR, 0);

        // then
        var actual = team1.GetFullDepthChart();

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void SamePositionAnotherPlayerIntoChartGivesCorrectOrder2()
    {
        // Arrange
        StringBuilder sb = new StringBuilder();
        sb.Append("WR: [2,1,3]");
        sb.Append(Environment.NewLine);
        sb.Append("KR: [1]");

        var expected = sb.ToString();
        var expectedPlayersUnderAlice = "WR: [1,3]";

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
        var nflLeague = leagueFactory.GetLeague("nfl");
        var team1 = new Team(nflLeague);

        // When
        team1.AddPlayerToDepthChart(bob, Positions.WR, 0);
        team1.AddPlayerToDepthChart(alice, Positions.WR, 0);
        team1.AddPlayerToDepthChart(charlie, Positions.WR, 2);
        team1.AddPlayerToDepthChart(bob, Positions.KR);

        // then
        var actual = team1.GetFullDepthChart();
        var actualPlayersUnderAlice = team1.GetPlayersUnderPlayerInDepthChart(alice, Positions.WR);

        // Assert
        actual.Should().Be(expected);
        actualPlayersUnderAlice.Should().Be(expectedPlayersUnderAlice);
    }
}