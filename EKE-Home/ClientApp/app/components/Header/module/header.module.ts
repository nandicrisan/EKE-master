import { NgModule } from '@angular/core';

import { NavMenuComponent } from '../navmenu/navmenu.component';
import { HeaderComponent } from '../top/header.component';

@NgModule({
    declarations: [
        HeaderComponent,
        NavMenuComponent
    ],
    imports: [],
    providers: [],
    exports: [NavMenuComponent, HeaderComponent]
})
export class HeaderModule { }  