// based on https://github.com/AmperSoftware/Sbox-Source1-Base/blob/11b5ea9e60630af3d1f36d65296f80aa90a9ce56/GameMovement/GameMovement.Duck.cs, modified to handle sprinting.
using Sandbox;
using static ProjectPollux.PolluxPlayer;

namespace ProjectPollux.Player.GameMovement;

public partial class PolluxGameMovement
{
	public bool IsSprinted => Player.Tags.Has( PlayerTags.Sprinted );
	public bool IsSprinting => SprintTime > 0;

	public float TimeToSprint => .1f;
	public float SprintTime { get; set; }

	public virtual void SimulateSprinting()
	{
		if ( WishSprint() )
		{
			OnSprinting();
		}
		else
		{
			OnUnsprinting();
		}

		if ( Pawn.Tags.Has( PlayerTags.Sprinted ) )
			SetTag( PlayerTags.Sprinted );
	}

	public virtual void OnSprinting()
	{
		if ( !CanSprint() )
			return;

		if ( !IsSprinted && SprintTime >= TimeToSprint || IsInAir )
		{
			OnFinishedSprinting();
		}

		if ( SprintTime < TimeToSprint )
		{
			SprintTime += Time.Delta;

			if ( SprintTime > TimeToSprint )
				SprintTime = TimeToSprint;
		}
	}

	public virtual void OnUnsprinting()
	{
		if ( IsSprinted && SprintTime == 0 || IsInAir )
		{
			OnFinishedUnsprinting();
		}

		if ( SprintTime > 0 )
		{
			SprintTime -= Time.Delta;

			if ( SprintTime < 0 )
				SprintTime = 0;
		}
	}

	public virtual void OnFinishedSprinting()
	{
		if ( Pawn.Tags.Has( PlayerTags.Sprinted ) )
			return;

		Pawn.Tags.Add( PlayerTags.Sprinted );
		SprintTime = TimeToSprint;

		Sound.FromScreen("SuitSounds.SprintStart");

		Player.AuxPowerLoad += 25f;

		if ( IsGrounded )
		{
			Position -= GetPlayerMins( true ) - GetPlayerMins( false );
		}
		else
		{
			var hullSizeNormal = GetPlayerMaxs( false ) - GetPlayerMins( false );
			var hullSizeCrouch = GetPlayerMaxs( true ) - GetPlayerMins( true );
			var viewDelta = hullSizeNormal - hullSizeCrouch;
			Position += viewDelta;
		}
	}

	public virtual void OnFinishedUnsprinting()
	{
		if ( !Pawn.Tags.Has( PlayerTags.Sprinted ) )
			return;

		Player.Tags.Remove( PlayerTags.Sprinted );

		Player.AuxPowerLoad -= 25f;

		SprintTime = 0;
	}

	public override bool CanSprint()
	{
		if ( Player != Pawn )
		{
			var newPlayer = Pawn as PolluxPlayer;
			PawnChanged( newPlayer, Player );
			Player = newPlayer;
		}
		if ( Player.IsSuitEquipped )
		{
			if ( Player.AuxPower < 10 ) return false;
			else return true;
		}
		else return false;
	}


}
