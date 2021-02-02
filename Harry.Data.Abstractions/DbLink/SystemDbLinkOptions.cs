namespace Harry.Data.DbLink
{
    public class SystemDbLinkOptions
    {
        public const string DefaultDbLinkName = "_SYSTEM_";

        /// <summary>
        /// 链接名称
        /// </summary>
        public string DbLinkName { get; set; } = DefaultDbLinkName;

        /// <summary>
        /// 链接字符串
        /// </summary>
        public string ConnectionString { get; set; }
    }
}
