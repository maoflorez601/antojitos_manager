import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { DetalleProducto } from '../shared/detalle-producto.model';
import { DetalleProductoService } from '../shared/detalle-producto.service';
import { DetalleVentaService } from '../shared/detalle-venta.service';

@Component({
  selector: 'app-detalles-ventas',
  templateUrl: './detalles-ventas.component.html',
  styles: [
  ]
})
export class DetallesVentasComponent implements OnInit {

  constructor(public serviceProd: DetalleProductoService, public serviceVenta: DetalleVentaService,
    private toastr: ToastrService) { }


  ngOnInit(): void {
    this.serviceProd.refreshList();
  }

  populateForm(selectedRecord:DetalleProducto){
    //this.service.formData = selectedRecord;
    this.serviceProd.detalleProducto = Object.assign({},selectedRecord);
  }

  onDelete(id:number){
    if(confirm('¿Esta seguro que desea eliminar el registro del producto?')){
      this.serviceProd.deleteDetalleProducto(id).subscribe(
        res=>{
          this.serviceProd.refreshList();
          this.toastr.success("La eliminación del producto ha sido satisfactoria",'Administración Producto')
        },
        err=>{console.log(err)}
      );
    }
    
  }

  onAddProducto(producto: DetalleProducto){   
    this.serviceVenta.addProducto(producto);
    this.serviceVenta.cantidadProducto = 1;
  }

  limpiarCampos(){
    this.serviceVenta.ventas = [];
    this.serviceVenta.documentoCliente = null;
    this.serviceVenta.cantidadProducto = 1;
    this.serviceVenta.valorTotal = 0;
  }

  generarFactura(){
    this.serviceVenta.generarVenta().subscribe(
      res =>{
        this.limpiarCampos();
        this.serviceProd.refreshList();
        this.toastr.success('La factura se guardo con exito.','Registro Factura');
      },
      err =>  { console.log(err); }
    );
  }
}
