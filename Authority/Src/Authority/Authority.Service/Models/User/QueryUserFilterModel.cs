using Materal.Model;

namespace Authority.Service.Models.User
{
    public class QueryUserFilterModel : PageRequestModel
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Equal]
        public string Account { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [Contains]
        public string Name { get; set; }
    }
}
