# 基于浏览器内核的高级爬虫
基于C#.NET+PhantomJS+Sellenium的高级网络爬虫程序。可执行Javascript代码、触发各类事件、操纵页面Dom结构、甚至可以移除不喜欢的CSS样式。


今日头条@全栈解密：[查看完整教程](http://www.toutiao.com/i6304492725462893058/ "今日头条@全栈解密")

### 主要特性

- 支持Ajax请求事件的触发及捕获；
- 支持异步并发抓取；
- 支持自动事件通知；
- 支持代理切换;
- 支持操作Cookies；


### 运行截图	

- 抓取城市列表

![使用正则表达式清洗数据](https://github.com/coldicelion/Simple-Web-Crawler/blob/master/Wesley.Crawler.SimpleCrawler/Images/3.%E4%BD%BF%E7%94%A8%E6%AD%A3%E5%88%99%E6%B8%85%E6%B4%97%E6%95%B0%E6%8D%AE.png?raw=true)


- 抓取酒店列表

![抓取城市下的酒店列表](https://github.com/coldicelion/Simple-Web-Crawler/blob/master/Wesley.Crawler.SimpleCrawler/Images/4.%E6%8A%93%E5%8F%96%E5%9F%8E%E5%B8%82%E4%B8%8B%E7%9A%84%E9%85%92%E5%BA%97%E5%88%97%E8%A1%A8.png?raw=true)


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


