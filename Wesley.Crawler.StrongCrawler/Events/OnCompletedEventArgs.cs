using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wesley.Crawler.StrongCrawler.Events
{
    /// <summary>
    /// 爬虫完成事件
    /// </summary>
    public class OnCompletedEventArgs
    {
        public Uri Uri { get; private set; }// 爬虫URL地址
        public int ThreadId { get; private set; }// 任务线程ID
        public string PageSource { get; private set; }// 页面源代码
        public IWebDriver WebDriver { get;private set; }
        public long Milliseconds { get; private set; }// 爬虫请求执行事件
        public OnCompletedEventArgs(Uri uri, int threadId, long milliseconds, string pageSource, IWebDriver driver)
        {
            this.Uri = uri;
            this.ThreadId = threadId;
            this.Milliseconds = milliseconds;
            this.PageSource = pageSource;
            this.WebDriver = driver;
        }
    }
}
