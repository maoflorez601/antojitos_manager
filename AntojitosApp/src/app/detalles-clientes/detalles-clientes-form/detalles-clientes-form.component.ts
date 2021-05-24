import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { DetalleCliente } from 'src/app/shared/detalle-cliente.model';
import { DetalleClienteService } from 'src/app/shared/detalle-cliente.service';

@Component({
  selector: 'app-detalles-clientes-form',
  templateUrl: './detalles-clientes-form.component.html',
  styles: [
  ]
})
export class DetallesClientesFormComponent implements OnInit {

  constructor(public service:DetalleClienteService, 
    private toastr:ToastrService) { }

  ngOnInit(): void {
  }

  onSubmit(form: NgForm){
    if(this.service.formData.IdCliente == 0){
      this.insertRecord(form);
    }
    else {
      this.updateRecord(form);
    }
  }

  insertRecord(form:NgForm){
    this.service.postDetalleCliente().subscribe(
      res =>{
        this.resetForm(form);
        this.service.refreshList();
        this.toastr.success('El registro fue satisfactorio','Registro de nuevo cliente');
      },
      err =>  { console.log(err); }
    );
  }

  updateRecord(form: NgForm){
    this.service.putDetalleCliente().subscribe(
      res =>{
        this.resetForm(form);
        this.service.refreshList();
        //toastr.info para mostrar cuadro azul de información
        this.toastr.info('Los datos se actualizaron satisfactoriamente','Actualización datos del cliente');
      },
      err =>  { console.log(err); }
    );
  }

  resetForm(form:NgForm){
    form.form.reset();
    this.service.formData = new DetalleCliente();
  }

}
