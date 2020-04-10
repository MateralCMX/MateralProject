using System.ComponentModel.DataAnnotations;

namespace MateralProject.Core.Models
{
    /// <summary>
    /// 调换位序请求模型
    /// </summary>
    public sealed class ExchangeIndexRequestModel<T>
    {
        /// <summary>
        /// 交换唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识1不可以为空")]
        public T ExchangeID { get; set; }
        /// <summary>
        /// 目标唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识2不可以为空")]
        public T TargetID { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        public bool ForUnder { get; set; } = true;
    }
}
