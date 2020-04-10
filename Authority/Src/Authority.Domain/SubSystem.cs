using MateralProject.Domain;
using System;

namespace Authority.Domain
{
    /// <summary>
    /// 子系统
    /// </summary>
    public sealed class SubSystem : BaseEntity<Guid>
    {
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
        /// 位序
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool Display { get; set; }
        /// <summary>
        /// 携带数据
        /// </summary>
        public string Data { get; set; }
    }
}
