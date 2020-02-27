import { Node } from './Node';
import { Element } from './Element';
import { NodalLoad } from './NodalLoad';
import { ElementLoad } from './ElementLoad';

export class Structure {
    constructor(
        public nodes: Node[],
        public elements: Element[],
        public nodalLoads: NodalLoad[],
        public elementLoads: ElementLoad[]
    ) {}
}