using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wesley.Crawler.StrongCrawler
{
    public class Operation
    {
        public int Timeout { get; set; }

        public Action<IWebDriver> Action { get; set; }

        public Func<IWebDriver, bool> Condition { get; set; }

    }
}
