
using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using System.Threading;

using Newtonsoft.Json;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System.Reflection;
using System.Text.RegularExpressions;
using XenoCore.Logging;


namespace SeleniumXn.Tooling
{
    public class SessionManager
    {

        Random rand = new Random();


        private string logMessage = "";
        private IWebDriver driver { get; set; }
        private string browserName { get; set; }

        private string driverFilePath { get; set; }

        Logger logger = new Logger();






        public IWebDriver GetDriver()
        {
            return driver;
        }


        //  ---------------------------------------------------------------
        //  CREATING A SESSION
        //  ---------------------------------------------------------------
        //  ---------------------------------------------------------------

        #region CreateSession

        /// <summary>
        /// <para>Returns a String for the filename of the WebDriver</para>
        /// <para>browserName = The name of the browser
        /// <br>---Gecko = FF or FireFox</br>
        /// <br>---Chrome = Google or Chrome</br>
        /// <br>---IE = IE or IExplore</br>
        /// <br>---MSEdge = Edge or MSEdge</br></para>
        /// </summary>
        /// <returns>String value for the driver's file name</returns>
        private void SetBrowserDriver()
        {
            string funcName = MethodBase.GetCurrentMethod().Name;

            switch (browserName.ToLower())
            {
                case "ff":
                case "firefox":
                    var ffOptions = new FirefoxOptions();

                    ffOptions.AddArgument("--headless"); // Run in headless mode
                    ffOptions.AddArgument("--disable-gpu"); // Disable GPU acceleration
                    ffOptions.AddArgument("--no-sandbox"); // Required for some environments
                    ffOptions.AddArgument("--disable-dev-shm-usage"); // Overcome limited resource problems
                    driver = new FirefoxDriver();
                    //                    driverFileName = Constants.FIREFOX_MAC_DRIVER_NAME;
                    break;
                case "chrome":
                case "google":
                    var chOptions = new ChromeOptions();
                    chOptions.AddArgument("--headless"); // Run in headless mode
                    chOptions.AddArgument("--disable-gpu"); // Disable GPU acceleration
                    chOptions.AddArgument("--no-sandbox"); // Required for some environments
                    chOptions.AddArgument("--disable-dev-shm-usage"); // Overcome limited resource problems

                    driver = new ChromeDriver(chOptions);
                    //                    OpenQA.Selenium.Chromium.ChromiumDriverService;
                    //                    driverFileName = Constants.CHROME_MAC_DRIVER_NAME;
                    break;
                /*                case "ie":
                                case "iexplore":
                 //                   driver = new IEDriver();
                  //                  driverFileName = Constants.IE_MAC_DRIVER_NAME;
                                    break;
                */
                case "edge":
                case "msedge":
                    var edOptions = new EdgeOptions();
                    edOptions.AddArgument("--headless"); // Run in headless mode
                    edOptions.AddArgument("--disable-gpu"); // Disable GPU acceleration
                    edOptions.AddArgument("--no-sandbox"); // Required for some environments
                    edOptions.AddArgument("--disable-dev-shm-usage"); // Overcome limited resource problems




                    driver = new EdgeDriver();
                    //                    driverFileName = Constants.MSEDGE_MAC_DRIVER_NAME;
                    break;
                default:
                    string LogMsg = "The Browser Provided does not match an acceptable value.";
                    logger.Write(LogMsg, funcName);
                    throw new Exception(LogMsg);

            }

        }


        /// <summary>
        /// <para>Creates the IWebDriver session based on the browser selected</para>
        /// <para>browserName = The name of the browser
        /// <br>---Gecko = FF or FireFox</br>
        /// <br>---Chrome = Google or Chrome</br>
        /// <br>---IE = IE or IExplore</br>
        /// <br>---MSEdge = Edge or MSEdge</br></para>
        /// </summary>
        /// <param name="browserName"></param>
        /// <returns></returns>
        private IWebDriver CreateSession()
        {
            string funcName = MethodBase.GetCurrentMethod().Name;
            try
            {
                switch (browserName.ToLower())
                {
                    case "ff":
                        return new FirefoxDriver(driverFilePath);

                    case "firefox":
                        return new FirefoxDriver(driverFilePath);
                    case "chrome":
                        return new ChromeDriver(driverFilePath);
                    case "google":
                        return new ChromeDriver(driverFilePath);
                    case "ie":
                        return new InternetExplorerDriver(driverFilePath);
                    case "iexplore":
                        return new InternetExplorerDriver(driverFilePath);
                    case "edge":
                        return new EdgeDriver(driverFilePath);
                    case "msedge":
                        return new EdgeDriver(driverFilePath);
                    default:
                        string LogMsg = "Unable to Locate WebDriver";
                        logger.Write(LogMsg, funcName);
                        throw new Exception(LogMsg);
                }
            }
            catch (Exception e)
            {
                logMessage = $"Error while Attempting to Create Session\n{e}";
                logger.Write(logMessage, funcName);
                throw new Exception(logMessage);
            }
        }


        /// <summary>
        /// Instantiates the class and Opens the browser session 
        /// <para><br>inBrowserName = the name of the browser to open</br>
        /// <br>-Options:</br>
        /// <br>---FireFox, FF</br>
        /// <br>---Google, Chrome</br>
        /// <br>---IE, IExplore</br>
        /// <br>---Edge, MSEdge</br>
        /// </para>
        /// </summary>
        /// <param name="inBrowserName"></param>

        public SessionManager(string inBrowserName)
        {
            string funcName = MethodBase.GetCurrentMethod().Name;

            logger.Write("Opening Browser", funcName);
            this.browserName = inBrowserName;

            try
            {
                SetBrowserDriver();


                // TODO: Remove Set Driver Path
                //                SetDriverFilePath();

                driver = CreateSession();

                driver.Manage().Window.Maximize();
            }
            catch (Exception e)
            {
                logMessage = $"Error while attempting to create the Browser Session\n{e}";
                logger.Write(logMessage, funcName);
                throw new Exception(logMessage);
            }
        }


        #endregion




    }
}


