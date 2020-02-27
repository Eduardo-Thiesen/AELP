export class NodalLoad {
    constructor(   
        public fx: number,
        public fy: number,
        public mz: number,
        public node: number
    ) {}

}