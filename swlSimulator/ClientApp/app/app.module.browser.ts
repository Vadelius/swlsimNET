import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppModuleShared } from './app.module.shared';
import { AppComponent } from './components/app/app.component';

require('!style-loader!css-loader!../assets/css/core.css');
require('!style-loader!css-loader!../assets/css/components.css');
require('!style-loader!css-loader!../assets/css/responsive.css');
require('!style-loader!css-loader!../assets/css/icons.css');

@NgModule({
	bootstrap: [AppComponent],
	imports: [BrowserModule, AppModuleShared],
	providers: [{ provide: 'BASE_URL', useFactory: getBaseUrl }]
})
export class AppModule {}

export function getBaseUrl(): string {
	return document.getElementsByTagName('base')[0].href;
}
