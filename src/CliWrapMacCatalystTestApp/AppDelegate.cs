using System.Diagnostics;
using CliWrap;
using CliWrap.Buffered;

namespace CliWrapMacCatalystTestApp;

[Register ("AppDelegate")]
public class AppDelegate : UIApplicationDelegate {
	public override UIWindow? Window {
		get;
		set;
	}

	public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
	{
		// create a new window instance based on the screen size
		Window = new UIWindow (UIScreen.MainScreen.Bounds);

		// create a UIViewController with a single UILabel
		var vc = new UIViewController ();
		vc.View!.AddSubview (new UILabel (Window!.Frame) {
			BackgroundColor = UIColor.SystemBackground,
			TextAlignment = UITextAlignment.Center,
			Text = "Hello, Mac Catalyst!",
			AutoresizingMask = UIViewAutoresizing.All,
		});
		Window.RootViewController = vc;

		// make the window visible
		Window.MakeKeyAndVisible ();
		
		
		
		var process = new Process();
		process.StartInfo.UseShellExecute = false;
		process.StartInfo.RedirectStandardOutput = true;
		process.StartInfo.FileName = "date";
		process.Start();
		string output = process.StandardOutput.ReadToEnd();
		process.WaitForExit();
        
		Debug.WriteLine("## System.Diagnostics.Process ##");
		Debug.WriteLine(output);

		Task.Run(async () =>
		{
			var result = await Cli.Wrap("date")
				.ExecuteBufferedAsync();

			Debug.WriteLine("## StandardOutput ##");
			Debug.WriteLine(result.StandardOutput);
			Debug.WriteLine("## StandardError ##");
			Debug.WriteLine(result.StandardError);
			Debug.WriteLine("## ##");
		});
		
		return true;
	}
}
