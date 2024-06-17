

using SeleniumXn.Tooling;
using System.Threading;


BrowserControls bc = new BrowserControls("google");

bc.NavTo("https://www.gamestop.com/search/?store=0681");

//bc.FindElement("//button/span[text()='Menu']/parent::button");
bc.FindElement("//a[contains(text(),'Change')][contains(text(),'Store')]");
bc.Click();
bc.FindElement("store-postal-code", "id");
bc.SetAttribute("value", "56303");

bc.FindElement("//form[contains(@class,'store-locator')]//button[@type='submit']");



Thread.Sleep(100000);


