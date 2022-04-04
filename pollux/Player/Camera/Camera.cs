using Sandbox;
using Source1;

namespace Pollux.Player.Camera
{
	partial class PolluxCamera : Source1Camera
	{
		[ClientVar] public static new float plx_viewmodel_fov { get; set; } = 54;

		public override void Update()
		{
			base.Update();
			ViewModelFieldOfView = plx_viewmodel_fov;
		}
	}
}
