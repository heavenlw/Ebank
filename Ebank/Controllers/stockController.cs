using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace Ebank.Controllers
{
    public class stockController : ApiController
    {
        [HttpGet]
        public Stock Result(string code)
        {
            HttpWebRequest requestScore = (HttpWebRequest)WebRequest.Create(string.Format("http://data.gtimg.cn/flashdata/hk/latest/daily/hk{0}.js?maxage=43201", code));
          string data =   Awol.WebHelper.GetResponseStr(requestScore,"utf-8",null,null);
            return getresult(data);
        }


        public Stock getresult(string data)
        {
            Stock stock = new Stock();
            try
            {
                 stock = GetFristCol(data);
                return stock;
            }
            catch (Exception t)
            {
                return stock;
            }
        }


        [HttpGet]
        public Stock Detail(string code)
        {
            Stock stock = new Stock();
            try
            {
                //Stock stock = new Stock();
                HttpWebRequest requestScore = (HttpWebRequest)WebRequest.Create(string.Format("http://qt.gtimg.cn/r=0.5745015831198543q=r_hk{0}", code));
                string data = Awol.WebHelper.GetResponseStr(requestScore, "gbk", null, null);
                MatchCollection matches = Regex.Matches(data, "~(.*?)~", RegexOptions.Singleline);
                stock.CName = matches[0].Groups[1].ToString();
                stock.Price = matches[1].Groups[1].ToString();

                return stock;
            }
            catch (Exception t)
            { return stock; }

        }



        public Stock GetFristCol(string data)
        {
            List<Price> prices = new List<Price>();
            List<DataItem> items = new List<DataItem>();
            string firstCol = string.Empty;
            List<string> lines = data.Split('\n').ToList<string>();
            for (int i = 2; i < lines.Count; i++)
            {
                string line = lines[i];
                List<string> numStrs = line.Split(' ').ToList<string>();
                if (numStrs.Count < 5)
                    continue;
                DataItem item = new DataItem();
                Price price = new Price();
                price.content = new string[4];
                for (int numI = 0; numI < numStrs.Count; numI++)
                {
                    if (numI == 0)
                        item.content = numStrs[numI];
                    if (numI > 0 && numI < 5)
                    {
                        price.content[numI-1] = numStrs[numI];
                        if (numI == 3)
                        {
                            price.content[numI-1] = numStrs[numI+1];
                        }
                        if (numI == 4)
                        {
                            price.content[numI - 1] = numStrs[numI-1];
                        }
                    }
                   // if (numI > 0 && numI < 4)

                        //price.content += ",";

                }
                items.Add(item);
                prices.Add(price);
            }
            Stock stock = new Stock();
            stock.price = new Price[prices.Count];
            for (int itemI = 0; itemI < items.Count; itemI++)
            {
                stock.data += items[itemI].content;
                if (itemI != items.Count - 1)
                    stock.data += ",";
            }
            for (int priceI = 0; priceI < prices.Count; priceI++)
            {
                stock.price[priceI] = prices[priceI];   // += "[" + prices[priceI].content + "]";
                //if (priceI != prices.Count - 2)
                //    stock.price += ";";
            }
           // stock.data = "[" + stock.data + "]";
           // stock.price = "[" + stock.price  + "]";
            return stock;
        }
        public class Price
        {
            public string[] content { get; set; }
        }
        public class DataItem
        {
            public string content { get; set; }
        }
        public class Stock
        {
            public string CName { get;set; }
            public string data { get; set; }
            public string Price { get;  set; }
            public Price[] price { get; set; }
        }
    }
}