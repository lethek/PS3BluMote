using System.Threading;

namespace PS3BluMote
{

	public class ControlThread
	{

		public ControlThread(CancellationToken cancellationToken)
		{
			Current = new Thread(OnRun);
			_cancellationToken = cancellationToken;
		}

		public void Start()
		{
			if (!Current.IsAlive) {
				Current.Start();
			} else {
				//TODO: reload config?
			}
		}

		protected void OnRun()
		{
			while (!_cancellationToken.IsCancellationRequested)
			{
				Thread.Sleep(1);
				//
			}
		}

		protected readonly Thread Current;

		private readonly CancellationToken _cancellationToken;

	}

}
