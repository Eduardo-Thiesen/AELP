import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSelectModule } from '@angular/material/select';
import { MatListModule } from '@angular/material/list';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';

import { ElementsComponent } from './Components/elements/elements.component';
import { SectionsComponent } from './Components/sections/sections.component';
import { NodesComponent } from './Components/nodes/nodes.component';
import { AppComponent } from './Components/app.component';
import { MaterialsComponent } from './Components/materials/materials.component';
import { NodalLoadsComponent } from './Components/nodalLoads/nodalLoads.component';
import { ElementLoadsComponent } from './Components/elementLoads/elementLoads.component';
import { ResultsService } from './Services/results.service';
import { ScientificNotationPipe } from './Pipes/ScientificNotation.pipe';

@NgModule({
  declarations: [
    AppComponent,
    NodesComponent,
    MaterialsComponent,
    SectionsComponent,
    ElementsComponent,
    NodalLoadsComponent,
    ElementLoadsComponent,
    ScientificNotationPipe
  ],
  imports: [
    MatSelectModule,
    BrowserModule,
    HttpClientModule,
    FormsModule,
    MatFormFieldModule,
    MatCheckboxModule,
    MatCardModule,
    BrowserAnimationsModule,
    MatInputModule,
    MatListModule,
    MatButtonModule,
    MatTableModule
  ],
  providers: [ResultsService],
  bootstrap: [AppComponent]
})
export class AppModule { }
