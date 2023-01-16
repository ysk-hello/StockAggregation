using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using System;
using System.Net.Http;

namespace ComObjSample
{
    public class KabutanAccess
    {
        private Company _company;

        public KabutanAccess(Company company)
        {
            _company = company;
        }

        private IHtmlDocument GetIHtmlDocument(string url)
        {
            IHtmlDocument doc;

            try
            {
                using (var client = new HttpClient())
                using (var stream = client.GetStreamAsync(url).Result)
                {
                    var parser = new HtmlParser();
                    doc = parser.ParseDocumentAsync(stream).Result;

                    return doc;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private StockDetailData Parse(IHtmlDocument doc)
        {
            try
            {
                var data = new StockDetailData();

                var code = doc.QuerySelector("#stockinfo_i1 > div.si_i1_1 > h2 > span");
                data.Code = code.TextContent.Trim();

                var name = doc.QuerySelector("#stockinfo_i1 > div.si_i1_1 > h2");
                data.Name = name.TextContent.Replace(data.Code, string.Empty).Trim();

                var stockType = doc.QuerySelector("#stockinfo_b1 > div > div:nth-child(1)");
                data.StockType = stockType.TextContent.Trim();

                var date = doc.QuerySelector("#kobetsu_left > h2:nth-child(2) > time");
                data.Date = DateTime.Parse(date.Attributes[0].Value.Trim());

                var openTime = doc.QuerySelector("#kobetsu_left > table:nth-child(3) > tbody > tr:nth-child(1) > td:nth-child(4) > time");
                data.OpenDateTime = DateTime.Parse(openTime.Attributes[0].Value.Trim());

                var open = doc.QuerySelector("#kobetsu_left > table:nth-child(3) > tbody > tr:nth-child(1) > td:nth-child(2)");
                data.OpenPrice = double.Parse(open.TextContent.Trim());

                var highTime = doc.QuerySelector("#kobetsu_left > table:nth-child(3) > tbody > tr:nth-child(2) > td:nth-child(4) > time");
                data.HighDateTime = DateTime.Parse(highTime.Attributes[0].Value.Trim());

                var high = doc.QuerySelector("#kobetsu_left > table:nth-child(3) > tbody > tr:nth-child(2) > td:nth-child(2)");
                data.HighPrice = double.Parse(high.TextContent.Trim());

                var lowTime = doc.QuerySelector("#kobetsu_left > table:nth-child(3) > tbody > tr:nth-child(3) > td:nth-child(4) > time");
                data.LowDateTime = DateTime.Parse(lowTime.Attributes[0].Value.Trim());

                var low = doc.QuerySelector("#kobetsu_left > table:nth-child(3) > tbody > tr:nth-child(3) > td:nth-child(2)");
                data.LowPrice = double.Parse(low.TextContent.Trim());

                var closeTime = doc.QuerySelector("#kobetsu_left > table:nth-child(3) > tbody > tr:nth-child(4) > td:nth-child(4) > time");
                data.CloseDateTime = DateTime.Parse(closeTime.Attributes[0].Value.Trim());

                var close = doc.QuerySelector("#kobetsu_left > table:nth-child(3) > tbody > tr:nth-child(4) > td:nth-child(2)");
                data.ClosePrice = double.Parse(close.TextContent.Trim());

                var volume = doc.QuerySelector("#kobetsu_left > table:nth-child(4) > tbody > tr:nth-child(1) > td");
                data.Volume = double.Parse(volume.TextContent.Replace("株", string.Empty).Trim());

                var contracts = doc.QuerySelector("#kobetsu_left > table:nth-child(4) > tbody > tr:nth-child(4) > td");
                data.NumberOfContracts = double.Parse(contracts.TextContent.Replace("回", string.Empty).Trim());

                var per = doc.QuerySelector("#stockinfo_i3 > table > tbody > tr:nth-child(1) > td:nth-child(1)");
                if (double.TryParse(per.TextContent.Replace("倍", string.Empty).Trim(), out double tempPer))
                {
                    data.Per = tempPer;
                }
                else
                {
                    data.Per = double.NaN;
                }

                var pbr = doc.QuerySelector("#stockinfo_i3 > table > tbody > tr:nth-child(1) > td:nth-child(2)");
                if (double.TryParse(pbr.TextContent.Replace("倍", string.Empty).Trim(), out double tempPbr))
                {
                    data.Pbr = tempPbr;
                }
                else
                {
                    data.Pbr = double.NaN;
                }

                var divident = doc.QuerySelector("#stockinfo_i3 > table > tbody > tr:nth-child(1) > td:nth-child(3)");
                if (double.TryParse(divident.TextContent.Replace("％", string.Empty).Trim(), out double tempDivident))
                {
                    data.DividendYield = tempDivident;
                }
                else
                {
                    data.DividendYield = double.NaN;
                }

                var credit = doc.QuerySelector("#stockinfo_i3 > table > tbody > tr:nth-child(1) > td:nth-child(4)");
                if (double.TryParse(credit.TextContent.Replace("倍", string.Empty).Trim(), out double tempCredit))
                {
                    data.CreditRatio = tempCredit;
                }
                else
                {
                    data.CreditRatio = double.NaN;
                }

                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public StockDetailData GetStockDetailData()
        {
            var doc = GetIHtmlDocument($"https://kabutan.jp/stock/?code={_company.Code}");

            var data = Parse(doc);

            return data;
        }
    }
}
