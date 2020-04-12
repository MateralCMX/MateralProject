<template>
    <div>
        <el-button>默认按钮</el-button>
        <el-button type="primary">主要按钮</el-button>
        <el-button type="success">成功按钮</el-button>
        <el-button type="info">信息按钮</el-button>
        <el-button type="warning">警告按钮</el-button>
        <el-button type="danger">危险按钮</el-button>
    </div>
</template>
<script lang="ts">
import Vue from 'vue';
import Component from 'vue-class-component'
@Component
export default class UserList extends Vue {
    private beforeMount() {
        this.$emit('changeTitle', '用户列表');
    }
};
</script>