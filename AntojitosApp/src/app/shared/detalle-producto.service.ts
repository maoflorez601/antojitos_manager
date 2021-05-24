import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DetalleProducto } from './detalle-producto.model';

@Injectable({
  providedIn: 'root'
})
export class DetalleProductoService {

  constructor(private http: HttpClient) { }

  readonly baseURL = 'http://localhost:18132/api/TestProductoes'
  detalleProducto: DetalleProducto = new DetalleProducto();
  list: DetalleProducto[];

  //función usada para agregar nuevo registro de producto en BD
  postDetalleProducto(){
    return this.http.post(this.baseURL, this.detalleProducto)
  }
  
  //funcion para modificar registro de producto existente en la BD
  putDetalleProducto(){
    return this.http.put(`${this.baseURL}/${this.detalleProducto.IdProducto}`, this.detalleProducto)    
  }

  deleteDetalleProducto(id:number){
    return this.http.delete(`${this.baseURL}/${id}`)    
  }

  //funcion que llama una lista de todos los productos en la BD
  refreshList(){
    this.http.get(this.baseURL)
    .toPromise()
    .then(res => this.list = res as DetalleProducto[]);    
  }
}
