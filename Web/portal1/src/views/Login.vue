<template>
  <el-card class="login-panel">
    <div slot="header">
      <span>MateralProject</span>
    </div>
    <el-form :model="formData" :rules="formDataRules" ref="formData">
      <el-form-item prop="account">
        <el-input v-model="formData.account" :disabled="isLogin" placeholder="账号"></el-input>
      </el-form-item>
      <el-form-item prop="password">
        <el-input v-model="formData.password" :disabled="isLogin" show-password placeholder="密码"></el-input>
      </el-form-item>
      <el-form-item>
        <el-button class="login-form-button" :loading="isLogin" type="primary" @click="onBtnLoginClick">登录</el-button>
      </el-form-item>
    </el-form>
  </el-card>
</template>
<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import { AuthorityService } from '../services/authorityService';
import { ResultTypeEnum } from '../services/models/result/resultTypeEnum';
import { AuthorityHelper } from '../common/authorityHelper';
@Component
export default class Login extends Vue {
  private formData = {
    account: '',
    password: '',
  };
  private formDataRules = {
    account: [
      { required: true, message: '请输入账号', trigger: 'blur' },
    ],
    password: [
      { required: true, message: '请输入密码', trigger: 'blur' },
    ],
  };
  private isLogin = true;
  private beforeMount() {
    AuthorityHelper.removeToken();
    const key = 'ISLOGINRELOAD';
    const value = sessionStorage.getItem(key);
    if (value) {
      sessionStorage.removeItem(key);
      location.reload();
    } else {
      sessionStorage.setItem(key, 'true');
      this.isLogin = false;
    }
  }
  private onBtnLoginClick() {
    (this.$refs.formData as any).validate(async (valid: boolean) => {
      if (valid) {
        await this.loginAsync();
      } else {
        return false;
      }
    });
  }
  private async loginAsync() {
    this.isLogin = true;
    const authorityService = new AuthorityService(this.$router, this.$message);
    const result = await authorityService.login({
      Account: this.formData.account,
      Password: this.formData.password,
    });
    if (result.ResultType === ResultTypeEnum.Success) {
      this.$router.push({ name: 'Index' });
    }
    this.isLogin = false;
  }
}
</script>
<style scoped>
.login-panel {
  margin: 85px auto 0;
  max-width: 300px;
}
.login-form-button {
  width: 100%;
}
</style>