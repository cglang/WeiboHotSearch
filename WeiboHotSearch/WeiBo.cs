using HtmlAgilityPack;

namespace WeiboHotSearch
{
    public class WeiBo
    {
        private readonly HttpClient _httpClient;

        public WeiBo(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Record> GetHotSearchAsync()
        {
            var now = DateTime.Now;
            var hots = new List<Topic>();
            try
            {
                var head = new HttpKeyValue()
                {
                    new("Cookie", "SUB=_2AkMVmy_Sf8NxqwJRmPAdyG7qa45xzw3EieKjx94JJRMxHRl-yT92qlQutRB6PhsBPKzG51VAmwn5ShPVsq3W5uBkC582;")
                };
                var result = await _httpClient.GetStringAsync("https://s.weibo.com/top/summary", null, head);

                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(result);

                // 以下==从html中解析想要的内容

                // 选取展示内容表格
                var tbody = htmlDocument.DocumentNode.SelectSingleNode(@"//*[@id=""pl_top_realtimehot""]/table/tbody");
                // 选择表格内行的第二个单元格
                var tds = tbody.ChildNodes.Where(x => x.Name == "tr").SelectMany(x => x.SelectNodes("td[2]"));

                foreach (var td in tds)
                {
                    var keyword = td.ChildNodes.FirstOrDefault(x => x.Name == "a")?.InnerText.Trim() ?? string.Empty;
                    var hottext = td.ChildNodes.FirstOrDefault(x => x.Name == "span")?.InnerText.Trim() ?? string.Empty;
                    var source = $"{keyword}.*.{hottext}";

                    var heat = 0;
                    var tag = string.Empty;

                    var tags = hottext.Split(" ");
                    if (tags.Length > 1)
                    {
                        _ = int.TryParse(tags[1], out heat);
                        tag = tags[0];
                    }
                    else
                    {
                        _ = int.TryParse(hottext, out heat);
                    }
                    if (heat > 0)
                    {
                        hots.Add(new(keyword, heat, tag, source));
                    }
                }

                var record = new Record(result, hots, now);
                return record;
            }
            catch
            {
                return new("请求获取内容错误", hots, now);
            }
        }
    }
}