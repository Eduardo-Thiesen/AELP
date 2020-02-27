export class Result {
    constructor(
        public displacements: NodeDisplacements[],
        public reactions: ElementStress[]
    ) {}
}

export class NodeDisplacements {
    node: number;
    dx: number;
    dy: number;
    rz: number;
}

export class ElementStress {
    element: number;
    ni: number;
    qi: number;
    mi: number;
    nj: number;
    qj: number;
    mj: number;
}