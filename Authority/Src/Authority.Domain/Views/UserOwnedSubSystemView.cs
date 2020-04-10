using Materal.TTA.Common;
using System;

namespace Authority.Domain.Views
{
    /// <summary>
    /// 用户拥有的子系统
    /// </summary>
    [ViewEntity]
    public sealed class UserOwnedSubSystemView : IEntity<int>
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        public Guid UserID { get; set; }
        /// <summary>
        /// 子系统唯一标识
        /// </summary>
        public Guid SubSystemID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 代码
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
