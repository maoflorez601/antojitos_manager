import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { DetalleCliente } from '../shared/detalle-cliente.model';
import { DetalleClienteService } from '../shared/detalle-cliente.service';

@Component({
  selector: 'app-detalles-clientes',
  templateUrl: './detalles-clientes.component.html',
  styles: [
  ]
})
export class DetallesClientesComponent implements OnInit {

  constructor(public service: DetalleClienteService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.service.refreshList();
  }

  populateForm(selectedRecord:DetalleCliente){
    //this.service.formData = selectedRecord;
    this.service.formData = Object.assign({},selectedRecord);
  }

  onDelete(id:number){
    if(confirm('¿Esta seguro que desea eliminar el registro del cliente?')){
      this.service.deleteDetalleCliente(id).subscribe(
        res=>{
          this.service.refreshList();
          this.toastr.success("La eliminación del cliente ha sido satisfactoria",'Administración Cliente')
        },
        err=>{console.log(err)}
      );
    }
    
  }

}
