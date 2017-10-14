import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Configuration } from './app/app.constants'
import { HubConnection } from '@aspnet/signalr-client';
import { Http } from '@angular/http'

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})

export class HomeComponent implements OnInit {
    private hubConnection: HubConnection;
    public async: any;
    message = '';
    messages: string[] = [];


    constructor(
        private httpService: Http) {
    }

    apiValues: string[] = [];
    public sendMessage(): void {
        const data = `Sent: ${this.message}`;

        this.hubConnection.invoke('Send', data);
        this.messages.push(data);
    }

    ngOnInit() {


        // Below is webAPI

        this.httpService.get('/api/values').subscribe((values: any) => {
            this.apiValues = values.json() as string[];
        });


        // Below is SingalR.

        this.hubConnection = new HubConnection('/loopy');

        this.hubConnection.on('Send', (data: any) => {
            const received = `Received: ${data}`;
            this.messages.push(received);
        });

        this.hubConnection.start()
            .then(() => {
                console.log('Hub connection started')
            })
            .catch(err => {
                console.log('Error while establishing connection')
            });

    }

}
