import { Component, Output, Input, EventEmitter } from '@angular/core';
import { Node } from 'src/app/Models/Node';
import { NodalLoad } from 'src/app/Models/NodalLoad';

@Component({
  selector: 'app-nodalLoads',
  templateUrl: './nodalLoads.component.html',
  styleUrls: ['./nodalLoads.component.css']
})
export class NodalLoadsComponent {

  @Output() listChangedEvent = new EventEmitter<NodalLoad[]>();

  //Listas vindas de outros componentes
  @Input() nodes: Node[] = [];

  // NodalLoad attributes
  nodalLoads: NodalLoad[] = [];
  fx: number;
  fy: number;
  Mz: number;
  node: number;

  // Colunas da tabela
  displayedColumns: string[] = [ 'fx', 'fy', 'mz', 'no', 'trash' ];

  constructor() { }

  addNodalLoad() {
    
    // Adiciona um novo elemento ao array
    this.nodalLoads.push(new NodalLoad(this.fx, this.fy, this.Mz, this.node));
    
    // Reseta os valores do formulário
    this.fx = 0;
    this.fy = 0;
    this.Mz = 0;
    this.node = 0;

    this.updateList(this.nodalLoads);
  }

  removeNodalLoad(nodalLoad: NodalLoad) {
    const index = this.nodalLoads.indexOf(nodalLoad);
    if(index != -1){
      this.nodalLoads.splice(index, 1);
    }

    this.updateList(this.nodalLoads);
  }

  updateList(nodalLoads: NodalLoad[]) {
    this.nodalLoads = [...nodalLoads];
    this.listChangedEvent.emit(nodalLoads);
  }
}
