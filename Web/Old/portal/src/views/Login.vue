<template>
  <div>
    
  </div>
  <!-- <a-card class="login-panel" title="MateralProject">
    <a-form-model :model="form" ref="ruleForm" :rules="rules" autocomplete="off" class="login-form">
      <a-form-model-item prop="account">
        <a-input :disabled="isLogin" v-model="form.account" placeholder="账号">
          <a-icon slot="prefix" type="user" />
        </a-input>
      </a-form-model-item>
      <a-form-model-item prop="password">
        <a-input-password :disabled="isLogin" v-model="form.password" placeholder="密码">
          <a-icon slot="prefix" type="lock" />
        </a-input-password>
      </a-form-model-item>
      <a-form-model-item>
        <a-button type="primary" class="login-form-button" :loading="isLogin" @click="onBtnLoginClick">登录</a-button>
      </a-form-model-item>
    </a-form-model>
  </a-card> -->
</template>
<script>
import { AuthorityService } from '../services/authorityService';
import { ResultTypeEnum } from '../services/models/result/resultTypeEnum';
import { AuthorityHelper } from '../common/authorityHelper'
export default {
    name: "Login",
    data() {
        return {
            isLogin: true,
            form: {
                account: '',
                password: ''
            },
            rules: {
                account: [
                    { required: true, message: '请填写账号', trigger: 'blur' }
                ],
                password: [
                    { required: true, message: '请填写密码', trigger: 'blur' }
                ]
            },
        };
    },
    beforeMount() {
      AuthorityHelper.removeToken();
      const key = 'ISLOGINRELOAD';
      const value = sessionStorage.getItem(key);
      if(value) {
        sessionStorage.removeItem(key);
        location.reload();
      } else {
        sessionStorage.setItem(key, true);
        this.isLogin = false;
      }
    },
    methods: {
        onBtnLoginClick() {
            this.$refs.ruleForm.validate(valid => {
                if (valid) {
                    this.login();
                } else {
                    return false;
                }
            });
        },
        async login() {
            this.isLogin = true;
            const authorityService = new AuthorityService(this.$router, this.$message);
            const result = await authorityService.login({
                Account: this.form.account,
                Password: this.form.password
            });
            if (result.ResultType === ResultTypeEnum.Success){
                this.$router.push({ name: 'Index' });
            }
            this.isLogin = false;
        }
    },
};
</script>
<style scoped>
p {
  margin: 0;
}
.login-panel {
  margin: 85px auto 0;
  max-width: 300px;
}
.login-form-forgot {
  float: right;
}
.login-form-button {
  width: 100%;
}
.version {
  font-size: 10px;
  color: #b6b6b6;
}
</style>
