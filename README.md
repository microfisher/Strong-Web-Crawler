# 基于浏览器内核的高级爬虫
基于C#.NET+PhantomJS+Sellenium的高级网络爬虫程序。可执行Javascript代码、触发各类事件、操纵页面Dom结构、甚至可以移除不喜欢的CSS样式。

很多网站都用Ajax动态加载、翻页，比如携程网的评论数据。如果是用之前那个简单的爬虫，是很难直接抓取到所有评论数据的，我们需要去分析那漫天的Javascript代码寻找API数据接口，还要时刻提防对方增加数据陷阱或修改API接口地。

如果通过高级爬虫，就可以完全无视这些问题，无论他们如何加密Javascript代码来隐藏API接口，最终的数据都必要呈现在网站页面上的Dom结构中，不然普通用户也就没法看到了。所以我们可以完全不分析API数据接口，直接从Dom中提取数据，甚至都不需要写那复杂的正则表达式。


今日头条@全栈解密：[查看完整教程](http://www.toutiao.com/i6304492725462893058/ "今日头条@全栈解密")

### 主要特性

- 支持Ajax请求事件的触发及捕获；
- 支持异步并发抓取；
- 支持自动事件通知；
- 支持代理切换;
- 支持操作Cookies；


### 运行截图	

- 抓取酒店数据

![抓取酒店数据](https://github.com/coldicelion/Strong-Web-Crawler/blob/master/Wesley.Crawler.StrongCrawler/Resources/%E9%85%92%E5%BA%97%E8%AF%84%E8%AE%BA1.PNG)


- 抓取评论数据

![抓取酒店评论](
https://github.com/coldicelion/Strong-Web-Crawler/blob/master/Wesley.Crawler.StrongCrawler/Resources/%E9%85%92%E5%BA%97%E8%AF%84%E8%AE%BA2.PNG)


### 示例代码

        /// <summary>
        /// 抓取酒店评论
        /// </summary>
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

	

### 技术探讨/联系方式

- QQ号: 276679490

- 爬虫架构讨论群：180085853


