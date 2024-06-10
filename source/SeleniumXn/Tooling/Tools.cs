using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using System.Threading;



namespace SeleniumXn.Tooling
{


    public class Tools
    {
        Random rand = new Random();


        /// <summary>
        /// <para>Mimicks human delay in actions by pausing for a random time between 2 given times. </para>
        /// <para>maxSec = Max Wait time in Seconds</para>
        /// <para>minSec =  Min Wait time in Seconds</para>
        /// </summary>
        public void RSleep(int minSec = 4, int maxSec = 7)
        {
            if (minSec >= maxSec)
            {
                throw new Exception("The minSec must be less than the maxSec wait times. ");
            }
            else
            {
                Thread.Sleep(rand.Next(minSec * 1000, maxSec * 1000));
            }
        }
        /// <summary>
        /// <para>Waits for the HTML Page to load.</para>
        /// <para>Only used in Debugging</para>
        /// <para>Depreciated</para>
        /// </summary>
        /// <returns>IWebElement</returns>
        public IWebElement WaitForHTMLLoad(IWebDriver driver)
        {
            string[] elementsPossible = ["html", "body", "title", "div"];
            IWebElement element;
            foreach(string possible in elementsPossible)
            {
                By locator = By.XPath($"//{possible}");
                try {
                    element = WaitForElement(driver, locator, 1000, 4000);
                    Console.WriteLine($"Found {possible}");
                    return element;
                }
                catch (Exception e){
Console.WriteLine($"Could Not find by {possible}.");
                }

            }
            throw new Exception("Failed to Detect Page Load");

        }

        /// <summary>
        /// <para>Waits for the element to be loaded then returns the element</para>
        /// <para>If the element cannot be found, returns an IWebElement with a value of Null</para>
        /// <para>driver = the IWebDriver to access</para>
        /// <para>by = the "By" to use as a locator. (IE: by:By.Id("someID"))</para>
        /// <para>maxAttempts = the maximum number of attempts to find the element (optional | default = 10)</para>
        /// <para>timeBetweenAttempts = the time in ms between attempts to find the element (optional | default = 2000)</para>
        /// </summary>
        /// <returns>IWebDriver</returns>
        public IWebElement WaitForElement(IWebDriver driver, By by, int maxAttempts = 10, int timeBetweenAttempts = 2000)
        {
            IWebElement element;
            int currentAttempt = 0;
            do
            {
                if (currentAttempt > 0)
                {
                    Thread.Sleep(timeBetweenAttempts);
                }
                try
                {
                    element = driver.FindElement(by);
                    return element;
                }
                catch
                {
                    currentAttempt++;
                    Console.WriteLine("Retry #:\t" + currentAttempt.ToString());
                    continue;
                }


            }
            while (currentAttempt < maxAttempts);


            return null;
        }


        /// <summary>
        /// <para>This returns True if one of the By Options is found. </para>
        /// <para>driver = the IWebDriver used</para>
        /// <para>byList = a list of all By Options you want to check for</para>
        /// </summary>
        /// <returns>true if 1 of the "by" options is found</returns>
        public bool ElementFoundOr(IWebDriver driver, By[] byList)
        {
            bool elementFound = false;
            foreach (var item in byList)
            {
                try
                {
                    driver.FindElement(item);
                    elementFound = true;
                    break;
                }
                catch
                {
                    elementFound = false;
                }

            }
            return elementFound;
        }

        /// <summary>
        /// <para>This returns True if one of the By Options is found. </para>
        /// <para>driver = the IWebDriver used</para>
        /// <para>xpathList = a list of all xpaths you want to check for</para>
        /// </summary>
        /// <returns>true if 1 of the "xpaths" is found</returns>
        public bool ElementFoundOr(IWebDriver driver, params string[] xpathList)
        {
            bool elementFound = false;
            foreach (var item in xpathList)
            {
                try
                {
                    driver.FindElement(By.XPath(item));
                    elementFound = true;
                    break;
                }
                catch
                {
                    elementFound = false;
                }

            }
            return elementFound;
        }


