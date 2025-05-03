using ObjCRuntime;
using UIKit;
using SQLitePCL;

namespace ShoppingListClient;

public class Program
{
	// This is the main entry point of the application.
	static void Main(string[] args)
	{
		// Initialize native SQLite library before app launch
		SQLitePCL.Batteries_V2.Init();
		// if you want to use a different Application Delegate class from "AppDelegate"
		// you can specify it here.
		UIApplication.Main(args, null, typeof(AppDelegate));
	}
}
