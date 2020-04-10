using Materal.ConvertHelper;
using Materal.StringHelper;

namespace MateralProject.Core.Models
{
    /// <summary>
    /// 服务配置对象
    /// </summary>
    public class ServiceConfigModel
    {
        /// <summary>
        /// 是否为HTTPS
        /// </summary>
        public bool IsSSL { get; set; }
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// 服务Url
        /// </summary>
        public string ServiceAddress { get; set; }
        /// <summary>
        /// 服务端口
        /// </summary>
        public int ServicePort { get; set; }
        /// <summary>
        /// 健康检查
        /// </summary>
        public string ServiceHealth { get; set; }
        /// <summary>
        /// Service地址
        /// </summary>
        public string ServiceUrl => IsSSL ? $"https://{ServiceAddress}:{ServicePort}" : $"http://{ServiceAddress}:{ServicePort}";
        /// <summary>
        /// 健康检查地址
        /// </summary>
        public string ServiceHealthUrl => $"{ServiceUrl}/{ServiceHealth}";
        /// <summary>
        /// 更改服务
        /// </summary>
        /// <param name="url"></param>
        public void ChangeService(string url)
        {
            IsSSL = url.Contains("https://");
            url = IsSSL ? url.Substring("https://".Length) : url.Substring("http://".Length);
            string[] temp = url.Split(':');
            if (temp[0].IsIPv4())
            {
                ServiceAddress = temp[0];
            }
            if (temp.Length == 2 && temp[1].IsNumberPositive())
            {
                ServicePort = temp[1].ConvertTo<int>();
            }
            else
            {
                ServicePort = 80;
            }
        }
    }
}
