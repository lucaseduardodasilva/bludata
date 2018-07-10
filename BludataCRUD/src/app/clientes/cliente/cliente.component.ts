import { Component, OnInit } from '@angular/core';
import { ClienteService } from '../shared/cliente.service';
import { NgForm, FormBuilder, Validators } from '@angular/forms'
import {NgxMaskModule} from 'ngx-mask'
import { Estado } from '../shared/estado.model'

//import { ToastrService } from 'ngx-toastr'

@Component({
  selector: 'app-cliente',
  templateUrl: './cliente.component.html',
  styleUrls: ['./cliente.component.css']
})
export class ClienteComponent implements OnInit {
  estados: Estado[] = [];

public nomeValido: boolean

  constructor(public clienteService : ClienteService) {

   }

  ngOnInit() {
    this.resetForm();
    this.clienteService.getEstados();
  }
  resetForm(form? : NgForm){
    if(form != null)
      form.reset();
    this.clienteService.clienteSelecionado = {
        ClienteID : null,
        Nome : '',
        Cpf : '',
        Rg : '',
        Telefone : '',
        DataNascimento : null,
        DataCadastro : null,
        Estado : ''
    }
  }

  onSubmit(form : NgForm){
    if(form.value.ClienteID == null){
    this.clienteService.postCliente(form.value)
    .subscribe(data => {
      this.resetForm(form);
      this.clienteService.getClienteList();

      //this.toastr.success('Cliente adicionado com sucesso!', 'Registro de clientes')
    })
  }
    else{
      this.clienteService.putCliente(form.value.ClienteID, form.value)
      .subscribe(data => {
        this.resetForm(form);
        this.clienteService.getClienteList();
      })
    }
  }
}
