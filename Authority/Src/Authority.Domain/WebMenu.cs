using MateralProject.Core.Tree;
using MateralProject.Domain;
using System;

namespace Authority.Domain
{
    /// <summary>
    /// 网页菜单
    /// </summary>
    public sealed class WebMenu : BaseEntity<Guid>, ITreeDomain<Guid>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 样式
        /// </summary>
        public string Style { get; set; }
        /// <summary>
        /// 携带数据
        /// </summary>
        public string Data { get; set; }
        /// <summary>
        /// 位序
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        public Guid? ParentID { get; set; }
        /// <summary>
        /// 所属子系统唯一标识
        /// </summary>
        public Guid SubSystemID { get; set; }
    }
}
