namespace WeiboHotSearch
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            WeiBo _weiBo = new(new HttpClient());

            var record = await _weiBo.GetHotSearchAsync();

            ShowHot(record.Topics);

            void ShowHot(IEnumerable<Topic> hots)
            {
                string[,] table = new string[hots.Count() + 1, 4];

                table[0, 0] = "";
                table[0, 1] = $"关键词  {DateTime.Now:yyyy-MM-dd HH:mm}";
                table[0, 2] = "热度";
                table[0, 3] = "标签";

                var index = 1;
                foreach (var hot in hots)
                {
                    table[index, 0] = index.ToString();
                    table[index, 1] = hot.Keyword;
                    table[index, 2] = hot.Heat.ToString();
                    table[index, 3] = hot.Tag;
                    index++;
                }

                ConsoleBox.WriteBoxTable(table);
            }
        }
    }
}