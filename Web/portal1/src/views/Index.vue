<template>
  <el-container>
    <el-header class="header">
      <router-link class="logo" to="/">MateralProject</router-link>
      <el-menu
        :default-active="activeIndex"
        mode="horizontal"
        background-color="#3f85ed"
        text-color="rgba(255, 255, 255, 0.65)"
        active-text-color="#fff"
        :unique-opened="true"
        class="menu"
      >
        <el-menu-item index="conDep" @click="push('conDep', { router:'Index' })">发布中心</el-menu-item>
        <el-menu-item index="configCenter" @click="push('configCenter', { router:'Index' })">配置中心</el-menu-item>
        <el-menu-item index="gateway" @click="push('gateway', { router:'Index' })">网关中心</el-menu-item>
        <el-menu-item index="authority" @click="push('authority', { router:'User/List' })">权限中心</el-menu-item>
      </el-menu>
      <router-link class="logout" to="/Login">退出登录</router-link>
    </el-header>
    <el-container>
      <el-aside width="200px">
        <el-menu>
          <el-submenu index="1">
            <template slot="title">
              <i class="el-icon-location"></i>
              <span>导航一</span>
            </template>
            <el-menu-item-group>
              <template slot="title">分组一</template>
              <el-menu-item index="1-1">选项1</el-menu-item>
              <el-menu-item index="1-2">选项2</el-menu-item>
            </el-menu-item-group>
            <el-menu-item-group title="分组2">
              <el-menu-item index="1-3">选项3</el-menu-item>
            </el-menu-item-group>
            <el-submenu index="1-4">
              <template slot="title">选项4</template>
              <el-menu-item index="1-4-1">选项1</el-menu-item>
            </el-submenu>
          </el-submenu>
          <el-menu-item index="2">
            <i class="el-icon-menu"></i>
            <span slot="title">导航二</span>
          </el-menu-item>
          <el-menu-item index="3" disabled>
            <i class="el-icon-document"></i>
            <span slot="title">导航三</span>
          </el-menu-item>
          <el-menu-item index="4">
            <i class="el-icon-setting"></i>
            <span slot="title">导航四</span>
          </el-menu-item>
        </el-menu>
      </el-aside>
      <div class="main-panel">
        <el-main>
          <div id="subapp-container"></div>
        </el-main>
        <el-footer>Materal ©2020 Created by Materal</el-footer>
      </div>
    </el-container>
  </el-container>
</template>
<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

@Component
export default class Index extends Vue {
  public constructor() {
    super();
    (window as any).app = this;
  }
  private appContent = '';
  private loading = false;
  private activeIndex = '';
  private mounted() {
    let currentData = location.hash;
    if (currentData.startsWith('#/')) {
      currentData = currentData.substring(2);
    }
    const index = currentData.indexOf('/');
    if (index > 0) {
      currentData = currentData.substring(0, index);
    }
    if (currentData) {
      this.activeIndex = currentData;
    }
  }
  private push(url: string, data: any) {
    if (location.hash.startsWith('#/' + url)) {
      if ((window as any).navigate) {
        (window as any).navigate(data);
      }
    } else {
      (window as any).historyData = data;
      window.location.hash = url;
    }
  }
}
</script>
<style scoped>
.header {
  padding: 0;
  display: flex;
  flex-wrap: nowrap;
  background-color: #3f85ed;
  line-height: 60px;
  font-size: 14px;
}
.logo {
  padding: 0 20px;
  color: #fff;
  font-size: 20px;
  font-weight: bold;
  text-decoration: none;
}
.menu {
  flex-grow: 1;
  height: 60px;
}
.logout {
  padding: 0 20px;
  color: rgba(255, 255, 255, 0.65);
  margin-left: auto;
  text-decoration: none;
}
.logout:hover {
  background-color: rgb(50, 106, 190);
}
.main-panel {
  display: flex;
  flex-direction: column;
  width: 100%;
  background-color: #e2e2e2;
}
.el-main {
  margin: 24px 24px 0;
  padding: 20px;
  border-radius: 4px;
  background-color: #fff;
  min-height: 790px;
}
.el-footer {
  text-align: center;
  line-height: 60px;
}
</style>