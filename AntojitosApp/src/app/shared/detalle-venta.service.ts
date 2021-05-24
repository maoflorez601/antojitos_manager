import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DetalleCliente } from './detalle-cliente.model';
import { DetalleProducto } from './detalle-producto.model';
import { DetalleVenta } from './detalle-venta.model';
import { FacturaVenta } from './factura-venta.model';

@Injectable({
  providedIn: 'root'
})
export class DetalleVentaService {

  constructor(private http: HttpClient) { }

  readonly productosURL = 'http://localhost:18132/api/TestProductoes';
  readonly documentoURL = 'http://localhost:18132/api/TestClientes/Documento';
  readonly facturaURL = 'http://localhost:18132/api/TestFacturas';
  readonly facturaDetallesURL = 'http://localhost:18132/api/TestFacturaDetalles';
  detalleProducto: DetalleProducto = new DetalleProducto();
  detalleVenta: DetalleVenta = new DetalleVenta();
  detalleCliente: DetalleCliente = new DetalleCliente();
  facturaVenta: FacturaVenta = new FacturaVenta();
  list: DetalleProducto[];
  documentoCliente=null;
  ventas: Array<DetalleVenta> = [];
  valorTotal: number = 0;
  fechaActual: number = Date.now();
  cantidadProducto:number = 1;

  //función usada para agregar nuevo registro de producto en BD
  postDetalleProducto(){
    return this.http.post(this.productosURL, this.detalleProducto)
  }
  
  //funcion para modificar registro de producto existente en la BD
  putDetalleProducto(){
    return this.http.put(`${this.productosURL}/${this.detalleProducto.IdProducto}`, this.detalleProducto)    
  }

  deleteDetalleProducto(id:number){
    return this.http.delete(`${this.productosURL}/${id}`)    
  }

  //funcion que llama una lista de todos los productos en la BD
  refreshList(){
    this.http.get(this.productosURL)
    .toPromise()
    .then(res => this.list = res as DetalleProducto[]);    
  }

  getCliente(){
    this.detalleCliente = new DetalleCliente();
    this.http.get(`${this.documentoURL}/${this.documentoCliente}`)
    .toPromise()
    .then(res => this.detalleCliente = res as DetalleCliente);    
    //this.detalleCliente.Nombres = 'adas'
  }

  addProducto(producto: DetalleProducto){
    this.detalleVenta = new DetalleVenta();
    this.detalleVenta.IdProducto = producto.IdProducto;
    this.detalleVenta.NombreProducto = producto.Nombre;
    this.detalleVenta.ValorUnidad = producto.ValorUnidad;
    this.detalleVenta.Cantidad = this.cantidadProducto;
    //this.detalleVenta.Fecha = this.fechaActual;

    this.ventas.push(this.detalleVenta);
    this.valorTotal += producto.ValorUnidad * this.cantidadProducto;
  }

  //función usada para agregar nuevo registro de factura en BD
  generarVenta(){
    this.getCliente(); //se llama al cliente para que traiga el id

    this.facturaVenta = new FacturaVenta();
    this.facturaVenta.IdCliente = this.detalleCliente.IdCliente;
    this.facturaVenta.FechaVenta = new Date();
    this.facturaVenta.ValorTotal = this.valorTotal;
    this.facturaVenta.IdClienteNavigation=this.detalleCliente;
    //console.log(this.facturaVenta.FechaVenta.getDate)
    console.log('detalle cliente=' + this.detalleCliente.IdCliente.toString)

    //despues de llenar los campos se registra la factura
    return this.http.post(this.facturaURL, this.facturaVenta)
  }
}
