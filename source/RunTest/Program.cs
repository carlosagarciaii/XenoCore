

using SeleniumXn.Tooling;
using System.Threading;


BrowserControls bc = new BrowserControls("google");

bc.NavTo("https://www.gamestop.com");


Thread.Sleep(100000);


