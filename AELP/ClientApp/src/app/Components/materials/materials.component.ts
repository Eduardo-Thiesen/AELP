import { Component, Output, EventEmitter } from '@angular/core';
import { Material } from 'src/app/Models/Material';

@Component({
  selector: 'app-materials',
  templateUrl: './materials.component.html',
  styleUrls: ['./materials.component.css']
})
export class MaterialsComponent {

  @Output() listChangedEvent = new EventEmitter<Material[]>();

  // Material attributes
  materials: Material[] = [];
  materialName: string;
  materialE: number;
  materialAlfa: number;
  materialV: number;

  // Colunas da tabela
  displayedColumns: string[] = [ 'name', 'E', 'Alfa', "V", 'trash' ];

  constructor() { }

  addMaterial() {
    // Adiciona um novo material ao array
    this.materials.push(new Material(this.materialName, this.materialE, this.materialV, this.materialAlfa));
    // Reseta os valores do formulário
    this.materialName = "";
    this.materialE = 0;
    this.materialAlfa = 0;
    this.materialV = 0;

    this.updateList(this.materials);
  }

  removeMaterial(material: Material) {
    const index = this.materials.indexOf(material);
    if(index != -1){
      this.materials.splice(index, 1);
    }

    this.updateList(this.materials);
  }

  updateList(materials: Material[]) {
    this.materials = [...materials];
    this.listChangedEvent.emit(materials);
  }
}
