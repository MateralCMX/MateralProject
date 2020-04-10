using System.ComponentModel.DataAnnotations;
using Materal.Model;

namespace Authority.PresentationModel.SubSystem
{
    /// <summary>
    /// 查询子系统请求模型
    /// </summary>
    public class QuerySubSystemFilterRequestModel : PageRequestModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(50, ErrorMessage = "名称长度不能超过50")]
        public string Name { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        [StringLength(50, ErrorMessage = "代码长度不能超过50")]
        public string Code { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool? Display { get; set; }
    }
}
