export class Material {
    constructor(        
        public name: string,
        public elasticityModule: number,
        public poisson: number,
        public thermalExpCoef: number,
        ) {}
}