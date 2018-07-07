import { Injectable } from '@angular/core';
import { Cliente } from './cliente.model';


import { Http, Response, Headers, RequestOptions, RequestMethod } from '@angular/http';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ClienteService {

  clienteSelecionado : Cliente;
  clienteList : Cliente[];
  constructor(public http : Http) { }

  postCliente(cli : Cliente){
    var body = JSON.stringify(cli); 
    var headerOptions = new Headers({'Content-Type':'application/json'});
    var requestOptions = new RequestOptions({method : RequestMethod.Post, headers : headerOptions});  
    return this.http.post('http://localhost:51826/api/PostCliente', body, requestOptions)
  }

  putCliente(id, cli){
    var body = JSON.stringify(cli); 
    var headerOptions = new Headers({'Content-Type':'application/json'});
    var requestOptions = new RequestOptions({method : RequestMethod.Put, headers : headerOptions});  
    return this.http.put
    ('http://localhost:51826/api/PutCliente/' + id, body, requestOptions)
    .pipe(map(res => res.json()))
  }

  getClienteList(): Promise<Cliente[]>{
    return this.http.get('http://localhost:51826/api/GetCliente')
    .toPromise().then(x => (JSON.parse(x.json())));
  }

  deleteCliente(id: number){
    return this.http.delete('http://localhost:51826/api/DeleteCliente/' + id).pipe(map(res => res.json()));
  }
}

