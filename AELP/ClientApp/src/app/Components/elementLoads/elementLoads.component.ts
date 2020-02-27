import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Element } from 'src/app/Models/Element';
import { ElementLoad, LoadType } from 'src/app/Models/ElementLoad';
import { XY } from 'src/app/Models/XY';

@Component({
  selector: 'app-elementLoads',
  templateUrl: './elementLoads.component.html',
  styleUrls: ['./elementLoads.component.css']
})
export class ElementLoadsComponent {

  @Output() listChangedEvent = new EventEmitter<ElementLoad[]>();

  //Listas vindas de outros componentes
  @Input() elements: Element[] = [];

  // Element attributes
  loadTypes: LoadType[] = [1,2,3,4,5];
  elementLoads: ElementLoad[] = [];
  i: number;
  j: number;
  direction: XY = new XY(0, 0);
  qi: number;
  qj: number;
  element: number;
  loadType: LoadType;

  displayedColumns: string[] = [ 'type', 'element', 'direction', 'start', 'qi', 'end', 'qj', 'trash' ];

  constructor() { }

  addElementLoad() {
    // Adiciona um novo elemento ao array
    this.elementLoads.push(new ElementLoad(this.i, this.j, this.direction, this.qi, this.qj, this.element, this.loadType));
    
    // Reseta os valores do formulário
    this.i = 0;
    this.j = 0;
    this.direction.x = 0;
    this.direction.y = 0;
    this.qi = 0;
    this.qj = 0;

    this.updateList(this.elementLoads);
  }

  removeElementLoad(load: ElementLoad) {
    const index = this.elementLoads.indexOf(load);
    if(index != -1){
      this.elementLoads.splice(index, 1);
    }

    this.updateList(this.elementLoads);
  }

  updateList(elementLoads: ElementLoad[]) {
    this.elementLoads = [...elementLoads];
    this.listChangedEvent.emit(elementLoads);
  }
}
