<template>
  <div id="subapp-container">
    <div v-if="loading" class="subapp-loading">
      <div class="subapp-loading-text">{{loadingText}}</div>
    </div>
    <div v-html="appContent" />
  </div>
</template>
<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
@Component
export default class SubApp extends Vue {
  @Prop() public loading!: boolean;
  @Prop() public appContent!: string;
  public loadingText = '加载中';
  private loadingPointCount = 0;
  public mounted() {
    this.addText();
  }
  private addText() {
    if (this.loading) {
      if (this.loadingPointCount === 5) {
        this.loadingText = '加载中';
        this.loadingPointCount = 0;
      } else {
        this.loadingText += '.';
        this.loadingPointCount++;
      }
      setTimeout(() => {
        this.addText();
      }, 500);
    }
  }
}
</script>
<style scoped>
.subapp-loading {
  position: absolute;
  left: 0;
  right: 0;
  bottom: 0;
  top: 0;
  background-color: rgba(255, 255, 255, 0.65);
}
.subapp-loading-text {
  position: absolute;
  left: 50%;
  top: 50%;
  transform: translate(-50%, -50%);
}
</style>