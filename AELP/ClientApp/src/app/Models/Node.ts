// export class Node {
//     number: number;
//     x: number;
//     y: number;
//     restrictions: {
//       x: boolean;
//       y: boolean;
//       z: boolean
//     };
// }

export class Node {
    constructor(
      public number: number,
      public x: number,
      public y: number,
      public restrictions: number[]
    ) {}
  }