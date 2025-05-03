using ObjCRuntime;
using UIKit;
using SQLitePCL;

namespace ShoppingListClient;

public class Program
{
	// This is the main entry point of the application.
	static void Main(string[] args)
	{
		// if you want to use a different Application Delegate class from "AppDelegate"
		// you can specify it here.
		UIApplication.Main(args, null, typeof(AppDelegate));
#if DEBUG
    SQLitePCL.Batteries_V2.Init();
#endif
	}
}
