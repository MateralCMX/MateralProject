using Authority.DataTransmitModel.WebMenu;
using Authority.Domain;
using Authority.Domain.Repositories;
using Authority.Domain.Repositories.Views;
using Authority.Domain.Views;
using Authority.Service;
using Authority.Service.Models.WebMenu;
using Authority.SqliteEFRepository;
using AutoMapper;
using Materal.ConvertHelper;
using MateralProject.Core.Tree;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Authority.ServiceImpl
{
    /// <summary>
    /// 网页菜单权限服务
    /// </summary>
    public sealed class WebMenuServiceImpl : IWebMenuService
    {
        private readonly IWebMenuRepository _webMenuRepository;
        private readonly IUserOwnedWebMenuViewRepository _userOwnedWebMenuViewRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorityUnitOfWork _authorityUnitOfWork;
        public WebMenuServiceImpl(IWebMenuRepository webMenuRepository, IMapper mapper, IAuthorityUnitOfWork authorityUnitOfWork, IUserOwnedWebMenuViewRepository userOwnedWebMenuViewRepository)
        {
            _webMenuRepository = webMenuRepository;
            _mapper = mapper;
            _authorityUnitOfWork = authorityUnitOfWork;
            _userOwnedWebMenuViewRepository = userOwnedWebMenuViewRepository;
        }
        public async Task AddWebMenuAsync(AddWebMenuModel model)
        {
            var webMenu = model.CopyProperties<WebMenu>();
            webMenu.Index = _webMenuRepository.GetMaxIndex() + 1;
            _authorityUnitOfWork.RegisterAdd(webMenu);
            await _authorityUnitOfWork.CommitAsync();
        }
        public async Task EditWebMenuAsync(EditWebMenuModel model)
        {
            WebMenu webMenuFromDB = await _webMenuRepository.FirstOrDefaultAsync(model.ID);
            if (webMenuFromDB == null) throw new InvalidOperationException("网页菜单权限不存在");
            model.CopyProperties(webMenuFromDB);
            webMenuFromDB.UpdateTime = DateTime.Now;
            _authorityUnitOfWork.RegisterEdit(webMenuFromDB);
            await _authorityUnitOfWork.CommitAsync();
        }
        public async Task DeleteWebMenuAsync([Required(ErrorMessage = "唯一标识不能为空")]Guid id)
        {
            List<WebMenu> allWebMenus = await _webMenuRepository.FindAsync(m => true);
            WebMenu webMenuFromDB = allWebMenus.FirstOrDefault(m => m.ID == id);
            if (webMenuFromDB == null) throw new InvalidOperationException("网页菜单权限不存在");
            ICollection<WebMenu> allChild = GetAllChild(allWebMenus, id);
            foreach (WebMenu menuAuthority in allChild)
            {
                _authorityUnitOfWork.RegisterDelete(menuAuthority);
            }
            _authorityUnitOfWork.RegisterDelete(webMenuFromDB);
            await _authorityUnitOfWork.CommitAsync();
        }
        public async Task<WebMenuDTO> GetWebMenuInfoAsync([Required(ErrorMessage = "唯一标识不能为空")]Guid id)
        {
            WebMenu webMenuFromDB = await _webMenuRepository.FirstOrDefaultAsync(id);
            if (webMenuFromDB == null) throw new InvalidOperationException("网页菜单权限不存在");
            return _mapper.Map<WebMenuDTO>(webMenuFromDB);
        }
        public async Task<List<WebMenuTreeDTO>> GetWebMenuTreeAsync([Required(ErrorMessage = "子系统唯一标识不能为空")]Guid subSystemID)
        {
            List<WebMenu> allWebMenus = await _webMenuRepository.FindAsync(m => m.SubSystemID == subSystemID,m => m.Index);
            return TreeHelper.GetTreeList<WebMenuTreeDTO, WebMenu, Guid>(allWebMenus, null,
                webMenu => _mapper.Map<WebMenuTreeDTO>(webMenu));
        }
        public async Task<List<WebMenuTreeDTO>> GetWebMenuTreeAsync([Required(ErrorMessage = "用户唯一标识不能为空")]Guid userID, [Required(ErrorMessage = "子系统唯一标识不能为空")]Guid subSystemID)
        {
            List<UserOwnedWebMenuView> userOwnedWebMenus = await _userOwnedWebMenuViewRepository.FindAsync(m => m.UserID == userID && m.SubSystemID == subSystemID, m => m.Index);
            return TreeHelper.GetTreeListByAttribute<WebMenuTreeDTO, UserOwnedWebMenuView, Guid>(userOwnedWebMenus, null,
                webMenu => _mapper.Map<WebMenuTreeDTO>(webMenu));
        }
        public async Task ExchangeWebMenuIndexAsync([Required(ErrorMessage = "唯一标识不能为空")]Guid exchangeID, Guid? parentID, Guid? targetID, bool forUnder = true)
        {
            if (parentID.HasValue && !await _webMenuRepository.ExistedAsync(parentID.Value))
            {
                throw new InvalidOperationException("父级唯一标识不存在");
            }
            WebMenu webMenuFromDB = await _webMenuRepository.FirstOrDefaultAsync(exchangeID);
            if (webMenuFromDB == null) throw new InvalidOperationException("该网页菜单权限不存在");
            webMenuFromDB.ParentID = parentID;
            if (targetID.HasValue)
            {
                WebMenu indexWebMenu = await _webMenuRepository.FirstOrDefaultAsync(targetID.Value);
                List<WebMenu> webMenus = await GetWebMenusByIndex(webMenuFromDB, indexWebMenu);
                ExchangeIndex(exchangeID, forUnder, webMenus);
            }
            _authorityUnitOfWork.RegisterEdit(webMenuFromDB);
            await _authorityUnitOfWork.CommitAsync();
        }
        #region 私有方法
        /// <summary>
        /// 获得所有子级
        /// </summary>
        /// <param name="webMenus"></param>
        /// <param name="parentID"></param>
        /// <returns></returns>
        private ICollection<WebMenu> GetAllChild(List<WebMenu> webMenus, Guid? parentID)
        {
            var result = new List<WebMenu>();
            List<WebMenu> child = webMenus.Where(m => m.ParentID == parentID).ToList();
            result.AddRange(child);
            foreach (WebMenu webMenu in child)
            {
                result.AddRange(GetAllChild(webMenus, webMenu.ID));
            }
            return result;
        }
        /// <summary>
        /// 根据位序获取同级的位序内信息
        /// </summary>
        /// <param name="webMenu1"></param>
        /// <param name="webMenu2"></param>
        /// <returns></returns>
        private async Task<List<WebMenu>> GetWebMenusByIndex(WebMenu webMenu1, WebMenu webMenu2)
        {
            if (webMenu1.ParentID != webMenu2.ParentID) throw new InvalidOperationException("两个网页菜单权限不属于同级");
            var webMenus = new List<WebMenu>
            {
                webMenu1,
                webMenu2
            };
            webMenus = webMenus.OrderBy(m => m.Index).ToList();
            WebMenu firstWebMenu = webMenus[0];
            WebMenu lastWebMenu = webMenus[1];
            webMenus.AddRange(await _webMenuRepository.FindAsync(m => m.ParentID == firstWebMenu.ParentID && m.Index > firstWebMenu.Index && m.Index < lastWebMenu.Index));
            webMenus = webMenus.OrderBy(m => m.Index).ToList();
            return webMenus;
        }
        /// <summary>
        /// 调换位序
        /// </summary>
        /// <param name="exchangeID"></param>
        /// <param name="forUnder"></param>
        /// <param name="webMenus"></param>
        private void ExchangeIndex(Guid exchangeID, bool forUnder, IReadOnlyList<WebMenu> webMenus)
        {
            var count = 0;
            int startIndex;
            int indexTemp;
            if (exchangeID == webMenus[0].ID)
            {
                startIndex = forUnder ? webMenus.Count - 1 : webMenus.Count - 2;
                indexTemp = webMenus[startIndex].Index;
                for (int i = startIndex; i > count; i--)
                {
                    webMenus[i].Index = webMenus[i - 1].Index;
                    webMenus[i].UpdateTime = DateTime.Now;
                    _authorityUnitOfWork.RegisterEdit(webMenus[i]);
                }
            }
            else
            {
                count = webMenus.Count - 1;
                startIndex = forUnder ? 1 : 0;
                indexTemp = webMenus[startIndex].Index;
                for (int i = startIndex; i < count; i++)
                {
                    webMenus[i].Index = webMenus[i + 1].Index;
                    webMenus[i].UpdateTime = DateTime.Now;
                    _authorityUnitOfWork.RegisterEdit(webMenus[i]);
                }
            }
            webMenus[count].Index = indexTemp;
            webMenus[count].UpdateTime = DateTime.Now;
            _authorityUnitOfWork.RegisterEdit(webMenus[count]);
        }
        #endregion
    }
}
