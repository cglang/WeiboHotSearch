namespace WeiboHotSearch
{
    public struct Record
    {
        public Record(string html, IEnumerable<Topic> topics, DateTime time)
        {
            Time = time;
            Html = html;
            Topics = topics;
        }

        public DateTime Time { get; }

        public string Html { get; set; }

        public IEnumerable<Topic> Topics { get; set; }
    }

    public struct Topic
    {
        public Topic(string keyword, int heat, string tag, string source)
        {
            Keyword = keyword;
            Heat = heat;
            Tag = tag;
            Source = source;
        }

        /// <summary>
        /// 关键词
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// 热度
        /// </summary>
        public int Heat { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// 源内容
        /// </summary>
        public string Source { get; set; }
    }
}