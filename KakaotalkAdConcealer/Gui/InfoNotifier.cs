using Microsoft.Toolkit.Uwp.Notifications;

namespace KakaotalkAdConcealer.Gui
{
	public class InfoNotifier
	{
		public static void ShowRunningNotification()
		{
			new ToastContentBuilder()
				.AddText("Goodbye, Ads", hintMaxLines: 1)
				.AddText("Ads blocker is running now!")
				.Show();
		}
	}
}
