import { Injectable } from '@angular/core';
import { DetalleCliente } from './detalle-cliente.model';
import { HttpClient } from "@angular/common/http";
//import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DetalleClienteService {

  constructor(private http: HttpClient) { }

  readonly baseURL = 'http://localhost:18132/api/TestClientes'
  formData: DetalleCliente = new DetalleCliente();
  list: DetalleCliente[];

  listaCliente:any=[];

  //función usada para agregar nuevo registro de cliente en BD
  postDetalleCliente(){
    return this.http.post(this.baseURL, this.formData)
  }
  
  //funcion para modificar registro de cliente existente en la BD
  putDetalleCliente(){
    return this.http.put(`${this.baseURL}/${this.formData.IdCliente}`, this.formData)    
  }

  deleteDetalleCliente(id:number){
    return this.http.delete(`${this.baseURL}/${id}`)    
  }

  //funcion que llama una lista de todos los clientes en la BD
  refreshList(){
    this.http.get(this.baseURL)
    .toPromise()
    .then(res => this.list = res as DetalleCliente[]);    
  }
}
