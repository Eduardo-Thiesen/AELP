import { Component, Output, EventEmitter, ChangeDetectorRef } from '@angular/core';
import { Node } from '../../Models/Node'

@Component({
  selector: 'app-nodes',
  templateUrl: './nodes.component.html',
  styleUrls: ['./nodes.component.css']
})
export class NodesComponent {

  @Output() listChangedEvent = new EventEmitter<Node[]>(); 

  // Node attributes
  nodes: Node[] = [];
  nodeValX: number;
  nodeValY: number;
  deslocX: boolean = false; // Controla a checkbox
  deslocY: boolean = false; // Controla a checkbox
  rotacaoZ: boolean = false; // Controla a checkbox

  // Colunas ta tabela
  displayedColumns: string[] = [ 'number', 'x', 'y', 'restX', 'restY', 'restZ', 'trash' ];

  constructor() { }

  addNode() {
    const number = this.nodes.length + 1;
    // Pega os dados do ngModel
    const xInt = this.deslocX ? 1 : 0;
    const yInt = this.deslocY ? 1 : 0;
    const zInt = this.rotacaoZ ? 1 : 0;
    const restrictions = [xInt, yInt, zInt];
    // Adiciona um novo node ao array
    const node = new Node(number, this.nodeValX, this.nodeValY, restrictions)
    this.nodes.push(node);

    // Reseta os valores do formulário
    this.nodeValX = 0;
    this.nodeValY = 0;
    this.deslocX = false;
    this.deslocY = false;
    this.rotacaoZ = false;

    this.updateList(this.nodes);
  }

  removeNode(node: Node) {
    console.log("Entrou no remove");
    console.log("node do parametro: ", node);

    const index = this.nodes.indexOf(node);
    if(index != -1){
      this.nodes.splice(index, 1);
    }

    this.updateList(this.nodes);
  }

  updateList(nodes: Node[]) {
    this.nodes = [...nodes];
    this.listChangedEvent.emit(nodes);
  }
}