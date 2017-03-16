using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wesley.Crawler.StrongCrawler.Events
{
    public class OnErrorEventArgs
    {
        public Uri Uri { get; set; }

        public Exception Exception { get; set; }

        public OnErrorEventArgs(Uri uri,Exception exception) {
            this.Uri = uri;
            this.Exception = exception;
        }
    }
}
