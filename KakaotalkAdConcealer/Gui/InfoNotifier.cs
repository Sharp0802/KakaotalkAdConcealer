using KakaotalkAdConcealer.Properties;
using Microsoft.Toolkit.Uwp.Notifications;

namespace KakaotalkAdConcealer.Gui
{
	/// <summary>
	/// Helper for showing toast notification
	/// </summary>
	public class InfoNotifier
	{
		/// <summary>
		/// Show notification for startup
		/// </summary>
		public static void ShowRunningNotification()
		{
			new ToastContentBuilder()
				.AddText(Resources.GoodbyeAds, hintMaxLines: 1)
				.AddText(Resources.RunningNow)
				.Show();
		}
	}
}
