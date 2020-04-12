import './Index.css';
import 'zone.js';
import { SubAppList } from '../../PortalApp/SubAppList'
import { PortalApp } from '../../PortalApp/PortalApp'
import { BasiceViewModel } from '../../common/BasiceViewModel';

class IndexViewModel extends BasiceViewModel {
    private portalApp: PortalApp;
    private menuData = [
        {
            Name: '用户管理',
            Data: '{"url":"authority","data":{"name":"UserList"}}'
        },
        {
            Name: '子系统管理',
            Data: '{"url":"authority","data":{"name":"SubSystemList"}}'
        },
        {
            Name: '角色管理',
            Data: '{"url":"authority","data":{"name":"RoleList"}}'
        },
        {
            Name: '菜单管理',
            Data: '{"url":"authority","data":{"name":"WebMenuList"}}'
        }
    ]
    public constructor() {
        super();
        if (SubAppList.length > 0) {
            this.portalApp = new PortalApp();
            this.portalApp.Init(SubAppList);
        }
        this.initMenu(this.menuData);
    }
    private initMenu(data: any[]) {
        const subMenu = document.getElementById('sub-menu');
        for (let i = 0; i < data.length; i++) {
            const item = data[i];
            const itemElement = document.createElement('li');
            itemElement.classList.add('sub-menu-item');
            itemElement.innerText = item.Name;
            itemElement.addEventListener('click', () => {
                const itemData = JSON.parse(item.Data);
                this.push(itemData.url, itemData.data);
                itemElement.classList.add('active');
            });
            subMenu.appendChild(itemElement);
        }
    }
    public push(url: string, data: any) {
        if (location.hash.startsWith('#/' + url)) {
            if ((window as any).navigate) {
                (window as any).navigate(data);
            }
        } else {
            (window as any).historyData = data;
            window.location.hash = '/' + url;
        }
    }
}
window.addEventListener('load', () => {
    new IndexViewModel();
})