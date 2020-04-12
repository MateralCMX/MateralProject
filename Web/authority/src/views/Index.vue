<template>
    <div>
        <div>{{title}}</div>
        <router-view></router-view>
    </div>
</template>
<script lang="ts">
import Vue from 'vue';
import Component from 'vue-class-component'
@Component
export default class Index extends Vue {
    private title: string = '权限中心';
    private changeTitlte(title: string) {
        this.title = title;
    }
};
</script>