        /// <summary>
        /// <para>This returns True if one of the By Options is found. </para>
        /// <para>driver = the IWebDriver used</para>
        /// <para>xpathList = a list of all xpaths you want to check for</para>
        /// </summary>
        /// <returns>true if 1 of the "xpaths" is found</returns>
        public bool ElementFoundOr(IWebDriver driver, List<string> xpathList)
        {
            bool elementFound = false;
            foreach (var item in xpathList)
            {
                try
                {
                    driver.FindElement(By.XPath(item));
                    elementFound = true;
                    break;
                }
                catch
                {
                    elementFound = false;
                }

            }
            return elementFound;
        }




        /// <summary>
        /// <para>Searches for a single element using the "By"</para>
        /// <para>IE: By.Xpath("//div[@class='banner']") will search for 1 div tag with a class of banner </para>
        /// <para>driver = the IWebDriver to access</para>
        /// <para>by = the "By" to use as a locator. (IE: by:By.Id("someID"))</para>
        /// </summary>
        /// <returns>bool</returns>
        public bool ElementFound(IWebDriver driver, By by)
        {

            try
            {
                driver.FindElement(by);

                return true;

            }
            catch
            {
                return false;
            }


        }




        /// <summary>
        /// <para>Searches for a single element using the "By"</para>
        /// <para>driver = the IWebDriver to access</para>
        /// <para>xpath = the xpath of the element to use as a locator. </para>
        /// </summary>
        /// <returns>bool</returns>
        public bool ElementFound(IWebDriver driver, string xpath)
        {
            try
            {
                driver.FindElement(By.XPath(xpath));

                return true;

            }
            catch
            {
                return false;
            }



        }
        /// <summary>
        /// <para>Checks to see if a list of elements can be found by Xpath</para>
        /// <para>driver = the IWebDriver to access</para>
        /// <para>xpath = receives XPaths to all elments that must be present.</para>
        /// </summary>
        /// <returns>bool</returns>
        public bool ElementsFound(IWebDriver driver, params string[] xpath)
        {
            By locator;

            foreach (var xp in xpath)
            {
                locator = By.XPath(xp);
                if (!ElementFound(driver, locator))
                {
                    return false;
                }

            }
            return true;

        }
        /// <summary>
        /// <para>Checks to see if a list of elements can be found by Xpath</para>
        /// <para>driver = the IWebDriver to access</para>
        /// <para>xpath = receives a string list of XPaths to all elments that must be present.</para>
        /// </summary>
        /// <returns>bool</returns>
        public bool ElementsFound(IWebDriver driver, List<string> xpath)
        {
            By locator;

            foreach (var xp in xpath)
            {
                locator = By.XPath(xp);
                if (!ElementFound(driver, locator))
                {
                    return false;
                }

            }
            return true;

        }


        /// <summary>
        /// <para>This searches for all instances of an element where the ID starts with a string.</para>
        /// <para>driver = the IWebDriver being used</para>
        /// <para>elementIDPrefix = the prefix for the element ID. (IE: rqOption will populate rqOption0, rqOption1, etc)</para>
        /// <para>max2Find = the maximum items to look for. (Default 20)</para>
        /// <para>For Instance: elementID: "option" will return a list with "option0","option1", etc</para>
        /// </summary>
        /// <returns>List{string}</returns>
        public List<string> GetOptionsByID(IWebDriver driver, string elementIDPrefix, int max2Find = 20)
        {
            List<string> optionsFound = new List<string>();

            for (int i = 0; i <= max2Find; i++)
            {
                if (ElementFound(driver, By.Id(elementIDPrefix + i.ToString())))
                {
                    optionsFound.Add(elementIDPrefix + i.ToString());
                }
            }
            return optionsFound;
        }


        /// <summary>
        /// <para>This will close multiple tabs preserving only the base tab (WindowHandles[0])</para>
        /// <para>driver = the IWebDriver you will be using</para>
        /// <para>num2Close = the number of tabs to close (Default: 1)</para>
        /// <para>NOTE: This will return false if unable to close the requested number of tabs, but will not throw an exception</para>
        /// </summary>
        /// <returns>bool - true if all closed successfully, else false</returns>
        public bool CloseTab(IWebDriver driver, int num2Close = 1)
        {
            for (int i = 0; i < num2Close; i++)
            {
                try
                {
                    driver.SwitchTo().Window(driver.WindowHandles[1]);
                    driver.Close();
                    driver.SwitchTo().Window(driver.WindowHandles[0]);
                }
                catch
                {
                    return false;
                }
            }
            return true;

        }


    }
}
