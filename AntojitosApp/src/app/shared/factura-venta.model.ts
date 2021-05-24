import { DetalleCliente } from "./detalle-cliente.model";

export class FacturaVenta {
    IdFactura:number=0;
    IdCliente:number=0;
    FechaVenta:Date=new Date();
    ValorTotal:number=0;
    IdClienteNavigation:DetalleCliente=new DetalleCliente();
}
