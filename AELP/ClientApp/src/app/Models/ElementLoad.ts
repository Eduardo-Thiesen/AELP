import { XY } from './XY';

export class ElementLoad {
    constructor(  
        public i: number,
        public j: number,
        public direction: XY,
        public qi: number,
        public qj: number,
        public element: number,
        public loadType: number
        ) {}
}

export enum LoadType {
    Concentrated = 1,
    Trapezoidal = 2,
    Moment = 3,
    UniformTemp = 4,
    TempGradient = 5
}