using Materal.Model;
using System.ComponentModel.DataAnnotations;

namespace Authority.Service.Models.SubSystem
{
    public sealed class QuerySubSystemFilterModel : PageRequestModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Contains, StringLength(50, ErrorMessage = "名称长度不能超过50")]
        public string Name { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        [Equal, StringLength(50, ErrorMessage = "代码长度不能超过50")]
        public string Code { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        [Equal]
        public bool? Display { get; set; }
    }
}
