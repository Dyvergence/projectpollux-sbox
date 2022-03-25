using ProjectPollux;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

partial class PolluxCamera : Source1.Source1Camera
{
	Vector3 LastPosition { get; set; }
	Rotation LastRotation { get; set; }
	public override void Update()
	{
		var player = PolluxPlayer.LocalPlayer;
		if ( player == null ) return;

		Viewer = player;
		Position = player.EyePosition;
		Rotation = player.EyeRotation;
		FieldOfView = 90f;

		CalculatePlayerView( player );

		LastPosition = Position;
		LastRotation = Rotation;
	}
}
