import { Component } from '@angular/core';
import { Material } from '../Models/Material';
import { Node } from '../Models/Node';
import { Section } from '../Models/Section';
import { Element } from '../Models/Element';
import { NodalLoad } from '../Models/NodalLoad';
import { ElementLoad } from '../Models/ElementLoad';
import { Structure } from './../Models/Structure';
import { ResultsService } from '../Services/results.service';
import { Result } from '../Models/Result';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  
  nodes: Node[] = [];
  materials: Material[] = [];
  sections: Section[] = [];
  elements: Element[] = [];
  nodalLoads: NodalLoad[] = [];
  elementLoads: ElementLoad[] = [];

  structure: Structure;
  result: Result;
  error: boolean = false;

  displayedColumns: string[] = [ 'no', 'dx', 'dy', 'rz' ];
  displayedColumns2: string[] = [ 'elem', 'ni', 'qi', 'mi', 'nj', 'qj', 'mj' ];

  constructor(private resultsService: ResultsService) {}

  updateNodes(newNodes: Node[]) {
    this.nodes = newNodes;
  }

  updateMaterials(newMats: Material[]) {
    this.materials = newMats;
  }

  updateSections(newSections: Section[]) {
    this.sections = newSections;
  }

  updateElements(newElems: Element[]) {
    this.elements = newElems;
  }
  
  updateNodalLoads(newNLoads: NodalLoad[]) {
    this.nodalLoads = newNLoads;
  }

  updateElementLoads(newELoads: ElementLoad[]) {
    this.elementLoads = newELoads;
  }

  getResults() {
    this.error = false;
    this.structure = new Structure(this.nodes, this.elements, this.nodalLoads, this.elementLoads);
    console.log(this.structure);

    this.resultsService.getResults(this.structure).subscribe(response => {
      this.result = new Result(response.displacements, response.reactions);
      console.log(this.result);
    }, error => {
      this.result = null;
      console.log(error);
      this.error = true;
    });
  }

  isCalculated() {
    return !!this.result;
  }
}