using MateralProject.Domain;
using System;
using MateralProject.Core.Tree;

namespace Authority.Domain
{
    /// <summary>
    /// 角色
    /// </summary>
    public sealed class Role : BaseEntity<Guid>, ITreeDomain<Guid>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
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
