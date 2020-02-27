import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Structure } from '../Models/Structure';
import { Result } from '../Models/Result';
import { environment } from 'src/environments/environment.prod';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ResultsService {

constructor(private http: HttpClient) { }

  getResults(structure: Structure): Observable<Result> {
    let result: Result;
    
    // const structureList: Structure[] = [ structure ];
    const url: string = environment.apiEndpoint + '/structure';
    const json: string = JSON.stringify(structure);
    console.log(json);

    let params = new HttpParams().append("structure", json);
    console.log(params);

    return this.http.get<Result>(url, { params } );
  }
}
