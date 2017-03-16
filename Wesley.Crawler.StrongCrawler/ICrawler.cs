using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wesley.Crawler.StrongCrawler.Events;

namespace Wesley.Crawler.StrongCrawler
{
    public interface ICrawler
    {
        event EventHandler<OnStartEventArgs> OnStart;//爬虫启动事件

        event EventHandler<OnCompletedEventArgs> OnCompleted;//爬虫完成事件

        event EventHandler<OnErrorEventArgs> OnError;//爬虫出错事件

        Task Start(Uri uri, Script script, Operation operation); //启动爬虫进程

    }
}






