using AppKit;
using Microsoft.MobileBlazorBindings.WebView.macOS;

namespace TackleStore.Clients.macOS
{
    internal static class MainClass
    {
        private static void Main(string[] args)
        {
            BlazorHybridMacOS.Init();
            NSApplication.Init();
            NSApplication.SharedApplication.Delegate = new AppDelegate();
            NSApplication.Main(args);
        }
    }
}
