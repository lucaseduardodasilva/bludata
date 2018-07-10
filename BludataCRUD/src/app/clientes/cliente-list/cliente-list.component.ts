import { Component, OnInit } from '@angular/core';
import { ClienteService } from '../shared/cliente.service';

import { Cliente } from '../shared/cliente.model'

@Component({
  selector: 'app-cliente-list',
  templateUrl: './cliente-list.component.html',
  styleUrls: ['./cliente-list.component.css']
})
export class ClienteListComponent implements OnInit {
  clienteSelecionado: Cliente = new Cliente();
  clientes: Cliente[] = [];

  constructor(public clientService : ClienteService) { }

  ngOnInit() {
    this.clientService.getClienteList()
      .then(x => {
        this.clientes = x.slice();
        console.log(this.clientes);
      }
      );
  }

  showForEdit(cli : Cliente){
      this.clientService.clienteSelecionado = Object.assign({}, cli);
  }

  showCliente(index){ 
    this.clienteSelecionado =this.clientes[index];
  }

  onDelete(id : number){
      if(confirm("Você tem certeza que deseja deletar este cliente?") == true){
        this.clientService.deleteCliente(id)
        .subscribe(x => {
          this.clientService.getClienteList()
        })
      }    
  }
}