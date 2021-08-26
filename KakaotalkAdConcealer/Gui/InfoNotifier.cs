using KakaotalkAdConcealer.Properties;
using Microsoft.Toolkit.Uwp.Notifications;

namespace KakaotalkAdConcealer.Gui
{
	public class InfoNotifier
	{
		public static void ShowRunningNotification()
		{
			new ToastContentBuilder()
				.AddText(Resources.GoodbyeAds, hintMaxLines: 1)
				.AddText(Resources.RunningNow)
				.Show();
		}
	}
}
