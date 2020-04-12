<template>
    <router-view @changeTitle="changeTitle2"></router-view>
</template>
<script lang="ts">
import Vue from 'vue';
import Component from 'vue-class-component'
@Component
export default class UserIndex extends Vue {
    public changeTitlte2(title: string) {
        console.log("UserIndex", title);
        // this.$emit('changeTitle', title);
    }
};
</script>