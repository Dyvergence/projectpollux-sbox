namespace ProjectPollux;

partial class PolluxCamera : Source1.Source1Camera
{
	public override void Update()
	{
		base.Update();
		ViewModelFieldOfView = 0f;
	}
}
