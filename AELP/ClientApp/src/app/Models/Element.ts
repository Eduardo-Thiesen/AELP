import { Material } from './Material';
import { Section } from './Section';

export class Element {
    constructor(
        public number: number,
        public i: number,
        public j: number,
        public material: Material,
        public section: Section,
    ) {}
}