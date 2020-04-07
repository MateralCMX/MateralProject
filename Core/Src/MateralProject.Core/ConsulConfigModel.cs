namespace MateralProject.Core
{
    public class ConsulConfigModel
    {
        /// <summary>
        /// 启用标识
        /// </summary>
        public bool Enable { get; set; }
        /// <summary>
        /// ConsulUrl
        /// </summary>
        public string ConsulUrl { get; set; }
        /// <summary>
        /// Consul端口
        /// </summary>
        public int ConsulPort { get; set; }
        /// <summary>
        /// Consul地址
        /// </summary>
        public string ConsulAddress => $"{ConsulUrl}:{ConsulPort}";
    }
}
