using System;
namespace Authority.DataTransmitModel.WebMenu
{
    /// <summary>
    /// 网页菜单权限数据传输模型
    /// </summary>
    public class WebMenuDTO
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 样式
        /// </summary>
        public string Style { get; set; }
        /// <summary>
        /// 模块代码
        /// </summary>
        public string ModeCode { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 所属子系统唯一标识
        /// </summary>
        public Guid SubSystemID { get; set; }
    }
}
