import { Component, Output, EventEmitter } from '@angular/core';
import { Section } from 'src/app/Models/Section';

@Component({
  selector: 'app-sections',
  templateUrl: './sections.component.html',
  styleUrls: ['./sections.component.css']
})
export class SectionsComponent {
  
  @Output() listChangedEvent = new EventEmitter<Section[]>();
  
  // Section attributes
  sections: Section[] = [];
  inertia: number;
  area: number;

  // Colunas da tabela
  displayedColumns: string[] = [ 'number', 'inertia', 'area', 'trash' ];

  constructor() {}

  addSection() {
    const number = this.sections.length + 1;
    // Adiciona uma nova seção ao array
    this.sections.push(new Section(number, this.inertia, this.area));

    // Reseta os valores do formulário
    this.inertia = 0;
    this.area = 0;

    this.updateList(this.sections);
  }

  removeSection(section: Section) {
    const index = this.sections.indexOf(section);
    if(index != -1){
      this.sections.splice(index, 1);
    }

    this.updateList(this.sections);
  }

  updateList(sections: Section[]) {
    this.sections = [...sections];
    this.listChangedEvent.emit(sections);
  }
}
