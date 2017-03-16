
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Wesley.Crawler.StrongCrawler.Events;
using Wesley.Crawler.StrongCrawler.Models;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.PhantomJS;

namespace Wesley.Crawler.StrongCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            var hotelUrl = "http://hotels.ctrip.com/hotel/434938.html";
            var hotelCrawler = new StrongCrawler();
            hotelCrawler.OnStart += (s, e) =>
            {
                Console.WriteLine("爬虫开始抓取地址：" + e.Uri.ToString());
            };
            hotelCrawler.OnError += (s, e) =>
            {
                Console.WriteLine("爬虫抓取出现错误：" + e.Uri.ToString() + "，异常消息：" + e.Exception.ToString());
            };
            hotelCrawler.OnCompleted += (s, e) =>
            {
                HotelCrawler(e);
            };
            var operation = new Operation
            {
                Action = (x) => {
                    //通过Selenium驱动点击页面的“酒店评论”
                    x.FindElement(By.XPath("//*[@id='commentTab']")).Click();
                },
                Condition = (x) => {
                    //判断Ajax评论内容是否已经加载成功
                    return x.FindElement(By.XPath("//*[@id='commentList']")).Displayed && x.FindElement(By.XPath("//*[@id='hotel_info_comment']/div[@id='commentList']")).Displayed && !x.FindElement(By.XPath("//*[@id='hotel_info_comment']/div[@id='commentList']")).Text.Contains("点评载入中");
                },
                Timeout = 5000
            };

            hotelCrawler.Start(new Uri(hotelUrl), null, operation);//不操作JS先将参数设置为NULL

            Console.ReadKey();
        }


        private static void HotelCrawler(OnCompletedEventArgs e) {
            //Console.WriteLine(e.PageSource);
            //File.WriteAllText(Environment.CurrentDirectory + "\\cc.html", e.PageSource, Encoding.UTF8);

            var hotelName = e.WebDriver.FindElement(By.XPath("//*[@id='J_htl_info']/div[@class='name']/h2[@class='cn_n']")).Text;
            var address = e.WebDriver.FindElement(By.XPath("//*[@id='J_htl_info']/div[@class='adress']")).Text;
            var price = e.WebDriver.FindElement(By.XPath("//*[@id='div_minprice']/p[1]")).Text;
            var score = e.WebDriver.FindElement(By.XPath("//*[@id='base_bd']/div[4]/div[2]/div[1]/div/a/p[1]/span")).Text;
            var reviewCount = e.WebDriver.FindElement(By.XPath("//*[@id='base_bd']/div[4]/div[2]/div[1]/div/a/span")).Text;

            var comments = e.WebDriver.FindElement(By.XPath("//*[@id='hotel_info_comment']/div[@id='commentList']/div[1]/div[1]/div[1]"));
            var currentPage =Convert.ToInt32(comments.FindElement(By.XPath("div[@class='c_page_box']/div[@class='c_page']/div[contains(@class,'c_page_list')]/a[@class='current']")).Text);
            var totalPage = Convert.ToInt32(comments.FindElement(By.XPath("div[@class='c_page_box']/div[@class='c_page']/div[contains(@class,'c_page_list')]/a[last()]")).Text);
            var messages = comments.FindElements(By.XPath("div[@class='comment_detail_list']/div"));
            var nextPage = Convert.ToInt32(comments.FindElement(By.XPath("div[@class='c_page_box']/div[@class='c_page']/div[contains(@class,'c_page_list')]/a[@class='current']/following-sibling::a[1]")).Text);

            Console.WriteLine();
            Console.WriteLine("名称：" + hotelName);
            Console.WriteLine("地址：" + address);
            Console.WriteLine("价格：" + price);
            Console.WriteLine("评分：" + score);
            Console.WriteLine("数量：" + reviewCount);
            Console.WriteLine("页码：" + "当前页（" + currentPage + "）" + "下一页（" + nextPage + "）" + "总页数（" + totalPage + "）" + "每页（" + messages.Count + "）");
            Console.WriteLine();
            Console.WriteLine("===============================================");
            Console.WriteLine();
            Console.WriteLine("点评内容：");

            foreach (var message in messages)
            {
                Console.WriteLine("帐号：" + message.FindElement(By.XPath("div[contains(@class,'user_info')]/p[@class='name']")).Text);
                Console.WriteLine("房型：" + message.FindElement(By.XPath("div[@class='comment_main']/p/a")).Text);
                Console.WriteLine("内容：" + message.FindElement(By.XPath("div[@class='comment_main']/div[@class='comment_txt']/div[1]")).Text.Substring(0,50) + "....");
                Console.WriteLine();
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("===============================================");
            Console.WriteLine("地址：" + e.Uri.ToString());
            Console.WriteLine("耗时：" + e.Milliseconds + "毫秒");


        }
    }
}
