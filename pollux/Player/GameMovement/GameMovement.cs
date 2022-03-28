using Sandbox;
using Source1;

namespace ProjectPollux.Player.GameMovement;

public partial class PolluxGameMovement : Source1GameMovement
{
	PolluxPlayer Player { get; set; }

	public override void PlayerMove()
	{
		base.PlayerMove();

		SimulateSprinting();
	}
}
