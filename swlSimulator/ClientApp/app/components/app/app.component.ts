import { Component } from '@angular/core';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { Http } from '@angular/http'

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})

export class AppComponent {

constructor(private httpService: Http) { }
apiService: string[] = [];
ngOnInit() {
    this.httpService.get('/api/Service').subscribe(values => {
        this.apiService = values.json() as string[];
    });
}
}