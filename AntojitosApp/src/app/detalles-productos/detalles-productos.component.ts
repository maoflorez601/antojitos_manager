import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { DetalleProducto } from '../shared/detalle-producto.model';
import { DetalleProductoService } from '../shared/detalle-producto.service';

@Component({
  selector: 'app-detalles-productos',
  templateUrl: './detalles-productos.component.html',
  styles: [
  ]
})
export class DetallesProductosComponent implements OnInit {

  constructor(public service: DetalleProductoService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.service.refreshList();
  }

  populateForm(selectedRecord:DetalleProducto){
    //this.service.formData = selectedRecord;
    this.service.detalleProducto = Object.assign({},selectedRecord);
  }

  onDelete(id:number){
    if(confirm('¿Esta seguro que desea eliminar el registro del producto?')){
      this.service.deleteDetalleProducto(id).subscribe(
        res=>{
          this.service.refreshList();
          this.toastr.success("La eliminación del producto ha sido satisfactoria",'Administración Producto')
        },
        err=>{console.log(err)}
      );
    }
    
  }

}
