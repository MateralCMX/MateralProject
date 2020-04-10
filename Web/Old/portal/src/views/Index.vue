<template>
  <div></div>
  <!-- <a-layout>
    <a-layout-header class="header">
      <router-link class="logo" to="/">MateralProject</router-link>
      <a-menu class="menu" v-model="current" theme="dark" mode="horizontal">
        <a-menu-item key="conDep" @click="push('conDep', { router:'Index' })">发布中心</a-menu-item>
        <a-menu-item key="configCenter" @click="push('config-center', { router:'Index' })">配置中心</a-menu-item>
        <a-menu-item key="gateway" @click="push('gateway', { router:'Index' })">网关中心</a-menu-item>
        <a-menu-item key="authority" @click="push('authority', { router:'SubSystem/List' })">权限中心</a-menu-item>
      </a-menu>
      <router-link class="logout" to="/Login">退出登录</router-link>
    </a-layout-header>
    <a-layout>
      <a-layout-sider width="200" class="submenu">
        <a-menu mode="inline">
          <a-sub-menu key="sub1">
            <span slot="title">
              <a-icon type="user" />subnav 1
            </span>
            <a-menu-item key="1">option1</a-menu-item>
            <a-menu-item key="2">option2</a-menu-item>
            <a-menu-item key="3">option3</a-menu-item>
            <a-menu-item key="4">option4</a-menu-item>
          </a-sub-menu>
          <a-sub-menu key="sub2">
            <span slot="title">
              <a-icon type="laptop" />subnav 2
            </span>
            <a-menu-item key="5">option5</a-menu-item>
            <a-menu-item key="6">option6</a-menu-item>
            <a-menu-item key="7">option7</a-menu-item>
            <a-menu-item key="8">option8</a-menu-item>
          </a-sub-menu>
          <a-sub-menu key="sub3">
            <span slot="title">
              <a-icon type="notification" />subnav 3
            </span>
            <a-menu-item key="9">option9</a-menu-item>
            <a-menu-item key="10">option10</a-menu-item>
            <a-menu-item key="11">option11</a-menu-item>
            <a-menu-item key="12">option12</a-menu-item>
          </a-sub-menu>
        </a-menu>
      </a-layout-sider>
      <a-layout-content class="content">
        <div id="subapp-container"></div>
      </a-layout-content>
    </a-layout>
    <a-layout-footer class="footer">Ant Design ©2018 Created by Ant UED</a-layout-footer>
  </a-layout> -->
</template>
<script>
export default {
  name: "Index",
  data() {
    return {
      current: []
    };
  },
  mounted() {
    let currentData = location.hash;
    if (currentData.startsWith("#/")) {
      currentData = currentData.substring(2);
    }
    const index = currentData.indexOf("/");
    if (index > 0) {
      currentData = currentData.substring(0, index);
    }
    if (currentData) {
      this.current = [currentData];
    }
  },
  methods: {
    push(url, data) {
      if (location.hash.startsWith( '#/' + url)) {
        if (window.navigate) {
          window.navigate(data);
        }
      } else {
        window.historyData = data;
        window.location.hash = url;
      }
    }
  }
};
</script>
<style scoped>
.header {
  width: 100%;
  z-index: 999;
  display: flex;
  flex-wrap: nowrap;
}
.logo {
  padding: 0 20px;
  color: #fff;
  font-size: 20px;
  font-weight: bold;
}
.logout {
  padding: 0 20px;
  color: rgba(255, 255, 255, 0.65);
  margin-left: auto;
}
.logout:hover {
  color: #fff;
}
.menu {
  line-height: 64px;
}
.submenu {
  background: #fff;
}
.content {
  position: relative;
  background: #fff;
  padding: 24px;
  margin: 24px 24px 0;
}
.footer {
  text-align: center;
}
</style>
