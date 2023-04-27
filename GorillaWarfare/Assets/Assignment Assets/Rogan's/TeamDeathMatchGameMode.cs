using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Free : MonoBehaviour
{
    public int numPlayersPerTeam = 4;
    public int numTeams = 2;

    private List<GameObject> players = new List<GameObject>();
    private Dictionary<GameObject, int> playerTeams = new Dictionary<GameObject, int>();
    private Dictionary<int, int> teamKills = new Dictionary<int, int>();

    void Start()
    {
        // Find all player objects in the scene and add them to the list of players
        GameObject[] playerObjs = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject playerObj in playerObjs)
        {
            players.Add(playerObj);
        }

        // Randomly assign each player to a team
        for (int i = 0; i < players.Count; i++)
        {
            int team = Random.Range(1, numTeams + 1);
            playerTeams.Add(players[i], team);
        }

        // Initialize team kill counts to 0
        for (int i = 1; i <= numTeams; i++)
        {
            teamKills.Add(i, 0);
        }
    }

    void Update()
    {
        // Check if any players have been eliminated
        List<GameObject> playersToRemove = new List<GameObject>();
        foreach (GameObject playerObj in players)
        {
            if (IsPlayerEliminated(playerObj))
            {
                // Remove the player from the game
                playersToRemove.Add(playerObj);
                Destroy(playerObj);
            }
        }
        foreach (GameObject playerToRemove in playersToRemove)
        {
            players.Remove(playerToRemove);
        }

        // Check if there is a winning team
        int winningTeam = DetermineWinningTeam();
        if (winningTeam != 0)
        {
            // End the game and declare the winning team
            Debug.Log("Team " + winningTeam + " has won the game!");
        }
    }

    public bool IsPlayerEliminated(GameObject playerObj)
    {
        // Insert logic here to determine if the player has been eliminated
        return false; // Replace with your own logic
    }

    public int DetermineWinningTeam()
    {
        // Reset team kill counts to 0
        foreach (int team in teamKills.Keys)
        {
            teamKills[team] = 0;
        }

        // Tally up the kills for each team
        foreach (GameObject playerObj in players)
        {
            if (!IsPlayerEliminated(playerObj))
            {
                int team = playerTeams[playerObj];
                teamKills[team]++;
            }
        }

        // Determine the winning team based on the team kill counts
        int maxKills = 0;
        int winningTeam = 0;
        foreach (KeyValuePair<int, int> pair in teamKills)
        {
            if (pair.Value > maxKills)
            {
                maxKills = pair.Value;
                winningTeam = pair.Key;
            }
        }
        if (maxKills == 0)
        {
            // If no team has any kills, return 0 to indicate that there is no winning team yet
            return 0;
        }
        else
        {
            return winningTeam;
        }
    }
}


