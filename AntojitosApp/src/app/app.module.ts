import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from "@angular/forms";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';

import { AppComponent } from './app.component';
import { DetallesClientesComponent } from './detalles-clientes/detalles-clientes.component';
import { DetallesClientesFormComponent } from './detalles-clientes/detalles-clientes-form/detalles-clientes-form.component';
import { HttpClientModule } from '@angular/common/http';
import { DetallesProductosComponent } from './detalles-productos/detalles-productos.component';
import { DetallesProductosFormComponent } from './detalles-productos/detalles-productos-form/detalles-productos-form.component';
import { RouterModule, Routes } from '@angular/router';
import { DetallesVentasComponent } from './detalles-ventas/detalles-ventas.component';

// Aqui se agregan las rutas para poder navegar entre los componentes
const appRoutes: Routes = [
  { path: '', component: DetallesVentasComponent }, //el componente que se defina aqui es con el que inicializará la aplicacion
  { path: 'cliente-comp', component: DetallesClientesComponent },
  { path: 'producto-comp', component: DetallesProductosComponent },
  { path: 'ventas-comp', component: DetallesVentasComponent },
];

@NgModule({
  declarations: [
    AppComponent,
    DetallesClientesComponent,
    DetallesClientesFormComponent,
    DetallesProductosComponent,
    DetallesProductosFormComponent,
    DetallesVentasComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    RouterModule.forRoot(
      appRoutes,
      { enableTracing: true } // <-- debugging purposes only
    )
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
