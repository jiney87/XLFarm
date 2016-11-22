using UnityEngine;
using System.Collections;

public class Message_BoatQuoteOver : Message {

	public int playerId;

	public static Message_BoatQuoteOver create(int playerId)
	{
		Message_BoatQuoteOver msg = new Message_BoatQuoteOver ();
		msg.type = MsgType.BoatQuoteOver;
		msg.playerId = playerId;
		return msg;
	}
}
