using System;

namespace Authority.DataTransmitModel.SubSystem
{
    /// <summary>
    /// 子系统列表数据传输模型
    /// </summary>
    public class SubSystemListDTO
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 唯一代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 样式
        /// </summary>
        public string Style { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool Display { get; set; }
    }
}
