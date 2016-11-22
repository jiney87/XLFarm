using UnityEngine;
using System.Collections;

public class Message_BoatQuote : Message {

	public int playerId;
	public float quoteTime;

	public static Message_BoatQuote create(int playerId,float quoteTime)
	{
		Message_BoatQuote msg = new Message_BoatQuote ();
		msg.type = MsgType.BoatQuote;
		msg.playerId = playerId;
		msg.quoteTime = quoteTime;
		return msg;
	}
}
