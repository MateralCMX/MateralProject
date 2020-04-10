using System.ComponentModel.DataAnnotations;

namespace Authority.Service.Models.SubSystem
{
    public class AddSubSystemModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "名称不可以为空"), MaxLength(50, ErrorMessage = "名称长度不能超过50")]
        public string Name { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        [Required(ErrorMessage = "代码不可以为空"), MaxLength(50, ErrorMessage = "代码长度不能超过50")]
        public string Code { get; set; }
        /// <summary>
        /// 样式
        /// </summary>
        public string Style { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool Display { get; set; } = false;
        /// <summary>
        /// 携带数据
        /// </summary>
        public string Data { get; set; }
    }
}
