import { enableProdMode, NgZone } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { Router } from '@angular/router';
import { AppModule } from './app/app.module';
import { environment } from './environments/environment';
import singleSpaAngular from 'single-spa-angular';
import { singleSpaPropsSubject } from './single-spa/single-spa-props';

if (environment.production) {
  enableProdMode();
}
let LifeCycles: any = {};
if (!(window as any).__POWERED_BY_QIANKUN__) {
  require('zone');
  platformBrowserDynamic()
    .bootstrapModule(AppModule)
    .catch(err => console.error(err));
} else {
  LifeCycles = singleSpaAngular({
    bootstrapFunction: singleSpaProps => {
      singleSpaPropsSubject.next(singleSpaProps);
      return platformBrowserDynamic().bootstrapModule(AppModule)
        .catch(err => console.error(err));
    },
    template: '<app-root />',
    Router,
    NgZone
  });
}
export const bootstrap = LifeCycles.bootstrap;
export const mount = LifeCycles.mount;
export const unmount = LifeCycles.unmount;
