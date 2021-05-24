import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { DetalleProducto } from 'src/app/shared/detalle-producto.model';
import { DetalleProductoService } from 'src/app/shared/detalle-producto.service';

@Component({
  selector: 'app-detalles-productos-form',
  templateUrl: './detalles-productos-form.component.html',
  styles: [
  ]
})
export class DetallesProductosFormComponent implements OnInit {

  constructor(public service:DetalleProductoService, 
    private toastr:ToastrService) { }

  ngOnInit(): void {
  }

  onSubmit(form: NgForm){
    if(this.service.detalleProducto.IdProducto == 0){
      this.insertRecord(form);
    }
    else {
      this.updateRecord(form);
    }
  }

  insertRecord(form:NgForm){
    this.service.postDetalleProducto().subscribe(
      res =>{
        this.resetForm(form);
        this.service.refreshList();
        this.toastr.success('El registro fue satisfactorio','Registro de nuevo producto');
      },
      err =>  { console.log(err); }
    );
  }

  updateRecord(form: NgForm){
    this.service.putDetalleProducto().subscribe(
      res =>{
        this.resetForm(form);
        this.service.refreshList();
        //toastr.info para mostrar cuadro azul de información
        this.toastr.info('Los datos se actualizaron satisfactoriamente','Actualización datos del producto');
      },
      err =>  { console.log(err); }
    );
  }

  resetForm(form:NgForm){
    form.form.reset();
    this.service.detalleProducto = new DetalleProducto();
  }

}
