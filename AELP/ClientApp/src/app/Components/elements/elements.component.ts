import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Element } from 'src/app/Models/Element';
import { Material } from 'src/app/Models/Material';
import { Section } from 'src/app/Models/Section';
import { Node } from 'src/app/Models/Node';

@Component({
  selector: 'app-elements',
  templateUrl: './elements.component.html',
  styleUrls: ['./elements.component.css']
})
export class ElementsComponent {

  @Output() listChangedEvent = new EventEmitter<Element[]>();

  //Listas vindas de outros componentes
  @Input() materials: Material[] = [];
  @Input() sections: Section[] = [];
  @Input() nodes: Node[] = [];

  // Element attributes
  elements: Element[] = [];
  i: number;
  j: number;
  material: Material;
  section: Section;

  displayedColumns: string[] = [ 'number', 'i', 'j', 'material', 'section', 'trash' ];

  constructor() { }

  addElement() {
    const number = this.elements.length + 1;

    // Adiciona um novo elemento ao array
    this.elements.push(new Element(number, this.i, this.j, this.material, this.section));
    
    // Reseta os valores do formulário
    this.i = 0;
    this.j = 0;

    this.updateList(this.elements);
  }

  removeElement(element: Element) {
    const index = this.elements.indexOf(element);
    if(index != -1){
      this.elements.splice(index, 1);
    }

    this.updateList(this.elements);
  }

  updateList(elements: Element[]) {
    this.elements = [...elements];
    this.listChangedEvent.emit(elements);
  }
}
