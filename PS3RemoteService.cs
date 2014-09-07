using System;

namespace PS3BluMote
{

	public class PS3RemoteService
	{

		public event EventHandler<ButtonData> OnButtonDown;
		public event EventHandler<EventArgs> OnButtonReleased;
		public event EventHandler<RemoteStateData> OnConnected;
		public event EventHandler<RemoteStateData> OnDisconnected;
		public event EventHandler<RemoteStateData> OnBatteryLifeChanged;


		public PS3RemoteService(ModelXml model)
		{
			Model = model;
		}


		public void Run()
		{
		}


		public void SetRemote(int vendorId, int productId)
		{
			try {
				//var vendorId = int.Parse(txtVendorId.Text.Remove(0, 2), System.Globalization.NumberStyles.HexNumber);
				//var productId = int.Parse(txtProductId.Text.Remove(0, 2), System.Globalization.NumberStyles.HexNumber);
				Remote = new PS3Remote(vendorId, productId);

				Remote.ButtonDown += Remote_ButtonDown;
				Remote.ButtonReleased += Remote_ButtonReleased;

				Remote.ButtonDown += OnButtonDown;
				Remote.ButtonReleased += OnButtonReleased;
				Remote.Connected += OnConnected;
				Remote.Disconnected += OnDisconnected;
				Remote.BatteryLifeChanged += OnBatteryLifeChanged;

				Remote.Connect();

			} catch {
				//MessageBox.Show(
				//	"An error occured whilst attempting to load the remote.",
				//	"PS3BluMote: Remote error!",
				//	MessageBoxButtons.OK,
				//	MessageBoxIcon.Error
				//);
			}
		}


		private void Remote_ButtonDown(object sender, ButtonData e)
		{
			throw new NotImplementedException();
		}


		private void Remote_ButtonReleased(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}


		protected ModelXml Model;
		protected PS3Remote Remote;

	}

}