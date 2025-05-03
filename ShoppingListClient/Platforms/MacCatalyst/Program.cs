using ObjCRuntime;
using UIKit;
using SQLitePCL;

namespace ShoppingListClient;

public class Program
{
	// This is the main entry point of the application.
	static void Main(string[] args)
	{
        // Initialize SQLite native library before app start
        SQLitePCL.Batteries_V2.Init();
        // Launch the application (specify AppDelegate)
        UIApplication.Main(args, null, typeof(AppDelegate));
	}
}
