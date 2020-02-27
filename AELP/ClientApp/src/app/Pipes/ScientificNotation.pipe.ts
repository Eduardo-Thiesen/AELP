import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'ScientificNotation'
})
export class ScientificNotationPipe implements PipeTransform {

  constructor() {}

  transform(value: any, args?: any): any {
    if (!isNaN(value)) {
      const n = Number(value);
      return n.toExponential(3);
    }
  }

}
