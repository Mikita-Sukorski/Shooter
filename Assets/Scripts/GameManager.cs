using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private static Dictionary<string, Player> _players = new Dictionary<string, Player>();

	private const string PLAYER_ID = "Player ";

	public static void RegisterPlayer(string _netId, Player player)
	{
		string playerId = PLAYER_ID + _netId;
		_players.Add(playerId, player);

		player.transform.name = playerId;
	}

	public static void UnRegisterPlayer(string playerId)
	{
		_players.Remove(playerId);
	}

	public static Player GetPlayer(string playerId)
	{
		return _players[playerId];
	}
}
